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
    public partial class deleteUser : Form
    {
        string query = "select * from authors;";
        public deleteUser()
        {
            InitializeComponent();
            button4.BackColor = Color.Green;
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
        void authors()
        {
            MySqlConnection conn = DBUtils.GetDBConnection();
            try
            {
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
        void authors_login()
        {
            string query1 = "select auth_log from auth join authors on authors.author_id = auth.auth_id where author_name = '" + comboBox1.Text + "';";
            MySqlConnection conn = DBUtils.GetDBConnection();
            MySqlCommand cmDB = new MySqlCommand(query1, conn);
                try
                {
                    conn.Open();
                    object result = cmDB.ExecuteScalar();
                    if (result != null)
                    {
                         textBox1.Text = result.ToString();                   
                    }
                    else
                    {
                         MessageBox.Show("Ничего не найдено!");
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка!" + Environment.NewLine + ex.Message);
                }
        }
        private void deleteUser_Load(object sender, EventArgs e)
        {
            authors();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            string query = "delete from authors where author_name = '" + comboBox1.Text + "';";
            string query1 = "delete from auth where auth_log = '" + textBox1.Text + "';";
            string query2 = "select auth_id from auth join authors on authors.author_id = auth.auth_id where authors.author_name = '" + comboBox1.Text + "' and auth.auth_log = '" + textBox1.Text + "' and auth.auth_pwd = '" + textBox3.Text + "';";
            MySqlConnection conn = DBUtils.GetDBConnection();
            MySqlCommand cmDB = new MySqlCommand(query2, conn);
            if (comboBox1.Text != "" && textBox1.Text != "" && textBox3.Text != "")
            { 
                try
                {
                    conn.Open();
                    int result = 0;
                    result = Convert.ToInt32(cmDB.ExecuteScalar());
                    if (result > 0)
                    {
                        getData(query + query1);
                        MessageBox.Show("Удаление прошло успешно!");
                        comboBox1.SelectedIndex = -1;
                        textBox1.Clear();
                        textBox3.Clear();
                        comboBox1.Items.Clear();
                        authors();
                    }
                    else if (result == 0)
                    {
                        MessageBox.Show("Введён неверный пароль!");
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка!" + Environment.NewLine + ex.Message);
                }               
            }        
            else
            {
                MessageBox.Show("Заполните все пустые поля!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                authors_login();
            }       
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            textBox1.Clear();
            textBox3.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox3.PasswordChar == '*')
            {
                textBox3.PasswordChar = '\0';
                button4.BackColor = Color.Red;
            }
            else if (textBox3.PasswordChar == '\0')
            {
                textBox3.PasswordChar = '*';
                button4.BackColor = Color.Green;
            }
        }
    }
}
