using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Data.SQLite;
using System.Drawing.Drawing2D;

namespace DialogueEditor
{
    public partial class Form1 : Form
    {
        private Form1 f1;
        private SQLiteConnection DB;
        SQLiteCommand cmd;
        SQLiteCommand cmdCount;
        SQLiteCommand cmdNPCtext;
        SQLiteCommand cmdUIcount;
        private int npc_id;
        private int node_id;
        private List<NodeUI> nodeContainerUI = new List<NodeUI>();
        private List<NodeContainer> nodeContainer = new List<NodeContainer>();
        List<NodeUI> del = new List<NodeUI>();
        private NodeUI n;
        List<Control> dellist = new List<Control>();
        SQLiteDataReader reader;
        int countResult;
        public List<List<int>> realCount = new List<List<int>>();
        List<int> usedNode = new List<int>();
        private Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
          
               
        }
        
        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            f1.Location = this.Location;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // if (f1 != null) f1.Close();
           // f1 = new Form1();
           // f1.Controls.Clear();
           // f1.TopMost = true;
           // f1.TransparencyKey = SystemColors.Control;
           //// this.IsMdiContainer = true;
           //// f1.MdiParent = this;
           // //f1.FormBorderStyle = 0;
           // f1.Anchor = (AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left);
           // f1.Dock = DockStyle.Fill;
           // f1.StartPosition = FormStartPosition.Manual;
           // f1.Location = this.Location;
           // f1.AutoScroll = true;

           // f1.Show();


            DB = new SQLiteConnection("Data Source= " + @" E:\Projects\RPG_Van_Helsing\Assets\StreamingAssets\world.bytes");
            DB.Open();
            cmd = DB.CreateCommand();
            cmdNPCtext = DB.CreateCommand();
            cmdUIcount = DB.CreateCommand();
            cmdCount = DB.CreateCommand();
            cmdUIcount = DB.CreateCommand();

            cmdNPCtext.CommandText = $"select Npc_text from 'dialogue_node' where Npc_id = {npc_id}";
            cmdUIcount.CommandText = $"select count(*) from 'dialogue_node' where Npc_id = {npc_id}";


            var UIcountResult = Convert.ToInt16(cmdUIcount.ExecuteScalar());
            dellist.Clear();
            nodeContainer.Clear();
            for(int i=0;i<Controls.Count;i++)
            {
               if( Controls[i].GetType().ToString() == "DialogueEditor.NodeUI")
                {
                    
                    dellist.Add(Controls[i]);
                }
            }
            if (dellist != null)
            {
                foreach (Control c in dellist)
                {
                    Controls.Remove(c);
                }
            }

            for (int i = 0; i < UIcountResult; i++)
            {
               
                nodeContainerUI.Add(new NodeUI());
                nodeContainerUI[i].Visible = true;
                nodeContainerUI[i].Location = new Point((Width/2)-(197/2), 50+(i*400));
                nodeContainerUI[i].Name = "nodeUI" + i;
                nodeContainerUI[i].groupBox1.Text = "Node ID:" + i;
                Controls.Add(nodeContainerUI[i]);
                nodeContainer.Add(new NodeContainer(nodeContainerUI[i]));
            }
            
            for (int i = 0; i < nodeContainer.Count; i++)
            {
                cmdCount.CommandText = $"select count(*) from 'dialogue_answers' where Node_id = {i} and Npc_id = {npc_id}";
                cmd.CommandText = $"select * from 'dialogue_answers' where Node_id = {i} and Npc_id = {npc_id}";

               

                foreach (TextBox t in nodeContainer[i].answerBoxList)
                {
                    t.Text = "";
                }
                foreach (TextBox t in nodeContainer[i].questIdList)
                {
                    t.Text = "";
                }
                foreach (CheckBox t in nodeContainer[i].startCheckBoxList)
                {
                    t.Checked = false;
                }
                foreach (CheckBox t in nodeContainer[i].finishCheckBoxList)
                {
                    t.Checked = false;
                }
                foreach (CheckBox t in nodeContainer[i].exitCheckBoxList)
                {
                    t.Checked = false;
                }
                foreach (TextBox t in nodeContainer[i].toNodeList)
                {
                    t.Text = "";
                }
                


                countResult = Convert.ToInt16(cmdCount.ExecuteScalar());
                textBox6.Text = nodeContainer.Count().ToString();

                 
                
                reader = cmdNPCtext.ExecuteReader();
                for (int j = 0; j < nodeContainer.Count(); j++)
                {
                    reader.Read();
                    var npcTextResult = reader.GetValue(0).ToString();
                    nodeContainer[j].npcTextBox.Text = npcTextResult;
                }
                reader.Close();

                reader = cmd.ExecuteReader();
                for (int j = 0; j < countResult; j++)
                {
                    reader.Read();
                    nodeContainer[i].answerBoxList[j].Text = reader.GetValue(2).ToString();
                    nodeContainer[i].questIdList[j].Text = reader.GetValue(8).ToString();
                    nodeContainer[i].toNodeList[j].Text = reader.GetValue(3).ToString();
                    if (reader.GetValue(6).ToString() == "1") nodeContainer[i].startCheckBoxList[j].Checked = true;
                    if (reader.GetValue(7).ToString() == "1") nodeContainer[i].finishCheckBoxList[j].Checked = true;
                    if ((bool)reader.GetValue(4) == true) nodeContainer[i].exitCheckBoxList[j].Checked = true;


                }

                reader.Close();
            }
            
            

        }
        private void button2_Click(object sender, EventArgs e)
        {
            usedNode.Clear();
            realCount.Clear();
            for (int j = 0; j < nodeContainer.Count(); j++)
            {
                nodeContainer[j].rCount.Clear();
                for (int i = 0; i < nodeContainer[j].toNodeList.Count; i++)
                {
                    if (nodeContainer[j].toNodeList[i].Text != "" && nodeContainer[j].toNodeList[i].Text != "0" )
                    {
                        if (!usedNode.Contains(Convert.ToInt32(nodeContainer[j].toNodeList[i].Text)))
                        {
                            nodeContainer[j].rCount.Add(Convert.ToInt32((nodeContainer[j].toNodeList[i].Text)));
                            usedNode.Add(Convert.ToInt32((nodeContainer[j].toNodeList[i].Text)));
                        }
                    }
                }
                if (nodeContainer[j].rCount.Count != 0 )
                {
                    realCount.Add(nodeContainer[j].rCount);
                }
            }

            for (int j = 0; j < realCount.Count; j++)
            {
                for (int i = 0; i < realCount[j].Count; i++)
                {
                    var num = realCount[j].ElementAt(i);
                    nodeContainerUI[num].Location = new Point((Width / (realCount[j].Count + 1)) - (197 / 2) + (Width * i / (realCount[j].Count + 1)), 450 + (j * 400));
                    nodeContainer.RemoveAt(num);
                    nodeContainer.Insert(num, new NodeContainer(nodeContainerUI[num]));
                }
            }
            Graphics g = CreateGraphics();
            Pen pen = new Pen(Color.Red, 3);
            Pen pen2 = new Pen(Color.Blue, 3);
            Pen pen3 = new Pen(Color.DarkOliveGreen, 3);
            pen.StartCap = LineCap.RoundAnchor;
            pen.EndCap = LineCap.ArrowAnchor;

            for (int j = 0; j < usedNode.Count; j++)
            {

                for (int i = 0; i < nodeContainer[j].toNodeList.Count; i++)
                {
                    if (nodeContainer[j].toNodeList[i].Text != "" && nodeContainer[j].toNodeList[i].Text != "0")
                    {

                        Point pStart = PointToClient(nodeContainer[j].startPoint[i]);
                        Point pEnd = PointToClient(nodeContainer[Convert.ToInt16(nodeContainer[j].toNodeList[i].Text)].endPoint);

                        Point pUpRight = PointToClient(nodeContainer[j].intermediatePointUpRight);
                        Point pUpLeft = PointToClient(nodeContainer[j].intermediatePointUpLeft);
                        Point pDownRight = PointToClient(nodeContainer[j].intermediatePointDownRight);
                        Point pDownLeft = PointToClient(nodeContainer[j].intermediatePointDownLeft);
                        Point pMiddleRight = PointToClient(nodeContainer[j].intermediatePointMiddleRight);
                        Point pMiddleLeft = PointToClient(nodeContainer[j].intermediatePointMiddleLeft);

                        Point pUp = PointToClient(nodeContainer[Convert.ToInt16(nodeContainer[j].toNodeList[i].Text)].intermediatePointUp);


                        

                        g.DrawRectangle(pen2, pUpRight.X, pUpRight.Y, 4, 4);
                        g.DrawRectangle(pen2, pUpLeft.X, pUpLeft.Y, 4, 4);
                        g.DrawRectangle(pen2, pDownRight.X, pDownRight.Y, 4, 4);
                        g.DrawRectangle(pen2, pDownLeft.X, pDownLeft.Y, 4, 4);
                        g.DrawRectangle(pen2, pMiddleRight.X, pMiddleRight.Y, 4, 4);
                        g.DrawRectangle(pen2, pMiddleLeft.X, pMiddleLeft.Y, 4, 4);
                        g.DrawRectangle(pen, pUp.X, pUp.Y, 4, 4);

                        Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));

                        double LineLength(Point p1, Point p2)
                        {
                            var lineLength = Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y));
                            return lineLength;
                        }



                        if (LineLength(pMiddleRight, pEnd) < LineLength(pMiddleLeft,pEnd))
                        {
                            Point point = new Point();
                            if (LineLength(pUpRight, pEnd) < LineLength(pDownRight, pEnd)) point = pUpRight;
                            else point = pDownRight;

                            g.DrawCurve(new Pen(randomColor, 4), new Point[] { pStart, pMiddleRight, point, pUp, pEnd });
                        }
                        else
                        {
                            Point point = new Point();
                            if (LineLength(pUpLeft, pEnd) < LineLength(pDownLeft, pEnd)) point = pUpLeft;
                            else point = pDownLeft;

                            g.DrawCurve(new Pen(randomColor, 4), new Point[] { pStart, pMiddleLeft, point, pUp, pEnd });
                        }
                    }
                }
            }

        }
        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            npc_id = Convert.ToInt16( textBox7.Text);
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            node_id = Convert.ToInt16(textBox8.Text);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void nodeUI1_Load(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void nodeUI1_Load_1(object sender, EventArgs e)
        {

        }

        private void nodeUI2_Load(object sender, EventArgs e)
        {

        }

        private void nodeUI1_Load_2(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
