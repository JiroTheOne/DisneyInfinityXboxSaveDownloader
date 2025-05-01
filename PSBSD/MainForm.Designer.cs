namespace PSBSD
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            DownloadBtn = new Button();
            BrowseBtn = new Button();
            pictureBox1 = new PictureBox();
            LogList = new ListBox();
            label1 = new Label();
            button1 = new Button();
            progressBar = new ProgressBar();
            pictureBox2 = new PictureBox();
            EditionImage2 = new PictureBox();
            EditionImage3 = new PictureBox();
            EditionLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EditionImage2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EditionImage3).BeginInit();
            SuspendLayout();
            // 
            // DownloadBtn
            // 
            DownloadBtn.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DownloadBtn.Location = new Point(180, 495);
            DownloadBtn.Margin = new Padding(3, 4, 3, 4);
            DownloadBtn.Name = "DownloadBtn";
            DownloadBtn.Size = new Size(162, 77);
            DownloadBtn.TabIndex = 1;
            DownloadBtn.Text = "Authenticate and Download";
            DownloadBtn.UseVisualStyleBackColor = true;
            DownloadBtn.Click += DownloadBtn_Click;
            // 
            // BrowseBtn
            // 
            BrowseBtn.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BrowseBtn.Location = new Point(12, 495);
            BrowseBtn.Margin = new Padding(3, 4, 3, 4);
            BrowseBtn.Name = "BrowseBtn";
            BrowseBtn.Size = new Size(162, 77);
            BrowseBtn.TabIndex = 2;
            BrowseBtn.Text = "Set output folder";
            BrowseBtn.UseVisualStyleBackColor = true;
            BrowseBtn.Click += BrowseBtn_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, -1);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(291, 68);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // LogList
            // 
            LogList.FormattingEnabled = true;
            LogList.Location = new Point(0, 103);
            LogList.Margin = new Padding(3, 4, 3, 4);
            LogList.Name = "LogList";
            LogList.Size = new Size(619, 384);
            LogList.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(7, 62);
            label1.Name = "label1";
            label1.Size = new Size(281, 37);
            label1.TabIndex = 5;
            label1.Text = "SAVE DOWNLOADER";
            // 
            // button1
            // 
            button1.Location = new Point(445, 496);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(174, 33);
            button1.TabIndex = 6;
            button1.Text = "Copy output";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(0, 581);
            progressBar.Margin = new Padding(3, 4, 3, 4);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(553, 31);
            progressBar.Step = 1;
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.TabIndex = 7;
            // 
            // pictureBox2
            // 
            pictureBox2.Cursor = Cursors.Hand;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(352, 487);
            pictureBox2.Margin = new Padding(3, 4, 3, 4);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(86, 97);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 8;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // EditionImage2
            // 
            EditionImage2.Anchor = AnchorStyles.Top;
            EditionImage2.Cursor = Cursors.Hand;
            EditionImage2.Image = (Image)resources.GetObject("EditionImage2.Image");
            EditionImage2.Location = new Point(441, 7);
            EditionImage2.Margin = new Padding(3, 4, 3, 4);
            EditionImage2.Name = "EditionImage2";
            EditionImage2.Size = new Size(84, 88);
            EditionImage2.SizeMode = PictureBoxSizeMode.StretchImage;
            EditionImage2.TabIndex = 9;
            EditionImage2.TabStop = false;
            EditionImage2.Click += EditionImage2_Click;
            // 
            // EditionImage3
            // 
            EditionImage3.Anchor = AnchorStyles.Top;
            EditionImage3.Cursor = Cursors.Hand;
            EditionImage3.Image = (Image)resources.GetObject("EditionImage3.Image");
            EditionImage3.Location = new Point(531, 7);
            EditionImage3.Margin = new Padding(3, 4, 3, 4);
            EditionImage3.Name = "EditionImage3";
            EditionImage3.Size = new Size(84, 88);
            EditionImage3.SizeMode = PictureBoxSizeMode.StretchImage;
            EditionImage3.TabIndex = 10;
            EditionImage3.TabStop = false;
            EditionImage3.Click += EditionImage3_Click;
            // 
            // EditionLabel
            // 
            EditionLabel.AutoSize = true;
            EditionLabel.BackColor = Color.Transparent;
            EditionLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            EditionLabel.Location = new Point(360, 7);
            EditionLabel.Name = "EditionLabel";
            EditionLabel.Size = new Size(78, 56);
            EditionLabel.TabIndex = 11;
            EditionLabel.Text = "Choose\r\nEdition:";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(622, 604);
            Controls.Add(EditionLabel);
            Controls.Add(EditionImage3);
            Controls.Add(EditionImage2);
            Controls.Add(progressBar);
            Controls.Add(button1);
            Controls.Add(LogList);
            Controls.Add(BrowseBtn);
            Controls.Add(DownloadBtn);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Controls.Add(pictureBox2);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            MaximumSize = new Size(640, 651);
            MinimumSize = new Size(640, 651);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Disney Infinity Save Downloader";
            Shown += MainForm_Shown;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)EditionImage2).EndInit();
            ((System.ComponentModel.ISupportInitialize)EditionImage3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button DownloadBtn;
        private Button BrowseBtn;
        private PictureBox pictureBox1;
        private ListBox LogList;
        private Label label1;
        private Button button1;
        private ProgressBar progressBar;
        private PictureBox pictureBox2;
        private PictureBox EditionImage2;
        private PictureBox EditionImage3;
        private Label EditionLabel;
    }
}
