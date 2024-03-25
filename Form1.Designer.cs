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
            LoadOriginalImagebtn = new Button();
            label3 = new Label();
            label4 = new Label();
            Graytxt = new TextBox();
            Whitetxt = new TextBox();
            label5 = new Label();
            label6 = new Label();
            ApplyBtn = new Button();
            pictureBox3 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
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
            SaveBtn.Location = new Point(1824, 522);
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
            BlackRd.Location = new Point(1825, 391);
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
            WhiteRb.Location = new Point(1825, 416);
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
            GrayRb.Location = new Point(1825, 441);
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
            txtSize.Location = new Point(1824, 481);
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
            label1.Location = new Point(1825, 373);
            label1.Name = "label1";
            label1.Size = new Size(36, 15);
            label1.TabIndex = 7;
            label1.Text = "Color";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1824, 463);
            label2.Name = "label2";
            label2.Size = new Size(50, 15);
            label2.TabIndex = 8;
            label2.Text = "Size Pen";
            // 
            // LoadOriginalImagebtn
            // 
            LoadOriginalImagebtn.Location = new Point(320, 955);
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
            label3.Location = new Point(1825, 122);
            label3.Name = "label3";
            label3.Size = new Size(93, 15);
            label3.TabIndex = 10;
            label3.Text = "Mask thresholds";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(1825, 158);
            label4.Name = "label4";
            label4.Size = new Size(39, 15);
            label4.TabIndex = 11;
            label4.Text = "Gray<";
            // 
            // Graytxt
            // 
            Graytxt.Location = new Point(1870, 155);
            Graytxt.Name = "Graytxt";
            Graytxt.Size = new Size(44, 23);
            Graytxt.TabIndex = 12;
            Graytxt.TextChanged += Colortxt_TextChanged;
            Graytxt.KeyPress += textBox1_KeyPress;
            // 
            // Whitetxt
            // 
            Whitetxt.Location = new Point(1870, 184);
            Whitetxt.Name = "Whitetxt";
            Whitetxt.Size = new Size(44, 23);
            Whitetxt.TabIndex = 14;
            Whitetxt.TextChanged += Colortxt_TextChanged;
            Whitetxt.KeyPress += textBox1_KeyPress;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(1825, 187);
            label5.Name = "label5";
            label5.Size = new Size(46, 15);
            label5.TabIndex = 13;
            label5.Text = "White<";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(1848, 219);
            label6.Name = "label6";
            label6.Size = new Size(43, 15);
            label6.TabIndex = 15;
            label6.Text = "Black>";
            // 
            // ApplyBtn
            // 
            ApplyBtn.Location = new Point(1825, 254);
            ApplyBtn.Name = "ApplyBtn";
            ApplyBtn.Size = new Size(75, 23);
            ApplyBtn.TabIndex = 17;
            ApplyBtn.Text = "Apply";
            ApplyBtn.UseVisualStyleBackColor = true;
            ApplyBtn.Click += ApplyBtn_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.Location = new Point(918, 37);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(900, 900);
            pictureBox3.TabIndex = 18;
            pictureBox3.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1918, 1041);
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
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
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
        private Button LoadOriginalImagebtn;
        private Label label3;
        private Label label4;
        private TextBox Graytxt;
        private TextBox Whitetxt;
        private Label label5;
        private Label label6;
        private Button ApplyBtn;
        private PictureBox pictureBox3;
    }
}
