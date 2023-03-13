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
    public partial class reg : Form
    {
        public reg()
        {
            InitializeComponent();
            reg reg = Application.OpenForms[0] as reg;
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
                MessageBox.Show("ошибка!" + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "insert into auth (auth_log, auth_pwd) values ('" + textBox2.Text + "', '" + textBox3.Text + "');";
            string query1 = "insert into authors (author_name, author_birth) values ('" + textBox1.Text + "', '" + dateTimePicker1.Text + "');";
            string query2 = "update authors set author_auth = (select auth_id from auth where auth_log = '" + textBox2.Text + "') order by author_id desc limit 1;";
            string query3 = "select auth_id from auth where auth_log = '" + textBox2.Text + "';";
            MySqlConnection conn = DBUtils.GetDBConnection();
            MySqlCommand cmDB = new MySqlCommand(query3, conn);
            authorization auth = new authorization();
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                try
                {
                    conn.Open();
                    int result = 0;
                    result = Convert.ToInt32(cmDB.ExecuteScalar());
                    if (result == 0)
                    {
                        getData(query + query1 + query2);
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                        MessageBox.Show("Вы успешно зарегистрировали пользователя!");
                    }
                    else if (result > 0)
                    {
                        MessageBox.Show("Такой логин уже существует!");
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

        private void reg_Load(object sender, EventArgs e)
        {
            button3.BackColor = Color.Green;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.PasswordChar == '*')
            {
                textBox3.PasswordChar = '\0';
                button3.BackColor = Color.Red;
            }
            else if (textBox3.PasswordChar == '\0')
            {
                textBox3.PasswordChar = '*';
                button3.BackColor = Color.Green;
            }
        }

        private void reg_FormClosing(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
