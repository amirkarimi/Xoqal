#region License
// MachineIdComponents.cs
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
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Machine ID components which will be used to generate a machine ID.
    /// </summary>
    [Flags]
    public enum MachineIdComponents
    {
        /// <summary>
        /// BaseBoard
        /// </summary>
        BaseBoard = 1,

        /// <summary>
        /// CPU
        /// </summary>
        Cpu = 2,

        /// <summary>
        /// BIOS
        /// </summary>
        Bios = 4,

        /// <summary>
        /// Disk
        /// </summary>
        Disk = 8,

        /// <summary>
        /// Video
        /// </summary>
        Video = 16,

        /// <summary>
        /// MacAddress
        /// </summary>
        MacAddress = 32
    }
}
