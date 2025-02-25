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
	public partial class Supplier : Form
	{
		static string conn = System.Configuration.ConfigurationManager.ConnectionStrings["MedicineConnectionString"].ConnectionString;
		int Supplier_ID { get; set; }
		public Supplier(int Supplier_ID)
		{
			this.Supplier_ID = Supplier_ID;
			InitializeComponent();
		}

		private void Supplier_Load(object sender, EventArgs e)
		{
			LoadData();
		}

		public void LoadData()
		{
			{
				using (DataContext db = new DataContext(conn))
				{
					dataGridView1.DataSource = db.GetTable<Suppliers>().Where(p => p.SupplierID == Supplier_ID)
																	  .Select(d => new { SupplierID = d.SupplierID, S_Name= d.S_Name, F_Name = d.F_Name, Address = d.Address, Phone = d.Phone, Director = d.Director });

					dataGridView1.Columns["SupplierID"].Visible = false;
					dataGridView1.Columns["SupplierID"].DisplayIndex = 0;

					dataGridView1.Columns["S_name"].HeaderText = "Сокращенное название";
					dataGridView1.Columns["S_Name"].DisplayIndex = 1;

					dataGridView1.Columns["F_Name"].HeaderText = "Название";
					dataGridView1.Columns["F_Name"].DisplayIndex = 2;

					dataGridView1.Columns["Address"].HeaderText = "Адрес";
					dataGridView1.Columns["Address"].DisplayIndex = 3;

					dataGridView1.Columns["Phone"].HeaderText = "Телефон";
					dataGridView1.Columns["Phone"].DisplayIndex = 4;

					dataGridView1.Columns["Director"].HeaderText = "Директор";
					dataGridView1.Columns["Director"].DisplayIndex = 5;
				}
				dataGridView1.Refresh();
			}
		}

		private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
		{

		}

		private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			try
			{
				if (e.Button == MouseButtons.Right)
				{
					dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
					dataGridView1.Rows[e.RowIndex].Selected = true;
					dataGridView1.Focus();
				}
			}
			catch (Exception)
			{

			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			SupplierAddForm frm = new SupplierAddForm(1, 0);
			frm.Owner = this;
			frm.Show();
		}

		private void dataGridView_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				contextMenuStrip2.Show(Cursor.Position.X, Cursor.Position.Y);
			}
		}

		private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
		{

		}

		private void редактироватьToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			SupplierAddForm frm = new SupplierAddForm(2, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["SupplierID"].Value));
			frm.Owner = this;
			frm.Show();
		}

		private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridView1.SelectedRows.Count > 0)
			{
				int SupplierID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["SupplierID"].Value);

				using (DataContext db = new DataContext(conn))
				{
					var document = db.GetTable<Suppliers>().FirstOrDefault(u => u.SupplierID == SupplierID);
					if (document != null)
					{
						db.GetTable<Suppliers>().DeleteOnSubmit(document);
						db.SubmitChanges();
						this.LoadData();
					}
					else
					{
						MessageBox.Show("Поступление не найдено!");
					}
				}
			}
			else
			{
				MessageBox.Show("Выберите поступление для удаления!");
			}
		}
	}
}
