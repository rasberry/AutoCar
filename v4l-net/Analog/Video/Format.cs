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
	/// <summary>
	/// Represents a picture format.
	/// </summary>
	public class Format
	{
		#region Private Fields
		
		private v4l2_fmtdesc fmtdesc;
		
		#endregion Private Fields
		
		#region Constructors and Destructors
		
		internal Format(v4l2_fmtdesc fmtdesc)
		{
			this.fmtdesc = fmtdesc;
		}
		
		#endregion Constructors and Destructors
		
		#region Public Properties
		
		/*public v4l2_buf_type Type
		{
			get { return fmtdesc.type; }
		}*/
		
		public uint Flags
		{
			get { return fmtdesc.flags; }
		}
		
		public string Description
		{
			get { return fmtdesc.description; }
		}
		
		public v4l2_pix_format_id pixelformat
		{
			get { return fmtdesc.pixelformat; }
		}
		
		#endregion Public Properties
		
		#region Internal Properties
		
		internal uint Index
		{
			get { return fmtdesc.index; }
		}
		
		#endregion Internal Properties
	}
}
