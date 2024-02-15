using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace abo_elmagd
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            hellotxt.Parent = pictureBox1;
            hellotxt.BackColor = Color.Transparent;

            searchbtn.Parent = pictureBox1;
            searchbtn.BackColor = Color.Transparent;

            databasebtn.Parent = pictureBox1;
            databasebtn.BackColor = Color.Transparent;

            billsbtn.Parent = pictureBox1;
            billsbtn.BackColor = Color.Transparent;


            string enteredPassword = PromptForPassword(); // Method to prompt for password
            if (enteredPassword != "123") // Replace "your_password" with the actual password
            {
                MessageBox.Show("Incorrect password. Access denied.");
                this.Close(); // Close the application if the password is incorrect
            }
        }

        // Method to prompt for password
        private string PromptForPassword()
        {
            Form prompt = new Form();
            prompt.StartPosition = FormStartPosition.CenterScreen; // Set the dialog to appear in the middle of the screen
            prompt.Width = 300;
            prompt.Height = 150;
            prompt.Text = "Enter Password";

            Label textLabel = new Label() { Left = 50, Top = 20, Width = 200, Text = "Enter the password:" };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 200 };
            textBox.PasswordChar = '*'; // To hide the password

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

    



        private void databasebtn_Click(object sender, EventArgs e)
        {
            Databaseform databasef = new Databaseform();
            databasef.Show();
        }

  
        

        private void billsbtn_Click(object sender, EventArgs e)
        {
            billsform billsf = new billsform();
            billsf.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form5 form55 = new Form5();
            form55.Show();
        }

       
    }
}
