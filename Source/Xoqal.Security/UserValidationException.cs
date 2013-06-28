#region License
// UserValidationException.cs
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
    using System.Runtime.Serialization;
    using System.Text;

    /// <summary>
    /// The exception that is thrown for invalid user insert or update.
    /// </summary>
    [Serializable]
    public class UserValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserValidationException" /> class.
        /// </summary>
        /// <param name="userValidationError">The user validation error.</param>
        public UserValidationException(UserValidationError userValidationError)
        {
            this.UserValidationError = userValidationError;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserValidationException" /> class.
        /// </summary>
        /// <param name="userValidationError">The user validation error.</param>
        /// <param name="message">The message.</param>
        public UserValidationException(UserValidationError userValidationError, string message)
            : base(message)
        {
            this.UserValidationError = userValidationError;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserValidationException" /> class.
        /// </summary>
        /// <param name="userValidationError">The user validation error.</param>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public UserValidationException(UserValidationError userValidationError, string message, Exception inner)
            : base(message, inner)
        {
            this.UserValidationError = userValidationError;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserValidationException" /> class.
        /// </summary>
        /// <param name="userValidationError">The user validation error.</param>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected UserValidationException(UserValidationError userValidationError, SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.UserValidationError = userValidationError;
        }

        /// <summary>
        /// Gets or sets the user validation error.
        /// </summary>
        /// <value>
        /// The user validation error.
        /// </value>
        public UserValidationError UserValidationError { get; set; }
    }
}
