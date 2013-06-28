#region License
// ExcelExporter.cs
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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using ClosedXML.Excel;
    using Xoqal.Globalization;

    /// <summary>
    /// Exports a general data to an excel file.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExcelExporter<T> : ExcelExporterBase<T>
    {
        /// <summary>
        /// Exports the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        protected override void Export(IEnumerable<T> data)
        {
            var properties = TypeDescriptor.GetProperties(typeof(T));

            int col = 1;
            foreach (PropertyDescriptor property in properties)
            {
                var cell = this.Worksheet.Cell(1, col);
                cell.Value = property.DisplayName;
                cell.Style.Fill.BackgroundColor = XLColor.FromArgb(49, 134, 155);
                cell.Style.Font.Bold = true;
                cell.Style.Font.FontColor = XLColor.White;
                col++;
            }

            var items = data.ToList();
            foreach (var item in items)
            {
                int row = items.IndexOf(item) + 2;
                col = 1;
                foreach (PropertyDescriptor property in properties)
                {
                    var cell = this.Worksheet.Cell(row, col);
                    cell.Value = this.GetPropertyValue(property, item);
                    col++;
                }
            }
        }

        private object GetPropertyValue(PropertyDescriptor property, object component)
        {
            object value = property.GetValue(component);

            if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
            {
                value = GlobalDateTime.ToShortDateString((DateTime)value);
            }

            return value;
        }
    }
}
