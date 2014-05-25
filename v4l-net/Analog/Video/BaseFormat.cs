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

using Video4Linux.Analog.Kernel;

namespace Video4Linux.Analog.Video
{
	abstract public class BaseFormat
	{
		#region Protected Fields
		
		internal v4l2_format format;
		
		#endregion Protected Fields
		
		#region Constructors and Destructors
		
		internal BaseFormat()
			: this(new v4l2_format())
		{}
		
		internal BaseFormat(v4l2_format format)
		{
			this.format = format;
		}
		
		#endregion Constructors and Destructors
		
		#region Internal Methods
		
		internal void Get(Analog.Adapter adapter)
		{
			if (adapter.IoControl.GetFormat(ref format) < 0)
				throw new Exception("VIDIOC_G_FMT");
		}
		
		internal void Set(Analog.Adapter adapter)
		{
			if (adapter.IoControl.SetFormat(ref format) < 0)
				throw new Exception("VIDIOC_S_FMT");
		}
		
		internal v4l2_format ToStruct()
		{
			return format;
		}
		
		#endregion Internal Methods
	}
}