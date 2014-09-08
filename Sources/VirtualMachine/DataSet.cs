using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtEvolver.VirtualMachine
{
	public partial class DataSet
	{
		private double[] data;

		public double this[int index]
		{
			get
			{
				return data[index];
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

		private DataSet() { }

		public double GetValueAt(int x, int y)
		{
			return data[GetIndex(x, y)];
		}

		public int GetIndex(int x, int y)
		{
			return x + (Width * y);
		}
	}
}
