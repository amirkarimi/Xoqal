#region License
// DelayBindingExtension.cs
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
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    [MarkupExtensionReturnType(typeof(object))]
    public class DelayBindingExtension : MarkupExtension
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DelayBindingExtension" /> class.
        /// </summary>
        public DelayBindingExtension()
        {
            this.Delay = TimeSpan.FromSeconds(0.5);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelayBindingExtension" /> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public DelayBindingExtension(PropertyPath path)
            : this()
        {
            this.Path = path;
        }

        /// <summary>
        /// Gets or sets the converter.
        /// </summary>
        /// <value>
        /// The converter.
        /// </value>
        public IValueConverter Converter { get; set; }

        /// <summary>
        /// Gets or sets the converter parameter.
        /// </summary>
        /// <value>
        /// The converter parameter.
        /// </value>
        public object ConverterParameter { get; set; }

        /// <summary>
        /// Gets or sets the name of the element.
        /// </summary>
        /// <value>
        /// The name of the element.
        /// </value>
        public string ElementName { get; set; }

        /// <summary>
        /// Gets or sets the relative source.
        /// </summary>
        /// <value>
        /// The relative source.
        /// </value>
        public RelativeSource RelativeSource { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public object Source { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether validates on data errors.
        /// </summary>
        /// <value>
        /// <c>true</c> if validates on data errors; otherwise, <c>false</c>.
        /// </value>
        public bool ValidatesOnDataErrors { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether validates on exceptions.
        /// </summary>
        /// <value>
        /// <c>true</c> if validates on exceptions; otherwise, <c>false</c>.
        /// </value>
        public bool ValidatesOnExceptions { get; set; }

        /// <summary>
        /// Gets or sets the delay.
        /// </summary>
        /// <value>
        /// The delay.
        /// </value>
        public TimeSpan Delay { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        [ConstructorArgument("path")]
        public PropertyPath Path { get; set; }

        /// <summary>
        /// Gets or sets the converter culture.
        /// </summary>
        /// <value>
        /// The converter culture.
        /// </value>
        [TypeConverter(typeof(CultureInfoIetfLanguageTagConverter))]
        public CultureInfo ConverterCulture { get; set; }

        /// <summary>
        /// When implemented in a derived class, returns an object that is provided as the value of the target property for this markup extension.
        /// </summary>
        /// <param name="serviceProvider">A service provider helper that can provide services for the markup extension.</param>
        /// <returns>
        /// The object value to set on the property where the extension is applied.
        /// </returns>
        /// <exception cref="System.NotSupportedException"></exception>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var valueProvider = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            if (valueProvider != null)
            {
                var bindingTarget = valueProvider.TargetObject as DependencyObject;
                var bindingProperty = valueProvider.TargetProperty as DependencyProperty;
                if (bindingProperty == null || bindingTarget == null)
                {
                    throw new NotSupportedException(
                        string.Format(
                            "The property '{0}' on target '{1}' is not valid for a DelayBinding. The DelayBinding target must be a DependencyObject, " +
                                "and the target property must be a DependencyProperty.",
                            valueProvider.TargetProperty,
                            valueProvider.TargetObject));
                }

                var binding = new Binding
                {
                    Path = this.Path,
                    Converter = this.Converter,
                    ConverterCulture = this.ConverterCulture,
                    ConverterParameter = this.ConverterParameter
                };
                if (this.ElementName != null)
                {
                    binding.ElementName = this.ElementName;
                }

                if (this.RelativeSource != null)
                {
                    binding.RelativeSource = this.RelativeSource;
                }

                if (this.Source != null)
                {
                    binding.Source = this.Source;
                }

                binding.ValidatesOnDataErrors = this.ValidatesOnDataErrors;
                binding.ValidatesOnExceptions = this.ValidatesOnExceptions;

                return DelayBinding.SetBinding(bindingTarget, bindingProperty, this.Delay, binding);
            }

            return null;
        }
    }
}
