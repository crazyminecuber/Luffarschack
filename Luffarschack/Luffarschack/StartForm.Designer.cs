namespace Luffarschack
{
    partial class StartForm
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
            this.cbxAntalSpelare = new System.Windows.Forms.ComboBox();
            this.btnKör = new System.Windows.Forms.Button();
            this.cbxBrädstorlek = new System.Windows.Forms.ComboBox();
            this.grpSpelare1 = new System.Windows.Forms.GroupBox();
            this.pbxSpelare1 = new System.Windows.Forms.PictureBox();
            this.btnBild1 = new System.Windows.Forms.Button();
            this.tbxSpelarNamn1 = new System.Windows.Forms.TextBox();
            this.grpSpelare3 = new System.Windows.Forms.GroupBox();
            this.pbxSpelare3 = new System.Windows.Forms.PictureBox();
            this.btnBild3 = new System.Windows.Forms.Button();
            this.tbxSpelarNamn3 = new System.Windows.Forms.TextBox();
            this.grpSpelare2 = new System.Windows.Forms.GroupBox();
            this.pbxSpelare2 = new System.Windows.Forms.PictureBox();
            this.btnBild2 = new System.Windows.Forms.Button();
            this.tbxSpelarNamn2 = new System.Windows.Forms.TextBox();
            this.grpAI2 = new System.Windows.Forms.GroupBox();
            this.btnAIFil2 = new System.Windows.Forms.Button();
            this.pbxAI2 = new System.Windows.Forms.PictureBox();
            this.btnAIBild2 = new System.Windows.Forms.Button();
            this.tbxAI2 = new System.Windows.Forms.TextBox();
            this.grpAI1 = new System.Windows.Forms.GroupBox();
            this.btnAIfil1 = new System.Windows.Forms.Button();
            this.pbxAI1 = new System.Windows.Forms.PictureBox();
            this.btnAIBild1 = new System.Windows.Forms.Button();
            this.tbxAI1 = new System.Windows.Forms.TextBox();
            this.fbdHämtaBild = new System.Windows.Forms.OpenFileDialog();
            this.AIfilväljare = new System.Windows.Forms.OpenFileDialog();
            this.grpSpelare1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSpelare1)).BeginInit();
            this.grpSpelare3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSpelare3)).BeginInit();
            this.grpSpelare2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSpelare2)).BeginInit();
            this.grpAI2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxAI2)).BeginInit();
            this.grpAI1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxAI1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxAntalSpelare
            // 
            this.cbxAntalSpelare.FormattingEnabled = true;
            this.cbxAntalSpelare.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.cbxAntalSpelare.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "AI Match"});
            this.cbxAntalSpelare.Location = new System.Drawing.Point(12, 92);
            this.cbxAntalSpelare.Name = "cbxAntalSpelare";
            this.cbxAntalSpelare.Size = new System.Drawing.Size(121, 21);
            this.cbxAntalSpelare.TabIndex = 1;
            this.cbxAntalSpelare.Text = "Antal Spelare";
            this.cbxAntalSpelare.SelectedIndexChanged += new System.EventHandler(this.cbxAntalSpelare_SelectedIndexChanged);
            // 
            // btnKör
            // 
            this.btnKör.Location = new System.Drawing.Point(12, 164);
            this.btnKör.Name = "btnKör";
            this.btnKör.Size = new System.Drawing.Size(75, 23);
            this.btnKör.TabIndex = 2;
            this.btnKör.Text = "Spela";
            this.btnKör.UseVisualStyleBackColor = true;
            this.btnKör.Click += new System.EventHandler(this.btnKör_Click);
            // 
            // cbxBrädstorlek
            // 
            this.cbxBrädstorlek.FormattingEnabled = true;
            this.cbxBrädstorlek.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.cbxBrädstorlek.Items.AddRange(new object[] {
            "3",
            "5",
            "9",
            "15",
            "25"});
            this.cbxBrädstorlek.Location = new System.Drawing.Point(12, 119);
            this.cbxBrädstorlek.Name = "cbxBrädstorlek";
            this.cbxBrädstorlek.Size = new System.Drawing.Size(121, 21);
            this.cbxBrädstorlek.TabIndex = 3;
            this.cbxBrädstorlek.Text = "Brädstorlek";
            this.cbxBrädstorlek.SelectedIndexChanged += new System.EventHandler(this.cbxBrädstorlek_SelectedIndexChanged);
            // 
            // grpSpelare1
            // 
            this.grpSpelare1.Controls.Add(this.pbxSpelare1);
            this.grpSpelare1.Controls.Add(this.btnBild1);
            this.grpSpelare1.Controls.Add(this.tbxSpelarNamn1);
            this.grpSpelare1.Location = new System.Drawing.Point(317, 12);
            this.grpSpelare1.Name = "grpSpelare1";
            this.grpSpelare1.Size = new System.Drawing.Size(178, 80);
            this.grpSpelare1.TabIndex = 4;
            this.grpSpelare1.TabStop = false;
            this.grpSpelare1.Text = "Spelare 1";
            // 
            // pbxSpelare1
            // 
            this.pbxSpelare1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbxSpelare1.Location = new System.Drawing.Point(127, 19);
            this.pbxSpelare1.Name = "pbxSpelare1";
            this.pbxSpelare1.Size = new System.Drawing.Size(45, 45);
            this.pbxSpelare1.TabIndex = 5;
            this.pbxSpelare1.TabStop = false;
            // 
            // btnBild1
            // 
            this.btnBild1.Location = new System.Drawing.Point(7, 46);
            this.btnBild1.Name = "btnBild1";
            this.btnBild1.Size = new System.Drawing.Size(99, 23);
            this.btnBild1.TabIndex = 1;
            this.btnBild1.Text = "Välj spelarbild";
            this.btnBild1.UseVisualStyleBackColor = true;
            this.btnBild1.Click += new System.EventHandler(this.btnBild1_Click);
            // 
            // tbxSpelarNamn1
            // 
            this.tbxSpelarNamn1.Location = new System.Drawing.Point(6, 19);
            this.tbxSpelarNamn1.MaxLength = 15;
            this.tbxSpelarNamn1.Name = "tbxSpelarNamn1";
            this.tbxSpelarNamn1.Size = new System.Drawing.Size(100, 20);
            this.tbxSpelarNamn1.TabIndex = 0;
            // 
            // grpSpelare3
            // 
            this.grpSpelare3.Controls.Add(this.pbxSpelare3);
            this.grpSpelare3.Controls.Add(this.btnBild3);
            this.grpSpelare3.Controls.Add(this.tbxSpelarNamn3);
            this.grpSpelare3.Location = new System.Drawing.Point(317, 184);
            this.grpSpelare3.Name = "grpSpelare3";
            this.grpSpelare3.Size = new System.Drawing.Size(178, 80);
            this.grpSpelare3.TabIndex = 5;
            this.grpSpelare3.TabStop = false;
            this.grpSpelare3.Text = "Spelare 3";
            // 
            // pbxSpelare3
            // 
            this.pbxSpelare3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbxSpelare3.Location = new System.Drawing.Point(127, 19);
            this.pbxSpelare3.Name = "pbxSpelare3";
            this.pbxSpelare3.Size = new System.Drawing.Size(45, 45);
            this.pbxSpelare3.TabIndex = 3;
            this.pbxSpelare3.TabStop = false;
            // 
            // btnBild3
            // 
            this.btnBild3.Location = new System.Drawing.Point(7, 51);
            this.btnBild3.Name = "btnBild3";
            this.btnBild3.Size = new System.Drawing.Size(99, 23);
            this.btnBild3.TabIndex = 1;
            this.btnBild3.Text = "Välj spelarbild";
            this.btnBild3.UseVisualStyleBackColor = true;
            this.btnBild3.Click += new System.EventHandler(this.btnBild3_Click);
            // 
            // tbxSpelarNamn3
            // 
            this.tbxSpelarNamn3.Location = new System.Drawing.Point(7, 20);
            this.tbxSpelarNamn3.MaxLength = 15;
            this.tbxSpelarNamn3.Name = "tbxSpelarNamn3";
            this.tbxSpelarNamn3.Size = new System.Drawing.Size(100, 20);
            this.tbxSpelarNamn3.TabIndex = 0;
            // 
            // grpSpelare2
            // 
            this.grpSpelare2.Controls.Add(this.pbxSpelare2);
            this.grpSpelare2.Controls.Add(this.btnBild2);
            this.grpSpelare2.Controls.Add(this.tbxSpelarNamn2);
            this.grpSpelare2.Location = new System.Drawing.Point(317, 98);
            this.grpSpelare2.Name = "grpSpelare2";
            this.grpSpelare2.Size = new System.Drawing.Size(178, 80);
            this.grpSpelare2.TabIndex = 6;
            this.grpSpelare2.TabStop = false;
            this.grpSpelare2.Text = "Spelare 2";
            // 
            // pbxSpelare2
            // 
            this.pbxSpelare2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbxSpelare2.Location = new System.Drawing.Point(127, 19);
            this.pbxSpelare2.Name = "pbxSpelare2";
            this.pbxSpelare2.Size = new System.Drawing.Size(45, 45);
            this.pbxSpelare2.TabIndex = 4;
            this.pbxSpelare2.TabStop = false;
            // 
            // btnBild2
            // 
            this.btnBild2.Location = new System.Drawing.Point(7, 51);
            this.btnBild2.Name = "btnBild2";
            this.btnBild2.Size = new System.Drawing.Size(99, 23);
            this.btnBild2.TabIndex = 1;
            this.btnBild2.Text = "Välj spelarbild";
            this.btnBild2.UseVisualStyleBackColor = true;
            this.btnBild2.Click += new System.EventHandler(this.btnBild2_Click);
            // 
            // tbxSpelarNamn2
            // 
            this.tbxSpelarNamn2.Location = new System.Drawing.Point(7, 21);
            this.tbxSpelarNamn2.MaxLength = 15;
            this.tbxSpelarNamn2.Name = "tbxSpelarNamn2";
            this.tbxSpelarNamn2.Size = new System.Drawing.Size(100, 20);
            this.tbxSpelarNamn2.TabIndex = 0;
            // 
            // grpAI2
            // 
            this.grpAI2.Controls.Add(this.btnAIFil2);
            this.grpAI2.Controls.Add(this.pbxAI2);
            this.grpAI2.Controls.Add(this.btnAIBild2);
            this.grpAI2.Controls.Add(this.tbxAI2);
            this.grpAI2.Location = new System.Drawing.Point(153, 128);
            this.grpAI2.Name = "grpAI2";
            this.grpAI2.Size = new System.Drawing.Size(157, 109);
            this.grpAI2.TabIndex = 7;
            this.grpAI2.TabStop = false;
            this.grpAI2.Text = "AI 2";
            // 
            // btnAIFil2
            // 
            this.btnAIFil2.Location = new System.Drawing.Point(7, 75);
            this.btnAIFil2.Name = "btnAIFil2";
            this.btnAIFil2.Size = new System.Drawing.Size(99, 23);
            this.btnAIFil2.TabIndex = 4;
            this.btnAIFil2.Text = "Välj AI fil";
            this.btnAIFil2.UseVisualStyleBackColor = true;
            this.btnAIFil2.Click += new System.EventHandler(this.btnAIFil2_Click);
            // 
            // pbxAI2
            // 
            this.pbxAI2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbxAI2.Location = new System.Drawing.Point(106, 19);
            this.pbxAI2.Name = "pbxAI2";
            this.pbxAI2.Size = new System.Drawing.Size(45, 45);
            this.pbxAI2.TabIndex = 3;
            this.pbxAI2.TabStop = false;
            // 
            // btnAIBild2
            // 
            this.btnAIBild2.Location = new System.Drawing.Point(7, 46);
            this.btnAIBild2.Name = "btnAIBild2";
            this.btnAIBild2.Size = new System.Drawing.Size(99, 23);
            this.btnAIBild2.TabIndex = 1;
            this.btnAIBild2.Text = "Välj AI-bild";
            this.btnAIBild2.UseVisualStyleBackColor = true;
            this.btnAIBild2.Click += new System.EventHandler(this.btnAIBild2_Click);
            // 
            // tbxAI2
            // 
            this.tbxAI2.Location = new System.Drawing.Point(6, 19);
            this.tbxAI2.MaxLength = 15;
            this.tbxAI2.Name = "tbxAI2";
            this.tbxAI2.Size = new System.Drawing.Size(100, 20);
            this.tbxAI2.TabIndex = 0;
            // 
            // grpAI1
            // 
            this.grpAI1.Controls.Add(this.btnAIfil1);
            this.grpAI1.Controls.Add(this.pbxAI1);
            this.grpAI1.Controls.Add(this.btnAIBild1);
            this.grpAI1.Controls.Add(this.tbxAI1);
            this.grpAI1.Location = new System.Drawing.Point(153, 12);
            this.grpAI1.Name = "grpAI1";
            this.grpAI1.Size = new System.Drawing.Size(158, 110);
            this.grpAI1.TabIndex = 5;
            this.grpAI1.TabStop = false;
            this.grpAI1.Text = "AI 1";
            // 
            // btnAIfil1
            // 
            this.btnAIfil1.Location = new System.Drawing.Point(7, 76);
            this.btnAIfil1.Name = "btnAIfil1";
            this.btnAIfil1.Size = new System.Drawing.Size(99, 23);
            this.btnAIfil1.TabIndex = 3;
            this.btnAIfil1.Text = "Välj AI fil";
            this.btnAIfil1.UseVisualStyleBackColor = true;
            this.btnAIfil1.Click += new System.EventHandler(this.btnAIfil1_Click);
            // 
            // pbxAI1
            // 
            this.pbxAI1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbxAI1.Location = new System.Drawing.Point(106, 19);
            this.pbxAI1.Name = "pbxAI1";
            this.pbxAI1.Size = new System.Drawing.Size(45, 45);
            this.pbxAI1.TabIndex = 2;
            this.pbxAI1.TabStop = false;
            // 
            // btnAIBild1
            // 
            this.btnAIBild1.Location = new System.Drawing.Point(7, 46);
            this.btnAIBild1.Name = "btnAIBild1";
            this.btnAIBild1.Size = new System.Drawing.Size(99, 23);
            this.btnAIBild1.TabIndex = 1;
            this.btnAIBild1.Text = "Välj AI-bild";
            this.btnAIBild1.UseVisualStyleBackColor = true;
            this.btnAIBild1.Click += new System.EventHandler(this.btnAI1_Click);
            // 
            // tbxAI1
            // 
            this.tbxAI1.Location = new System.Drawing.Point(6, 19);
            this.tbxAI1.MaxLength = 15;
            this.tbxAI1.Name = "tbxAI1";
            this.tbxAI1.Size = new System.Drawing.Size(100, 20);
            this.tbxAI1.TabIndex = 0;
            // 
            // fbdHämtaBild
            // 
            this.fbdHämtaBild.FileName = "openFileDialog1";
            // 
            // AIfilväljare
            // 
            this.AIfilväljare.FileName = "openFileDialog1";
            this.AIfilväljare.Filter = "AI filer (*.exe)|*.exe";
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 328);
            this.Controls.Add(this.grpAI1);
            this.Controls.Add(this.grpAI2);
            this.Controls.Add(this.grpSpelare2);
            this.Controls.Add(this.grpSpelare3);
            this.Controls.Add(this.grpSpelare1);
            this.Controls.Add(this.cbxBrädstorlek);
            this.Controls.Add(this.btnKör);
            this.Controls.Add(this.cbxAntalSpelare);
            this.Name = "StartForm";
            this.Text = "Luffarschack";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StartForm_FormClosing);
            this.Load += new System.EventHandler(this.StartForm_Load);
            this.grpSpelare1.ResumeLayout(false);
            this.grpSpelare1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSpelare1)).EndInit();
            this.grpSpelare3.ResumeLayout(false);
            this.grpSpelare3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSpelare3)).EndInit();
            this.grpSpelare2.ResumeLayout(false);
            this.grpSpelare2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSpelare2)).EndInit();
            this.grpAI2.ResumeLayout(false);
            this.grpAI2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxAI2)).EndInit();
            this.grpAI1.ResumeLayout(false);
            this.grpAI1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxAI1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxAntalSpelare;
        private System.Windows.Forms.Button btnKör;
        private System.Windows.Forms.ComboBox cbxBrädstorlek;
        private System.Windows.Forms.GroupBox grpSpelare1;
        private System.Windows.Forms.Button btnBild1;
        private System.Windows.Forms.TextBox tbxSpelarNamn1;
        private System.Windows.Forms.GroupBox grpSpelare3;
        private System.Windows.Forms.Button btnBild3;
        private System.Windows.Forms.TextBox tbxSpelarNamn3;
        private System.Windows.Forms.GroupBox grpSpelare2;
        private System.Windows.Forms.Button btnBild2;
        private System.Windows.Forms.TextBox tbxSpelarNamn2;
        private System.Windows.Forms.GroupBox grpAI2;
        private System.Windows.Forms.Button btnAIBild2;
        private System.Windows.Forms.TextBox tbxAI2;
        private System.Windows.Forms.GroupBox grpAI1;
        private System.Windows.Forms.Button btnAIBild1;
        private System.Windows.Forms.TextBox tbxAI1;
        private System.Windows.Forms.PictureBox pbxSpelare1;
        private System.Windows.Forms.PictureBox pbxSpelare3;
        private System.Windows.Forms.PictureBox pbxSpelare2;
        private System.Windows.Forms.PictureBox pbxAI2;
        private System.Windows.Forms.PictureBox pbxAI1;
        private System.Windows.Forms.OpenFileDialog fbdHämtaBild;
        private System.Windows.Forms.Button btnAIFil2;
        private System.Windows.Forms.Button btnAIfil1;
        private System.Windows.Forms.OpenFileDialog AIfilväljare;
    }
}