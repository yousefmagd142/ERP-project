using abo_elmagd.almagd_dbDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace abo_elmagd
{

    public partial class Databaseform : Form

    {
        public static String sql = "Data Source=yousef\\sqlexpress;Initial Catalog=almagd_db;Integrated Security=True;TrustServerCertificate=True";
        public SqlConnection con = new SqlConnection(sql);
        
        public Databaseform()
        {

            InitializeComponent();
            // Calculate the X coordinate to position the form slightly to the right
            int x = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2 + 100; // Adjust 100 to your preference
            int y = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(x, y);
            sea(comboBox1, "select name from suppliers");
            sea(textBox8, "select name from item_table");
            sea(comboBox2, "select name from cust_table"); 
            sea(textBox2, "select name from item_table");

        }

        public void gridview_date(string query,DataGridView data)
        {
            SqlCommand cmd = new SqlCommand(""+query+"", con);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            data.DataSource = dt;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 5) // Replace 1 with the index of the tab you want to protect
            {
                string enteredPassword = PromptForPassword(); // Implement a method to prompt for password

                if (enteredPassword != "aboelmagd") // Replace "your_password" with the actual password
                {
                    MessageBox.Show("Incorrect password. Access denied.");
                    tabControl1.SelectedIndex = 0; // Replace 0 with the index of the default tab
                }
            }

        }

        private string PromptForPassword()
        {
            Form prompt = new Form();
            prompt.StartPosition = FormStartPosition.CenterScreen; // Set the dialog to appear in the middle of the screen
            prompt.Width = 300;
            prompt.Height = 150;
            prompt.Text = "Enter Password";

            Label textLabel = new Label() { Left = 50, Top = 20, Text = "Enter the password:" };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 200 };
            textBox.PasswordChar = '*'; // Para ocultar la contraseña

            Button confirmation = new Button() { Text = "OK", Left = 150, Width = 100, Top = 80 };
            confirmation.Click += (sender, e) => { prompt.Close(); };

            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.AcceptButton = confirmation;
            textBox.Focus();
            textBox.Select();
            textBox.SelectAll();
            prompt.ShowDialog();


            return textBox.Text;
        }

        public void sea(ComboBox com, string sta)
        {
            com.Items.Clear();
            SqlCommand cmd = new SqlCommand(sta, con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                com.Items.Add(ds.Tables[0].Rows[i][0].ToString());
        }
 
        private void add_customer_Click(object sender, EventArgs e)
        {
            if (add_customer.Text == "")
            {
                MessageBox.Show("يجب اضافه عميل");
            }
            else
            {
                con.Open();
                string insert2 = "INSERT INTO cust_table (name,phone_number) VALUES(@name,@phone_number)";
                SqlCommand cmd2 = new SqlCommand(insert2, con);
                cmd2.Parameters.AddWithValue("@name", add_customerdb.Text);
                if (phone_numberdb.Text == "")
                {
                    MessageBox.Show("للعلم لايوجد رقم تليفون");
                    cmd2.Parameters.AddWithValue("@phone_number", phone_numberdb.Text = "null");
                }
                else { cmd2.Parameters.AddWithValue("@phone_number", phone_numberdb.Text); }
                cmd2.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("تم اضافة العميل بنجاح");
                add_customerdb.Text = "";
                phone_numberdb.Text = "";
                cust_id.Text = "";
                custrest.Text = "";
            }
            gridview_date("select * from  cust_table", dataGridView2);
        }
        private void Databaseform_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'almagd_dbDataSet.item_table' table. You can move, or remove it, as needed.
            this.item_tableTableAdapter.Fill(this.almagd_dbDataSet.item_table);
            // TODO: This line of code loads data into the 'almagd_dbDataSet.inventory' table. You can move, or remove it, as needed.
            this.inventoryTableAdapter.Fill(this.almagd_dbDataSet.inventory);
            // TODO: This line of code loads data into the 'almagd_dbDataSet.inventory' table. You can move, or remove it, as needed.
            this.inventoryTableAdapter.Fill(this.almagd_dbDataSet.inventory);
            // TODO: This line of code loads data into the 'almagd_dbDataSet.item_table' table. You can move, or remove it, as needed.
            this.item_tableTableAdapter.Fill(this.almagd_dbDataSet.item_table);
            // TODO: This line of code loads data into the 'almagd_dbDataSet.importation' table. You can move, or remove it, as needed.
            // TODO: This line of code loads data into the 'almagd_dbDataSet.Expenses' table. You can move, or remove it, as needed.
            this.expensesTableAdapter.Fill(this.almagd_dbDataSet.Expenses);
            // TODO: This line of code loads data into the 'almagd_dbDataSet.suppliers' table. You can move, or remove it, as needed.
            this.suppliersTableAdapter.Fill(this.almagd_dbDataSet.suppliers);
            // TODO: This line of code loads data into the 'almagd_dbDataSet.cust_table' table. You can move, or remove it, as needed.
            this.cust_tableTableAdapter.Fill(this.almagd_dbDataSet.cust_table);
            // TODO: This line of code loads data into the 'almagd_dbDataSet.inv_details' table. You can move, or remove it, as needed.
            this.inv_detailsTableAdapter.Fill(this.almagd_dbDataSet.inv_details);
            // TODO: This line of code loads data into the 'almagd_dbDataSet.inv_details' table. You can move, or remove it, as needed.
            this.inv_detailsTableAdapter.Fill(this.almagd_dbDataSet.inv_details);
        }

        private void custdeletbtn_Click(object sender, EventArgs e)
        {
            con.Open();
            string name = cust_id.Text;
            string query = "delete from cust_table where id ='" + name + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            cust_id.Text = "";
            add_customerdb.Text = "";
            phone_numberdb.Text = "";
            custrest.Text = "";
            con.Close();
            MessageBox.Show("deleted");
            gridview_date("select * from  cust_table", dataGridView2);

        }

        private void updatcustbtn_Click(object sender, EventArgs e)
        {
            con.Open();
            int id = int.Parse(cust_id.Text);
            string name = add_customerdb.Text;
            string phone = phone_numberdb.Text;
            decimal rest = decimal.Parse( custrest.Text);

            string query = "update cust_table set  phone_number='" + phone + "',name='"+name+"',rest='"+rest+"'  where id='" + id + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            add_customerdb.Text = "";
            phone_numberdb.Text = "";
            cust_id.Text = "";
            custrest.Text = "";
            MessageBox.Show("Updated");
            con.Close();
            gridview_date("select * from  cust_table", dataGridView2);

        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index;
            index = e.RowIndex;
            DataGridViewRow selectedrow = dataGridView2.Rows[index];
            cust_id.Text = selectedrow.Cells[0].Value.ToString();
            add_customerdb.Text = selectedrow.Cells[1].Value.ToString();
            phone_numberdb.Text = selectedrow.Cells[2].Value.ToString();
            custrest.Text = selectedrow.Cells[3].Value.ToString();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();

            string start = dateTimePicker1.Text;
            string end = dateTimePicker2.Text;
            string query = "SELECT * FROM inv_details WHERE date >= '" + start + "' AND date <= '" + end + "'";
            SqlCommand cmd = new SqlCommand(query,con);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            dataGridView3.DataSource = dt;
            con.Close();

            int columnIndex = 4; // Assuming the column index you want to sum is 2 (change it to the actual column index)
            decimal sum = 0;

            for (int i = 0; i < dataGridView3.Rows.Count; i++)
            {
                if (dataGridView3.Rows[i].Cells[columnIndex].Value != null)
                {
                    decimal cellValue;
                    if (decimal.TryParse(dataGridView3.Rows[i].Cells[columnIndex].Value.ToString(), out cellValue))
                    {
                        sum += cellValue;
                    }
                }
            }
            textBox1.Text = sum.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();

            string start = textBox2.Text;
            string query = "SELECT * FROM inv_details WHERE item = '" + start + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            dataGridView3.DataSource = dt;
            con.Close();

            int columnIndex = 4; // Assuming the column index you want to sum is 2 (change it to the actual column index)
            decimal sum = 0;

            for (int i = 0; i < dataGridView3.Rows.Count; i++)
            {
                if (dataGridView3.Rows[i].Cells[columnIndex].Value != null)
                {
                    decimal cellValue;
                    if (decimal.TryParse(dataGridView3.Rows[i].Cells[columnIndex].Value.ToString(), out cellValue))
                    {
                        sum += cellValue;
                    }
                }
            }
            textBox3.Text = sum.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            //المبيعات
            con.Open();
            string start = dateTimePicker5.Text;
            string end =  dateTimePicker6.Text;
            string qqquery = "SELECT total FROM inv_details where date >='"+start+"' and date<='"+end+"'";
            SqlCommand cmdff = new SqlCommand(qqquery, con);
            SqlDataAdapter adapterr = new SqlDataAdapter(cmdff);
            DataTable dataTablee = new DataTable();
            adapterr.Fill(dataTablee);

            decimal sumson = 0;
            foreach (DataRow row in dataTablee.Rows)
            {
                decimal value1 = row["total"] != DBNull.Value ? Convert.ToDecimal(row["total"]) : 0;

                sumson += value1 ;
            }

            con.Close();
            
            //sumson = sumson - sume - mopaid;
            salestxt.Text = sumson.ToString();


            //الفلوس ال برا
            int columnIndexx = 3; // Assuming the column index you want to sum is 2 (change it to the actual column index)
            decimal summ = 0;

            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (dataGridView2.Rows[i].Cells[columnIndexx].Value != null)
                {
                    decimal cellValue;
                    if (decimal.TryParse(dataGridView2.Rows[i].Cells[columnIndexx].Value.ToString(), out cellValue))
                    {
                        summ += cellValue;
                    }
                }
            }
            moneyouttxt.Text = summ.ToString();



            //البضاعه الموجوده

            con.Open();
            string qquery = "SELECT quantty, entry_price FROM item_table";
            SqlCommand cmdf = new SqlCommand(qquery, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmdf);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            decimal totalMultiplicationSum = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                decimal value1 = row["quantty"] != DBNull.Value ? Convert.ToDecimal(row["quantty"]) : 0;
                decimal value2 = row["entry_price"] != DBNull.Value ? Convert.ToDecimal(row["entry_price"]) : 0;

                decimal multiplicationResult = value1 * value2;
                totalMultiplicationSum += multiplicationResult;
            }

            con.Close();
            producttxt.Text = totalMultiplicationSum.ToString();
        }



        private void button9_Click(object sender, EventArgs e)
        {
            if (add_customer.Text == "")
            {
                MessageBox.Show("يجب اضافه عميل");

            }
            else
            {
                con.Open();
                string insert = "INSERT INTO suppliers (name,phone_number) VALUES(@name,@phone_number)";
                SqlCommand cmd = new SqlCommand(insert, con);
                cmd.Parameters.AddWithValue("@name", sup_name.Text);


                if (sup_phone.Text == "")
                {
                    MessageBox.Show("للعلم لايوجد رقم تليفون");
                    cmd.Parameters.AddWithValue("@phone_number", sup_phone.Text = "null");
                }
                else { cmd.Parameters.AddWithValue("@phone_number", sup_phone.Text); }

                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("تم اضافة المورد بنجاح");
                sup_name.Text = "";
                sup_phone.Text = "";
                cust_id.Text = "";
                sup_id.Text = "";
            }
            gridview_date("select * from  suppliers", supGridView);
        }

        private void phonee_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            int id = int.Parse(sup_id.Text);
            string name = sup_name.Text;
            string phone = sup_phone.Text;
            decimal debt = decimal.Parse(supdebt.Text);

            string query = "update suppliers set  phone_number='" + phone + "',name='" + name + "',debt='" + debt + "'  where id='" + id + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            sup_name.Text = "";
            sup_phone.Text = "";
            sup_id.Text = "";
            supdebt.Text = "";
            MessageBox.Show("Updated");
            con.Close();
            gridview_date("select * from  suppliers", supGridView);
        }

        private void supGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index;
            index = e.RowIndex;
            DataGridViewRow selectedrow = supGridView.Rows[index];
            sup_id.Text = selectedrow.Cells[0].Value.ToString();
            sup_name.Text = selectedrow.Cells[1].Value.ToString();
            sup_phone.Text = selectedrow.Cells[2].Value.ToString();
            supdebt.Text = selectedrow.Cells[3].Value.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            con.Open();
            string name = sup_id.Text;
            string query = "delete from suppliers where id ='" + name + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            sup_id.Text = "";
            sup_name.Text = "";
            sup_phone.Text = "";
            supdebt.Text = "";
            con.Close();
            MessageBox.Show("deleted");
            gridview_date("select * from  suppliers", supGridView);
        }

        
        private void button11_Click(object sender, EventArgs e)
        {
            //البضاعه الوارد
            con.Open();
            string start = dateTimePicker5.Text;
            string end = dateTimePicker6.Text;
            string qquery = "SELECT quantty, entry_price FROM importation where date>='"+start+"' and date <='"+end+"'";
            SqlCommand cmdf = new SqlCommand(qquery, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmdf);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // Calculate the sum of the multiplication of two columns for each row
            decimal totalMultiplicationSum = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                // Assuming the columns to be multiplied are named "Column1" and "Column2"
                decimal value1 = Convert.ToDecimal(row["quantty"]);
                decimal value2 = Convert.ToDecimal(row["entry_price"]);

                decimal multiplicationResult = value1 * value2;
                totalMultiplicationSum += multiplicationResult;
            }
            importationtxt.Text = totalMultiplicationSum.ToString();
            con.Close();



            //التزامات
            int columnIndex = 3; 
            decimal sum = 0;

            for (int i = 0; i < supGridView.Rows.Count; i++)
            {
                if (supGridView.Rows[i].Cells[columnIndex].Value != null)
                {
                    decimal cellValue;
                    if (decimal.TryParse(supGridView.Rows[i].Cells[columnIndex].Value.ToString(), out cellValue))
                    {
                        sum += cellValue;
                    }
                }
            }
            monyon.Text = sum.ToString();



            //المصرزفات
            con.Open();
            string st = dateTimePicker5.Text;
            string en = dateTimePicker6.Text;
            string uery = "SELECT price FROM expenses where date >='" + st + "' and date<='" + en + "'";
            SqlCommand cm = new SqlCommand(uery, con);
            SqlDataAdapter adap = new SqlDataAdapter(cm);
            DataTable dataTab = new DataTable();
            adap.Fill(dataTab);

            decimal s = 0;
            foreach (DataRow row in dataTab.Rows)
            {
                decimal value1 = row["price"] != DBNull.Value ? Convert.ToDecimal(row["price"]) : 0;

                s += value1;
            }
            extxt.Text = s.ToString();
            con.Close();
            
        }

        private void button14_Click(object sender, EventArgs e)
        {
            con.Open();
            string insert = "INSERT INTO expenses (name,price, date) VALUES(@name,@price,@date)";
            SqlCommand cmd = new SqlCommand(insert, con);
            cmd.Parameters.AddWithValue("@name", name.Text);
            cmd.Parameters.AddWithValue("@price", price.Text);
            cmd.Parameters.AddWithValue("@date", date.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("تم اضافة العملية بنجاح");
            name.Text = "";
            price.Text = "";
            date.Text = "";
            idi.Text = "";
            gridview_date("select * from  expenses", expensesGridView);

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index;
            index = e.RowIndex;
            DataGridViewRow selectedrow = expensesGridView.Rows[index];
            idi.Text = selectedrow.Cells[0].Value.ToString();
            name.Text = selectedrow.Cells[1].Value.ToString();
            price.Text = selectedrow.Cells[2].Value.ToString();
            date.Text = selectedrow.Cells[3].Value.ToString();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            con.Open();
            int id = int.Parse(idi.Text);
            string namee = name.Text;
            string pricee = price.Text;
            string datee = date.Text;

            string query = "update expenses set  name='" + namee + "',price='" + pricee + "',date='" + datee + "'  where id='" + id + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            name.Text = "";
            price.Text = "";
            idi.Text = "";
            MessageBox.Show("Updated");
            con.Close();
            gridview_date("select * from  expenses", expensesGridView);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            con.Open();
            string namee = idi.Text;
            string query = "delete from expenses where id ='" + namee + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            idi.Text = "";
            name.Text = "";
            price.Text = "";
            con.Close();
            MessageBox.Show("deleted");
            gridview_date("select * from  expenses", expensesGridView);
        }

        

        private void button16_Click(object sender, EventArgs e)
        {
            con.Open();
            string start = textBox8.Text;
            string query = "SELECT * FROM item_table WHERE name = '" + start + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            itemGridView.DataSource = dt;
            con.Close();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            con.Open();
            string start = textBox9.Text;
            string query = "SELECT * FROM item_table WHERE id = '" + start + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            itemGridView.DataSource = dt;
            con.Close();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            con.Open();
            string name = textBox11.Text;
            int quantty=int.Parse(textBox12.Text);
            int id = int.Parse(textBox10.Text);
            string query = "update item_table set name='"+name+"',quantty='"+quantty+"'where id='"+id+"'";
            SqlCommand cmd=new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("تم التعديل");
            gridview_date("select * from  item_table", itemGridView);


        }

        private void itemGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index;
            index = e.RowIndex;
            DataGridViewRow selectedrow = itemGridView.Rows[index];
            textBox10.Text = selectedrow.Cells[0].Value.ToString();
            textBox11.Text = selectedrow.Cells[1].Value.ToString();
            textBox12.Text = selectedrow.Cells[2].Value.ToString();
            
        }

        private void button19_Click(object sender, EventArgs e)
        {
            con.Open();
            int id=int.Parse(textBox10.Text);
            string q = "delete from item_table where id='"+id+"'";
            SqlCommand cmd= new SqlCommand(q, con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("done");
            gridview_date("select * from  item_table", itemGridView);

        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (textBox11.Text == "")
            {
                MessageBox.Show("يجب اضافة منتج");
            }
            else
            {
                con.Open();
                string insert = "INSERT INTO item_table (name) VALUES(@name)";
                SqlCommand cmd = new SqlCommand(insert, con);
                cmd.Parameters.AddWithValue("@name", textBox11.Text);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            gridview_date("select * from  item_table", itemGridView);


        }

        
        

        private void button5_Click(object sender, EventArgs e)
        {
            con.Open();

            string start = dateTimePicker3.Text;
            string end = dateTimePicker4.Text;
            string query = "SELECT * FROM expenses WHERE date >= '" + start + "' AND date <= '" + end + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            expensesGridView.DataSource = dt;
            con.Close();

            int columnIndex = 2; // Assuming the column index you want to sum is 2 (change it to the actual column index)
            decimal sum = 0;

            for (int i = 0; i < expensesGridView.Rows.Count; i++)
            {
                if (expensesGridView.Rows[i].Cells[columnIndex].Value != null)
                {
                    decimal cellValue;
                    if (decimal.TryParse(expensesGridView.Rows[i].Cells[columnIndex].Value.ToString(), out cellValue))
                    {
                        sum += cellValue;
                    }
                }
            }
            textBox13.Text = sum.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            con.Open();
            String text =comboBox1.Text;
            string query = "select * from suppliers where name='" + text + "'";
            SqlCommand cmd= new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            supGridView.DataSource = dt;
            con.Close( );
        }

        private void button8_Click(object sender, EventArgs e)
        {
            con.Open( );
            String text =comboBox2.Text;
            String query = "select * from cust_table where name='" + text + "'";
            SqlCommand command= new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = command;
            DataTable dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            con.Close( );
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int columnIndex = 9;
            decimal sum = 0;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[columnIndex].Value != null)
                {
                    decimal cellValue;
                    if (decimal.TryParse(dataGridView1.Rows[i].Cells[columnIndex].Value.ToString(), out cellValue))
                    {
                        sum += cellValue;
                    }
                }
            }
            con.Open();
            string query = "insert into inventory(total_sales,products,money_out,importation,obligations," +
                "expenses,date_from,date_to,cash) values(@total_sales,@products,@money_out,@importation,@obligations," +
                "@expenses,@date_from,@date_to,@cash)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@total_sales",decimal.Parse( salestxt.Text));
            cmd.Parameters.AddWithValue("@products",decimal.Parse( producttxt.Text));
            cmd.Parameters.AddWithValue("@money_out", decimal.Parse(moneyouttxt.Text));
            cmd.Parameters.AddWithValue("@importation", decimal.Parse(importationtxt.Text));
            cmd.Parameters.AddWithValue("@obligations", decimal.Parse(monyon.Text));
            cmd.Parameters.AddWithValue("@expenses", decimal.Parse(extxt.Text));
            cmd.Parameters.AddWithValue("@date_from", dateTimePicker5.Text);
            cmd.Parameters.AddWithValue("@date_to", dateTimePicker6.Text);
            decimal sales = decimal.Parse(salestxt.Text);
            decimal moneyout = decimal.Parse(moneyouttxt.Text);
            decimal import = decimal.Parse(importationtxt.Text);
            decimal ex =decimal.Parse(extxt.Text);
            decimal cash = (sales+sum) - (moneyout + import + ex);
            cmd.Parameters.AddWithValue("@cash", cash);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("done");
            gridview_date("select * from  inventory", dataGridView1);



        }

        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int index;
            index = e.RowIndex;
            DataGridViewRow selectedrow = dataGridView1.Rows[index];
            idtxt.Text = selectedrow.Cells[0].Value.ToString();
            salestxt.Text = selectedrow.Cells[1].Value.ToString();
            producttxt.Text = selectedrow.Cells[2].Value.ToString();
            moneyouttxt.Text = selectedrow.Cells[3].Value.ToString();
            importationtxt.Text = selectedrow.Cells[4].Value.ToString();
            monyon.Text = selectedrow.Cells[5].Value.ToString();
            extxt.Text = selectedrow.Cells[6].Value.ToString();
            dateTimePicker5.Text = selectedrow.Cells[7].Value.ToString();
            dateTimePicker6.Text = selectedrow.Cells[8].Value.ToString();


        }

        private void button15_Click(object sender, EventArgs e)
        {
            int id=int.Parse(idtxt.Text);
            decimal sales= decimal.Parse(salestxt.Text);
            decimal products= decimal.Parse(producttxt.Text);
            decimal moneyout= decimal.Parse(moneyouttxt.Text);
            decimal importation= decimal.Parse(importationtxt.Text);
            decimal obligations= decimal.Parse(monyon.Text);
            decimal expenses= decimal.Parse(extxt.Text);
            string datefrom= dateTimePicker5.Text;
            string dateto= dateTimePicker6.Text;
            con.Open();
            string query = "update inventory set total_sales='"+sales+"',products='"+products+"',money_out='"+moneyout+"'"+
                ",importation='"+importation+"',obligations='"+obligations+"',expenses='"+expenses+"',date_from='"+datefrom+"'"+
                ",date_to='"+dateto+"'where id='"+id+"'";
            SqlCommand cmd=new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("done");
            gridview_date("select * from  inventory", dataGridView1);
        }

        
    }

}
        