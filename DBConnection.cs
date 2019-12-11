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
        SQLiteDataReader readerTask;
        int nodeUIWidth = 197;
        int nodeUIHeight = 50;
        int nodeUIStartOffset = 700;
        int nodeUIoffset = 450;
        private int npc_id;
        Control.ControlCollection Controls;
        public Form4 form;
        public Panel panel;
        List<int> usedNode = new List<int>();
        public List<List<int>> realCount = new List<List<int>>();
        Pen pen = new Pen(Color.Red, 3);
        Pen pen2 = new Pen(Color.Blue, 3);
        Pen pen3 = new Pen(Color.DarkOliveGreen, 3);
        Pen pen4 = new Pen(Color.AliceBlue, 3);
        List<Pen> penList = new List<Pen>();
        private Random rnd = new Random();
        public List<Point[]> pointsList = new List<Point[]>();
        StringBuilder questSb;
        StringBuilder dialogueSb;
        Point try_pEnd;
        Point try_pUp;
        List<Temp> temp;
        List<NodeTemp> nodeTemp = new List<NodeTemp>();
        bool isOpened = false;

        public DBConnection(string BDpath,StringBuilder questSb, StringBuilder dialogueSb)
        {
            DB = new SQLiteConnection("Data Source= " + BDpath);
            cmd = DB.CreateCommand();
            cmdNPCtext = DB.CreateCommand();
            cmdUIcount = DB.CreateCommand();
            cmdCount = DB.CreateCommand();
            cmdUIcount = DB.CreateCommand();
            cmdUIAnswerCount = DB.CreateCommand();
            this.questSb = questSb;
            this.dialogueSb = dialogueSb;
        }

        public void OpenConnection()
        {
            if(!isOpened)
            {
                DB.Open();
                isOpened = true;
            }
        }
        public void CloseConnection()
        {
            if (isOpened)
            {
                DB.Close();
                isOpened = false;
            }
        }

        #region LoadDatesFromDB
        public void GetFromDB(int npc_id, Form4 form, Panel panel)
        {
            this.Controls = panel.Controls;
            this.npc_id = npc_id;
            this.form = form;
            this.panel = panel;
            
            

            ClearNodeUIElements();
            CreateNodeCounteinerList();
            ClearFields();
            LoadNpcText();
            GetNodeAllDates();
            GetRealCountNodes();
            RestructingNodes();
        }

        public int GetUIElementCount()
        {
            cmdUIcount.CommandText = $"select count(*) from 'dialogue_node' where Npc_id = {npc_id}";
            var UIcountResult = Convert.ToInt16(cmdUIcount.ExecuteScalar());
            return UIcountResult;
        }

        //public int GetNpcTextCount()
        //{
        //    countResult = Convert.ToInt16(cmdCount.ExecuteScalar());
        //    return countResult;
        //}

        public void ClearNodeUIElements()
        {
            dellist.Clear();
            nodeContainer.Clear();
            if (Controls.Count != 0)
            {
                for (int i = 0; i < Controls.Count; i++)
                {
                    if (Controls[i].GetType().ToString() == "DialogueEditor.NodeUI")
                    {
                        dellist.Add(Controls[i]);
                    }
                }
            }
            if (dellist.Count != 0)
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

                nodeContainerUI.Add(new NodeUI(UIanswerCountResult, i, form, panel));
            //    nodeContainerUI[i].Visible = true;
                nodeContainerUI[i].Location = new Point((panel.Width / 2) - (nodeUIWidth / 2), nodeUIHeight + (i * nodeUIoffset));
                nodeContainerUI[i].Name = "nodeUI" + i;
                nodeContainerUI[i].groupBox1.Text = "Node ID:" + i;
                Controls.Add(nodeContainerUI[i]);
                nodeContainer.Add(new NodeContainer(nodeContainerUI[i]));
            }
        }
        public void ClearFields()
        {
            for (int i = 0; i < nodeContainer.Count; i++)
            {
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
            }
        }
        public void LoadNpcText()
        {
            cmdNPCtext.CommandText = $"select Npc_text from 'dialogue_node' where Npc_id = {npc_id}";
            reader = cmdNPCtext.ExecuteReader();
            for (int j = 0; j < nodeContainer.Count(); j++)
            {
                reader.Read();
                var npcTextResult = reader.GetValue(0).ToString();
                nodeContainer[j].npcTextBox.Text = npcTextResult;
            }
            reader.Close();
        }
        public void GetNodeAllDates()
        {
            for (int i = 0; i < nodeContainer.Count; i++)
            {
               // cmdCount.CommandText = $"select count(*) from 'dialogue_answers' where Node_id = {i} and Npc_id = {npc_id}";
                cmd.CommandText = $"select * from 'dialogue_answers' where Node_id = {i} and Npc_id = {npc_id}";
                reader = cmd.ExecuteReader();

                for (int j = 0; j < nodeContainerUI[i].count; j++)
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

        public List<NodeTemp> SaveToTemp()
            {
            for (int i = 0; i < nodeContainer.Count(); i++)
            {
                if (nodeContainerUI[i].npcText == null)
                {
                    nodeTemp.Clear();
                    return nodeTemp;
                }
                var npcText = nodeContainerUI[i].npcText;
                temp = new List<Temp>();
                for (int j = 0; j < nodeContainer[i].answerBoxList.Count; j++)
                {
                    var ID = nodeContainerUI[i].answerUIList[j].answerId;

                    var answerBoxText = nodeContainerUI[i].answerUIList[j].answerBoxText;

                    var questIdText = nodeContainerUI[i].answerUIList[j].questIdText;
                    var toNodeText = nodeContainerUI[i].answerUIList[j].toNodeText;
                    var startCheckBoxValue = nodeContainerUI[i].answerUIList[j].startCheckBoxValue;
                    var finishCheckBoxValue = nodeContainerUI[i].answerUIList[j].finishCheckBoxValue;
                    var exitCheckBoxValue = nodeContainerUI[i].answerUIList[j].exitCheckBoxValue;
                    temp.Add(new Temp(ID, answerBoxText, questIdText, toNodeText, startCheckBoxValue, finishCheckBoxValue, exitCheckBoxValue));
                }
                nodeTemp.Add(new NodeTemp(npcText, temp));
            }
            return nodeTemp;
        }
        public void LoadFromTemp(List<NodeTemp> temp)
        {
            if (temp.Count != 0)
            {
                for (int i = 0; i < nodeContainer.Count(); i++)
                {
                    nodeContainer[i].npcTextBox.Text = temp[i].npcText;

                    for (int j = 0; j < nodeContainer[i].answerBoxList.Count; j++)
                    {
                        nodeContainer[i].AnswerIDList[j].Text = temp[i].tempList[j].ID;
                        nodeContainer[i].answerBoxList[j].Text = temp[i].tempList[j].answerBoxText;
                        nodeContainer[i].questIdList[j].Text = temp[i].tempList[j].questIdText;
                        nodeContainer[i].toNodeList[j].Text = temp[i].tempList[j].toNodeText;
                        nodeContainer[i].startCheckBoxList[j].Checked = temp[i].tempList[j].startCheckBoxValue;
                        nodeContainer[i].finishCheckBoxList[j].Checked = temp[i].tempList[j].finishCheckBoxValue;
                        nodeContainer[i].exitCheckBoxList[j].Checked = temp[i].tempList[j].exitCheckBoxValue;
                    }
                }
            }
        }

        public void SaveChangesToDB()
        {
            for (int i = 0; i < nodeContainer.Count(); i++)
            {

                var npcText = nodeContainer[i].npcTextBox.Text;
                cmdNPCtext.CommandText = $"update 'dialogue_node' SET Npc_text ='{npcText}' where Npc_id = {npc_id} and Node_id = {i}";
                WriteCommand(cmdNPCtext.CommandText,"dialogue");
                cmdNPCtext.ExecuteNonQuery();

                for (int j = 0; j < nodeContainer[i].answerBoxList.Count; j++)
                {

                    var ID = nodeContainer[i].AnswerIDList[j].Text;
                    if (ID == "") continue;
                    var answerBoxText = nodeContainer[i].answerBoxList[j].Text;
                    var questIdText = nodeContainer[i].questIdList[j].Text;
                    string toNodeText;
                    if (Convert.ToInt16(nodeContainer[i].toNodeList[j].Text) >= nodeContainer.Count)
                    {
                        MessageBox.Show($"ERROR SAVE: Node '{nodeContainer[i].toNodeList[j].Text}' does not exist");
                        toNodeText = 0.ToString();
                    }
                    else
                    {
                        toNodeText = nodeContainer[i].toNodeList[j].Text;
                    }
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
                    WriteCommand(cmd.CommandText, "dialogue");
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void WriteCommand(string cmd, string value)
        {
            if (value == "quest")
            {
                questSb.AppendFormat(cmd + "; " + "\r\n");
            }

            if (value == "dialogue")
            {
                dialogueSb.AppendFormat(cmd + "; " + "\r\n");
            }
        }
        public void CreateDBPatch(string tableValue)
        {
            var path = @".\DB_Patches\";
            var date = DateTime.Now.ToString("yyyyMMdd-HHmm");
            var dbname = "_world_";
            var table = tableValue;
            var format = ".sql";
            if(tableValue=="quest")
            {
                File.AppendAllText(path + date + dbname + table + format, questSb.ToString());
            }
            if (tableValue == "dialogue")
            {
                File.AppendAllText(path + date + dbname + table + format, dialogueSb.ToString());
            }
            
            MessageBox.Show($"Patch name is:  {date + dbname + table + format}");
        }

        public void CreateNewNode(string npc_text)
        {
            cmd.CommandText = $"insert into 'dialogue_node' (Npc_id, Node_ID, Npc_text) values ('{npc_id}','{nodeContainer.Count}','{npc_text}') ";
            WriteCommand(cmd.CommandText, "dialogue");
            cmd.ExecuteNonQuery();
        }
        public void DeleteNode(int node_id)
        {
            if (node_id == 0)
            {
                MessageBox.Show($"ERROR: can't delete Starting Node ({node_id}) ");
                return;
            }
            var checkBindings = true;

            {
                for (int j = 0; j < nodeContainerUI.Count; j++)
                {
                    for (int i = 0; i < nodeContainerUI[j].answerUIList.Count; i++)
                    {
                        if (nodeContainerUI[j].answerUIList[i].textBox6.Text == node_id.ToString())
                        {
                            checkBindings = false;
                            MessageBox.Show($"ERROR: Node {node_id} used in other nodes");
                            return;

                        }
                        else
                        {
                            checkBindings = true;
                        }
                    }
                }
            }
            if (checkBindings)
            {
                cmd.CommandText = $"delete from 'dialogue_node' where Npc_id={npc_id} and Node_ID={node_id}";
                WriteCommand(cmd.CommandText, "dialogue");
                cmd.ExecuteNonQuery();
            }
        }

        public void CreateNewAnswer(int node_ID)
        {
            cmd.CommandText = $"insert into 'dialogue_answers' (Npc_id, Node_ID, Answer_text, To_node, End_dialogue) values ('{npc_id}','{node_ID}','answer','0','0')";
            WriteCommand(cmd.CommandText, "dialogue");
            cmd.ExecuteNonQuery();
        }

        public void DeleteAnswer(int answer_id)
        {
            cmd.CommandText = $"delete from 'dialogue_answers' where Id ={answer_id}";
            WriteCommand(cmd.CommandText, "dialogue");
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
                    var toNodeValue = nodeContainer[j].toNodeList[i].Text;
                    if (toNodeValue != "" && toNodeValue != "0")
                    {
                        if (!usedNode.Contains(Convert.ToInt32(toNodeValue)))
                        {
                            nodeContainer[j].rCount.Add(Convert.ToInt32(toNodeValue));
                            usedNode.Add(Convert.ToInt32((toNodeValue)));
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
                    try
                    {
                        var num = realCount[j].ElementAt(i);
                        nodeContainerUI[num].Location = new Point((panel.Width / (realCount[j].Count + 1)) - (nodeUIWidth / 2) + (panel.Width * i / (realCount[j].Count + 1)), nodeUIStartOffset + (j * nodeUIoffset));
                        nodeContainer.RemoveAt(num);
                        nodeContainer.Insert(num, new NodeContainer(nodeContainerUI[num]));
                        
                    }
                    catch
                    {
                        MessageBox.Show($@"ERROR: 'Node' is not correct");
                    }

                }
            }
        }
        #endregion

        #region Drawing
        //public void CreateGraph()
        //{
        //    g = panel.CreateGraphics();
        //}

        public (List<Point[]>,List<Pen>) DrawPointsAndLines()
        {
            for (int j = 0; j < usedNode.Count; j++)
            {
                for (int i = 0; i < nodeContainer[j].toNodeList.Count; i++)
                {
                    var toNodeValue = nodeContainer[j].toNodeList[i].Text;
                    if (toNodeValue != "" && toNodeValue != "0")
                    {
                        try
                        {
                            try_pEnd = panel.PointToClient(new Point(nodeContainer[Convert.ToInt16(toNodeValue)].endPoint.X - panel.HorizontalScroll.Value, nodeContainer[Convert.ToInt16(toNodeValue)].endPoint.Y - panel.VerticalScroll.Value));
                            try_pUp = panel.PointToClient(new Point(nodeContainer[Convert.ToInt16(toNodeValue)].intermediatePointUp.X - panel.HorizontalScroll.Value, nodeContainer[Convert.ToInt16(toNodeValue)].intermediatePointUp.Y - panel.VerticalScroll.Value));

                        }
                        catch
                        {
                            MessageBox.Show($"ERROR DRAW: Node '{toNodeValue}' does not exits. Reset to 'Null'");
                            toNodeValue = 0.ToString();
                        }
                        finally
                        {
                            Point pEnd = try_pEnd;
                            Point pUp = try_pUp;
                            Point pRightStart = panel.PointToClient(new Point(nodeContainer[j].startRightPoint[i].X - panel.HorizontalScroll.Value, nodeContainer[j].startRightPoint[i].Y - panel.VerticalScroll.Value));
                            Point pLeftStart = panel.PointToClient(new Point(nodeContainer[j].startLeftPoint[i].X - panel.HorizontalScroll.Value, nodeContainer[j].startLeftPoint[i].Y - panel.VerticalScroll.Value));
                            Point pUpRight = panel.PointToClient(new Point(nodeContainer[j].intermediatePointUpRight.X - panel.HorizontalScroll.Value, nodeContainer[j].intermediatePointUpRight.Y - panel.VerticalScroll.Value));
                            Point pUpLeft = panel.PointToClient(new Point(nodeContainer[j].intermediatePointUpLeft.X - panel.HorizontalScroll.Value, nodeContainer[j].intermediatePointUpLeft.Y - panel.VerticalScroll.Value));
                            Point pDownRight = panel.PointToClient(new Point(nodeContainer[j].intermediatePointDownRight.X - panel.HorizontalScroll.Value, nodeContainer[j].intermediatePointDownRight.Y - panel.VerticalScroll.Value));
                            Point pDownLeft = panel.PointToClient(new Point(nodeContainer[j].intermediatePointDownLeft.X - panel.HorizontalScroll.Value, nodeContainer[j].intermediatePointDownLeft.Y - panel.VerticalScroll.Value));
                            Point pMiddleRight = panel.PointToClient(new Point(nodeContainer[j].intermediatePointMiddleRight.X - panel.HorizontalScroll.Value, nodeContainer[j].intermediatePointMiddleRight.Y - panel.VerticalScroll.Value));
                            Point pMiddleLeft = panel.PointToClient(new Point(nodeContainer[j].intermediatePointMiddleLeft.X - panel.HorizontalScroll.Value, nodeContainer[j].intermediatePointMiddleLeft.Y - panel.VerticalScroll.Value));

                            //For line settings
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
                                pointsList.Add(new Point[] { pRightStart, pMiddleRight, point, pUp });
                                penList.Add(new Pen(randomColor, 3));
                            }
                            else
                            {
                                Point point = new Point();
                                if (LineLength(pUpLeft, pUp) < LineLength(pDownLeft, pUp))
                                {
                                    point = pUpLeft;
                                }
                                else point = pDownLeft;
                                pointsList.Add(new Point[] { pLeftStart, pMiddleLeft, point, pUp });
                                penList.Add(new Pen(randomColor, 3));
                            }
                        }
                    }
                }
            }
            return (pointsList, penList);
        }
        public double LineLength(Point p1, Point p2)
        {
            var lineLength = Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y));
            return lineLength;
        }

        //public void DrawLines()
        //{
        //    for (int i = 0; i < pointsList.Count; i++)
        //    {
        //        penList[i].CustomEndCap = new AdjustableArrowCap(4, 7);
        //        g.DrawCurve(penList[i], pointsList[i]);

        //    }
        //    pointsList.Clear();
        //}
        #endregion

        #region Load Quest from DB
        public string[] LoadTaskTypes()
        {
            OpenConnection();
            cmdCount.CommandText = $"select count(*) from 'quest_task_types'";
            var typesCount = Convert.ToInt16(cmdCount.ExecuteScalar());
            string[] types = new string[typesCount];
            cmd.CommandText = $"select * from 'quest_task_types'";
            reader = cmd.ExecuteReader();
            for (int i = 0; i < typesCount; i++)
            {
                reader.Read();
                types[i] = reader.GetValue(1).ToString();
            }
            reader.Close();
            CloseConnection();
            return types;
        }

        public (string[], string[]) LoadNpcList()
        {
            OpenConnection();
            cmdCount.CommandText = $"select count(*) from 'npc'";
            var npcCount = Convert.ToInt16(cmdCount.ExecuteScalar());
            (string[], string[]) npcListTuple = (new string[npcCount], new string[npcCount]);
            cmd.CommandText = $"select id, npc_name from 'npc'";
            reader = cmd.ExecuteReader();
            for (int i = 0; i < npcCount; i++)
            {
                reader.Read();
                npcListTuple.Item1[i] = reader.GetValue(0).ToString();
                npcListTuple.Item2[i] = reader.GetValue(1).ToString();
            }
            reader.Close();
         //   CloseConnection();
            return npcListTuple;
        }

        public string[] LoadGameEvents()
        {
            OpenConnection();
            cmdCount.CommandText = $"select count(*) from 'game_event_types'";
            var eventsCount = Convert.ToInt16(cmdCount.ExecuteScalar());
            string[] events = new string[eventsCount];
            cmd.CommandText = $"select * from 'game_event_types'";
            reader = cmd.ExecuteReader();
            for (int i = 0; i < eventsCount; i++)
            {
                reader.Read();
                events[i] = reader.GetValue(1).ToString();
            }
            reader.Close();
            CloseConnection();
            return events;
        }

        // public string[] LoadDialogueList()
        public (string[], string[]) LoadDialogueList(string state)
        {
            OpenConnection();
            if (state != "task")
            {
                cmdCount.CommandText = $"select count(*), Id, Answer_text from 'dialogue_answers' where {state}_quest=1";
                cmd.CommandText = $"select Id, Answer_text from 'dialogue_answers' where {state}_quest=1";
            }
            else
            {
                cmdCount.CommandText = $"select count(*), Id, Answer_text from 'dialogue_answers'";
                cmd.CommandText = $"select Id, Answer_text from 'dialogue_answers'";
            }
            var dialogueCount = Convert.ToInt16(cmdCount.ExecuteScalar());
            (string[], string[]) dialogueListTuple = (new string[dialogueCount], new string[dialogueCount]);

            reader = cmd.ExecuteReader();
            for (int i = 0; i < dialogueCount; i++)
            {
                reader.Read();
                dialogueListTuple.Item1[i] = $"{reader.GetValue(0).ToString()}";
                dialogueListTuple.Item2[i] = $"{reader.GetValue(1).ToString()}";
            }
            reader.Close();
            ////  CloseConnection();
            return dialogueListTuple;
        }

        public int GetTasksCount(int questID)
        {
            OpenConnection();
            cmdCount.CommandText = $"select count(*) from 'quest_objectives' where QuestId={questID}";
            var tasksCount = cmdCount.ExecuteScalar();
            CloseConnection();
            return Convert.ToInt16(tasksCount);
        }

        public string GetQuestDescription(int questID)
        {
            OpenConnection();
            cmd.CommandText = $"select Description from 'quest_locale_ru' where QuestId={questID}";
            var questDesciption = cmd.ExecuteScalar().ToString();

            CloseConnection();
            return questDesciption;
        }
        public string GetQuestName(int questID)
        {
            OpenConnection();
            cmd.CommandText = $"select Title from 'quest_locale_ru' where QuestId={questID}";
            var questName = cmd.ExecuteScalar().ToString();
           
            CloseConnection();
            return questName;
        }
        
        public void LoadTaskList(int questID, ref List<TaskUI> taskCounteinerUI)
        {
            OpenConnection();
            cmdUIcount.CommandText = $"select * from 'quest_objectives' inner join 'quest_task_types' on quest_objectives.Type = quest_task_types.id where QuestId={questID}";
            readerTask = cmdUIcount.ExecuteReader();
            for (int i = 0; i < taskCounteinerUI.Count; i++)
            {
                readerTask.Read();
                taskCounteinerUI[i].taskType = readerTask.GetValue(7).ToString();
                taskCounteinerUI[i].targetID = readerTask.GetValue(3).ToString();
                taskCounteinerUI[i].amount = readerTask.GetValue(4).ToString();
                taskCounteinerUI[i].isOptional = readerTask.GetValue(5).ToString();
                taskCounteinerUI[i].taskID = Convert.ToInt32(readerTask.GetValue(0));
            }
            readerTask.Close();
            CloseConnection();

        }

        public string LoadQuestSelectID(int questID, string value)
        {
            OpenConnection();
            cmd.CommandText = $"select * from 'quest' where Id={questID}";
            reader = cmd.ExecuteReader();
            reader.Read();
            if (value == "start")
            {
                var startDialogueId = reader.GetValue(7).ToString();
                reader.Close();
                CloseConnection();
                return startDialogueId;
            }
            if (value == "end")
            {
                var endDialogueId = reader.GetValue(8).ToString();
                reader.Close();
                CloseConnection();
                return endDialogueId;
            }

            else
            {
                CloseConnection();
                throw new Exception("error value");
            }

        }

        public string LoadQuestSelectType(int questID, string value)
        {
            OpenConnection();
            cmd.CommandText = $"select StartQuestEventType,EndQuestEventType from 'quest' where Id={questID}";
            reader = cmd.ExecuteReader();
            reader.Read();
            if (value == "start")
            {
                var id = reader.GetValue(0).ToString();
                reader.Close();
                cmdUIAnswerCount.CommandText = $"select game_events from 'game_event_types' where id ={id}";
                var startQuestEventType = cmdUIAnswerCount.ExecuteScalar().ToString();
                CloseConnection();
                return startQuestEventType;
            }
            else if (value == "end")
            {
                var id = reader.GetValue(1).ToString();
                reader.Close();
                cmdUIAnswerCount.CommandText = $"select game_events from 'game_event_types' where id ={id}";
                var endQuestEventType = cmdUIAnswerCount.ExecuteScalar().ToString();
                CloseConnection();
                return endQuestEventType;
            }
            else
            {
                CloseConnection();
                throw new Exception("error value");
            }
        }

        public (string[], string[]) LoadIdByEventType(string eventType, string state)
        {
            switch (eventType)
            {
                case "DialogAnswerSelect":
                    return LoadDialogueList(state);

                case "NpcDie":
                    return LoadNpcList();

                default:
                    MessageBox.Show($"ERROR: type  \"{eventType}\" doesn't work yet");
                    return (new string[0], new string[0]);
            }
        }

        public (string[], string[]) LoadIdByTaskType(string taskType)
        {
            switch (taskType)
            {
                case "SelectAnswer":
                    return LoadDialogueList("task");

                case "KillNpc":
                    return LoadNpcList();

                default:
                    MessageBox.Show($"ERROR: type  \"{taskType}\" doesn't work yet");
                    return (new string[0], new string[0]);
            }
        }

        public string[] LoadQuestList()
        {
            OpenConnection();
            cmdCount.CommandText = $"select count(*) from 'quest'";
            var questCount = Convert.ToInt16(cmdCount.ExecuteScalar());
            string[] questList = new string[questCount];
            cmd.CommandText = $"select Id from 'quest'";
            reader = cmd.ExecuteReader();
            for (int i = 0; i < questCount; i++)
            {
                reader.Read();
                questList[i] = reader.GetValue(0).ToString();
            }
            reader.Close();
            CloseConnection();
            return questList;

        }
        #endregion

        #region Save Quest
        public void SaveQuestToDB(string startQuestEventType, string startQuestTargetID, string endQuestEventType, string endQuestTargetID, ref List<TaskUI> taskCounteiner, int questId, string questName, string questDescription)
        {
            cmdNPCtext.CommandText = $"select id from 'game_event_types' where game_events='{startQuestEventType}'";
            var startQuestEventTypeForID = cmdNPCtext.ExecuteScalar().ToString();

            cmdNPCtext.CommandText = $"select id from 'game_event_types' where game_events='{endQuestEventType}'";
            var endQuestEventTypeForID = cmdNPCtext.ExecuteScalar().ToString();

            cmd.CommandText = $"update 'quest_locale_ru' SET Title = '{questName}', Description = '{questDescription}'";
            WriteCommand(cmd.CommandText,"quest");
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"update 'quest' SET StartDialogId = '{startQuestTargetID}', EndDialogId = '{endQuestTargetID}', StartQuestEventType = '{startQuestEventTypeForID}', EndQuestEventType = '{endQuestEventTypeForID}' where Id = '{questId}'";
            WriteCommand(cmd.CommandText, "quest");
            cmd.ExecuteNonQuery();

            for (int i = 0; i < taskCounteiner.Count; i++)
            {
                cmdNPCtext.CommandText = $"select id from 'quest_task_types' where type='{taskCounteiner[i].taskType}'";
                var taskTypeForID = cmdNPCtext.ExecuteScalar().ToString();

                cmdNPCtext.CommandText = $"update 'quest_objectives' SET Type='{taskTypeForID}', TargetId='{taskCounteiner[i].targetID}', Amount='{taskCounteiner[i].amount}',isOptional ='{taskCounteiner[i].isOptional}' where QuestId='{questId}' and Id ='{taskCounteiner[i].taskID}'";
                WriteCommand(cmdNPCtext.CommandText, "quest");
                cmdNPCtext.ExecuteNonQuery();
            }

        }
        #endregion

        #region Add new Quest and Task
        public void AddNewQuest(int startDialogId, int endDialogId, int startQuestEventType, int endQuestEventType)
        {
            cmd.CommandText = $"insert into 'quest' (StartDialogId, EndDialogId, StartQuestEventType, EndQuestEventType) values ('{startDialogId}','{endDialogId}','{startQuestEventType}','{endQuestEventType}') ";
            WriteCommand(cmd.CommandText, "quest");
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"select id from 'quest' order by id desc limit 1";
            var npcID = cmd.ExecuteScalar();
            cmd.CommandText = $"insert into 'quest_locale_ru' (QuestId) values ('{npcID}')";
            WriteCommand(cmd.CommandText, "quest");
            cmd.ExecuteNonQuery();
        }

        public void AddNewTask(int questId)
        {
            cmd.CommandText = $"insert into 'quest_objectives' (QuestId) values ('{questId}') ";
            WriteCommand(cmd.CommandText, "quest");
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"select id from 'quest_objectives' order by id desc limit 1";
            var taskID = cmd.ExecuteScalar();
            cmd.CommandText = $"insert into 'quest_objectives_locale_ru' (ObjectiveId) values ('{taskID}')";
            WriteCommand(cmd.CommandText, "quest");
            cmd.ExecuteNonQuery();
        }

        #endregion

        #region Delete Quest and Task
        public void DeleteTask(int taskId)
        {
            OpenConnection();
            cmd.CommandText = $"delete from 'quest_objectives' where Id ={taskId}";
            WriteCommand(cmd.CommandText, "quest");
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"delete from 'quest_objectives_locale_ru' where ObjectiveId ={taskId}";
            WriteCommand(cmd.CommandText, "quest");
            cmd.ExecuteNonQuery();
            CloseConnection();
        }

        public void DeleteQuest(int questId)
        {
            cmd.CommandText = $"delete from 'quest' where Id ={questId}";
            WriteCommand(cmd.CommandText, "quest");
            cmd.ExecuteNonQuery();
            
            cmd.CommandText = $"delete from 'quest_locale_ru' where QuestId ={questId}";
            WriteCommand(cmd.CommandText, "quest");
            cmd.ExecuteNonQuery();

            cmdCount.CommandText = $"select count(id) from 'quest_objectives' where QuestId={questId}";
            var taskCount = Convert.ToInt16(cmdCount.ExecuteScalar());
            cmd.CommandText = $"select id from 'quest_objectives' where QuestId={questId} ";
            var reader = cmd.ExecuteReader();
            for (int i=0;i<taskCount;i++)
            {
                reader.Read();
                var taskID = reader.GetValue(0).ToString();
                cmdUIcount.CommandText = $"delete from 'quest_objectives_locale_ru' where ObjectiveId ={taskID}";
                WriteCommand(cmdUIcount.CommandText, "quest");
                cmdUIcount.ExecuteNonQuery();
            }
            reader.Close();
            cmd.CommandText = $"delete from 'quest_objectives' where QuestId ={questId}";
            WriteCommand(cmd.CommandText, "quest");
            cmd.ExecuteNonQuery();
        }
        #endregion


    }
}



