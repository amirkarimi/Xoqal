#region License
// IRoleRepository.cs
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

namespace Xoqal.Security
{
    using System;
    using Xoqal.Data;

    /// <summary>
    /// Represents the role repository.
    /// </summary>
    public interface IRoleRepository
    {
        /// <summary>
        /// Determines whether is the specified role name duplicated.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <param name="excludedRoleId">The excluded role ID.</param>
        /// <returns>
        ///   <c>true</c> if the specified role name is duplicated; otherwise, <c>false</c>.
        /// </returns>
        bool CheckRoleNameDuplicate(string roleName, Guid? excludedRoleId = null);
    }
}
