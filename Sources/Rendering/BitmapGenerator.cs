using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using ArtEvolver.VirtualMachine;
using ArtEvolver.Rendering.Generators;

namespace ArtEvolver.Rendering
{
	public static class BitmapGenerator
	{
		public static Bitmap Generate(DataContainer data, IColorGenerator generator)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}

			if (generator == null)
			{
				throw new ArgumentNullException("generator");
			}

			var bitmap = new Bitmap(data.Width, data.Height, PixelFormat.Format24bppRgb);

			var lockedData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
				ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

			var managedBytes = new byte[Math.Abs(lockedData.Stride) * lockedData.Height];

			// Copy locked data into managed array.
			Marshal.Copy(lockedData.Scan0, managedBytes, 0, managedBytes.Length);

			var dataIndex = 0;

			for (var i = 0; i < managedBytes.Length; i += 3)
			{
				var color = generator.Generate(data[dataIndex]);

				managedBytes[i + 0] = color.Red;
				managedBytes[i + 1] = color.Green;
				managedBytes[i + 2] = color.Blue;

				dataIndex += 1;
			}

			// Copy managed array back into locked data.
			Marshal.Copy(managedBytes, 0, lockedData.Scan0, managedBytes.Length);

			bitmap.UnlockBits(lockedData);

			return bitmap;
		}
	}
}
