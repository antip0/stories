using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace stories
{
    public partial class authorization : Form
    {
        string query = "select * from authors;";
        string query1 = "select * from auth;";
        public static bool flag = false;
        public authorization()
        {
            InitializeComponent();
            button6.BackColor = Color.Green;
        }
        public void authors()
        {
            MySqlConnection conn = DBUtils.GetDBConnection();
            try
            {
                comboBox1.Items.Clear();
                conn.Open();
                MySqlCommand cmDB = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmDB.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetString("author_name"));
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка!" + Environment.NewLine + ex.Message);
            }
        }
        public void authors_login()
        {
            MySqlConnection conn = DBUtils.GetDBConnection();          
            try
            {
                comboBox2.Items.Clear();
                conn.Open();
                MySqlCommand cmDB = new MySqlCommand(query1, conn);
                MySqlDataReader reader = cmDB.ExecuteReader();
                while (reader.Read())
                {
                    comboBox2.Items.Add(reader.GetString("auth_log"));
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка!" + Environment.NewLine + ex.Message);
            }
        }
        public void getData(string query)
        {
            MySqlConnection conn = DBUtils.GetDBConnection();
            MySqlDataAdapter ada = new MySqlDataAdapter(query, conn);
            try
            {
                conn.Open();
                DataTable table = new DataTable();
                ada.Fill(table);
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            authors();
            authors_login();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            reg reg = new reg();
            reg.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string script = "select * from auth join authors on authors.author_id = auth.auth_id where authors.author_name = '" + comboBox1.Text + "' and auth.auth_pwd = '" + textBox1.Text + "';";
            string script1 = "select * from auth where auth.auth_log = '" + comboBox2.Text + "' and auth.auth_pwd = '" + textBox1.Text + "';";
            MySqlConnection connection = DBUtils.GetDBConnection();
            MySqlDataAdapter ada = new MySqlDataAdapter(script, connection);
            DataTable table = new DataTable();
            ada.Fill(table);
            MySqlCommand cmDB = new MySqlCommand(script, connection);
            MySqlCommand cmDB1 = new MySqlCommand(script1, connection);
            if (comboBox1.Text == "" && comboBox2.Text != "")
            {
                try
                {
                    connection.Open();
                    int result = 0;
                    result = Convert.ToInt32(cmDB1.ExecuteScalar());
                    if (result > 0)
                    {
                        MessageBox.Show("Авторизация прошла успешно!");
                        mainMenu mainMenu = new mainMenu(result);
                        mainMenu.Owner = this;
                        this.Hide();
                        mainMenu.Show();
                        cmDB1.ExecuteNonQuery();
                    }
                    else if (textBox1.Text == "" && comboBox2.Text == "")
                    {
                        MessageBox.Show("Укажите Логин и введите пароль!");
                    }
                    else if (textBox1.Text == "")
                    {
                        MessageBox.Show("Введите пароль!");
                    }
                    else if (comboBox2.Text == "")
                    {
                        MessageBox.Show("Укажите Логин!");
                    }
                    else
                    {
                        MessageBox.Show("Неверный пароль!");
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка!" + Environment.NewLine + ex.Message);
                }
            }
            else if (comboBox2.Text == "" && comboBox1.Text != "")
            {
                try
                {
                    connection.Open();
                    int result = 0;
                    result = Convert.ToInt32(cmDB.ExecuteScalar());
                    if (result > 0)
                    {
                        MessageBox.Show("Здравствуйте!");
                        mainMenu mainMenu = new mainMenu(result);
                        mainMenu.Owner = this;
                        this.Hide();
                        mainMenu.Show();
                        cmDB.ExecuteNonQuery();
                    }
                    else if (textBox1.Text == "" && comboBox1.Text == "")
                    {
                        MessageBox.Show("Укажите ФИО и введите пароль!");
                    }
                    else if (textBox1.Text == "")
                    {
                        MessageBox.Show("Введите пароль!");
                    }
                    else if (comboBox1.Text == "")
                    {
                        MessageBox.Show("Укажите ФИО");
                    }
                    else
                    {
                        MessageBox.Show("Неверный пароль!");
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка!" + Environment.NewLine + ex.Message);
                }
            }
            else if (comboBox1.Text == "" && comboBox2.Text == "" && textBox1.Text == "")
            {
                DialogResult res = MessageBox.Show("Укажите ФИО или Логин и введите пароль!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (res == DialogResult.OK)
                {
                    return;
                }
            }
            else if (comboBox1.Text == "" && comboBox2.Text == "")
            {
                DialogResult res = MessageBox.Show("Укажите ФИО или Логин!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (res == DialogResult.OK)
                {
                    return;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            flag = true;
            comboBox2.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            deleteUser deleteUser = new deleteUser();
            deleteUser.ShowDialog();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти?", "Выход", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox1.PasswordChar == '*')
            {
                textBox1.PasswordChar = '\0';
                button6.BackColor = Color.Red;
            }
            else if (textBox1.PasswordChar == '\0')
            {
                textBox1.PasswordChar = '*';
                button6.BackColor = Color.Green;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            authors();
            authors_login();
        }
    }
}
