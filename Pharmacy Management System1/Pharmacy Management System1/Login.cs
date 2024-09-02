using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pharmacy_Management_System1
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text == "user" && textBox2.Text == "1234")
            {
                HomeForm2 homeForm2 = new HomeForm2();
                homeForm2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong Username Or Password");
            }         

        }

        private void Login_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
           /* if (e.KeyChar == (char)Keys.Enter)
            {
                button1.PerformClick();
            }*/
            if(e.KeyChar==(char)Keys.Enter)
            {
                button1.PerformClick();
            }
        }
    }
}
