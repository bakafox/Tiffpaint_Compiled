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
            BlackRd = new RadioButton();
            WhiteRb = new RadioButton();
            GrayRb = new RadioButton();
            txtSize = new TextBox();
            label1 = new Label();
            label2 = new Label();
            LoadOriginalImagebtn = new Button();
            label3 = new Label();
            label4 = new Label();
            Graytxt = new TextBox();
            Whitetxt = new TextBox();
            label5 = new Label();
            label6 = new Label();
            ApplyBtn = new Button();
            pictureBox3 = new PictureBox();
            Back_btn = new Button();
            Further_btn = new Button();
            ChunkInfo_lbl = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(36, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(700, 700);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(36, 12);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(700, 700);
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            pictureBox2.MouseDown += pictureBox2_MouseDown;
            pictureBox2.MouseMove += pictureBox2_MouseMove;
            pictureBox2.MouseUp += pictureBox2_MouseUp;
            // 
            // BlackRd
            // 
            BlackRd.AutoSize = true;
            BlackRd.Location = new Point(1480, 425);
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
            WhiteRb.Location = new Point(1480, 450);
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
            GrayRb.Location = new Point(1480, 475);
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
            txtSize.Location = new Point(1479, 515);
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
            label1.Location = new Point(1480, 407);
            label1.Name = "label1";
            label1.Size = new Size(36, 15);
            label1.TabIndex = 7;
            label1.Text = "Color";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1479, 497);
            label2.Name = "label2";
            label2.Size = new Size(50, 15);
            label2.TabIndex = 8;
            label2.Text = "Size Pen";
            // 
            // LoadOriginalImagebtn
            // 
            LoadOriginalImagebtn.Location = new Point(230, 733);
            LoadOriginalImagebtn.Name = "LoadOriginalImagebtn";
            LoadOriginalImagebtn.Size = new Size(161, 23);
            LoadOriginalImagebtn.TabIndex = 9;
            LoadOriginalImagebtn.Text = "Загрузить изображение";
            LoadOriginalImagebtn.UseVisualStyleBackColor = true;
            LoadOriginalImagebtn.Click += LoadOriginalImagebtn_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(1480, 156);
            label3.Name = "label3";
            label3.Size = new Size(93, 15);
            label3.TabIndex = 10;
            label3.Text = "Mask thresholds";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(1480, 192);
            label4.Name = "label4";
            label4.Size = new Size(39, 15);
            label4.TabIndex = 11;
            label4.Text = "Gray<";
            // 
            // Graytxt
            // 
            Graytxt.Location = new Point(1525, 189);
            Graytxt.Name = "Graytxt";
            Graytxt.Size = new Size(44, 23);
            Graytxt.TabIndex = 12;
            Graytxt.TextChanged += Colortxt_TextChanged;
            Graytxt.KeyPress += textBox1_KeyPress;
            // 
            // Whitetxt
            // 
            Whitetxt.Location = new Point(1525, 218);
            Whitetxt.Name = "Whitetxt";
            Whitetxt.Size = new Size(44, 23);
            Whitetxt.TabIndex = 14;
            Whitetxt.TextChanged += Colortxt_TextChanged;
            Whitetxt.KeyPress += textBox1_KeyPress;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(1480, 221);
            label5.Name = "label5";
            label5.Size = new Size(46, 15);
            label5.TabIndex = 13;
            label5.Text = "White<";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(1503, 253);
            label6.Name = "label6";
            label6.Size = new Size(43, 15);
            label6.TabIndex = 15;
            label6.Text = "Black>";
            // 
            // ApplyBtn
            // 
            ApplyBtn.Location = new Point(1480, 288);
            ApplyBtn.Name = "ApplyBtn";
            ApplyBtn.Size = new Size(75, 23);
            ApplyBtn.TabIndex = 17;
            ApplyBtn.Text = "Apply";
            ApplyBtn.UseVisualStyleBackColor = true;
            ApplyBtn.Click += ApplyBtn_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.Location = new Point(760, 12);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(700, 700);
            pictureBox3.TabIndex = 18;
            pictureBox3.TabStop = false;
            // 
            // Back_btn
            // 
            Back_btn.Location = new Point(230, 762);
            Back_btn.Name = "Back_btn";
            Back_btn.Size = new Size(75, 23);
            Back_btn.TabIndex = 19;
            Back_btn.Text = "Back";
            Back_btn.UseVisualStyleBackColor = true;
            Back_btn.Click += Back_btn_Click;
            // 
            // Further_btn
            // 
            Further_btn.Location = new Point(316, 762);
            Further_btn.Name = "Further_btn";
            Further_btn.Size = new Size(75, 23);
            Further_btn.TabIndex = 20;
            Further_btn.Text = "Further";
            Further_btn.UseVisualStyleBackColor = true;
            Further_btn.Click += Further_btn_Click;
            // 
            // ChunkInfo_lbl
            // 
            ChunkInfo_lbl.AutoSize = true;
            ChunkInfo_lbl.Location = new Point(726, 733);
            ChunkInfo_lbl.Name = "ChunkInfo_lbl";
            ChunkInfo_lbl.Size = new Size(10, 15);
            ChunkInfo_lbl.TabIndex = 21;
            ChunkInfo_lbl.Text = ".";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(1242, 741);
            label7.Name = "label7";
            label7.Size = new Size(51, 15);
            label7.TabIndex = 22;
            label7.Text = "A - Back";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(1242, 756);
            label8.Name = "label8";
            label8.Size = new Size(49, 15);
            label8.TabIndex = 23;
            label8.Text = "S - Next";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(1242, 771);
            label9.Name = "label9";
            label9.Size = new Size(70, 15);
            label9.TabIndex = 24;
            label9.Text = "Z - Zoom in";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(1242, 786);
            label10.Name = "label10";
            label10.Size = new Size(78, 15);
            label10.TabIndex = 25;
            label10.Text = "X - Zoom out";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1584, 861);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(ChunkInfo_lbl);
            Controls.Add(Further_btn);
            Controls.Add(Back_btn);
            Controls.Add(pictureBox3);
            Controls.Add(ApplyBtn);
            Controls.Add(label6);
            Controls.Add(Whitetxt);
            Controls.Add(label5);
            Controls.Add(Graytxt);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(LoadOriginalImagebtn);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtSize);
            Controls.Add(GrayRb);
            Controls.Add(WhiteRb);
            Controls.Add(BlackRd);
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
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private RadioButton BlackRd;
        private RadioButton WhiteRb;
        private RadioButton GrayRb;
        private TextBox txtSize;
        private Label label1;
        private Label label2;
        private Button LoadOriginalImagebtn;
        private Label label3;
        private Label label4;
        private TextBox Graytxt;
        private TextBox Whitetxt;
        private Label label5;
        private Label label6;
        private Button ApplyBtn;
        private PictureBox pictureBox3;
        private Button Back_btn;
        private Button Further_btn;
        private Label ChunkInfo_lbl;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
    }
}
