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
        List<NodeTemp> temp;
        bool tempSaveFlag = true;
        public int testi = 0;
        public Form1(Form2 startform)
        {
            InitializeComponent();
            this.startform = startform;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (temp != null)
            {
                temp.Clear();
            }
            DBUpdate(); 
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            if (DB != null)
            {
                comboBox1.Items.Clear();
                comboBox1.Items.AddRange(DB.LoadNpcList().Item1);
            }
            else
            {
                MessageBox.Show("ERROR: DataBase not selected");
            }
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
            if (DB != null)
            {
                if (npc_id != 0)
                {
                    DB.SaveChangesToDB();
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

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                npc_id = Convert.ToInt16(comboBox1.Text);
            }
            catch
            {
                MessageBox.Show("ERROR: ID cannot be a string, You must enter only Number");
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
           if (DB!=null) DB.CloseConnection();
            startform.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (DB != null)
            {
                DB.CreateDBPatch("dialogue");
            }
            else
            {
                MessageBox.Show("ERROR: DataBase not selected");
            }
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
                    CopyDB();
                }
            }                
        }
        private void button6_Click(object sender, EventArgs e)
        {
            CopyDB();
        }

        public void CopyDB()
        {
            dbpath = copyDbDirectory;
            label2.Text = copyDbDirectory;
            if (dbpath != null)
            {
                DB = new DBConnection(dbpath, sb);
            }
            else
            {
                MessageBox.Show("ERROR: DataBase not selected");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (DB != null)
            {

                if (npc_id != 0)
                {
                    var result = MessageBox.Show("Are you save changes?", "Adding new Task", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        DB.CreateNewNode("npc text");
                        //SaveToTempAndLoad();
                        DBUpdate();
                    }
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                isClearTempDB = true;
            }
            else isClearTempDB = false;
        }

        public void DBUpdate()
        {
            if (DB != null)
            {
                DB = new DBConnection(dbpath, sb);  // Добавлено sb в конструктор, нужно проверить.
            
                if (npc_id != 0)
                {
                    DB.OpenConnection();
                   // DB.GetFromDB(npc_id, Controls, this);
                    DB.CreateGraph();
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

        private void Form1_Resize(object sender, EventArgs e)
        {
           // DBUpdate();
          SaveToTempAndLoad();
        }
        public void SaveToTempAndLoad()
        {
            if (tempSaveFlag)
            {
                temp = DB.SaveToTemp();
                tempSaveFlag = false;
            }
            if (DB != null)
            {
                DBUpdate();
                DB.LoadFromTemp(temp);
            }
            tempSaveFlag = true;
        }
    }
}
