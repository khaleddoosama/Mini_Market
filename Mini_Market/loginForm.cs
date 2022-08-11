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
    public partial class LoginForm : Form
    {
        internal static int customerId;
        internal static string customeruserName;
        internal static int adminId;
        internal static string adminuserName;




        public LoginForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        //private void guna2Button1_Click(object sender, EventArgs e)
        //{

        //}

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label_signup_MouseEnter(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void Button_signup_Click(object sender, EventArgs e)
        {
            signupForm signup = new signupForm();
            signup.Show();
            this.Hide();
        }

        private void Button_login_Click(object sender, EventArgs e)
        {

            try
            {
                if (comboBox1.Text=="Admin")
                {

                    SqlConnection con = new SqlConnection(@"Data Source=KHALED;Initial Catalog=SuperMarket;Integrated Security=True");
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from Admin where UserName='" + TextBox_username.Text + "' and password='" + TextBox_password.Text + "'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        adminId = Convert.ToInt32(dt.Rows[0][0]);
                        adminuserName = dt.Rows[0][3].ToString();
                        adminForm admin = new adminForm();
                        admin.Show();
                        this.Hide();
                    }
                    else
                    {
                        //if not exist then show error message
                        MessageBox.Show("Username or Password is incorrect");
                    }
                }
                else if (comboBox1.Text == "Customer")
                {

                    SqlConnection con = new SqlConnection(@"Data Source=KHALED;Initial Catalog=SuperMarket;Integrated Security=True");
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from Customer where UserName='" + TextBox_username.Text + "' and password='" + TextBox_password.Text + "'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0) { 
                        customerId = Convert.ToInt32(dt.Rows[0][0]);
                        customeruserName = dt.Rows[0][3].ToString();
                        //if exist then go to customer form
                        customerForm customer = new customerForm();
                        customer.Show();
                        this.Hide();
                    }
                    else
                    {
                        //if not exist then show error message
                        MessageBox.Show("Username or Password is incorrect");
                    }
                }
                else
                {
                    MessageBox.Show("Please select user type");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

  
        private void Button_update_Click(object sender, EventArgs e)
        {
            updateForm update = new updateForm();
            update.Show();
            this.Hide();
        }

        private void Button_delete_Click(object sender, EventArgs e)
        {

        }

        private void label_signup_Click(object sender, EventArgs e)
        {
            signupForm signup = new signupForm();
            signup.Show();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
