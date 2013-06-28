#region License
// DbContextUnitOfWork.cs
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
    using System.Data.Entity;
    using System.Data.Entity.Validation;
    using System.Data.Objects;
    using System.Diagnostics;

    /// <summary>
    /// Represents the base class for EF unit of work.
    /// </summary>
    public class DbContextUnitOfWork : IUnitOfWork
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextUnitOfWork" /> class.
        /// </summary>
        /// <param name="context"> The context. </param>
        public DbContextUnitOfWork(DbContext context)
        {
            this.Context = context;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the context.
        /// </summary>
        public DbContext Context { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Commits this instance.
        /// </summary>
        public virtual void Commit()
        {
            try
            {
                this.Context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                Debug.WriteLine(this.GetDbEntityValidationErrorMessage(ex));
                throw;
            }
        }

        /// <summary>
        /// Rolls the back.
        /// </summary>
        public virtual void RollBack()
        {
            foreach (System.Data.Entity.Infrastructure.DbEntityEntry entity in this.Context.ChangeTracker.Entries())
            {
                if (entity.State == EntityState.Added)
                {
                    entity.State = EntityState.Detached;
                }

                if (entity.State == EntityState.Deleted || entity.State == EntityState.Modified)
                {
                    entity.State = EntityState.Unchanged;
                }
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

        #region Private Methods

        /// <summary>
        /// Get <see cref="DbEntityValidationException"/> validation error for each property.
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private string GetDbEntityValidationErrorMessage(DbEntityValidationException ex)
        {
            string message = string.Empty;
            foreach (var validationErrors in ex.EntityValidationErrors)
            {
                foreach (var validationError in validationErrors.ValidationErrors)
                {
                    message += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                }
            }

            return message;
        }
            
        #endregion 
    }
}
