namespace Budget
{
    partial class GroupingAssignmentForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnApplyTypes = new System.Windows.Forms.Button();
            this.chBoxShowUntypedOnly = new System.Windows.Forms.CheckBox();
            this.btnSaveBudgetItems = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnApplyToSelected = new System.Windows.Forms.Button();
            this.btnApplyOneTime = new System.Windows.Forms.Button();
            this.chBoxIgnore = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboTrType = new System.Windows.Forms.ComboBox();
            this.tbPattern = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btnSaveGroupingPatterns = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.gridGroupingPatterns = new System.Windows.Forms.DataGridView();
            this.iDDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patternDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrTypeComboColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ForIncomeColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.forIgnoreDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnApply = new System.Windows.Forms.DataGridViewButtonColumn();
            this.transacTypePatternBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mainDataSet1 = new Budget.MainDataSet();
            this.budgetEditingGridCtrl1 = new Budget.BudgetEditingGridCtrl();
            this.label1 = new System.Windows.Forms.Label();
            this.transacTypePatternTableAdapter = new Budget.MainDataSetTableAdapters.TransacTypePatternTableAdapter();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingPatterns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.transacTypePatternBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnApplyTypes
            // 
            this.btnApplyTypes.Location = new System.Drawing.Point(140, 7);
            this.btnApplyTypes.Name = "btnApplyTypes";
            this.btnApplyTypes.Size = new System.Drawing.Size(113, 23);
            this.btnApplyTypes.TabIndex = 1;
            this.btnApplyTypes.Text = "Apply All Groupings";
            this.btnApplyTypes.UseVisualStyleBackColor = true;
            this.btnApplyTypes.Click += new System.EventHandler(this.btnApplyTypes_Click);
            // 
            // chBoxShowUntypedOnly
            // 
            this.chBoxShowUntypedOnly.AutoSize = true;
            this.chBoxShowUntypedOnly.Checked = true;
            this.chBoxShowUntypedOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chBoxShowUntypedOnly.Location = new System.Drawing.Point(103, 18);
            this.chBoxShowUntypedOnly.Name = "chBoxShowUntypedOnly";
            this.chBoxShowUntypedOnly.Size = new System.Drawing.Size(118, 17);
            this.chBoxShowUntypedOnly.TabIndex = 2;
            this.chBoxShowUntypedOnly.Text = "Untyped Items Only";
            this.chBoxShowUntypedOnly.UseVisualStyleBackColor = true;
            this.chBoxShowUntypedOnly.CheckStateChanged += new System.EventHandler(this.chBoxShowUntypedOnly_CheckStateChanged);
            // 
            // btnSaveBudgetItems
            // 
            this.btnSaveBudgetItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveBudgetItems.Location = new System.Drawing.Point(0, 408);
            this.btnSaveBudgetItems.Name = "btnSaveBudgetItems";
            this.btnSaveBudgetItems.Size = new System.Drawing.Size(113, 23);
            this.btnSaveBudgetItems.TabIndex = 3;
            this.btnSaveBudgetItems.Text = "Save Changes";
            this.btnSaveBudgetItems.UseVisualStyleBackColor = true;
            this.btnSaveBudgetItems.Click += new System.EventHandler(this.btnSaveBudgetItems_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnApplyToSelected);
            this.splitContainer1.Panel1.Controls.Add(this.btnApplyOneTime);
            this.splitContainer1.Panel1.Controls.Add(this.chBoxIgnore);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.comboTrType);
            this.splitContainer1.Panel1.Controls.Add(this.tbPattern);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Controls.Add(this.btnSaveGroupingPatterns);
            this.splitContainer1.Panel1.Controls.Add(this.btnApplyTypes);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.gridGroupingPatterns);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.budgetEditingGridCtrl1);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.chBoxShowUntypedOnly);
            this.splitContainer1.Panel2.Controls.Add(this.btnSaveBudgetItems);
            this.splitContainer1.Size = new System.Drawing.Size(1276, 658);
            this.splitContainer1.SplitterDistance = 223;
            this.splitContainer1.TabIndex = 4;
            // 
            // btnApplyToSelected
            // 
            this.btnApplyToSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplyToSelected.Location = new System.Drawing.Point(1166, 0);
            this.btnApplyToSelected.Name = "btnApplyToSelected";
            this.btnApplyToSelected.Size = new System.Drawing.Size(107, 23);
            this.btnApplyToSelected.TabIndex = 15;
            this.btnApplyToSelected.Text = "Apply to Selected";
            this.btnApplyToSelected.UseVisualStyleBackColor = true;
            this.btnApplyToSelected.Click += new System.EventHandler(this.btnApplyToSelected_Click);
            // 
            // btnApplyOneTime
            // 
            this.btnApplyOneTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplyOneTime.Location = new System.Drawing.Point(1111, 0);
            this.btnApplyOneTime.Name = "btnApplyOneTime";
            this.btnApplyOneTime.Size = new System.Drawing.Size(49, 23);
            this.btnApplyOneTime.TabIndex = 14;
            this.btnApplyOneTime.Text = "Apply";
            this.btnApplyOneTime.UseVisualStyleBackColor = true;
            this.btnApplyOneTime.Click += new System.EventHandler(this.btnApplyOneTime_Click);
            // 
            // chBoxIgnore
            // 
            this.chBoxIgnore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chBoxIgnore.AutoSize = true;
            this.chBoxIgnore.Location = new System.Drawing.Point(1049, 4);
            this.chBoxIgnore.Name = "chBoxIgnore";
            this.chBoxIgnore.Size = new System.Drawing.Size(56, 17);
            this.chBoxIgnore.TabIndex = 13;
            this.chBoxIgnore.Text = "Ignore";
            this.chBoxIgnore.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(772, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "TrType:";
            // 
            // comboTrType
            // 
            this.comboTrType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboTrType.FormattingEnabled = true;
            this.comboTrType.Location = new System.Drawing.Point(822, 0);
            this.comboTrType.Name = "comboTrType";
            this.comboTrType.Size = new System.Drawing.Size(221, 21);
            this.comboTrType.TabIndex = 10;
            // 
            // tbPattern
            // 
            this.tbPattern.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPattern.Location = new System.Drawing.Point(472, 1);
            this.tbPattern.Name = "tbPattern";
            this.tbPattern.Size = new System.Drawing.Size(294, 20);
            this.tbPattern.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(378, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "One-time pattern:";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(119, 197);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Cancel Changes";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnSaveGroupingPatterns
            // 
            this.btnSaveGroupingPatterns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveGroupingPatterns.Location = new System.Drawing.Point(0, 197);
            this.btnSaveGroupingPatterns.Name = "btnSaveGroupingPatterns";
            this.btnSaveGroupingPatterns.Size = new System.Drawing.Size(113, 23);
            this.btnSaveGroupingPatterns.TabIndex = 6;
            this.btnSaveGroupingPatterns.Text = "Save Changes";
            this.btnSaveGroupingPatterns.UseVisualStyleBackColor = true;
            this.btnSaveGroupingPatterns.Click += new System.EventHandler(this.btnSaveGroupingPatterns_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(0, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Grouping Patterns";
            // 
            // gridGroupingPatterns
            // 
            this.gridGroupingPatterns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridGroupingPatterns.AutoGenerateColumns = false;
            this.gridGroupingPatterns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridGroupingPatterns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn1,
            this.patternDataGridViewTextBoxColumn,
            this.TrTypeComboColumn,
            this.ForIncomeColumn,
            this.forIgnoreDataGridViewCheckBoxColumn,
            this.ColumnApply});
            this.gridGroupingPatterns.DataSource = this.transacTypePatternBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridGroupingPatterns.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridGroupingPatterns.Location = new System.Drawing.Point(0, 32);
            this.gridGroupingPatterns.Name = "gridGroupingPatterns";
            this.gridGroupingPatterns.Size = new System.Drawing.Size(1276, 159);
            this.gridGroupingPatterns.TabIndex = 0;
            this.gridGroupingPatterns.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridGroupingPatterns_CellContentClick);
            // 
            // iDDataGridViewTextBoxColumn1
            // 
            this.iDDataGridViewTextBoxColumn1.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn1.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn1.Name = "iDDataGridViewTextBoxColumn1";
            this.iDDataGridViewTextBoxColumn1.ReadOnly = true;
            this.iDDataGridViewTextBoxColumn1.Width = 40;
            // 
            // patternDataGridViewTextBoxColumn
            // 
            this.patternDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.patternDataGridViewTextBoxColumn.DataPropertyName = "Pattern";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.patternDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.patternDataGridViewTextBoxColumn.HeaderText = "Pattern";
            this.patternDataGridViewTextBoxColumn.Name = "patternDataGridViewTextBoxColumn";
            // 
            // TrTypeComboColumn
            // 
            this.TrTypeComboColumn.DataPropertyName = "TrType";
            this.TrTypeComboColumn.HeaderText = "TrType";
            this.TrTypeComboColumn.Name = "TrTypeComboColumn";
            this.TrTypeComboColumn.Width = 140;
            // 
            // ForIncomeColumn
            // 
            this.ForIncomeColumn.DataPropertyName = "ForIncome";
            this.ForIncomeColumn.HeaderText = "Income";
            this.ForIncomeColumn.Name = "ForIncomeColumn";
            this.ForIncomeColumn.Width = 45;
            // 
            // forIgnoreDataGridViewCheckBoxColumn
            // 
            this.forIgnoreDataGridViewCheckBoxColumn.DataPropertyName = "ForIgnore";
            this.forIgnoreDataGridViewCheckBoxColumn.HeaderText = "Ignore";
            this.forIgnoreDataGridViewCheckBoxColumn.Name = "forIgnoreDataGridViewCheckBoxColumn";
            this.forIgnoreDataGridViewCheckBoxColumn.Width = 45;
            // 
            // ColumnApply
            // 
            this.ColumnApply.HeaderText = "Apply";
            this.ColumnApply.Name = "ColumnApply";
            this.ColumnApply.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnApply.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColumnApply.Text = "Apply";
            this.ColumnApply.UseColumnTextForButtonValue = true;
            // 
            // transacTypePatternBindingSource
            // 
            this.transacTypePatternBindingSource.DataMember = "TransacTypePattern";
            this.transacTypePatternBindingSource.DataSource = this.mainDataSet1;
            // 
            // mainDataSet1
            // 
            this.mainDataSet1.DataSetName = "MainDataSet";
            this.mainDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // budgetEditingGridCtrl1
            // 
            this.budgetEditingGridCtrl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.budgetEditingGridCtrl1.Location = new System.Drawing.Point(0, 37);
            this.budgetEditingGridCtrl1.Name = "budgetEditingGridCtrl1";
            this.budgetEditingGridCtrl1.Size = new System.Drawing.Size(1272, 365);
            this.budgetEditingGridCtrl1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(-3, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Budget Items";
            // 
            // transacTypePatternTableAdapter
            // 
            this.transacTypePatternTableAdapter.ClearBeforeFill = true;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.Location = new System.Drawing.Point(131, 647);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(113, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "Cancel Changes";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // GroupingAssignmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 703);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.splitContainer1);
            this.Name = "GroupingAssignmentForm";
            this.Text = "Grouping Assignment Form";
            this.Load += new System.EventHandler(this.TrTypeForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingPatterns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.transacTypePatternBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataSet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnApplyTypes;
        private System.Windows.Forms.CheckBox chBoxShowUntypedOnly;
        private System.Windows.Forms.Button btnSaveBudgetItems;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView gridGroupingPatterns;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSaveGroupingPatterns;
        private System.Windows.Forms.Label label2;
        private MainDataSet mainDataSet1;
        private System.Windows.Forms.BindingSource transacTypePatternBindingSource;
        private MainDataSetTableAdapters.TransacTypePatternTableAdapter transacTypePatternTableAdapter;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private BudgetEditingGridCtrl budgetEditingGridCtrl1;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn patternDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn TrTypeComboColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ForIncomeColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn forIgnoreDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnApply;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chBoxIgnore;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboTrType;
        private System.Windows.Forms.TextBox tbPattern;
        private System.Windows.Forms.Button btnApplyOneTime;
        private System.Windows.Forms.Button btnApplyToSelected;
    }
}