using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtEvolver.Rendering
{
	public struct HsbColor
	{
		public const double MaxHue        = 6;
		public const double MaxSaturation = 1;
		public const double MaxBrightness = 1;

		public double Hue        { get; set; }
		public double Saturation { get; set; }
		public double Brightness { get; set; }

		public HsbColor(double hue, double saturation, double brightness) : this()
		{
			this.Hue        = hue;
			this.Saturation = saturation;
			this.Brightness = brightness;
		}

		public RgbColor ToRgbColor()
		{
			var hue        = MathUtility.Constrain(this.Hue, 0, MaxHue);
			var saturation = MathUtility.Constrain(this.Saturation, 0, MaxSaturation);
			var brightness = MathUtility.Constrain(this.Brightness, 0, MaxBrightness);

			return RgbColor.FromHsb(hue, saturation, brightness);
		}

		public override string ToString()
		{
			return string.Format("HSB({0}, {1}, {2})", Hue, Saturation, Brightness);
		}
	}
}
