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



namespace Mini_Market
{
    public partial class adminForm : Form
    {
        public adminForm()
        {
            InitializeComponent();
        }

        private void adminForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'superMarketDataSet3.frequent_cutomers_' table. You can move, or remove it, as needed.
            this.frequent_cutomers_TableAdapter.Fill(this.superMarketDataSet3.frequent_cutomers_);
            // TODO: This line of code loads data into the 'superMarketDataSet3.orders' table. You can move, or remove it, as needed.
            this.ordersTableAdapter.Fill(this.superMarketDataSet3.orders);
            // TODO: This line of code loads data into the 'superMarketDataSet3.products' table. You can move, or remove it, as needed.
            SqlConnection con = new SqlConnection(@"Data Source=KHALED;Initial Catalog=SuperMarket;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from products", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            guna2DataGridView1.DataSource = dt;
            TextBox_id.Hide();
            ComboBox_show.SelectedIndex = 0;

            guna2DataGridView1.Show();
            DataGridView_orders.Hide();
            guna2DataGridView_frequent.Hide();
            string adminuserName = LoginForm.adminuserName;
            label_admin.Text = "Welcome " + adminuserName;
            int adminid = LoginForm.adminId;
            

        }

        private void Button_add_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=KHALED;Initial Catalog=SuperMarket;Integrated Security=True");
                con.Open();
                if (TextBox_name.Text == "" || TextBox_price.Text == "" || TextBox_quantity.Text == ""||TextBox_restocked.Text==""||ComboBox.Text=="")
                {
                    MessageBox.Show("Please fill all the fields");
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("insert into products(pr_name,price,quantity,re_stocked_product,p_category) values('" + TextBox_name.Text + "','" + TextBox_price.Text + "','" + TextBox_quantity.Text + "','" + TextBox_restocked.Text +"','"+ComboBox.Text +"')", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product added successfully");
                    TextBox_name.Text = "";
                    TextBox_price.Text = "";
                    TextBox_quantity.Text = "";
                    TextBox_restocked.Text = "";
                    ComboBox.Text = "";
                    this.productsTableAdapter3.Fill(this.superMarketDataSet3.products);

                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void Button_signup_Click(object sender, EventArgs e)
        {
            signupForm signup = new signupForm();
            signup.Show();
            this.Hide();
        }

        private void Button_updatedelete_Click(object sender, EventArgs e)
        {
            updateForm update = new updateForm();
            update.Show();
            this.Hide();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Button_update_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=KHALED;Initial Catalog=SuperMarket;Integrated Security=True");
                TextBox_id.Text = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
                con.Open();

                SqlCommand cmd = new SqlCommand("select * from products ", con);

               

                if (TextBox_name.Text == "" || TextBox_price.Text == "" || TextBox_quantity.Text == "" || TextBox_restocked.Text == "" ||ComboBox.Text == "")
                {
                    TextBox_name.Text = guna2DataGridView1.CurrentRow.Cells[1].Value.ToString();
                    TextBox_price.Text = guna2DataGridView1.CurrentRow.Cells[2].Value.ToString();
                    TextBox_quantity.Text = guna2DataGridView1.CurrentRow.Cells[3].Value.ToString();
                    TextBox_restocked.Text = guna2DataGridView1.CurrentRow.Cells[4].Value.ToString();
                    MessageBox.Show("Please fill all the fields");
                }
                else
                {
                    cmd.CommandText = "update products set pr_name='" + TextBox_name.Text  + "',price='" + TextBox_price.Text + "',quantity='" + TextBox_quantity.Text + "',re_stocked_product='" + TextBox_restocked.Text + "',p_category='" + ComboBox.Text + "' where pro_id='" + TextBox_id.Text + "'";
                    cmd.ExecuteNonQuery();
                    adminForm_Load(sender, e);
                    TextBox_name.Text = "";
                    TextBox_price.Text = "";
                    TextBox_quantity.Text = "";
                    TextBox_restocked.Text = "";
                    ComboBox.Text = "";
                    con.Close();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_delete_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=KHALED;Initial Catalog=SuperMarket;Integrated Security=True");
                TextBox_id.Text = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
                con.Open();

                SqlCommand cmd = new SqlCommand("select * from products ", con);



           

                DialogResult result = MessageBox.Show("Are you sure you want to delete this product(id="+TextBox_id.Text +")?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    cmd.CommandText = "delete from products where pro_id='" + TextBox_id.Text + "'";
                    cmd.ExecuteNonQuery();
                    TextBox_name.Text = "";
                    TextBox_price.Text = "";
                    TextBox_quantity.Text = "";
                    TextBox_restocked.Text = "";
                    ComboBox.Text = "";
                    con.Close();
                    this.productsTableAdapter3.Fill(this.superMarketDataSet3.products);
                }
                else
                {
                    MessageBox.Show("Data not Deleted");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextBox_restocked_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox_search_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=KHALED;Initial Catalog=SuperMarket;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from products where pr_name like '%" + TextBox_search.Text + "%'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                guna2DataGridView1.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }

        private void label_logout_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Hide();
        }

        private void label_logout_MouseEnter(object sender, EventArgs e)
        {
            label_logout.ForeColor = Color.Red;
        }

        private void label_logout_MouseLeave(object sender, EventArgs e)
        {
            label_logout.ForeColor = Color.Blue;
        }

        private void ComboBox_show_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=KHALED;Initial Catalog=SuperMarket;Integrated Security=True");
            con.Open();
            if(ComboBox_show.Text=="All Products")
            {
                adminForm_Load(sender, e);
            }
            else if (ComboBox_show.Text=="Available Products")
            {
                SqlCommand cmd = new SqlCommand("select * from products where quantity>0", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                guna2DataGridView1.DataSource = dt;
            }
            else if (ComboBox_show.Text == "Products re-stocked")
            {
                SqlCommand cmd = new SqlCommand("select * from products where quantity<re_stocked_product", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                guna2DataGridView1.DataSource = dt;
            }
            con.Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            DataGridView_orders.Show();
            guna2DataGridView1.Hide();
            guna2DataGridView_frequent.Hide();
            SqlConnection con = new SqlConnection(@"Data Source=KHALED;Initial Catalog=SuperMarket;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from orders where ad_id = "+LoginForm.adminId, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DataGridView_orders.DataSource = dt;
            con.Close();
        }

        private void Button_frequent_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridView_orders.Hide();
                guna2DataGridView1.Hide();
                guna2DataGridView_frequent.Show();
                SqlConnection con = new SqlConnection(@"Data Source=KHALED;Initial Catalog=SuperMarket;Integrated Security=True");
                con.Open();
                //select custommers who order more than 3 products and send their id to table frequent_customers  
                SqlCommand cmd = new SqlCommand("select top 10 customer.cu_id,COUNT(or_id)from customer, orders where customer.cu_id = orders.cu_id group by customer.cu_id,customer.f_name,customer.l_name order by count(or_id) desc", con);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                //insert into frequent_customers_ data in guna2datadatagridview_frequent  
                for (int i = 0; i < (guna2DataGridView_frequent.Rows.Count)-1; i++)
                {
                    //check if the customer id is already in the table frequent_customers_ 
                    SqlCommand cmd1 = new SqlCommand("select * from frequent_cutomers_ where cu_id = " + guna2DataGridView_frequent.Rows[i].Cells[0].Value.ToString(), con);
                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    sda1.Fill(dt1);
                    if (dt1.Rows.Count == 0)
                    {
                        
                        SqlCommand cmd2 = new SqlCommand("insert into frequent_cutomers_ values(" + guna2DataGridView_frequent.Rows[i].Cells[0].Value.ToString() + ",'" + 10 + "')", con);
                        cmd2.ExecuteNonQuery();
                    }
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void guna2DataGridView_frequent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
