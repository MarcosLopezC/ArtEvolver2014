using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtEvolver.VirtualMachine
{
	public class Program
	{
		public IList<Operation> Operations { get; private set; }

		public IList<double> Data { get; private set; }

		public int StackSize { get; set; }

		public Program()
		{
			Operations = new List<Operation>(1000);
			Data       = new List<double>(1000);
			StackSize  = 1000;
		}

		public string SerializeOperations()
		{
			var output = new StringBuilder();

			for (var i = 0; i < Operations.Count; i += 1)
			{
				output.AppendFormat("{0};\n", Enum.GetName(typeof(Operation), Operations[i]));
			}

			return output.ToString();
		}

		public string SerializeData()
		{
			var output = new StringBuilder();

			for (var i = 0; i < Data.Count; i += 1)
			{
				output.AppendFormat("{0};\n", Data[i]);
			}

			return output.ToString();
		}

		public void ParseOperations(string input)
		{
			var operationNames = input.Split(';');

			foreach (var operationName in operationNames)
			{
				if (string.IsNullOrWhiteSpace(operationName))
				{
					continue;
				}

				Operation operationValue;

				if (Enum.TryParse<Operation>(operationName, out operationValue))
				{
					this.Operations.Add(operationValue);
				}
				else
				{
					throw new Exception("Invalid operation.");
				}
			}
		}

		public void ParseData(string input)
		{
			var dataSet = input.Split(';');

			foreach (var valueString in dataSet)
			{
				if (string.IsNullOrWhiteSpace(valueString))
				{
					continue;
				}

				double value;

				if (double.TryParse(valueString, out value))
				{
					this.Data.Add(value);
				}
				else
				{
					throw new Exception("Invalid data value.");
				}
			}
		}
	}
}
