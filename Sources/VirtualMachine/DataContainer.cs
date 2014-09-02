using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtEvolver.Rendering;
using ArtEvolver.Extensions;

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
				throw new ArgumentOutOfRangeException("width", width, "Width must be greater than 0.");
			}

			if (height < 0)
			{
				throw new ArgumentOutOfRangeException("height", height, "Height must be greater than 0.");
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

		public double GetMax()
		{
			var max = data[0];

			for (var i = 1; i < data.Length; i += 1)
			{
				double value = data[i];

				if (value.IsNumber() && value > max)
				{
					max = value;
				}
			}

			return max;
		}

		public double GetMin()
		{
			var min = data[0];

			for (var i = 1; i < data.Length; i += 1)
			{
				double value = data[i];

				if (value.IsNumber() && value < min)
				{
					min = value;
				}
			}

			return min;
		}

		public double GetAverage()
		{
			return GetAverage(0, data.Length);
		}

		private double GetAverage(int head, int tail)
		{
			double sum = 0;
			int count  = 0;

			var range = tail - head;

			if (range > 2)
			{
				var mid = (head + tail) / 2;

				var leftAverage  = GetAverage(head, mid);
				var rightAverage = GetAverage(mid + 1, tail);

				if (leftAverage.IsNumber())
				{
					sum   += leftAverage;
					count += 1;
				}

				if (rightAverage.IsNumber())
				{
					sum   += rightAverage;
					count += 1;
				}
			}
			else
			{
				for (var i = head; i < tail; i += 1)
				{
					var value = data[i];

					if (value.IsNumber())
					{
						sum   += value;
						count += 1;
					}
				}
			}

			if (count == 0)
			{
				return double.NaN;
			}
			else
			{
				return sum / count;
			}
		}

		public int GetNumberCount()
		{
			var count = 0;

			for (var i = 0; i < data.Length; i += 1)
			{
				if (MathUtility.IsNumber(data[i]))
				{
					count += 1;
				}
			}

			return count;
		}

		private int GetIndex(int x, int y)
		{
			return x + (Width * y);
		}
	}
}
