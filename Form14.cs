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
    public partial class Form14 : Form
    {
        bool check, show;
        DateTime checkin, checkout;
        string  roomnum;        
        public Form14()
        {
            InitializeComponent();
        }
        DateTime today;
        private MySqlConnection databaseConnection()
        {
            string connectionString = " datasource=127.0.0.1;port=3306;username=root;password=;database=project;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }      
        private void showroom2(string valueToSearch)
        {
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();
            conn.Open();

            MySqlCommand cmd;

            cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT id,fname_user,lname_user,email_user,phone_user,sex_user,Address_user,type_room,number_room,price_room,check_in_user,check_out_user,count_day,total_price_user,note FROM  `booking` WHERE number_room = \"{valueToSearch}\" ";

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);
            conn.Close();
            dataGridView3.DataSource = ds.Tables[0].DefaultView;
        }
        private void showroom3()
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
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
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

        private void Form14_Load(object sender, EventArgs e)
        {            
            showroom2("*");
            showroom3();
            typeroom();
           
            Time.Text = DateTime.Now.ToLongTimeString();
            Date.Text = DateTime.Now.ToLongDateString();
            check_in_2.Text = Date.Text;
            check_out_2.Text = Date.Text;
            today = check_in_2.Value;

            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;
            this.Location = new Point(0, 0);
            this.Size = new Size(w, h);           
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

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("คุณยืนยันที่จะออกจากระบบใช่หรือไม่?",
           "ออกจากระบบ", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                this.Hide();
                Form12 f = new Form12();
                f.Show();
                
            }
            else if (dialog == DialogResult.No)
            {

            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && !char.IsControl(ch))
            {
                e.Handled = true;
                MessageBox.Show("ใส่ตัวเลขระหว่าง 0-9 เท่านั้น", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (textBox7.TextLength >= 4 && ch != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
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

                    else if (textBox2.Text == "" || textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || comboBox1.SelectedIndex == -1 || textBox5.Text == "" || comboBox2.SelectedIndex == -1 || comboBox3.SelectedIndex == -1 || textBox6.Text == "" || check_in_2.Text == "" || check_out_2.Text == "" || textBox8.Text == "" || textBox9.Text == "")
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

                        String sql = "INSERT INTO `booking` (fname_user,lname_user,email_user,phone_user,sex_user,Address_user,type_room,number_room,price_room,check_in_user,check_out_user,count_day,total_price_user)VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + comboBox1.SelectedItem + "','" + textBox5.Text + "','" + comboBox2.SelectedItem + "','" + comboBox3.SelectedItem + "','" + textBox6.Text + "','" + check_in_2.Text + "','" + check_out_2.Text + "','" + textBox8.Text + "','" + textBox9.Text + "')";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        try
                        {
                            int rows = cmd.ExecuteNonQuery();
                            if (rows > 0)
                            {
                                Program.name = $"{textBox1.Text}  {textBox2.Text}";
                                Program.typeroom = comboBox2.SelectedItem.ToString();
                                Program.numroom = comboBox3.SelectedItem.ToString();
                                Program.checkin = check_in_2.Text;
                                Program.checkout = check_out_2.Text;
                                Program.price = textBox9.Text;
                                Program.oldform = "fm14";
                                MessageBox.Show("จองที่พักสำเร็จ!");
                                this.Hide();
                                Form15 f = new Form15();
                                f.Show();
                                showroom3();
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

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            
            DateTime date1 = check_in_2.Value.Date;
            DateTime date2 = check_out_2.Value.Date;
            if (date2 < date1)
            {
                MessageBox.Show("กรูณาตรวจสอบวันจองที่พัก");
            }
            else
            {
                TimeSpan tp = date2 - date1;
                double dateiff = tp.TotalDays;
                //int dateiff = ((TimeSpan)(date2 - date1)).Days;
                textBox8.Text = dateiff.ToString();
                int price = int.Parse(textBox6.Text);
                int day = int.Parse(textBox8.Text);
                int total = price * day;
                textBox9.Text = total.ToString();
            }
            
        }            

        private void check_in_2_ValueChanged(object sender, EventArgs e)
        {
            int res = DateTime.Compare(check_in_2.Value, today);
            if (res <= 0)
            {
                MessageBox.Show("กรูณาตรวจสอบวันจองที่พัก");
            }
            
        }

        private void check_out_2_ValueChanged(object sender, EventArgs e)
        {
            int res = DateTime.Compare(check_out_2.Value, today);
            if (res <= 0)
            {
                MessageBox.Show("กรูณาตรวจสอบวันจองที่พัก");
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string valueToSearch = textBox7.Text.ToString();
            showroom2(valueToSearch);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            MySqlConnection conn = databaseConnection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT room FROM `room` WHERE  type_room = '" + comboBox2.Text + "' ";
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
            cmd2.CommandText = "SELECT price FROM `room` WHERE  type_room = '" + comboBox2.Text + "' ";
            MySqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                string price = dr2.GetValue(0).ToString();
                textBox6.Text = price;
            }
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView3.CurrentRow.Selected = true;                    
            textBox1.Text = dataGridView3.Rows[e.RowIndex].Cells["fname_user"].FormattedValue.ToString();
            textBox2.Text = dataGridView3.Rows[e.RowIndex].Cells["lname_user"].FormattedValue.ToString();
            textBox3.Text = dataGridView3.Rows[e.RowIndex].Cells["email_user"].FormattedValue.ToString();
            textBox4.Text = dataGridView3.Rows[e.RowIndex].Cells["phone_user"].FormattedValue.ToString();
            comboBox1.SelectedItem = dataGridView3.Rows[e.RowIndex].Cells["sex_user"].FormattedValue.ToString();
            textBox5.Text = dataGridView3.Rows[e.RowIndex].Cells["Address_user"].FormattedValue.ToString();
            comboBox2.SelectedItem = dataGridView3.Rows[e.RowIndex].Cells["type_room"].FormattedValue.ToString();
            var num = dataGridView3.Rows[e.RowIndex].Cells["number_room"].FormattedValue.ToString();
            comboBox3.SelectedItem = num;
            textBox6.Text = dataGridView3.Rows[e.RowIndex].Cells["price_room"].FormattedValue.ToString();
            check_in_2.Text = dataGridView3.Rows[e.RowIndex].Cells["check_in_user"].FormattedValue.ToString();
            check_out_2.Text = dataGridView3.Rows[e.RowIndex].Cells["check_out_user"].FormattedValue.ToString();
            textBox8.Text = dataGridView3.Rows[e.RowIndex].Cells["count_day"].FormattedValue.ToString();
            textBox9.Text = dataGridView3.Rows[e.RowIndex].Cells["total_price_user"].FormattedValue.ToString();
            textBox11.Text = dataGridView3.Rows[e.RowIndex].Cells["note"].FormattedValue.ToString();
            textBox10.Text = "ไม่ว่าง";
            textBox10.BackColor = System.Drawing.Color.Red;
            textBox10.ForeColor = System.Drawing.Color.Black;

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            int selectedRow = dataGridView3.CurrentCell.RowIndex;
            int deletId = Convert.ToInt32(dataGridView3.Rows[selectedRow].Cells["id"].Value);
            MySqlConnection conn = databaseConnection();
            string sql = "DELETE FROM `booking` WHERE id = '" + deletId + "'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            int rows = cmd.ExecuteNonQuery();
            conn.Close();
            if (rows > 0)
            {
                MessageBox.Show("ลบข้อมูลสำเร็จ");               
                showroom2("*");
                showroom3();

            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            int selectedRow = dataGridView3.CurrentCell.RowIndex;
            int editId = Convert.ToInt32(dataGridView3.Rows[selectedRow].Cells["id"].Value);
            MySqlConnection conn = databaseConnection();
            string sql = "UPDATE `booking` SET fname_user = '" + textBox1.Text + "',lname_user = '" + textBox2.Text + "',email_user = '" + textBox3.Text + "',phone_user= '" + textBox4.Text + "',sex_user = '" + comboBox1.SelectedItem + "',Address_user= '" + textBox5.Text + "',type_room = '" + comboBox2.SelectedItem + "',number_room = '" + comboBox3.SelectedItem + "' , price_room= '" + textBox6.Text + "' , check_in_user= '" + check_in_2.Text + "' , check_out_user = '" + check_out_2.Text + "' , count_day = '" + textBox8.Text + "', total_price_user = '" + textBox9.Text + "', note = '" + textBox11.Text + "' WHERE id = '" + editId + "' ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            int rows = cmd.ExecuteNonQuery();
            conn.Close();
            if (textBox2.Text == "" || textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || comboBox1.SelectedIndex == -1 || textBox5.Text == "" || comboBox2.SelectedIndex == -1 || comboBox3.SelectedIndex == -1 || textBox6.Text == "" || check_in_2.Text == "" || check_out_2.Text == "" || textBox8.Text == "" || textBox9.Text == "" || textBox11.Text == "")
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบ", "กรอกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (rows > 0)
            {
                MessageBox.Show("แก้ไขข้อมูลสำเร็จ");
                showroom2("*");
                showroom3();

            }
            
            else if (check == false)
            {
                MessageBox.Show("ห้องพักไม่ว่าง");
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox11.Clear();
            check_in_2.Value = DateTime.Now;
            check_out_2.Value = DateTime.Now;
            comboBox1.Text = string.Empty;
            comboBox2.Text = string.Empty;
            comboBox3.Text = string.Empty;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (System.Text.Encoding.UTF8.GetByteCount(new char[] { e.KeyChar }) > 1)
            {
                e.Handled = true;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (Regex.IsMatch(textBox3.Text, pattern))
            {
                errorProvider1.Clear();
            }
            else
            {
                errorProvider1.SetError(this.textBox3, "กรุณาระบุที่อยู่อีเมลที่ถูกต้อง");
                return;
            }
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

        private void label11_Click(object sender, EventArgs e)
        {

        }       

        private void label10_Click(object sender, EventArgs e)
        {

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
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (show == false) //โชว์MessageBox.Show
            {
                checkroom(comboBox3.Text, false); // ห้องไม่ว่าง
            }
            show = false; //โชว์กลับ
        }

        private void checkroom(string room, bool status)
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
                if (check_in_2.Value.Date >= checkin && check_in_2.Value.Date <= checkout && status == false) //status == false โชว์กล่องข้อความ
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
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            roomnum = dataGridView1.Rows[e.RowIndex].Cells["room"].FormattedValue.ToString();
            checkroom(roomnum, true); // โชว์ห้องว่าง
            show = true; //ไม่โชว์
            comboBox2.SelectedItem = dataGridView1.Rows[e.RowIndex].Cells["type_room"].FormattedValue.ToString();
            comboBox3.SelectedItem = dataGridView1.Rows[e.RowIndex].Cells["room"].FormattedValue.ToString();
        }
    }
}
