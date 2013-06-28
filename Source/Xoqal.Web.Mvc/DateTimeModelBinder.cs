#region License
// DateTimeModelBinder.cs
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

namespace Xoqal.Web.Mvc
{
    using System;
    using System.Globalization;
    using System.Web.Mvc;

    /// <summary>
    /// A customized DateTime model binder which also supports Persian calendar.
    /// </summary>
    /// <remarks>
    /// To show that the time is in Persian format the value should have a "P" prefix. Ex. /Report/View/19/P13901010 (P13901010 means 1390/10/10 in persian calendar)
    /// </remarks>
    public class DateTimeModelBinder : IModelBinder
    {
        #region IModelBinder Members

        /// <summary>
        /// Binds the model.
        /// </summary>
        /// <param name="controllerContext"> The controller context. </param>
        /// <param name="bindingContext"> The binding context. </param>
        /// <returns> </returns>
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (result == null)
            {
                return null;
            }

            string value = result.AttemptedValue;

            // Check for null value
            if (value.Trim().ToLower() == "null" || string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            // Check for persian date
            if (value.StartsWith("P"))
            {
                // This is a persian date format
                return this.ParsePersianDate(value);
            }

            DateTime dt;
            if (DateTime.TryParseExact(value, "yyyyMMdd", null, DateTimeStyles.None, out dt))
            {
                return dt;
            }

            return DateTime.Parse(value);
        }

        #endregion

        /// <summary>
        /// Parses the persian date.
        /// </summary>
        /// <param name="value"> The raw value. </param>
        /// <returns> </returns>
        private DateTime ParsePersianDate(string value)
        {
            // Skip the "P" char
            value = value.Substring(1);

            int year;
            int month;
            int day;

            string[] parts = value.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 3)
            {
                if (parts[2].Length == 4)
                {
                    year = int.Parse(parts[2]);
                    month = int.Parse(parts[1]);
                    day = int.Parse(parts[0]);
                }
                else
                {
                    year = int.Parse(parts[0]);
                    month = int.Parse(parts[1]);
                    day = int.Parse(parts[2]);
                }
            }
            else
            {
                year = int.Parse(value.Substring(0, 4));
                month = int.Parse(value.Substring(4, 2));
                day = int.Parse(value.Substring(6, 2));
            }

            var pc = new PersianCalendar();
            return pc.ToDateTime(year, month, day, 0, 0, 0, 0);
        }
    }
}
