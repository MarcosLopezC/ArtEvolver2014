using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtEvolver.Extensions;

namespace ArtEvolver.Rendering
{
	public struct Color
	{
		private const byte MaxValue = byte.MaxValue;

		private static readonly Color White = new Color(MaxValue, MaxValue, MaxValue);

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
			this.Red   = (byte)(red   * MaxValue);
			this.Green = (byte)(green * MaxValue);
			this.Blue  = (byte)(blue  * MaxValue);
		}

		public static Color FromHue(double hue)
		{
			if (hue.IsNaN())
			{
				return White;
			}

			if (hue < 0 || hue >= 6)
			{
				throw new ArgumentOutOfRangeException("hue", hue, "Hue must be between 0 and 6.");
			}

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
			if (saturation < 0 || saturation > 1)
			{
				throw new ArgumentOutOfRangeException("saturation", saturation, "Saturation must be between 0 and 1.");
			}

			if (brightness < 0 || brightness > 1)
			{
				throw new ArgumentOutOfRangeException("brightness", brightness, "Brightness must be between 0 and 1.");
			}

			var complement = 1 - saturation;

			// Get pure hue.
			var color = FromHue(hue);

			// Apply saturation.
			color.Red   += (byte)(complement * (MaxValue - color.Red));
			color.Green += (byte)(complement * (MaxValue - color.Green));
			color.Blue  += (byte)(complement * (MaxValue - color.Blue));

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
