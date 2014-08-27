using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtEvolver.Rendering;

namespace ArtEvolver.VirtualMachine
{
	public static class Interpreter
	{
		private const double TruthyValue = 1;
		private const double FalsyValue  = 0;

		private static readonly double[] EmptyData = new double[] {0};

		public static double Execute(Program program, double x, double y)
		{
			if (program == null)
			{
				throw new ArgumentNullException();
			}

			if (program.StackSize < 1)
			{
				throw new ArgumentOutOfRangeException("StackSize must be greater than 1.");
			}

			var operations = program.Operations;
			var data       = program.Data.Count > 0 ? program.Data : EmptyData;

			int minDataIndex = 0;
			int maxDataIndex = data.Count - 1;

			var stack          = new Stack(program.StackSize);
			double accumulator = 0;
			int dataIndex      = 0;

			for (int i = 0; i < operations.Count; i += 1)
			{
				switch (operations[i])
				{
					case Operation.Add:
						accumulator += stack.Pop();
						break;

					case Operation.Substract:
						accumulator -= stack.Pop();
						break;

					case Operation.Multiply:
						accumulator *= stack.Pop();
						break;

					case Operation.Divide:
						accumulator /= stack.Pop();
						break;

					case Operation.Modulus:
						accumulator = MathUtility.Mod(accumulator, stack.Pop());
						break;

					case Operation.Remainder:
						accumulator %= stack.Pop();
						break;

					case Operation.AbsoluteValue:
						accumulator = Math.Abs(accumulator);
						break;

					case Operation.Negate:
						accumulator *= -1;
						break;

					case Operation.Square:
						accumulator = accumulator * accumulator;
						break;

					case Operation.Cube:
						accumulator = accumulator * accumulator * accumulator;
						break;

					case Operation.SquareRoot:
						accumulator = Math.Sqrt(accumulator);
						break;

					case Operation.Exponent:
						accumulator = Math.Pow(accumulator, stack.Pop());
						break;

					case Operation.Logarithm:
						accumulator = Math.Log(accumulator, stack.Pop());
						break;

					case Operation.Sine:
						accumulator = Math.Sin(accumulator);
						break;

					case Operation.Cosine:
						accumulator = Math.Cos(accumulator);
						break;

					case Operation.Tangent:
						accumulator = Math.Tan(accumulator);
						break;

					case Operation.ArcSine:
						accumulator = Math.Asin(accumulator);
						break;

					case Operation.ArcCosine:
						accumulator = Math.Acos(accumulator);
						break;

					case Operation.ArcTangent:
						accumulator = Math.Atan(accumulator);
						break;

					case Operation.Floor:
						accumulator = Math.Floor(accumulator);
						break;

					case Operation.Ceiling:
						accumulator = Math.Ceiling(accumulator);
						break;

					case Operation.Round:
						accumulator = Math.Round(accumulator);
						break;

					case Operation.Truncate:
						accumulator = Math.Truncate(accumulator);
						break;

					case Operation.Min:
						accumulator = Math.Min(accumulator, stack.Pop());
						break;

					case Operation.Max:
						accumulator = Math.Max(accumulator, stack.Pop());
						break;

					case Operation.Pop:
						accumulator = stack.Pop();
						break;

					case Operation.Push:
						stack.Push(accumulator);
						break;

					case Operation.ClearAccumulator:
						accumulator = 0;
						break;

					case Operation.ClearStack:
						stack.Clear();
						break;

					case Operation.Swap:
						var temp = stack.Pop();
						stack.Push(accumulator);
						accumulator = temp;
						break;

					case Operation.GetX:
						accumulator = x;
						break;

					case Operation.GetY:
						accumulator = y;
						break;

					case Operation.GetData:
						accumulator = data[dataIndex];
						break;

					case Operation.ResetIndex:
						dataIndex = minDataIndex;
						break;

					case Operation.IncrementIndex:
						dataIndex = Math.Min(maxDataIndex, dataIndex + 1);
						break;

					case Operation.DecrementIndex:
						dataIndex = Math.Max(minDataIndex, dataIndex - 1);
						break;

					case Operation.And:
						if (IsTruthy(accumulator))
						{
							accumulator = stack.Pop();
						}
						else
						{
							stack.Pop();
						}
						break;

					case Operation.Or:
						if (!IsTruthy(accumulator))
						{
							accumulator = stack.Pop();
						}
						else
						{
							stack.Pop();
						}
						break;

					case Operation.Xor:
						accumulator = IsTruthy(accumulator) ^ IsTruthy(stack.Pop()) ? TruthyValue : FalsyValue;
						break;

					case Operation.Not:
						accumulator = IsTruthy(accumulator) ? FalsyValue : TruthyValue;
						break;

					case Operation.GreaterThan:
						accumulator = accumulator > stack.Pop() ? TruthyValue : FalsyValue;
						break;

					case Operation.LessThan:
						accumulator = accumulator < stack.Pop() ? TruthyValue : FalsyValue;
						break;

					case Operation.EqualTo:
						accumulator = accumulator == stack.Pop() ? TruthyValue : FalsyValue;
						break;

					case Operation.If:
						if (!IsTruthy(accumulator))
						{
							i = Skip(program, i);
						}
						break;

					case Operation.Else:
						i = Skip(program, i);
						break;

					case Operation.EndIf:
						// Does nothing. It's just a marker.
						break;

					default:
						throw new InvalidOperationException();
				}
			}

			return accumulator;
		}

		public static void ComputeRegion(Program program, DataContainer container, Region region)
		{
			if (program == null)
			{
				throw new ArgumentNullException("program");
			}

			if (container == null)
			{
				throw new ArgumentNullException("container");
			}

			if (region == null)
			{
				throw new ArgumentNullException("region");
			}

			if (region.Width < 0)
			{
				throw new ArgumentOutOfRangeException("region.Width", "Region width must be greater than 1.");
			}

			if (region.Height < 0)
			{
				throw new ArgumentOutOfRangeException("region.Height", "Region height must be greater than 1."); 
			}

			var i = 0;

			for (var y = 0; y < container.Height; y += 1)
			{
				var yRatio = (double)y / container.Height;

				var yPosition = (yRatio * region.Height) + region.Y;

				for (var x = 0; x < container.Width; x += 1)
				{
					var xRatio = (double)x / container.Width;

					var xPosition = (xRatio * region.Width) + region.X;

					container[i] = Execute(program, xPosition, yPosition);

					i += 1;
				}
			}
		}

		private static bool IsTruthy(double value)
		{
			return value == FalsyValue ? false : true;
		}

		private static int Skip(Program program, int index)
		{
			var operations = program.Operations;

			for (index += 1; index < operations.Count; index += 1)
			{
				switch (operations[index])
				{
					case Operation.If:
						index = Skip(program, index) + 1;
						break;

					case Operation.Else:
					case Operation.EndIf:
						return index;
				}
			}

			return operations.Count - 2;
		}
	}
}
