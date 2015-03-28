using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Video4Linux.Analog;
using Video4Linux.Analog.Video;

namespace AutoCar
{
	class VideoGrabber
	{
		bool _enabled = true;
		public void Stop()
		{
			_enabled = false;
		}

		public VideoGrabber(string deviceone,string devicetwo)
		{
			_deviceone = deviceone;
			_devicetwo = devicetwo;
		}

		string _deviceone;
		string _devicetwo;
		int _w;
		int _h;

		//private byte[] lastImage;
		private Func<Bitmap> lastImage;
		private byte[] bufferone;
		private byte[] buffertwo;
		private object lastlock = new object();

		public void Start()
		{
			var adapterone = new Adapter(_deviceone);
			var adaptertwo = new Adapter(_devicetwo);
			VideoCaptureFormat fone, ftwo;
			
			bufferone = Init(adapterone,out fone);
			buffertwo = Init(adaptertwo,out ftwo);
			_w = (int)fone.Width;
			_h = (int)fone.Height;
			//lastImage = new byte[_w * _h * 2];

			adapterone.StartStreaming();
			adaptertwo.StartStreaming();

			Stopwatch sw = new Stopwatch();
			sw.Start();
			//WL("started "+(Console.KeyAvailable?"haskey":"nokey"));

			try {
				while(_enabled)
				{
					int rone = adapterone.VideoStream.Read(bufferone,0,bufferone.Length);
					int rtwo = adaptertwo.VideoStream.Read(buffertwo,0,buffertwo.Length);
					//WL("Read "+rone+","+rtwo+" bytes");
					long stamp = sw.ElapsedTicks;
					
					//RawToBitmap("_1",fone,bufferone,false);
					//RawToBitmap("_2",ftwo,buffertwo,false);
					
					//RawToPGM("C1",stamp,fone,bufferone,true);
					//RawToPGM("C2",stamp,ftwo,buffertwo,true);
					//WL("ellapsed "+sw.ElapsedMilliseconds);
					
					lock(lastlock) {
						//DiffImage(fone.Width,fone.Height,bufferone,buffertwo,lastImage);
						lastImage = StereoProcess.Go(fone.Width,fone.Height,bufferone,buffertwo);
					}
					//RawToPGM("D",stamp,fone,bufferdif,true);
					//WL("ellapsed "+sw.ElapsedMilliseconds);
				}
			} finally {
				adapterone.StopStreaming();
				adaptertwo.StopStreaming();
			}
		}

		public byte[] GetImage()
		{
			//byte[] copy = null;
			//lock(lastlock) {
			//	//copy = new byte[lastImage.Length];
			//	//System.Buffer.BlockCopy(lastImage,0,copy,0,lastImage.Length);
			//	copy = RawGreyToImage(_w,_h,lastImage);
			//	//Console.WriteLine(BitConverter.ToString(copy));
			//}
			//return copy;
			lock(lastlock) {
				Bitmap b = lastImage.Invoke(); //create the copy now
				MemoryStream m = new MemoryStream();
				b.Save(m,ImageFormat.Jpeg);
				return m.ToArray();
			}
		}

		public byte[] GetLeftImage()
		{
			byte[] copy = RawGreyToImage(_w,_h,bufferone);
			return copy;
		}
		public byte[] GetRightImage()
		{
			byte[] copy = RawGreyToImage(_w,_h,buffertwo);
			return copy;
		}

		static byte[] Init(Adapter adapter, out VideoCaptureFormat format)
		{
			format = new VideoCaptureFormat() {
				Field = Video4Linux.Analog.Kernel.v4l2_field.Any
				,Width = 358,Height = 288
				,PixelFormat = Video4Linux.Analog.Kernel.v4l2_pix_format_id.GREY
			};
			adapter.SetFormat(format);
			//adapter.GetFormat(format);
			int count = (int)(format.Width*format.Height*2);
			byte[] buffer = new byte[count];
			//WL("Method "+adapter.CaptureMethod+" "+format.Width+" "+format.Height+" "+format.PixelFormat+" "+format.Field+" "+format.BytesPerLine+" "+count);
			return buffer;
		}

		static void Read(Adapter adapter,byte[] buffer)
		{
			int r = adapter.VideoStream.Read(buffer,0,buffer.Length);
			//WL("Read "+r+" bytes");
		}

		const int window = 2;
		static void DiffImage(uint w,uint h, byte[] one, byte[] two, byte[] diff)
		{
			int len = Math.Min(one.Length,two.Length);
			uint wxsize = w/window;
			uint wysize = h/window;

			for(uint wx=0; wx<wxsize; wx++)
			{
				for(uint wy=0; wy<wysize; wy++)
				{
					double sum = 0;
					for(uint vx=0; vx<window; vx++)
					{
						for(uint vy=0; vy<window; vy++)
						{
							uint x = wx*window+vx;
							uint y = wy*window+vy;
							uint b = y*w + x;
							int vone = (one[b+1] + (one[b]<<8));
							int vtwo = (two[b+1] + (two[b]<<8));
							sum += vone > vtwo ? vone - vtwo : vtwo - vone;
						}
					}
					short avg = (short)(sum / window * window);
					for(uint vx=0; vx<window; vx++)
					{
						for(uint vy=0; vy<window; vy++)
						{
							uint x = wx*window+vx;
							uint y = wy*window+vy;
							uint b = y*w + x;
							diff[b*2+1] = (byte)(avg & 255);
							diff[b*2] = (byte)(avg >> 8);
						}
					}
				}
			}
		}

		static void DiffImageOne(uint w, uint h, byte[] one, byte[] two, byte[] diff)
		{
			int len = Math.Min(one.Length,two.Length);
			for(int b=0; b<len; b+=2) {
				int n1 = (one[b+1] + (one[b]<<8));
				int n2 = (two[b+1] + (two[b]<<8));
				int d = Math.Abs(n2 - n1);
				diff[b+1] = (byte)(d & 255);
				diff[b] = (byte)(d >> 8);
			}
		}

		static byte[] RawGreyToImage(int w,int h,byte[] buffer)
		{
			Bitmap bmp = new Bitmap(w, h, PixelFormat.Format32bppArgb);
			LockBitmap lbmp = new LockBitmap(bmp);
			lbmp.LockBits();
			for(int b=0; b<buffer.Length;b+=2) {
				int pi = b*2; //for every 2 gray bytes we write 4 argb bytes
				byte c = (byte)BitConverter.ToInt16(buffer,b);
				lbmp.Pixels[pi] = c;
				lbmp.Pixels[pi+1] = c;
				lbmp.Pixels[pi+2] = c;
				lbmp.Pixels[pi+3] = 255; //a
			}
			lbmp.UnlockBits();

			MemoryStream s = new MemoryStream();
			bmp.Save(s,ImageFormat.Jpeg);
			return s.ToArray();
		}

		static void RawToPGM(string extra,long stamp, VideoCaptureFormat format, byte[] buffer, bool save)
		{
			if (!save) { return; }
			int w = (int)format.Width, h = (int)format.Height;
			string name = stamp.ToString("000000000000") + extra + ".pgm";
			FileStream fs = File.Open(name,FileMode.CreateNew,FileAccess.Write,FileShare.Read);
			string header = "P5 "+w+" "+h+" 65535 ";
			byte[] bh = Encoding.ASCII.GetBytes(header);
			fs.Write(bh,0,bh.Length);
			fs.Write(buffer,0,buffer.Length);
			fs.Close();
		}

		static Bitmap ColorToBitmap(int w,int h, Color[] argb)
		{
			//WL("argb.Length = "+argb.Length+" buff = "+buffer.Length);
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
			return bmp;
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
