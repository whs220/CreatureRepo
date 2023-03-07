using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LevelEditor
{
    /// <summary>
    /// Leah Torregiano
    /// 230203
    /// Purpose: Create a simplistic level editor
    /// ================= Main Form =================
    /// </summary>
    public partial class MainForm : Form
    {
        //fields
        private int width;
        private int height;
        private string messageError;
        private editorForm editor;

        /// <summary>
        /// creates an instance of MainForm
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// initializes neccessary fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            //intialize fields
            width = 0;
            height = 0;
            messageError = "";
        }

        /// <summary>
        /// check if data is valid
        /// if yes, create level editor with specified dimensions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCreate_Click(object sender, EventArgs e)
        {
            editor = new editorForm(32, 24);
            editor.ShowDialog();
        }

        /// <summary>
        /// have user choose a file to load
        /// tries to open specified file
        /// say if it's successful or not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            //pick the level to load
            OpenFileDialog opener = new OpenFileDialog();
            opener.Title = "Open a level file";
            opener.Filter = "Level Files|*.level";

            //open editor if file is selected
            DialogResult result = opener.ShowDialog();
            if(result == DialogResult.OK)
            {
                editor = new editorForm(opener.FileName, opener.SafeFileName);
                editor.ShowDialog();
            }
        }
    }
}
