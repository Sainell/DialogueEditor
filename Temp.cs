using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialogueEditor
{
    public class Temp
    {
       // public string npcText { get; private set; }
        public string ID{ get; private set; }
        public string answerBoxText{ get; private set; }
        public string questIdText{ get; private set; }
        public string toNodeText{ get; private set; }
        public bool startCheckBoxValue{ get; private set; }
        public bool finishCheckBoxValue{ get; private set; }
        public bool exitCheckBoxValue{ get; private set; }

        // public Temp(string npcText,string ID,string answerBoxText,string questIdText,string toNodeText, bool startCheckBoxValue,bool finishCheckBoxValue,bool exitCheckBoxValue)
        public Temp(string ID, string answerBoxText, string questIdText, string toNodeText, bool startCheckBoxValue, bool finishCheckBoxValue, bool exitCheckBoxValue)
        {
            // this.npcText = npcText;
            this.ID = ID;
            this.answerBoxText = answerBoxText;
            this.questIdText = questIdText;
            this.toNodeText = toNodeText;
            this.startCheckBoxValue = startCheckBoxValue;
            this.finishCheckBoxValue = finishCheckBoxValue;
            this.exitCheckBoxValue = exitCheckBoxValue;
        }
    }
}
