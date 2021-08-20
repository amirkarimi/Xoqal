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
    public interface IImporterContainer
    {
        /// <summary>
        /// Gets a value indicating whether this instance can import.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can import; otherwise, <c>false</c>.
        /// </value>
        bool CanImport { get; }

        /// <summary>
        /// Gets the importer.
        /// </summary>
        /// <returns></returns>
        IImporter GetImporter(string name);

        /// <summary>
        /// Sets the import data.
        /// </summary>
        /// <param name="data">The data.</param>
        void SetImportData(IEnumerable data);
    }
}
