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
    public partial class frontOfficeForm : Form
    {
        public frontOfficeForm()
        {
            InitializeComponent();
        }

        private void btnReservation_Click(object sender, EventArgs e)
        {
            this.Hide();
            new ReservationForm().ShowDialog();
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            
            this.Hide();
            new CheckIn().ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            this.Hide();
            new request().ShowDialog();
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            
            this.Hide();
            new CheckOut().ShowDialog();
        }

        private void btnMasterRoomType_Click(object sender, EventArgs e)
        {

            this.Hide();
            new MasterRoomType().ShowDialog();
        }

        private void btnMasterRoom_Click(object sender, EventArgs e)
        {

            this.Hide();
            new MasterRoom().ShowDialog();
        }

        private void btnMasterItem_Click(object sender, EventArgs e)
        {

            this.Hide();
            new MasterItem().ShowDialog();
        }
    }
}
