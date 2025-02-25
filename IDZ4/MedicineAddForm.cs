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
	public partial class MedicineAddForm : Form
	{
		static string conn = System.Configuration.ConfigurationManager.ConnectionStrings["MedicineConnectionString"].ConnectionString;

		int oper { get; set; }

		int UserID { get; set; }
		public MedicineAddForm(int oper, int UserID)
		{
			this.oper = oper;
			this.UserID = UserID;
			InitializeComponent();
		}

		private void label2_Click(object sender, EventArgs e)
		{
			
		}

		private void MedicineAddForm_Load(object sender, EventArgs e)
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
					var med = db.GetTable<Medicines>().FirstOrDefault(u => u.Id == this.UserID);
					this.textBox1.Text = med.Name;
					this.textBox2.Text = med.Indications;
					this.textBox3.Text = med.Unit;
					this.textBox4.Text = med.Quantity.ToString();
					this.textBox5.Text = med.Manufacturer;
				}
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(this.textBox1.Text) ||
				string.IsNullOrEmpty(this.textBox2.Text) ||
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
						Medicines med = new Medicines { Name = this.textBox1.Text, Indications = this.textBox2.Text, Unit = this.textBox3.Text, Quantity = Convert.ToInt32(this.textBox4.Text), Manufacturer = this.textBox5.Text };
						db.GetTable<Medicines>().InsertOnSubmit(med);
						db.SubmitChanges();
					}
					else
					{
						var med = db.GetTable<Medicines>().FirstOrDefault(u => u.Id == this.UserID);
						med.Name = this.textBox1.Text;
						med.Indications = this.textBox2.Text;
						med.Unit = this.textBox3.Text;
						med.Quantity = Convert.ToInt32(this.textBox4.Text);
						med.Manufacturer = this.textBox5.Text;
						db.SubmitChanges();
					}
				}
				Medicine frm = this.Owner as Medicine;
				frm.LoadData();
				frm.Refresh();
				this.Close();
			}
		}
	}
}
