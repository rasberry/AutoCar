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
	/// Represents a input device status.
	/// </summary>
	public enum InputStatus : uint
	{
		// General
		/// <summary>
		/// The device is off.
		/// </summary>
		NoPower              = 0x00000001,
		/// <summary>
		/// The device has no signal.
		/// </summary>
		NoSignal             = 0x00000002,
		/// <summary>
		/// The hardware supports color decoding but does not detect color modulation in the signal.
		/// </summary>
		NoColor              = 0x00000004,
			
		// Analog Video
		/// <summary>
		/// No horizontal synchronization lock.
		/// </summary>
		NoHorizontalSyncLock = 0x00000100,
		/// <summary>
		/// A color killer circuit automatically disables color decoding when it detects no color
		/// modulation. When this flag is set the color killer is enabled and has shut off color
		/// decoding.
		/// </summary>
		ColorKill            = 0x00000200,
		
		// Digital Video
		/// <summary>
		/// No synchronization lock.
		/// </summary>
		NoSyncLock           = 0x00010000,
		/// <summary>
		/// No equalizer lock.
		/// </summary>
		NoEqualizerLock      = 0x00020000,
		/// <summary>
		/// Carrier recovery failed.
		/// </summary>
		NoCarrier            = 0x00040000,
		
		// VCR and Set-Top Box
		/// <summary>
		/// Macrovision is an analog copy prevention system mangling the video signal to confuse
		/// video recorders. When this flag is set Macrovision has been detected.
		/// </summary>
		Macrovision          = 0x01000000,
		/// <summary>
		/// Conditional access denied.
		/// </summary>
		NoAccess             = 0x02000000,
		/// <summary>
		/// VTR time constant.
		/// </summary>
		VTR                  = 0x04000000
	}
}