#region License
// AdvancedObjectContext.cs
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

namespace Xoqal.Data.EntityFramework.Extensions
{
    using System;
    using System.Data;
    using System.Data.EntityClient;
    using System.Data.Objects;
    using System.Linq;
    using Xoqal.Core.Models;
    using Xoqal.Security;

    /// <summary>
    /// Represents an <see cref="ObjectContext" /> type that have more functionality to set some entity properties automatically.
    /// </summary>
    public class AdvancedObjectContext : ObjectContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedObjectContext" /> class.
        /// </summary>
        /// <param name="connectionString"> The connection string. </param>
        public AdvancedObjectContext(string connectionString)
            : base(connectionString)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedObjectContext" /> class.
        /// </summary>
        /// <param name="connection"> The connection. </param>
        public AdvancedObjectContext(EntityConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedObjectContext" /> class.
        /// </summary>
        /// <param name="connectionString"> The connection string. </param>
        /// <param name="defaultContainerName"> Default name of the container. </param>
        public AdvancedObjectContext(string connectionString, string defaultContainerName)
            : base(connectionString, defaultContainerName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedObjectContext" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="defaultContainerName">The defaultContainerName.</param>
        public AdvancedObjectContext(EntityConnection connection, string defaultContainerName)
            : base(connection, defaultContainerName)
        {
        }

        /// <summary>
        /// Persists all updates to the data source with the specified <see cref="T:System.Data.Objects.SaveOptions" /> .
        /// </summary>
        /// <param name="options">A <see cref="T:System.Data.Objects.SaveOptions" /> value that determines the behavior of the operation.</param>
        /// <returns>
        /// The number of objects in an <see cref="F:System.Data.EntityState.Added" /> , <see cref="F:System.Data.EntityState.Modified" /> , or <see cref="F:System.Data.EntityState.Deleted" /> state when <see cref="M:System.Data.Objects.ObjectContext.SaveChanges" /> was called.
        /// </returns>
        /// <exception cref="T:System.Data.OptimisticConcurrencyException">An optimistic concurrency violation has occurred.</exception>
        public override int SaveChanges(SaveOptions options)
        {
            // Set some fields which can automatically filled
            foreach (ObjectStateEntry entry in this.ObjectStateManager.GetObjectStateEntries(EntityState.Added).ToList())
            {
                var entityWithGuidKey = entry.Entity as IGuidKey;
                if (entityWithGuidKey != null)
                {
                    if (entityWithGuidKey.Id == default(Guid))
                    {
                        entityWithGuidKey.Id = Guid.NewGuid();
                    }
                }

                var entityWithCreateTime = entry.Entity as ICreateTime;
                if (entityWithCreateTime != null)
                {
                    if (entityWithCreateTime.CreateTime == default(DateTime))
                    {
                        entityWithCreateTime.CreateTime = DateTime.Now;
                    }
                }

                var entityWithLastUpdateTime = entry.Entity as ILastUpdateTime;
                if (entityWithLastUpdateTime != null)
                {
                    if (entityWithLastUpdateTime.LastUpdateTime == default(DateTime))
                    {
                        entityWithLastUpdateTime.LastUpdateTime = entityWithCreateTime == null
                            ? DateTime.Now
                            : entityWithCreateTime.CreateTime;
                    }
                }

                var entityWithCreateUser = entry.Entity as ICreateUser;
                if (entityWithCreateUser != null)
                {
                    var currentUser = Authentication.Default.GetCurrentUserPrincipal(false);
                    if (entityWithCreateUser.CreateUserId == null && currentUser != null)
                    {
                        entityWithCreateUser.CreateUserId = currentUser.Identity.Id;
                    }
                }

                var entityWithLastUpdateUser = entry.Entity as ILastUpdateUser;
                if (entityWithLastUpdateUser != null)
                {
                    var currentUser = Authentication.Default.GetCurrentUserPrincipal(false);
                    if (entityWithLastUpdateUser.LastUpdateUserId == null && currentUser != null)
                    {
                        entityWithLastUpdateUser.LastUpdateUserId = entityWithCreateUser == null
                            ? currentUser.Identity.Id
                            : entityWithCreateUser.CreateUserId;
                    }
                }
            }

            foreach (ObjectStateEntry entry in this.ObjectStateManager.GetObjectStateEntries(EntityState.Modified).ToList())
            {
                var entityWithLastUpdateTime = entry.Entity as ILastUpdateTime;
                if (entityWithLastUpdateTime != null)
                {
                    entityWithLastUpdateTime.LastUpdateTime = DateTime.Now;
                }

                var entityWithLastUpdateUser = entry.Entity as ILastUpdateUser;
                if (entityWithLastUpdateUser != null)
                {
                    var currentUser = Authentication.Default.GetCurrentUserPrincipal(false);
                    entityWithLastUpdateUser.LastUpdateUserId = currentUser != null ? currentUser.Identity.Id : (Guid?)null;
                }
            }

            return base.SaveChanges(options);
        }
    }
}
