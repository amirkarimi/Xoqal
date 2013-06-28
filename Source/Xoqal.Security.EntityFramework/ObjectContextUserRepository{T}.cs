#region License
// ObjectContextUserRepository{T}.cs
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
    using System.Data.Objects;
    using System.Linq;
    using Data.EntityFramework;
    using Data.EntityFramework.Linq;

    /// <summary>
    /// User repository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectContextUserRepository<T> : ObjectContextRepository<T>, IUserRepository<T>
        where T : class, IUser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectContextUserRepository{T}" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ObjectContextUserRepository(ObjectContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Gets the query.
        /// </summary>
        public override IQueryable<T> Query
        {
            get { return base.Query.OrderBy(u => u.UserName).Include(u => u.Roles).Include("Roles.RolesInPermissions"); }
        }

        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <returns>The current user if there was any logged in, otherwise null.</returns>
        public T GetCurrentUser()
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
        public T GetUser(Guid userId)
        {
            return this.Query.FirstOrDefault(u => u.Id == userId);
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userName"> Name of the user. </param>
        /// <returns> </returns>
        public T GetUser(string userName)
        {
            userName = userName.ToLower();
            return this.Query.FirstOrDefault(u => u.UserName.ToLower() == userName);
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userName"> Name of the user. </param>
        /// <param name="password"> The password. </param>
        /// <returns> </returns>
        public T GetUser(string userName, string password)
        {
            userName = userName.ToLower();
            return this.Query.FirstOrDefault(u => u.UserName.ToLower() == userName && u.Password == password);
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
            this.CheckValidation(entity);
            base.Add(entity);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="Xoqal.Security.UserValidationException"></exception>
        public override void Update(T entity)
        {
            this.CheckValidation(entity);
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
        /// Checks the validation.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="Xoqal.Security.UserValidationException"></exception>
        private void CheckValidation(T entity)
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
