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
    public partial class AnswerUI : UserControl
    {

        NodeUI node;
        public string answerId { get; set; }
        public string answerBoxText { get; set; }
        public string questIdText { get; set; }
        public string toNodeText { get; set; }
        public bool startCheckBoxValue { get; set; }
        public bool finishCheckBoxValue { get; set; }
        public bool exitCheckBoxValue { get; set; }
        public bool taskCheckBoxValue { get; set; }


        public AnswerUI(NodeUI node)
        {
            InitializeComponent();
            this.node = node;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            node.AnswerDelete(Convert.ToInt16(answerId));
        }

        private void label13_TextChanged(object sender, EventArgs e)
        {
            answerId = label13.Text;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            toNodeText = textBox6.Text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            answerBoxText = textBox1.Text;
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            questIdText = textBox10.Text;
            if(textBox10.Text == "0")
            {
                checkBox2.Enabled = false;
                checkBox6.Enabled = false;
                checkBox7.Enabled = false;
                checkBox2.Checked = false;
                checkBox6.Checked = false;
                checkBox7.Checked = false;
            }
            else
            {
                checkBox2.Enabled = true;
                checkBox6.Enabled = true;
                checkBox7.Enabled = true;
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            startCheckBoxValue = checkBox6.Checked;
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            finishCheckBoxValue = checkBox7.Checked;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            exitCheckBoxValue = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            taskCheckBoxValue = checkBox2.Checked;
        }
    }
}
