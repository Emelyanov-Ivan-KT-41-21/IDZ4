using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDZ4
{
	[Table(Name="Medicines")]
	internal class Medicines
	{
		[Column(IsPrimaryKey = true, IsDbGenerated = true, Name = "Medicine_ID")]
		public int Id { get; set; }

		[Column(Name = "Medicine_Name")]
		public string Name { get; set; }

		[Column(Name = "Indications")]
		public string Indications { get; set; }

		[Column(Name = "Unit")]
		public string Unit { get; set; }

		[Column(Name = "Package_Quantity")]
		public int Quantity { get; set; }

		[Column(Name = "Manufacturer_Name")]
		public string Manufacturer { get; set; }

	}
}
