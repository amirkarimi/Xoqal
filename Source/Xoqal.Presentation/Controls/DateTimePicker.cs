#region License
// DateTimePicker.cs
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

namespace Xoqal.Presentation.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    public class DateTimePicker : Control
    {
        /// <summary>
        /// Identifies the <see cref="SelectedDateTime" /> dependency property
        /// </summary>
        public static readonly DependencyProperty SelectedDateTimeProperty =
            DependencyProperty.Register("SelectedDateTime", typeof(DateTime?), typeof(DateTimePicker), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedDateTimeChange));

        /// <summary>
        /// Identifies the <see cref="DatePart" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty DatePartProperty =
            DependencyProperty.Register("DatePart", typeof(DateTime?), typeof(DateTimePicker), new UIPropertyMetadata(null, OnDatePartChange));

        /// <summary>
        /// Identifies the <see cref="TimePart" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty TimePartProperty =
            DependencyProperty.Register("TimePart", typeof(DateTime?), typeof(DateTimePicker), new UIPropertyMetadata(null, OnTimePartChange));

        /// <summary>
        /// Initializes the <see cref="DateTimePicker" /> class.
        /// </summary>
        static DateTimePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DateTimePicker), new FrameworkPropertyMetadata(typeof(DateTimePicker)));
        }

        /// <summary>
        /// Gets or sets the date time value.
        /// </summary>
        /// <value>
        /// The date time value.
        /// </value>
        public DateTime? SelectedDateTime
        {
            get { return (DateTime?)GetValue(SelectedDateTimeProperty); }
            set { this.SetValue(SelectedDateTimeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the date part.
        /// </summary>
        /// <value>
        /// The date part.
        /// </value>
        public DateTime? DatePart
        {
            get { return (DateTime?)GetValue(DatePartProperty); }
            set { this.SetValue(DatePartProperty, value); }
        }

        /// <summary>
        /// Gets or sets the time part.
        /// </summary>
        /// <value>
        /// The time part.
        /// </value>
        public DateTime? TimePart
        {
            get { return (DateTime?)GetValue(TimePartProperty); }
            set { this.SetValue(TimePartProperty, value); }
        }

        /// <summary>
        /// Called when SelectedDateTime change.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnSelectedDateTimeChange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            DateTimePicker dateTimePicker = (DateTimePicker)sender;
            DateTime? dateTime = (DateTime?)e.NewValue;
            if (dateTime.HasValue)
            {
                dateTimePicker.DatePart = dateTime.Value.Date;
                dateTimePicker.TimePart = dateTime;
            }
        }

        /// <summary>
        /// Called when DatePart change.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnDatePartChange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            DateTimePicker dateTimePicker = (DateTimePicker)sender;
            DateTime? datePart = (DateTime?)e.NewValue;
            if (datePart.HasValue)
            {
                if (dateTimePicker.SelectedDateTime.HasValue)
                {
                    datePart += dateTimePicker.SelectedDateTime.Value.TimeOfDay;
                }

                dateTimePicker.SelectedDateTime = datePart;
            }
        }

        /// <summary>
        /// Called when TimePart change.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnTimePartChange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            DateTimePicker dateTimePicker = (DateTimePicker)sender;
            DateTime? timePart = (DateTime?)e.NewValue;
            if (timePart.HasValue)
            {
                if (dateTimePicker.SelectedDateTime.HasValue)
                {
                    timePart = dateTimePicker.SelectedDateTime.Value.Date + timePart.Value.TimeOfDay;
                }

                dateTimePicker.SelectedDateTime = timePart;
            }
        }
    }
}
