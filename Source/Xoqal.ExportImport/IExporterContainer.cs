#region License
// IExporterContainer.cs
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

    /// <summary>
    /// Represents an exporter container.
    /// </summary>
    public interface IExporterContainer
    {
        /// <summary>
        /// Gets a value indicating whether this instance can export.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can export; otherwise, <c>false</c>.
        /// </value>
        bool CanExport { get; }

        /// <summary>
        /// Gets the exporter name of the file.
        /// </summary>
        /// <value>
        /// The exporter name of the file.
        /// </value>
        string ExporterFileName { get; }

        /// <summary>
        /// Gets the exporter.
        /// </summary>
        /// <returns></returns>
        IExporter GetExporter(string name);

        /// <summary>
        /// Gets the export data.
        /// </summary>
        /// <returns></returns>
        IEnumerable GetExportData();
    }
}
