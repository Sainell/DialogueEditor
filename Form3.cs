using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogueEditor
{
    public partial class Form3 : Form
    {
        private Form2 startform;
        public Form3(Form2 startform)
        {
            InitializeComponent();
            this.startform = startform;
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            startform.Show();
        }
    }
}
