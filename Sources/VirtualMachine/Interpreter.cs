using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtEvolver.VirtualMachine
{
	public static class Interpreter
	{
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

			var stack          = new Stack(program.StackSize);
			double accumulator = 0;
			int dataIndex      = 0;

			for (int i = 0; i < program.Operations.Count; i += 1)
			{
				switch (program.Operations[i])
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
						accumulator %= stack.Pop();
						break;

					case Operation.Remainder:
						accumulator = Math.IEEERemainder(accumulator, stack.Pop());
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

					case Operation.Clear:
						accumulator = 0;
						break;

					case Operation.GetX:
						accumulator = x;
						break;

					case Operation.GetY:
						accumulator = y;
						break;

					case Operation.GetData:
						accumulator = program.Data[dataIndex];
						break;

					case Operation.GetPi:
						accumulator = Math.PI;
						break;

					case Operation.GetE:
						accumulator = Math.E;
						break;

					case Operation.Next:
						dataIndex = Math.Min(program.Data.Count, dataIndex + 1);
						break;

					case Operation.Previous:
						dataIndex = Math.Max(0, dataIndex - 1);
						break;

					default:
						throw new InvalidOperationException();
				}
			}

			return accumulator;
		}
	}
}
