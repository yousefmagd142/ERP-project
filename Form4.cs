using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace abo_elmagd
{
    public partial class billsform : Form
    {
        public static billsform instance;

        public static String sql = "Data Source=yousef\\sqlexpress;Initial Catalog=almagd_db;Integrated Security=True;User Id='';Password=''";
        public SqlConnection con = new SqlConnection(sql);
        
        public billsform()
        {
            InitializeComponent();
            instance = this;
            this.StartPosition = FormStartPosition.CenterScreen;
        }


        private void billsform_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'almagd_dbDataSet.importation' table. You can move, or remove it, as needed.
            this.importationTableAdapter.Fill(this.almagd_dbDataSet.importation);
            // TODO: This line of code loads data into the 'almagd_dbDataSet1.item_table' table. You can move, or remove it, as needed.
            nof.Focus();
            nof.Select();
            nof.SelectAll();
            sea(itembox, "SELECT item_name COLLATE Arabic_CI_AI_KS_WS AS item_name FROM importation");
            sea(custname, "select name from cust_table");
            sea(supcomboBox, "select name from suppliers");
            sea(item_name, "select name from item_table");


        }

        

        private void custname_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Enter)
            {

                itembox.Focus();
            }


        }
        private void itembox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                pricebox.Focus();
            }
        }

        private void pricebox_KeyDown(object sender, KeyEventArgs e)
        {


            if (e.KeyCode == Keys.Enter)
            {
                quanttybox.Focus();
            }
        }
        private void quanttyboox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                addbtn.PerformClick();
                itembox.Text = "";
                pricebox.Text = "";
                quanttybox.Text = "";
                itembox.Focus();
            }
            
        }
       
        private void addbtn_Click(object sender, EventArgs e)
        {
            
            if (quanttybox.Text.Trim() == "" || quanttybox.Text.Trim() == "0")
            {
                MessageBox.Show("الكميه فاضيه");
            }
            string item = itembox.Text;
            int quanttyy = Int32.Parse(quanttybox.Text);
            double price = double.Parse(pricebox.Text) ;
            double suptotal =price*quanttyy;
            dataGV.Rows.Add(suptotal, quanttyy, price, item );
            double z = double.Parse(totalx.Text);
            z += suptotal;
            totalx.Text=z.ToString();

            //--------------------------------------------------
            con.Open();

            


            string insert = "INSERT INTO inv_details (date,total,price,quantty,id,item,cust_name) VALUES(@date,@total,@price,@quantty,@id,@item,@cust_name)";
            SqlCommand cmd = new SqlCommand(insert, con);
            int x = int.Parse(pricebox.Text);
            int y = int.Parse(quanttybox.Text);
            cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(datetime.Text));
            cmd.Parameters.AddWithValue("@total", x*y);
            cmd.Parameters.AddWithValue("@price", pricebox.Text);
            cmd.Parameters.AddWithValue("@quantty", quanttybox.Text);
            cmd.Parameters.AddWithValue("@id", nof.Text);
            cmd.Parameters.AddWithValue("@item", itembox.Text);
            cmd.Parameters.AddWithValue("@cust_name", custname.Text);
            cmd.ExecuteNonQuery();

            string namee = itembox.Text;
            int newqtyy = int.Parse(quanttybox.Text);
            string queryy = "update item_table set quantty =quantty-" + newqtyy + " where name='" + namee + "'";
            SqlCommand cmddd = new SqlCommand(queryy, con);
            cmddd.ExecuteNonQuery();


            con.Close();

        }


        private void quanttyboox_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (e.KeyChar != 8)
            {
                if (e.KeyChar < 48 | e.KeyChar > 57)
                {
                    e.Handled = true;
                }

            }
            
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

        private void totalx_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar != 8)
            {
                if (e.KeyChar < 48 | e.KeyChar > 57)
                {
                    e.Handled = true;
                }

            }
        }
        private void printbtn_Click(object sender, EventArgs e)
        {
            
            ((Form)printPreviewDialog1).WindowState = FormWindowState.Maximized;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
            


        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int margin = 10;
            Font  f =new Font("Arial",18,FontStyle.Bold);
            
            string almagd = "المجد لأعمال الليزر";
            string strnof = nof.Text + " : رقم الفاتورة";
            string strdate =  datetime.Text + " : التاريخ";
            string strname =  "الاسم : " + custname.Text ;
            

            SizeF almagdsize = e.Graphics.MeasureString(almagd, f);
            SizeF fontsizenof =e.Graphics.MeasureString(strnof, f);
            SizeF fontsizedate =e.Graphics.MeasureString(strdate,f);
            SizeF fontsizename =e.Graphics.MeasureString(strname,f);


            e.Graphics.DrawImage(Properties.Resources.images__2_, margin, margin, 200, 200);
            e.Graphics.DrawString(almagd, f, Brushes.Black, (e.PageBounds.Width - almagdsize.Width - margin) / 2, margin + 20);
            e.Graphics.DrawString(strnof, f, Brushes.Black, (e.PageBounds.Width - fontsizenof.Width - margin)/2,almagdsize.Height+ margin+30);
            e.Graphics.DrawString(strdate, f, Brushes.Black, e.PageBounds.Width - fontsizedate.Width - margin, almagdsize.Height+margin + fontsizenof.Height+30);
            e.Graphics.DrawString(strname, f, Brushes.Black, e.PageBounds.Width - fontsizename.Width - margin, margin+fontsizenof.Height+ almagdsize.Height+fontsizedate.Height+30); 
            

            float prehight = margin*6+ pictureBox1.Height;

            e.Graphics.DrawRectangle(Pens.Black, margin, prehight, e.PageBounds.Width - margin * 2,e.PageBounds.Height-margin*2-prehight);

            float coalhight = 60;
            float coal1width = 300;
            float coal2width = 150+coal1width;
            float coal3width = 150 +  coal2width ;
            float coal4width = 150+coal3width;
            
            
            e.Graphics.DrawLine(Pens.Black, margin, prehight + coalhight, e.PageBounds.Width - margin, prehight + coalhight);

            e.Graphics.DrawString("                الصنف", f, Brushes.Black, e.PageBounds.Width - coal1width, prehight+17);
            e.Graphics.DrawLine(Pens.Black, e.PageBounds.Width - coal1width, prehight, e.PageBounds.Width - coal1width, e.PageBounds.Height - margin * 2);

            e.Graphics.DrawString("      السعر", f, Brushes.Black, e.PageBounds.Width - coal2width, prehight+17);
            e.Graphics.DrawLine(Pens.Black, e.PageBounds.Width - coal2width, prehight, e.PageBounds.Width - coal2width, e.PageBounds.Height - margin * 2);

            e.Graphics.DrawString("       الكمية", f, Brushes.Black, e.PageBounds.Width - coal3width, prehight + 17);
            e.Graphics.DrawLine(Pens.Black, e.PageBounds.Width - coal3width, prehight, e.PageBounds.Width - coal3width, e.PageBounds.Height - margin * 2);

            e.Graphics.DrawString("الاجمالي         ", f, Brushes.Black, e.PageBounds.Width - coal4width, prehight + 17);

            float rowshight = 60;
            for(int x = 0;x<dataGV.Rows.Count;x++)
            {
                e.Graphics.DrawString(dataGV.Rows[x].Cells[0].Value.ToString(), f, Brushes.Black, e.PageBounds.Width -coal4width, prehight + rowshight);
                e.Graphics.DrawString(dataGV.Rows[x].Cells[1].Value.ToString(), f, Brushes.Black, e.PageBounds.Width - coal3width + 55, prehight + rowshight);
                e.Graphics.DrawString(dataGV.Rows[x].Cells[2].Value.ToString(), f, Brushes.Black, e.PageBounds.Width - coal2width + 55, prehight + rowshight);
                e.Graphics.DrawString(dataGV.Rows[x].Cells[3].Value.ToString(), f, Brushes.Black, e.PageBounds.Width - coal1width+120, prehight + rowshight);
                e.Graphics.DrawLine(Pens.Black, margin, prehight + rowshight +coalhight, e.PageBounds.Width - margin, prehight + rowshight+coalhight);

                rowshight += 60;
            }
            e.Graphics.DrawString(totalx.Text, f, Brushes.Black, e.PageBounds.Width - coal4width, prehight + rowshight);
            e.Graphics.DrawString("اجمالي الفاتورة", f, Brushes.Black, e.PageBounds.Width - coal3width +10, prehight + rowshight);
            e.Graphics.DrawLine(Pens.Black, margin, prehight + rowshight + coalhight+15, e.PageBounds.Width - margin, prehight + rowshight + coalhight+15);

            e.Graphics.DrawString(textBox1.Text, f, Brushes.Black, e.PageBounds.Width - coal4width, prehight + rowshight + 40);
            e.Graphics.DrawString("       القديم", f, Brushes.Black, e.PageBounds.Width - coal3width, prehight + rowshight + 40);

            e.Graphics.DrawString(paid.Text, f, Brushes.Black, e.PageBounds.Width - coal4width, prehight + rowshight + 80);
            e.Graphics.DrawString("     المدفوع", f, Brushes.Black, e.PageBounds.Width - coal3width, prehight + rowshight + 80);

            decimal final=decimal.Parse(totalx.Text)+decimal.Parse(textBox1.Text)-decimal.Parse(paid.Text);
            e.Graphics.DrawString(final.ToString(), f, Brushes.Black, e.PageBounds.Width - coal4width, prehight + rowshight + 120);
            e.Graphics.DrawString("       الباقي", f, Brushes.Black, e.PageBounds.Width - coal3width, prehight + rowshight +120);




        }

        public DataTable details_table ()
        {
            con.Open();
            DataTable dt = new DataTable();
            string query = "SELECT * FROM inv_details";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da =new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            return dt;
        }


        private void button2_Click(object sender, EventArgs e)
        {
 


            con.Open();
            int x;
            string select = "select max(id) from inv_details";
            SqlCommand cmd = new SqlCommand(select, con);
            x = (int)cmd.ExecuteScalar();
            con.Close();
            x = x+1;
            nof.Text= x.ToString();


            custname.Focus();
            custname.Select();
            custname.SelectAll();
        }

        private void cust_phone_KeyDown(object sender, KeyEventArgs e)
        {
         
            if (e.KeyCode == Keys.Enter)
            {

                itembox.Focus();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            String name = custname.Text;
            string q = "select rest from cust_table where name='" + name + "' ";
            SqlCommand cmd = new SqlCommand(q, con);
            object result = cmd.ExecuteScalar();

            if (result != DBNull.Value)
            {
                decimal h;
                decimal.TryParse(result.ToString(), out h);          
                decimal x = decimal.Parse(totalx.Text) - decimal.Parse(paid.Text);
                decimal updatedRest = x + h; // Perform the addition
                string query = "update cust_table set rest='" + updatedRest + "' where name='" + name + "' ";
                SqlCommand cmdd = new SqlCommand(query, con);
                cmdd.ExecuteNonQuery();
                
            }
            else
            {
                decimal x = decimal.Parse(totalx.Text) - decimal.Parse(paid.Text);
                string query = "update cust_table set rest='" + x +  "' where name='" + name + "' ";
                SqlCommand cmdd = new SqlCommand(query, con);
                cmdd.ExecuteNonQuery();
            }
            con.Close();
            MessageBox.Show("تم الحفظ");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            String name = custname.Text;
            string q = "select rest from cust_table where name='" + name + "' ";
            SqlCommand cmd = new SqlCommand(q, con);
            object result = cmd.ExecuteScalar();
            if (result != DBNull.Value)
            {
                decimal h;
                decimal.TryParse(result.ToString(), out h);
                textBox1.Text =  h.ToString(); // Perform the addition
                

            }
            else
            {
                textBox1.Text = "0";
            }
            con.Close ();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            String name =supcomboBox.Text;
            string q = "select debt from suppliers where name='" + name + "' ";
            SqlCommand cmd = new SqlCommand(q, con);
            object result = cmd.ExecuteScalar();
            if (result != DBNull.Value)
            {
                decimal h;
                decimal.TryParse(result.ToString(), out h);
                textBox2.Text = h.ToString(); // Perform the addition


            }
            else
            {
                textBox2.Text = "0";
            }
            con.Close();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            con.Open();
            int x;
            string select = "select max(id) from importation";
            SqlCommand cmd = new SqlCommand(select, con);
            x = (int)cmd.ExecuteScalar();
            con.Close();
            x = x + 1;
            idd.Text = x.ToString();
            supcomboBox.Focus();
            supcomboBox.Select();
            supcomboBox.SelectAll();
        }
        public void gridview_date(string query, DataGridView data)
        {
            SqlCommand cmd = new SqlCommand("" + query + "", con);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            data.DataSource = dt;
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (item_name.Text == "")
            {
                MessageBox.Show("يجب اضافة منتج");
            }
            else
            {
                int quaantty = int.Parse(item_quantty.Text);
                string namee = item_name.Text;
                con.Open();
                string insert3 = "INSERT INTO importation (item_name,quantty,entry_price,supplier,date,id) VALUES(@item_name,@quantty,@entry_price,@supplier,@date,@id)";
                decimal entry = decimal.Parse(entry_price.Text);
                string insert = "update item_table set quantty =quantty + '" + quaantty + "',entry_price='"+entry+"' where name ='" + namee + "'";
                SqlCommand cmd3 = new SqlCommand(insert3, con);
                SqlCommand cmd = new SqlCommand(insert, con);
                cmd.ExecuteNonQuery();
                cmd3.Parameters.AddWithValue("@item_name", item_name.Text);
                if (item_quantty.Text == "")
                {
                    MessageBox.Show("للعلم الكميه = 0", "warning!");
                    cmd3.Parameters.AddWithValue("@quantty", item_quantty.Text = "0");

                }
                else
                {
                    cmd3.Parameters.AddWithValue("@quantty", item_quantty.Text);
                    cmd3.Parameters.AddWithValue("@entry_price", entry_price.Text);
                    cmd3.Parameters.AddWithValue("@supplier", supcomboBox.Text);
                    cmd3.Parameters.AddWithValue("@date", dateTimePicker3.Text);
                    cmd3.Parameters.AddWithValue("@id", idd.Text);
                    
                }


                int x = int.Parse(item_quantty.Text);
                decimal y = decimal.Parse(entry_price.Text);
                decimal z = x * y;
                decimal sum = decimal.Parse(totalimp.Text);
                sum += z;
                totalimp.Text = sum.ToString();




                cmd3.ExecuteNonQuery();
                con.Close();


                MessageBox.Show("تم اضافة المنتج بنجاح");
                item_quantty.Text = "";
                entry_price.Text = "";
            }
            gridview_date("select * from importation", dataGridView4);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            con.Open();
            String name = supcomboBox.Text;
            string q = "select debt from suppliers where name='" + name + "' ";
            SqlCommand cmd = new SqlCommand(q, con);
            object result = cmd.ExecuteScalar();

            if (result != DBNull.Value)
            {
                decimal h;
                decimal.TryParse(result.ToString(), out h);
                decimal x2 = decimal.Parse(totalimp.Text) - decimal.Parse(imppaid.Text);
                decimal updatedRest = x2 + h; // Perform the addition
                string query = "update suppliers set debt='" + updatedRest + "' where name='" + name + "' ";
                SqlCommand cmdd = new SqlCommand(query, con);
                cmdd.ExecuteNonQuery();

            }
            else
            {
                decimal x2 = decimal.Parse(totalimp.Text) - decimal.Parse(imppaid.Text);
                string query = "update suppliers set debt='" + x2 + "' where name='" + name + "' ";
                SqlCommand cmdd = new SqlCommand(query, con);
                cmdd.ExecuteNonQuery();
            }
            MessageBox.Show("تم الحفظ");
            totalimp.Text = "0";
            imppaid.Text = "0";
            con.Close();
        }

        private void supcomboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                item_name.Focus();
            }
        }

        private void item_name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                entry_price.Focus();
            }
        }

        private void entry_price_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                item_quantty.Focus();
            }
        }

        private void item_quantty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                button8.PerformClick();
                item_quantty.Text = "";
                entry_price.Text = "";
                item_name.Focus();
            }
        }

       
    }
}
