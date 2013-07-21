#region License
// LanguageItem.cs
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

namespace Xoqal.Globalization
{
    using System.Globalization;

    /// <summary>
    /// Represents a language information.
    /// </summary>
    public class LanguageItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageItem" /> class.
        /// </summary>
        /// <param name="cultureInfo"> The CultureInfo object. </param>
        /// <param name="order"> The order. </param>
        public LanguageItem(CultureInfo cultureInfo, int order)
        {
            this.CultureInfo = cultureInfo;
            this.Order = order;
        }

        /// <summary>
        /// Gets the culture info.
        /// </summary>
        public CultureInfo CultureInfo { get; private set; }

        /// <summary>
        /// Gets the order.
        /// </summary>
        public int Order { get; private set; }
    }
}
