using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Project_1_
{
    public partial class Form4 : Form
    {
        bool check , show ;
        DateTime checkin, checkout;
        string roomnum;
        private MySqlConnection databaseConnection()
        {
            string connectionString = " datasource=127.0.0.1;port=3306;username=root;password=;database=project;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }
        public Form4()
        {
            InitializeComponent();
        }
        DateTime today;
        
        private void showroom()
        {
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();
            conn.Open();

            MySqlCommand cmd;

            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id,type_room,room,price FROM `room` ";

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);
            conn.Close();
            dataGridView1_user.DataSource = ds.Tables[0].DefaultView;
        }
        private void typeroom()
        {
            MySqlConnection conn = databaseConnection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT DISTINCT type_room FROM `room` ";
            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    comboBox2.Items.Add(dr.GetValue(0).ToString());
                }
            }



        }


        private void pictureBox9_Click(object sender, EventArgs e)
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

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox3.Text, "[^0-9&&A-z&&@&&.&&_]"))
            {
                MessageBox.Show("กรุณากรอกอีเมลให้ถูกต้อง", "กรอกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Regex reg = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.co\w+([m]\w+)*");
                if (!reg.IsMatch(textBox3.Text))
                {
                    MessageBox.Show("กรุณากรอกอีเมลให้ถูกต้อง", "กรอกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (textBox4.TextLength < 10)
                    {
                        MessageBox.Show("กรุณากรอกเบอร์โทรศัพท์ให้ครบ");
                    }

                    else if (textBox2.Text == "" || textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || comboBox1.SelectedIndex == -1 || textBox5.Text == "" || comboBox2.SelectedIndex == -1 || comboBox3.SelectedIndex == -1 || textBox6.Text == "" || check_in.Text == "" || check_out.Text == "" || textBox8.Text == "" || textBox7.Text == "")
                    {
                        MessageBox.Show("กรุณากรอกข้อมูลให้ครบ", "กรอกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (check == false)
                    {
                        MessageBox.Show("ห้องพักไม่ว่าง");
                    }
                    else
                    {
                        string connectionSting = "datasource=127.0.0.1;port=3306;username=root;password=;database=project;";
                        MySqlConnection conn = new MySqlConnection(connectionSting);
                        conn.Open();

                        String sql = "INSERT INTO `booking` (fname_user,lname_user,email_user,phone_user,sex_user,Address_user,type_room,number_room,price_room,check_in_user,check_out_user,count_day,total_price_user)VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + comboBox1.SelectedItem + "','" + textBox5.Text + "','" + comboBox2.SelectedItem + "','" + comboBox3.SelectedItem + "','" + textBox6.Text + "','" + check_in.Text + "','" + check_out.Text + "','" + textBox8.Text + "','" + textBox7.Text + "')";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        try
                        {
                            int rows = cmd.ExecuteNonQuery();
                            if (rows > 0)
                            {
                                Program.name = $"{textBox1.Text}  {textBox2.Text}";
                                Program.typeroom = comboBox2.SelectedItem.ToString();
                                Program.numroom = comboBox3.SelectedItem.ToString();
                                Program.checkin = check_in.Text;
                                Program.checkout = check_out.Text;
                                Program.price = textBox7.Text;
                                Program.oldform = "fm4";
                                MessageBox.Show("จองที่พักสำเร็จ!");
                                this.Hide();
                                Form15 f = new Form15();
                                f.Show();
                                showroom();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                
            } 
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            check_in.Value = DateTime.Now;
            check_out.Value = DateTime.Now;
            comboBox1.Text = string.Empty;
            comboBox2.Text = string.Empty;
            comboBox3.Text = string.Empty;


        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f = new Form2();
            f.Show();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            showroom();
            typeroom();
                       
            Time.Text = DateTime.Now.ToLongTimeString();
            Date.Text = DateTime.Now.ToLongDateString();
            check_in.Text = Date.Text;
            check_out.Text = Date.Text;
            today = check_in.Value;
            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;
            this.Location = new Point(0, 0);
            this.Size = new Size(w, h);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && !char.IsControl(ch))
            {
                e.Handled = true;
                MessageBox.Show("ใส่ตัวเลขระหว่าง 0-9 เท่านั้น", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (textBox4.TextLength >= 10 && ch != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {       
        }      

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            MySqlConnection conn = databaseConnection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT room FROM `room` WHERE type_room = '" + comboBox2.Text + "' ";
            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    comboBox3.Items.Add(dr.GetValue(0).ToString());
                }
            }
            MySqlConnection conn2 = databaseConnection();
            conn2.Open();
            MySqlCommand cmd2 = conn2.CreateCommand();
            cmd2.CommandText = "SELECT price FROM `room` WHERE type_room = '" + comboBox2.Text + "' ";
            MySqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                string price = dr2.GetValue(0).ToString();
                textBox6.Text = price;
            }


        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DateTime date1 = check_in.Value.Date;
            DateTime date2 = check_out.Value.Date;
            if (date2 <= date1)
            {
                MessageBox.Show("กรูณาตรวจสอบวันจองที่พัก");
            }
            else
            {
                TimeSpan tp = date2 - date1;
                double dateiff = tp.TotalDays;
                textBox8.Text = dateiff.ToString();
                int price = int.Parse(textBox6.Text);
                int day = int.Parse(textBox8.Text);
                int total = price * day;
                textBox7.Text = total.ToString();
            }
                
        }

        private void check_in_ValueChanged(object sender, EventArgs e)
        {            
            int res = DateTime.Compare(check_in.Value, today);
            if (res <= 0)
            {
                MessageBox.Show("กรูณาตรวจสอบวันจองที่พัก");
            }
        }

        private void check_out_ValueChanged(object sender, EventArgs e)
        {
            int res = DateTime.Compare(check_out.Value, today);
            if (res <= 0)
            {
                MessageBox.Show("กรูณาตรวจสอบวันจองที่พัก");
            }
        }             

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (show == false) //โชว์MessageBox.Show
            {
                checkroom(comboBox3.Text, false); // ห้องไม่ว่าง
            }
            show = false; //โชว์กลับ
        }
        private void checkroom(string room , bool status)
        {
            
            MySqlConnection conn = databaseConnection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT check_in_user,check_out_user FROM booking WHERE number_room = \"{room}\" ";
            MySqlDataReader dr = cmd.ExecuteReader();            
            if (dr.Read())
            {
                checkin = Convert.ToDateTime(dr.GetValue(0).ToString()); //รับค่า
                checkout = Convert.ToDateTime(dr.GetValue(1).ToString());
                if (check_in.Value.Date >= checkin && check_in.Value.Date <= checkout && status == false ) //status == false โชว์กล่องข้อความ
                {                   
                    check = false;
                    textBox10.Text = "ไม่ว่าง";
                    textBox10.BackColor = System.Drawing.Color.Red;
                    textBox10.ForeColor = System.Drawing.Color.Black;
                    MessageBox.Show("ห้องพักไม่ว่าง");
                }
                else if (status == true) //ไม่โชว์ในCellcli
                {
                    check = false;
                    textBox10.Text = "ไม่ว่าง";
                    textBox10.BackColor = System.Drawing.Color.Red;
                    textBox10.ForeColor = System.Drawing.Color.Black;
                }             
                else
                {
                    check = true;
                    textBox10.Text = "ว่าง";
                    textBox10.BackColor = System.Drawing.Color.Green;
                    textBox10.ForeColor = System.Drawing.Color.White;
                }
            }
            else
            {
                
                check = true;
                textBox10.Text = "ว่าง";
                textBox10.BackColor = System.Drawing.Color.Green;
                textBox10.ForeColor = System.Drawing.Color.White;
            }
            
        }     

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (System.Text.Encoding.UTF8.GetByteCount(new char[] { e.KeyChar }) > 1)
            {
                e.Handled = true;
            }


        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Date_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_user_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            dataGridView1_user.CurrentRow.Selected = true;
            roomnum = dataGridView1_user.Rows[e.RowIndex].Cells["room"].FormattedValue.ToString();
            checkroom(roomnum, true); // โชว์ห้องว่าง
            show = true; //ไม่โชว์
            comboBox2.SelectedItem = dataGridView1_user.Rows[e.RowIndex].Cells["type_room"].FormattedValue.ToString();
            comboBox3.SelectedItem = dataGridView1_user.Rows[e.RowIndex].Cells["room"].FormattedValue.ToString();
            
        }
    }
}
