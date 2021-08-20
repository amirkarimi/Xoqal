#region License
// DelayBinding.cs
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

namespace Xoqal.Presentation.Extentions
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Threading;

    /// <summary>
    /// 
    /// </summary>
    public class DelayBinding
    {
        private readonly BindingExpressionBase bindingExpression;
        private readonly DispatcherTimer timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="DelayBinding" /> class.
        /// </summary>
        /// <param name="bindingExpression">The binding expression.</param>
        /// <param name="bindingTarget">The binding target.</param>
        /// <param name="bindingTargetProperty">The binding target property.</param>
        /// <param name="delay">The delay.</param>
        protected DelayBinding(
            BindingExpressionBase bindingExpression,
            DependencyObject bindingTarget,
            DependencyProperty bindingTargetProperty,
            TimeSpan delay)
        {
            this.bindingExpression = bindingExpression;

            // Subscribe to notifications for when the target property changes. This event handler will be 
            // invoked when the user types, clicks, or anything else which changes the target property
            DependencyPropertyDescriptor descriptor = DependencyPropertyDescriptor.FromProperty(
                bindingTargetProperty, bindingTarget.GetType());
            descriptor.AddValueChanged(bindingTarget, this.OnBindingTargetTargetPropertyChanged);

            // Add support so that the Enter key causes an immediate commit
            var frameworkElement = bindingTarget as FrameworkElement;
            if (frameworkElement != null)
            {
                frameworkElement.KeyUp += this.OnBindingTargetKeyUp;
            }

            // Setup the timer, but it won't be started until changes are detected
            this.timer = new DispatcherTimer();
            this.timer.Tick += this.OnTimerTick;
            this.timer.Interval = delay;
        }

        /// <summary>
        /// Sets the binding.
        /// </summary>
        /// <param name="bindingTarget">The binding target.</param>
        /// <param name="bindingTargetProperty">The binding target property.</param>
        /// <param name="delay">The delay.</param>
        /// <param name="binding">The binding.</param>
        /// <returns></returns>
        public static object SetBinding(
            DependencyObject bindingTarget, DependencyProperty bindingTargetProperty, TimeSpan delay, Binding binding)
        {
            // Override some specific settings to enable the behavior of delay binding
            binding.Mode = BindingMode.TwoWay;
            binding.UpdateSourceTrigger = UpdateSourceTrigger.Explicit;

            // Apply and evaluate the binding
            BindingExpressionBase bindingExpression = BindingOperations.SetBinding(bindingTarget, bindingTargetProperty, binding);

            // Setup the delay timer around the binding. This object will live as long as the target element lives, since it subscribes to the changing event, 
            // and will be garbage collected as soon as the element isn't required (e.g., when it's Window closes) and the timer has stopped.
            new DelayBinding(bindingExpression, bindingTarget, bindingTargetProperty, delay);

            // Return the current value of the binding (since it will have been evaluated because of the binding above)
            return bindingTarget.GetValue(bindingTargetProperty);
        }

        /// <summary>
        /// Called when binding target key up.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyEventArgs" /> instance containing the event data.</param>
        private void OnBindingTargetKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
            {
                return;
            }

            this.timer.Stop();
            this.bindingExpression.UpdateSource();
        }

        /// <summary>
        /// Called when binding target target property changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void OnBindingTargetTargetPropertyChanged(object sender, EventArgs e)
        {
            this.timer.Stop();
            this.timer.Start();
        }

        /// <summary>
        /// Called when timer tick.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void OnTimerTick(object sender, EventArgs e)
        {
            this.timer.Stop();
            this.bindingExpression.UpdateSource();
        }
    }
}
