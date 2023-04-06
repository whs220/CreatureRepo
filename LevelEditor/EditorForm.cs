﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
    /// ================ Editor Form ================
    /// </summary>
    public partial class editorForm : Form
    {
        //fields
        private int width;
        private int height;
        private string fileName;
        private Color color;
        private int gridSize;
        private bool changed;

        /// <summary>
        /// creates an instance of EditorForm from create
        /// using width and height values
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public editorForm(int width, int height)
        {
            InitializeComponent();

            //set field values
            this.width = width;
            this.height = height;            
            
            //default values
            color = Color.LightGray;
            gridSize = groupMap.Height / height;
            changed = false;
            //make picture box grid
            for (int i = 0; i < height; i++)                                                 
            {                                                                                
                for (int j = 0; j < width; j++)                                              
                {                                                                            
                    //picture box properties                                                 
                    PictureBox pictureBox = new PictureBox();
                    
                    //add picture box to controls and to events                              
                    groupMap.Controls.Add(pictureBox);                                     
                    pictureBox.Height = gridSize;                                            
                    pictureBox.Width = gridSize;                                             
                    pictureBox.Location = new Point(                                         
                    j * gridSize,                        
                    i * gridSize);                       
                    pictureBox.BackColor = color;

                    //add to event
                    pictureBox.Click += ChangeColor;
                }                                                                            
            }                                                                                
        }

        /// <summary>
        /// creates an instance of EditorForm from load
        /// using a file name
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="safeFileName"></param>
        public editorForm(string fileName, string safeFileName)
        {
            InitializeComponent();

            //set field values
            this.fileName = safeFileName;
            changed = false;

            //read in file info 
            StreamReader reader = new StreamReader(fileName);

            try
            {
                //field values
                width = 32;
                height = 32;
                color = Color.LightGray;
                gridSize = groupMap.Height / height;
                Color tileColor = Color.LightGray;
                PictureBox temp = new PictureBox();
                

                int row;
                int col;

                #region first run
                //read each line of data in the file
                string lineOfData = reader.ReadLine();
                //store the split data into an array
                string[] objectData = lineOfData.Split(',');

                //determine coords
                row = int.Parse(objectData[0]);
                col = int.Parse(objectData[1]);

                //determine what kind color to make the box
                if (objectData[2] == "ground")
                {
                    tileColor = Color.White;
                }
                else if (objectData[2] == "top")
                {
                    tileColor = Color.Pink;
                }
                else if (objectData[2] == "rightWall")
                {
                    tileColor = Color.Gray;
                }
                else if (objectData[2] == "leftWall")
                {
                    tileColor = Color.Black;
                }
                else if (objectData[2] == "platform")
                {
                    tileColor = Color.Purple;
                }
                else if (objectData[2] == "player")
                {
                    tileColor = Color.Green;
                }
                else if (objectData[2] == "cold")
                {
                    tileColor = Color.Blue;
                }
                else if (objectData[2] == "hot")
                {
                    tileColor = Color.Red;
                }
                else if (objectData[2] == "exit")
                {
                    tileColor = Color.Yellow;
                }
                #endregion

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        if ((row + 1 == i && j == 0) || (row == i && col + 1 == j))
                        {
                            if ((lineOfData = reader.ReadLine()) != null)
                            {
                                //store the split data into an array
                                objectData = lineOfData.Split(',');

                                //determine coords
                                row = int.Parse(objectData[0]);
                                col = int.Parse(objectData[1]);

                                //determine what kind color to make the box
                                if (objectData[2] == "ground")
                                {
                                    tileColor = Color.White;
                                }
                                else if (objectData[2] == "top")
                                {
                                    tileColor = Color.Pink;
                                }
                                else if (objectData[2] == "rightWall")
                                {
                                    tileColor = Color.Gray;
                                }
                                else if (objectData[2] == "leftWall")
                                {
                                    tileColor = Color.Black;
                                }
                                else if (objectData[2] == "platform")
                                {
                                    tileColor = Color.Purple;
                                }
                                else if (objectData[2] == "player")
                                {
                                    tileColor = Color.Green;
                                }
                                else if (objectData[2] == "cold")
                                {
                                    tileColor = Color.Blue;
                                }
                                else if (objectData[2] == "hot")
                                {
                                    tileColor = Color.Red;
                                }
                                else if (objectData[2] == "exit")
                                {
                                    tileColor = Color.Yellow;
                                }
                            }
                        }

                        if (row == i && col == j)
                        {
                            color = tileColor;
                        }
                        else
                        {
                            color = Color.LightGray;
                        }

                        //picture box properties                                                 
                        PictureBox pictureBox = new PictureBox();

                            //add picture box to controls and to events                              
                            groupMap.Controls.Add(pictureBox);
                            pictureBox.Height = gridSize;
                            pictureBox.Width = gridSize;
                            pictureBox.Location = new Point(
                            j * gridSize,
                            i * gridSize);
                            pictureBox.BackColor = color;
                            

                            //add to event
                            pictureBox.Click += ChangeColor;
                        
                    }
                }
                //set window title
                this.Text = "Level Editor - " + this.fileName;
                //message to show successful save
                MessageBox.Show("File loaded successfully", "File loaded");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// choose what color to use
        /// set current color to the color selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void colorSelect_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button button = (Button)sender;
                color = button.BackColor;
                colorCurrent.BackColor = color;
            }
        }

        /// <summary>
        /// changes color of the picture box that was clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ChangeColor(object sender, EventArgs e)
        {
            if (sender is PictureBox)
            {
                PictureBox pictureBox = (PictureBox)sender;
                pictureBox.BackColor = color;
            }
            //add asterik to title if change was made
            if (!changed)
            {
                this.Text += "*";
                changed = true;
            }
        }

        /// <summary>
        /// save file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            //open save file dialog
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Save a level file";
            save.Filter = "Level Files|*.level";

            DialogResult result = save.ShowDialog();

            if(result == DialogResult.OK)
            {
                //write data to a file
                StreamWriter writer = new StreamWriter(save.FileName);
                for (int i = 0; i < 32; i++)
                {
                    for (int j = 0; j < 32; j++)
                    {
                        switch (groupMap.Controls[(i*32)+j].BackColor.ToArgb())
                        {
                            case -16181: //pink
                                writer.WriteLine(i + "," + j + "," + "top");
                                break;
                            case -16776961: //blue
                                writer.WriteLine(i + "," + j + ","+"cold");
                                break;
                            case -65536: //red
                                writer.WriteLine(i + "," + j + "," + "hot");
                                break;
                            case -16744448: //green
                                writer.WriteLine(i + "," + j + "," + "player");
                                break;
                            case -256: //yellow
                                writer.WriteLine(i + "," + j + "," + "exit");
                                break;
                            case -8388480: //purple
                                writer.WriteLine(i + "," + j + "," + "platform");
                                break;
                            case -1: //white
                                writer.WriteLine(i + "," + j + "," + "ground");
                                break;
                            case -16777216: //black                                    
                                writer.WriteLine(i + "," + j + "," + "leftWall");     
                                break;                                                 
                            case -8355712: //gray
                                writer.WriteLine(i + "," + j + "," + "rightWall");
                                break;
                            case -9404272: //slate gray
                                //writer.WriteLine(i + "," + j + "," + "");
                                break;
                            default:
                                break;
                        }
                    }
                }
                writer.Close();

                //shortened file name to add to title
                int split = 0;
                for(int i = 0; i < save.FileName.Length; i++)
                {
                    if (save.FileName[i] == '\\')
                    {
                        split = i+1;
                    }
                }
                fileName = save.FileName.Substring(split);

                this.Text = "Level Editor - " + fileName;

                //file no longer has unsaved changes
                changed = false;
                //tell user the file was saved successfully
                MessageBox.Show("File saved successfully", "File saved");
            }
        }

        /// <summary>
        /// load a level file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            //open an open file dialog
            OpenFileDialog opener = new OpenFileDialog();
            opener.Title = "Open a level file";
            opener.Filter = "Level Files|*.level";

            DialogResult result = opener.ShowDialog();

            //read data to respective values ------------------------------------------//
            StreamReader reader = new StreamReader(opener.FileName);                   //
            fileName = opener.SafeFileName;                                            //
                                                                                       //
            if (result == DialogResult.OK)                                             //
            {                                                                          //
                groupMap.Controls.Clear();                                             //
                try                                                                    //
                {                                                                      //
                    width = 32;                                                        //
                    height = 32;                                                       //
                    gridSize = groupMap.Height / height;                               //

                    Color tileColor = Color.LightGray;

                    int row;
                    int col;


                    #region first run
                    //read each line of data in the file
                    string lineOfData = reader.ReadLine();
                    //store the split data into an array
                    string[] objectData = lineOfData.Split(',');

                    //determine coords
                    row = int.Parse(objectData[0]);
                    col = int.Parse(objectData[1]);

                    //determine what kind color to make the box
                    if (objectData[2] == "ground")
                    {
                        tileColor = Color.White;
                    }
                    else if (objectData[2] == "top")
                    {
                        tileColor = Color.Pink;
                    }
                    else if (objectData[2] == "rightWall")
                    {
                        tileColor = Color.Gray;
                    }
                    else if (objectData[2] == "leftWall")
                    {
                        tileColor = Color.Black;
                    }
                    else if (objectData[2] == "platform")
                    {
                        tileColor = Color.Purple;
                    }
                    else if (objectData[2] == "player")
                    {
                        tileColor = Color.Green;
                    }
                    else if (objectData[2] == "cold")
                    {
                        tileColor = Color.Blue;
                    }
                    else if (objectData[2] == "hot")
                    {
                        tileColor = Color.Red;
                    }
                    else if (objectData[2] == "exit")
                    {
                        tileColor = Color.Yellow;
                    }
                    #endregion

                    for (int i = 0; i < height; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            if ((row + 1 == i && j == 0) || (row == i && col + 1 == j))
                            {
                                if ((lineOfData = reader.ReadLine()) != null)
                                {
                                    //store the split data into an array
                                    objectData = lineOfData.Split(',');

                                    //determine coords
                                    row = int.Parse(objectData[0]);
                                    col = int.Parse(objectData[1]);

                                    //determine what kind color to make the box
                                    if (objectData[2] == "ground")
                                    {
                                        tileColor = Color.White;
                                    }
                                    else if (objectData[2] == "top")
                                    {
                                        tileColor = Color.Pink;
                                    }
                                    else if (objectData[2] == "rightWall")
                                    {
                                        tileColor = Color.Gray;
                                    }
                                    else if (objectData[2] == "leftWall")
                                    {
                                        tileColor = Color.Black;
                                    }
                                    else if (objectData[2] == "platform")
                                    {
                                        tileColor = Color.Purple;
                                    }
                                    else if (objectData[2] == "player")
                                    {
                                        tileColor = Color.Green;
                                    }
                                    else if (objectData[2] == "cold")
                                    {
                                        tileColor = Color.Blue;
                                    }
                                    else if (objectData[2] == "hot")
                                    {
                                        tileColor = Color.Red;
                                    }
                                    else if (objectData[2] == "exit")
                                    {
                                        tileColor = Color.Yellow;
                                    }
                                }
                            }
                            if (row == i && col == j)
                            {
                                color = tileColor;
                            }
                            else
                            {
                                color = Color.LightGray;
                            }

                            //picture box properties                                                 
                            PictureBox pictureBox = new PictureBox();

                            //add picture box to controls and to events                              
                            groupMap.Controls.Add(pictureBox);
                            pictureBox.Height = gridSize;
                            pictureBox.Width = gridSize;
                            pictureBox.Location = new Point(
                            j * gridSize,
                            i * gridSize);
                            pictureBox.BackColor = color;

                            //add to event
                            pictureBox.Click += ChangeColor;

                        }
                    }

                }                                                                      //
                catch(Exception ex)                                                    //
                {                                                                      //
                    MessageBox.Show(ex.Message);                                       //
                }                                                                      //
                //---------------------------------------------------------------------//

                //put file name in title
                this.Text = "Level Editor - " + fileName;
                //no unsaved changes
                changed = false;
                //tell user file was successfully loaded
                MessageBox.Show("File loaded successfully", "File loaded");
            }
        }

        /// <summary>
        /// close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if there are unsaved changes
            if (changed)
            {
                //check if user wants to save first
                DialogResult result = MessageBox.Show("There are unsaved changes. Are you sure you want to quit?", "Unsaved changes", MessageBoxButtons.YesNo);
                //if they dont want to save first
                if(result == DialogResult.Yes)
                {
                    //do nothing
                }
                //if they want to save
                else
                {
                    //cancel action
                    e.Cancel = true;
                }
            }
        }
    }
}
