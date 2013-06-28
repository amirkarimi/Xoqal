#region License
// ViewModelExtensions.cs
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

namespace Xoqal.Core.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;

    /// <summary>
    /// The view model extensions.
    /// </summary>
    public static class ViewModelExtensions
    {
        /// <summary>
        /// Raises the <see cref="INotifyPropertyChanged"/> event.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="notificationObject"></param>
        /// <param name="expression"></param>
        public static void RaisePropertyChanged<T, TProperty>(this T notificationObject, Expression<Func<TProperty>> expression)
            where T : Models.INotificationObject
        {
            notificationObject.RaisePropertyChanged(ViewModelExtensions.GetPropertyName(expression));
        }

        /// <summary>
        /// Gets the property name which specified in the expression.
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string GetPropertyName<TProperty>(Expression<Func<TProperty>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;

            if (memberExpression == null)
            {
                throw new NotSupportedException("Non-member expressions are not supported.");
            }

            return memberExpression.Member.Name;
        }
    }
}
