#region License
// DbContextUserRepository{T}.cs
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

namespace Xoqal.Security.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Data.EntityFramework;

    /// <summary>
    /// User repository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DbContextUserRepository<T> : DbContextRepository<T>, IUserRepository<T>
        where T : class, IUser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextUserRepository{T}" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public DbContextUserRepository(System.Data.Entity.DbContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Gets the query.
        /// </summary>
        public override IQueryable<T> Query
        {
            get { return base.Query.Include(u => u.Roles).Include(u => u.Roles.Select(r => r.RolesInPermissions)).OrderBy(u => u.UserName); }
        }

        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <returns>The current user if there was any logged in, otherwise null.</returns>
        public virtual T GetCurrentUser()
        {
            var userPrincipal = Authentication.Default.GetCurrentUserPrincipal();
            if (userPrincipal == null)
            {
                return null;
            }

            return this.GetUser(userPrincipal.Identity.Id);
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns></returns>
        public virtual T GetUser(Guid userId)
        {
            var user = this.Query.FirstOrDefault(u => u.Id == userId);
            return user;
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userName"> Name of the user. </param>
        /// <returns> </returns>
        public virtual T GetUser(string userName)
        {
            userName = userName.ToLower();
            var user = this.Query.FirstOrDefault(u => u.UserName.ToLower() == userName);
            return user;
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userName"> Name of the user. </param>
        /// <param name="password"> The password. </param>
        /// <returns> </returns>
        public virtual T GetUser(string userName, string password)
        {
            userName = userName.ToLower();
            var user = this.Query.FirstOrDefault(u => u.UserName.ToLower() == userName && u.Password == password);
            return user;
        }

        /// <summary>
        /// Determines whether the specified user name exists.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="excludedUserId">The excluded user ID.</param>
        /// <returns>
        ///   <c>true</c> if the user name exists, otherwise, <c>false</c>.
        /// </returns>
        public bool UserNameExists(string userName, Guid? excludedUserId = null)
        {
            if (excludedUserId.HasValue)
            {
                return base.Query.Any(u => u.UserName.ToLower() == userName.ToLower() && u.IsActive && (u.Id != excludedUserId));
            }

            return base.Query.Any(u => u.UserName.ToLower() == userName.ToLower() && u.IsActive);
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="Xoqal.Security.UserValidationException"></exception>
        public override void Add(T entity)
        {
            this.CheckValidationForAdd(entity);
            base.Add(entity);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="Xoqal.Security.UserValidationException"></exception>
        public override void Update(T entity)
        {
            this.CheckValidationForUpdate(entity);
            base.Update(entity);
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns></returns>
        IUser IUserRepository.GetUser(Guid userId)
        {
            return this.GetUser(userId);
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        IUser IUserRepository.GetUser(string userName)
        {
            return this.GetUser(userName);
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        IUser IUserRepository.GetUser(string userName, string password)
        {
            return this.GetUser(userName, password);
        }

        /// <summary>
        /// Checks the user validation for update.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="Xoqal.Security.UserValidationException"></exception>
        private void CheckValidationForUpdate(T entity)
        {
            if (string.IsNullOrWhiteSpace(entity.UserName) && entity.UserName.Contains(' '))
            {
                throw new UserValidationException(UserValidationError.InvalidUserName);
            }
            
            if (this.Context.Entry(entity).Property(e => e.UserName).IsModified &&
                this.UserNameExists(entity.UserName, entity.Id == default(Guid) ? null : (Guid?)entity.Id))
            {
                throw new UserValidationException(UserValidationError.DuplicatedUserName);
            }
        }

        /// <summary>
        /// Checks the user validation for add.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="Xoqal.Security.UserValidationException"></exception>
        private void CheckValidationForAdd(T entity)
        {
            if (string.IsNullOrWhiteSpace(entity.UserName) && entity.UserName.Contains(' '))
            {
                throw new UserValidationException(UserValidationError.InvalidUserName);
            }

            if (this.UserNameExists(entity.UserName, entity.Id == default(Guid) ? null : (Guid?)entity.Id))
            {
                throw new UserValidationException(UserValidationError.DuplicatedUserName);
            }
        }
    }
}
