using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtEvolver
{
	public static class MathUtility
	{
		public const double Tau = Math.PI * 2;

		public static double Normalize(double value, double min, double max)
		{
			return (value - min) / (max - min);
		}

		public static double LinearInterpolation(double value, double min, double max)
		{
			return (1 - value) * min + value * max;
		}

		public static double Map(double value, double fromMin, double fromMax, double toMin, double toMax)
		{
			var normal = Normalize(value, fromMin, fromMax);
			return LinearInterpolation(normal, toMin, toMax);
		}

		public static double Constrain(double value, double min, double max)
		{
			return Math.Min(Math.Max(value, min), max);
		}

		public static int Constrain(int value, int min, int max)
		{
			return Math.Min(Math.Max(value, min), max);
		}

		public static double Mod(double dividend, double divisor)
		{
			return dividend - (divisor * Math.Floor(dividend / divisor));
		}

		public static int Mod(int dividend, int divisor)
		{
			return dividend - (divisor * (int)Math.Floor((double)dividend / divisor));
		}

		public static double TriangularMod(double dividend, double divisor)
		{
			var period = divisor * 2;

			var mod = Mod(dividend, period);

			return mod <= divisor ? mod : period - mod;
		}

		public static int TriangularMod(int dividend, int divisor)
		{
			var period = divisor * 2;

			var mod = Mod(dividend, period);

			return mod <= divisor ? mod : period - mod;
		}
	}
}
