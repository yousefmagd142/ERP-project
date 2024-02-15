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

namespace abo_elmagd
{
    public partial class Form5 : Form
    {
        public static String sql = "Data Source=yousef\\sqlexpress;Initial Catalog=almagd_db;Integrated Security=True;User Id='';Password=''";
        public SqlConnection con = new SqlConnection(sql);
        public Form5()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            sea(searchcustname, "select name from cust_table");
            sea(textBox2, "select name from suppliers");

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
        public void gridview_date(string query, DataGridView data)
        {
            SqlCommand cmd = new SqlCommand("" + query + "", con);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            data.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            string iddd = idd.Text;
            string namee = name.Text;
            int quanttye = int.Parse(quantty.Text);
            string query = "delete from inv_details where id ='" + iddd + "'and item ='" + namee + "'";
            string query3 = "update item_table set quantty = quantty + " + quanttye + " where  name ='" + namee + "'";



            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            SqlCommand cmd3 = new SqlCommand(query3, con);
            cmd3.ExecuteNonQuery();

            idd.Text = "";
            name.Text = "";
            quantty.Text = "";

            con.Close();
            MessageBox.Show("Deleted");
            gridview_date("select * from  inv_details", dataGridView1);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            string iddd = idd.Text;
            string namee = name.Text;
            int quanttye = int.Parse(quantty.Text);
            string query = "update inv_details set quantty = quantty - " + quanttye + " where  item ='" + namee + "'and id ='" + iddd + "'";
            string query3 = "update item_table set quantty = quantty + " + quanttye + " where  name ='" + namee + "'";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            SqlCommand cmd3 = new SqlCommand(query3, con);
            cmd3.ExecuteNonQuery();

            idd.Text = "";
            name.Text = "";
            quantty.Text = "";

            con.Close();
            MessageBox.Show("done");
            gridview_date("select * from  inv_details", dataGridView1);

        }

        private void searchcustbtn_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("select * from inv_details where cust_name like @cust_name+'%'", con);
            cmd1.Parameters.AddWithValue("cust_name", searchcustname.Text);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd1;
            DataTable dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void searchidbtn_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("select * from inv_details where id like @id+'%'", con);
            cmd1.Parameters.AddWithValue("id", searchid.Text);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd1;
            DataTable dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'almagd_dbDataSet.importation' table. You can move, or remove it, as needed.
            this.importationTableAdapter.Fill(this.almagd_dbDataSet.importation);
            // TODO: This line of code loads data into the 'almagd_dbDataSet1.importation' table. You can move, or remove it, as needed.
            this.importationTableAdapter.Fill(this.almagd_dbDataSet1.importation);
            // TODO: This line of code loads data into the 'almagd_dbDataSet.importation' table. You can move, or remove it, as needed.
            this.importationTableAdapter.Fill(this.almagd_dbDataSet.importation);
            // TODO: This line of code loads data into the 'almagd_dbDataSet.importation' table. You can move, or remove it, as needed.
            this.importationTableAdapter.Fill(this.almagd_dbDataSet.importation);
            // TODO: This line of code loads data into the 'almagd_dbDataSet.importation' table. You can move, or remove it, as needed.
            this.importationTableAdapter.Fill(this.almagd_dbDataSet.importation);
            // TODO: This line of code loads data into the 'almagd_dbDataSet.inv_details' table. You can move, or remove it, as needed.
            this.inv_detailsTableAdapter.Fill(this.almagd_dbDataSet.inv_details);

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index;
            index = e.RowIndex;
            DataGridViewRow selectedrow = dataGridView1.Rows[index];
            idd.Text = selectedrow.Cells[0].Value.ToString();
            name.Text = selectedrow.Cells[2].Value.ToString();
            quantty.Text = selectedrow.Cells[4].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("select * from importation where supplier like @supplier+'%'", con);
            cmd1.Parameters.AddWithValue("supplier", textBox2.Text);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd1;
            DataTable dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("select * from importation where id like @id+'%'", con);
            cmd1.Parameters.AddWithValue("id", textBox1.Text);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd1;
            DataTable dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            con.Close();
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index;
            index = e.RowIndex;
            DataGridViewRow selectedrow = dataGridView2.Rows[index];
            textBox5.Text = selectedrow.Cells[0].Value.ToString();
            textBox4.Text = selectedrow.Cells[1].Value.ToString();
            textBox3.Text = selectedrow.Cells[2].Value.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            con.Open();
            string iddd = textBox5.Text;
            string namee = textBox4.Text;
            int quanttye = int.Parse(textBox3.Text);
            string query = "delete from importation where id ='" + iddd + "'and item_name ='" + namee + "'";
            string query3 = "update item_table set quantty = quantty - " + quanttye + " where  name ='" + namee + "'";



            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            SqlCommand cmd3 = new SqlCommand(query3, con);
            cmd3.ExecuteNonQuery();

            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";

            con.Close();
            MessageBox.Show("Deleted");
            gridview_date("select * from  importation", dataGridView2);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            con.Open();
            string iddd = textBox5.Text;
            string namee = textBox4.Text;
            int quanttye = int.Parse(textBox3.Text);
            string query = "update importation set quantty = quantty - " + quanttye + " where  item_name ='" + namee + "'and id ='" + iddd + "'";
            string query3 = "update item_table set quantty = quantty - " + quanttye + " where  name ='" + namee + "'";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            SqlCommand cmd3 = new SqlCommand(query3, con);
            cmd3.ExecuteNonQuery();

            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";

            con.Close();
            MessageBox.Show("done");
            gridview_date("select * from  importation", dataGridView2);

        }
    }
}
