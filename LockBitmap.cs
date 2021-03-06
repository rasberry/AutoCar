﻿// Source: http://www.codeproject.com/Tips/240428/Work-with-bitmap-faster-with-Csharp
// License: http://www.codeproject.com/info/cpol10.aspx

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace AutoCar
{
	public class LockBitmap
	{
		Bitmap source = null;
		IntPtr Iptr = IntPtr.Zero;
		BitmapData bitmapData = null;

		public byte[] Pixels { get; set; }
		public int Depth { get; private set; }
		public int Width { get; private set; }
		public int Height { get; private set; }
		public Bitmap Source { get { return source; }}
	 
		public LockBitmap(Bitmap source)
		{
			this.source = source;
		}
	 
		/// <summary>
		/// Lock bitmap data
		/// </summary>
		public void LockBits()
		{
			// Get width and height of bitmap
			Width = source.Width;
			Height = source.Height;
 
			// get total locked pixels count
			int PixelCount = Width * Height;
 
			// Create rectangle to lock
			Rectangle rect = new Rectangle(0, 0, Width, Height);
 
			// get source bitmap pixel format size
			Depth = System.Drawing.Bitmap.GetPixelFormatSize(source.PixelFormat);
 
			// Check if bpp (Bits Per Pixel) is 8, 24, or 32
			if (Depth != 8 && Depth != 24 && Depth != 32)
			{
				throw new ArgumentException("Only 8, 24 and 32 bpp images are supported.");
			}
 
			// Lock bitmap and return bitmap data
			bitmapData = source.LockBits(rect, ImageLockMode.ReadWrite,source.PixelFormat);
 
			// create byte array to copy pixel values
			int step = Depth / 8;
			Pixels = new byte[PixelCount * step];
			Iptr = bitmapData.Scan0;
 
			// Copy data from pointer to array
			Marshal.Copy(Iptr, Pixels, 0, Pixels.Length);
		}
	 
		/// <summary>
		/// Unlock bitmap data
		/// </summary>
		public void UnlockBits()
		{
			// Copy data from byte array to pointer
			Marshal.Copy(Pixels, 0, Iptr, Pixels.Length);
 
			// Unlock bitmap data
			source.UnlockBits(bitmapData);
		}
	 
		/// <summary>
		/// Get the color of the specified pixel
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public Color GetPixel(int x, int y)
		{
			Color clr = Color.Empty;
	 
			// Get color components count
			int cCount = Depth / 8;
	 
			// Get start index of the specified pixel
			int i = ((y * Width) + x) * cCount;
	 
			if (i > Pixels.Length - cCount)
				throw new IndexOutOfRangeException();
	 
			if (Depth == 32) // For 32 bpp get Red, Green, Blue and Alpha
			{
				byte b = Pixels[i];
				byte g = Pixels[i + 1];
				byte r = Pixels[i + 2];
				byte a = Pixels[i + 3]; // a
				clr = Color.FromArgb(a, r, g, b);
			}
			if (Depth == 24) // For 24 bpp get Red, Green and Blue
			{
				byte b = Pixels[i];
				byte g = Pixels[i + 1];
				byte r = Pixels[i + 2];
				clr = Color.FromArgb(r, g, b);
			}
			if (Depth == 8)
			// For 8 bpp get color value (Red, Green and Blue values are the same)
			{
				byte c = Pixels[i];
				clr = Color.FromArgb(c, c, c);
			}
			return clr;
		}
	 
		/// <summary>
		/// Set the color of the specified pixel
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="color"></param>
		public void SetPixel(int x, int y, Color color)
		{
			SetPixel(x,y,color.ToArgb()); //seems to be bgra?
		}

		public void SetPixel(int x, int y, int value) //value should be rgba
		{
			// Get color components count
			int cCount = Depth / 8;
	 
			// Get start index of the specified pixel
			int i = ((y * Width) + x) * cCount;

			if (Depth == 32) // For 32 bpp set Red, Green, Blue and Alpha
			{
				byte[] c = BitConverter.GetBytes(value);
				Buffer.BlockCopy(c,0,Pixels,0,4);
			}
			if (Depth == 24) // For 24 bpp set Red, Green and Blue
			{
				byte[] c = BitConverter.GetBytes(value);
				Buffer.BlockCopy(c,0,Pixels,0,3);
			}
			if (Depth == 16) //For grayscale 16bpp
			{
				byte[] c = BitConverter.GetBytes(value);
				Buffer.BlockCopy(c,0,Pixels,0,2);
			}
			if (Depth == 8)
			{
				Pixels[i] = (byte)value;
			}
		}
	}

}
