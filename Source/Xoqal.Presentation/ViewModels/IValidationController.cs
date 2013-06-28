#region License
// IValidationController.cs
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

namespace Xoqal.Presentation.ViewModels
{
    using System.Windows;

    /// <summary>
    /// Represents the validation helper for view-models.
    /// </summary>
    public interface IValidationController
    {
        /// <summary>
        /// Gets a value indicating whether the attached element has any validation error.
        /// </summary>
        /// <value> <c>true</c> if the attached element has any validation error; otherwise, <c>false</c> . </value>
        bool HasError { get; }

        /// <summary>
        /// Gets a value indicating whether validation controller is attached to the corresponding element.
        /// </summary>
        /// <value> <c>true</c> if validation controller is attached to the corresponding element; otherwise, <c>false</c> . </value>
        bool IsAttached { get; }

        /// <summary>
        /// Attaches to the element for validation control.
        /// </summary>
        /// <param name="element"> </param>
        void Attach(DependencyObject element);
    }
}
