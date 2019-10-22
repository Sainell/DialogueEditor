using System;
using System.Collections.Generic;
using System.Drawing;
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
        public List<TextBox> toNodeList = new List<TextBox>();
        public List<CheckBox> startCheckBoxList = new List<CheckBox>();
        public List<CheckBox> finishCheckBoxList = new List<CheckBox>();
        public List<CheckBox> exitCheckBoxList = new List<CheckBox>();
        public List<Point> startRightPoint = new List<Point>();
        public List<Point> startLeftPoint = new List<Point>();
        public List<Label> AnswerIDList = new List<Label>();
        public List<int> rCount = new List<int>();
        public TextBox npcTextBox;
        public Point endPoint;
        public Point intermediatePointUpLeft;
        public Point intermediatePointUpRight;
        public Point intermediatePointDownLeft;
        public Point intermediatePointDownRight;
        public Point intermediatePointMiddleLeft;
        public Point intermediatePointMiddleRight;
        public Point intermediatePointUp;
        public Point intermediatePointDown;
        NodeUI node;

       public NodeContainer(NodeUI node)
        {
            this.node = node;
            npcTextBox = node.textBox9;

            for (int i = 0; i < node.count; i++)
            {
                AnswerIDList.Add(node.answerUIList[i].label13);
                answerBoxList.Add(node.answerUIList[i].textBox1);
                questIdList.Add(node.answerUIList[i].textBox10);
                startCheckBoxList.Add(node.answerUIList[i].checkBox6);
                finishCheckBoxList.Add(node.answerUIList[i].checkBox7);
                exitCheckBoxList.Add(node.answerUIList[i].checkBox1);
                toNodeList.Add(node.answerUIList[i].textBox6);
                startRightPoint.Add(node.answerUIList[i].PointToScreen(new Point(node.answerUIList[i].textBox6.Location.X + node.answerUIList[i].textBox6.Width + 7, node.answerUIList[i].textBox6.Location.Y + node.answerUIList[i].textBox6.Height / 2)));
                startLeftPoint.Add(node.answerUIList[i].PointToScreen(new Point(node.answerUIList[i].textBox6.Location.X - 157, node.answerUIList[i].textBox6.Location.Y + node.answerUIList[i].textBox6.Height / 2)));
            }

            endPoint = node.PointToScreen(new Point(node.groupBox1.Location.X + (node.groupBox1.Width / 2), node.groupBox1.Location.Y + (node.groupBox1.Height / 2)));

            intermediatePointMiddleRight = node.PointToScreen(new Point(node.groupBox1.Location.X + (node.groupBox1.Width * 5 / 4), node.groupBox1.Location.Y + (node.groupBox1.Height / 2)));
            intermediatePointMiddleLeft = node.PointToScreen(new Point(node.groupBox1.Location.X - (node.groupBox1.Width * 1 / 4), node.groupBox1.Location.Y + (node.groupBox1.Height / 2)));

            intermediatePointDownRight = node.PointToScreen(new Point(node.groupBox1.Location.X + (node.groupBox1.Width * 5 / 4), node.groupBox1.Location.Y + (node.groupBox1.Height)));
            intermediatePointDownLeft = node.PointToScreen(new Point(node.groupBox1.Location.X - (node.groupBox1.Width * 1 / 4), node.groupBox1.Location.Y + (node.groupBox1.Height)));

            intermediatePointUpRight = node.PointToScreen(new Point(node.groupBox1.Location.X + (node.groupBox1.Width * 5 / 4), node.groupBox1.Location.Y));
            intermediatePointUpLeft = node.PointToScreen(new Point(node.groupBox1.Location.X - (node.groupBox1.Width * 1 / 4), node.groupBox1.Location.Y));

            intermediatePointUp = node.PointToScreen(new Point(node.groupBox1.Location.X + (node.groupBox1.Width / 2), node.groupBox1.Location.Y - 10));
            intermediatePointDown = node.PointToScreen(new Point(node.groupBox1.Location.X + (node.groupBox1.Width / 2), node.groupBox1.Location.Y + (node.groupBox1.Height) + 10));

        }
    }
}
