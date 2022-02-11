namespace Server
{
    partial class frmSetup2
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetup2));
            this.dgGroups = new System.Windows.Forms.DataGridView();
            this.cmdSave = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.cmdCopyDown = new System.Windows.Forms.Button();
            this.DataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.cmdCopyRight = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgGroups)).BeginInit();
            this.SuspendLayout();
            // 
            // dgGroups
            // 
            this.dgGroups.AllowUserToAddRows = false;
            this.dgGroups.AllowUserToDeleteRows = false;
            this.dgGroups.AllowUserToResizeColumns = false;
            this.dgGroups.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgGroups.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgGroups.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DataGridViewTextBoxColumn1,
            this.Column1});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgGroups.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgGroups.Location = new System.Drawing.Point(12, 12);
            this.dgGroups.MultiSelect = false;
            this.dgGroups.Name = "dgGroups";
            this.dgGroups.RowHeadersVisible = false;
            this.dgGroups.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgGroups.Size = new System.Drawing.Size(1269, 456);
            this.dgGroups.TabIndex = 37;
            this.dgGroups.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellClick);
            // 
            // cmdSave
            // 
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Location = new System.Drawing.Point(1055, 476);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(226, 32);
            this.cmdSave.TabIndex = 38;
            this.cmdSave.Text = "Save and Close";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdCopyDown
            // 
            this.cmdCopyDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCopyDown.Location = new System.Drawing.Point(591, 476);
            this.cmdCopyDown.Name = "cmdCopyDown";
            this.cmdCopyDown.Size = new System.Drawing.Size(226, 32);
            this.cmdCopyDown.TabIndex = 39;
            this.cmdCopyDown.Text = "Copy Row Down";
            this.cmdCopyDown.UseVisualStyleBackColor = true;
            this.cmdCopyDown.Click += new System.EventHandler(this.cmdCopyDown_Click);
            // 
            // DataGridViewTextBoxColumn1
            // 
            this.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.DataGridViewTextBoxColumn1.HeaderText = "Period";
            this.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1";
            this.DataGridViewTextBoxColumn1.ReadOnly = true;
            this.DataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewTextBoxColumn1.Width = 60;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column1.HeaderText = "Player 1";
            this.Column1.Items.AddRange(new object[] {
            "Individual",
            "Random",
            "Sorted"});
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column1.Width = 90;
            // 
            // cmdCopyRight
            // 
            this.cmdCopyRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCopyRight.Location = new System.Drawing.Point(823, 476);
            this.cmdCopyRight.Name = "cmdCopyRight";
            this.cmdCopyRight.Size = new System.Drawing.Size(226, 32);
            this.cmdCopyRight.TabIndex = 40;
            this.cmdCopyRight.Text = "Copy Cell Right";
            this.cmdCopyRight.UseVisualStyleBackColor = true;
            this.cmdCopyRight.Click += new System.EventHandler(this.cmdCopyRight_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(45, 476);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(445, 16);
            this.label2.TabIndex = 42;
            this.label2.Text = "*All players will have their groups set after the Individual phase.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(45, 494);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(325, 16);
            this.label1.TabIndex = 43;
            this.label1.Text = "*All players must change set at the same time.";
            // 
            // frmSetup2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1293, 517);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmdCopyRight);
            this.Controls.Add(this.cmdCopyDown);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.dgGroups);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSetup2";
            this.Text = "Group Setup";
            this.Load += new System.EventHandler(this.frmSetup2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgGroups)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.DataGridView dgGroups;
        internal System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.ColorDialog colorDialog1;
        internal System.Windows.Forms.Button cmdCopyDown;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column1;
        internal System.Windows.Forms.Button cmdCopyRight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}