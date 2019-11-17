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

        public TaskUI()
        {
            InitializeComponent();
        }
   
        public string taskType { get { return comboBox2.SelectedText; } set { comboBox2.SelectedItem= value; } }
        public string targetID { get { return comboBox5.SelectedText; } set { comboBox5.SelectedItem = value; } }
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
    }
}
