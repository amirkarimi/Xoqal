#region License
// ExcelExporterBase.cs
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

namespace Xoqal.ExportImport
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClosedXML.Excel;

    /// <summary>
    /// Represents a generic excel exporter.
    /// </summary>
    public abstract class ExcelExporterBase<T> : IExporter<T>
    {
        #region Fields

        private System.IO.Stream dataStream;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the file extension.
        /// </summary>
        public string FileExtensionFilter
        {
            get { return "Excel (*.xls)|*.xlsx"; }
        }

        /// <summary>
        /// Gets the workbook.
        /// </summary>
        protected XLWorkbook Workbook { get; private set; }

        /// <summary>
        /// Gets the worksheet.
        /// </summary>
        protected IXLWorksheet Worksheet { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Exports the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="stream">The stream.</param>
        public void Export(IEnumerable<T> data, System.IO.Stream stream)
        {
            this.dataStream = stream;
            this.InitializeWorkbook();
            
            this.Export(data);

            this.SaveWorkbook();
        }

        /// <summary>
        /// Exports the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="stream">The stream.</param>
        void IExporter.Export(IEnumerable data, System.IO.Stream stream)
        {
            this.Export((IEnumerable<T>)data, stream);
        }

        #endregion

        #region Abstract Methods

        /// <summary>
        /// Exports the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        protected abstract void Export(IEnumerable<T> data);

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes the workbook.
        /// </summary>
        private void InitializeWorkbook()
        {
            this.Workbook = new XLWorkbook();
            this.Worksheet = this.Workbook.Worksheets.Add("Sheet1");
        }

        /// <summary>
        /// Saves the workbook.
        /// </summary>
        private void SaveWorkbook()
        {
            this.Workbook.SaveAs(this.dataStream);
        }

        #endregion
    }
}
