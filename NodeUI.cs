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
        public Form4 form;
        public Panel panel;
        public string npcText { get; set; }
        public int id;

        public NodeUI(int count, int node_ID,Form4 form, Panel panel)
        {
            InitializeComponent();

            this.count = count;
            this.node_ID = node_ID;
            this.DB = form.DB;
            this.form = form;
            this.panel = panel;
  
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
            var result = MessageBox.Show("Are you save changes?", "Adding new answer", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                DB.CreateNewAnswer(this.node_ID);
                form.DialogueDBUpdate();
                // form.SaveToTempAndLoad();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you save changes?", "Deleting node", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                DB.DeleteNode(node_ID, id);
                form.DialogueDBUpdate();
                // form.SaveToTempAndLoad();
            }
        }

        public void AnswerDelete(int answerNumber)
        {
            var result = MessageBox.Show("Are you save changes?", "Deleting answer", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                DB.DeleteAnswer(answerNumber);
                form.DialogueDBUpdate();
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            npcText = textBox9.Text;
        }
    }
}
