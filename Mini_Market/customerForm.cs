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
    public partial class customerForm : Form
    {
        public customerForm()
        {
            InitializeComponent();
        }

        private void customerForm_Load(object sender, EventArgs e)
        {
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

            string customeruserName = LoginForm.customeruserName;
            label_username.Text = customeruserName;

            ComboBox_show.SelectedIndex = 0;
            guna2DataGridView1.Show();
            guna2DataGridView_orders.Hide();
            Button_delete.Hide();
            TextBox_price.ReadOnly = true;

        }

        private void Button_order_Click(object sender, EventArgs e)
        {
            try
            {
                int customerId = LoginForm.customerId;

                SqlConnection con = new SqlConnection(@"Data Source=KHALED;Initial Catalog=SuperMarket;Integrated Security=True");
                con.Open();
                TextBox_id.Text = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
                TextBox_name.Text = guna2DataGridView1.CurrentRow.Cells[1].Value.ToString();
                TextBox_price.Text = guna2DataGridView1.CurrentRow.Cells[2].Value.ToString();

                if (TextBox_quantity.Text == "")
                {
                    MessageBox.Show("Please Enter Quantity");
                }
                else
                {
                    //check if customer in frequent_cutomer_ table or not 
                    SqlCommand cmd0 = new SqlCommand("select * from frequent_cutomers_ where cu_id = '" + customerId + "'", con);
                    SqlDataAdapter da0 = new SqlDataAdapter(cmd0);
                    DataTable dt0 = new DataTable();
                    da0.Fill(dt0);
                   
                    int quantity = Convert.ToInt32(TextBox_quantity.Text);
                    int price = Convert.ToInt32(TextBox_price.Text);
                    double total = quantity * price;
                    if (dt0.Rows.Count != 0)
                    {
                        total =total - Convert.ToDouble(quantity * price*Convert.ToInt32(dt0.Rows[0]["discount"])/100);

                    }
                    DialogResult result = MessageBox.Show("Are you sure you want to order this product for"+total+"$?", "Confirmation", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        DateTime date = DateTime.Now;

                        SqlCommand cmd = new SqlCommand("select * from products where pro_id = '" + TextBox_id.Text + "'", con);
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            int quantityInDatabase = Convert.ToInt32(dr["quantity"].ToString());
                            if (quantityInDatabase >= quantity)
                            {
                                int newQuantity = quantityInDatabase - quantity;
                                SqlCommand cmd2 = new SqlCommand("update products set quantity = '" + newQuantity + "' where pro_id = '" + TextBox_id.Text + "'", con);
                                dr.Close();
                                cmd2.ExecuteNonQuery();
                                SqlCommand cmd3 = new SqlCommand("insert into orders values('" +TextBox_id.Text +"','"+customerId +"','"+ 1 +"','"+date+"','"+ quantity + "','" + price+"','"+total + "')", con);
                                cmd3.ExecuteNonQuery();


                                SqlDataAdapter da = new SqlDataAdapter("select * from orders where pr_id = '" + TextBox_id.Text + "'", con);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                MessageBox.Show("Your order id is: "+dt.Rows[0][0].ToString());
                            }
                            else
                            {
                                MessageBox.Show("The quantity is not enough");
                            }
                        }


                    }
                    else
                    {
                        MessageBox.Show("Order Canceled");
                    }
                }
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


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

        private void ComboBox_show_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=KHALED;Initial Catalog=SuperMarket;Integrated Security=True");
            con.Open();
            if (ComboBox_show.Text=="All Products")
            {
                customerForm_Load(sender, e);
            }
            else if (ComboBox_show.Text=="Available Products")
            {
                SqlCommand cmd = new SqlCommand("select * from products where quantity>0", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                guna2DataGridView1.DataSource = dt;
            }
            con.Close();
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

        private void Button_myorders_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=KHALED;Initial Catalog=SuperMarket;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from orders where cu_id = '" + LoginForm.customerId + "'", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            guna2DataGridView_orders.DataSource = dt;
            guna2DataGridView_orders.Show();
            Button_delete.Show();
            guna2DataGridView1.Hide();
            con.Close();
        }

        private void Button_delete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=KHALED;Initial Catalog=SuperMarket;Integrated Security=True");
            con.Open();
            DialogResult result = MessageBox.Show("Are you sure you want to delete this order(id="+guna2DataGridView_orders.CurrentRow.Cells[0].Value.ToString()+")?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                SqlCommand cmd = new SqlCommand("select * from products where pro_id = '" + guna2DataGridView_orders.CurrentRow.Cells[1].Value.ToString() + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    int quantityInDatabase = Convert.ToInt32(dr["quantity"].ToString());
                    int quantity = Convert.ToInt32(guna2DataGridView_orders.CurrentRow.Cells[4].Value.ToString());
                    int newQuantity = quantityInDatabase + quantity;
                    SqlCommand cmd2 = new SqlCommand("update products set quantity = '" + newQuantity + "' where pro_id = '" + guna2DataGridView_orders.CurrentRow.Cells[1].Value.ToString() + "'", con);
                    dr.Close();
                    cmd2.ExecuteNonQuery();
                }
                SqlCommand cmd3 = new SqlCommand("delete from orders where or_id = '" + guna2DataGridView_orders.CurrentRow.Cells[0].Value.ToString() + "'", con);
                cmd3.ExecuteNonQuery();
                MessageBox.Show("Order Deleted");
                Button_myorders_Click(sender, e);

            }
            else
            {
                MessageBox.Show("Order Canceled");
            }

        }

        private void guna2DataGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int customerId = LoginForm.customerId;

                SqlConnection con = new SqlConnection(@"Data Source=KHALED;Initial Catalog=SuperMarket;Integrated Security=True");
                con.Open();
                TextBox_id.Text = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
                TextBox_name.Text = guna2DataGridView1.CurrentRow.Cells[1].Value.ToString();
                TextBox_price.Text = guna2DataGridView1.CurrentRow.Cells[2].Value.ToString();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextBox_price_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

}
