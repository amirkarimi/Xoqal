#region License
// NameOf.cs
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

namespace Xoqal.Data.EntityFramework.Linq
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;

    /// <summary>
    /// Used as a helper for EntityFrameworkExtensions and maybe others
    /// </summary>
    /// <typeparam name="T"> T </typeparam>
    /// <remarks>
    /// Created by A. Karimi (karimi@dev-frame.com)
    /// </remarks>
    public static class NameOf<T>
    {
        /// <summary>
        /// Properties the specified expression.
        /// </summary>
        /// <typeparam name="TProp"> The type of the prop. </typeparam>
        /// <param name="expr"> The expression.</param>
        /// <returns> The text format of the expression </returns>
        public static string Property<TProp>(Expression<Func<T, TProp>> expr)
        {
            var body = expr.Body as MemberExpression;
            if (body == null)
            {
                throw new ArgumentException("Parameter expr must be a memberexpression");
            }

            string name = body.ToString();

            // Remove first parameter
            int index = name.IndexOf(".");
            if (index != -1)
            {
                name = name.Substring(index + 1);
            }

            return name;
        }
    }
}
