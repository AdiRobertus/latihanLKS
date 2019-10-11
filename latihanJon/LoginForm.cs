using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace latihanJon
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbUser.Text) || string.IsNullOrEmpty(tbPass.Text))
            {
                MessageBox.Show("username dan password must filled");
                return;
            }

            DataSet ds = DB.Login(tbUser.Text, tbPass.Text);

            if (ds.Tables[0].Rows.Count < 1)
            {
                MessageBox.Show("invalid login");
            }

            int job = ds.Tables[0].Rows[0].Field<int>("JobID");

            this.Hide();
            switch(job)
            {
                case 1:
                    new frontOfficeForm().ShowDialog();
                    break;
                case 4:
                    new houseKeeperForm().ShowDialog();
                    break;
                case 6:
                    new houseKeeperSupervisorForm().ShowDialog();
                    break;
                case 7:
                    new AdminForm().ShowDialog();
                    break;    
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
