#region License
// DbContextRolesInPermissionRepository{T}.cs
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
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Entity;
    using Data.EntityFramework;

    /// <summary>
    /// RolesInPermission repository.
    /// </summary>
    public class DbContextRolesInPermissionRepository<T> : DbContextRepository<T>, IRolesInPermissionRepository<T>
        where T : class, IRolesInPermission
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextRolesInPermissionRepository{T}" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public DbContextRolesInPermissionRepository(DbContext context)
            : base(context)
        {
        }
    }
}
