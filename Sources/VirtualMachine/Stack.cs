using System;

namespace ArtEvolver.VirtualMachine
{
	public class Stack
	{
		private const double DefaultValue = 0.0;

		private readonly int maxIndex;

		private int index = -1;

		private double[] values;

		public int Count
		{
			get { return index + 1; }
		}

		public int Capacity
		{
			get { return values.Length; }
		}

		public Stack() : this(1000) { }

		public Stack(int capacity)
		{
			if (capacity < 1)
			{
				throw new ArgumentOutOfRangeException("capacity", "Capacity has to be greater than 1.");
			}

			values = new double[capacity];
			maxIndex = capacity - 1;
		}

		public void Push(double value)
		{
			if (index < maxIndex)
			{
				index += 1;
			}

			values[index] = value;
		}

		public double Pop()
		{
			if (index >= 0)
			{
				var value = values[index];

				index -= 1;

				return value;
			}
			else
			{
				return DefaultValue;
			}
		}

		public double Peek()
		{
			if (index >= 0)
			{
				return values[index];
			}
			else
			{
				return DefaultValue;
			}
		}

		public void Clear()
		{
			index = -1;
		}

		public override string ToString()
		{
			return string.Format("Top = {0}; Count = {1}", Peek(), Count);
		}
	}
}
