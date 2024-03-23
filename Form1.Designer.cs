namespace Tiffpaint
{
    partial class Form1
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
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            SaveBtn = new Button();
            BlackRd = new RadioButton();
            WhiteRb = new RadioButton();
            GrayRb = new RadioButton();
            txtSize = new TextBox();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(12, 37);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(900, 900);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(12, 37);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(900, 900);
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            pictureBox2.MouseDown += pictureBox2_MouseDown;
            pictureBox2.MouseMove += pictureBox2_MouseMove;
            pictureBox2.MouseUp += pictureBox2_MouseUp;
            // 
            // SaveBtn
            // 
            SaveBtn.Location = new Point(1830, 914);
            SaveBtn.Name = "SaveBtn";
            SaveBtn.Size = new Size(75, 23);
            SaveBtn.TabIndex = 2;
            SaveBtn.Text = "Сохранить";
            SaveBtn.UseVisualStyleBackColor = true;
            SaveBtn.Click += SaveBtn_Click;
            // 
            // BlackRd
            // 
            BlackRd.AutoSize = true;
            BlackRd.Location = new Point(1831, 783);
            BlackRd.Name = "BlackRd";
            BlackRd.Size = new Size(53, 19);
            BlackRd.TabIndex = 3;
            BlackRd.TabStop = true;
            BlackRd.Text = "Black";
            BlackRd.UseVisualStyleBackColor = true;
            BlackRd.CheckedChanged += BlackRd_CheckedChanged;
            // 
            // WhiteRb
            // 
            WhiteRb.AutoSize = true;
            WhiteRb.Location = new Point(1831, 808);
            WhiteRb.Name = "WhiteRb";
            WhiteRb.Size = new Size(56, 19);
            WhiteRb.TabIndex = 4;
            WhiteRb.TabStop = true;
            WhiteRb.Text = "White";
            WhiteRb.UseVisualStyleBackColor = true;
            WhiteRb.CheckedChanged += WhiteRb_CheckedChanged;
            // 
            // GrayRb
            // 
            GrayRb.AutoSize = true;
            GrayRb.Location = new Point(1831, 833);
            GrayRb.Name = "GrayRb";
            GrayRb.Size = new Size(49, 19);
            GrayRb.TabIndex = 5;
            GrayRb.TabStop = true;
            GrayRb.Text = "Grey";
            GrayRb.UseVisualStyleBackColor = true;
            GrayRb.CheckedChanged += GrayRb_CheckedChanged;
            // 
            // txtSize
            // 
            txtSize.Location = new Point(1830, 873);
            txtSize.Name = "txtSize";
            txtSize.Size = new Size(67, 23);
            txtSize.TabIndex = 6;
            txtSize.TextAlign = HorizontalAlignment.Right;
            txtSize.TextChanged += txtSize_TextChanged;
            txtSize.KeyPress += textBox1_KeyPress;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(1831, 765);
            label1.Name = "label1";
            label1.Size = new Size(36, 15);
            label1.TabIndex = 7;
            label1.Text = "Color";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1830, 855);
            label2.Name = "label2";
            label2.Size = new Size(50, 15);
            label2.TabIndex = 8;
            label2.Text = "Size Pen";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1908, 1041);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtSize);
            Controls.Add(GrayRb);
            Controls.Add(WhiteRb);
            Controls.Add(BlackRd);
            Controls.Add(SaveBtn);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            KeyPreview = true;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Button SaveBtn;
        private RadioButton BlackRd;
        private RadioButton WhiteRb;
        private RadioButton GrayRb;
        private TextBox txtSize;
        private Label label1;
        private Label label2;
    }
}
