using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtEvolver.Rendering;

namespace ArtEvolver.VirtualMachine
{
	public class DataContainer
	{
		private double[] data;

		public double this[int index]
		{
			get
			{
				return data[index];
			}
			set
			{
				data[index] = value;
			}
		}

		public int Length
		{
			get
			{
				return data.Length;
			}
		}

		public int Width { get; private set; }

		public int Height { get; private set; }

		public DataContainer(int width, int height)
		{
			if (width < 0)
			{
				throw new ArgumentOutOfRangeException("width", "Width must be greater than 0.");
			}

			if (height < 0)
			{
				throw new ArgumentOutOfRangeException("height", "Height must be greater than 0.");
			}

			this.Width  = width;
			this.Height = height;

			data = new double[Width * Height];
		}

		public double GetValueAt(int x, int y)
		{
			return data[GetIndex(x, y)];
		}

		public void SetValueAt(int x, int y, double value)
		{
			data[GetIndex(x, y)] = value;
		}

		private int GetIndex(int x, int y)
		{
			return x + (Width * y);
		}
	}
}
