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
	public partial class Medicine : Form
	{
		static string conn = System.Configuration.ConfigurationManager.ConnectionStrings["MedicineConnectionString"].ConnectionString;

		public Medicine()
		{
			InitializeComponent();
		}

		public void LoadData()
		{
			using (DataContext db = new DataContext(conn))
			{
				dataGridView1.DataSource = db.GetTable<Medicines>().Select(d => new { ID = d.Id, Name = d.Name, Indications = d.Indications, Unit = d.Unit, Quantity = d.Quantity, Manufacturer = d.Manufacturer });

				dataGridView1.Columns["Id"].HeaderText = "Код лекарства";
				dataGridView1.Columns["Id"].DisplayIndex = 0;

				dataGridView1.Columns["Name"].HeaderText = "Название";
				dataGridView1.Columns["Name"].DisplayIndex = 1;

				dataGridView1.Columns["Indications"].HeaderText = "Показания";
				dataGridView1.Columns["Indications"].DisplayIndex = 2;

				dataGridView1.Columns["Unit"].HeaderText = "Единица";
				dataGridView1.Columns["Unit"].DisplayIndex = 3;

				dataGridView1.Columns["Quantity"].HeaderText = "Количество";
				dataGridView1.Columns["Quantity"].DisplayIndex = 4;

				dataGridView1.Columns["Manufacturer"].HeaderText = "Производитель";
				dataGridView1.Columns["Manufacturer"].DisplayIndex = 5;

				dataGridView1.Refresh();
			}

		}

		private void button1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void Medicine_Load(object sender, EventArgs e)
		{
			LoadData();
		}
		private void button2_Click(object sender, EventArgs e)
		{

		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}
		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
		{

		}

		private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
			}
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

		private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MedicineAddForm frm = new MedicineAddForm(2, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value));
			frm.Owner = this;
			frm.Show();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			MedicineAddForm frm = new MedicineAddForm(1, 0);
			frm.Owner = this;
			frm.Show();
		}

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int medicineId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);

                using (DataContext db = new DataContext(conn))
                {
                    var medicine = db.GetTable<Medicines>().FirstOrDefault(u => u.Id == medicineId);
					if (medicine != null)
					{

						var relatedSupplies = db.GetTable<Supplies>().Where(s => s.Id == medicineId).ToList();
						if (relatedSupplies.Count > 0)
						{

							var result = MessageBox.Show("Существуют связанные поставки.");

						}
						else
						{
							db.GetTable<Medicines>().DeleteOnSubmit(medicine);
							db.SubmitChanges();

							this.LoadData();
						}
					}
					else
					{
						MessageBox.Show("Лекарство не найдено!");
					}
                }
            }
            else
            {
                MessageBox.Show("Выберите лекарство для удаления!");
            }
        }

        private void поступленияToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Supply frm = new Supply(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value));
			frm.Owner = this;
			frm.Show();
		}
	}
}
