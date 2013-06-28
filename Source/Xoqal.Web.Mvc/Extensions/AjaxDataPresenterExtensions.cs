#region License
// AjaxDataPresenterExtensions.cs
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
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using Xoqal.Web.Mvc.Models;

    /// <summary>
    /// AJAX extensions to use in data presentation.
    /// </summary>
    public static class AjaxDataPresenterExtensions
    {
        #region Pager

        /// <summary>
        /// Generates a pager compatible with the <see cref="Controllers.PresenterController{TModel,TCriteria,TKey}" /> .
        /// </summary>
        /// <param name="ajaxHelper">The AJAX helper.</param>
        /// <param name="paginatedData">The paginated data.</param>
        /// <param name="ajaxOptions">The ajax options.</param>
        /// <param name="firstLinkText">The first link text.</param>
        /// <param name="previousLinkText">The previous link text.</param>
        /// <param name="nextLinkText">The next link text.</param>
        /// <param name="lastLinkText">The last link text.</param>
        /// <param name="currentPageFormat">The format of the current page indicator.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static IHtmlString Pager(
            this AjaxHelper ajaxHelper,
            IPaginated paginatedData,
            AjaxOptions ajaxOptions,
            string firstLinkText = "<<",
            string previousLinkText = "<",
            string nextLinkText = ">",
            string lastLinkText = ">>",
            string currentPageFormat = "{0}/{1}",
            object htmlAttributes = null)
        {
            return Pager(
                ajaxHelper,
                paginatedData,
                ajaxOptions,
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
        /// <param name="ajaxHelper">The AJAX helper.</param>
        /// <param name="paginatedData">The paginated data.</param>
        /// <param name="ajaxOptions">The ajax options.</param>
        /// <param name="firstLinkText">The first link text.</param>
        /// <param name="previousLinkText">The previous link text.</param>
        /// <param name="nextLinkText">The next link text.</param>
        /// <param name="lastLinkText">The last link text.</param>
        /// <param name="currentPageFormat">The format of the current page indicator.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static IHtmlString Pager(
            this AjaxHelper ajaxHelper,
            IPaginated paginatedData,
            AjaxOptions ajaxOptions,
            string firstLinkText = "<<",
            string previousLinkText = "<",
            string nextLinkText = ">",
            string lastLinkText = ">>",
            string currentPageFormat = "{0}/{1}",
            IDictionary<string, object> htmlAttributes = null)
        {
            return DataPresenterExtensions.Pager(
                paginatedData,
                (linkText, routeValues, pageLinkHtmlAttributes) =>
                    ajaxHelper.PersistRouteLink(linkText, routeValues, ajaxOptions, pageLinkHtmlAttributes, false),
                firstLinkText,
                previousLinkText,
                nextLinkText,
                lastLinkText,
                currentPageFormat,
                htmlAttributes);
        }

        #endregion

        #region Sort

        /// <summary>
        /// Generates a link for sorting operations compatible with <see cref="Controllers.PresenterController{TModel,TCriteria,TKey}" /> .
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="ajaxHelper">The AJAX helper.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="ajaxOptions">The ajax options.</param>
        /// <param name="ascendingGlyph">The ascending glyph.</param>
        /// <param name="descendingGlyph">The descending glyph.</param>
        /// <returns></returns>
        public static IHtmlString SortLink<TModel, TValue>(
            this AjaxHelper ajaxHelper,
            string linkText,
            Expression<Func<TModel, TValue>> sortExpression,
            AjaxOptions ajaxOptions,
            HtmlString ascendingGlyph = null,
            HtmlString descendingGlyph = null)
        {
            return SortLink(ajaxHelper, new HtmlString(linkText), sortExpression, ajaxOptions, ascendingGlyph, descendingGlyph);
        }

        /// <summary>
        /// Generates a link for sorting operations compatible with <see cref="Controllers.PresenterController{TModel,TCriteria,TKey}" /> .
        /// </summary>
        /// <param name="ajaxHelper">The AJAX helper.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="ajaxOptions">The ajax options.</param>
        /// <param name="ascendingGlyph">The ascending glyph.</param>
        /// <param name="descendingGlyph">The descending glyph.</param>
        /// <returns></returns>
        public static IHtmlString SortLink(
            this AjaxHelper ajaxHelper,
            string linkText,
            LambdaExpression sortExpression,
            AjaxOptions ajaxOptions,
            HtmlString ascendingGlyph = null,
            HtmlString descendingGlyph = null)
        {
            return SortLink(ajaxHelper, new HtmlString(linkText), sortExpression, ajaxOptions, ascendingGlyph, descendingGlyph);
        }

        /// <summary>
        /// Generates a link for sorting operations compatible with <see cref="Controllers.PresenterController{TModel,TCriteria,TKey}" /> .
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="ajaxHelper">The AJAX helper.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="ajaxOptions">The ajax options.</param>
        /// <param name="ascendingGlyph">The ascending glyph.</param>
        /// <param name="descendingGlyph">The descending glyph.</param>
        /// <returns></returns>
        public static IHtmlString SortLink<TModel, TValue>(
            this AjaxHelper ajaxHelper,
            IHtmlString linkText,
            Expression<Func<TModel, TValue>> sortExpression,
            AjaxOptions ajaxOptions,
            HtmlString ascendingGlyph = null,
            HtmlString descendingGlyph = null)
        {
            return SortLink(ajaxHelper, linkText, (LambdaExpression)sortExpression, ajaxOptions, ascendingGlyph, descendingGlyph);
        }

        /// <summary>
        /// Generates a link for sorting operations compatible with <see cref="Controllers.PresenterController{TModel,TCriteria,TKey}" /> .
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="ajaxHelper">The AJAX helper.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="ajaxOptions">The ajax options.</param>
        /// <param name="ascendingGlyph">The ascending glyph.</param>
        /// <param name="descendingGlyph">The descending glyph.</param>
        /// <returns></returns>
        public static IHtmlString SortLink<TModel>(
            this AjaxHelper ajaxHelper,
            string linkText,
            Expression<Func<TModel, object>> sortExpression,
            AjaxOptions ajaxOptions,
            HtmlString ascendingGlyph = null,
            HtmlString descendingGlyph = null)
        {
            return SortLink(ajaxHelper, linkText, (LambdaExpression)sortExpression, ajaxOptions, ascendingGlyph, descendingGlyph);
        }

        /// <summary>
        /// Generates a link for sorting operations compatible with <see cref="Controllers.PresenterController{TModel,TCriteria,TKey}" /> .
        /// </summary>
        /// <param name="ajaxHelper">The AJAX helper.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="ajaxOptions">The ajax options.</param>
        /// <param name="ascendingGlyph">The ascending glyph.</param>
        /// <param name="descendingGlyph">The descending glyph.</param>
        /// <returns></returns>
        public static IHtmlString SortLink(
            this AjaxHelper ajaxHelper,
            IHtmlString linkText,
            LambdaExpression sortExpression,
            AjaxOptions ajaxOptions,
            HtmlString ascendingGlyph = null,
            HtmlString descendingGlyph = null)
        {
            return SortLink(ajaxHelper, linkText, ExpressionHelper.GetExpressionText(sortExpression), ajaxOptions, ascendingGlyph, descendingGlyph);
        }

        /// <summary>
        /// Generates a link for sorting operations compatible with <see cref="Controllers.PresenterController{TModel,TCriteria,TKey}" /> .
        /// </summary>
        /// <param name="ajaxHelper">The AJAX helper.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="ajaxOptions">The ajax options.</param>
        /// <param name="ascendingGlyph">The ascending glyph.</param>
        /// <param name="descendingGlyph">The descending glyph.</param>
        /// <returns></returns>
        public static IHtmlString SortLink(
            this AjaxHelper ajaxHelper,
            string linkText,
            string sortExpression,
            AjaxOptions ajaxOptions,
            HtmlString ascendingGlyph = null,
            HtmlString descendingGlyph = null)
        {
            return SortLink(ajaxHelper, linkText, sortExpression, ajaxOptions, null, ascendingGlyph, descendingGlyph);
        }

        /// <summary>
        /// Generates a link for sorting operations compatible with <see cref="Controllers.PresenterController{TModel,TCriteria,TKey}" /> .
        /// </summary>
        /// <param name="ajaxHelper">The AJAX helper.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="ajaxOptions">The ajax options.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <param name="ascendingGlyph">The ascending glyph.</param>
        /// <param name="descendingGlyph">The descending glyph.</param>
        /// <returns></returns>
        public static IHtmlString SortLink(
            this AjaxHelper ajaxHelper,
            string linkText,
            string sortExpression,
            AjaxOptions ajaxOptions,
            object htmlAttributes = null,
            HtmlString ascendingGlyph = null,
            HtmlString descendingGlyph = null)
        {
            return SortLink(
                ajaxHelper,
                linkText,
                sortExpression,
                ajaxOptions,
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes),
                ascendingGlyph,
                descendingGlyph);
        }

        /// <summary>
        /// Generates a link for sorting operations compatible with <see cref="Controllers.PresenterController{TModel,TCriteria,TKey}" /> .
        /// </summary>
        /// <param name="ajaxHelper">The AJAX helper.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="ajaxOptions">The ajax options.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <param name="ascendingGlyph">The ascending glyph.</param>
        /// <param name="descendingGlyph">The descending glyph.</param>
        /// <returns></returns>
        public static IHtmlString SortLink(
            this AjaxHelper ajaxHelper,
            string linkText,
            string sortExpression,
            AjaxOptions ajaxOptions,
            IDictionary<string, object> htmlAttributes = null,
            HtmlString ascendingGlyph = null,
            HtmlString descendingGlyph = null)
        {
            return SortLink(ajaxHelper, new HtmlString(linkText), sortExpression, ajaxOptions, htmlAttributes, ascendingGlyph, descendingGlyph);
        }

        /// <summary>
        /// Generates a link for sorting operations compatible with <see cref="Controllers.PresenterController{TModel,TCriteria,TKey}" /> .
        /// </summary>
        /// <param name="ajaxHelper">The AJAX helper.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="ajaxOptions">The ajax options.</param>
        /// <param name="ascendingGlyph">The ascending glyph.</param>
        /// <param name="descendingGlyph">The descending glyph.</param>
        /// <returns></returns>
        public static IHtmlString SortLink(
            this AjaxHelper ajaxHelper,
            IHtmlString linkText,
            string sortExpression,
            AjaxOptions ajaxOptions,
            HtmlString ascendingGlyph = null,
            HtmlString descendingGlyph = null)
        {
            return SortLink(ajaxHelper, linkText, sortExpression, ajaxOptions, null, ascendingGlyph, descendingGlyph);
        }

        /// <summary>
        /// Generates a link for sorting operations compatible with <see cref="Controllers.PresenterController{TModel,TCriteria,TKey}" /> .
        /// </summary>
        /// <param name="ajaxHelper">The AJAX helper.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="ajaxOptions">The ajax options.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <param name="ascendingGlyph">The ascending glyph.</param>
        /// <param name="descendingGlyph">The descending glyph.</param>
        /// <returns></returns>
        public static IHtmlString SortLink(
            this AjaxHelper ajaxHelper,
            IHtmlString linkText,
            string sortExpression,
            AjaxOptions ajaxOptions,
            object htmlAttributes = null,
            HtmlString ascendingGlyph = null,
            HtmlString descendingGlyph = null)
        {
            return SortLink(
                ajaxHelper,
                linkText,
                sortExpression,
                ajaxOptions,
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes),
                ascendingGlyph,
                descendingGlyph);
        }

        /// <summary>
        /// Generates a link for sorting operations compatible with <see cref="Controllers.PresenterController{TModel,TCriteria,TKey}" /> .
        /// </summary>
        /// <param name="ajaxHelper">The AJAX helper.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="ajaxOptions">The ajax options.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <param name="ascendingGlyph">The ascending glyph.</param>
        /// <param name="descendingGlyph">The descending glyph.</param>
        /// <returns></returns>
        public static IHtmlString SortLink(
            this AjaxHelper ajaxHelper,
            IHtmlString linkText,
            string sortExpression,
            AjaxOptions ajaxOptions,
            IDictionary<string, object> htmlAttributes = null,
            HtmlString ascendingGlyph = null,
            HtmlString descendingGlyph = null)
        {
            HtmlString sortGlyph;
            string processedSortExpression = DataPresenterExtensions.GenerateSortExpression(
                ajaxHelper.ViewContext, sortExpression, ascendingGlyph, descendingGlyph, out sortGlyph);

            return ajaxHelper.PersistRouteLink(
                string.Format("{0} {1}", linkText, sortGlyph == null ? string.Empty : sortGlyph.ToString()),
                new { sortExpression = processedSortExpression },
                ajaxOptions,
                htmlAttributes,
                false);
        }

        #endregion

        #region Grid

        /// <summary>
        /// Renders a data grid.
        /// </summary>
        /// <typeparam name="TModel">The model.</typeparam>
        /// <param name="ajaxHelper">The AJAX helper.</param>
        /// <param name="paginatedData">The paginated data.</param>
        /// <param name="columns">The columns.</param>
        /// <param name="ajaxOptions">The AJAX options.</param>
        /// <param name="tableHtmlAttributes">The table HTML attributes.</param>
        /// <param name="rowHtmlAttributes">The row HTML attributes.</param>
        /// <param name="ascendingGlyph">The ascending glyph.</param>
        /// <param name="descendingGlyph">The descending glyph.</param>
        /// <returns></returns>
        public static IHtmlString DataGrid<TModel>(
            this AjaxHelper ajaxHelper, 
            IPaginated<TModel> paginatedData, 
            DataGridColumnCollection<TModel> columns, 
            AjaxOptions ajaxOptions, 
            object tableHtmlAttributes = null, 
            Func<TModel, object> rowHtmlAttributes = null, 
            HtmlString ascendingGlyph = null, 
            HtmlString descendingGlyph = null)
        {
            return DataPresenterExtensions.DataGrid(
                paginatedData,
                columns,
                tableHtmlAttributes,
                rowHtmlAttributes,
                ascendingGlyph,
                descendingGlyph, 
                column => ajaxHelper.SortLink(column.Title, column.SortExpression, ajaxOptions).ToString());
        }

        #endregion
    }
}
