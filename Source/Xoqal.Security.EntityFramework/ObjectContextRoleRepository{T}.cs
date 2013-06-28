#region License
// ObjectContextRoleRepository{T}.cs
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

    /// <summary>
    /// Role repository.
    /// </summary>
    public class ObjectContextRoleRepository<T> : ObjectContextRepository<T>, IRoleRepository<T>
        where T : class, IRole
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectContextRoleRepository{T}" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ObjectContextRoleRepository(ObjectContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Gets the query.
        /// </summary>
        public override IQueryable<T> Query
        {
            get { return base.Query.OrderBy(r => r.Name); }
        }

        /// <summary>
        /// Determines whether is role name duplicated, the specified role name.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <param name="excludedRoleId">The excluded role id.</param>
        /// <returns>
        /// <c>true</c> if is role name duplicated, the specified role name; otherwise, <c>false</c>.
        /// </returns>
        public bool CheckRoleNameDuplicate(string roleName, Guid? excludedRoleId = null)
        {
            if (excludedRoleId.HasValue)
            {
                return base.Query.Any(r => r.Name.ToLower() == roleName.ToLower() && (r.Id != excludedRoleId));
            }
            else
            {
                return base.Query.Any(r => r.Name.ToLower() == roleName.ToLower());
            }
        }
    }
}
