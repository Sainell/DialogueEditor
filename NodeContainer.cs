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
        public List<CheckBox> startCheckBoxList = new List<CheckBox>();
        public List<CheckBox> finishCheckBoxList = new List<CheckBox>();
        public List<CheckBox> exitCheckBoxList = new List<CheckBox>();
        public List<TextBox> toNodeList = new List<TextBox>();
        public List<int> rCount = new List<int>();
        public List<Point> startPoint = new List<Point>();
        public List<Point> startRightPoint = new List<Point>();
        public List<Point> startLeftPoint = new List<Point>();
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

            answerBoxList.Add(node.textBox1);
            answerBoxList.Add(node.textBox2);
            answerBoxList.Add(node.textBox3);
            answerBoxList.Add(node.textBox4);

            questIdList.Add(node.textBox10);
            questIdList.Add(node.textBox11);
            questIdList.Add(node.textBox12);
            questIdList.Add(node.textBox13);

            startCheckBoxList.Add(node.checkBox6);
            startCheckBoxList.Add(node.checkBox9);
            startCheckBoxList.Add(node.checkBox11);
            startCheckBoxList.Add(node.checkBox13);

            finishCheckBoxList.Add(node.checkBox7);
            finishCheckBoxList.Add(node.checkBox8);
            finishCheckBoxList.Add(node.checkBox10);
            finishCheckBoxList.Add(node.checkBox12);

            exitCheckBoxList.Add(node.checkBox1);
            exitCheckBoxList.Add(node.checkBox2);
            exitCheckBoxList.Add(node.checkBox3);
            exitCheckBoxList.Add(node.checkBox4);

            toNodeList.Add(node.textBox6);
            toNodeList.Add(node.textBox7);
            toNodeList.Add(node.textBox8);
            toNodeList.Add(node.textBox15);

            startPoint.Add(node.PointToScreen(new Point(node.textBox6.Location.X + node.textBox6.Width / 2, node.textBox6.Location.Y + node.textBox6.Height / 2)));
            startPoint.Add(node.PointToScreen(new Point(node.textBox7.Location.X + node.textBox7.Width / 2, node.textBox7.Location.Y + node.textBox7.Height / 2)));
            startPoint.Add(node.PointToScreen(new Point(node.textBox8.Location.X + node.textBox8.Width / 2, node.textBox8.Location.Y + node.textBox8.Height / 2)));
            startPoint.Add(node.PointToScreen(new Point(node.textBox15.Location.X + node.textBox15.Width / 2, node.textBox15.Location.Y + node.textBox15.Height / 2)));

            startRightPoint.Add(node.PointToScreen(new Point(node.textBox6.Location.X + node.textBox6.Width + 7, node.textBox6.Location.Y + node.textBox6.Height / 2)));
            startRightPoint.Add(node.PointToScreen(new Point(node.textBox7.Location.X + node.textBox7.Width + 7, node.textBox7.Location.Y + node.textBox7.Height / 2)));
            startRightPoint.Add(node.PointToScreen(new Point(node.textBox8.Location.X + node.textBox8.Width + 7, node.textBox8.Location.Y + node.textBox8.Height / 2)));
            startRightPoint.Add(node.PointToScreen(new Point(node.textBox15.Location.X + node.textBox15.Width + 7, node.textBox15.Location.Y + node.textBox15.Height / 2)));

            startLeftPoint.Add(node.PointToScreen(new Point(node.textBox6.Location.X - 157, node.textBox6.Location.Y + node.textBox6.Height / 2)));
            startLeftPoint.Add(node.PointToScreen(new Point(node.textBox7.Location.X - 157, node.textBox7.Location.Y + node.textBox7.Height / 2)));
            startLeftPoint.Add(node.PointToScreen(new Point(node.textBox8.Location.X - 157, node.textBox8.Location.Y + node.textBox8.Height / 2)));
            startLeftPoint.Add(node.PointToScreen(new Point(node.textBox15.Location.X - 157, node.textBox15.Location.Y + node.textBox15.Height / 2)));

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
