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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
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
            var properties = this.GetProperties(typeof(T)).ToArray();
            int col = 1;
            foreach (var property in properties)
            {
                var cell = this.Worksheet.Cell(1, col);
                cell.Value = property.GetDisplayName();
                cell.SetDataType(XLCellValues.Text);
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
                foreach (var property in properties)
                {
                    var cell = this.Worksheet.Cell(row, col);
                    cell.Value = this.GetPropertyValue(property, item);
                    if (property.PropertyType == typeof(string))
                    {
                        cell.SetDataType(XLCellValues.Text);
                    }
                    col++;
                }
            }
        }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private IEnumerable<PropertyDescriptor> GetProperties(Type type)
        {
            return TypeDescriptor
                .GetProperties(type)
                .OfType<PropertyDescriptor>()
                .Where(p => !p.Attributes.OfType<Attributes.IgnoreAttribute>().Any());
        }

        /// <summary>
        /// Gets the specified property value for the specified component.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="component"></param>
        /// <returns></returns>
        private object GetPropertyValue(PropertyDescriptor property, object component)
        {
            object value = property.GetValue(component);

            if (property.PropertyType == typeof(DateTime) || (property.PropertyType == typeof(DateTime?) && value != null))
            {
                value = GlobalDateTime.ToShortDateTimeString((DateTime)value);
            }

            return value;
        }
    }
}
