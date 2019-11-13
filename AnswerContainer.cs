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
        public string answerBox;
        public string questId;
        public bool startCheckBox;
        public bool finishCheckBox;
        public bool exitCheckBox;
        public string toNode;

        public AnswerContainer(AnswerUI answer)
        {
            answerBox = answer.answerBoxText;
            questId = answer.answerId;
            startCheckBox = answer.startCheckBoxValue;
            finishCheckBox = answer.finishCheckBoxValue;
            exitCheckBox = answer.exitCheckBoxValue;
            toNode = answer.toNodeText;
        }
    }
}
