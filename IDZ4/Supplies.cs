using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDZ4
{
	[Table(Name = "Supplies")]
	internal class Supplies
	{
		[Column(IsPrimaryKey = true, IsDbGenerated = true, Name = "Supply_Code")]
		public int SupplyID { get; set; }

		[Column(Name = "Supply_Date")]
		public DateTime S_Date { get; set; }

		[Column(Name = "Unit_Price")]
		public decimal Price { get; set; }

		[Column(Name = "Quantity")]
		public int Quantity2 { get; set; }

		[Column(Name = "Medicine_ID")]
		public int Id { get; set; }

		[Column(Name = "Supplier_ID")]
		public int SupplierID { get; set; }
	}
}
