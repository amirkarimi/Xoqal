#region License
// EnumExtensions.cs
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
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// HTML extension to use for enum.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// The enum radio list extension.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="enumType">Type of the enum.</param>
        /// <param name="defaultLabel">The default label.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <param name="radioHtmlAttributes">The radio HTML attributes.</param>
        /// <returns></returns>
        public static HtmlString EnumRadioListFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            Type enumType,
            string defaultLabel = "",
            object htmlAttributes = null,
            object radioHtmlAttributes = null)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var enumMembers = Utilities.EnumHelper.GetEnumValueDisplays(enumType);
            var sb = new StringBuilder();

            var fieldId = htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression));
            var fieldName = htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));

            var unobtrusiveValidationAttributes = htmlHelper.GetUnobtrusiveValidationAttributes(fieldName);
            if (unobtrusiveValidationAttributes.ContainsKey("data-val-number"))
            {
                unobtrusiveValidationAttributes.Remove("data-val-number");
            }

            if (!string.IsNullOrWhiteSpace(defaultLabel))
            {
                var radioTag = new TagBuilder("input");
                if (metaData.Model == null)
                {
                    radioTag.Attributes.Add("checked", "checked");
                }

                radioTag.Attributes.Add("id", fieldId);
                radioTag.Attributes.Add("name", fieldName);
                radioTag.Attributes.Add("type", "radio");
                radioTag.Attributes.Add("value", string.Empty);
                radioTag.MergeAttributes(unobtrusiveValidationAttributes);
                sb.AppendFormat(
                    "<label>{0}{1}</label>",
                    radioTag,
                    defaultLabel);
            }

            foreach (var enumMember in enumMembers)
            {
                var radioTag = new TagBuilder("input");
                if (metaData.Model != null &&
                    (metaData.Model.Equals(enumMember.Key) ||
                    metaData.Model.Equals(Enum.Parse(enumType, enumMember.Key.ToString()))))
                {
                    radioTag.Attributes.Add("checked", "checked");
                }

                radioTag.Attributes.Add("id", fieldId);
                radioTag.Attributes.Add("name", fieldName);
                radioTag.Attributes.Add("type", "radio");
                radioTag.Attributes.Add("value", enumMember.Key.ToString());

                radioTag.MergeAttributes(unobtrusiveValidationAttributes);

                radioTag.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(radioHtmlAttributes));
                
                sb.AppendFormat(
                    "<label>{0}{1}</label>",
                    radioTag,
                    enumMember.Value);
            }

            var div = new TagBuilder("div") { InnerHtml = sb.ToString() };
            div.AddCssClass("radio-button-list");
            div.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            return new HtmlString(div.ToString());
        }

        /// <summary>
        /// The enum check box list extension.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="enumType">Type of the enum.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static HtmlString EnumCheckBoxListFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            Type enumType,
            object htmlAttributes = null)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var enumMembers = Utilities.EnumHelper.GetEnumValueDisplays(enumType);
            var sb = new StringBuilder();

            var listModel = (IList)metaData.Model;
            var items = enumMembers.Select(e => new SelectListItem
                {
                    Selected = listModel.Cast<object>().Any(l => l.Equals(e.Key) || l.Equals(Enum.Parse(enumType, e.Key.ToString()))),
                    Text = e.Value,
                    Value = e.Key.ToString()
                });

            foreach (var item in items)
            {
                sb.Append(@"<label><input type=""checkbox"" name=""");
                sb.Append(metaData.PropertyName);
                sb.Append("\" value=\"");
                sb.Append(item.Value);
                sb.Append("\"");

                if (item.Selected)
                {
                    sb.Append(" checked=\"chekced\"");
                }

                sb.Append(" />");
                sb.Append(item.Text);
                sb.Append("</label>");
            }

            var div = new TagBuilder("div") { InnerHtml = sb.ToString() };
            div.AddCssClass("check-box-list");
            div.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            return new HtmlString(div.ToString());
        }

        /// <summary>
        /// The enum drop down list extension.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="enumType">Type of the enum.</param>
        /// <param name="defaultLabel">The default label.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static HtmlString EnumDropDownListFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            Type enumType,
            string defaultLabel = "",
            object htmlAttributes = null)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var enumMembers = Utilities.EnumHelper.GetEnumValueDisplays(enumType);
            var stringBuilder = new StringBuilder();
            var fieldId = htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression));
            var fieldName = htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));
            var unobtrusiveValidationAttributes = htmlHelper.GetUnobtrusiveValidationAttributes(fieldName, metaData);

            if (unobtrusiveValidationAttributes.ContainsKey("data-val-number"))
            {
                unobtrusiveValidationAttributes.Remove("data-val-number");
            }

            var selectTagBuilder = new TagBuilder("select");
            if (!string.IsNullOrWhiteSpace(defaultLabel))
            {
                var optionTagBuilder = new TagBuilder("option");

                if (metaData.Model == null)
                {
                    optionTagBuilder.Attributes.Add("selected", "selected");
                }

                optionTagBuilder.Attributes.Add("value", string.Empty);
                optionTagBuilder.SetInnerText(defaultLabel);
                selectTagBuilder.MergeAttributes(unobtrusiveValidationAttributes);
                stringBuilder.Append(optionTagBuilder);
            }

            foreach (var enumMember in enumMembers)
            {
                var optionTagBuilder = new TagBuilder("option");

                if (metaData.Model != null && 
                    (metaData.Model.Equals(enumMember.Key) || 
                    metaData.Model.Equals(Enum.Parse(enumType, enumMember.Key.ToString()))))
                {
                    optionTagBuilder.Attributes.Add("selected", "selected");
                }

                optionTagBuilder.Attributes.Add("value", enumMember.Key.ToString());
                optionTagBuilder.SetInnerText(enumMember.Value);
                stringBuilder.Append(optionTagBuilder);
            }

            selectTagBuilder.Attributes.Add("id", fieldId);
            selectTagBuilder.Attributes.Add("name", fieldName);
            selectTagBuilder.InnerHtml = stringBuilder.ToString();

            var div = new TagBuilder("div") { InnerHtml = selectTagBuilder.ToString() };
            div.AddCssClass("drop-down-list");
            div.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            return new HtmlString(div.ToString());
        }
    }
}
