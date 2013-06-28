#region License
// ExcelImporterBase.cs
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
    /// Represents a generic excel importer.
    /// </summary>
    public abstract class ExcelImporterBase<T> : IImporter<T>
    {
        #region Fields

        private System.IO.Stream stream;

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
        public IEnumerable<T> Import(System.IO.Stream stream)
        {
            this.stream = stream;
            this.OpenWorkbook();

            return this.Import();
        }

        /// <summary>
        /// Imports data from the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        IEnumerable IImporter.Import(System.IO.Stream stream)
        {
            return this.Import(stream);
        }

        #endregion

        #region Abstract Methods

        /// <summary>
        /// Import the data.
        /// </summary>
        /// <returns>Imported data.</returns>
        protected abstract IEnumerable<T> Import();

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes the workbook.
        /// </summary>
        private void OpenWorkbook()
        {
            this.Workbook = new XLWorkbook(this.stream);
            this.Worksheet = this.Workbook.Worksheets.FirstOrDefault();
            if (this.Workbook == null)
            {
                throw new ImportExportException("Workbook dosen't contain any worksheet.");
            }
        }

        /// <summary>
        /// Saves the workbook.
        /// </summary>
        private void SaveWorkbook()
        {
            this.Workbook.SaveAs(this.stream);
        }

        #endregion
    }
}
