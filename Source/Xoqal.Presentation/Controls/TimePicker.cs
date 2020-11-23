#region License
// TimePicker.cs
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

    /// <summary>
    /// 
    /// </summary>
    [TemplatePart(Name = "PART_HourTextBox", Type = typeof(TimePicker))]
    [TemplatePart(Name = "PART_MinuteTextBox", Type = typeof(TimePicker))]
    public class TimePicker : Control
    {
        /// <see>
        /// Identifies the <see cref="IsDefaultNow" /> dependency property.
        /// </see>
        public static readonly DependencyProperty IsDefaultNowProperty =
            DependencyProperty.Register("IsDefaultNow", typeof(bool), typeof(TimePicker), new UIPropertyMetadata(true));

        /// <summary>
        /// Identifies the <see cref="TimeValue" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty TimeValueProperty =
            DependencyProperty.Register("TimeValue", typeof(DateTime?), typeof(TimePicker), new UIPropertyMetadata(null, OnTimeValueChange));

        /// <summary>
        /// Identifies the <see cref="Hour" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty HourProperty =
            DependencyProperty.Register("Hour", typeof(int), typeof(TimePicker), new UIPropertyMetadata(0, OnHourChange, CoerceHour));

        /// <summary>
        /// Identifies the <see cref="Minute" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty MinuteProperty =
            DependencyProperty.Register("Minute", typeof(int), typeof(TimePicker), new UIPropertyMetadata(0, OnMinuteChange, CoerceMinute));

        private TextBox hourTextBox;
        private TextBox minuteTextBox;

        /// <summary>
        /// Initializes the <see cref="TimePicker"/> class.
        /// </summary>
        static TimePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TimePicker), new FrameworkPropertyMetadata(typeof(TimePicker)));
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is default now.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is default now; otherwise, <c>false</c>.
        /// </value>
        public bool IsDefaultNow
        {
            get { return (bool)GetValue(IsDefaultNowProperty); }
            set { this.SetValue(IsDefaultNowProperty, value); }
        }

        /// <summary>
        /// Gets or sets the time value.
        /// </summary>
        /// <value>
        /// The time value.
        /// </value>
        public DateTime? TimeValue
        {
            get { return (DateTime?)GetValue(TimeValueProperty); }
            set { this.SetValue(TimeValueProperty, value); }
        }

        /// <summary>
        /// Gets or sets the hour.
        /// </summary>
        /// <value>
        /// The hour.
        /// </value>
        public int Hour
        {
            get { return (int)GetValue(HourProperty); }
            set { this.SetValue(HourProperty, value); }
        }

        /// <summary>
        /// Gets or sets the minute.
        /// </summary>
        /// <value>
        /// The minute.
        /// </value>
        public int Minute
        {
            get { return (int)GetValue(MinuteProperty); }
            set { this.SetValue(MinuteProperty, value); }
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate" />.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.hourTextBox = this.Template.FindName("PART_HourTextBox", this) as TextBox;
            this.minuteTextBox = Template.FindName("PART_MinuteTextBox", this) as TextBox;

            this.hourTextBox.PreviewKeyDown += new KeyEventHandler(this.HourTextBox_PreviewKeyDown);
            this.minuteTextBox.PreviewKeyDown += new KeyEventHandler(this.MinuteTextBox_PreviewKeyDown);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.FrameworkElement.Initialized"/> event. This method is invoked whenever <see cref="P:System.Windows.FrameworkElement.IsInitialized"/> is set to true internally.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.RoutedEventArgs"/> that contains the event data.</param>
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            if (this.IsDefaultNow)
            {
                DateTime dt = DateTime.Now;
                this.Hour = dt.Hour;
                this.Minute = dt.Minute;
            }
        }

        /// <summary>
        /// Called when TimeValue change.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnTimeValueChange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TimePicker timePicker = (TimePicker)sender;
            DateTime? dt = (DateTime?)e.NewValue;
            timePicker.Hour = dt == null ? 0 : dt.Value.Hour;
            timePicker.Minute = dt == null ? 0 : dt.Value.Minute;
        }

        /// <summary>
        /// Called when Hour change.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnHourChange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TimePicker timePicker = (TimePicker)sender;
            int hour = (int)e.NewValue;
            int minute = timePicker.Minute;
            if (timePicker.TimeValue.HasValue)
            {
                timePicker.SetCurrentValue(TimePicker.TimeValueProperty, timePicker.TimeValue.Value.Date.AddHours(hour).AddMinutes(minute));
            }
            else
            {
                timePicker.SetCurrentValue(TimePicker.TimeValueProperty, DateTime.MinValue.AddHours(hour).AddMinutes(minute));
            }
        }

        /// <summary>
        /// Called when Hour coerce.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="baseValue">The base value.</param>
        private static object CoerceHour(DependencyObject sender, object baseValue)
        {
            int value = (int)baseValue;
            if (value > 23)
            {
                value = 23;
            }

            if (value < 0)
            {
                value = 0;
            }

            return value;
        }

        /// <summary>
        /// Called when Minute change.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnMinuteChange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TimePicker timePicker = (TimePicker)sender;
            int hour = timePicker.Hour;
            int minute = (int)e.NewValue;
            if (timePicker.TimeValue.HasValue)
            {
                timePicker.SetCurrentValue(TimePicker.TimeValueProperty, timePicker.TimeValue.Value.Date.AddHours(hour).AddMinutes(minute));
            }
            else
            {
                timePicker.SetCurrentValue(TimePicker.TimeValueProperty, DateTime.MinValue.AddHours(hour).AddMinutes(minute));
            }
        }

        /// <summary>
        /// Called when Minute coerce.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="baseValue">The base value.</param>
        private static object CoerceMinute(DependencyObject sender, object baseValue)
        {
            int value = (int)baseValue;
            if (value > 59)
            {
                value = 59;
            }

            if (value < 0)
            {
                value = 0;
            }

            return value;
        }

        /// <summary>
        /// Handles the PreviewKeyDown event of the hourTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void HourTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    this.Hour++;
                    break;
                case Key.PageUp:
                    this.Hour += 10;
                    break;
                case Key.Down:
                    this.Hour--;
                    break;
                case Key.PageDown:
                    this.Hour -= 10;
                    break;
            }
        }

        /// <summary>
        /// Handles the PreviewKeyDown event of the MinuteTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void MinuteTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    this.Minute++;
                    break;
                case Key.PageUp:
                    this.Minute += 10;
                    break;
                case Key.Down:
                    this.Minute--;
                    break;
                case Key.PageDown:
                    this.Minute -= 10;
                    break;
            }
        }
    }
}
