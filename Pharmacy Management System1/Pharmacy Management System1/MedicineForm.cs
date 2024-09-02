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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Pharmacy_Management_System1
{
    public partial class MedicineForm : Form
    {
        public void populate()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-NSG13A1\SQLEXPRESS;Initial Catalog=PharmacyCenter_db;Integrated Security=True;Pooling=False;Encrypt=True;TrustServerCertificate=True");
            conn.Open();
            string myquery = "select * from Medicine_tbl";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(myquery,conn);
            SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(dataAdapter);
            var ds=new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            conn.Close();
        }
       
        public MedicineForm()
        {
            InitializeComponent();
        }

        public void fillcombobox()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-NSG13A1\SQLEXPRESS;Initial Catalog=PharmacyCenter_db;Integrated Security=True;Pooling=False;Encrypt=True;TrustServerCertificate=True");

            string myquery = "select Compname from Company_tbl";
            SqlCommand cmd = new SqlCommand(myquery, conn);
            SqlDataReader rdr;
            try
            {
                conn.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("Compname", typeof(string));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                comboBox1.ValueMember = "Compname";
                comboBox1.DataSource = dt;


                conn.Close();
            }
            catch
            {

            }  
        }
        private void MedicineForm_Load(object sender, EventArgs e)
        {
            
            populate();
            fillcombobox();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || comboBox1.SelectedItem== null)
            {
                MessageBox.Show("Missing Data,Fill All The Information");
            }
            else 
            {           
              string cs = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
              SqlConnection conn = new SqlConnection(cs);
              
              conn.Open();
              string query= " insert into Medicine_tbl values(@MedName,@BPrice,@SPrice,@MedQty,@ExpDate,@Company)";
              SqlCommand cmd = new SqlCommand(query,conn);
              cmd.Parameters.AddWithValue("@MedName",textBox1.Text);
              cmd.Parameters.AddWithValue("@BPrice", textBox2.Text);
              cmd.Parameters.AddWithValue("@SPrice", textBox3.Text);
              cmd.Parameters.AddWithValue("@MedQty", textBox4.Text);
              cmd.Parameters.AddWithValue("@ExpDate", dateTimePicker1.Value);
              cmd.Parameters.AddWithValue("@Company", comboBox1.SelectedValue.ToString());
              int a=cmd.ExecuteNonQuery();
              if(a>0)
              {
                  MessageBox.Show("Medicine Successfully Added");
              }
              else
              {
                  MessageBox.Show("Medicine Not Added");
              }
              
              conn.Close();
              populate();
            }
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            comboBox1.SelectedItem=null;
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
                SqlConnection conn = new SqlConnection(cs);

                conn.Open();
                string query = "UPDATE Medicine_tbl SET BPrice = @BPrice, SPrice = @SPrice, MedQty = @MedQty, ExpDate = @ExpDate, Company = @Company WHERE MedName = @MedName";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MedName", textBox1.Text);
                cmd.Parameters.AddWithValue("@BPrice", textBox2.Text);
                cmd.Parameters.AddWithValue("@SPrice", textBox3.Text);
                cmd.Parameters.AddWithValue("@MedQty", textBox4.Text);
                cmd.Parameters.AddWithValue("@ExpDate", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@Company", comboBox1.SelectedValue.ToString());
                int a = cmd.ExecuteNonQuery();
                if (a > 0)
                {
                    MessageBox.Show("Medicine Successfully Updated");
                }
                else
                {
                    MessageBox.Show("Medicine Not Updated");
                }

                conn.Close();
                populate();
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                comboBox1.SelectedIndex=-1;
            }
            catch
            {

            }
               



        }
        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox1.Text=="")
            {
                MessageBox.Show("Wrong Operation.Click on the medicine to be deleted");
            }
            else
            {

            
              string cs = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
              SqlConnection conn = new SqlConnection(cs);
              
              conn.Open();
              string query = "DELETE FROM  Medicine_tbl WHERE MedName=@MedName";
              SqlCommand cmd = new SqlCommand(query, conn);
              cmd.Parameters.AddWithValue("@MedName", textBox1.Text);
              cmd.Parameters.AddWithValue("@BPrice", textBox2.Text);
              cmd.Parameters.AddWithValue("@SPrice", textBox3.Text);
              cmd.Parameters.AddWithValue("@MedQty", textBox4.Text);
              cmd.Parameters.AddWithValue("@ExpDate", dateTimePicker1.Value);
              cmd.Parameters.AddWithValue("@Company", comboBox1.SelectedValue.ToString());
              int a = cmd.ExecuteNonQuery();
              if (a > 0)
              {
                  MessageBox.Show("Medicine Deleted Successfully ");
              }
              else
              {
                  MessageBox.Show("Medicine Not Deleted");
              }
              
              conn.Close();
              populate();
            }
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            comboBox1.SelectedIndex = -1;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            HomeForm2 homeForm2 = new HomeForm2();
            homeForm2.Show();
            this.Hide();
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            comboBox1.SelectedItem = dataGridView1.Rows[0].Cells[4].Value;
            //dateTimePicker1.Value=dataGridView1.Rows[0].Cells[5].Value.ToString();

        }
    }
}
