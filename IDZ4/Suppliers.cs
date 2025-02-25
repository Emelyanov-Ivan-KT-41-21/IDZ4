using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDZ4
{
	[Table(Name = "Suppliers")]
	internal class Suppliers
	{
		[Column(IsPrimaryKey = true, IsDbGenerated = true, Name = "Supplier_ID")]
		public int SupplierID { get; set; }

		[Column(Name = "Short_Name")]
		public string S_Name { get; set; }

		[Column(Name = "Full_Name")]
		public string F_Name { get; set; }

		[Column(Name = "Legal_Address")]
		public string Address { get; set; }

		[Column(Name = "Phone")]
		public string Phone { get; set; }

		[Column(Name = "Director_Name")]
		public string Director { get; set; }
	}
}
