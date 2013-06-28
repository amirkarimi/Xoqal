#region License
// EnumHelper.cs
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

namespace Xoqal.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;

    /// <summary>
    /// Helps display the enum items.
    /// </summary>
    public class EnumHelper
    {
        /// <summary>
        /// Gets the enum display.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string GetEnumDisplay<T>(T value)
        {
            return GetEnumDisplay(typeof(T), value);
        }

        /// <summary>
        /// Gets the enum display.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string GetEnumDisplay(Type type, object value)
        {
            string enumName = Enum.GetName(type, value);
            if (enumName == null)
            {
                return string.Empty;
            }

            FieldInfo f = type.GetField(enumName);
            if (f != null)
            {
                object[] attr = f.GetCustomAttributes(typeof(DisplayAttribute), false);
                if (attr.Length != 0)
                {
                    var displayAttribute = (DisplayAttribute)attr[0];
                    return Utilities.ResourceHelper.GetResourceValue(displayAttribute.ResourceType, displayAttribute.Name);
                }

                return enumName;
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the enum displays.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string[] GetEnumDisplays<T>()
        {
            return GetEnumDisplays(typeof(T));
        }

        /// <summary>
        /// Gets the enum displays.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static string[] GetEnumDisplays(Type type)
        {
            var displays = new List<string>();
            foreach (FieldInfo f in type.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                object[] attr = f.GetCustomAttributes(typeof(DisplayAttribute), false);
                if (attr.Length != 0)
                {
                    var displayAttribute = (DisplayAttribute)attr[0];
                    displays.Add(Utilities.ResourceHelper.GetResourceValue(displayAttribute.ResourceType, displayAttribute.Name));
                }
                else
                {
                    displays.Add(f.Name);
                }
            }

            return displays.ToArray();
        }

        /// <summary>
        /// Gets the enum display.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static string GetEnumDisplay(object obj)
        {
            Type type = obj.GetType();
            if (type.IsEnum)
            {
                FieldInfo f = type.GetField(obj.ToString());
                if (f != null)
                {
                    object[] attr = f.GetCustomAttributes(typeof(DisplayAttribute), false);
                    if (attr.Length != 0)
                    {
                        var displayAttribute = (DisplayAttribute)attr[0];
                        return Utilities.ResourceHelper.GetResourceValue(displayAttribute.ResourceType, displayAttribute.Name);
                    }
                    else
                    {
                        return f.Name;
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the enum object displays.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static Dictionary<object, string> GetEnumObjectDisplays(Type type)
        {
            var displays = new Dictionary<object, string>();
            foreach (FieldInfo field in type.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                object[] attr = field.GetCustomAttributes(typeof(DisplayAttribute), false);
                if (attr.Length == 0)
                {
                    displays.Add(Enum.Parse(type, field.Name), field.Name);
                }
                else
                {
                    var displayAttribute = (DisplayAttribute)attr[0];
                    displays.Add(
                        Enum.Parse(type, field.Name),
                        Utilities.ResourceHelper.GetResourceValue(displayAttribute.ResourceType, displayAttribute.Name));
                }
            }

            return displays;
        }

        /// <summary>
        /// Gets the enum value displays.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static Dictionary<object, string> GetEnumValueDisplays(Type type)
        {
            var displays = new Dictionary<object, string>();
            foreach (FieldInfo field in type.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                object[] attr = field.GetCustomAttributes(typeof(DisplayAttribute), false);
                if (attr.Length == 0)
                {
                    displays.Add(Convert.ChangeType(Enum.Parse(type, field.Name), Enum.GetUnderlyingType(type)), field.Name);
                }
                else
                {
                    var displayAttribute = (DisplayAttribute)attr[0];
                    displays.Add(
                        Convert.ChangeType(Enum.Parse(type, field.Name), Enum.GetUnderlyingType(type)),
                        Utilities.ResourceHelper.GetResourceValue(displayAttribute.ResourceType, displayAttribute.Name));
                }
            }

            return displays;
        }

        /// <summary>
        /// Gets the enum by display.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="displayName">The display name.</param>
        /// <returns></returns>
        public static object GetEnumByDisplay<T>(string displayName)
        {
            foreach (FieldInfo f in typeof(T).GetFields())
            {
                object[] attr = f.GetCustomAttributes(typeof(DisplayAttribute), false);
                if (attr.Length != 0)
                {
                    var displayAttribute = (DisplayAttribute)attr[0];
                    if (displayName == Utilities.ResourceHelper.GetResourceValue(displayAttribute.ResourceType, displayAttribute.Name))
                    {
                        return Enum.Parse(typeof(T), f.Name);
                    }
                }
            }

            return null;
        }
    }
}
