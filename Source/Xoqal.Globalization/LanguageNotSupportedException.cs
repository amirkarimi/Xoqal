#region License
// LanguageNotSupportedException.cs
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

namespace Xoqal.Globalization
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents the error that a language is not supported.
    /// </summary>
    [Serializable]
    public class LanguageNotSupportedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageNotSupportedException" /> class.
        /// </summary>
        /// <param name="cultureInfo"> The CultureInfo. </param>
        public LanguageNotSupportedException(CultureInfo cultureInfo)
        {
            this.RequestedCultureInfo = cultureInfo;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageNotSupportedException" /> class.
        /// </summary>
        /// <param name="message"> The message. </param>
        public LanguageNotSupportedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageNotSupportedException" /> class.
        /// </summary>
        /// <param name="message"> The message. </param>
        /// <param name="inner"> The inner exception. </param>
        public LanguageNotSupportedException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageNotSupportedException" /> class.
        /// </summary>
        /// <param name="info"> The serialization information. </param>
        /// <param name="context"> The streaming context. </param>
        protected LanguageNotSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Gets the requested CultureInfo which is not supported.
        /// </summary>
        public CultureInfo RequestedCultureInfo { get; private set; }
    }
}
