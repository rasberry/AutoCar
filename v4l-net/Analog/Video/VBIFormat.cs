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
	abstract public class VBIFormat : BaseFormat
	{
		#region Constructors and Destructors
		
		internal VBIFormat()
			: base()
		{}
		
		#endregion Constructors and Destructors
		
		#region Public Properties
		
		public uint SamplingRate
		{
			get { return format.fmt.vbi.sampling_rate; }
			set { format.fmt.vbi.sampling_rate = value; }
		}
		
		/*public uint offset;
		public uint samples_per_line;
		public uint sample_format;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
		public uint[] start;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
		public uint[] count;*/
		
		#endregion Public Properties
	}
}
