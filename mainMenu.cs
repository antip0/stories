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
    public partial class mainMenu : Form
    {
        public int ID = 0;
        public mainMenu(int ID_log)
        {
            InitializeComponent();
            ID = ID_log;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            viewStories viewStories = new viewStories(ID);
            viewStories.Owner = this;
            this.Hide();
            viewStories.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти?", "Выход", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Owner.Show();
            this.Close();
        }

        private void mainMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
