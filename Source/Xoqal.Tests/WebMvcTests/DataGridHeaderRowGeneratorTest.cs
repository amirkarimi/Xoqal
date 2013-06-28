#region License
// DataGridHeaderRowGeneratorTest.cs
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

namespace Xoqal.Tests.WebMvcTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Xoqal.Tests.WebMvcTests.Models;
    using Xoqal.Web.Mvc.Components;
    using Xoqal.Web.Mvc.Models;

    [TestClass]
    public class DataGridHeaderRowGeneratorTest
    {
        [TestMethod]
        public void GetHtmlStringWithSortExpressionReturnsCorrectResult()
        {
            var dataGridColumns = new DataGridColumnCollection<DataGridSampleModel>
            {
                {
                    "Title",
                    model => model.FirstProperty,
                    model => model.FirstProperty
                }
            };

            IDataGridHeaderRowGenerator<DataGridSampleModel> sut = new DataGridHeaderRowGenerator<DataGridSampleModel>();

            var actual = sut
                .GetHtmlString(
                    dataGridColumns,
                    columns => string.Format("<a class='sortLink'>{0}</a>", columns.Title))
                .ToString();

            Assert.IsTrue(actual.StartsWith("<tr>"));
            Assert.IsTrue(actual.EndsWith("</tr>"));
            Assert.IsTrue(actual.Contains(string.Format("<th><a class='sortLink'>{0}</a></th>", dataGridColumns[0].Title)));
        }

        [TestMethod]
        public void GetHtmlStringWithoutSortExpressionReturnsCorrectResult()
        {
            var dataGridColumns = new DataGridColumnCollection<DataGridSampleModel>
            {
                {
                    "Title",
                    model => model.FirstProperty
                }
            };

            IDataGridHeaderRowGenerator<DataGridSampleModel> sut = new DataGridHeaderRowGenerator<DataGridSampleModel>();

            var actual = sut
                .GetHtmlString(
                    dataGridColumns,
                    column => string.Format("<a class='sortLink'>{0}</a>", column.Title))
                .ToString();

            Assert.IsTrue(actual.StartsWith("<tr>"));
            Assert.IsTrue(actual.EndsWith("</tr>"));
            Assert.IsFalse(actual.Contains(string.Format("<th><a class='sortLink'>{0}</a></th>", dataGridColumns[0].Title)));
            Assert.IsTrue(actual.Contains(string.Format("<th>{0}</th>", dataGridColumns[0].Title)));
        }
    }
}
