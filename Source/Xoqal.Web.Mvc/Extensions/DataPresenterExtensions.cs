#region License
// DataPresenterExtensions.cs
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

namespace Xoqal.Web.Mvc.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using Xoqal.Web.Mvc.Models;

    /// <summary>
    /// HTML extension to use in data presentation.
    /// </summary>
    public static class DataPresenterExtensions
    {
        #region Consts

        /// <summary>
        /// The Sort Expression Key.
        /// </summary>
        public const string SortExpressionKey = "sortExpression";

        #endregion

        #region Pager

        /// <summary>
        /// Generates a pager compatible with the Fx data presenter controller.
        /// </summary>
        /// <param name="htmlHelper"> The HTML helper. </param>
        /// <param name="paginatedData"> The paginated data. </param>
        /// <param name="firstLinkText"> </param>
        /// <param name="previousLinkText"> The previous link text. </param>
        /// <param name="nextLinkText"> The next link text. </param>
        /// <param name="currentPageFormat"> </param>
        /// <param name="htmlAttributes"> The HTML attributes. </param>
        /// <param name="lastLinkText"> </param>
        /// <returns> </returns>
        public static IHtmlString Pager(
            this HtmlHelper htmlHelper,
            IPaginated paginatedData,
            string firstLinkText = "<<",
            string previousLinkText = "<",
            string nextLinkText = ">",
            string lastLinkText = ">>",
            string currentPageFormat = "{0}/{1}",
            object htmlAttributes = null)
        {
            return Pager(
                htmlHelper,
                paginatedData,
                firstLinkText,
                previousLinkText,
                nextLinkText,
                lastLinkText,
                currentPageFormat,
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// Generates a pager compatible with <see cref="Controllers.PresenterController{TModel,TCriteria,TKey}" /> .
        /// </summary>
        /// <param name="htmlHelper"> The HTML helper. </param>
        /// <param name="paginatedData"> The paginated data. </param>
        /// <param name="firstLinkText"> </param>
        /// <param name="previousLinkText"> The previous link text. </param>
        /// <param name="nextLinkText"> The next link title. </param>
        /// <param name="currentPageFormat"> </param>
        /// <param name="htmlAttributes"> The HTML attributes. </param>
        /// <param name="lastLinkText"> </param>
        /// <returns> </returns>
        public static IHtmlString Pager(
            this HtmlHelper htmlHelper,
            IPaginated paginatedData,
            string firstLinkText = "<<",
            string previousLinkText = "<",
            string nextLinkText = ">",
            string lastLinkText = ">>",
            string currentPageFormat = "{0}/{1}",
            IDictionary<string, object> htmlAttributes = null)
        {
            return Pager(
                paginatedData,
                (linkText, routeValues, pageLinkHtmlAttributes) =>
                    htmlHelper.PersistRouteLink(linkText, routeValues, pageLinkHtmlAttributes, false),
                firstLinkText,
                previousLinkText,
                nextLinkText,
                lastLinkText,
                currentPageFormat,
                htmlAttributes);
        }

        #endregion

        #region SortLink

        /// <summary>
        /// Generates a link for sorting operations compatible with <see cref="Controllers.PresenterController{TModel,TCriteria,TKey}" /> .
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="ascendingGlyph">The ascending glyph.</param>
        /// <param name="descendingGlyph">The descending glyph.</param>
        /// <returns></returns>
        public static IHtmlString SortLink<TModel, TValue>(
            this HtmlHelper htmlHelper,
            string linkText,
            Expression<Func<TModel, TValue>> sortExpression,
            HtmlString ascendingGlyph = null,
            HtmlString descendingGlyph = null)
        {
            return SortLink(htmlHelper, new HtmlString(linkText), sortExpression, ascendingGlyph, descendingGlyph);
        }

        /// <summary>
        /// Generates a link for sorting operations compatible with <see cref="Controllers.PresenterController{TModel,TCriteria,TKey}" /> .
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="ascendingGlyph">The ascending glyph.</param>
        /// <param name="descendingGlyph">The descending glyph.</param>
        /// <returns></returns>
        public static IHtmlString SortLink(
            this HtmlHelper htmlHelper,
            string linkText,
            LambdaExpression sortExpression,
            HtmlString ascendingGlyph = null,
            HtmlString descendingGlyph = null)
        {
            return SortLink(htmlHelper, new HtmlString(linkText), sortExpression, ascendingGlyph, descendingGlyph);
        }

        /// <summary>
        /// Generates a link for sorting operations compatible with <see cref="Controllers.PresenterController{TModel,TCriteria,TKey}" /> .
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="ascendingGlyph">The ascending glyph.</param>
        /// <param name="descendingGlyph">The descending glyph.</param>
        /// <returns></returns>
        public static IHtmlString SortLink<TModel, TValue>(
            this HtmlHelper htmlHelper,
            IHtmlString linkText,
            Expression<Func<TModel, TValue>> sortExpression,
            HtmlString ascendingGlyph = null,
            HtmlString descendingGlyph = null)
        {
            return SortLink(htmlHelper, linkText, (LambdaExpression)sortExpression, ascendingGlyph, descendingGlyph);
        }

        /// <summary>
        /// Generates a link for sorting operations compatible with <see cref="Controllers.PresenterController{TModel,TCriteria,TKey}" /> .
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="ascendingGlyph">The ascending glyph.</param>
        /// <param name="descendingGlyph">The descending glyph.</param>
        /// <returns></returns>
        public static IHtmlString SortLink<TModel>(
            this HtmlHelper htmlHelper,
            string linkText,
            Expression<Func<TModel, object>> sortExpression,
            HtmlString ascendingGlyph = null,
            HtmlString descendingGlyph = null)
        {
            return SortLink(htmlHelper, linkText, (LambdaExpression)sortExpression, ascendingGlyph, descendingGlyph);
        }

        /// <summary>
        /// Generates a link for sorting operations compatible with <see cref="Controllers.PresenterController{TModel,TCriteria,TKey}" /> .
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="ascendingGlyph">The ascending glyph.</param>
        /// <param name="descendingGlyph">The descending glyph.</param>
        /// <returns></returns>
        public static IHtmlString SortLink(
            this HtmlHelper htmlHelper,
            IHtmlString linkText,
            LambdaExpression sortExpression,
            HtmlString ascendingGlyph = null,
            HtmlString descendingGlyph = null)
        {
            return SortLink(htmlHelper, linkText, ExpressionHelper.GetExpressionText(sortExpression), ascendingGlyph, descendingGlyph);
        }

        /// <summary>
        /// Generates a link for sorting operations compatible with <see cref="Controllers.PresenterController{TModel,TCriteria,TKey}" /> .
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="ascendingGlyph">The ascending glyph.</param>
        /// <param name="descendingGlyph">The descending glyph.</param>
        /// <returns></returns>
        public static IHtmlString SortLink(
            this HtmlHelper htmlHelper,
            string linkText,
            string sortExpression,
            HtmlString ascendingGlyph = null,
            HtmlString descendingGlyph = null)
        {
            return SortLink(htmlHelper, linkText, sortExpression, null, ascendingGlyph, descendingGlyph);
        }

        /// <summary>
        /// Generates a link for sorting operations compatible with <see cref="Controllers.PresenterController{TModel,TCriteria,TKey}" /> .
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <param name="ascendingGlyph">The ascending glyph.</param>
        /// <param name="descendingGlyph">The descending glyph.</param>
        /// <returns></returns>
        public static IHtmlString SortLink(
            this HtmlHelper htmlHelper,
            string linkText,
            string sortExpression,
            object htmlAttributes = null,
            HtmlString ascendingGlyph = null,
            HtmlString descendingGlyph = null)
        {
            return SortLink(
                htmlHelper,
                linkText,
                sortExpression,
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes),
                ascendingGlyph,
                descendingGlyph);
        }

        /// <summary>
        /// Generates a link for sorting operations compatible with <see cref="Controllers.PresenterController{TModel,TCriteria,TKey}" /> .
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <param name="ascendingGlyph">The ascending glyph.</param>
        /// <param name="descendingGlyph">The descending glyph.</param>
        /// <returns></returns>
        public static IHtmlString SortLink(
            this HtmlHelper htmlHelper,
            string linkText,
            string sortExpression,
            IDictionary<string, object> htmlAttributes = null,
            HtmlString ascendingGlyph = null,
            HtmlString descendingGlyph = null)
        {
            return SortLink(htmlHelper, new HtmlString(linkText), sortExpression, htmlAttributes, ascendingGlyph, descendingGlyph);
        }

        /// <summary>
        /// Generates a link for sorting operations compatible with <see cref="Controllers.PresenterController{TModel,TCriteria,TKey}" /> .
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="ascendingGlyph">The ascending glyph.</param>
        /// <param name="descendingGlyph">The descending glyph.</param>
        /// <returns></returns>
        public static IHtmlString SortLink(
            this HtmlHelper htmlHelper,
            IHtmlString linkText,
            string sortExpression,
            HtmlString ascendingGlyph = null,
            HtmlString descendingGlyph = null)
        {
            return SortLink(htmlHelper, linkText, sortExpression, null, ascendingGlyph, descendingGlyph);
        }

        /// <summary>
        /// Generates a link for sorting operations compatible with <see cref="Controllers.PresenterController{TModel,TCriteria,TKey}" /> .
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <param name="ascendingGlyph">The ascending glyph.</param>
        /// <param name="descendingGlyph">The descending glyph.</param>
        /// <returns></returns>
        public static IHtmlString SortLink(
            this HtmlHelper htmlHelper,
            IHtmlString linkText,
            string sortExpression,
            object htmlAttributes = null,
            HtmlString ascendingGlyph = null,
            HtmlString descendingGlyph = null)
        {
            return SortLink(
                htmlHelper,
                linkText,
                sortExpression,
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes),
                ascendingGlyph,
                descendingGlyph);
        }

        /// <summary>
        /// Generates a link for sorting operations compatible with <see cref="Controllers.PresenterController{TModel,TCriteria,TKey}" /> .
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <param name="ascendingGlyph">The ascending glyph.</param>
        /// <param name="descendingGlyph">The descending glyph.</param>
        /// <returns></returns>
        public static IHtmlString SortLink(
            this HtmlHelper htmlHelper,
            IHtmlString linkText,
            string sortExpression,
            IDictionary<string, object> htmlAttributes = null,
            HtmlString ascendingGlyph = null,
            HtmlString descendingGlyph = null)
        {
            HtmlString sortGlyph;
            string processedSortExpression = GenerateSortExpression(
                htmlHelper.ViewContext, sortExpression, ascendingGlyph, descendingGlyph, out sortGlyph);

            return htmlHelper.PersistRouteLink(
                string.Format("{0} {1}", linkText, sortGlyph == null ? string.Empty : sortGlyph.ToString()),
                null,
                new { sortExpression = processedSortExpression },
                htmlAttributes,
                false);
        }

        #endregion

        #region DataGrid

        /// <summary>
        /// Renders a data grid.
        /// </summary>
        /// <typeparam name="TModel">The model.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="paginatedData">The paginated data.</param>
        /// <param name="columns">The columns.</param>
        /// <param name="tableHtmlAttributes">The table HTML attributes.</param>
        /// <param name="rowHtmlAttributes">The row HTML attributes.</param>
        /// <param name="ascendingGlyph">The ascending glyph.</param>
        /// <param name="descendingGlyph">The descending glyph.</param>
        /// <returns></returns>
        public static IHtmlString DataGrid<TModel>(
            this HtmlHelper htmlHelper, 
            IPaginated<TModel> paginatedData, 
            DataGridColumnCollection<TModel> columns, 
            object tableHtmlAttributes = null, 
            Func<TModel, object> rowHtmlAttributes = null, 
            HtmlString ascendingGlyph = null, 
            HtmlString descendingGlyph = null)
        {
            return DataGrid(
                paginatedData,
                columns,
                tableHtmlAttributes,
                rowHtmlAttributes,
                ascendingGlyph,
                descendingGlyph, 
                column => htmlHelper.SortLink(column.Title, column.SortExpression).ToString());
        }

        /// <summary>
        /// Renders a data grid.
        /// </summary>
        /// <typeparam name="TModel">The model.</typeparam>
        /// <param name="paginatedData">The paginated data.</param>
        /// <param name="columns">The columns.</param>
        /// <param name="tableHtmlAttributes">The table HTML attributes.</param>
        /// <param name="rowHtmlAttributes">The row HTML attributes.</param>
        /// <param name="ascendingGlyph">The ascending glyph.</param>
        /// <param name="descendingGlyph">The descending glyph.</param>
        /// <param name="sortLinkGenerator">The sort link generator.</param>
        /// <returns></returns>
        internal static IHtmlString DataGrid<TModel>(
            IPaginated<TModel> paginatedData, 
            DataGridColumnCollection<TModel> columns, 
            object tableHtmlAttributes, 
            Func<TModel, object> rowHtmlAttributes, 
            HtmlString ascendingGlyph, 
            HtmlString descendingGlyph, 
            Func<DataGridColumn<TModel>, string> sortLinkGenerator)
        {
            Components.IDataGridGenerator<TModel> dataGridGenerator = 
                new Components.DataGridGenerator<TModel>(new Components.DataGridHeaderRowGenerator<TModel>(), new Components.DataGridDataRowGenerator<TModel>());

            return dataGridGenerator.GetHtmlString(
                paginatedData,
                columns,
                tableHtmlAttributes,
                rowHtmlAttributes,
                ascendingGlyph,
                descendingGlyph, 
                sortLinkGenerator);
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Generates a pager compatible with <see cref="Controllers.PresenterController{TModel,TCriteria}" /> .
        /// </summary>
        /// <param name="paginatedData"> </param>
        /// <param name="routeLinkGenerator"> </param>
        /// <param name="firstLinkText"> </param>
        /// <param name="previousLinkText"> </param>
        /// <param name="nextLinkText"> </param>
        /// <param name="lastLinkText"> </param>
        /// <param name="currentPageFormat"> </param>
        /// <param name="htmlAttributes"> </param>
        /// <returns> </returns>
        internal static HtmlString Pager(
            IPaginated paginatedData,
            Func<string, object, IDictionary<string, object>, IHtmlString> routeLinkGenerator,
            string firstLinkText,
            string previousLinkText,
            string nextLinkText,
            string lastLinkText,
            string currentPageFormat,
            IDictionary<string, object> htmlAttributes)
        {
            var sb = new StringBuilder();

            // First Page
            var firstAttributes = new Dictionary<string, object> { { "id", "firstPage" } };
            if (paginatedData.IsFirstPage)
            {
                sb.AppendLine(GenerateDisabledLink(firstLinkText, firstAttributes));
            }
            else
            {
                sb.AppendLine(routeLinkGenerator(firstLinkText, new { page = 1 }, firstAttributes).ToString());
            }

            // Previous Page
            var prevAttributes = new Dictionary<string, object> { { "id", "prevPage" } };
            if (paginatedData.IsFirstPage)
            {
                sb.AppendLine(GenerateDisabledLink(previousLinkText, prevAttributes));
            }
            else
            {
                sb.AppendLine(routeLinkGenerator(previousLinkText, new { page = paginatedData.PreviousPage }, prevAttributes).ToString());
            }

            // Current Page
            if (!string.IsNullOrWhiteSpace(currentPageFormat))
            {
                sb.AppendLine(
                    string.Format(
                        "<span id=\"currentPage\">{0}</span>",
                        string.Format(currentPageFormat, paginatedData.CurrentPage, paginatedData.PageCount)));
            }

            // Next Page
            var nextAttributes = new Dictionary<string, object> { { "id", "nextPage" } };
            if (paginatedData.IsLastPage)
            {
                sb.AppendLine(GenerateDisabledLink(nextLinkText, nextAttributes));
            }
            else
            {
                sb.AppendLine(routeLinkGenerator(nextLinkText, new { page = paginatedData.NextPage }, nextAttributes).ToString());
            }

            // Last Page
            var lastAttributes = new Dictionary<string, object> { { "id", "lastPage" } };
            if (paginatedData.IsLastPage)
            {
                sb.AppendLine(GenerateDisabledLink(lastLinkText, lastAttributes));
            }
            else
            {
                sb.AppendLine(routeLinkGenerator(lastLinkText, new { page = paginatedData.PageCount }, lastAttributes).ToString());
            }

            var pagerDiv = new TagBuilder("div") { InnerHtml = sb.ToString() };
            pagerDiv.MergeAttributes(htmlAttributes);
            return new HtmlString(pagerDiv.ToString());
        }

        /// <summary>
        /// Processes the given sort expression and generates the new link.
        /// </summary>
        /// <param name="viewContext"> </param>
        /// <param name="sortExpression"> </param>
        /// <param name="ascendingGlyph"> </param>
        /// <param name="descendingGlyph"> </param>
        /// <param name="sortGlyph"> </param>
        /// <returns> </returns>
        internal static string GenerateSortExpression(
            ViewContext viewContext, string sortExpression, HtmlString ascendingGlyph, HtmlString descendingGlyph, out HtmlString sortGlyph)
        {
            var existingSortExpression = (string)viewContext.RouteData.Values[SortExpressionKey];
            if (string.IsNullOrWhiteSpace(existingSortExpression))
            {
                existingSortExpression = viewContext.HttpContext.Request.QueryString[SortExpressionKey];
            }

            // Set sort glyph
            sortGlyph = GetSortGlyph(sortExpression, existingSortExpression, ascendingGlyph, descendingGlyph);

            // Make descending sort direction
            if (existingSortExpression == sortExpression)
            {
                sortExpression += " DESC";
            }

            return sortExpression;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the sort glyph according to the given parameters.
        /// </summary>
        /// <param name="sortExpression"> </param>
        /// <param name="existingSortExpression"> </param>
        /// <param name="ascendingGlyph"> </param>
        /// <param name="descendingGlyph"> </param>
        /// <returns> </returns>
        private static HtmlString GetSortGlyph(
            string sortExpression, string existingSortExpression, HtmlString ascendingGlyph, HtmlString descendingGlyph)
        {
            HtmlString sortGlyph = null;
            if (!string.IsNullOrWhiteSpace(existingSortExpression))
            {
                string originalSortExpression = existingSortExpression;
                bool isDescending = existingSortExpression.EndsWith(" DESC");
                if (isDescending)
                {
                    originalSortExpression = originalSortExpression.Substring(0, originalSortExpression.Length - 5);
                }

                if (originalSortExpression == sortExpression)
                {
                    if (isDescending)
                    {
                        // Descending
                        if (descendingGlyph == null)
                        {
                            // Set default value
                            descendingGlyph = new HtmlString("▲");
                        }

                        sortGlyph = descendingGlyph;
                    }
                    else
                    {
                        // Ascending
                        if (ascendingGlyph == null)
                        {
                            // Set default value
                            ascendingGlyph = new HtmlString("▼");
                        }

                        sortGlyph = ascendingGlyph;
                    }
                }
            }

            return sortGlyph;
        }

        /// <summary>
        /// Generates a disabled link.
        /// </summary>
        /// <param name="firstLinkText"> </param>
        /// <param name="attributes"> </param>
        /// <returns> </returns>
        private static string GenerateDisabledLink(string firstLinkText, IDictionary<string, object> attributes)
        {
            var spanTag = new TagBuilder("span") { InnerHtml = firstLinkText };
            spanTag.MergeAttributes(attributes);
            return spanTag.ToString();
        }

        #endregion
    }
}
