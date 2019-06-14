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
    public partial class Edit : Form
    {
        public Edit()
        {
            InitializeComponent();
            StringBuilder qadress = new StringBuilder();
            qadress.Append("https://www.google.com.ua/maps/@49.8388749,24.0303077,13z?hl=uk&authuser=0");
            webBrowser1.Navigate(qadress.ToString());
        }

        private void btnAddHand_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Hide();
        }
    }
}
