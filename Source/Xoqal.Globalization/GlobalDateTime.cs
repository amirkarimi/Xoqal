#region License
// GlobalDateTime.cs
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
    using System.Threading;

    /// <summary>
    /// Represents the globalized DateTime object virtually base on the given DateTime.
    /// </summary>
    /// <remarks>
    /// Created by A. Karimi (karimi@dev-frame.com)
    /// </remarks>
    public class GlobalDateTime
    {
        #region Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalDateTime" /> class.
        /// </summary>
        /// <param name="dateTime"> The date time. </param>
        public GlobalDateTime(DateTime dateTime)
        {
            this.DateTimeValue = dateTime;

            this.Calendar = GetCalendar();
            this.DefaultDateTimeFormat = GetDateTimeFormat();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a <see cref="GlobalDateTime" /> object that is set to the current date and time on this computer.
        /// </summary>
        public static GlobalDateTime Now
        {
            get { return new GlobalDateTime(DateTime.Now); }
        }

        /// <summary>
        /// Gets or sets the date time.
        /// </summary>
        /// <value> The date time. </value>
        public DateTime DateTimeValue { get; set; }

        /// <summary>
        /// Gets or sets the calendar.
        /// </summary>
        /// <value> The calendar. </value>
        public Calendar Calendar { get; set; }

        /// <summary>
        /// Gets or sets the default date time format info.
        /// </summary>
        /// <value> The default date time format info. </value>
        public DateTimeFormatInfo DefaultDateTimeFormat { get; set; }

        /// <summary>
        /// Gets the year.
        /// </summary>
        public int Year
        {
            get { return this.Calendar.GetYear(this.DateTimeValue); }
        }

        /// <summary>
        /// Gets the month.
        /// </summary>
        public int Month
        {
            get { return this.Calendar.GetMonth(this.DateTimeValue); }
        }

        /// <summary>
        /// Gets the day of month.
        /// </summary>
        public int Day
        {
            get { return this.Calendar.GetDayOfMonth(this.DateTimeValue); }
        }

        /// <summary>
        /// Gets the hour.
        /// </summary>
        public int Hour
        {
            get { return this.Calendar.GetHour(this.DateTimeValue); }
        }

        /// <summary>
        /// Gets the minute.
        /// </summary>
        public int Minute
        {
            get { return this.Calendar.GetMinute(this.DateTimeValue); }
        }

        /// <summary>
        /// Gets the second.
        /// </summary>
        public int Second
        {
            get { return this.Calendar.GetSecond(this.DateTimeValue); }
        }

        /// <summary>
        /// Gets the milliseconds.
        /// </summary>
        public double Milliseconds
        {
            get { return this.Calendar.GetMilliseconds(this.DateTimeValue); }
        }

        /// <summary>
        /// Gets the day of week.
        /// </summary>
        public DayOfWeek DayOfWeek
        {
            get { return this.Calendar.GetDayOfWeek(this.DateTimeValue); }
        }

        /// <summary>
        /// Gets the two digit year.
        /// </summary>
        public int TwoDigitYear
        {
            get { return this.Year % 100; }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets the current UI calendar.
        /// </summary>
        /// <returns> </returns>
        public static Calendar GetCalendar()
        {
            // Check for Persian language
            if (Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName == "fa")
            {
                return new PersianCalendar();
            }

            return Thread.CurrentThread.CurrentUICulture.Calendar;
        }

        /// <summary>
        /// Gets the current UI <see cref="T:System.Globalization.DateTimeFormatInfo" /> .
        /// </summary>
        /// <returns> </returns>
        public static DateTimeFormatInfo GetDateTimeFormat()
        {
            // Check for Persian language
            if (Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName == "fa")
            {
                return GetPersianDateTimeFormatInfo();
            }

            return Thread.CurrentThread.CurrentUICulture.DateTimeFormat;
        }

        /// <summary>
        /// Returns the short date format of this instance.
        /// </summary>
        /// <returns> </returns>
        public static string ToShortDateString(DateTime dt)
        {
            return new GlobalDateTime(dt).ToShortDateString();
        }

        /// <summary>
        /// Returns the short date and time format of this instance.
        /// </summary>
        /// <returns> </returns>
        public static string ToShortDateTimeString(DateTime dt)
        {
            return new GlobalDateTime(dt).ToShortDateTimeString();
        }

        /// <summary>
        /// Returns the long date format of this instance.
        /// </summary>
        /// <returns> </returns>
        public static string ToLongDateString(DateTime dt)
        {
            return new GlobalDateTime(dt).ToLongDateString();
        }

        /// <summary>
        /// Returns the long date and time format of this instance.
        /// </summary>
        /// <returns> </returns>
        public static string ToLongDateTimeString(DateTime dt)
        {
            return new GlobalDateTime(dt).ToLongDateTimeString();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="dateTime"> The date time. </param>
        /// <param name="format"> The format. </param>
        /// <returns> A <see cref="System.String" /> that represents this instance. </returns>
        public static string ToString(DateTime dateTime, string format)
        {
            return new GlobalDateTime(dateTime).ToString(format);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents the time in a relative manner.
        /// </summary>
        /// <param name="dateTime"> The date time. </param>
        /// <param name="underMinuteFormat"> The under minute format. </param>
        /// <param name="underHourFormat"> The under hour format. </param>
        /// <param name="underDayFormat"> The under day format. </param>
        /// <param name="defaultFormat"> The default format. </param>
        /// <returns> </returns>
        public static string ToRelativeDateTimeString(
            DateTime dateTime, string underMinuteFormat, string underHourFormat, string underDayFormat, string defaultFormat)
        {
            return new GlobalDateTime(dateTime).ToRelativeDateTimeString(underMinuteFormat, underHourFormat, underDayFormat, defaultFormat);
        }

        /// <summary>
        /// Gets the date time format info.
        /// </summary>
        /// <returns> </returns>
        public static DateTimeFormatInfo GetPersianDateTimeFormatInfo()
        {
            var dateFormat = new DateTimeFormatInfo
            {
                AbbreviatedDayNames = new[] { "ی", "د", "س", "چ", "پ", "ج", "ش" },
                AbbreviatedMonthGenitiveNames =
                    new[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", string.Empty },
                AbbreviatedMonthNames =
                    new[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", string.Empty },
                AMDesignator = "صبح",
                CalendarWeekRule = CalendarWeekRule.FirstDay,
                DateSeparator = "؍",
                DayNames = new[] { "یکشنبه", "دوشنبه", "سه‌شنبه", "چهار‌شنبه", "پنجشنبه", "جمعه", "شنبه" },
                FirstDayOfWeek = DayOfWeek.Saturday,
                FullDateTimePattern = "dddd dd MMMM yyyy",
                LongDatePattern = "dd MMMM yyyy",
                LongTimePattern = "h:mm:ss tt",
                MonthDayPattern = "dd MMMM",
                MonthGenitiveNames =
                    new[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", string.Empty },
                MonthNames =
                    new[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", string.Empty },
                PMDesignator = "عصر",
                ShortDatePattern = "dd؍MM؍yyyy",
                ShortestDayNames = new[] { "ی", "د", "س", "چ", "پ", "ج", "ش" },
                ShortTimePattern = "HH:mm",
                TimeSeparator = ":",
                YearMonthPattern = "MMMM yyyy",
            };

            return dateFormat;
        }

        #endregion

        #region To...String Methods

        /// <summary>
        /// Returns the short date format of this instance.
        /// </summary>
        /// <returns> </returns>
        public string ToShortDateString()
        {
            return this.ToString(this.DefaultDateTimeFormat.ShortDatePattern);
        }

        /// <summary>
        /// Returns the short date and time format of this instance.
        /// </summary>
        /// <returns> </returns>
        public string ToShortDateTimeString()
        {
            return this.ToString(this.DefaultDateTimeFormat.ShortTimePattern + " " + this.DefaultDateTimeFormat.ShortDatePattern);
        }

        /// <summary>
        /// Returns the long date format of this instance.
        /// </summary>
        /// <returns> </returns>
        public string ToLongDateString()
        {
            return this.ToString(this.DefaultDateTimeFormat.LongDatePattern);
        }

        /// <summary>
        /// Returns the long date and time format of this instance.
        /// </summary>
        /// <returns> </returns>
        public string ToLongDateTimeString()
        {
            return this.ToString(this.DefaultDateTimeFormat.LongTimePattern + " " + this.DefaultDateTimeFormat.LongDatePattern);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="format"> The format. </param>
        /// <returns> A <see cref="System.String" /> that represents this instance. </returns>
        public string ToString(string format)
        {
            if (this.Calendar is PersianCalendar)
            {
                string[] autoReplaces = new string[] 
                {
                    "fffffff", "ffffff", "fffff", "ffff", "fff", "ff", "f",
                    "FFFFFFF", "FFFFFF", "FFFFF", "FFFF", "FFF", "FF", "F",
                    "gg", "g",
                    "hh", "HH", "mm", "ss", "tt", "t"
                };

                foreach (var autoReplace in autoReplaces)
                {
                    format = format.Replace(autoReplace, this.DateTimeValue.ToString(autoReplace, this.DefaultDateTimeFormat));
                }

                format = format.Replace("h", this.Hour.ToString());
                format = format.Replace("dddd", this.DefaultDateTimeFormat.GetDayName(this.DayOfWeek));
                format = format.Replace("ddd", this.DefaultDateTimeFormat.GetAbbreviatedDayName(this.DayOfWeek));
                format = format.Replace("dd", this.Day.ToString("00"));
                format = format.Replace("d", this.Day.ToString());
                format = format.Replace("MMMM", this.DefaultDateTimeFormat.GetMonthName(this.Month));
                format = format.Replace("MMM", this.DefaultDateTimeFormat.GetAbbreviatedMonthName(this.Month));
                format = format.Replace("MM", this.Month.ToString("00"));
                format = format.Replace("M", this.Month.ToString());
                format = format.Replace("yyyy", this.Year.ToString("0000"));
                format = format.Replace("yyy", this.Year.ToString("000"));
                format = format.Replace("yy", (this.Year % 100).ToString("00"));
                format = format.Replace("y", (this.Year % 100).ToString());

                return format;
            }

            var dt = new DateTime(this.Year, this.Month, this.Day, this.Hour, this.Minute, this.Second, (int)this.Milliseconds);
            return dt.ToString(format, this.DefaultDateTimeFormat);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents the time in a relative manner.
        /// </summary>
        /// <returns> </returns>
        public string ToRelativeDateTimeString(
            string underMinuteFormat, string underHourFormat, string underDayFormat, string defaultFormat)
        {
            TimeSpan diff = DateTime.Now - this.DateTimeValue;
            if (diff.TotalSeconds > 0)
            {
                if (diff.TotalMinutes < 1)
                {
                    return string.Format(underMinuteFormat, Math.Round(diff.TotalSeconds));
                }

                if (diff.TotalHours < 1)
                {
                    return string.Format(underHourFormat, Math.Round(diff.TotalMinutes));
                }

                if (diff.TotalDays < 1)
                {
                    return string.Format(underDayFormat, Math.Round(diff.TotalHours));
                }
            }

            return this.ToString(defaultFormat);
        }

        #endregion
    }
}
