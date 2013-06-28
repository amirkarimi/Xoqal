#region License
// GeneratorOptions.cs
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

namespace Xoqal.Generator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class GeneratorOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneratorOptions" /> class.
        /// </summary>
        public GeneratorOptions()
        {
            this.ExcludedEntities = new string[0];
        }

        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        /// <value>
        /// The name of the project.
        /// </value>
        public string ProjectName { get; set; }

        /// <summary>
        /// Gets or sets the entities assembly path.
        /// </summary>
        /// <value>
        /// The entities assembly path.
        /// </value>
        public string EntitiesAssemblyPath { get; set; }

        /// <summary>
        /// Gets or sets the output folder.
        /// </summary>
        /// <value>
        /// The output folder.
        /// </value>
        public string OutputFolder { get; set; }

        /// <summary>
        /// Gets or sets the excluded entities.
        /// </summary>
        /// <value>
        /// The excluded entities.
        /// </value>
        public string[] ExcludedEntities { get; set; }

        /// <summary>
        /// Gets or sets the entity infoes.
        /// </summary>
        /// <value>
        /// The entity infoes.
        /// </value>
        public IEnumerable<EntityInfo> EntityInfoes { get; set; }

        /// <summary>
        /// Gets the entities namespace.
        /// </summary>
        /// <value>
        /// The entities namespace.
        /// </value>
        public string EntitiesNamespace
        {
            get { return string.Format("{0}.Entities", this.ProjectName); }
        }

        /// <summary>
        /// Gets the data namespace.
        /// </summary>
        /// <value>
        /// The data namespace.
        /// </value>
        public string DataNamespace
        {
            get { return string.Format("{0}.Data", this.ProjectName); }
        }

        /// <summary>
        /// Gets the services namespace.
        /// </summary>
        /// <value>
        /// The services namespace.
        /// </value>
        public string ServicesNamespace
        {
            get { return string.Format("{0}.Services", this.ProjectName); }
        }

        /// <summary>
        /// Gets the data directory.
        /// </summary>
        /// <value>
        /// The data directory.
        /// </value>
        public string DataDirectory
        {
            get { return Path.Combine(this.OutputFolder, "Data"); }
        }

        /// <summary>
        /// Gets the services directory.
        /// </summary>
        /// <value>
        /// The services directory.
        /// </value>
        public string ServicesDirectory
        {
            get { return Path.Combine(this.OutputFolder, "Services"); }
        }
    }
}
