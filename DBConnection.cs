using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Drawing.Drawing2D;
using System.Text;
using System.IO;

namespace DialogueEditor
{
    public class DBConnection
    {
        SQLiteCommand cmd;
        SQLiteCommand cmdCount;
        SQLiteCommand cmdNPCtext;
        SQLiteCommand cmdUIcount;
        SQLiteCommand cmdUIAnswerCount;
        public SQLiteConnection DB { get; private set; }
        private List<NodeUI> nodeContainerUI = new List<NodeUI>();
        private List<NodeContainer> nodeContainer = new List<NodeContainer>();
        List<NodeUI> del = new List<NodeUI>();
        List<Control> dellist = new List<Control>();
        SQLiteDataReader reader;
        int countResult;
        private int npc_id;
        Control.ControlCollection Controls;
        public Form1 form;
        List<int> usedNode = new List<int>();
        public List<List<int>> realCount = new List<List<int>>();
        Graphics g;
        Pen pen = new Pen(Color.Red, 3);
        Pen pen2 = new Pen(Color.Blue, 3);
        Pen pen3 = new Pen(Color.DarkOliveGreen, 3);
        Pen pen4 = new Pen(Color.AliceBlue, 3);
        List<Pen> penList = new List<Pen>();
        private Random rnd = new Random();
        List<Point[]> pointsList = new List<Point[]>();
        StringBuilder sb = new StringBuilder();


        public DBConnection(string BDpath)
        {
            DB = new SQLiteConnection("Data Source= " + BDpath);
            cmd = DB.CreateCommand();
            cmdNPCtext = DB.CreateCommand();
            cmdUIcount = DB.CreateCommand();
            cmdCount = DB.CreateCommand();
            cmdUIcount = DB.CreateCommand();
            cmdUIAnswerCount = DB.CreateCommand();
        }

        public void OpenConnection()
        {
            DB.Open();
        }
        public void CloseConnection()
        {
            DB.Close();
        }

        public void GetFromDB(int npc_id, Control.ControlCollection controls, Form1 form)
        {
            this.Controls = controls;
            this.npc_id = npc_id;
            this.form = form;

            cmdUIcount.CommandText = $"select count(*) from 'dialogue_node' where Npc_id = {npc_id}";

            ClearNodeUIElements();
            CreateNodeCounteinerList();
            GetNodeAllDates();
            GetRealCountNodes();
            RestructingNodes();
        }
        public int GetUIElementCount()
        {
            var UIcountResult = Convert.ToInt16(cmdUIcount.ExecuteScalar());
            return UIcountResult;
        }

        public int GetNpcTextCount()
        {
            countResult = Convert.ToInt16(cmdCount.ExecuteScalar());
            return countResult;
        }
        public void ClearNodeUIElements()
        {
            dellist.Clear();
            nodeContainer.Clear();
            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i].GetType().ToString() == "DialogueEditor.NodeUI")
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
        }

        public void CreateNodeCounteinerList()
        {
            for (int i = 0; i < GetUIElementCount(); i++)
            {
                cmdUIAnswerCount.CommandText = $"select count(*) from 'dialogue_answers' where Npc_id ={npc_id} and Node_ID = {i}";
                var UIanswerCountResult = Convert.ToInt16(cmdUIAnswerCount.ExecuteScalar());

                nodeContainerUI.Add(new NodeUI(UIanswerCountResult, i, form.DB, form));
                nodeContainerUI[i].Visible = true;
                nodeContainerUI[i].Location = new Point((form.Width / 2) - (197 / 2), 50 + (i * 450));
                nodeContainerUI[i].Name = "nodeUI" + i;
                nodeContainerUI[i].groupBox1.Text = "Node ID:" + i;
                Controls.Add(nodeContainerUI[i]);
                nodeContainer.Add(new NodeContainer(nodeContainerUI[i]));
            }
        }

        public void GetNodeAllDates()
        {
            for (int i = 0; i < nodeContainer.Count; i++)
            {
                cmdCount.CommandText = $"select count(*) from 'dialogue_answers' where Node_id = {i} and Npc_id = {npc_id}";
                cmd.CommandText = $"select * from 'dialogue_answers' where Node_id = {i} and Npc_id = {npc_id}";
                cmdNPCtext.CommandText = $"select Npc_text from 'dialogue_node' where Npc_id = {npc_id}";

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

                reader = cmdNPCtext.ExecuteReader();
                for (int j = 0; j < nodeContainer.Count(); j++)
                {
                    reader.Read();
                    var npcTextResult = reader.GetValue(0).ToString();
                    nodeContainer[j].npcTextBox.Text = npcTextResult;
                }
                reader.Close();

                reader = cmd.ExecuteReader();
                for (int j = 0; j < GetNpcTextCount(); j++)
                {
                    reader.Read();
                    nodeContainer[i].AnswerIDList[j].Text = reader.GetValue(0).ToString();
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

        public void SaveChangesToDB()
        {
            for (int i = 0; i < nodeContainer.Count(); i++)
            {

                var npcText = nodeContainer[i].npcTextBox.Text;
                cmdNPCtext.CommandText = $"update 'dialogue_node' SET Npc_text ='{npcText}' where Npc_id = {npc_id} and Node_id = {i}";
                WriteCommand(cmdNPCtext.CommandText);
                cmdNPCtext.ExecuteNonQuery();

                for (int j = 0; j < nodeContainer[i].answerBoxList.Count; j++)
                {

                    var ID = nodeContainer[i].AnswerIDList[j].Text;
                    if (ID == "") continue;
                    var answerBoxText = nodeContainer[i].answerBoxList[j].Text;
                    var questIdText = nodeContainer[i].questIdList[j].Text;
                    var toNodeText = nodeContainer[i].toNodeList[j].Text;
                    int startCheckBoxValue;
                    if (nodeContainer[i].startCheckBoxList[j].Checked == true) { startCheckBoxValue = 1; }
                    else startCheckBoxValue = 0;
                    int finishCheckBoxValue;
                    if (nodeContainer[i].finishCheckBoxList[j].Checked == true) { finishCheckBoxValue = 1; }
                    else finishCheckBoxValue = 0;
                    int exitCheckBoxValue;
                    if (nodeContainer[i].exitCheckBoxList[j].Checked == true) { exitCheckBoxValue = 1; }
                    else exitCheckBoxValue = 0;

                    cmd.CommandText = $"update 'dialogue_answers' SET Answer_text='{answerBoxText}', To_node ='{toNodeText}', Quest_ID='{questIdText}', End_dialogue='{exitCheckBoxValue}', Start_quest='{startCheckBoxValue}', End_quest='{finishCheckBoxValue}'   where Id = '{ID}'";
                    WriteCommand(cmd.CommandText);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void WriteCommand(string cmd)
        {
            sb.AppendFormat(cmd + "; " + "\r\n");
        }
        public void CreateDBPatch()
        {
            SaveChangesToDB();
            var path = @".\DB_Patches\";
            var date = DateTime.Now.ToString("yyyyMMdd-HHmm");
            var dbname = "_world_";
            var table = "dialogues";
            var format = ".sql";

            File.AppendAllText(path + date + dbname + table + format, sb.ToString());
            MessageBox.Show($"Patch name is:  {date + dbname + table + format}");
        }

        public void CreateNewNode(string npc_text)
        {
            cmd.CommandText = $"insert into 'dialogue_node' (Npc_id, Node_ID, Npc_text) values ('{npc_id}','{nodeContainer.Count}','{npc_text}') ";
            WriteCommand(cmd.CommandText);
            cmd.ExecuteNonQuery();
        }
        public void DeleteNode(int node_id)
        {
            cmd.CommandText = $"delete from 'dialogue_node' where Npc_id={npc_id} and where Node_ID={node_id}";
            WriteCommand(cmd.CommandText);
            cmd.ExecuteNonQuery();
        }

        public void CreateNewAnswer(int node_ID)
        {
            cmd.CommandText = $"insert into 'dialogue_answers' (Npc_id, Node_ID, Answer_text, To_node, End_dialogue) values ('{npc_id}','{node_ID}','answer','0','0')";
            WriteCommand(cmd.CommandText);
            cmd.ExecuteNonQuery();
        }

        public void DeleteAnswer(int answer_id)
        {
            cmd.CommandText = $"delete from 'dialogue_answers' where Id ={answer_id}";
            WriteCommand(cmd.CommandText);
            cmd.ExecuteNonQuery();
        }

        public void GetRealCountNodes()
        {
            usedNode.Clear();
            realCount.Clear();
            for (int j = 0; j < nodeContainer.Count(); j++)
            {
                nodeContainer[j].rCount.Clear();
                for (int i = 0; i < nodeContainer[j].toNodeList.Count; i++)
                {
                    if (nodeContainer[j].toNodeList[i].Text != "" && nodeContainer[j].toNodeList[i].Text != "0")
                    {
                        if (!usedNode.Contains(Convert.ToInt32(nodeContainer[j].toNodeList[i].Text)))
                        {
                            nodeContainer[j].rCount.Add(Convert.ToInt32((nodeContainer[j].toNodeList[i].Text)));
                            usedNode.Add(Convert.ToInt32((nodeContainer[j].toNodeList[i].Text)));
                        }
                    }
                }
                if (nodeContainer[j].rCount.Count != 0)
                {
                    realCount.Add(nodeContainer[j].rCount);
                }
            }
        }
        public void RestructingNodes()
        {
            for (int j = 0; j < realCount.Count; j++)
            {
                for (int i = 0; i < realCount[j].Count; i++)
                {
                    var num = realCount[j].ElementAt(i);
                    nodeContainerUI[num].Location = new Point((form.Width / (realCount[j].Count + 1)) - (197 / 2) + (form.Width * i / (realCount[j].Count + 1)), 700 + (j * 450));
                    nodeContainer.RemoveAt(num);
                    nodeContainer.Insert(num, new NodeContainer(nodeContainerUI[num]));
                }
            }
        }

        public void CreateGraph()
        {
            g = form.CreateGraphics();
            pen.StartCap = LineCap.RoundAnchor;
            pen.EndCap = LineCap.ArrowAnchor;
        }

        public void DrawPointsAndLines()
        {
            for (int j = 0; j < usedNode.Count; j++)
            {
                for (int i = 0; i < nodeContainer[j].toNodeList.Count; i++)
                {
                    if (nodeContainer[j].toNodeList[i].Text != "" && nodeContainer[j].toNodeList[i].Text != "0")
                    {

                        Point pRightStart = form.PointToClient(new Point(nodeContainer[j].startRightPoint[i].X, nodeContainer[j].startRightPoint[i].Y - form.VerticalScroll.Value));
                        Point pLeftStart = form.PointToClient(new Point(nodeContainer[j].startLeftPoint[i].X, nodeContainer[j].startLeftPoint[i].Y - form.VerticalScroll.Value));
                        Point pEnd = form.PointToClient(new Point(nodeContainer[Convert.ToInt16(nodeContainer[j].toNodeList[i].Text)].endPoint.X, nodeContainer[Convert.ToInt16(nodeContainer[j].toNodeList[i].Text)].endPoint.Y - form.VerticalScroll.Value));
                        Point pUpRight = form.PointToClient(new Point(nodeContainer[j].intermediatePointUpRight.X, nodeContainer[j].intermediatePointUpRight.Y - form.VerticalScroll.Value));
                        Point pUpLeft = form.PointToClient(new Point(nodeContainer[j].intermediatePointUpLeft.X, nodeContainer[j].intermediatePointUpLeft.Y - form.VerticalScroll.Value));
                        Point pDownRight = form.PointToClient(new Point(nodeContainer[j].intermediatePointDownRight.X, nodeContainer[j].intermediatePointDownRight.Y - form.VerticalScroll.Value));
                        Point pDownLeft = form.PointToClient(new Point(nodeContainer[j].intermediatePointDownLeft.X,nodeContainer[j].intermediatePointDownLeft.Y - form.VerticalScroll.Value));
                        Point pMiddleRight = form.PointToClient(new Point(nodeContainer[j].intermediatePointMiddleRight.X, nodeContainer[j].intermediatePointMiddleRight.Y - form.VerticalScroll.Value));
                        Point pMiddleLeft = form.PointToClient(new Point(nodeContainer[j].intermediatePointMiddleLeft.X, nodeContainer[j].intermediatePointMiddleLeft.Y - form.VerticalScroll.Value));
                        Point pUp = form.PointToClient(new Point(nodeContainer[Convert.ToInt16(nodeContainer[j].toNodeList[i].Text)].intermediatePointUp.X, nodeContainer[Convert.ToInt16(nodeContainer[j].toNodeList[i].Text)].intermediatePointUp.Y - form.VerticalScroll.Value));

                        //Point pStart = form.PointToClient(nodeContainer[j].startPoint[i]);
                        //Point pRightStart = form.PointToClient(nodeContainer[j].startRightPoint[i]);
                        //Point pLeftStart = form.PointToClient(nodeContainer[j].startLeftPoint[i]);
                        //Point pEnd = form.PointToClient(nodeContainer[Convert.ToInt16(nodeContainer[j].toNodeList[i].Text)].endPoint);
                        //Point pUpRight = form.PointToClient(nodeContainer[j].intermediatePointUpRight);
                        //Point pUpLeft = form.PointToClient(nodeContainer[j].intermediatePointUpLeft);
                        //Point pDownRight = form.PointToClient(nodeContainer[j].intermediatePointDownRight);
                        //Point pDownLeft = form.PointToClient(nodeContainer[j].intermediatePointDownLeft);
                        //Point pMiddleRight = form.PointToClient(nodeContainer[j].intermediatePointMiddleRight);
                        //Point pMiddleLeft = form.PointToClient(nodeContainer[j].intermediatePointMiddleLeft);
                        //Point pUp = form.PointToClient(nodeContainer[Convert.ToInt16(nodeContainer[j].toNodeList[i].Text)].intermediatePointUp);

                        //g.DrawRectangle(pen2, pUpRight.X, pUpRight.Y, 4, 4);
                        //g.DrawRectangle(pen2, pUpLeft.X, pUpLeft.Y, 4, 4);
                        //g.DrawRectangle(pen2, pDownRight.X, pDownRight.Y, 4, 4);
                        //g.DrawRectangle(pen2, pDownLeft.X, pDownLeft.Y, 4, 4);
                        //g.DrawRectangle(pen2, pMiddleRight.X, pMiddleRight.Y, 4, 4);
                        //g.DrawRectangle(pen2, pMiddleLeft.X, pMiddleLeft.Y, 4, 4);
                        //g.DrawRectangle(pen, pUp.X, pUp.Y, 4, 4);
                        //g.DrawRectangle(pen, pRightStart.X, pRightStart.Y, 4, 4);
                        //g.DrawRectangle(pen, pLeftStart.X, pLeftStart.Y, 4, 4);

                        Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));

                        if (LineLength(pMiddleRight, pEnd) < LineLength(pMiddleLeft, pEnd))
                        {
                            Point point = new Point();
                            if (LineLength(pUpRight, pUp) < LineLength(pDownRight, pUp))
                            {
                                point = pUpRight;
                            }
                            else point = pDownRight;
                            pointsList.Add(new Point[] { pRightStart, pMiddleRight, point, pUp, pEnd });
                            penList.Add(new Pen(randomColor,3));
                        }
                        else
                        {
                            Point point = new Point();
                            if (LineLength(pUpLeft, pUp) < LineLength(pDownLeft, pUp))
                            {
                                point = pUpLeft;
                            }
                            else point = pDownLeft;
                            pointsList.Add(new Point[] { pLeftStart, pMiddleLeft, point, pUp, pEnd });
                            penList.Add(new Pen(randomColor,3));

                        }
                    }
                }
            }
        }
        public double LineLength(Point p1, Point p2)
        {
            var lineLength = Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y));
            return lineLength;
        }

        public void DrawLines()
        {
            
            for (int i = 0; i < pointsList.Count; i++)
            {
                 g.DrawCurve(penList[i], pointsList[i]);
            }
            pointsList.Clear();
        }
        
    }
    
}



