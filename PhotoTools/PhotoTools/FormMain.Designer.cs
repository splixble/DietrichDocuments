namespace PhotoTools
{
    partial class FormMain
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
            this.splitConOuter = new System.Windows.Forms.SplitContainer();
            this.listFiles = new System.Windows.Forms.ListView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openPhotoDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSelectionsInCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteUnmatchedRAWFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitConOuter)).BeginInit();
            this.splitConOuter.Panel1.SuspendLayout();
            this.splitConOuter.Panel2.SuspendLayout();
            this.splitConOuter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitConOuter
            // 
            this.splitConOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitConOuter.Location = new System.Drawing.Point(0, 38);
            this.splitConOuter.Name = "splitConOuter";
            // 
            // splitConOuter.Panel1
            // 
            this.splitConOuter.Panel1.Controls.Add(this.listFiles);
            // 
            // splitConOuter.Panel2
            // 
            this.splitConOuter.Panel2.Controls.Add(this.pictureBox1);
            this.splitConOuter.Size = new System.Drawing.Size(2922, 958);
            this.splitConOuter.SplitterDistance = 630;
            this.splitConOuter.TabIndex = 0;
            // 
            // listFiles
            // 
            this.listFiles.CheckBoxes = true;
            this.listFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listFiles.HideSelection = false;
            this.listFiles.Location = new System.Drawing.Point(0, 0);
            this.listFiles.Name = "listFiles";
            this.listFiles.Size = new System.Drawing.Size(630, 958);
            this.listFiles.TabIndex = 0;
            this.listFiles.UseCompatibleStateImageBehavior = false;
            this.listFiles.View = System.Windows.Forms.View.List;
            this.listFiles.SelectedIndexChanged += new System.EventHandler(this.listFiles_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(2288, 958);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(2922, 38);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openPhotoDirectoryToolStripMenuItem,
            this.saveSelectionsInCSVToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(62, 34);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openPhotoDirectoryToolStripMenuItem
            // 
            this.openPhotoDirectoryToolStripMenuItem.Name = "openPhotoDirectoryToolStripMenuItem";
            this.openPhotoDirectoryToolStripMenuItem.Size = new System.Drawing.Size(339, 40);
            this.openPhotoDirectoryToolStripMenuItem.Text = "Open Photo Directory";
            this.openPhotoDirectoryToolStripMenuItem.Click += new System.EventHandler(this.openPhotoDirectoryToolStripMenuItem_Click);
            // 
            // saveSelectionsInCSVToolStripMenuItem
            // 
            this.saveSelectionsInCSVToolStripMenuItem.Name = "saveSelectionsInCSVToolStripMenuItem";
            this.saveSelectionsInCSVToolStripMenuItem.Size = new System.Drawing.Size(339, 40);
            this.saveSelectionsInCSVToolStripMenuItem.Text = "Save Selections in CSV";
            this.saveSelectionsInCSVToolStripMenuItem.Click += new System.EventHandler(this.saveSelectionsInCSVToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteUnmatchedRAWFilesToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(66, 34);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // deleteUnmatchedRAWFilesToolStripMenuItem
            // 
            this.deleteUnmatchedRAWFilesToolStripMenuItem.Name = "deleteUnmatchedRAWFilesToolStripMenuItem";
            this.deleteUnmatchedRAWFilesToolStripMenuItem.Size = new System.Drawing.Size(402, 40);
            this.deleteUnmatchedRAWFilesToolStripMenuItem.Text = "Delete Unmatched RAW Files";
            this.deleteUnmatchedRAWFilesToolStripMenuItem.Click += new System.EventHandler(this.deleteUnmatchedRAWFilesToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2922, 996);
            this.Controls.Add(this.splitConOuter);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "Form1";
            this.splitConOuter.Panel1.ResumeLayout(false);
            this.splitConOuter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitConOuter)).EndInit();
            this.splitConOuter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitConOuter;
        private System.Windows.Forms.ListView listFiles;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openPhotoDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSelectionsInCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteUnmatchedRAWFilesToolStripMenuItem;
    }
}

