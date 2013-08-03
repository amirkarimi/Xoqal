#region License
// Paginated.cs
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

namespace Xoqal.Core.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Paginated<T> : IPaginated<T>
    {
        /// <summary>
        /// Create a new instance of the <see cref="T:Paginated"/> class.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="totalRowsCount"></param>
        public Paginated(IEnumerable<T> data, int totalRowsCount)
        {
            this.Data = data;
            this.TotalRowsCount = totalRowsCount;
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// Gets the data.
        /// </summary>
        IEnumerable IPaginated.Data
        {
            get { return this.Data; }
        }

        /// <summary>
        /// Gets the total number of data.
        /// </summary>
        public int TotalRowsCount { get; set; }
    }
}
