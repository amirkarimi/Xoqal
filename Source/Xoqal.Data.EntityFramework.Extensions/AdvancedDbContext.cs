#region License
// AdvancedDbContext.cs
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
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;
    using Xoqal.Core.Models;

    /// <summary>
    /// Represents a <see cref="DbContext" /> type that have more functionality to set some entity properties automatically.
    /// </summary>
    public class AdvancedDbContext : DbContext
    {
        /// <summary>
        /// Initialized a new instance of the <see cref="AdvancedDbContext"/> class.
        /// </summary>
        public AdvancedDbContext()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedDbContext"/> class.
        /// </summary>
        /// <param name="nameOrConnectinString"></param>
        public AdvancedDbContext(string nameOrConnectinString)
            : base(nameOrConnectinString)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedDbContext"/> class.
        /// </summary>
        /// <param name="model"></param>
        public AdvancedDbContext(System.Data.Entity.Infrastructure.DbCompiledModel model)
            : base(model)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedDbContext" /> class.
        /// </summary>
        /// <param name="nameOrConnectionString">Either the database name or a connection string.</param>
        /// <param name="model">The model that will back this context.</param>
        public AdvancedDbContext(string nameOrConnectionString, System.Data.Entity.Infrastructure.DbCompiledModel model)
            : base(nameOrConnectionString, model)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedDbContext"/> class.
        /// </summary>
        /// <param name="existingConnection"></param>
        /// <param name="contextOwnsConnection"></param>
        public AdvancedDbContext(System.Data.Common.DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedDbContext"/> class.
        /// </summary>
        /// <param name="objectContext"></param>
        /// <param name="dbContextOwnsObjectContext"></param>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed.")]
        public AdvancedDbContext(System.Data.Entity.Core.Objects.ObjectContext objectContext, bool dbContextOwnsObjectContext)
            : base(objectContext, dbContextOwnsObjectContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedDbContext"/> class.
        /// </summary>
        /// <param name="existingConnection"></param>
        /// <param name="model"></param>
        /// <param name="contextOwnsConnection"></param>
        public AdvancedDbContext(System.Data.Common.DbConnection existingConnection, System.Data.Entity.Infrastructure.DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
        }

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>
        /// The number of objects written to the underlying database.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException"/>
        public override int SaveChanges()
        {
            // Set some fields which can automatically filled
            foreach (var entry in this.ChangeTracker.Entries().Where(e => e.State == System.Data.Entity.EntityState.Added).ToList())
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
                    var currentUser = Security.Authentication.Default.GetCurrentUserPrincipal(false);
                    if (entityWithCreateUser.CreateUserId == null && currentUser != null)
                    {
                        entityWithCreateUser.CreateUserId = currentUser.Identity.Id;
                    }
                }

                var entityWithLastUpdateUser = entry.Entity as ILastUpdateUser;
                if (entityWithLastUpdateUser != null)
                {
                    var currentUser = Security.Authentication.Default.GetCurrentUserPrincipal(false);
                    if (entityWithLastUpdateUser.LastUpdateUserId == null && currentUser != null)
                    {
                        entityWithLastUpdateUser.LastUpdateUserId = entityWithCreateUser == null
                            ? currentUser.Identity.Id
                            : entityWithCreateUser.CreateUserId;
                    }
                }
            }

            foreach (var entry in this.ChangeTracker.Entries().Where(e => e.State == System.Data.Entity.EntityState.Modified).ToList())
            {
                var entityWithLastUpdateTime = entry.Entity as ILastUpdateTime;
                if (entityWithLastUpdateTime != null)
                {
                    entityWithLastUpdateTime.LastUpdateTime = DateTime.Now;
                }
                
                var entityWithLastUpdateUser = entry.Entity as ILastUpdateUser;
                if (entityWithLastUpdateUser != null)
                {
                    var currentUser = Security.Authentication.Default.GetCurrentUserPrincipal(false);
                    entityWithLastUpdateUser.LastUpdateUserId = currentUser != null ? currentUser.Identity.Id : (Guid?)null;
                }
            }

            return base.SaveChanges();
        }
    }
}
