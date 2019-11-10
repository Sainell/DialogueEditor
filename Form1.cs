using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DialogueEditor
{
    public partial class Form1 : Form
    {

        private int npc_id;
        private List<NodeUI> nodeContainerUI = new List<NodeUI>();
        private List<NodeContainer> nodeContainer = new List<NodeContainer>();
        List<NodeUI> del = new List<NodeUI>();
        List<Control> dellist = new List<Control>();
        public List<List<int>> realCount = new List<List<int>>();
        List<int> usedNode = new List<int>();
        private Random rnd = new Random();
        public DBConnection DB;
        OpenFileDialog ofd = new OpenFileDialog();
        string dbpath;
        Pen pen = new Pen(Color.Red, 3);
        Pen pen2 = new Pen(Color.Blue, 3);
        Pen pen3 = new Pen(Color.DarkOliveGreen, 3);
        Pen pen4 = new Pen(Color.AliceBlue, 3);
        string copyDbDirectory = @".\_tempWorld.bytes";
        bool isClearTempDB = false;
        public StringBuilder sb = new StringBuilder();
        private Form2 startform;

        public Form1(Form2 startform)
        {
            InitializeComponent();
            MouseWheel += new MouseEventHandler(Form1_MouseWheel);
            this.startform = startform;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBUpdate(); 
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            npc_id = Convert.ToInt16( textBox7.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(textBox7);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button4);
            panel1.Controls.Add(button5);
            panel1.Controls.Add(button6);

        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (DB != null)
            {
                    DB.DrawPointsAndLines();
                    DB.DrawLines();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DB.SaveChangesToDB();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
           if (DB!=null) DB.CloseConnection();
            startform.Show();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DB.CreateDBPatch();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                
                try
                {
                    FileInfo fn = new FileInfo(ofd.FileName);
                    fn.CopyTo(copyDbDirectory,isClearTempDB);
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

        private void button5_Click(object sender, EventArgs e)
        {
            DB.CreateNewNode("npc text");
            DBUpdate();
        }

        private void Form1_Scroll(object sender, ScrollEventArgs e)
        {
            MenuPanelScrolling();
        }

        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            MenuPanelScrolling();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                isClearTempDB = true;
            }
            else isClearTempDB = false;
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            //if (dbpath != null)
            //{
            //   DBUpdate();
            //}
        }

        public void DBUpdate()
        {
            if (dbpath != null)
            {
                DB = new DBConnection(dbpath);
                DB.OpenConnection();
                if (npc_id != 0)
                {
                    DB.GetFromDB(npc_id, Controls, this);
                    DB.CreateGraph();
                    MenuPanelScrolling();
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
        private void MenuPanelScrolling()
        {
         //  panel1.Location = new Point(panel1.Location.X, 0);
        }
    }
}
