#region License
// PersianDateHelper.cs
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
    using System;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Helps parsing Persian date.
    /// </summary>
    public static class PersianDateHelper
    {
        /// <summary>
        /// Parses the Persian date.
        /// </summary>
        /// <param name="rawValue"> The raw value. </param>
        /// <returns> </returns>
        public static DateTime? ParsePersianDate(string rawValue)
        {
            try
            {
                rawValue = rawValue.Trim();

                string[] splitedDateTime = rawValue.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                string rawTime = splitedDateTime.FirstOrDefault(s => s.Contains(':'));
                string rawDate = splitedDateTime.FirstOrDefault(s => !s.Contains(':'));

                if (rawDate == null)
                {
                    return null;
                }

                string[] splitedDate = rawDate.Split(new char[] { '/', ',', '؍' });
                int day = int.Parse(splitedDate[0]);
                int month = int.Parse(splitedDate[1]);
                int year = int.Parse(splitedDate[2]);

                if (year <= 99)
                {
                    year += 1300;
                }

                int hour = 0;
                int minute = 0;
                int second = 0;

                if (!string.IsNullOrWhiteSpace(rawTime))
                {
                    string[] splitedTime = rawTime.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    hour = int.Parse(splitedTime[0]);
                    minute = int.Parse(splitedTime[1]);
                    if (splitedTime.Length > 2)
                    {
                        var lastPart = splitedTime[2].Trim();
                        var formatInfo = GlobalDateTime.GetPersianDateTimeFormatInfo();
                        if (lastPart.Equals(formatInfo.PMDesignator, StringComparison.InvariantCultureIgnoreCase))
                        {
                            if (hour < 12)
                            {
                                hour += 12;
                            }
                        }
                        else
                        {
                            int.TryParse(lastPart, out second);
                        }
                    }
                }

                var pc = new PersianCalendar();
                return pc.ToDateTime(year, month, day, hour, minute, second, 0);
            }
            catch
            {
                return null;
            }
        }
    }
}
