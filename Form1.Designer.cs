namespace abo_elmagd
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.hellotxt = new System.Windows.Forms.Label();
            this.databasebtn = new System.Windows.Forms.Button();
            this.billsbtn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.searchbtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // hellotxt
            // 
            this.hellotxt.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.hellotxt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hellotxt.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hellotxt.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.hellotxt.Location = new System.Drawing.Point(274, 9);
            this.hellotxt.Name = "hellotxt";
            this.hellotxt.Size = new System.Drawing.Size(229, 50);
            this.hellotxt.TabIndex = 3;
            this.hellotxt.Text = "hello abo elmagd";
            this.hellotxt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // databasebtn
            // 
            this.databasebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.databasebtn.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.databasebtn.Location = new System.Drawing.Point(40, 246);
            this.databasebtn.Name = "databasebtn";
            this.databasebtn.Size = new System.Drawing.Size(109, 48);
            this.databasebtn.TabIndex = 4;
            this.databasebtn.Text = "data base";
            this.databasebtn.UseVisualStyleBackColor = true;
            this.databasebtn.Click += new System.EventHandler(this.databasebtn_Click);
            // 
            // billsbtn
            // 
            this.billsbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.billsbtn.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.billsbtn.Location = new System.Drawing.Point(40, 354);
            this.billsbtn.Name = "billsbtn";
            this.billsbtn.Size = new System.Drawing.Size(109, 48);
            this.billsbtn.TabIndex = 4;
            this.billsbtn.Text = "bills";
            this.billsbtn.UseVisualStyleBackColor = true;
            this.billsbtn.Click += new System.EventHandler(this.billsbtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::abo_elmagd.Properties.Resources.images__1_;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 450);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // searchbtn
            // 
            this.searchbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchbtn.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchbtn.Location = new System.Drawing.Point(40, 300);
            this.searchbtn.Name = "searchbtn";
            this.searchbtn.Size = new System.Drawing.Size(109, 48);
            this.searchbtn.TabIndex = 5;
            this.searchbtn.Text = "search";
            this.searchbtn.UseVisualStyleBackColor = true;
            this.searchbtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.searchbtn);
            this.Controls.Add(this.billsbtn);
            this.Controls.Add(this.databasebtn);
            this.Controls.Add(this.hellotxt);
            this.Controls.Add(this.pictureBox1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = " ";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label hellotxt;
        private System.Windows.Forms.Button databasebtn;
        private System.Windows.Forms.Button billsbtn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button searchbtn;
    }
}

