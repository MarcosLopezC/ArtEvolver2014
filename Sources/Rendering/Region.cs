using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtEvolver.Rendering
{
	public struct Region
	{
		public double X { get; set; }
		public double Y { get; set; }

		public double Width { get; set; }
		public double Height { get; set; }

		public bool IsEmpty
		{
			get
			{
				return Width == 0 && Height == 0;
			}
		}

		public Region(double width, double height) : this()
		{
			this.Width  = width;
			this.Height = height;
		}

		public Region(double width, double height, double x, double y) : this(width, height)
		{
			this.X = x;
			this.Y = y;
		}
	}
}
