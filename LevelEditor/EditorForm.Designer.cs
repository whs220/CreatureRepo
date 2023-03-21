namespace LevelEditor
{
    partial class editorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupMap = new System.Windows.Forms.GroupBox();
            this.groupSelect = new System.Windows.Forms.GroupBox();
            this.pink = new System.Windows.Forms.Button();
            this.slateGray = new System.Windows.Forms.Button();
            this.purple = new System.Windows.Forms.Button();
            this.white = new System.Windows.Forms.Button();
            this.gray = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.yellow = new System.Windows.Forms.Button();
            this.green = new System.Windows.Forms.Button();
            this.red = new System.Windows.Forms.Button();
            this.blue = new System.Windows.Forms.Button();
            this.lightGray = new System.Windows.Forms.Button();
            this.black = new System.Windows.Forms.Button();
            this.groupCurrentColor = new System.Windows.Forms.GroupBox();
            this.colorCurrent = new System.Windows.Forms.PictureBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.groupSelect.SuspendLayout();
            this.groupCurrentColor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorCurrent)).BeginInit();
            this.SuspendLayout();
            // 
            // groupMap
            // 
            this.groupMap.AutoSize = true;
            this.groupMap.BackColor = System.Drawing.Color.Transparent;
            this.groupMap.Location = new System.Drawing.Point(263, 7);
            this.groupMap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupMap.Name = "groupMap";
            this.groupMap.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupMap.Size = new System.Drawing.Size(889, 800);
            this.groupMap.TabIndex = 0;
            this.groupMap.TabStop = false;
            this.groupMap.Text = "Map";
            // 
            // groupSelect
            // 
            this.groupSelect.Controls.Add(this.pink);
            this.groupSelect.Controls.Add(this.slateGray);
            this.groupSelect.Controls.Add(this.purple);
            this.groupSelect.Controls.Add(this.white);
            this.groupSelect.Controls.Add(this.gray);
            this.groupSelect.Controls.Add(this.button1);
            this.groupSelect.Controls.Add(this.yellow);
            this.groupSelect.Controls.Add(this.green);
            this.groupSelect.Controls.Add(this.red);
            this.groupSelect.Controls.Add(this.blue);
            this.groupSelect.Controls.Add(this.lightGray);
            this.groupSelect.Controls.Add(this.black);
            this.groupSelect.Location = new System.Drawing.Point(23, 7);
            this.groupSelect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupSelect.Name = "groupSelect";
            this.groupSelect.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupSelect.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupSelect.Size = new System.Drawing.Size(215, 288);
            this.groupSelect.TabIndex = 1;
            this.groupSelect.TabStop = false;
            this.groupSelect.Text = "Tile Selector";
            // 
            // pink
            // 
            this.pink.BackColor = System.Drawing.Color.SlateGray;
            this.pink.Location = new System.Drawing.Point(81, 225);
            this.pink.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pink.Name = "pink";
            this.pink.Size = new System.Drawing.Size(53, 47);
            this.pink.TabIndex = 17;
            this.pink.UseVisualStyleBackColor = false;
            this.pink.Click += new System.EventHandler(this.colorSelect_Click);
            // 
            // slateGray
            // 
            this.slateGray.BackColor = System.Drawing.Color.Green;
            this.slateGray.Location = new System.Drawing.Point(145, 19);
            this.slateGray.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.slateGray.Name = "slateGray";
            this.slateGray.Size = new System.Drawing.Size(58, 57);
            this.slateGray.TabIndex = 16;
            this.slateGray.Text = "player";
            this.slateGray.UseVisualStyleBackColor = false;
            this.slateGray.Click += new System.EventHandler(this.colorSelect_Click);
            // 
            // purple
            // 
            this.purple.BackColor = System.Drawing.Color.Pink;
            this.purple.Location = new System.Drawing.Point(15, 129);
            this.purple.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.purple.Name = "purple";
            this.purple.Size = new System.Drawing.Size(89, 45);
            this.purple.TabIndex = 15;
            this.purple.Text = "top";
            this.purple.UseVisualStyleBackColor = false;
            this.purple.Click += new System.EventHandler(this.colorSelect_Click);
            // 
            // white
            // 
            this.white.BackColor = System.Drawing.Color.White;
            this.white.Location = new System.Drawing.Point(110, 129);
            this.white.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.white.Name = "white";
            this.white.Size = new System.Drawing.Size(89, 45);
            this.white.TabIndex = 14;
            this.white.Text = "ground";
            this.white.UseVisualStyleBackColor = false;
            this.white.Click += new System.EventHandler(this.colorSelect_Click);
            // 
            // gray
            // 
            this.gray.BackColor = System.Drawing.Color.Gray;
            this.gray.Location = new System.Drawing.Point(109, 178);
            this.gray.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gray.Name = "gray";
            this.gray.Size = new System.Drawing.Size(89, 43);
            this.gray.TabIndex = 13;
            this.gray.Text = "rightWall";
            this.gray.UseVisualStyleBackColor = false;
            this.gray.Click += new System.EventHandler(this.colorSelect_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Black;
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(15, 178);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 43);
            this.button1.TabIndex = 12;
            this.button1.Text = "leftWall";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.colorSelect_Click);
            // 
            // yellow
            // 
            this.yellow.BackColor = System.Drawing.Color.MediumOrchid;
            this.yellow.Location = new System.Drawing.Point(109, 80);
            this.yellow.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.yellow.Name = "yellow";
            this.yellow.Size = new System.Drawing.Size(89, 45);
            this.yellow.TabIndex = 11;
            this.yellow.Text = "blank";
            this.yellow.UseVisualStyleBackColor = false;
            this.yellow.Click += new System.EventHandler(this.colorSelect_Click);
            // 
            // green
            // 
            this.green.BackColor = System.Drawing.Color.Purple;
            this.green.Location = new System.Drawing.Point(15, 80);
            this.green.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.green.Name = "green";
            this.green.Size = new System.Drawing.Size(89, 45);
            this.green.TabIndex = 10;
            this.green.Text = "platform";
            this.green.UseVisualStyleBackColor = false;
            this.green.Click += new System.EventHandler(this.colorSelect_Click);
            // 
            // red
            // 
            this.red.BackColor = System.Drawing.Color.Red;
            this.red.Location = new System.Drawing.Point(81, 19);
            this.red.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.red.Name = "red";
            this.red.Size = new System.Drawing.Size(58, 57);
            this.red.TabIndex = 9;
            this.red.Text = "hot";
            this.red.UseVisualStyleBackColor = false;
            this.red.Click += new System.EventHandler(this.colorSelect_Click);
            // 
            // blue
            // 
            this.blue.BackColor = System.Drawing.Color.Blue;
            this.blue.Location = new System.Drawing.Point(15, 19);
            this.blue.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.blue.Name = "blue";
            this.blue.Size = new System.Drawing.Size(60, 57);
            this.blue.TabIndex = 8;
            this.blue.Text = "cold";
            this.blue.UseVisualStyleBackColor = false;
            this.blue.Click += new System.EventHandler(this.colorSelect_Click);
            // 
            // lightGray
            // 
            this.lightGray.BackColor = System.Drawing.Color.LightGray;
            this.lightGray.Location = new System.Drawing.Point(145, 225);
            this.lightGray.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lightGray.Name = "lightGray";
            this.lightGray.Size = new System.Drawing.Size(53, 47);
            this.lightGray.TabIndex = 5;
            this.lightGray.Text = "blank";
            this.lightGray.UseVisualStyleBackColor = false;
            this.lightGray.Click += new System.EventHandler(this.colorSelect_Click);
            // 
            // black
            // 
            this.black.BackColor = System.Drawing.Color.Yellow;
            this.black.Location = new System.Drawing.Point(15, 225);
            this.black.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.black.Name = "black";
            this.black.Size = new System.Drawing.Size(60, 47);
            this.black.TabIndex = 4;
            this.black.UseVisualStyleBackColor = false;
            this.black.Click += new System.EventHandler(this.colorSelect_Click);
            // 
            // groupCurrentColor
            // 
            this.groupCurrentColor.Controls.Add(this.colorCurrent);
            this.groupCurrentColor.Location = new System.Drawing.Point(23, 311);
            this.groupCurrentColor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupCurrentColor.Name = "groupCurrentColor";
            this.groupCurrentColor.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupCurrentColor.Size = new System.Drawing.Size(215, 194);
            this.groupCurrentColor.TabIndex = 2;
            this.groupCurrentColor.TabStop = false;
            this.groupCurrentColor.Text = "Current Tile";
            // 
            // colorCurrent
            // 
            this.colorCurrent.BackColor = System.Drawing.Color.LightGray;
            this.colorCurrent.Location = new System.Drawing.Point(42, 42);
            this.colorCurrent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.colorCurrent.Name = "colorCurrent";
            this.colorCurrent.Size = new System.Drawing.Size(132, 118);
            this.colorCurrent.TabIndex = 0;
            this.colorCurrent.TabStop = false;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(54, 518);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(151, 126);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "Save File";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(54, 682);
            this.buttonLoad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(151, 126);
            this.buttonLoad.TabIndex = 4;
            this.buttonLoad.Text = "Load File";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // editorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 817);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.groupCurrentColor);
            this.Controls.Add(this.groupSelect);
            this.Controls.Add(this.groupMap);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "editorForm";
            this.Text = "Level Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.editorForm_FormClosing);
            this.groupSelect.ResumeLayout(false);
            this.groupCurrentColor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.colorCurrent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupMap;
        private System.Windows.Forms.GroupBox groupSelect;
        private System.Windows.Forms.Button lightGray;
        private System.Windows.Forms.Button black;
        private System.Windows.Forms.GroupBox groupCurrentColor;
        private System.Windows.Forms.PictureBox colorCurrent;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Button purple;
        private System.Windows.Forms.Button white;
        private System.Windows.Forms.Button gray;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button yellow;
        private System.Windows.Forms.Button green;
        private System.Windows.Forms.Button red;
        private System.Windows.Forms.Button blue;
        private System.Windows.Forms.Button slateGray;
        private System.Windows.Forms.Button pink;
    }
}