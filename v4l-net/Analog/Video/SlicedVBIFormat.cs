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

namespace Video4Linux.Analog.Video
{
	abstract public class SlicedVBIFormat : BaseFormat
	{
		#region Constructors and Destructors
		
		internal SlicedVBIFormat()
			: base()
		{}
		
		#endregion Constructors and Destructors
		
		#region Public Properties
		
		/*public uint ServiceSet
		{
			get { return format.fmt.sliced.service_set; }
			set { format.fmt.sliced.service_set = value; }
		}*/
		
		/*public uint service_set;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=48)]
		public ushort[] service_lines;
		public uint io_size;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
		public uint[] reserved;*/
		
		#endregion Public Properties
	}
}