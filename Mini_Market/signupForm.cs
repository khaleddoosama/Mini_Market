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
using DocumentFormat.OpenXml.Drawing.Diagrams;

namespace Mini_Market
{
    public partial class signupForm : Form
    {
        public signupForm()
        {
            InitializeComponent();
        }

        private void signupForm_Load(object sender, EventArgs e)
        {
            
        }

        private void Button_signup_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextBox_password.Text == TextBox_copassword.Text)
                {
                    string username = TextBox_username.Text;
                    string password = TextBox_password.Text;
                    string email = TextBox_email.Text;
                    string phone = TextBox_phone.Text;
                    string address = TextBox_address.Text;
                    string firstname = TextBox_firstname.Text;
                    string lastname = TextBox_lastname.Text;
                    string signup_date = DateTime.Now.ToString();

                    //add new user to database
                    SqlConnection con = new SqlConnection(@"Data Source=KHALED;Initial Catalog=SuperMarket;Integrated Security=True");
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into customer(f_name,l_name,UserName,password,address,phone,email,sign_up_date) values('" + firstname + "','" + lastname + "','" + username + "','" + password + "','" + address + "','" + phone + "','" + email +"','"+signup_date +"')", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Signup Successful");
                    con.Close();
                    LoginForm login = new LoginForm();
                    login.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Password and Confirm Password do not match!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
