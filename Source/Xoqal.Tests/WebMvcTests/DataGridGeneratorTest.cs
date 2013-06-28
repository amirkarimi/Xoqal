#region License
// DataGridGeneratorTest.cs
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
    using System.Web;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Ploeh.AutoFixture;
    using Xoqal.Web.Mvc.Components;
    using Xoqal.Web.Mvc.Models;

    [TestClass]
    public class DataGridGeneratorTest
    {
        [TestMethod]
        public void GetHtmlStringInvokesDataGridRowGenerators()
        {
            var fixture = new Fixture();

            var headerRowGeneratorMock = new Mock<IDataGridHeaderRowGenerator<Models.DataGridSampleModel>>();
            var dataRowGeneratorMock = new Mock<IDataGridDataRowGenerator<Models.DataGridSampleModel>>();
            int dataCount = 3;
            var paginatedData = new Paginated<Models.DataGridSampleModel>(fixture.CreateMany<Models.DataGridSampleModel>(dataCount), dataCount, 1, 10);

            headerRowGeneratorMock.Setup(g => g.GetHtmlString(It.IsAny<DataGridColumnCollection<Models.DataGridSampleModel>>(), It.IsAny<Func<DataGridColumn<Models.DataGridSampleModel>, string>>())).Returns(new HtmlString(string.Empty));
            dataRowGeneratorMock.Setup(g => g.GetHtmlString(It.IsAny<Models.DataGridSampleModel>(), It.IsAny<DataGridColumnCollection<Models.DataGridSampleModel>>(), It.IsAny<Func<Models.DataGridSampleModel, object>>())).Returns(new HtmlString(string.Empty));

            var dataGridColumns = new DataGridColumnCollection<Models.DataGridSampleModel>
            {
                {
                    "Title",
                    model => model.FirstProperty,
                    model => model.FirstProperty
                }
            };

            var sut = new DataGridGenerator<Models.DataGridSampleModel>(headerRowGeneratorMock.Object, dataRowGeneratorMock.Object);

            sut.GetHtmlString(
                paginatedData, 
                dataGridColumns,
                null,
                null,
                null,
                null, null);

            headerRowGeneratorMock.Verify(a => a.GetHtmlString(dataGridColumns, null), Times.Once());
            dataRowGeneratorMock.Verify(a => a.GetHtmlString(It.IsAny<Models.DataGridSampleModel>(), dataGridColumns, null), Times.Exactly(dataCount));
        }

        [TestMethod]
        public void GetHtmlStringWithTableAttributeReturnsCorrectResult()
        {
            var fixture = new Fixture();

            var headerRowGeneratorMock = new Mock<IDataGridHeaderRowGenerator<Models.DataGridSampleModel>>();
            var dataRowGeneratorMock = new Mock<IDataGridDataRowGenerator<Models.DataGridSampleModel>>();
            int dataCount = 3;
            var paginatedData = new Paginated<Models.DataGridSampleModel>(fixture.CreateMany<Models.DataGridSampleModel>(dataCount), dataCount, 1, 10);

            headerRowGeneratorMock.Setup(g => g.GetHtmlString(It.IsAny<DataGridColumnCollection<Models.DataGridSampleModel>>(), It.IsAny<Func<DataGridColumn<Models.DataGridSampleModel>, string>>())).Returns(new HtmlString(string.Empty));
            dataRowGeneratorMock.Setup(g => g.GetHtmlString(It.IsAny<Models.DataGridSampleModel>(), It.IsAny<DataGridColumnCollection<Models.DataGridSampleModel>>(), It.IsAny<Func<Models.DataGridSampleModel, object>>())).Returns(new HtmlString(string.Empty));

            var dataGridColumns = new DataGridColumnCollection<Models.DataGridSampleModel>
            {
                {
                    "Title",
                    model => model.FirstProperty,
                    model => model.FirstProperty
                }
            };

            var sut = new DataGridGenerator<Models.DataGridSampleModel>(headerRowGeneratorMock.Object, dataRowGeneratorMock.Object);

            var actual = sut.GetHtmlString(
                paginatedData,
                dataGridColumns,
                new { @class = "data" },
                null,
                null,
                null, null).ToString();

            Assert.IsTrue(actual.StartsWith("<table class=\"data\">"));
            Assert.IsTrue(actual.EndsWith("</table>"));
        }
    }
}
