namespace Client
{
    partial class frmInstructions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInstructions));
            this.gbQuiz = new System.Windows.Forms.GroupBox();
            this.cmdSubmitQuiz = new System.Windows.Forms.Button();
            this.txtQuiz = new System.Windows.Forms.TextBox();
            this.cmdStart = new System.Windows.Forms.Button();
            this.cmdBack = new System.Windows.Forms.Button();
            this.cmdNext = new System.Windows.Forms.Button();
            this.RichTextBox1 = new System.Windows.Forms.RichTextBox();
            this.gbQuiz.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbQuiz
            // 
            this.gbQuiz.Controls.Add(this.cmdSubmitQuiz);
            this.gbQuiz.Controls.Add(this.txtQuiz);
            this.gbQuiz.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbQuiz.Location = new System.Drawing.Point(492, 438);
            this.gbQuiz.Name = "gbQuiz";
            this.gbQuiz.Size = new System.Drawing.Size(408, 65);
            this.gbQuiz.TabIndex = 9;
            this.gbQuiz.TabStop = false;
            this.gbQuiz.Text = "Quiz Answer";
            this.gbQuiz.Visible = false;
            // 
            // cmdSubmitQuiz
            // 
            this.cmdSubmitQuiz.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSubmitQuiz.Location = new System.Drawing.Point(321, 22);
            this.cmdSubmitQuiz.Name = "cmdSubmitQuiz";
            this.cmdSubmitQuiz.Size = new System.Drawing.Size(81, 29);
            this.cmdSubmitQuiz.TabIndex = 6;
            this.cmdSubmitQuiz.Text = "Submit";
            this.cmdSubmitQuiz.UseVisualStyleBackColor = true;
            this.cmdSubmitQuiz.Click += new System.EventHandler(this.cmdSubmitQuiz_Click);
            // 
            // txtQuiz
            // 
            this.txtQuiz.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuiz.Location = new System.Drawing.Point(6, 22);
            this.txtQuiz.Name = "txtQuiz";
            this.txtQuiz.Size = new System.Drawing.Size(308, 29);
            this.txtQuiz.TabIndex = 5;
            this.txtQuiz.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtQuiz.TextChanged += new System.EventHandler(this.txtQuiz_TextChanged);
            // 
            // cmdStart
            // 
            this.cmdStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmdStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdStart.Location = new System.Drawing.Point(217, 796);
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(87, 35);
            this.cmdStart.TabIndex = 8;
            this.cmdStart.Text = "Start";
            this.cmdStart.UseVisualStyleBackColor = false;
            this.cmdStart.Visible = false;
            this.cmdStart.Click += new System.EventHandler(this.cmdStart_Click);
            // 
            // cmdBack
            // 
            this.cmdBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdBack.Location = new System.Drawing.Point(55, 796);
            this.cmdBack.Name = "cmdBack";
            this.cmdBack.Size = new System.Drawing.Size(87, 35);
            this.cmdBack.TabIndex = 7;
            this.cmdBack.Text = "<< Back";
            this.cmdBack.UseVisualStyleBackColor = true;
            this.cmdBack.Visible = false;
            this.cmdBack.Click += new System.EventHandler(this.cmdBack_Click);
            // 
            // cmdNext
            // 
            this.cmdNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdNext.Location = new System.Drawing.Point(376, 796);
            this.cmdNext.Name = "cmdNext";
            this.cmdNext.Size = new System.Drawing.Size(87, 35);
            this.cmdNext.TabIndex = 6;
            this.cmdNext.Text = "Next >>";
            this.cmdNext.UseVisualStyleBackColor = true;
            this.cmdNext.Click += new System.EventHandler(this.cmdNext_Click);
            // 
            // RichTextBox1
            // 
            this.RichTextBox1.BackColor = System.Drawing.Color.White;
            this.RichTextBox1.Location = new System.Drawing.Point(8, 12);
            this.RichTextBox1.Name = "RichTextBox1";
            this.RichTextBox1.ReadOnly = true;
            this.RichTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.RichTextBox1.Size = new System.Drawing.Size(501, 778);
            this.RichTextBox1.TabIndex = 5;
            this.RichTextBox1.TabStop = false;
            this.RichTextBox1.Text = "";
            // 
            // frmInstructions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 843);
            this.ControlBox = false;
            this.Controls.Add(this.gbQuiz);
            this.Controls.Add(this.cmdStart);
            this.Controls.Add(this.cmdBack);
            this.Controls.Add(this.cmdNext);
            this.Controls.Add(this.RichTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmInstructions";
            this.Text = "Instructions";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmInstructions_Load);
            this.gbQuiz.ResumeLayout(false);
            this.gbQuiz.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox gbQuiz;
        internal System.Windows.Forms.Button cmdSubmitQuiz;
        internal System.Windows.Forms.TextBox txtQuiz;
        internal System.Windows.Forms.Button cmdStart;
        internal System.Windows.Forms.Button cmdBack;
        internal System.Windows.Forms.Button cmdNext;
        internal System.Windows.Forms.RichTextBox RichTextBox1;
    }
}