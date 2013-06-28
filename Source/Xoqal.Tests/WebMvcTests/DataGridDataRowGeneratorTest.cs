#region License
// DataGridDataRowGeneratorTest.cs
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
    using Ploeh.AutoFixture;
    using Xoqal.Tests.WebMvcTests.Models;
    using Xoqal.Web.Mvc.Components;
    using Xoqal.Web.Mvc.Models;

    [TestClass]
    public class DataGridDataRowGeneratorTest
    {
        [TestMethod]
        public void GetDataRowTagWithoutHtmlAttributesReturnsCorrectResult()
        {
            var fixture = new Fixture();
            var data = fixture.Create<DataGridSampleModel>();

            var dataGridColumns = new DataGridColumnCollection<DataGridSampleModel>
            {
                {
                    "Title",
                    model => model.FirstProperty,
                    model => model.FirstProperty
                }
            };

            IDataGridDataRowGenerator<DataGridSampleModel> sut = new DataGridDataRowGenerator<DataGridSampleModel>();

            var actual = sut
                .GetHtmlString(data, dataGridColumns, null)
                .ToString();

            Assert.IsTrue(actual.StartsWith("<tr>"));
            Assert.IsTrue(actual.EndsWith("</tr>"));
            Assert.IsTrue(actual.Contains(string.Format("<td>{0}</td>", data.FirstProperty)));
        }

        [TestMethod]
        public void GetDataRowTagWithHtmlAttributesReturnsCorrectResult()
        {
            var fixture = new Fixture();
            var data = fixture.Create<DataGridSampleModel>();

            var dataGridColumns = new DataGridColumnCollection<DataGridSampleModel>
            {
                {
                    "Title",
                    model => model.FirstProperty,
                    model => model.FirstProperty,
                    new { myAttribute = "attribute-value" }
                }
            };

            IDataGridDataRowGenerator<DataGridSampleModel> sut = new DataGridDataRowGenerator<DataGridSampleModel>();

            var actual = sut
                .GetHtmlString(data, dataGridColumns, model => new { dataAttribute = model.FirstProperty + "A" })
                .ToString();

            Assert.IsTrue(actual.StartsWith(string.Format("<tr dataAttribute=\"{0}\">", data.FirstProperty + "A")));
            Assert.IsTrue(actual.EndsWith("</tr>"));
            Assert.IsTrue(actual.Contains(string.Format("<td myAttribute=\"attribute-value\">{0}</td>", data.FirstProperty)));
        }
    }
}
