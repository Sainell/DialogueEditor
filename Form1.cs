using System;
using System.Collections.Generic;
using System.Drawing;
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

        public Form1()
        {
            InitializeComponent();                        
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            if (dbpath != null)
            {
                DB = new DBConnection(dbpath);
                DB.OpenConnection();
                if (npc_id != 0)
                {
                    DB.GetFromDB(npc_id, Controls, this);
                    DB.CreateGraph();
                }
                else
                {
                    MessageBox.Show("NPC ID not selected");
                }
            }
            else
                {
                    MessageBox.Show("DataBase not selected");
                }
           
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            npc_id = Convert.ToInt16( textBox7.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DB.CreateDBPatch();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                label2.Text = ofd.FileName;
                dbpath = ofd.FileName;
            }
                
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DB.CreateNewNode("test444");
        }
    }
}
