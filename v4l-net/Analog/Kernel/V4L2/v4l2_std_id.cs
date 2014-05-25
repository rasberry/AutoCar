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

namespace Video4Linux.Analog.Kernel
{
	internal enum v4l2_std_id : ulong
	{
		Unknown     = 0,
		PAL_B       = 0x00000001,
		PAL_B1      = 0x00000002,
		PAL_G       = 0x00000004,
		PAL_H       = 0x00000008,
		PAL_I       = 0x00000010,
		PAL_D       = 0x00000020,
		PAL_D1      = 0x00000040,
		PAL_K       = 0x00000080,
		PAL_M       = 0x00000100,
		PAL_N       = 0x00000200,
		PAL_Nc      = 0x00000400,
		PAL_60      = 0x00000800,
		NTSC_M      = 0x00001000,
		NTSC_M_JP   = 0x00002000,
		NTSC_443    = 0x00004000,
		NTSC_M_KR   = 0x00008000,
		SECAM_B     = 0x00010000,
		SECAM_D     = 0x00020000,
		SECAM_G     = 0x00040000,
		SECAM_H     = 0x00080000,
		SECAM_K     = 0x00100000,
		SECAM_K1    = 0x00200000,
		SECAM_L     = 0x00400000,
		SECAM_LC    = 0x00800000,
		ATSC_8_VSB  = 0x01000000,
		ATSC_16_VSB = 0x02000000,
		
		/*
		 * Composite video standards.
		 */
		Composite_PAL_BG   = PAL_B | PAL_B1 | PAL_G,
		Composite_PAL_B    = PAL_B | PAL_B1 | SECAM_B,
		Composite_PAL_GH   = PAL_G | PAL_H  | SECAM_G | SECAM_H,
		Composite_PAL_DK   = PAL_D | PAL_D1 | PAL_K,
		Composite_PAL      = Composite_PAL_BG | Composite_PAL_DK | PAL_H | PAL_I,
		Composite_NTSC     = NTSC_M | NTSC_M_JP | NTSC_M_KR,
		Composite_MN       = PAL_M | PAL_N | PAL_Nc | Composite_NTSC,
		Composite_SECAM_DK = SECAM_D | SECAM_K | SECAM_K1,
		Composite_DK       = Composite_PAL_DK | Composite_SECAM_DK,
		Composite_SECAM    =
			SECAM_B | SECAM_G | SECAM_H | Composite_SECAM_DK | SECAM_L | SECAM_LC,
		Composite_525_60   = PAL_M | PAL_60 | Composite_NTSC | NTSC_443,
		Composite_625_50   = Composite_PAL | PAL_N | PAL_Nc | Composite_SECAM,
		Composite_All      = Composite_525_60 | Composite_625_50
	}
}
