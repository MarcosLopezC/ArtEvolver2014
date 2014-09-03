using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtEvolver.Rendering.Generators
{
	public interface IColorGenerator
	{
		RgbColor Generate(double value);
	}
}
