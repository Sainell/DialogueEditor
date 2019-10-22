using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogueEditor
{
    class AnswerContainer
    {
        public TextBox answerBox;
        public TextBox questId;
        public CheckBox startCheckBox;
        public CheckBox finishCheckBox;
        public CheckBox exitCheckBox;
        public TextBox toNode;

        public AnswerContainer(AnswerUI answer)
        {
            answerBox = answer.textBox1;
            questId = answer.textBox10;
            startCheckBox = answer.checkBox6;
            finishCheckBox = answer.checkBox7;
            exitCheckBox = answer.checkBox1;
            toNode = answer.textBox6;
        }
    }
}
