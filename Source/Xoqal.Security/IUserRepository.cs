#region License
// IUserRepository.cs
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
    /// Represents the user repository.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IUser GetUser(Guid userId);

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        IUser GetUser(string userName);

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        IUser GetUser(string userName, string password);

        /// <summary>
        /// Determines whether the specified user name exists.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="excludedUserId">The excluded user ID.</param>
        /// <returns>
        /// <c>true</c> if the user name exists, otherwise, <c>false</c>.
        /// </returns>
        bool UserNameExists(string userName, Guid? excludedUserId = null);
    }
}
