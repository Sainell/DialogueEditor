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
        string answerId;
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
            
        }
    }
}
