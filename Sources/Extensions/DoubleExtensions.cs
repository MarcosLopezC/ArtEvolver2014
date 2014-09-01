using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtEvolver.Extensions
{
	public static class DoubleExtensions
	{
		public static bool IsNumber(this double value)
		{
			return MathUtility.IsNumber(value);
		}

		public static bool IsNaN(this double value)
		{
			return double.IsNaN(value);
		}
	}
}
