using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogueEditor
{
   public class NodeContainer
    {
        public List<TextBox> answerBoxList = new List<TextBox>();
        public List<TextBox> questIdList = new List<TextBox>();
        public List<CheckBox> startCheckBoxList = new List<CheckBox>();
        public List<CheckBox> finishCheckBoxList = new List<CheckBox>();
        public List<CheckBox> exitCheckBoxList = new List<CheckBox>();
        public List<TextBox> toNodeList = new List<TextBox>();
        public TextBox npcTextBox;
        public int rCount = 0;
        NodeUI node;

       public NodeContainer(NodeUI node)
        {
            this.node = node;
        
            npcTextBox = node.textBox9;
            answerBoxList.Add(node.textBox1);
            answerBoxList.Add(node.textBox2);
            answerBoxList.Add(node.textBox3);
            answerBoxList.Add(node.textBox4);
            answerBoxList.Add(node.textBox5);

            questIdList.Add(node.textBox10);
            questIdList.Add(node.textBox11);
            questIdList.Add(node.textBox12);
            questIdList.Add(node.textBox13);
            questIdList.Add(node.textBox14);

            startCheckBoxList.Add(node.checkBox6);
            startCheckBoxList.Add(node.checkBox9);
            startCheckBoxList.Add(node.checkBox11);
            startCheckBoxList.Add(node.checkBox13);
            startCheckBoxList.Add(node.checkBox15);

            finishCheckBoxList.Add(node.checkBox7);
            finishCheckBoxList.Add(node.checkBox8);
            finishCheckBoxList.Add(node.checkBox10);
            finishCheckBoxList.Add(node.checkBox12);
            finishCheckBoxList.Add(node.checkBox14);

            exitCheckBoxList.Add(node.checkBox1);
            exitCheckBoxList.Add(node.checkBox2);
            exitCheckBoxList.Add(node.checkBox3);
            exitCheckBoxList.Add(node.checkBox4);
            exitCheckBoxList.Add(node.checkBox5);

            toNodeList.Add(node.textBox6);
            toNodeList.Add(node.textBox7);
            toNodeList.Add(node.textBox8);
            toNodeList.Add(node.textBox15);
            toNodeList.Add(node.textBox16);

        }
    }
}
