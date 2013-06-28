#region License
// MD5PasswordHasher.cs
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
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Represents MD5 algorithm for hashing the passwords.
    /// </summary>
    public class MD5PasswordHasher : IPasswordHasher
    {
        #region IPasswordHasher Members

        /// <summary>
        /// Hashes the specified password.
        /// </summary>
        /// <param name="password"> The password. </param>
        /// <returns> </returns>
        public string Hash(string password)
        {
            byte[] hash = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }

        #endregion
    }
}
