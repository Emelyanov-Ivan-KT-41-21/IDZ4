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
using System.Windows.Forms.VisualStyles;

namespace IDZ4
{
	public partial class SupplyAddForm : Form
	{
		static string conn = System.Configuration.ConfigurationManager.ConnectionStrings["MedicineConnectionString"].ConnectionString;

		int oper {  get; set; }
		int SupplyID { get; set; }
		public SupplyAddForm(int oper, int SupplyID)
		{
			this.oper = oper;
			this.SupplyID = SupplyID;
			InitializeComponent();
		}

		private void SupplyAddForm_Load(object sender, EventArgs e)
		{
			if (this.oper == 1)
			{
				button2.Text = "Добавить";
				using (DataContext db = new DataContext(conn))
				{

					this.dateTimePicker1.Value = DateTime.Now;
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
					var med = db.GetTable<Supplies>().FirstOrDefault(u => u.SupplyID == this.SupplyID);
					this.dateTimePicker1.Value = med.S_Date;
					this.textBox2.Text = med.Price.ToString();
					this.textBox3.Text = med.Quantity2.ToString();
					this.textBox4.Text = med.Id.ToString();
					this.textBox5.Text = med.SupplierID.ToString();
				}
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(this.textBox2.Text) ||
				string.IsNullOrEmpty(this.textBox3.Text) ||
				string.IsNullOrEmpty(this.textBox4.Text) ||
				string.IsNullOrEmpty(this.textBox5.Text)
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
						Supplies med = new Supplies { S_Date = this.dateTimePicker1.Value, Price = Convert.ToInt32(this.textBox2.Text), Quantity2 = Convert.ToInt32(this.textBox3.Text), Id = Convert.ToInt32(this.textBox4.Text), SupplierID = Convert.ToInt32(this.textBox5.Text) };
						db.GetTable<Supplies>().InsertOnSubmit(med);
						db.SubmitChanges();
					}
					else
					{
						var med = db.GetTable<Supplies>().FirstOrDefault(u => u.SupplyID == this.SupplyID);
						med.S_Date = this.dateTimePicker1.Value;
						med.Price = Convert.ToInt32(this.textBox2.Text);
						med.Quantity2 = Convert.ToInt32(this.textBox3.Text);
						med.Id = Convert.ToInt32(this.textBox4.Text);
						med.SupplierID = Convert.ToInt32(this.textBox5.Text);
						db.SubmitChanges();
					}
				}
				Supply frm = this.Owner as Supply;
				frm.LoadData();
				frm.Refresh();
				this.Close();
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
