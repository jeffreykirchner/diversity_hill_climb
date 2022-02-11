namespace Client
{
    partial class frmNames
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNames));
            this.txtIDNumber = new System.Windows.Forms.TextBox();
            this.lblEarnings = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cmdSubmit = new System.Windows.Forms.Button();
            this.lbl1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtIDNumber
            // 
            this.txtIDNumber.BackColor = System.Drawing.Color.White;
            this.txtIDNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIDNumber.ForeColor = System.Drawing.Color.LightGray;
            this.txtIDNumber.Location = new System.Drawing.Point(47, 85);
            this.txtIDNumber.Name = "txtIDNumber";
            this.txtIDNumber.Size = new System.Drawing.Size(306, 29);
            this.txtIDNumber.TabIndex = 9;
            this.txtIDNumber.TabStop = false;
            this.txtIDNumber.Text = "<Your Student ID Number>";
            this.txtIDNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIDNumber.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtIDNumber_MouseClick);
            // 
            // lblEarnings
            // 
            this.lblEarnings.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEarnings.Location = new System.Drawing.Point(12, 9);
            this.lblEarnings.Name = "lblEarnings";
            this.lblEarnings.Size = new System.Drawing.Size(376, 33);
            this.lblEarnings.TabIndex = 11;
            this.lblEarnings.Text = "Your Earnings Are: $000.00";
            this.lblEarnings.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.ForeColor = System.Drawing.Color.LightGray;
            this.txtName.Location = new System.Drawing.Point(47, 50);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(306, 29);
            this.txtName.TabIndex = 8;
            this.txtName.TabStop = false;
            this.txtName.Text = "<Your Name Here>";
            this.txtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtName.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtName_MouseClick);
            // 
            // cmdSubmit
            // 
            this.cmdSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmdSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSubmit.Location = new System.Drawing.Point(47, 133);
            this.cmdSubmit.Name = "cmdSubmit";
            this.cmdSubmit.Size = new System.Drawing.Size(306, 42);
            this.cmdSubmit.TabIndex = 10;
            this.cmdSubmit.TabStop = false;
            this.cmdSubmit.Text = "Submit";
            this.cmdSubmit.UseVisualStyleBackColor = false;
            this.cmdSubmit.Click += new System.EventHandler(this.cmdSubmit_Click);
            // 
            // lbl1
            // 
            this.lbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl1.Location = new System.Drawing.Point(8, 59);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(380, 33);
            this.lbl1.TabIndex = 12;
            this.lbl1.Text = "Please remain in your seat quietly.";
            this.lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl1.Visible = false;
            // 
            // frmNames
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 192);
            this.ControlBox = false;
            this.Controls.Add(this.txtIDNumber);
            this.Controls.Add(this.lblEarnings);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.cmdSubmit);
            this.Controls.Add(this.lbl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmNames";
            this.Text = "Enter Your Name";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmNames_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtIDNumber;
        internal System.Windows.Forms.Label lblEarnings;
        internal System.Windows.Forms.TextBox txtName;
        internal System.Windows.Forms.Button cmdSubmit;
        internal System.Windows.Forms.Label lbl1;
    }
}