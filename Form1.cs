using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace Project_1_
{
    public partial class Form1 : Form
    {
        private MySqlConnection databaseConnection()
        {
            string connectionString = " datasource=127.0.0.1;port=3306;username=root;password=;database=project;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2_password.UseSystemPasswordChar = true;
            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;
            this.Location = new Point(0, 0);
            this.Size = new Size(w, h);

        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f = new Form3();
            f.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox1_email.Text, "[^0-9&&A-z&&@&&.&&_]"))
            {
                MessageBox.Show("กรุณากรอกอีเมลให้ถูกต้อง", "กรอกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Regex reg = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.co\w+([m]\w+)*");
                if (!reg.IsMatch(textBox1_email.Text))
                {
                    MessageBox.Show("กรุณากรอกอีเมลให้ถูกต้อง", "กรอกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (textBox1_email.Text == "" || textBox2_password.Text == "")
                    {
                        MessageBox.Show("กรุณากรอกข้อมูลให้ครบ", "กรอกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    else
                    {
                        string connectionString = " datasource=127.0.0.1;port=3306;username=root;password=;database=project;";
                        MySqlConnection conn = new MySqlConnection(connectionString);
                        conn.Open();

                        using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM  `registration_user` WHERE `email` ='" + textBox1_email.Text + "' AND `password` ='" + textBox2_password.Text + "'", conn))
                        {
                            MySqlDataReader dr = cmd.ExecuteReader();

                            if (dr.Read())
                            {
                                MessageBox.Show("เข้าสู่ระบบ");
                                Form2 f = new Form2();
                                this.Hide();
                                f.Show();

                            }
                            else
                            {
                                MessageBox.Show("อีเมล หรือ รหัสผ่านไม่ถูกต้อง!!!");
                            }

                        }

                    }
                }
            }                    
    }
           

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("คุณยืนยันที่จะออกจากโปรแกรมใช่หรือไม่?",
                "ออกจากโปรแกรม", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (dialog == DialogResult.No)
            {

            }
        }

        private void textBox1_email_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_email_Leave(object sender, EventArgs e)
        {
            string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (Regex.IsMatch(textBox1_email.Text,pattern))
            {
                errorProvider1.Clear();
            }
            else
            {
                errorProvider1.SetError(this.textBox1_email, "กรุณาระบุที่อยู่อีเมลที่ถูกต้อง");
                return;
            }
        }

        private void textBox2_password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (System.Text.Encoding.UTF8.GetByteCount(new char[] { e.KeyChar }) > 1)
            {
                e.Handled = true;
            }
        }

        private void showpassword_CheckedChanged(object sender, EventArgs e)
        {
            if (showpassword.Checked == true)
            {
                textBox2_password.UseSystemPasswordChar = false;
            }
            if (showpassword.Checked == false)
            {
                textBox2_password.UseSystemPasswordChar = true;
            }
        }

        private void textBox2_password_Validated(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox2_password.Text, "[^0-9&&A-z&&@&&.&&]"))
            {
                MessageBox.Show("กรุณาห้ามใส่อักษรพิเศษในรหัสผ่าน", "กรอกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2_password.Text = "";
            }
        }

        private void textBox1_email_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (System.Text.Encoding.UTF8.GetByteCount(new char[] { e.KeyChar }) > 1)
            {
                e.Handled = true;
            }
        }
    }
}
