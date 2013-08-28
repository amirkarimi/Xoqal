#region License
// ImportExportException.cs
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

namespace Xoqal.ExportImport
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Represents import or export errors.
    /// </summary>
    [Serializable]
    public class ImportExportException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImportExportException"/> class.
        /// </summary>
        public ImportExportException() 
        { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportExportException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ImportExportException(string message) : base(message) 
        { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportExportException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public ImportExportException(string message, Exception inner) 
            : base(message, inner) 
        { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportExportException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected ImportExportException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) 
        { 
        }
    }
}
