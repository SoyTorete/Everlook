//
//  WinWindowsInfoInitializer.cs
//
//  The Open Toolkit Library License
//
//  Copyright (c) 2006 - 2009 the Open Toolkit library, except where noted.
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights to
//  use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
//  the Software, and to permit persons to whom the Software is furnished to do
//  so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in all
//  copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
//  OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//  NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
//  HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
//  WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
//  OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Runtime.InteropServices;
using System.Security;
using OpenTK.Platform;

namespace OpenTK.Win
{
	public static class WinWindowsInfoInitializer
	{
		private const string WinLibGDKName = "libgdk-3-0.dll";
		
		public static IWindowInfo Initialize(IntPtr gdkWindowHandle)
		{
			IntPtr windowHandle = gdk_win32_window_get_handle(gdkWindowHandle);
			return Utilities.CreateWindowsWindowInfo(windowHandle);
		}

		[SuppressUnmanagedCodeSecurity, DllImport(WinLibGDKName, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr gdk_win32_window_get_handle(IntPtr w);

	}
}