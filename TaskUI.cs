using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogueEditor
{
    public partial class TaskUI : UserControl
    {
        Form3 form;

        public TaskUI(Form3 form)
        {
            InitializeComponent();
            this.form = form;
        }

        public string taskType { get { return comboBox2.SelectedItem.ToString(); } set { comboBox2.SelectedItem = value; } }
        public string targetID
        {
            get
            {
                if (comboBox5.SelectedItem != null)
                {
                    return comboBox5.SelectedItem.ToString();
                }
                else
                {
                    return "1";
                }
            }
            set
            {
                comboBox5.SelectedItem = value;
            }
        }

        public string amount { get { return textBox2.Text; } set { textBox2.Text = value; } }
        public string isOptional
        {
            get
            {
                if (checkBox1.Checked == true)
                {
                    return "1";
                }
                else
                {
                    return "0";
                };
            }

            set { if (value == "0") checkBox1.Checked = false; else checkBox1.Checked = true; }
        }

        public string taskName { get { return label4.Text; } set { label4.Text = value; } }

        public int taskID { get { return Convert.ToInt32(label5.Text); } set { label5.Text += value; } }


        public string[] taskTypeItems
        {
            set
            {
                comboBox2.Items.AddRange(value);
            }
        }
        public string[] targetIdItems
        {
            get
            {
                string[] str = new string[comboBox5.Items.Count];
                for (int i = 0; i < str.Length; i++)
                {
                    str[i] = comboBox5.Items[i].ToString();
                }
                return str;
            }
            set
            {
                comboBox5.Items.AddRange(value);
            }
        }

        public string[] targetValueItems
        {
            get
            {
                string[] str = new string[comboBox1.Items.Count];
                for (int i = 0; i < str.Length; i++)
                {
                    str[i] = comboBox1.Items[i].ToString();
                }
                return str;
            }
            set
            {
                comboBox1.Items.AddRange(value);
            }
        }

        public void ClearTasksUI()
        {
            comboBox2.Items.Clear();
            comboBox5.Items.Clear();
        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (form.DB != null)
            {
                comboBox5.Items.Clear();
                comboBox1.Items.Clear();
                targetIdItems = form.DB.LoadIdByTaskType(taskType).Item1;
                targetValueItems = form.DB.LoadIdByTaskType(taskType).Item2;
            }
            else
            {
                MessageBox.Show("ERROR: DataBase not selected");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (form.DB != null)
            {
                var result = MessageBox.Show("Are you save changes?", "Deleting the Task", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    form.DB.DeleteTask(taskID);
                    form.DBUpdate();
                }

            }
            else
            {
                MessageBox.Show("ERROR: DataBase not selected");
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = comboBox5.SelectedIndex;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox5.SelectedIndex = comboBox1.SelectedIndex;
        }
    }
}
