#region License
// ObjectContextRepository.cs
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

namespace Xoqal.Data.EntityFramework
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Metadata.Edm;
    using System.Data.Objects;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Linq.Expressions;
    using Linq;

    /// <summary>
    /// Represents the base class for a repository.
    /// </summary>
    /// <typeparam name="T"> Entity type. </typeparam>
    public abstract class ObjectContextRepository<T> : ObjectContextRepository<T, T> where T : class
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectContextRepository{T}" /> class.
        /// </summary>
        /// <param name="context"> The context. </param>
        public ObjectContextRepository(ObjectContext context)
            : base(context)
        {
        }

        #endregion
    }
}
