using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogueEditor
{
    public partial class Form3 : Form
    {
        private Form2 startform;
        public DBConnection DB;
        private int quest_id;
        private string dbpath;
        OpenFileDialog ofd = new OpenFileDialog();
        string copyDbDirectory = @".\_tempWorld.bytes";
        bool isClearTempDB = false;

        public Form3(Form2 startform)
        {
            InitializeComponent();
            this.startform = startform;
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            startform.Show();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {

                try
                {
                    FileInfo fn = new FileInfo(ofd.FileName);
                    fn.CopyTo(copyDbDirectory, isClearTempDB);
                    MessageBox.Show($"Database was successfully copied to: {copyDbDirectory}");
                }
                catch
                {
                    MessageBox.Show("DataBase copy already exist");
                }
                finally
                {
                    dbpath = copyDbDirectory;
                    label2.Text = copyDbDirectory;

                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dbpath = copyDbDirectory;
            label2.Text = copyDbDirectory;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            quest_id = Convert.ToInt16(textBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBUpdate();
        }
        public void DBUpdate()
        {
            if (dbpath != null)
            {
                DB = new DBConnection(dbpath);
                DB.OpenConnection();
                if (quest_id != 0)
                {
                    comboBox2.Items.AddRange(DB.LoadTaskTypes());
                }
                else
                {
                    MessageBox.Show("ERROR: NPC ID not selected");
                }
            }
            else
            {
                MessageBox.Show("ERROR: DataBase not selected");
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
           
        }
    }
}
