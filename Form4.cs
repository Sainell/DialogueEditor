using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogueEditor
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        public DBConnection DB;
        private int questId;
        private string dbpath;
        OpenFileDialog ofd = new OpenFileDialog();
        string copyDbDirectory = @".\_tempWorld.bytes";
        bool isClearTempDB = false;
        List<TaskUI> taskContainerUI = new List<TaskUI>();
        public StringBuilder questSb = new StringBuilder();
        public StringBuilder dialogueSb = new StringBuilder();
        public StringBuilder npcSb = new StringBuilder();
        //=====================================================
        private int npc_id; //for dialogs
        private int npcId;
        private List<NodeUI> nodeContainerUI = new List<NodeUI>();
        private List<NodeContainer> nodeContainer = new List<NodeContainer>();
        List<NodeUI> del = new List<NodeUI>();
        List<Control> dellist = new List<Control>();
        public List<List<int>> realCount = new List<List<int>>();
        List<int> usedNode = new List<int>();
        private Random rnd = new Random();
        Pen pen = new Pen(Color.Red, 3);
        Pen pen2 = new Pen(Color.Blue, 3);
        Pen pen3 = new Pen(Color.DarkOliveGreen, 3);
        Pen pen4 = new Pen(Color.AliceBlue, 3);
        List<NodeTemp> temp;
        bool tempSaveFlag = true;
        public int testi = 0;
        public bool paintFlag = false;


        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DB != null) DB.CloseConnection();
        }

        #region QuestEditor
        private void button12_Click(object sender, EventArgs e)
        {
            DBSelect();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            CopyDB();
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                questId = Convert.ToInt16(comboBox2.Text);
            }
            catch
            {
                MessageBox.Show("ERROR: ID cannot be a string, You must enter only Number");
            }
        }


        private void button9_Click(object sender, EventArgs e)
        {
            DBUpdate();
        }

        public void CopyDB()
        {
            dbpath = copyDbDirectory;
            label6.Text = copyDbDirectory;
            label2.Text = copyDbDirectory;
            label19.Text = copyDbDirectory;
            if (dbpath != null)
            {
                DB = new DBConnection(dbpath, questSb, dialogueSb, npcSb);
            }
            else
            {
                MessageBox.Show("ERROR: DataBase not selected");
            }
        }

        public void DBUpdate()
        {
            if (DB != null)
            {
                DB.OpenConnection();
                if (questId != 0)
                {
                    //try
                    //{
                    comboBox3.Items.Clear();
                    comboBox7.Items.Clear();
                    comboBox6.Items.Clear();
                    comboBox5.Items.Clear();
                    comboBox10.Items.Clear();
                    comboBox9.Items.Clear();
                    taskContainerUI.Clear();

                    textBox2.Text = DB.GetQuestName(questId);
                    textBox1.Text = DB.GetQuestDescription(questId);
                    comboBox3.Items.AddRange(DB.LoadGameEvents());
                    comboBox7.Items.AddRange(DB.LoadGameEvents());
                    comboBox3.SelectedItem = DB.LoadQuestSelectType(questId, "start");
                    comboBox7.SelectedItem = DB.LoadQuestSelectType(questId, "end");
                    comboBox6.SelectedItem = DB.LoadQuestSelectID(questId, "start");
                    comboBox10.SelectedItem = DB.LoadQuestSelectID(questId, "end");

                    CreateTaskContainerUI(DB.GetTasksCount(questId));

                    panel6.Location = new Point(panel9.Location.X, panel9.Location.Y + panel9.Height + 2);

                    foreach (TaskUI task in taskContainerUI)
                    {
                        task.taskTypeItems = DB.LoadTaskTypes();
                    }
                    DB.LoadTaskList(questId, ref taskContainerUI);
                    //        }
                    //      catch
                    //  {
                    //     MessageBox.Show($"ERROR: Quest ID  {questId} doesn't exist");
                    // }
                    //    finally
                    //     {
                    DB.CloseConnection();
                    //    }
                }
                else
                {
                    MessageBox.Show("ERROR: Quest ID not selected");
                }
            }
            else
            {
                MessageBox.Show("ERROR: DataBase not selected");
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                isClearTempDB = true;
            }
            else isClearTempDB = false;
        }

        private void CreateTaskContainerUI(int count)
        {
            panel9.Controls.Clear();
            for (int i = 0; i < count; i++)
            {
                taskContainerUI.Add(new TaskUI(this));
                taskContainerUI[i].Parent = panel9;
                taskContainerUI[i].Visible = true;
                taskContainerUI[i].Location = new Point(0, 0 + (i * 48));
                taskContainerUI[i].taskNumber = "Task " + (i + 1);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DB != null)
            {
                comboBox6.Items.Clear();
                comboBox5.Items.Clear();
                comboBox6.Items.AddRange(DB.LoadIdByEventType(comboBox3.SelectedItem.ToString(), "Start").Item1);
                comboBox5.Items.AddRange(DB.LoadIdByEventType(comboBox3.SelectedItem.ToString(), "Start").Item2);
            }

        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DB != null)
            {
                comboBox10.Items.Clear();
                comboBox9.Items.Clear();
                comboBox10.Items.AddRange(DB.LoadIdByEventType(comboBox7.SelectedItem.ToString(), "End").Item1);
                comboBox9.Items.AddRange(DB.LoadIdByEventType(comboBox7.SelectedItem.ToString(), "End").Item2);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (DB != null)
            {
                if (comboBox3.SelectedItem != null && comboBox6.SelectedItem != null && comboBox7.SelectedItem != null && comboBox10.SelectedItem != null)
                {

                    var startQuestEventType = comboBox3.SelectedItem.ToString();
                    //  var intPoint = comboBox6.SelectedItem.ToString().IndexOf(" ");
                    var startQuestTargetID = comboBox6.SelectedItem.ToString();//.Remove(intPoint);
                    var endQuestEventType = comboBox7.SelectedItem.ToString();
                    //  intPoint = comboBox10.SelectedItem.ToString().IndexOf(" ");
                    var endQuestTargetID = comboBox10.SelectedItem.ToString();

                    DB.OpenConnection();
                    DB.SaveQuestToDB(startQuestEventType, startQuestTargetID, endQuestEventType, endQuestTargetID, ref taskContainerUI, questId, textBox2.Text, textBox1.Text);
                    DB.CloseConnection();
                }
                else
                {
                    MessageBox.Show("ERROR: Quest is not Load or some fields have not selected value");
                }
            }
            else
            {
                MessageBox.Show("ERROR: DataBase not selected");
            }
        }
        // add new quest
        private void button11_Click(object sender, EventArgs e)
        {
            if (DB != null)
            {
                DB.OpenConnection();
                DB.AddNewQuest(1, 1, 1, 1);
                DB.CloseConnection();
                MessageBox.Show("New Quest has been created");
            }
            else
            {
                MessageBox.Show("ERROR: DataBase not selected");
            }
        }
        //add new task
        private void button14_Click(object sender, EventArgs e)
        {
            if (DB != null)
            {
                var result = MessageBox.Show("Are you save changes?", "Adding new Task", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    DB.OpenConnection();
                    DB.AddNewTask(questId);
                    DB.CloseConnection();
                    DBUpdate();
                }
            }
            else
            {
                MessageBox.Show("ERROR: DataBase not selected");
            }

        }
        //delete Quest
        private void button8_Click(object sender, EventArgs e)
        {

            if (DB != null)
            {
                if (questId != 0)
                {
                    var result = MessageBox.Show($"Are you sure you want to delete the Quest with ID \" {questId}\" ", "Delete Quest", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        DB.OpenConnection();
                        DB.DeleteQuest(questId);
                        DB.CloseConnection();
                        MessageBox.Show($"The Quest with ID \" {questId}\" has been deleted");
                    }
                }
                else
                {
                    MessageBox.Show("ERROR: Quest ID not selected");
                }
            }
            else
            {
                MessageBox.Show("ERROR: DataBase not selected");
            }

        }
        //create DB patch
        private void button7_Click(object sender, EventArgs e)
        {
            if (DB != null)
            {
                DB.OpenConnection();
                DB.CreateDBPatch("quest");
                DB.CloseConnection();
            }
            else
            {
                MessageBox.Show("ERROR: DataBase not selected");
            }
        }

        private void comboBox2_Click(object sender, EventArgs e)
        {
            if (DB != null)
            {
                comboBox2.Items.Clear();
                comboBox2.Items.AddRange(DB.LoadQuestList());
            }
            else
            {
                MessageBox.Show("ERROR: DataBase not selected");
            }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox5.SelectedIndex = comboBox6.SelectedIndex;
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox6.SelectedIndex = comboBox5.SelectedIndex;
        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox9.SelectedIndex = comboBox10.SelectedIndex;
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox10.SelectedIndex = comboBox9.SelectedIndex;
        }

        #endregion

        #region DialogueEditor

        private void button1_Click(object sender, EventArgs e)
        {
            if (temp != null)
            {
                temp.Clear();
            }
            if (comboBox1.Items.Contains(npc_id.ToString()))
            {
                DialogueDBUpdate();
            }
            else
            {
                MessageBox.Show("ERROR: ID does not exist");
            }

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
        private void comboBox1_TextChanged(object sender, EventArgs e)//
        {
            try
            {
                npc_id = Convert.ToInt16(comboBox1.Text);
                //if (!comboBox1.Items.Contains(npc_id.ToString()))
                //{
                //    MessageBox.Show("ERROR: ID does not exist");
                //}
            }
            catch
            {
                MessageBox.Show("ERROR: ID cannot be a string, You must enter only Number");
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            if (DB != null)
            {
                var pointsLines = DB.DrawPointsAndLines();
                for (int i = 0; i < pointsLines.Item1.Count; i++)
                {
                    pointsLines.Item2[i].CustomEndCap = new AdjustableArrowCap(4, 7);
                    e.Graphics.DrawCurve(pointsLines.Item2[i], pointsLines.Item1[i]);

                }
                pointsLines.Item1.Clear();

            }
        }

        private void panel2_Resize(object sender, EventArgs e)
        {
            if (DB != null)
            {
                if (npc_id != 0)
                {
                    // DialogueDBUpdate();
                    SaveToTempAndLoad();
                }
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

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DB != null) DB.CloseConnection();
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
            DBSelect();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            CopyDB();
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
                        DialogueDBUpdate();
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

        public void DialogueDBUpdate()
        {
            if (DB != null)
            {
                DB = new DBConnection(dbpath, questSb, dialogueSb,npcSb);  // Добавлено sb в конструктор, нужно проверить.

                if (npc_id != 0)
                {
                    DB.OpenConnection();
                    DB.GetFromDB(npc_id, this, panel2);
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

        public void SaveToTempAndLoad()
        {
            if (tempSaveFlag)
            {
                temp = DB.SaveToTemp();
                tempSaveFlag = false;
            }
            if (DB != null)
            {
                DialogueDBUpdate();
                DB.LoadFromTemp(temp);
            }
            tempSaveFlag = true;
        }
        #endregion
        private void DBSelect()
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
                    CopyDB();
                }
            }
        }
        #region NPCEditor
        #endregion
        private void button15_Click(object sender, EventArgs e)
        {
            CopyDB();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            DBSelect();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (DB != null)
            {        
                DB.CreateDBPatch("npc");
            }
            else
            {
                MessageBox.Show("ERROR: DataBase not selected");
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (DB != null)
            {
                DB.OpenConnection();
                if (npcId != 0)
                {
                    label22.Text = "";
                    comboBox12.Items.Clear();
                    comboBox13.Items.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                    textBox6.Clear();
                    textBox7.Clear();
                    textBox8.Clear();
                    textBox9.Clear();
                    textBox10.Clear();
                    textBox11.Clear();
                    textBox12.Clear();
                    

                    label2.Text = npcId.ToString();
                    comboBox12.Items.AddRange(DB.LoadNpcTypes());
                    comboBox13.Items.AddRange(DB.LoadNpcSocialGroups());

                    var NpcValueList = new List<string>();
                    NpcValueList.AddRange(DB.LoadNpcInfo(npcId));

                    label22.Text = npcId.ToString();
                    textBox4.Text = NpcValueList[0];
                    textBox5.Text = NpcValueList[1];
                    textBox6.Text = NpcValueList[2];
                    textBox7.Text = NpcValueList[3];

                    textBox11.Text = NpcValueList[4];
                    textBox12.Text = NpcValueList[5];
                    

                    textBox10.Text = NpcValueList[6];
                    textBox9.Text = NpcValueList[7];
                    textBox8.Text = NpcValueList[8];

                    textBox13.Text = NpcValueList[9];

                    comboBox12.SelectedIndex = Convert.ToInt16(NpcValueList[10])-1;
                    comboBox13.SelectedIndex = Convert.ToInt16(NpcValueList[11])-1;

                }
            }

        }
        private void button16_Click(object sender, EventArgs e)
        {
            if (DB != null)
            {
                if (comboBox12.SelectedItem != null 
                && comboBox13.SelectedItem != null 
                && textBox4.Text != ""
                && textBox5.Text!=""
                && textBox6.Text!=""
                && textBox7.Text!=""
                && textBox8.Text!=""
                && textBox9.Text!=""
                && textBox10.Text!=""
                && textBox11.Text!=""
                && textBox12.Text!="")

                {


                    DB.SaveNpcToDB(npcId, textBox4.Text, comboBox12.Text, comboBox13.Text,textBox13.Text, textBox10.Text, textBox9.Text, textBox8.Text, textBox5.Text, textBox6.Text, textBox7.Text, textBox11.Text, textBox12.Text);

                }
                else
                {
                    MessageBox.Show("ERROR: Quest is not Load or some fields have not selected value");
                }
            }
            else
            {
                MessageBox.Show("ERROR: DataBase not selected");
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (DB != null)
            {
                DB.AddNewNpc();
                MessageBox.Show("New Quest has been created");
            }
            else
            {
                MessageBox.Show("ERROR: DataBase not selected");
            }
        }

        private void comboBox11_TextChanged(object sender, EventArgs e)
        {
            try
            {
                npcId = Convert.ToInt16(comboBox11.Text);
            }
            catch
            {
                MessageBox.Show("ERROR: ID cannot be a string, You must enter only Number");
            }
        }

        private void comboBox11_Click(object sender, EventArgs e)
        {
            if (DB != null)
            {
                comboBox11.Items.Clear();
                comboBox11.Items.AddRange(DB.LoadNpcList().Item1);
            }
            else
            {
                MessageBox.Show("ERROR: DataBase not selected");
            }
        }

    }

} 
