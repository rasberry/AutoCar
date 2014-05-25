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

namespace Video4Linux.Analog
{
	/// <summary>
	/// Represents a capability of a Video4Linux device.
	/// </summary>
	public enum AdapterCapability : uint
	{
		/// <summary>
		/// The device supports the video capture interface.
		/// </summary>
		VideoCapture       = 0x00000001,
		/// <summary>
		/// The device supports the video output interface.
		/// </summary>
		VideoOutput        = 0x00000002,
		/// <summary>
		/// The device supports the video overlay interface. A video overlay device
		/// typically stores captured images directly in the video memory of a graphics
		/// card, with hardware clipping and scaling.
		/// </summary>
		VideoOverlay       = 0x00000004,
		/// <summary>
		/// The device supports the Raw VBI Capture interface, providing Teletext and
		/// Closed Caption data.
		/// </summary>
		VBICapture         = 0x00000010,
		/// <summary>
		/// The device supports the Raw VBI Output interface.
		/// </summary>
		VBIOutput          = 0x00000020,
		/// <summary>
		/// The device supports the Sliced VBI Capture interface.
		/// </summary>
		SlicedVBICapture   = 0x00000040,
		/// <summary>
		/// The device supports the Sliced VBI Output interface.
		/// </summary>
		SlicedVBIOutput    = 0x00000080,
		CaptureRDS         = 0x00000100,
		/// <summary>
		/// The device supports the Video Output Overlay (OSD) interface. Unlike the
		/// Video Overlay interface, this is a secondary function of video output devices
		/// and overlays an image onto an outgoing video signal.
		/// </summary>
		VideoOutputOverlay = 0x00000200,
		/// <summary>
		/// The device has some sort of tuner or modulator to receive or emit RF-modulated
		/// video signals.
		/// </summary>
		Tuner              = 0x00010000,
		/// <summary>
		/// The device has audio inputs or outputs. It may or may not support audio
		/// recording or playback, in PCM or compressed formats.
		/// </summary>
		Audio              = 0x00020000,
		/// <summary>
		/// The device is a radio receiver.
		/// </summary>
		Radio              = 0x00040000,
		/// <summary>
		/// The device supports the read() and/or write() I/O methods.
		/// </summary>
		ReadWrite          = 0x01000000,
		/// <summary>
		/// The device supports the asynchronous I/O methods.
		/// </summary>
		AsyncIO            = 0x02000000,
		/// <summary>
		/// The device supports the streaming I/O method.
		/// </summary>
		Streaming          = 0x04000000
	}
}
