#region License
// IPaginated{T}.cs
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

namespace Xoqal.Web.Mvc.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a paginated data.
    /// </summary>
    public interface IPaginated<out T> : IPaginated
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value> The data. </value>
        new IEnumerable<T> Data { get; }
    }
}
