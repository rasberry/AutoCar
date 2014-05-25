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
	/// Represents a video transmission standard.
	/// </summary>
	public class Standard
	{
		#region Private Fields
		
		private v4l2_standard standard;
		
		#endregion Private Fields
		
		#region Constructors and Destructors
		
		internal Standard(v4l2_standard standard)
		{
			this.standard = standard;
		}
		
		#endregion Constructors and Destructors
		
		#region Internal Properties
		
		internal v4l2_std_id Id
		{
			get { return standard.id; }
		}
		
		/// <summary>
		/// Gets the index of this standard in the list of all available standards.
		/// </summary>
		/// <value>The index of this standard.</value>
		internal uint Index
		{
			get { return standard.index; }
		}
		
		#endregion Internal Properties
		
		#region Public Properties
		
		/// <summary>
		/// Gets the name of the video transmission standard.
		/// </summary>
		/// <value>The name of the standard.</value>
		public string Name
		{
			get { return standard.name; }
		}
		
		public uint FrameLines
		{
			get { return standard.framelines; }
		}
		
		#endregion Public Properties
	}
}