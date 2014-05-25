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
	/// <summary>
	/// Represents an input device type.
	/// </summary>
	public enum InputType : uint
	{
		/// <summary>
		/// This input uses a tuner (RF demodulator).
		/// </summary>
		Tuner  = 1,
		
		/// <summary>
		/// Analog baseband input, for example CVBS/composite video, S-Video, RGB.
		/// </summary>
		Camera = 2
	}
}
