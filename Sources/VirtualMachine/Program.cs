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
	}
}
