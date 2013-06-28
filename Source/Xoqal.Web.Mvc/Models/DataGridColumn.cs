#region License
// DataGridColumn.cs
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
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a data grid column.
    /// </summary>
    public class DataGridColumn<TModel>
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="DataGridColumn{TModel}"/> class from being created.
        /// </summary>
        private DataGridColumn()
        {
        }

        /// <summary>
        /// Gets the title.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the sort expression.
        /// </summary>
        public LambdaExpression SortExpression { get; private set; }

        /// <summary>
        /// Gets the value selector.
        /// </summary>
        public Func<TModel, object> ValueSelector { get; private set; }

        /// <summary>
        /// Gets the this column HTML attributes selector.
        /// </summary>
        public Func<TModel, object> HtmlAttributesSelector { get; private set; }

        /// <summary>
        /// Gets or sets the HTML attributes.
        /// </summary>
        public object HtmlAttributes { get; private set; }

        /// <summary>
        /// Creates a new instance of <see cref="DataGridColumn{TModel}"/>.
        /// </summary>
        /// <returns></returns>
        public static DataGridColumn<TModel> Create()
        {
            return new DataGridColumn<TModel>();
        }

        /// <summary>
        /// Creates a new instance of <see cref="DataGridColumn{TModel}"/>.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns></returns>
        public static DataGridColumn<TModel> Create(string title)
        {
            return new DataGridColumn<TModel> { Title = title };
        }

        /// <summary>
        /// Sets the value selector.
        /// </summary>
        /// <param name="valueSelector">The value selector.</param>
        /// <returns></returns>
        public DataGridColumn<TModel> WithValue(Func<TModel, object> valueSelector)
        {
            this.ValueSelector = valueSelector;
            return this;
        }

        /// <summary>
        /// Sets the sort expression.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        public DataGridColumn<TModel> WithSort<TValue>(Expression<Func<TModel, TValue>> sortExpression)
        {
            this.SortExpression = sortExpression;
            return this;
        }

        /// <summary>
        /// Sets the HTML attributes.
        /// </summary>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public DataGridColumn<TModel> WithHtmlAttributes(object htmlAttributes)
        {
            this.HtmlAttributes = htmlAttributes;
            return this;
        }

        /// <summary>
        /// Sets the HTML attributes.
        /// </summary>
        /// <param name="htmlAttributesSelector">The HTML attributes selector.</param>
        /// <returns></returns>
        public DataGridColumn<TModel> WithHtmlAttributes(Func<TModel, object> htmlAttributesSelector)
        {
            this.HtmlAttributesSelector = htmlAttributesSelector;
            return this;
        }
    }
}
