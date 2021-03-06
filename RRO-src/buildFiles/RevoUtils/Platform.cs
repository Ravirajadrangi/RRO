﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace RevoUtils
{
    public static class Platform
    {
        
        public enum PlatformFlavor { Windows, CentOS, SLES, OpenSUSE, Ubuntu, OSX, UnknownUnix, Unknown }

        public static System.PlatformID GetPlatform()
        {  
            return Environment.OSVersion.Platform;
        }
        public static PlatformFlavor GetPlatformFlavor()
        {
            System.PlatformID platform = GetPlatform();

            if (platform == PlatformID.Win32NT)
            {
                return PlatformFlavor.Windows;
            }
            else if (platform == PlatformID.Unix)
            {
                if (System.IO.File.Exists("/etc/issue"))
                {
                    string issueText = System.IO.File.ReadAllText("/etc/issue");

                    if (issueText.Contains("CentOS"))
                        return PlatformFlavor.CentOS;
                    else if (issueText.Contains("SUSE Linux Enterprise"))
                        return PlatformFlavor.SLES;
                    else if (issueText.Contains("Ubuntu"))
                        return PlatformFlavor.Ubuntu;
                    else if (issueText.Contains("openSUSE"))
                        return PlatformFlavor.OpenSUSE;
                    else
                        return PlatformFlavor.UnknownUnix;
                }
                else
                    return PlatformFlavor.UnknownUnix;
            }
            else if (platform == PlatformID.MacOSX)
            {
                return PlatformFlavor.OSX;
            }
            else
            {
                return PlatformFlavor.Unknown;
            }
        }
        public static System.Version GetReleaseVersion()
        {
            System.PlatformID platform = GetPlatform();
            PlatformFlavor flavor = GetPlatformFlavor();
            
            if(platform == PlatformID.Win32NT)
            {
                return System.Environment.OSVersion.Version;
            }
            else if(platform == PlatformID.Unix)
            {
                if(flavor == PlatformFlavor.CentOS)
                {
                    if (System.IO.File.Exists("/etc/issue"))
                    {

                        string issueText = System.IO.File.ReadAllText("/etc/issue");
                        var versionString = Regex.Match(issueText, "[0-9].[0-9]{1,2}");
                        if (versionString.Success)
                            return new System.Version(versionString.Value);
                        else
                            return new System.Version(0, 0);
                    }
                    else
                    {
                        throw new Exception("No /etc/issue file for Flavor CentOS");
                    }
                }

                else if (flavor == PlatformFlavor.SLES)
                {
                    if (System.IO.File.Exists("/etc/issue"))
                    {
                        string issueText = System.IO.File.ReadAllText("/etc/issue");
                        var versionString = Regex.Match(issueText, "1[0-3]");

                        if (versionString.Success)
                            return new System.Version(Int32.Parse(versionString.ToString()), 0);
                        else
                            return new System.Version(0, 0);
                    }
                    else
                    {
                        throw new Exception("No /etc/issue file for Flavor SLES");
                    }
                }
                else if (flavor == PlatformFlavor.OpenSUSE)
                {
                    if (System.IO.File.Exists("/etc/issue"))
                    {
                        string issueText = System.IO.File.ReadAllText("/etc/issue");
                        var versionString = Regex.Match(issueText, "[0-9][0-9]{2}.[0-9]{1}");

                        if (versionString.Success)
                            return new System.Version(Int32.Parse(versionString.ToString()), 0);
                        else
                            return new System.Version(0, 0);
                    }
                    else
                    {
                        throw new Exception("No /etc/issue file for Flavor SLES");
                    }
                }
                else if (flavor == PlatformFlavor.Ubuntu)
                {
                    if (System.IO.File.Exists("/etc/issue"))
                    {
                        string issueText = System.IO.File.ReadAllText("/etc/issue");
                        var versionString = Regex.Match(issueText, "[0-9]{2}.[0-9]{2}");

                        if (versionString.Success)
                            return new System.Version(versionString.Value);
                        else
                            return new System.Version(0, 0);
                    }
                    else
                    {
                        throw new Exception("No /etc/issue file for Flavor Ubuntu");
                    }
                }
                else 
                {
                    throw new Exception("Linux flavor not implemented yet");
                }
            }
            else if (platform == PlatformID.MacOSX)
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new NotImplementedException();   
            }
        }
    }
}
