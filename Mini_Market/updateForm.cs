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

namespace Mini_Market
{
    public partial class updateForm : Form
    {
        public updateForm()
        {
            InitializeComponent();
        }

        private void updateForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'superMarketDataSet3.customer' table. You can move, or remove it, as needed.
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Button_showdata_Click(object sender, EventArgs e)
        {


            SqlConnection con = new SqlConnection(@"Data Source=KHALED;Initial Catalog=SuperMarket;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Customer where UserName='" + TextBox_username.Text + "' and password='" + TextBox_password.Text + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                guna2DataGridView1.DataSource = dt;
            }
            else
            {
                MessageBox.Show("No Data Found");
            }
            con.Close();
        }

        //this.customerTableAdapter.Fill(this.superMarketDataSet.customer);


        private void Button_update_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=KHALED;Initial Catalog=SuperMarket;Integrated Security=True");

                con.Open();

                SqlCommand cmd = new SqlCommand("select * from Customer where UserName='" + TextBox_username.Text + "' and password='" + TextBox_password.Text + "'", con);
                cmd.CommandText="update customer set f_name='" + guna2DataGridView1.Rows[0].Cells[1].Value.ToString() + "',l_name='" + guna2DataGridView1.Rows[0].Cells[2].Value.ToString() + "',UserName='" + guna2DataGridView1.Rows[0].Cells[3].Value.ToString() + "',password='" + guna2DataGridView1.Rows[0].Cells[4].Value.ToString() + "',address='" + guna2DataGridView1.Rows[0].Cells[5].Value.ToString() + "',phone='" + guna2DataGridView1.Rows[0].Cells[6].Value.ToString() + "',email='" + guna2DataGridView1.Rows[0].Cells[7].Value.ToString() + "' where UserName = '" + TextBox_username.Text + "' and password = '" + TextBox_password.Text + "'";
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Data Updated");
                Button_showdata_Click(sender, e);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_delete_Click(object sender, EventArgs e)
        {
            //delete user from database 
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=KHALED;Initial Catalog=SuperMarket;Integrated Security=True");

                con.Open();
                SqlCommand cmd = new SqlCommand("select * from Customer where UserName='" + TextBox_username.Text + "' and password='" + TextBox_password.Text + "'", con);
                cmd.CommandText = "delete from customer where UserName = '" + TextBox_username.Text + "' and password = '" + TextBox_password.Text + "'";
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show(TextBox_username.Text +" Deleted");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
