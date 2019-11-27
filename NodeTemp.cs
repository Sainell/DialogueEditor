using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialogueEditor
{
    public class NodeTemp
    {
        public string npcText { get; private set; }
        public List<Temp> tempList = new List<Temp>();

        public NodeTemp(string npcText, List<Temp> tempList)
        {
            this.npcText = npcText;
            this.tempList = tempList;
        }

    }
}
