#region LICENSE
/* 
 * Copyright (C) 2007 Tim Taubert (twenty-three@users.berlios.de)
 * 
 * This file is part of Video4Linux.Net.
 *
 * Video4Linux.Net is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or
 * (at your option) any later version.
 *
 * Video4Linux.Net is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program. If not, see <http://www.gnu.org/licenses/>.
 */
#endregion LICENSE

using System;
using System.Runtime.InteropServices;

using Video4Linux.Analog.Kernel.V4L1;

namespace Video4Linux.Core
{
	internal class V4L1IOControl
	{
		private int deviceHandle;
		
		public V4L1IOControl(int deviceHandle)
		{
			this.deviceHandle = deviceHandle;
		}
		
		public int QueryDeviceCapabilities(ref video_capability cap)
		{
			return ioctl(deviceHandle, -2143521279, ref cap);
		}
		
		[DllImport("libc", SetLastError=true)]
		private static extern int
			ioctl(int device, int request, ref video_capability argp);
	}
}
