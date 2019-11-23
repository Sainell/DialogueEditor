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
        private int questId;
        private string dbpath;
        OpenFileDialog ofd = new OpenFileDialog();
        string copyDbDirectory = @".\_tempWorld.bytes";
        bool isClearTempDB = false;
        List<TaskUI> taskContainerUI = new List<TaskUI>();
        public StringBuilder sb = new StringBuilder();

        public Form3(Form2 startform)
        {
            InitializeComponent();
            this.startform = startform;
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DB != null) DB.CloseConnection();
            startform.Show();
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
                    CopyDB();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CopyDB();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            questId = Convert.ToInt16(textBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBUpdate();
        }

        public void CopyDB()
        {
            dbpath = copyDbDirectory;
            label6.Text = copyDbDirectory;
        }

        public void DBUpdate()
        {
            if (dbpath != null)
            {
                DB = new DBConnection(dbpath);
                DB.OpenConnection();
                if (questId != 0)
                {
                    try
                    {
                        comboBox1.Items.Clear();
                        comboBox3.Items.Clear();
                        comboBox6.Items.Clear();
                        comboBox7.Items.Clear();
                        taskContainerUI.Clear();

                        comboBox1.Items.AddRange(DB.LoadGameEvents());
                        comboBox3.Items.AddRange(DB.LoadGameEvents());
                        comboBox1.SelectedItem = DB.LoadQuestSelectType(questId, "start");
                        comboBox3.SelectedItem = DB.LoadQuestSelectType(questId, "end");

                        comboBox6.SelectedItem = DB.LoadQuestSelectID(questId, "start");
                        comboBox7.SelectedItem = DB.LoadQuestSelectID(questId, "end");

                        CreateTaskContainerUI(DB.GetTasksCount(questId));

                        panel2.Location = new Point(panel4.Location.X, panel4.Location.Y + panel4.Height + 2);

                        foreach (TaskUI task in taskContainerUI)
                        {
                            task.taskTypeItems = DB.LoadTaskTypes();
                        }
                        DB.LoadTaskList(questId, ref taskContainerUI, sb);
                    }
                    catch
                    {
                        MessageBox.Show($"ERROR: Quest ID  {questId} doesn't exist");
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                isClearTempDB = true;
            }
            else isClearTempDB = false;
        }
        private void CreateTaskContainerUI(int count)
        {
            panel4.Controls.Clear();
            for (int i=0; i < count; i++)
            {
                taskContainerUI.Add(new TaskUI(this));
                taskContainerUI[i].Parent = panel4;
                taskContainerUI[i].Visible = true;
                taskContainerUI[i].Location = new Point(0,0+(i*48));
                taskContainerUI[i].taskName = "Task " + (i+1);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox6.Items.Clear();
            comboBox6.Items.AddRange(DB.LoadIdByEventType(comboBox1.SelectedItem.ToString()));
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox7.Items.Clear();
            comboBox7.Items.AddRange(DB.LoadIdByEventType(comboBox3.SelectedItem.ToString()));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var startQuestEventType = comboBox1.SelectedItem.ToString();
            var startQuestTargetID = comboBox6.SelectedItem.ToString();
            var endQuestEventType = comboBox3.SelectedItem.ToString();
            var endQuestTargetID = comboBox7.SelectedItem.ToString();

            DB.SaveQuestToDB(startQuestEventType, startQuestTargetID, endQuestEventType, endQuestTargetID, ref taskContainerUI, questId);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DB.AddNewQuest(1,1,1,1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DB.AddNewTask(questId);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DB.DeleteQuest(questId);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DB.CreateDBPatch("quest");
        }

    }
}
