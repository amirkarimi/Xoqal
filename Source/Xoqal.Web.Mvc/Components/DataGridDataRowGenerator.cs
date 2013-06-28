#region License
// DataGridDataRowGenerator.cs
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
    using Xoqal.Web.Mvc.Models;

    /// <summary>
    /// Data grid data row generator.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class DataGridDataRowGenerator<TModel> : IDataGridDataRowGenerator<TModel>
    {
        /// <summary>
        /// Gets the HTML string.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="columns">The columns.</param>
        /// <param name="rowHtmlAttributes">The row HTML attributes.</param>
        /// <returns></returns>
        public IHtmlString GetHtmlString(TModel model, DataGridColumnCollection<TModel> columns, Func<TModel, object> rowHtmlAttributes)
        {
            var dataRowTag = new TagBuilder("tr");

            // Add row attributes
            if (rowHtmlAttributes != null)
            {
                dataRowTag.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(rowHtmlAttributes(model)));
            }

            var sb = new StringBuilder();

            // Add row columns
            foreach (var column in columns)
            {
                var dataColumnTag = GetDataColumnTag(model, column);
                sb.AppendLine(dataColumnTag.ToString());
            }

            dataRowTag.InnerHtml = sb.ToString();
            return new HtmlString(dataRowTag.ToString());
        }

        /// <summary>
        /// Gets the data column tag.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="model">The model.</param>
        /// <param name="column">The column.</param>
        /// <returns></returns>
        internal static TagBuilder GetDataColumnTag(TModel model, DataGridColumn<TModel> column)
        {
            var dataColumnTag = new TagBuilder("td");
            object itemValue = column.ValueSelector(model);
            dataColumnTag.InnerHtml = itemValue == null ? string.Empty : itemValue.ToString();

            // First set the HTML attributes by its selector then if it doesn't exist use hard coded HTML attributes.
            if (column.HtmlAttributesSelector != null)
            {
                dataColumnTag.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(column.HtmlAttributesSelector(model)));
            }
            else if (column.HtmlAttributes != null)
            {
                dataColumnTag.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(column.HtmlAttributes));
            }

            return dataColumnTag;
        }
    }
}
