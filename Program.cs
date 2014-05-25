using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using Video4Linux.Analog;
using Video4Linux.Analog.Video;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace CamTest2
{
	class Program
	{
		static void Main(string[] args)
		{
			ParseArgs(args);
			var adapter = new Adapter(Device);
			Pic(adapter);
		}

		static string Device = null;
		static string OutputFile = null;
		static int FrameCount = 0;
		static void ParseArgs(string[] args)
		{
			int len = args.Length;
			for(int i=0; i<len; i++)
			{
				string c = args[i];
				if (c == "-d" && len>i) { Device = args[++i]; }
				else if (c == "-c" && len>i) { FrameCount = int.Parse(args[++i]); }
				else if (OutputFile == null) { OutputFile = c; }
			}
			if (Device == null) { Device = "/dev/video0"; }
			if (OutputFile == null) { OutputFile = "test.bin"; }
		}

		static void Pic(Adapter adapter)
		{
			VideoCaptureFormat format = new VideoCaptureFormat();
			adapter.GetFormat(format);
			Stopwatch timer = new Stopwatch();;
			timer.Start();
			int count = (int)(format.Width*format.Height*2);
			byte[] buffer = new byte[count];

			WL("Method "+adapter.CaptureMethod+" "+format.Width+" "+format.Height+" "+format.PixelFormat+" "+format.Field+" "+format.BytesPerLine);
	
			adapter.StartStreaming();
			for(int f=0; f<FrameCount; f++)
			{
				int r = adapter.VideoStream.Read(buffer,0,count);
				WL("Read "+r+" bytes");
				Bitmap bmp = RawToBitmap(format,buffer);
				WL("frame "+f+" "+timer.ElapsedMilliseconds);
			}
			adapter.StopStreaming();
			timer.Stop();
		}

		static Bitmap RawToBitmap(VideoCaptureFormat format, byte[] buffer)
		{
			int w = (int)format.Width, h = (int)format.Height;
			Color[] argb = YUV422toARGB8888(buffer, w, h);
			WL("argb.Length = "+argb.Length+" buff = "+buffer.Length);
			Bitmap bmp = new Bitmap(w, h, PixelFormat.Format32bppArgb);
			LockBitmap lbmp = new LockBitmap(bmp);
			lbmp.LockBits();
			for (int hi = 0; hi < h; hi++)
			{
				for (int wi = 0; wi < w; wi++)
				{
					Color c = argb[hi * w + wi];
					lbmp.SetPixel(wi, hi, c);
				}
			}
			lbmp.UnlockBits();
			string file = OutputFile + DateTime.Now.Ticks + ".png";
			bmp.Save(file, ImageFormat.Png);
			//File.WriteAllBytes(OutputFile,ConvertToRGB(buffer));
			return bmp;
		}

		static void Info(Adapter adapter)
		{
			WL("= Capabilities");
			foreach(AdapterCapability cap in adapter.Capabilities)
			{
				WL(cap.ToString());
			}
			WL("= Driver");
			WL(adapter.Driver);

			WL("= BusInfo");
			WL(adapter.BusInfo);

			//VideoCaptureFormat format = new VideoCaptureFormat();
			//adapter.GetFormat(format);
			//WL("= vc "+format.Width+" "+format.Height+" "+format.PixelFormat);
			//adapter.SetFormat(format);

			WL("=Inputs");
			foreach(Input inp in adapter.Inputs)
			{
				WL(inp.Name+" "+inp.Status+" "+inp.Type+" "+inp.SupportedStandards);
			}

			WL("=Outputs");
			foreach(Output outp in adapter.Outputs)
			{
				WL(outp.Name+" "+outp.Status+" "+outp.Type+" "+outp.SupportedStandards);
			}

			WL("=Standard");
			foreach(Standard std in adapter.Standards)
			{
				WL(std.Name+" "+std.FrameLines);
			}
		}

		static void WL(string message)
		{
			Console.WriteLine(message);
		}

		// Converts YUV420 NV21 to ARGB8888
		// 
		// @param data byte array on YUV420 NV21 format.
		// @param width pixels width
		// @param height pixels height
		// @return a ARGB8888 pixels int array. Where each int is a pixels ARGB. 
		//
		static Color[] YUV420toARGB8888(byte [] data, int width, int height)
		{
			int size = width*height;
			int offset = size;
			Color[] pixels = new Color[size];
			int u, v, y1, y2, y3, y4;
 
			// i along Y and the final pixels
			// k along pixels U and V
			for(int i=0, k=0; i < size; i+=2, k+=2) {
				y1 = data[i  ]&0xff;
				y2 = data[i+1]&0xff;
				y3 = data[width+i  ]&0xff;
				y4 = data[width+i+1]&0xff;
	
				v = data[offset+k  ]&0xff;
				u = data[offset+k+1]&0xff;
				//v = v-128;
				//u = u-128;
	
				pixels[i  ] = convertYUVtoARGB(y1, u, v);
				pixels[i+1] = convertYUVtoARGB(y2, u, v);
				pixels[width+i  ] = convertYUVtoARGB(y3, u, v);
				pixels[width+i+1] = convertYUVtoARGB(y4, u, v);
	
				if (i!=0 && (i+2)%width==0)
					i += width;
			}

			return pixels;
		}

		//YUYV (YUY2) format
		//Y0 U0 Y1 V0 Y2 U2 Y3 V2
		static Color[] YUV422toARGB8888(byte[] data,int width,int height)
		{
			int size = width*height;
			Color[] pixels = new Color[size];
			int u,v,y1,y2;
			for(int i=0, k=0; k<size; i+=4, k+=2) {
				y1 = data[i  ] & 0xff;
				u  = data[i+1] & 0xff;
				y2 = data[i+2] & 0xff;
				v  = data[i+3] & 0xff;

				pixels[k  ] = convertYUVtoARGB(y1,u,v);
				pixels[k+1] = convertYUVtoARGB(y2,u,v);
			}
			return pixels;
		}
 
		//static Color convertYUVtoARGB(int y, int u, int v)
		//{
		//	int r = y + (int)(1.772f*v);
		//	int g = y - (int)(0.344f*v + 0.714f*u);
		//	int b = y + (int)(1.402f*u);
		//	r = r>255? 255 : r<0 ? 0 : r;
		//	g = g>255? 255 : g<0 ? 0 : g;
		//	b = b>255? 255 : b<0 ? 0 : b;
		//	return Color.FromArgb(r,g,b);
		//}

		static Color convertYUVtoARGB(int y, int u, int v)
		{
			int C = y - 16;
			int D = u - 128;
			int E = v - 128;
			int R = clip(( 298 * C           + 409 * E + 128) >> 8);
			int G = clip(( 298 * C - 100 * D - 208 * E + 128) >> 8);
			int B = clip(( 298 * C + 516 * D           + 128) >> 8);
			return Color.FromArgb(R,G,B);
		}
		static int clip(int n) {
			return n < 0 ? 0 : n > 255 ? 255 : n;
		}
	}
}
