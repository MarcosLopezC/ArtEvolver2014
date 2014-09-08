using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtEvolver.VirtualMachine
{
	public partial class DataSet
	{
		public sealed class Builder
		{
			private Lazy<double[]> lazyData;

			public double this[int index]
			{
				get
				{
					return Data[index];
				}
				set
				{
					Data[index] = value;
				}
			}

			public int Width { get; private set; }

			public int Height { get; private set; }

			public int Length
			{
				get
				{
					return Data.Length;
				}
			}

			private double[] Data
			{
				get
				{
					return lazyData.Value;
				}
			}

			public Builder(int width, int height)
			{
				this.Width  = width;
				this.Height = height;

				lazyData = new Lazy<double[]>(() => new double[width * height]);
			}

			public DataSet Build()
			{
				var dataSet = new DataSet();

				dataSet.Width  = this.Width;
				dataSet.Height = this.Height;
				dataSet.data   = this.Data;

				// The lazy initializer is used so that if the builder is used to only create
				// one DataSet, then it will not have to copy the data array, but instead, just
				// hand it's array to the DataSet. If it's used after that, then it will lazily
				// copy the array.
				lazyData = new Lazy<double[]>(() => (double[])dataSet.data.Clone());

				return dataSet;
			}

			public double GetValueAt(int x, int y)
			{
				return Data[GetIndex(x, y)];
			}

			public int GetIndex(int x, int y)
			{
				return x + (Width * y);
			}
		}
	}
}
