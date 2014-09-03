using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtEvolver.Extensions;

namespace ArtEvolver.Rendering.Generators
{
	public class UniformGenerator : IColorGenerator
	{
		public RgbColor UndefinedColor { get; set; }

		public double HueScale { get; set; }

		public double HueOffset { get; set; }

		public double SaturationScale { get; set; }

		public double SaturationOffset { get; set; }

		public double BrightnessScale { get; set; }

		public double BrightnessOffset { get; set; }

		public UniformGenerator()
		{
			HueScale        = 1;
			SaturationScale = 1;
			BrightnessScale = 1;
		}

		public RgbColor Generate(double value)
		{
			if (value.IsNumber())
			{
				return RgbColor.FromHsb(
					MathUtility.Mod(HueOffset + HueScale * value, RgbColor.MaxHue),
					MathUtility.TriangularMod(SaturationOffset + SaturationScale * value, RgbColor.MaxSaturation),
					MathUtility.TriangularMod(BrightnessOffset + BrightnessScale * value, RgbColor.MaxBrightness)
				);
			}
			else
			{
				return UndefinedColor;
			}
		}
	}
}
