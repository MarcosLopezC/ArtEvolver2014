using System;

namespace ArtEvolver
{
	public class Stack
	{
		public const int MaxSize = 1000;

		private const int MaxIndex = MaxSize - 1;

		private const double DefaultValue = 0.0;

		private int index = 0;

		private double[] values;

		public double Top
		{
			get { return Peek(); }
		}

		public int Count
		{
			get { return index; }
		}

		public Stack()
		{
			values = new double[MaxSize];
		}

		public void Push(double value)
		{
			values[index] = value;

			if (index < MaxIndex)
			{
				index += 1;
			}
		}

		public double Pop()
		{
			if (index > 0)
			{
				index -= 1;

				return values[index];
			}
			else
			{
				return DefaultValue;
			}
		}

		public double Peek()
		{
			if (index > 0)
			{
				return values[index - 1];
			}
			else
			{
				return DefaultValue;
			}
		}

		public void Clear()
		{
			index = 0;
		}

		public override string ToString()
		{
			return string.Format("Top = {0} Count = {1}", Top, Count);
		}
	}
}
