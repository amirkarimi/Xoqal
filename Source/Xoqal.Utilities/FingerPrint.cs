#region License
// FingerPrint.cs
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
    using System.Management;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Retrieve a finger print from current machine which is useful for licensing.
    /// </summary>
    /// <remarks>
    /// Got from "http://www.codeproject.com/Articles/28678/Generating-Unique-Key-Finger-Print-for-a-Computer" and clean by A. Karimi (karimi@dev-frame.com)
    /// </remarks>
    public class FingerPrint
    {
        private static string fingerPrint = string.Empty;

        /// <summary>
        /// Gets the machine id.
        /// </summary>
        /// <param name="components">The components.</param>
        /// <returns></returns>
        public static string GetMachineId(MachineIdComponents components)
        {
            if (string.IsNullOrEmpty(fingerPrint))
            {
                StringBuilder machineIdSource = new StringBuilder();

                if (components.HasFlag(MachineIdComponents.BaseBoard))
                {
                    machineIdSource.Append("BASE >> ");
                    machineIdSource.AppendLine(GetBaseId());
                }

                if (components.HasFlag(MachineIdComponents.Cpu))
                {
                    machineIdSource.Append("CPU >> ");
                    machineIdSource.AppendLine(GetCpuId());
                }

                if (components.HasFlag(MachineIdComponents.Bios))
                {
                    machineIdSource.Append("BIOS >> ");
                    machineIdSource.AppendLine(GetBaseId());
                }

                if (components.HasFlag(MachineIdComponents.Disk))
                {
                    machineIdSource.Append("DISK >> ");
                    machineIdSource.AppendLine(GetDiskId());
                }

                if (components.HasFlag(MachineIdComponents.Video))
                {
                    machineIdSource.Append("VIDEO >> ");
                    machineIdSource.AppendLine(GetVideoId());
                }

                if (components.HasFlag(MachineIdComponents.MacAddress))
                {
                    machineIdSource.Append("MAC >> ");
                    machineIdSource.AppendLine(GetMacId());
                }

                fingerPrint = GetHash(machineIdSource.ToString());
            }

            return fingerPrint;
        }

        /// <summary>
        /// Gets the hash.
        /// </summary>
        /// <param name="s"> The s. </param>
        /// <returns> </returns>
        private static string GetHash(string s)
        {
            MD5 sec = new MD5CryptoServiceProvider();
            var enc = new ASCIIEncoding();
            byte[] bt = enc.GetBytes(s);
            return GetHexString(sec.ComputeHash(bt));
        }

        /// <summary>
        /// Gets the hex string.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns> </returns>
        private static string GetHexString(byte[] bytes)
        {
            string s = string.Empty;
            for (int i = 0; i < bytes.Length; i++)
            {
                byte b = bytes[i];
                int n = b;
                int n1 = n & 15;
                int n2 = (n >> 4) & 15;
                if (n2 > 9)
                {
                    s += ((char)(n2 - 10 + 'A')).ToString();
                }
                else
                {
                    s += n2.ToString();
                }

                if (n1 > 9)
                {
                    s += ((char)(n1 - 10 + 'A')).ToString();
                }
                else
                {
                    s += n1.ToString();
                }

                if ((i + 1) != bytes.Length && (i + 1) % 2 == 0)
                {
                    s += "-";
                }
            }

            return s;
        }

        #region Original Device ID Getting Code

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <param name="wmiClass"> The WMI class. </param>
        /// <param name="wmiProperty"> The WMI property. </param>
        /// <param name="wmiMustBeTrue"> The WMI must be true. </param>
        /// <returns> </returns>
        private static string GetIdentifier(string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string result = string.Empty;
            var mc = new ManagementClass(wmiClass);
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (mo[wmiMustBeTrue].ToString() == "True")
                {
                    //Only get the first one
                    if (result == string.Empty)
                    {
                        try
                        {
                            result = mo[wmiProperty].ToString();
                            break;
                        }
                        catch
                        {
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <param name="wmiClass"> The WMI class. </param>
        /// <param name="wmiProperty"> The WMI property. </param>
        /// <returns> </returns>
        private static string GetIdentifier(string wmiClass, string wmiProperty)
        {
            string result = string.Empty;
            var mc = new ManagementClass(wmiClass);
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                //Only get the first one
                if (result == string.Empty)
                {
                    try
                    {
                        result = mo[wmiProperty].ToString();
                        break;
                    }
                    catch
                    {
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the CPU ID.
        /// </summary>
        /// <returns> </returns>
        private static string GetCpuId()
        {
            //Uses first CPU identifier available in order of preference
            //Don't get all identifiers, as it is very time consuming
            string retVal = GetIdentifier("Win32_Processor", "UniqueId");
            if (retVal == string.Empty)
            {
                // If no UniqueID, use ProcessorID
                retVal = GetIdentifier("Win32_Processor", "ProcessorId");
                if (retVal == string.Empty)
                {
                    //If no ProcessorId, use Name
                    retVal = GetIdentifier("Win32_Processor", "Name");
                    if (retVal == string.Empty)
                    {
                        // If no Name, use Manufacturer
                        retVal = GetIdentifier("Win32_Processor", "Manufacturer");
                    }

                    //Add clock speed for extra security
                    retVal += GetIdentifier("Win32_Processor", "MaxClockSpeed");
                }
            }

            return retVal;
        }

        /// <summary>
        /// Gets the bios id.
        /// </summary>
        /// <returns> </returns>
        private static string GetBiosId()
        {
            return GetIdentifier("Win32_BIOS", "Manufacturer") + GetIdentifier("Win32_BIOS", "SMBIOSBIOSVersion") +
                GetIdentifier("Win32_BIOS", "IdentificationCode") + GetIdentifier("Win32_BIOS", "SerialNumber") +
                    GetIdentifier("Win32_BIOS", "ReleaseDate") + GetIdentifier("Win32_BIOS", "Version");
        }

        /// <summary>
        /// Gets the disk id.
        /// </summary>
        /// <returns> </returns>
        private static string GetDiskId()
        {
            return GetIdentifier("Win32_DiskDrive", "Model") + GetIdentifier("Win32_DiskDrive", "Manufacturer") +
                GetIdentifier("Win32_DiskDrive", "Signature") + GetIdentifier("Win32_DiskDrive", "TotalHeads");
        }

        private static string GetBaseId()
        {
            return GetIdentifier("Win32_BaseBoard", "Model") + GetIdentifier("Win32_BaseBoard", "Manufacturer") +
                GetIdentifier("Win32_BaseBoard", "Name") + GetIdentifier("Win32_BaseBoard", "SerialNumber");
        }

        /// <summary>
        /// Gets the video id.
        /// </summary>
        /// <returns> </returns>
        private static string GetVideoId()
        {
            return GetIdentifier("Win32_VideoController", "DriverVersion") + GetIdentifier("Win32_VideoController", "Name");
        }

        /// <summary>
        /// Macs the id.
        /// </summary>
        /// <returns> </returns>
        private static string GetMacId()
        {
            return GetIdentifier("Win32_NetworkAdapterConfiguration", "MACAddress", "IPEnabled");
        }

        #endregion
    }
}
