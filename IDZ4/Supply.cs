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
using System.Xml.Linq;

namespace IDZ4
{
	public partial class Supply : Form
	{
		static string conn = System.Configuration.ConfigurationManager.ConnectionStrings["MedicineConnectionString"].ConnectionString;

		int Id {  get; set; }

		public Supply(int Id)
		{
			this.Id = Id;
			InitializeComponent();
		}

		public void LoadData()
		{
			using (DataContext db = new DataContext(conn))
			{
				label1.Text = "Поступления для: " + db.GetTable<Medicines>().Select(d => new { d.Id, Name = d.Name }).FirstOrDefault(d => d.Id == this.Id).Name;

				dataGridView1.DataSource = db.GetTable<Supplies>().Where(d => d.Id == this.Id).Select(d => new { SupplyId = d.SupplyID, S_Date = d.S_Date, Price = d.Price, Quantity2 = d.Quantity2, Id = d.Id, SupplierID = d.SupplierID });

				dataGridView1.Columns["SupplyId"].HeaderText = "Код поступления";
				dataGridView1.Columns["SupplyId"].DisplayIndex = 0;

				dataGridView1.Columns["S_Date"].HeaderText = "Дата поставки";
				dataGridView1.Columns["S_Date"].DisplayIndex = 1;

				dataGridView1.Columns["Price"].HeaderText = "Цена за единицу";
				dataGridView1.Columns["Price"].DisplayIndex = 2;

				dataGridView1.Columns["Quantity2"].HeaderText = "Количество";
				dataGridView1.Columns["Quantity2"].DisplayIndex = 3;

				dataGridView1.Columns["Id"].HeaderText = "Код лекарства";
				dataGridView1.Columns["Id"].DisplayIndex = 4;

				dataGridView1.Columns["SupplierID"].HeaderText = "Код поставщика";
				dataGridView1.Columns["SupplierID"].DisplayIndex = 5;

				dataGridView1.Refresh();
			}
		}

		private void Supply_Load(object sender, EventArgs e)
		{
			LoadData();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			SupplyAddForm frm = new SupplyAddForm(1, 0);
			frm.Owner = this;
			frm.Show();
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

		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
		{

		}
		private void поступленияToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Supplier frm = new Supplier(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["SupplierID"].Value));
			frm.Owner = this;
			frm.Show();
		}

		private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SupplyAddForm frm = new SupplyAddForm(2, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["SupplyID"].Value));
			frm.Owner = this;
			frm.Show();
		}

		private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridView1.SelectedRows.Count > 0)
			{
				int SupplyID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["SupplyID"].Value);

				using (DataContext db = new DataContext(conn))
				{
					var document = db.GetTable<Supplies>().FirstOrDefault(u => u.SupplyID == SupplyID);
					if (document != null)
					{
						db.GetTable<Supplies>().DeleteOnSubmit(document);
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

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void dataGridView1_MouseClick1(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
			}
		}
	}
}
