using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihanJon
{
    public partial class MasterRoom : Form
    {
        private enum mode { Insert, Update };
        private mode modMode;
        public MasterRoom()
        {
            InitializeComponent();
        }

        private void updateDataGrid()
        {
            DataSet ds = DB.GetDataSet("SELECT * FROM Room");
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.Columns[0].Visible = false;

            DataSet ds2 = DB.GetDataSet("SELECT * FROM RoomType");
            comboBox1.DataSource = ds2.Tables[0];
            comboBox1.ValueMember = "ID";
            comboBox1.DisplayMember = "Name";
            comboBox1.SelectedIndex = 0;
        }

        private void MasterRoom_Load(object sender, EventArgs e)
        {
            updateDataGrid();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            btnInsert.Enabled = btnDelete.Enabled = btnUpdate.Enabled = false;
            btnSave.Enabled = btnCancel.Enabled = true;
            textBox1.Enabled = textBox2.Enabled = comboBox1.Enabled = textBox3.Enabled = true;

            modMode = mode.Update;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            btnInsert.Enabled = btnDelete.Enabled = btnUpdate.Enabled = false;
            btnSave.Enabled = btnCancel.Enabled = true;
            textBox1.Text = textBox2.Text = null;
            textBox1.Enabled = textBox2.Enabled = comboBox1.Enabled = textBox3.Enabled = true;

            modMode = mode.Insert;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure ?", "Question", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int id = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                DB.ExecuNoQue($"DELETE FROM Room WHERE ID = {id}");
                updateDataGrid();
            }
            else
            {
                btnInsert.Enabled = btnDelete.Enabled = btnUpdate.Enabled = true;
                btnSave.Enabled = btnCancel.Enabled = false;
                textBox1.Enabled = textBox2.Enabled = comboBox1.Enabled = textBox3.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (modMode == mode.Insert)
            {
                DB.ExecuNoQue($"INSERT INTO Room VALUES ({comboBox1.SelectedValue},{textBox1.Text},{textBox2.Text} ,'{textBox3.Text}')");
            }
            else if (modMode == mode.Update)
            {
                int id = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                DB.ExecuNoQue($"UPDATE Room Set RoomNumber = {textBox1.Text}, RoomTypeID = {comboBox1.SelectedValue}, RoomFloor = {textBox2.Text}, Description = '{textBox3.Text}' WHERE ID = {id}");
            }

            updateDataGrid();

            btnInsert.Enabled = btnDelete.Enabled = btnUpdate.Enabled = true;
            btnSave.Enabled = btnCancel.Enabled = false;
            textBox1.Enabled = textBox2.Enabled = comboBox1.Enabled = textBox3.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnInsert.Enabled = btnDelete.Enabled = btnUpdate.Enabled = true;
            btnSave.Enabled = btnCancel.Enabled = false;


            textBox1.Enabled = textBox2.Enabled = comboBox1.Enabled = textBox3.Enabled = false;
            textBox1.Text = textBox2.Text = null;
        }
    }
}
