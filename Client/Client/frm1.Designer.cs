namespace Client
{
    partial class frm1
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtMessages = new System.Windows.Forms.TextBox();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.pnlMain = new System.Windows.Forms.Panel();
            this.cmdReady = new System.Windows.Forms.Button();
            this.dgResults = new System.Windows.Forms.DataGridView();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Turn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblMaxTurns = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDistance = new System.Windows.Forms.Label();
            this.dgMoves = new System.Windows.Forms.DataGridView();
            this.DataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdSubmit = new System.Windows.Forms.Button();
            this.gpProgramResults = new System.Windows.Forms.GroupBox();
            this.lblMaxValue = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblEndingValue = new System.Windows.Forms.Label();
            this.lblStartingValue = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.gbGroups = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblGroup2 = new System.Windows.Forms.Label();
            this.dgGroup = new System.Windows.Forms.DataGridView();
            this.Round = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbSummary = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTotalEarnings = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPeriodEarnings = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPeriod = new System.Windows.Forms.TextBox();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.label10 = new System.Windows.Forms.Label();
            this.txtTimeRemaining = new System.Windows.Forms.TextBox();
            this.timer4 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgResults)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMoves)).BeginInit();
            this.gpProgramResults.SuspendLayout();
            this.gbGroups.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgGroup)).BeginInit();
            this.gbSummary.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtMessages
            // 
            this.txtMessages.BackColor = System.Drawing.Color.White;
            this.txtMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessages.Location = new System.Drawing.Point(497, 915);
            this.txtMessages.Name = "txtMessages";
            this.txtMessages.ReadOnly = true;
            this.txtMessages.Size = new System.Drawing.Size(798, 29);
            this.txtMessages.TabIndex = 38;
            this.txtMessages.TabStop = false;
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlMain.Location = new System.Drawing.Point(497, 13);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(800, 800);
            this.pnlMain.TabIndex = 39;
            this.pnlMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlMain_MouseMove);
            // 
            // cmdReady
            // 
            this.cmdReady.BackColor = System.Drawing.Color.PaleGreen;
            this.cmdReady.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdReady.Location = new System.Drawing.Point(1048, 827);
            this.cmdReady.Name = "cmdReady";
            this.cmdReady.Size = new System.Drawing.Size(247, 82);
            this.cmdReady.TabIndex = 43;
            this.cmdReady.Text = "Ready to Go On";
            this.cmdReady.UseVisualStyleBackColor = false;
            this.cmdReady.Visible = false;
            this.cmdReady.Click += new System.EventHandler(this.cmdReady_Click);
            // 
            // dgResults
            // 
            this.dgResults.AllowUserToAddRows = false;
            this.dgResults.AllowUserToDeleteRows = false;
            this.dgResults.AllowUserToResizeColumns = false;
            this.dgResults.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgResults.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column7,
            this.Column4,
            this.Turn,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewComboBoxColumn1,
            this.dataGridViewTextBoxColumn3,
            this.Column3});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgResults.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgResults.Location = new System.Drawing.Point(8, 25);
            this.dgResults.MultiSelect = false;
            this.dgResults.Name = "dgResults";
            this.dgResults.RowHeadersVisible = false;
            this.dgResults.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgResults.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgResults.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Gold;
            this.dgResults.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgResults.RowTemplate.Height = 26;
            this.dgResults.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgResults.Size = new System.Drawing.Size(581, 814);
            this.dgResults.TabIndex = 45;
            this.dgResults.SelectionChanged += new System.EventHandler(this.dgResults_SelectionChanged);
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Round";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column7.Width = 70;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column4.HeaderText = "Player";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 64;
            // 
            // Turn
            // 
            this.Turn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Turn.HeaderText = "Turn";
            this.Turn.Name = "Turn";
            this.Turn.ReadOnly = true;
            this.Turn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Turn.Width = 51;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dataGridViewTextBoxColumn2.HeaderText = "Move";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 57;
            // 
            // dataGridViewComboBoxColumn1
            // 
            this.dataGridViewComboBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewComboBoxColumn1.HeaderText = "Direction";
            this.dataGridViewComboBoxColumn1.Name = "dataGridViewComboBoxColumn1";
            this.dataGridViewComboBoxColumn1.ReadOnly = true;
            this.dataGridViewComboBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewComboBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewComboBoxColumn1.Width = 87;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn3.HeaderText = "Distance";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 86;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column3.HeaderText = "Value";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 61;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblMaxTurns);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblDistance);
            this.groupBox1.Controls.Add(this.dgMoves);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.DimGray;
            this.groupBox1.Location = new System.Drawing.Point(7, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(485, 322);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Program Your Search Pattern";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(6, 283);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(473, 34);
            this.label3.TabIndex = 50;
            this.label3.Text = "You will be paid for the single highest value found across all turns.";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMaxTurns
            // 
            this.lblMaxTurns.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxTurns.ForeColor = System.Drawing.Color.DimGray;
            this.lblMaxTurns.Location = new System.Drawing.Point(6, 243);
            this.lblMaxTurns.Name = "lblMaxTurns";
            this.lblMaxTurns.Size = new System.Drawing.Size(473, 36);
            this.lblMaxTurns.TabIndex = 49;
            this.lblMaxTurns.Text = "Your program will take up to N1 turns, consisting of the N2 moves specified above" +
    ", as long as a higher value is found each turn.";
            this.lblMaxTurns.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(6, 213);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(473, 23);
            this.label1.TabIndex = 48;
            this.label1.Text = "You will start at a random location on the dial.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDistance
            // 
            this.lblDistance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDistance.ForeColor = System.Drawing.Color.DimGray;
            this.lblDistance.Location = new System.Drawing.Point(6, 188);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(473, 21);
            this.lblDistance.TabIndex = 47;
            this.lblDistance.Text = "Distance can be 1 to NNNN.";
            this.lblDistance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgMoves
            // 
            this.dgMoves.AllowUserToAddRows = false;
            this.dgMoves.AllowUserToDeleteRows = false;
            this.dgMoves.AllowUserToResizeColumns = false;
            this.dgMoves.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMoves.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgMoves.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMoves.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DataGridViewTextBoxColumn1,
            this.Column1,
            this.Column2});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgMoves.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgMoves.Location = new System.Drawing.Point(66, 25);
            this.dgMoves.MultiSelect = false;
            this.dgMoves.Name = "dgMoves";
            this.dgMoves.RowHeadersVisible = false;
            this.dgMoves.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgMoves.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgMoves.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.dgMoves.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgMoves.RowTemplate.Height = 26;
            this.dgMoves.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgMoves.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgMoves.Size = new System.Drawing.Size(367, 156);
            this.dgMoves.TabIndex = 45;
            // 
            // DataGridViewTextBoxColumn1
            // 
            this.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.DataGridViewTextBoxColumn1.HeaderText = "Move";
            this.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1";
            this.DataGridViewTextBoxColumn1.ReadOnly = true;
            this.DataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewTextBoxColumn1.Width = 57;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Direction";
            this.Column1.Items.AddRange(new object[] {
            "← Counter Clockwise",
            "Clockwise →"});
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column1.Width = 200;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column2.HeaderText = "Distance";
            this.Column2.Name = "Column2";
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 86;
            // 
            // cmdSubmit
            // 
            this.cmdSubmit.BackColor = System.Drawing.Color.PaleGreen;
            this.cmdSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSubmit.ForeColor = System.Drawing.Color.Black;
            this.cmdSubmit.Location = new System.Drawing.Point(7, 331);
            this.cmdSubmit.Name = "cmdSubmit";
            this.cmdSubmit.Size = new System.Drawing.Size(482, 35);
            this.cmdSubmit.TabIndex = 46;
            this.cmdSubmit.Text = "Submit";
            this.cmdSubmit.UseVisualStyleBackColor = false;
            this.cmdSubmit.Click += new System.EventHandler(this.cmdSubmit_Click);
            // 
            // gpProgramResults
            // 
            this.gpProgramResults.Controls.Add(this.lblMaxValue);
            this.gpProgramResults.Controls.Add(this.label6);
            this.gpProgramResults.Controls.Add(this.lblEndingValue);
            this.gpProgramResults.Controls.Add(this.lblStartingValue);
            this.gpProgramResults.Controls.Add(this.label5);
            this.gpProgramResults.Controls.Add(this.label4);
            this.gpProgramResults.Controls.Add(this.dgResults);
            this.gpProgramResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpProgramResults.ForeColor = System.Drawing.Color.DimGray;
            this.gpProgramResults.Location = new System.Drawing.Point(1301, 3);
            this.gpProgramResults.Name = "gpProgramResults";
            this.gpProgramResults.Size = new System.Drawing.Size(595, 941);
            this.gpProgramResults.TabIndex = 44;
            this.gpProgramResults.TabStop = false;
            this.gpProgramResults.Text = "Program Results";
            // 
            // lblMaxValue
            // 
            this.lblMaxValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxValue.ForeColor = System.Drawing.Color.Black;
            this.lblMaxValue.Location = new System.Drawing.Point(364, 898);
            this.lblMaxValue.Name = "lblMaxValue";
            this.lblMaxValue.Size = new System.Drawing.Size(103, 28);
            this.lblMaxValue.TabIndex = 56;
            this.lblMaxValue.Text = "NNNN";
            this.lblMaxValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(152, 898);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(206, 28);
            this.label6.TabIndex = 55;
            this.label6.Text = "Max Possible Value:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEndingValue
            // 
            this.lblEndingValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndingValue.ForeColor = System.Drawing.Color.Black;
            this.lblEndingValue.Location = new System.Drawing.Point(364, 870);
            this.lblEndingValue.Name = "lblEndingValue";
            this.lblEndingValue.Size = new System.Drawing.Size(103, 28);
            this.lblEndingValue.TabIndex = 54;
            this.lblEndingValue.Text = "NNNN";
            this.lblEndingValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStartingValue
            // 
            this.lblStartingValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartingValue.ForeColor = System.Drawing.Color.Black;
            this.lblStartingValue.Location = new System.Drawing.Point(364, 842);
            this.lblStartingValue.Name = "lblStartingValue";
            this.lblStartingValue.Size = new System.Drawing.Size(103, 28);
            this.lblStartingValue.TabIndex = 53;
            this.lblStartingValue.Text = "NNNN";
            this.lblStartingValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(152, 870);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(206, 28);
            this.label5.TabIndex = 52;
            this.label5.Text = "Your Ending Value:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(152, 842);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(206, 28);
            this.label4.TabIndex = 51;
            this.label4.Text = "Starting Value:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gbGroups
            // 
            this.gbGroups.Controls.Add(this.label9);
            this.gbGroups.Controls.Add(this.lblGroup2);
            this.gbGroups.Controls.Add(this.dgGroup);
            this.gbGroups.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbGroups.ForeColor = System.Drawing.Color.DimGray;
            this.gbGroups.Location = new System.Drawing.Point(7, 372);
            this.gbGroups.Name = "gbGroups";
            this.gbGroups.Size = new System.Drawing.Size(482, 572);
            this.gbGroups.TabIndex = 45;
            this.gbGroups.TabStop = false;
            this.gbGroups.Text = "Group Information";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.DimGray;
            this.label9.Location = new System.Drawing.Point(10, 516);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(463, 50);
            this.label9.TabIndex = 53;
            this.label9.Text = "Another round will be run, starting from the ending point of the last round, if a" +
    "t least one player finds a higher value than the starting value.";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGroup2
            // 
            this.lblGroup2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGroup2.ForeColor = System.Drawing.Color.DimGray;
            this.lblGroup2.Location = new System.Drawing.Point(10, 480);
            this.lblGroup2.Name = "lblGroup2";
            this.lblGroup2.Size = new System.Drawing.Size(463, 25);
            this.lblGroup2.TabIndex = 52;
            this.lblGroup2.Text = "Each period your group\'s turn order will be randomly generated. ";
            this.lblGroup2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgGroup
            // 
            this.dgGroup.AllowUserToAddRows = false;
            this.dgGroup.AllowUserToDeleteRows = false;
            this.dgGroup.AllowUserToResizeColumns = false;
            this.dgGroup.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgGroup.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgGroup.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Round,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewComboBoxColumn2,
            this.Column5,
            this.Column6,
            this.Column8});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgGroup.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgGroup.Location = new System.Drawing.Point(8, 25);
            this.dgGroup.MultiSelect = false;
            this.dgGroup.Name = "dgGroup";
            this.dgGroup.RowHeadersVisible = false;
            this.dgGroup.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgGroup.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgGroup.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.White;
            this.dgGroup.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgGroup.RowTemplate.Height = 26;
            this.dgGroup.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgGroup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgGroup.Size = new System.Drawing.Size(465, 453);
            this.dgGroup.TabIndex = 46;
            this.dgGroup.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgGroup_CellMouseMove);
            // 
            // Round
            // 
            this.Round.HeaderText = "Round";
            this.Round.Name = "Round";
            this.Round.ReadOnly = true;
            this.Round.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Round.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Round.Width = 65;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Turn Order";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn5.Width = 60;
            // 
            // dataGridViewComboBoxColumn2
            // 
            this.dataGridViewComboBoxColumn2.HeaderText = "Player";
            this.dataGridViewComboBoxColumn2.Name = "dataGridViewComboBoxColumn2";
            this.dataGridViewComboBoxColumn2.ReadOnly = true;
            this.dataGridViewComboBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewComboBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewComboBoxColumn2.Width = 90;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Start Value";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column5.Width = 75;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "End Value";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column6.Width = 75;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Turn Count";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column8.Width = 75;
            // 
            // gbSummary
            // 
            this.gbSummary.Controls.Add(this.label10);
            this.gbSummary.Controls.Add(this.txtTimeRemaining);
            this.gbSummary.Controls.Add(this.label8);
            this.gbSummary.Controls.Add(this.txtTotalEarnings);
            this.gbSummary.Controls.Add(this.label7);
            this.gbSummary.Controls.Add(this.txtPeriodEarnings);
            this.gbSummary.Controls.Add(this.label2);
            this.gbSummary.Controls.Add(this.txtPeriod);
            this.gbSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbSummary.ForeColor = System.Drawing.Color.DimGray;
            this.gbSummary.Location = new System.Drawing.Point(497, 819);
            this.gbSummary.Name = "gbSummary";
            this.gbSummary.Size = new System.Drawing.Size(545, 90);
            this.gbSummary.TabIndex = 46;
            this.gbSummary.TabStop = false;
            this.gbSummary.Text = "Summary";
            // 
            // label8
            // 
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(285, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(108, 43);
            this.label8.TabIndex = 5;
            this.label8.Text = "Total\r\nEarnings (¢)";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTotalEarnings
            // 
            this.txtTotalEarnings.BackColor = System.Drawing.Color.White;
            this.txtTotalEarnings.Location = new System.Drawing.Point(285, 59);
            this.txtTotalEarnings.Name = "txtTotalEarnings";
            this.txtTotalEarnings.ReadOnly = true;
            this.txtTotalEarnings.Size = new System.Drawing.Size(108, 26);
            this.txtTotalEarnings.TabIndex = 4;
            this.txtTotalEarnings.TabStop = false;
            this.txtTotalEarnings.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(154, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 43);
            this.label7.TabIndex = 3;
            this.label7.Text = "Period\r\nEarnings (¢)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPeriodEarnings
            // 
            this.txtPeriodEarnings.BackColor = System.Drawing.Color.White;
            this.txtPeriodEarnings.Location = new System.Drawing.Point(154, 59);
            this.txtPeriodEarnings.Name = "txtPeriodEarnings";
            this.txtPeriodEarnings.ReadOnly = true;
            this.txtPeriodEarnings.Size = new System.Drawing.Size(108, 26);
            this.txtPeriodEarnings.TabIndex = 2;
            this.txtPeriodEarnings.TabStop = false;
            this.txtPeriodEarnings.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(23, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Period";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPeriod
            // 
            this.txtPeriod.BackColor = System.Drawing.Color.White;
            this.txtPeriod.Location = new System.Drawing.Point(23, 60);
            this.txtPeriod.Name = "txtPeriod";
            this.txtPeriod.ReadOnly = true;
            this.txtPeriod.Size = new System.Drawing.Size(108, 26);
            this.txtPeriod.TabIndex = 0;
            this.txtPeriod.TabStop = false;
            this.txtPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // timer3
            // 
            this.timer3.Enabled = true;
            this.timer3.Interval = 250;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // label10
            // 
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(416, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(108, 43);
            this.label10.TabIndex = 7;
            this.label10.Text = "Time\r\nRemaining";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTimeRemaining
            // 
            this.txtTimeRemaining.BackColor = System.Drawing.Color.White;
            this.txtTimeRemaining.Location = new System.Drawing.Point(416, 58);
            this.txtTimeRemaining.Name = "txtTimeRemaining";
            this.txtTimeRemaining.ReadOnly = true;
            this.txtTimeRemaining.Size = new System.Drawing.Size(108, 26);
            this.txtTimeRemaining.TabIndex = 6;
            this.txtTimeRemaining.TabStop = false;
            this.txtTimeRemaining.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // timer4
            // 
            this.timer4.Interval = 1000;
            this.timer4.Tick += new System.EventHandler(this.timer4_Tick);
            // 
            // frm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1901, 956);
            this.ControlBox = false;
            this.Controls.Add(this.gbSummary);
            this.Controls.Add(this.cmdReady);
            this.Controls.Add(this.cmdSubmit);
            this.Controls.Add(this.gbGroups);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gpProgramResults);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.txtMessages);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frm1";
            this.Text = "Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm1_FormClosing);
            this.Load += new System.EventHandler(this.frm1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgResults)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgMoves)).EndInit();
            this.gpProgramResults.ResumeLayout(false);
            this.gbGroups.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgGroup)).EndInit();
            this.gbSummary.ResumeLayout(false);
            this.gbSummary.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Timer timer1;
        internal System.Windows.Forms.TextBox txtMessages;
        public System.Windows.Forms.Timer timer2;
        public System.Windows.Forms.Panel pnlMain;
        internal System.Windows.Forms.Button cmdReady;
        internal System.Windows.Forms.DataGridView dgResults;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label lblDistance;
        internal System.Windows.Forms.Button cmdSubmit;
        internal System.Windows.Forms.DataGridView dgMoves;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label lblMaxTurns;
        public System.Windows.Forms.Label lblEndingValue;
        public System.Windows.Forms.Label lblStartingValue;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        public System.Windows.Forms.Label lblMaxValue;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.GroupBox gpProgramResults;
        internal System.Windows.Forms.DataGridView dgGroup;
        public System.Windows.Forms.GroupBox gbGroups;
        public System.Windows.Forms.Label lblGroup2;
        private System.Windows.Forms.GroupBox gbSummary;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtTotalEarnings;
        public System.Windows.Forms.TextBox txtPeriodEarnings;
        public System.Windows.Forms.TextBox txtPeriod;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Turn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewComboBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        public System.Windows.Forms.Label label9;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Round;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewComboBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox txtTimeRemaining;
        public System.Windows.Forms.Timer timer4;
    }
}