using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtEvolver.VirtualMachine
{
	public enum Operation : byte
	{
		Add,
		Substract,
		Multiply,
		Divide,
		Modulus,
		Remainder,

		AbsoluteValue,
		Negate,

		Square,
		Cube,
		SquareRoot,
		Exponent,
		Logarithm,

		Sine,
		Cosine,
		Tangent,
		ArcSine,
		ArcCosine,
		ArcTangent,

		Floor,
		Ceiling,
		Round,
		Truncate,

		Min,
		Max,

		Pop,
		Push,

		Clear,

		GetX,
		GetY,
		GetData,

		IncrementIndex,
		DecrementIndex,

		And,
		Or,
		Xor,
		Not,

		GreaterThan,
		LessThan,
		EqualTo,

		If,
		Else,
		EndIf,
	}
}
