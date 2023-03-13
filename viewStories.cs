using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stories
{
    public partial class viewStories : Form
    {
        public int ID = 0;
        public static bool flag = false;
        public static bool flag1 = false;
        string query0 = "select story_name, authors.author_name, genres.genre_name from stories join authors on authors.author_id = stories.story_author join genres on genres.genre_id = stories.story_genre;";
        string query1 = "select * from genres;";
        string query2 = "select story_name, authors.author_name, genres.genre_name from stories join authors on authors.author_id = stories.story_author join genres on genres.genre_id = stories.story_genre order by story_name desc;";
        string query3 = "select story_name, authors.author_name, genres.genre_name from stories join authors on authors.author_id = stories.story_author join genres on genres.genre_id = stories.story_genre order by author_name desc;";
        string query4 = "select story_name, authors.author_name, genres.genre_name from stories join authors on authors.author_id = stories.story_author join genres on genres.genre_id = stories.story_genre order by story_name;";
        string query5 = "select story_name, authors.author_name, genres.genre_name from stories join authors on authors.author_id = stories.story_author join genres on genres.genre_id = stories.story_genre order by author_name;";
        public viewStories(int ID_log)
        {
            InitializeComponent();
            ID = ID_log;
            getInfo(ID_log);
        }

        void Search()
        {
            string script = "select story_name, authors.author_name, genres.genre_name from stories join authors on authors.author_id = stories.story_author join genres on genres.genre_id = stories.story_id where story_name like '" + textBox1.Text + "%';";
            string script1 = "select story_name, authors.author_name, genres.genre_name from stories join authors on authors.author_id = stories.story_author join genres on genres.genre_id = stories.story_id where authors.author_name like '" + textBox1.Text + "%';";
            MySqlConnection conn = DBUtils.GetDBConnection();
            DataTable table = new DataTable();
            dataGridView1.DataSource = table;
            table.Clear();
            if (comboBox3.Text == "Названию произведения")
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmDB = new MySqlCommand(script, conn);
                    MySqlDataReader reader = cmDB.ExecuteReader();
                    while (reader.Read())
                    {
                        getData(script);
                    }
                    reader.Close();
                    conn.Close();
                    dataGridView1.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка!" + Environment.NewLine + ex.Message);
                }
            }

            else if (comboBox3.Text == "ФИО автора")
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmDB = new MySqlCommand(script1, conn);
                    MySqlDataReader reader = cmDB.ExecuteReader();
                    while (reader.Read())
                    {
                        getData(script1);
                    }
                    reader.Close();
                    conn.Close();
                    dataGridView1.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка!" + Environment.NewLine + ex.Message);
                }
            }
        }
        public void getInfo(int ID)
        {
            string query = "SELECT author_name from authors where author_id =" + ID + ";";
            MySqlConnection conn = DBUtils.GetDBConnection();
            MySqlCommand cmDB = new MySqlCommand(query, conn);
            try
            {
                conn.Open();
                MySqlDataReader rd = cmDB.ExecuteReader();
                if (rd.HasRows)
                {
                    rd.Read();
                    toolStripTextBox1.Text = rd.GetString(0);
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла непредвиденная ошибка!" + Environment.NewLine + ex.Message);
            }
        }
        public void getData(string query)
        {
            MySqlConnection connection = DBUtils.GetDBConnection();
            MySqlDataAdapter ada = new MySqlDataAdapter(query, connection);
            try
            {
                connection.Open();
                DataTable table = new DataTable();
                dataGridView1.DataSource = table;
                dataGridView1.ClearSelection();
                ada.Fill(table);
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка!" + Environment.NewLine + ex.Message);
            }
        }
        private void выйтиИзПрограммыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти?", "Выход", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void главноеМенюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Owner.Show();
            this.Close();
        }

        private void viewStories_Load(object sender, EventArgs e)
        {
            MySqlConnection conn = DBUtils.GetDBConnection();
            try
            {
                conn.Open();
                MySqlCommand cmDB = new MySqlCommand(query1, conn);
                MySqlDataReader reader = cmDB.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetString("genre_name"));
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка!" + Environment.NewLine + ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select story_name, authors.author_name, genres.genre_name from stories join authors on authors.author_id = stories.story_author join genres on genres.genre_id = stories.story_genre where genre_name = '" + comboBox1.Text + "';";
            if (comboBox1.Text != "Общий список")
            {
                getData(query);
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("Ничего не найдено!");
                }
            }
            else
            {
                getData(query0);
            }          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query6 = "select story_name, authors.author_name, genres.genre_name from stories join authors on authors.author_id = stories.story_author join genres on genres.genre_id = stories.story_genre where genre_name = '" + comboBox1.Text + "' order by story_name desc;";
            string query7 = "select story_name, authors.author_name, genres.genre_name from stories join authors on authors.author_id = stories.story_author join genres on genres.genre_id = stories.story_genre where genre_name = '" + comboBox1.Text + "' order by author_name desc;";
            if (comboBox1.Text == "Общий список")
            {
                if (comboBox2.Text == "названию")
                {
                    getData(query2);
                }
                else if (comboBox2.Text == "автору")
                {
                    getData(query3);
                }
                else
                {
                    MessageBox.Show("Выберите критерий сортировки!");
                }
            }
            else if (comboBox1.Text != "" && comboBox1.Text != "Общий список")
            {          
                if (comboBox2.Text == "названию")
                {
                    getData(query6);
                }
                else if (comboBox2.Text == "автору")
                {
                    getData(query7);
                }
                else
                {
                    MessageBox.Show("Выберите критерий сортировки!");
                }
            }
            else if (comboBox1.Text == "" && comboBox2.Text == "")
            {
                MessageBox.Show("Выберите критерий фильтрации и сортировки!");
            }
            else if (comboBox2.Text == "")
            {
                MessageBox.Show("Выберите критерий сортировки!");
            }
            else if (comboBox1.Text == "")
            {
                MessageBox.Show("Выберите критерий фильтрации!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string query8 = "select story_name, authors.author_name, genres.genre_name from stories join authors on authors.author_id = stories.story_author join genres on genres.genre_id = stories.story_genre where genre_name = '" + comboBox1.Text + "' order by story_name;";
            string query9 = "select story_name, authors.author_name, genres.genre_name from stories join authors on authors.author_id = stories.story_author join genres on genres.genre_id = stories.story_genre where genre_name = '" + comboBox1.Text + "' order by author_name;";
            if (comboBox1.Text == "Общий список")
            {
                if (comboBox2.Text == "названию")
                {
                    getData(query4);
                }
                else if (comboBox2.Text == "автору")
                {
                    getData(query5);
                }
                else
                {
                    MessageBox.Show("Выберите критерий сортировки!");
                }
            }
            else if (comboBox1.Text != "" && comboBox1.Text != "Общий список")
            {
                if (comboBox2.Text == "названию")
                {
                    getData(query8);
                }
                else if (comboBox2.Text == "автору")
                {
                    getData(query9);
                }
                else
                {
                    MessageBox.Show("Выберите критерий сортировки!");
                }
            }
            else if (comboBox1.Text == "" && comboBox2.Text == "")
            {
                MessageBox.Show("Выберите критерий фильтрации и сортировки!");
            }
            else if (comboBox2.Text == "")
            {
                MessageBox.Show("Выберите критерий сортировки!");
            }
            else if (comboBox1.Text == "")
            {
                MessageBox.Show("Выберите критерий фильтрации!");
            }
        }

        private void выйтиИзПрофиляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            authorization auth = new authorization();
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i].Name != "authorization")
                    Application.OpenForms[i].Close();
            }
            auth.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text == "")
            {
                textBox1.Enabled = false;
                MessageBox.Show("Выберите критерий поиска и нажмите кнопку 'Очистить'!");
            }
            else
            {
                Search();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (flag1 == true)
            {
                textBox1.Clear();
                textBox1.Enabled = true;
                flag1 = false;
            }
            else if (flag1 == false && textBox1.Enabled == true)
            {
                textBox1.Clear();
            }
            else
            {
                MessageBox.Show("Сначала выберите критерий поиска, а затем нажмите ккопку 'Очистить'!");
            }

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            flag1 = true;
            getData(query0);
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string script = "select story_text from stories where story_name = '" + textBox3.Text + "';";
            MySqlConnection conn = DBUtils.GetDBConnection();
            MySqlCommand cmDB = new MySqlCommand(script, conn);
            if (textBox3.Text != "")
            {
                try
                {
                    conn.Open();
                    object result = cmDB.ExecuteScalar();
                    if (result == null)
                    {
                        MessageBox.Show("Ничего не найдено!");
                    }
                    else
                    {
                        textBox2.Text = result.ToString();
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка!" + Environment.NewLine + ex.Message);
                }
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("Введите название произведения!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox3.Clear();
        }
    }
}
