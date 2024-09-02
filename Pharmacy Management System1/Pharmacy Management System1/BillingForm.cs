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
    public partial class BillingForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-NSG13A1\SQLEXPRESS;Initial Catalog=PharmacyCenter_db;Integrated Security=True;Pooling=False;Encrypt=True;TrustServerCertificate=True");
        public void populatecombobox()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-NSG13A1\SQLEXPRESS;Initial Catalog=PharmacyCenter_db;Integrated Security=True;Pooling=False;Encrypt=True;TrustServerCertificate=True");
            
            string myquery = "select * from Medicine_tbl";
            SqlCommand cmd = new SqlCommand(myquery, conn);
            SqlDataReader rdr;
            try
            {
                conn.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("Medname",typeof(string));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                comboBox1.ValueMember = "Medname";
                comboBox1.DataSource= dt;


                conn.Close();
            }
            catch
            {

            }
           
        }
        int x,unitp;
        public void fetchQty()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-NSG13A1\SQLEXPRESS;Initial Catalog=PharmacyCenter_db;Integrated Security=True;Pooling=False;Encrypt=True;TrustServerCertificate=True");
            conn.Open();
            string query = "select * from Medicine_tbl where Medname='"+comboBox1.SelectedValue.ToString()+"'";
            SqlCommand cmd =new SqlCommand(query, conn);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                x = Convert.ToInt32(dr["medQty"].ToString());
                unitp =Convert.ToInt32(dr["SPrice"].ToString());
                label2.Text = "Availabel Stock Is:"+dr["medQty"].ToString();
                label2.Visible = true;
            }
            conn.Close();


        }
        public void UpdateMedicine()
        {
            string cs = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
            SqlConnection conn = new SqlConnection(cs);

            conn.Open();
            int newQty=x-Convert.ToInt32(textBox1.Text);
            string query = "UPDATE Medicine_tbl SET  MedQty = @MedQty WHERE Medname=@Medname";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MedQty", textBox1.Text);         
            cmd.Parameters.AddWithValue("@Medname", comboBox1.SelectedItem.ToString());
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
        }
        public BillingForm()
        {
            InitializeComponent();
        }

        private void BillingForm_Load(object sender, EventArgs e)
        {
            populatecombobox();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fetchQty();
        }
        int grdtotal = 0;
        Bitmap bitmap;

        private void button2_Click(object sender, EventArgs e)
        {
            Panel panel = new Panel();  
            this.Controls.Add(panel);
            Graphics graphics = panel.CreateGraphics();
            Size size = this.ClientSize;
            bitmap=new Bitmap(size.Width, size.Height,graphics);
            graphics=Graphics.FromImage(bitmap);
            Point point = PointToScreen(panel.Location);
            graphics.CopyFromScreen(point.X, point.Y, 0, 0, size);
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();



        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, 0, 0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HomeForm2 homeForm2 = new HomeForm2();
            homeForm2.Show();
            this.Hide();
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n = 0;
            if(textBox1.Text==""|| Convert.ToInt32(textBox1.Text)>x)
            {
                MessageBox.Show("No Enough Stock Please Check Available Stock");
            }
            else
            {


               int total = Convert.ToInt32(textBox1.Text) * unitp;
               DataGridViewRow newRow = new DataGridViewRow();
               newRow.CreateCells(dataGridView1);
               newRow.Cells[0].Value = n + 1;
               newRow.Cells[1].Value = comboBox1.SelectedValue.ToString();
               newRow.Cells[2].Value = textBox1.Text;
               newRow.Cells[3].Value = unitp;
               newRow.Cells[4].Value = unitp * Convert.ToInt32(textBox1.Text);
               dataGridView1.Rows.Add(newRow);
               grdtotal = grdtotal + total;
               label3.Text = "Rs"+grdtotal;
                textBox1.Clear();
                populatecombobox();
            }
           

        }
    }
}
