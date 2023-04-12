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
            this.components = new System.ComponentModel.Container();
            this.groupMap = new System.Windows.Forms.GroupBox();
            this.groupSelect = new System.Windows.Forms.GroupBox();
            this.corner4 = new System.Windows.Forms.Button();
            this.corner3 = new System.Windows.Forms.Button();
            this.corner2 = new System.Windows.Forms.Button();
            this.corner1 = new System.Windows.Forms.Button();
            this.pillar = new System.Windows.Forms.Button();
            this.CGleft = new System.Windows.Forms.Button();
            this.CGright = new System.Windows.Forms.Button();
            this.filler = new System.Windows.Forms.Button();
            this.player = new System.Windows.Forms.Button();
            this.top = new System.Windows.Forms.Button();
            this.ground = new System.Windows.Forms.Button();
            this.rightWall = new System.Windows.Forms.Button();
            this.leftWall = new System.Windows.Forms.Button();
            this.blank = new System.Windows.Forms.Button();
            this.platform = new System.Windows.Forms.Button();
            this.hot = new System.Windows.Forms.Button();
            this.cold = new System.Windows.Forms.Button();
            this.lightGray = new System.Windows.Forms.Button();
            this.exit = new System.Windows.Forms.Button();
            this.groupCurrentColor = new System.Windows.Forms.GroupBox();
            this.colorCurrent = new System.Windows.Forms.PictureBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
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
            this.groupSelect.Controls.Add(this.corner4);
            this.groupSelect.Controls.Add(this.corner3);
            this.groupSelect.Controls.Add(this.corner2);
            this.groupSelect.Controls.Add(this.corner1);
            this.groupSelect.Controls.Add(this.pillar);
            this.groupSelect.Controls.Add(this.CGleft);
            this.groupSelect.Controls.Add(this.CGright);
            this.groupSelect.Controls.Add(this.filler);
            this.groupSelect.Controls.Add(this.player);
            this.groupSelect.Controls.Add(this.top);
            this.groupSelect.Controls.Add(this.ground);
            this.groupSelect.Controls.Add(this.rightWall);
            this.groupSelect.Controls.Add(this.leftWall);
            this.groupSelect.Controls.Add(this.blank);
            this.groupSelect.Controls.Add(this.platform);
            this.groupSelect.Controls.Add(this.hot);
            this.groupSelect.Controls.Add(this.cold);
            this.groupSelect.Controls.Add(this.lightGray);
            this.groupSelect.Controls.Add(this.exit);
            this.groupSelect.Location = new System.Drawing.Point(23, 7);
            this.groupSelect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupSelect.Name = "groupSelect";
            this.groupSelect.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupSelect.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupSelect.Size = new System.Drawing.Size(215, 612);
            this.groupSelect.TabIndex = 1;
            this.groupSelect.TabStop = false;
            this.groupSelect.Text = "Tile Selector";
            // 
            // corner4
            // 
            this.corner4.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.corner4.Location = new System.Drawing.Point(79, 389);
            this.corner4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.corner4.Name = "corner4";
            this.corner4.Size = new System.Drawing.Size(58, 58);
            this.corner4.TabIndex = 22;
            this.corner4.Text = "corner4";
            this.corner4.UseVisualStyleBackColor = false;
            this.corner4.Click += new System.EventHandler(this.colorSelect_Click);
            // 
            // corner3
            // 
            this.corner3.BackColor = System.Drawing.Color.Aquamarine;
            this.corner3.Location = new System.Drawing.Point(15, 390);
            this.corner3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.corner3.Name = "corner3";
            this.corner3.Size = new System.Drawing.Size(58, 58);
            this.corner3.TabIndex = 23;
            this.corner3.Text = "corner3";
            this.corner3.UseVisualStyleBackColor = false;
            this.corner3.Click += new System.EventHandler(this.colorSelect_Click);
            // 
            // corner2
            // 
            this.corner2.BackColor = System.Drawing.Color.Goldenrod;
            this.corner2.Location = new System.Drawing.Point(79, 328);
            this.corner2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.corner2.Name = "corner2";
            this.corner2.Size = new System.Drawing.Size(58, 58);
            this.corner2.TabIndex = 22;
            this.corner2.Text = "corner2";
            this.corner2.UseVisualStyleBackColor = false;
            this.corner2.Click += new System.EventHandler(this.colorSelect_Click);
            // 
            // corner1
            // 
            this.corner1.BackColor = System.Drawing.Color.Honeydew;
            this.corner1.Location = new System.Drawing.Point(15, 328);
            this.corner1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.corner1.Name = "corner1";
            this.corner1.Size = new System.Drawing.Size(58, 58);
            this.corner1.TabIndex = 21;
            this.corner1.Text = "corner1";
            this.corner1.UseVisualStyleBackColor = false;
            this.corner1.Click += new System.EventHandler(this.colorSelect_Click);
            // 
            // pillar
            // 
            this.pillar.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.pillar.Location = new System.Drawing.Point(143, 267);
            this.pillar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pillar.Name = "pillar";
            this.pillar.Size = new System.Drawing.Size(58, 58);
            this.pillar.TabIndex = 20;
            this.pillar.Text = "pillar";
            this.pillar.UseVisualStyleBackColor = false;
            this.pillar.Click += new System.EventHandler(this.colorSelect_Click);
            // 
            // CGleft
            // 
            this.CGleft.BackColor = System.Drawing.Color.Sienna;
            this.CGleft.Location = new System.Drawing.Point(79, 266);
            this.CGleft.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CGleft.Name = "CGleft";
            this.CGleft.Size = new System.Drawing.Size(58, 58);
            this.CGleft.TabIndex = 19;
            this.CGleft.Text = "CGleft";
            this.CGleft.UseVisualStyleBackColor = false;
            this.CGleft.Click += new System.EventHandler(this.colorSelect_Click);
            // 
            // CGright
            // 
            this.CGright.BackColor = System.Drawing.Color.Fuchsia;
            this.CGright.Location = new System.Drawing.Point(15, 266);
            this.CGright.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CGright.Name = "CGright";
            this.CGright.Size = new System.Drawing.Size(58, 58);
            this.CGright.TabIndex = 18;
            this.CGright.Text = "CGright";
            this.CGright.UseVisualStyleBackColor = false;
            this.CGright.Click += new System.EventHandler(this.colorSelect_Click);
            // 
            // filler
            // 
            this.filler.BackColor = System.Drawing.Color.LightBlue;
            this.filler.Location = new System.Drawing.Point(79, 204);
            this.filler.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.filler.Name = "filler";
            this.filler.Size = new System.Drawing.Size(58, 58);
            this.filler.TabIndex = 17;
            this.filler.Text = "filler";
            this.filler.UseVisualStyleBackColor = false;
            this.filler.Click += new System.EventHandler(this.colorSelect_Click);
            // 
            // player
            // 
            this.player.BackColor = System.Drawing.Color.Green;
            this.player.Location = new System.Drawing.Point(145, 19);
            this.player.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.player.Name = "player";
            this.player.Size = new System.Drawing.Size(58, 58);
            this.player.TabIndex = 16;
            this.player.Text = "player";
            this.player.UseVisualStyleBackColor = false;
            this.player.Click += new System.EventHandler(this.colorSelect_Click);
            // 
            // top
            // 
            this.top.BackColor = System.Drawing.Color.Pink;
            this.top.Location = new System.Drawing.Point(143, 81);
            this.top.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.top.Name = "top";
            this.top.Size = new System.Drawing.Size(58, 58);
            this.top.TabIndex = 15;
            this.top.Text = "top";
            this.top.UseVisualStyleBackColor = false;
            this.top.Click += new System.EventHandler(this.colorSelect_Click);
            // 
            // ground
            // 
            this.ground.BackColor = System.Drawing.Color.White;
            this.ground.Location = new System.Drawing.Point(15, 142);
            this.ground.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ground.Name = "ground";
            this.ground.Size = new System.Drawing.Size(58, 58);
            this.ground.TabIndex = 14;
            this.ground.Text = "ground";
            this.ground.UseVisualStyleBackColor = false;
            this.ground.Click += new System.EventHandler(this.colorSelect_Click);
            // 
            // rightWall
            // 
            this.rightWall.BackColor = System.Drawing.Color.Gray;
            this.rightWall.Location = new System.Drawing.Point(143, 143);
            this.rightWall.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rightWall.Name = "rightWall";
            this.rightWall.Size = new System.Drawing.Size(58, 58);
            this.rightWall.TabIndex = 13;
            this.rightWall.Text = "rightWall";
            this.rightWall.UseVisualStyleBackColor = false;
            this.rightWall.Click += new System.EventHandler(this.colorSelect_Click);
            // 
            // leftWall
            // 
            this.leftWall.BackColor = System.Drawing.Color.Black;
            this.leftWall.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.leftWall.Location = new System.Drawing.Point(79, 142);
            this.leftWall.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.leftWall.Name = "leftWall";
            this.leftWall.Size = new System.Drawing.Size(58, 58);
            this.leftWall.TabIndex = 12;
            this.leftWall.Text = "leftWall";
            this.leftWall.UseVisualStyleBackColor = false;
            this.leftWall.Click += new System.EventHandler(this.colorSelect_Click);
            // 
            // blank
            // 
            this.blank.BackColor = System.Drawing.Color.MediumOrchid;
            this.blank.Location = new System.Drawing.Point(79, 80);
            this.blank.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.blank.Name = "blank";
            this.blank.Size = new System.Drawing.Size(58, 58);
            this.blank.TabIndex = 11;
            this.blank.Text = "spring";
            this.blank.UseVisualStyleBackColor = false;
            this.blank.Click += new System.EventHandler(this.colorSelect_Click);
            // 
            // platform
            // 
            this.platform.BackColor = System.Drawing.Color.Purple;
            this.platform.Location = new System.Drawing.Point(15, 80);
            this.platform.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.platform.Name = "platform";
            this.platform.Size = new System.Drawing.Size(58, 58);
            this.platform.TabIndex = 10;
            this.platform.Text = "platform";
            this.platform.UseVisualStyleBackColor = false;
            this.platform.Click += new System.EventHandler(this.colorSelect_Click);
            // 
            // hot
            // 
            this.hot.BackColor = System.Drawing.Color.Red;
            this.hot.Location = new System.Drawing.Point(79, 18);
            this.hot.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.hot.Name = "hot";
            this.hot.Size = new System.Drawing.Size(58, 58);
            this.hot.TabIndex = 9;
            this.hot.Text = "hot";
            this.hot.UseVisualStyleBackColor = false;
            this.hot.Click += new System.EventHandler(this.colorSelect_Click);
            // 
            // cold
            // 
            this.cold.BackColor = System.Drawing.Color.Blue;
            this.cold.Location = new System.Drawing.Point(15, 19);
            this.cold.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cold.Name = "cold";
            this.cold.Size = new System.Drawing.Size(58, 58);
            this.cold.TabIndex = 8;
            this.cold.Text = "cold";
            this.cold.UseVisualStyleBackColor = false;
            this.cold.Click += new System.EventHandler(this.colorSelect_Click);
            // 
            // lightGray
            // 
            this.lightGray.BackColor = System.Drawing.Color.LightGray;
            this.lightGray.Location = new System.Drawing.Point(145, 205);
            this.lightGray.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lightGray.Name = "lightGray";
            this.lightGray.Size = new System.Drawing.Size(58, 58);
            this.lightGray.TabIndex = 5;
            this.lightGray.Text = "blank";
            this.lightGray.UseVisualStyleBackColor = false;
            this.lightGray.Click += new System.EventHandler(this.colorSelect_Click);
            // 
            // exit
            // 
            this.exit.BackColor = System.Drawing.Color.Yellow;
            this.exit.Location = new System.Drawing.Point(15, 204);
            this.exit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(58, 58);
            this.exit.TabIndex = 4;
            this.exit.Text = "exit";
            this.exit.UseVisualStyleBackColor = false;
            this.exit.Click += new System.EventHandler(this.colorSelect_Click);
            // 
            // groupCurrentColor
            // 
            this.groupCurrentColor.Controls.Add(this.colorCurrent);
            this.groupCurrentColor.Location = new System.Drawing.Point(73, 630);
            this.groupCurrentColor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupCurrentColor.Name = "groupCurrentColor";
            this.groupCurrentColor.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupCurrentColor.Size = new System.Drawing.Size(119, 102);
            this.groupCurrentColor.TabIndex = 2;
            this.groupCurrentColor.TabStop = false;
            this.groupCurrentColor.Text = "Current Tile";
            // 
            // colorCurrent
            // 
            this.colorCurrent.BackColor = System.Drawing.Color.LightGray;
            this.colorCurrent.Location = new System.Drawing.Point(27, 26);
            this.colorCurrent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.colorCurrent.Name = "colorCurrent";
            this.colorCurrent.Size = new System.Drawing.Size(68, 61);
            this.colorCurrent.TabIndex = 0;
            this.colorCurrent.TabStop = false;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(133, 735);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(105, 73);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "Save File";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(23, 735);
            this.buttonLoad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(104, 72);
            this.buttonLoad.TabIndex = 4;
            this.buttonLoad.Text = "Load File";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
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
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.GroupBox groupCurrentColor;
        private System.Windows.Forms.PictureBox colorCurrent;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Button top;
        private System.Windows.Forms.Button ground;
        private System.Windows.Forms.Button rightWall;
        private System.Windows.Forms.Button leftWall;
        private System.Windows.Forms.Button blank;
        private System.Windows.Forms.Button platform;
        private System.Windows.Forms.Button hot;
        private System.Windows.Forms.Button cold;
        private System.Windows.Forms.Button player;
        private System.Windows.Forms.Button filler;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button corner3;
        private System.Windows.Forms.Button corner2;
        private System.Windows.Forms.Button corner1;
        private System.Windows.Forms.Button pillar;
        private System.Windows.Forms.Button CGleft;
        private System.Windows.Forms.Button CGright;
        private System.Windows.Forms.Button corner4;
    }
}