namespace Client
{
    partial class frmTestMode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTestMode));
            this.cmdTestMode = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdTestMode
            // 
            this.cmdTestMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTestMode.Location = new System.Drawing.Point(12, 12);
            this.cmdTestMode.Name = "cmdTestMode";
            this.cmdTestMode.Size = new System.Drawing.Size(253, 51);
            this.cmdTestMode.TabIndex = 0;
            this.cmdTestMode.Text = "Take Control";
            this.cmdTestMode.UseVisualStyleBackColor = true;
            this.cmdTestMode.Click += new System.EventHandler(this.cmdTestMode_Click);
            // 
            // frmTestMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 74);
            this.ControlBox = false;
            this.Controls.Add(this.cmdTestMode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTestMode";
            this.Text = "Test Mode";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdTestMode;
    }
}