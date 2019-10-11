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
    public partial class MasterRoomType : Form
    {
        private enum mode {Insert, Update };
        private mode modMode;
        public MasterRoomType()
        {
            InitializeComponent();
        }

        private bool validate()
        {
            if (textBox1.Text == null || textBox2.Text == null || numericUpDown1.Value < 1)
                return false;
            else
                return true;
        }

        private void updateDataGrid()
        {
            DataSet ds = DB.GetDataSet("SELECT * FROM RoomType");
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.Columns[0].Visible = false;
        }

        private void MasterRoomType_Load(object sender, EventArgs e)
        {
            updateDataGrid();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            btnInsert.Enabled = btnDelete.Enabled = btnUpdate.Enabled = false;
            btnSave.Enabled = btnCancel.Enabled = true;
            textBox1.Text = textBox2.Text = null;
            textBox1.Enabled = textBox2.Enabled = numericUpDown1.Enabled = true;

            modMode = mode.Insert;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            btnInsert.Enabled = btnDelete.Enabled = btnUpdate.Enabled = false;
            btnSave.Enabled = btnCancel.Enabled = true;
            textBox1.Enabled = textBox2.Enabled = numericUpDown1.Enabled = true;

            modMode = mode.Update;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure ?","Question",MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                int id = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                DB.ExecuNoQue($"DELETE FROM RoomType WHERE ID = {id}");
                updateDataGrid();
            }
            else
            {
                btnInsert.Enabled = btnDelete.Enabled = btnUpdate.Enabled = true;
                btnSave.Enabled = btnCancel.Enabled = false;
                textBox1.Enabled = textBox2.Enabled = numericUpDown1.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (modMode == mode.Insert)
            {
                DB.ExecuNoQue($"INSERT INTO RoomType VALUES ('{textBox1.Text}','{numericUpDown1.Value}', '{textBox2.Text}')");
            }else if (modMode == mode.Update)
            {
                int id = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                string name = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                DB.ExecuNoQue($"UPDATE RoomType Set Name = '{textBox1.Text}', Capacity = {numericUpDown1.Value}, RoomPrice ={textBox2.Text} WHERE ID = {id} or Name = '{name}'");
            }

            updateDataGrid();

            btnInsert.Enabled = btnDelete.Enabled = btnUpdate.Enabled = true;
            btnSave.Enabled = btnCancel.Enabled = false;
            textBox1.Enabled = textBox2.Enabled = numericUpDown1.Enabled = false;
            textBox1.Enabled = textBox2.Enabled = numericUpDown1.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnInsert.Enabled = btnDelete.Enabled = btnUpdate.Enabled = true;
            btnSave.Enabled = btnCancel.Enabled = false;


            textBox1.Enabled = textBox2.Enabled = numericUpDown1.Enabled = false;
            textBox1.Text = textBox2.Text = null;
        }
    }
}
