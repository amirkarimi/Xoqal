#region License
// FocusBehaviour.cs
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;

    /// <summary>
    /// Provide set focus operation for binding.
    /// </summary>
    public static class FocusBehaviour
    {
        /// <summary>
        /// Identifies the ForceFocus dependency property
        /// </summary>
        public static readonly DependencyProperty ForceFocusProperty =
            DependencyProperty.RegisterAttached(
                "ForceFocus",
                typeof(bool),
                typeof(FocusBehaviour),
                new FrameworkPropertyMetadata(
                    false,
                    FrameworkPropertyMetadataOptions.None,
                    (d, e) =>
                    {
                        if ((bool)e.NewValue)
                        {
                            if (d is UIElement)
                            {
                                ((UIElement)d).Focus();
                            }
                        }
                    }));
        
        /// <summary>
        /// Gets the force focus.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        public static bool GetForceFocus(DependencyObject d)
        {
            return (bool)d.GetValue(FocusBehaviour.ForceFocusProperty);
        }

        /// <summary>
        /// Sets the force focus.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="val">if set to <c>true</c> [val].</param>
        public static void SetForceFocus(DependencyObject d, bool val)
        {
            d.SetValue(FocusBehaviour.ForceFocusProperty, val);
        }
    }
}
