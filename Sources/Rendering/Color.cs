using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtEvolver.Rendering
{
	public struct Color
	{
		private const byte Max = byte.MaxValue;

		public byte Red   { get; set; }
		public byte Green { get; set; }
		public byte Blue  { get; set; }

		public Color(byte red, byte green, byte blue) : this()
		{
			this.Red   = red;
			this.Green = green;
			this.Blue  = blue;
		}

		public Color(double red, double green, double blue) : this()
		{
			this.Red   = (byte)(red   * Max);
			this.Green = (byte)(green * Max);
			this.Blue  = (byte)(blue  * Max);
		}

		public static Color FromHue(double hue)
		{
			// Mapping hue's range from [0, 1) to [0, 6), for convenience. 
			hue = MathUtility.Mod(hue, 1) * 6;

			var sector = (int)(hue);

			const double High = 1;
			const double Low  = 0;

			var rising  = hue - sector;
			var falling = 1 - rising;

			switch (sector)
			{
				case 0:
					return new Color(High, rising, Low);

				case 1:
					return new Color(falling, High, Low);

				case 2:
					return new Color(Low, High, rising);

				case 3:
					return new Color(Low, falling, High);

				case 4:
					return new Color(rising, Low, High);

				case 5:
					return new Color(High, Low, falling);

				default:
					throw new IndexOutOfRangeException();
			}
		}

		public static Color FromHsb(double hue, double saturation, double brightness)
		{
			var complement = 1 - saturation;

			// Get pure hue.
			var color = FromHue(hue);

			// Apply saturation.
			color.Red   += (byte)(complement * (Max - color.Red));
			color.Green += (byte)(complement * (Max - color.Green));
			color.Blue  += (byte)(complement * (Max - color.Blue));

			// Apply brightness.
			color.Red   = (byte)(color.Red   * brightness);
			color.Green = (byte)(color.Green * brightness);
			color.Blue  = (byte)(color.Blue  * brightness);

			return color;
		}

		public override string ToString()
		{
			return string.Format("RGB({0}, {1}, {2})", Red, Green, Blue);
		}
	}
}
