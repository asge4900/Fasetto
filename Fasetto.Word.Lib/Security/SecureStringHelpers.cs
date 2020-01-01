﻿using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Fasetto.Word.Lib
{
    /// <summary>
    /// Helpers for the <see cref="SecureString"/> class
    /// </summary>
    public static class SecureStringHelpers
    {
        /// <summary>
        /// Unsecure a <see cref="SecureString"/> to plain text
        /// </summary>
        /// <param name="secureString">The secure string</param>
        /// <returns></returns>
        public static string Unsecure(this SecureString secureString)
        {
            //Make sure we have a secure string
            if (secureString == null)
            {
                return string.Empty;
            }

            //Get a pointer for an unsecure string in memory
            var unmanagedString = IntPtr.Zero;

            try
            {
                //Unsecures the password
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                //Clean up any memory allocation
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
