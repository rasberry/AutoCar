using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoCar
{
	public static class StereoProcess
	{
		public static Func<Bitmap> Go(uint w,uint h, byte[] one, byte[] two)
		{
			GCHandle gchOne = default(GCHandle), gchTwo = default(GCHandle);
			try {
				gchOne = GCHandle.Alloc(one,GCHandleType.Pinned);
				var ptrOne = gchOne.AddrOfPinnedObject();
				gchTwo = GCHandle.Alloc(two,GCHandleType.Pinned);
				var ptrTwo = gchTwo.AddrOfPinnedObject();
			
				var l_image = new Image<Gray,short>((int)w,(int)h,1,ptrOne)
					.Resize(0.25,Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
				var r_image = new Image<Gray,short>((int)w,(int)h,1,ptrTwo)
					.Resize(0.25,Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);

				var disparity = new Image<Gray, short>(l_image.Size);
				using (StereoSGBM stereoSolver = new StereoSGBM(
					 Config.MinDisparity
					,Config.NumDisparities
					,Config.SADWindowSize
					,Config.P1
					,Config.P2
					,Config.Disp12MaxDiff
					,Config.PreFilterCap
					,Config.UniquenessRatio
					,Config.SpeckleWindowSize
					,Config.SpeckleRange
					,StereoSGBM.Mode.SGBM
				)) {
					stereoSolver.FindStereoCorrespondence(
						l_image.Convert<Gray, byte>()
						,r_image.Convert<Gray, byte>()
						,disparity
					);
				}
				//make it lazy so that it don't have to copy it twice
				return () => disparity.Bitmap;
			}
			finally {
				if (gchOne.IsAllocated) { gchOne.Free(); }
				if (gchTwo.IsAllocated) { gchTwo.Free(); }
			}
		}
	}
}
