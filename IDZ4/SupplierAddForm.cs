using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IDZ4
{
	public partial class SupplierAddForm : Form
	{
		static string conn = System.Configuration.ConfigurationManager.ConnectionStrings["MedicineConnectionString"].ConnectionString;

		int oper { get; set; }
		int Supplier_ID { get; set; }
		public SupplierAddForm(int oper, int Supplier_ID)
		{
			this.oper = oper;
			this.Supplier_ID = Supplier_ID;
			InitializeComponent();
		}

		private void SupplierAddForm_Load(object sender, EventArgs e)
		{
			if (this.oper == 1)
			{
				button2.Text = "Добавить";
				using (DataContext db = new DataContext(conn))
				{

					this.textBox1.Text = "";
					this.textBox2.Text = "";
					this.textBox3.Text = "";
					this.textBox4.Text = "";
					this.textBox5.Text = "";
				}
			}
			else
			{
				button2.Text = "Редактировать";
				using (DataContext db = new DataContext(conn))
				{
					var med = db.GetTable<Suppliers>().FirstOrDefault(u => u.SupplierID == this.Supplier_ID);
					this.textBox1.Text = med.S_Name;
					this.textBox2.Text = med.F_Name;
					this.textBox3.Text = med.Address;
					this.textBox4.Text = med.Phone;
					this.textBox5.Text = med.Director;
				}
			}
		}



		private void button1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(this.textBox2.Text) ||
				string.IsNullOrEmpty(this.textBox3.Text) ||
				string.IsNullOrEmpty(this.textBox4.Text) ||
				string.IsNullOrEmpty(this.textBox5.Text) ||
				string.IsNullOrEmpty(this.textBox1.Text)
				)
			{
				MessageBox.Show("Не все поля заполнены, заполните их!!!");
			}
			else
			{
				using (DataContext db = new DataContext(conn))
				{
					if (this.oper == 1)
					{
						Suppliers med = new Suppliers { S_Name = this.textBox1.Text, F_Name = this.textBox2.Text, Address = this.textBox3.Text, Phone = this.textBox4.Text, Director = this.textBox5.Text };
						db.GetTable<Suppliers>().InsertOnSubmit(med);
						db.SubmitChanges();
					}
					else
					{
						var med = db.GetTable<Suppliers>().FirstOrDefault(u => u.SupplierID == this.Supplier_ID);
						med.S_Name = this.textBox1.Text;
						med.F_Name = this.textBox2.Text;
						med.Address = this.textBox3.Text;
						med.Phone = this.textBox4.Text;
						med.Director = this.textBox5.Text;
						db.SubmitChanges();
					}
				}
				Supplier frm = this.Owner as Supplier;
				frm.LoadData();
				frm.Refresh();
				this.Close();
			}
		}
	}
}
