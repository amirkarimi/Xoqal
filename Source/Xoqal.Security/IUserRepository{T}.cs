#region License
// IUserRepository{T}.cs
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Xoqal.Data;

    /// <summary>
    /// Represents the user repository.
    /// </summary>
    public interface IUserRepository<T> : IUserRepository
        where T : class, IUser
    {
        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <returns>The current user if there was any logged in, otherwise null.</returns>
        T GetCurrentUser();

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        new T GetUser(Guid userId);

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        new T GetUser(string userName);

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        new T GetUser(string userName, string password);
    }
}
