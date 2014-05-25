#region LICENSE
/* 
 * Copyright (C) 2007-2008 Tim Taubert (twenty-three@users.berlios.de)
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

using Mono.Unix.Native;

using Video4Linux.Analog.Kernel.V4L1;
using Video4Linux.Core;

namespace Video4Linux
{
	public class V4L1Adapter
	{
		public string Name
		{
			get
			{
				int res = ioControl.QueryDeviceCapabilities(ref cap);
				Console.WriteLine("res = " + res);
				
				return cap.name;
			}
		}
		
		public int ChannelCount
		{
			get
			{
				int res = ioControl.QueryDeviceCapabilities(ref cap);
				Console.WriteLine("res = " + res);
				
				return cap.channels;
			}
		}
		
		private int deviceHandle;
		private V4L1IOControl ioControl;
		private video_capability cap;
		
		public V4L1Adapter(string path)
		{
			deviceHandle = Syscall.open(path, OpenFlags.O_RDWR);
			if (deviceHandle < 0)
				throw new Exception("Adapter " + path + " cannot be opened.");
			
			ioControl = new V4L1IOControl(deviceHandle);
		}
		
		~V4L1Adapter()
		{
			Syscall.close(deviceHandle);
		}
	}
}
