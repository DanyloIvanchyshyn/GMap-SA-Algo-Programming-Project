using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Map_v1._0_project_team7_sa
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            pnlRoutes.Hide();
            //try
            //{
            StringBuilder qadress = new StringBuilder();
            qadress.Append("https://www.google.com.ua/maps/@49.8388749,24.0303077,13z?hl=uk&authuser=0");
            //if (city != string.Empty)
            //{
            //  qadress.Append(city + "," + "+");
            //}
            webBrowser1.Navigate(qadress.ToString());
        }
      /*  private void btnAdd_Click(object sender, EventArgs e)
        {
            Add frm1 = new Add();
            frm1.ShowDialog(this);
        }

       

        
        private void btnHideAll_Click(object sender, EventArgs e)
        {
            pnlRoutes.Hide();
        }

        private void btnEdit_Click_1(object sender, EventArgs e)
        {
            Edit fm2 = new Edit();
            fm2.ShowDialog(this);
           // Edit frm2 = new Edit();
            //frm2.ShowDialog(this);
        }*/

        private void btnMenu_Click_1(object sender, EventArgs e)
        {
            pnlRoutes.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Edit fm2 = new Edit();
            fm2.ShowDialog(this);
        }

        private void btnHideAll_Click(object sender, EventArgs e)
        {
            pnlRoutes.Hide();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add frm1 = new Add();
            frm1.ShowDialog(this);
        }
    }
}
