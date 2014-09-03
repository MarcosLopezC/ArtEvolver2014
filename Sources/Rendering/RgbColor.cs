using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtEvolver.Extensions;

namespace ArtEvolver.Rendering
{
	public struct RgbColor
	{
		public const byte MaxValue = byte.MaxValue;

		private static readonly RgbColor White = new RgbColor(MaxValue, MaxValue, MaxValue);

		public byte Red   { get; set; }
		public byte Green { get; set; }
		public byte Blue  { get; set; }

		public RgbColor(byte red, byte green, byte blue) : this()
		{
			this.Red   = red;
			this.Green = green;
			this.Blue  = blue;
		}

		private RgbColor(double red, double green, double blue) : this()
		{
			this.Red   = (byte)(red   * MaxValue);
			this.Green = (byte)(green * MaxValue);
			this.Blue  = (byte)(blue  * MaxValue);
		}

		public HsbColor ToHsbColor()
		{
			var Hsb = new HsbColor();

			var red   = (double)(Red)   / MaxValue;
			var green = (double)(Green) / MaxValue;
			var blue  = (double)(Blue)  / MaxValue;

			var max = Max(red, green, blue);
			var min = Min(red, green, blue);

			var delta = max - min;

			// Set hue.
			if (max == red)
			{
				Hsb.Hue =  MathUtility.Mod((green - blue) / delta, 6);
			}
			else if (max == green)
			{
				Hsb.Hue = ((blue - red) / delta) + 2;
			}
			else // if (max == blue)
			{
				Hsb.Hue = ((red - green) / delta) + 4;
			}

			// Set saturation.
			if (delta == 0)
			{
				Hsb.Saturation = 0;
			}
			else
			{
				Hsb.Saturation = delta / max;
			}

			// Set brightness.
			Hsb.Brightness = max;

			return Hsb;
		}

		public static RgbColor FromHue(double hue)
		{
			if (hue.IsNaN())
			{
				return White;
			}

			if (hue < 0 || hue >= HsbColor.MaxHue)
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
					return new RgbColor(High, rising, Low);

				case 1:
					return new RgbColor(falling, High, Low);

				case 2:
					return new RgbColor(Low, High, rising);

				case 3:
					return new RgbColor(Low, falling, High);

				case 4:
					return new RgbColor(rising, Low, High);

				case 5:
					return new RgbColor(High, Low, falling);

				default:
					throw new IndexOutOfRangeException();
			}
		}

		public static RgbColor FromHsb(double hue, double saturation, double brightness)
		{
			if (saturation < 0 || saturation > HsbColor.MaxSaturation)
			{
				throw new ArgumentOutOfRangeException("saturation", saturation, "Saturation must be between 0 and 1.");
			}

			if (brightness < 0 || brightness > HsbColor.MaxBrightness)
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

		private static double Min(double a, double b, double c)
		{
			return Math.Min(a, Math.Min(b, c));
		}

		private static double Max(double a, double b, double c)
		{
			return Math.Max(a, Math.Max(b, c));
		}

		public override string ToString()
		{
			return string.Format("RGB({0}, {1}, {2})", Red, Green, Blue);
		}
	}
}
