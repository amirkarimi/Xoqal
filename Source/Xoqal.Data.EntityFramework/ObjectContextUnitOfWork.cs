#region License
// ObjectContextUnitOfWork.cs
// 
// Copyright (c) 2012 Xoqal.com
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

namespace Xoqal.Data.EntityFramework
{
    using System;
    using System.Data;
    using System.Data.Objects;

    /// <summary>
    /// Represents the base class for EF unit of work.
    /// </summary>
    public class ObjectContextUnitOfWork : IUnitOfWork
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectContextUnitOfWork" /> class.
        /// </summary>
        /// <param name="context"> The context. </param>
        public ObjectContextUnitOfWork(ObjectContext context)
        {
            this.Context = context;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the context.
        /// </summary>
        public ObjectContext Context { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Commits this instance.
        /// </summary>
        public virtual void Commit()
        {
            this.Context.SaveChanges();
        }

        /// <summary>
        /// Rolls the back.
        /// </summary>
        public virtual void RollBack()
        {
            foreach (ObjectStateEntry entity in this.Context.ObjectStateManager.GetObjectStateEntries(EntityState.Added))
            {
                this.Context.Detach(entity);
            }

            foreach (ObjectStateEntry entity in
                this.Context.ObjectStateManager.GetObjectStateEntries(EntityState.Deleted | EntityState.Modified))
            {
                entity.ChangeState(EntityState.Unchanged);
            }
        }

        #endregion

        #region Dipose Methods

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"> <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources. </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Context.Dispose();
            }
        }

        #endregion
    }
}
