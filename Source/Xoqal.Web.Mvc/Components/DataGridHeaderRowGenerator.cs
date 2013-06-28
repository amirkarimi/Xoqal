#region License
// DataGridHeaderRowGenerator.cs
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

namespace Xoqal.Web.Mvc.Components
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// Data grid header row generator.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class DataGridHeaderRowGenerator<TModel> : IDataGridHeaderRowGenerator<TModel>
    {
        /// <summary>
        /// Gets the HTML string.
        /// </summary>
        /// <param name="columns">The columns.</param>
        /// <param name="sortLinkGenerator">The sort link generator.</param>
        /// <returns></returns>
        public System.Web.IHtmlString GetHtmlString(Models.DataGridColumnCollection<TModel> columns, Func<Models.DataGridColumn<TModel>, string> sortLinkGenerator)
        {
            var headerRowTag = new TagBuilder("tr");

            var sb = new StringBuilder();
            foreach (var column in columns)
            {
                var columnHeaderTag = new TagBuilder("th");
                columnHeaderTag.InnerHtml = column.SortExpression != null ? sortLinkGenerator(column) : column.Title;
                sb.AppendLine(columnHeaderTag.ToString());
            }

            headerRowTag.InnerHtml = sb.ToString();
            return new HtmlString(headerRowTag.ToString());
        }
    }
}
