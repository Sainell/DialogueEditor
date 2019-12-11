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


        private void button1_Click(object sender, EventArgs e)
        {
            DBUpdate();
        }

        public void CopyDB()
        {
            dbpath = copyDbDirectory;
            label6.Text = copyDbDirectory;
            if (dbpath != null)
            {
          //      DB = new DBConnection(dbpath,sb);
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
                    try
                    {
                        comboBox1.Items.Clear();
                        comboBox3.Items.Clear();
                        comboBox6.Items.Clear();
                        comboBox5.Items.Clear();
                        comboBox7.Items.Clear();
                        comboBox9.Items.Clear();
                        taskContainerUI.Clear();

                        textBox2.Text = DB.GetQuestName(questId);
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
                        DB.LoadTaskList(questId, ref taskContainerUI);
                    }
                    catch
                    {
                        MessageBox.Show($"ERROR: Quest ID  {questId} doesn't exist");
                    }
                    finally
                    {
                        DB.CloseConnection();
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
               // taskContainerUI.Add(new TaskUI(this));
                taskContainerUI[i].Parent = panel4;
                taskContainerUI[i].Visible = true;
                taskContainerUI[i].Location = new Point(0,0+(i*48));
                taskContainerUI[i].taskName = "Task " + (i+1);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DB != null)
            {
                comboBox6.Items.Clear();
                comboBox5.Items.Clear();
            //    comboBox6.Items.AddRange(DB.LoadIdByEventType(comboBox1.SelectedItem.ToString()).Item1);
            //    comboBox5.Items.AddRange(DB.LoadIdByEventType(comboBox1.SelectedItem.ToString()).Item2);
            }
            
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DB != null)
            {
                comboBox7.Items.Clear();
                comboBox9.Items.Clear();
               // comboBox7.Items.AddRange(DB.LoadIdByEventType(comboBox3.SelectedItem.ToString()).Item1);
              //  comboBox9.Items.AddRange(DB.LoadIdByEventType(comboBox3.SelectedItem.ToString()).Item2);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DB != null)
            {
                if (comboBox1.SelectedItem != null && comboBox6.SelectedItem!= null && comboBox3.SelectedItem!= null && comboBox7.SelectedItem!=null)
                {
                    var startQuestEventType = comboBox1.SelectedItem.ToString();
                    var startQuestTargetID = comboBox6.SelectedItem.ToString();
                    var endQuestEventType = comboBox3.SelectedItem.ToString();
                    var endQuestTargetID = comboBox7.SelectedItem.ToString();

                    DB.OpenConnection();
                //    DB.SaveQuestToDB(startQuestEventType, startQuestTargetID, endQuestEventType, endQuestTargetID, ref taskContainerUI, questId, textBox2.Text);
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
        private void button3_Click(object sender, EventArgs e)
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
        private void button7_Click(object sender, EventArgs e)
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
        private void button5_Click(object sender, EventArgs e)
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
               // DB.OpenConnection();
                comboBox2.Items.Clear();
                comboBox2.Items.AddRange(DB.LoadQuestList());
              //  DB.CloseConnection();
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

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox9.SelectedIndex = comboBox7.SelectedIndex;
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox7.SelectedIndex = comboBox9.SelectedIndex;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
