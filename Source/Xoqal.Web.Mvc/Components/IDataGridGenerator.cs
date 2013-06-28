#region License
// IDataGridGenerator.cs
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
    using Xoqal.Web.Mvc.Models;

    /// <summary>
    /// Represents a data grid generator.
    /// </summary>
    public interface IDataGridGenerator<TModel>
    {
        /// <summary>
        /// Gets the HTML string.
        /// </summary>
        /// <param name="paginatedData">The paginated data.</param>
        /// <param name="columns">The columns.</param>
        /// <param name="tableHtmlAttributes">The table HTML attributes.</param>
        /// <param name="rowHtmlAttributes">The row HTML attributes.</param>
        /// <param name="ascendingGlyph">The ascending glyph.</param>
        /// <param name="descendingGlyph">The descending glyph.</param>
        /// <param name="sortLinkGenerator">The sort link generator.</param>
        /// <returns></returns>
        IHtmlString GetHtmlString(IPaginated<TModel> paginatedData, DataGridColumnCollection<TModel> columns, object tableHtmlAttributes, Func<TModel, object> rowHtmlAttributes, HtmlString ascendingGlyph, HtmlString descendingGlyph, Func<DataGridColumn<TModel>, string> sortLinkGenerator);
    }
}
