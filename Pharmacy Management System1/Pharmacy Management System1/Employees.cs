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
using System.Configuration;

namespace Pharmacy_Management_System1
{
    public partial class Employees : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-NSG13A1\SQLEXPRESS;Initial Catalog=PharmacyCenter_db;Integrated Security=True;Pooling=False;Encrypt=True;TrustServerCertificate=True");
        public void populate()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-NSG13A1\SQLEXPRESS;Initial Catalog=PharmacyCenter_db;Integrated Security=True;Pooling=False;Encrypt=True;TrustServerCertificate=True");
            conn.Open();
            string myquery = "select * from Employee_tbl";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(myquery, conn);
            SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            conn.Close();
        }
        public Employees()
        {
            InitializeComponent();
        }

        private void Employees_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || comboBox1.SelectedItem.ToString() == null)
            {
                MessageBox.Show("Missing Data,Fill All The Information");
            }
            else
            {
                string cs = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
                SqlConnection conn = new SqlConnection(cs);

                conn.Open();
                string query = " insert into Employee_tbl values(@EmpId,@Empname,@EmpAge,@Empsalary,@EmpPhone ,@EmpGender,@EmpPass)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EmpId", textBox1.Text);
                cmd.Parameters.AddWithValue("@Empname", textBox2.Text);
                cmd.Parameters.AddWithValue("@EmpAge", textBox3.Text);
                cmd.Parameters.AddWithValue("@Empsalary", textBox4.Text);
                cmd.Parameters.AddWithValue("@EmpPhone", textBox5.Text);               
                cmd.Parameters.AddWithValue("@EmpGender", comboBox1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@EmpPass", textBox6.Text);

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
            textBox5.Clear();
            comboBox1.SelectedIndex=-1;
            textBox6.Clear();


        }
        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            comboBox1.SelectedItem = dataGridView1.Rows[0].Cells[5].Value;
            textBox6.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();





        }

        private void button2_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
            SqlConnection conn = new SqlConnection(cs);

            conn.Open();
            string query = "UPDATE Employee_tbl SET Empname = @Empname, EmpAge = @EmpAge, Empsalary = @Empsalary, EmpPhone = @EmpPhone, EmpGender = @EmpGender, EmpPass = @EmpPass WHERE EmpId = @EmpId";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@EmpId", textBox1.Text);
            cmd.Parameters.AddWithValue("@Empname", textBox2.Text);
            cmd.Parameters.AddWithValue("@EmpAge", textBox3.Text);
            cmd.Parameters.AddWithValue("@Empsalary", textBox4.Text);
            cmd.Parameters.AddWithValue("@EmpPhone", textBox5.Text);
            cmd.Parameters.AddWithValue("@EmpGender", comboBox1.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@EmpPass", textBox6.Text);

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
            textBox5.Clear();
            comboBox1.Items.Clear();
            textBox6.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "")
            {
                MessageBox.Show("Wrong Operation.Click on the Employee to be deleted");
            }
            else
            {


                string cs = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
                SqlConnection conn = new SqlConnection(cs);

                conn.Open();
                string query = "DELETE FROM  Employee_tbl WHERE EmpId=@EmpId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EmpId", textBox1.Text);
                cmd.Parameters.AddWithValue("@Empname", textBox2.Text);
                cmd.Parameters.AddWithValue("@EmpAge", textBox3.Text);
                cmd.Parameters.AddWithValue("@Empsalary", textBox4.Text);
                cmd.Parameters.AddWithValue("@EmpPhone", textBox5.Text);
                cmd.Parameters.AddWithValue("@EmpGender", comboBox1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@EmpPass", textBox6.Text);
                int a = cmd.ExecuteNonQuery();
                if (a > 0)
                {
                    MessageBox.Show("Employee Deleted Successfully ");
                }
                else
                {
                    MessageBox.Show("Employee Not Deleted");
                }

                conn.Close();
                populate();

            }
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            comboBox1.Items.Clear();
            textBox6.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HomeForm2 homeForm2 = new HomeForm2();
            homeForm2.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            comboBox1.SelectedItem = dataGridView1.Rows[0].Cells[5].Value;
            textBox6.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
        }
    }
}
