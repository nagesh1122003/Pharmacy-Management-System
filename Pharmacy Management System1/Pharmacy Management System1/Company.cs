using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Pharmacy_Management_System1
{
    public partial class Company : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-NSG13A1\SQLEXPRESS;Initial Catalog=PharmacyCenter_db;Integrated Security=True;Pooling=False;Encrypt=True;TrustServerCertificate=True");
        public void populate()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-NSG13A1\SQLEXPRESS;Initial Catalog=PharmacyCenter_db;Integrated Security=True;Pooling=False;Encrypt=True;TrustServerCertificate=True");
            conn.Open();
            string myquery = "select * from Company_tbl";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(myquery, conn);
            SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            conn.Close();
        }
        public Company()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Missing Data,Fill All The Information");
            }
            else
            {
                string cs = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
                SqlConnection conn = new SqlConnection(cs);

                conn.Open();
                string query = " insert into Company_tbl values(@CompId,@Compname,@CompPhone,@CompAddress)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CompId", textBox1.Text);
                cmd.Parameters.AddWithValue("@Compname", textBox2.Text);
                cmd.Parameters.AddWithValue("@CompPhone", textBox3.Text);
                cmd.Parameters.AddWithValue("@CompAddress", textBox4.Text);
               

                int a = cmd.ExecuteNonQuery();
                if (a > 0)
                {
                    MessageBox.Show("Information Successfully Added");
                }
                else
                {
                    MessageBox.Show("Information Not Added");
                }
                conn.Close();
                populate();
            }
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
            SqlConnection conn = new SqlConnection(cs);

            conn.Open();
            string query = "UPDATE Company_tbl SET Compname = @Compname, CompPhone = @CompPhone, CompAddress = @CompAddress WHERE CompId = @CompId";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@CompId", textBox1.Text);
            cmd.Parameters.AddWithValue("@Compname", textBox2.Text);
            cmd.Parameters.AddWithValue("@CompPhone", textBox3.Text);
            cmd.Parameters.AddWithValue("@CompAddress", textBox4.Text);
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Information Successfully Updated");
            }
            else
            {
                MessageBox.Show("Information Not Updated");
            }
            conn.Close();
            populate();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Wrong Operation.Click on the Company to be deleted");
            }
            else
            {


                string cs = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
                SqlConnection conn = new SqlConnection(cs);

                conn.Open();
                string query = "DELETE FROM  Company_tbl WHERE CompId=@CompId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CompId", textBox1.Text);
                cmd.Parameters.AddWithValue("@Compname", textBox2.Text);
                cmd.Parameters.AddWithValue("@CompPhone", textBox3.Text);
                cmd.Parameters.AddWithValue("@CompAddress", textBox4.Text);
                int a = cmd.ExecuteNonQuery();
                if (a > 0)
                {
                    MessageBox.Show("Company Deleted Successfully ");
                }
                else
                {
                    MessageBox.Show("Company Not Deleted");
                }
                conn.Close();
                populate();
            }
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            populate();
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HomeForm2 homeForm2 = new HomeForm2();
            homeForm2.Show();
            this.Hide();
        }

        private void Company_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            populate();
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
        }
    }
}
