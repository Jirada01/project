using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_1_
{
    public partial class Form15 : Form
    {
        public Form15()
        {
            InitializeComponent();
        }
        private void Form15_Load(object sender, EventArgs e)
        {
            DateTime datenow = DateTime.Now;
            DateTime tomorrow = datenow.AddDays(1);
            label1.Text = tomorrow.ToLongDateString() + "  23:59 น.";
            label2.Text = Program.price + "  บาท";
            label3.Text = Program.name;
            label4.Text = Program.typeroom;
            label5.Text = Program.numroom;
            label6.Text = Program.checkin;
            label7.Text = Program.checkout;
            Time.Text = DateTime.Now.ToLongTimeString();
            Date.Text = DateTime.Now.ToLongDateString();
            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;
            this.Location = new Point(0, 0);
            this.Size = new Size(w, h);
        }       

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if(Program.oldform == "fm14")
            {
                this.Hide();
                Form14 f = new Form14();
                f.Show();
            }
            else if (Program.oldform == "fm4")
            {
                this.Hide();
                Form4 f = new Form4();
                f.Show();
            }
            
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f = new Form2();
            f.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {           
        }
      
    }
}
