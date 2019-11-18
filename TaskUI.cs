﻿using System;
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
        public string targetID { get { return comboBox5.SelectedItem.ToString(); } set { comboBox5.SelectedItem = value; } }
        public string amount { get { return textBox2.Text; } set { textBox2.Text = value; } }
        public string isOptional
        { get
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
               for (int i=0;i<str.Length;i++)
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
        public void ClearTasksUI()
        {
            comboBox2.Items.Clear();
            comboBox5.Items.Clear();
        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           comboBox5.Items.Clear();
           comboBox5.Items.AddRange(form.DB.LoadIdByTaskType(taskType));
        }
    }
}