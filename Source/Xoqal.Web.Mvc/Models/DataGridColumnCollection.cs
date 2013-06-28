#region License
// DataGridColumnCollection.cs
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

namespace Xoqal.Web.Mvc.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a list of <see cref="DataGridColumnCollection{TModel}" /> objects.
    /// </summary>
    public class DataGridColumnCollection<TModel> : List<DataGridColumn<TModel>>
    {
        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="title"> The title. </param>
        /// <param name="sortExpression"> The sort expression. </param>
        /// <param name="valueSelector"> The value selector. </param>
        /// <param name="htmlAttributes"> The html attributes </param>
        public void Add<TValue>(
            string title,
            Func<TModel, object> valueSelector,
            Expression<Func<TModel, TValue>> sortExpression,
            object htmlAttributes = null)
        {
            this.Add(
                DataGridColumn<TModel>.Create(title)
                    .WithValue(valueSelector)
                    .WithSort(sortExpression)
                    .WithHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="title"> The title. </param>
        /// <param name="propertySelector"> The property selector. </param>
        /// <param name="htmlAttributes"> The html attributes </param>
        public void Add(string title, Func<TModel, object> propertySelector, object htmlAttributes = null)
        {
            this.Add(
                DataGridColumn<TModel>.Create(title)
                    .WithValue(propertySelector)
                    .WithHtmlAttributes(htmlAttributes));
        }
    }
}
