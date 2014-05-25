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

namespace Video4Linux.Analog.Audio
{
	/// <summary>
	/// Represents a capability of an audio input.
	/// </summary>
	public enum InputCapability : uint
	{
		/// <summary>
		/// This is a stereo input. The ﬂag is intended to automatically disable stereo
		/// recording etc. when the signal is always monaural.
		/// </summary>
		Stereo               = 0x00000001,
		/// <summary>
		/// Automatic Volume Level mode is supported.
		/// </summary>
		AutomaticVolumeLevel = 0x00000002
	}
}