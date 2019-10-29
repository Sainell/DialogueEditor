using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace DialogueEditor
{
    public partial class NodeUI : UserControl
    {
        public List<AnswerUI> answerUIList = new List<AnswerUI>();
        public int count;
        public DBConnection DB;
        public int node_ID;
        public Form1 form;

        public NodeUI(int count, int node_ID, DBConnection DB,Form1 form)
        {
            InitializeComponent();

            this.count = count;
            this.node_ID = node_ID;
            this.DB = DB;
            this.form = form;
  
        }
        
        public void CreateAnswers()
        {
            for (int i = 0; i < count; i++)
            {
                answerUIList.Add(new AnswerUI(this));
                answerUIList[i].Parent = this.groupBox1;
                answerUIList[i].Visible = true;
                answerUIList[i].Location = new Point(textBox9.Location.X,textBox9.Location.Y+50+(i*100));
                answerUIList[i].Name = "AnswerUI" + i;
                answerUIList[i].label13.Text = i.ToString();
            }
        }

        public NodeUI()
        {
            InitializeComponent();
        }

        private void NodeUI_Load(object sender, EventArgs e)
        {
            CreateAnswers();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           DB.CreateNewAnswer(this.node_ID);
           form.DBUpdate();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DB.DeleteNode(node_ID);
            form.DBUpdate();
        }

        public void AnswerDelete(int answerNumber)
        {
            DB.DeleteAnswer(answerNumber);
            form.DBUpdate();
        }
    }
}
