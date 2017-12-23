namespace DataMerge
{
    partial class frmDataMerge
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.theirDataDialog = new System.Windows.Forms.OpenFileDialog();
            this.ourDataDialog = new System.Windows.Forms.OpenFileDialog();
            this.grpTheirData = new System.Windows.Forms.GroupBox();
            this.btnTheirData = new System.Windows.Forms.Button();
            this.groOurData = new System.Windows.Forms.GroupBox();
            this.btnOurData = new System.Windows.Forms.Button();
            this.tlpMainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.grpOurDataPreview = new System.Windows.Forms.GroupBox();
            this.dgvOurData = new System.Windows.Forms.DataGridView();
            this.btnSaveMergedData = new System.Windows.Forms.Button();
            this.grpTheirDataPreview = new System.Windows.Forms.GroupBox();
            this.dgvTheirData = new System.Windows.Forms.DataGridView();
            this.flpTheirDataFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.flpOurDataFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.saveMergedData = new System.Windows.Forms.SaveFileDialog();
            this.grpTheirDataPreviewSize = new System.Windows.Forms.GroupBox();
            this.grpOurDataPreviewSize = new System.Windows.Forms.GroupBox();
            this.lbTheirDataPreviewSize = new System.Windows.Forms.ListBox();
            this.lbOurDataPreviewSize = new System.Windows.Forms.ListBox();
            this.grpTheirData.SuspendLayout();
            this.groOurData.SuspendLayout();
            this.tlpMainLayout.SuspendLayout();
            this.grpOurDataPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOurData)).BeginInit();
            this.grpTheirDataPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTheirData)).BeginInit();
            this.flpTheirDataFlow.SuspendLayout();
            this.flpOurDataFlow.SuspendLayout();
            this.grpTheirDataPreviewSize.SuspendLayout();
            this.grpOurDataPreviewSize.SuspendLayout();
            this.SuspendLayout();
            // 
            // theirDataDialog
            // 
            this.theirDataDialog.Filter = "Excel|*.xlsx;*.xls";
            // 
            // ourDataDialog
            // 
            this.ourDataDialog.Filter = "Excel|*.xlsx;*.xls";
            // 
            // grpTheirData
            // 
            this.grpTheirData.Controls.Add(this.btnTheirData);
            this.grpTheirData.Location = new System.Drawing.Point(3, 3);
            this.grpTheirData.Name = "grpTheirData";
            this.grpTheirData.Size = new System.Drawing.Size(200, 96);
            this.grpTheirData.TabIndex = 0;
            this.grpTheirData.TabStop = false;
            this.grpTheirData.Text = "Their Data";
            // 
            // btnTheirData
            // 
            this.btnTheirData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTheirData.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTheirData.Location = new System.Drawing.Point(3, 16);
            this.btnTheirData.Name = "btnTheirData";
            this.btnTheirData.Size = new System.Drawing.Size(194, 77);
            this.btnTheirData.TabIndex = 0;
            this.btnTheirData.Text = "Open &Their Data";
            this.btnTheirData.UseVisualStyleBackColor = true;
            this.btnTheirData.Click += new System.EventHandler(this.btnTheirData_Click);
            // 
            // groOurData
            // 
            this.groOurData.Controls.Add(this.btnOurData);
            this.groOurData.Location = new System.Drawing.Point(3, 3);
            this.groOurData.Name = "groOurData";
            this.groOurData.Size = new System.Drawing.Size(200, 93);
            this.groOurData.TabIndex = 2;
            this.groOurData.TabStop = false;
            this.groOurData.Text = "Our Data";
            // 
            // btnOurData
            // 
            this.btnOurData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOurData.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOurData.Location = new System.Drawing.Point(3, 16);
            this.btnOurData.Name = "btnOurData";
            this.btnOurData.Size = new System.Drawing.Size(194, 74);
            this.btnOurData.TabIndex = 0;
            this.btnOurData.Text = "Open &Our Data";
            this.btnOurData.UseVisualStyleBackColor = true;
            this.btnOurData.Click += new System.EventHandler(this.btnOurData_Click);
            // 
            // tlpMainLayout
            // 
            this.tlpMainLayout.ColumnCount = 3;
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMainLayout.Controls.Add(this.grpOurDataPreview, 2, 1);
            this.tlpMainLayout.Controls.Add(this.btnSaveMergedData, 2, 2);
            this.tlpMainLayout.Controls.Add(this.grpTheirDataPreview, 0, 1);
            this.tlpMainLayout.Controls.Add(this.flpTheirDataFlow, 0, 0);
            this.tlpMainLayout.Controls.Add(this.flpOurDataFlow, 2, 0);
            this.tlpMainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainLayout.Location = new System.Drawing.Point(0, 0);
            this.tlpMainLayout.Name = "tlpMainLayout";
            this.tlpMainLayout.RowCount = 3;
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tlpMainLayout.Size = new System.Drawing.Size(1377, 977);
            this.tlpMainLayout.TabIndex = 2;
            // 
            // grpOurDataPreview
            // 
            this.grpOurDataPreview.Controls.Add(this.dgvOurData);
            this.grpOurDataPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpOurDataPreview.Location = new System.Drawing.Point(701, 108);
            this.grpOurDataPreview.Name = "grpOurDataPreview";
            this.grpOurDataPreview.Size = new System.Drawing.Size(673, 791);
            this.grpOurDataPreview.TabIndex = 0;
            this.grpOurDataPreview.TabStop = false;
            this.grpOurDataPreview.Text = "Our Data Preview";
            // 
            // dgvOurData
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.dgvOurData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvOurData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvOurData.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvOurData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOurData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOurData.Location = new System.Drawing.Point(3, 16);
            this.dgvOurData.Name = "dgvOurData";
            this.dgvOurData.ReadOnly = true;
            this.dgvOurData.RowHeadersVisible = false;
            this.dgvOurData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOurData.Size = new System.Drawing.Size(667, 772);
            this.dgvOurData.TabIndex = 0;
            // 
            // btnSaveMergedData
            // 
            this.btnSaveMergedData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSaveMergedData.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveMergedData.Location = new System.Drawing.Point(701, 905);
            this.btnSaveMergedData.Name = "btnSaveMergedData";
            this.btnSaveMergedData.Size = new System.Drawing.Size(673, 69);
            this.btnSaveMergedData.TabIndex = 4;
            this.btnSaveMergedData.Text = "&Save Merged Data";
            this.btnSaveMergedData.UseVisualStyleBackColor = true;
            this.btnSaveMergedData.Click += new System.EventHandler(this.btnSaveMergedData_Click);
            // 
            // grpTheirDataPreview
            // 
            this.grpTheirDataPreview.Controls.Add(this.dgvTheirData);
            this.grpTheirDataPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpTheirDataPreview.Location = new System.Drawing.Point(3, 108);
            this.grpTheirDataPreview.Name = "grpTheirDataPreview";
            this.grpTheirDataPreview.Size = new System.Drawing.Size(672, 791);
            this.grpTheirDataPreview.TabIndex = 3;
            this.grpTheirDataPreview.TabStop = false;
            this.grpTheirDataPreview.Text = "Their Data Preview";
            // 
            // dgvTheirData
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvTheirData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvTheirData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvTheirData.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvTheirData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTheirData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTheirData.Location = new System.Drawing.Point(3, 16);
            this.dgvTheirData.Name = "dgvTheirData";
            this.dgvTheirData.ReadOnly = true;
            this.dgvTheirData.RowHeadersVisible = false;
            this.dgvTheirData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTheirData.Size = new System.Drawing.Size(666, 772);
            this.dgvTheirData.TabIndex = 0;
            // 
            // flpTheirDataFlow
            // 
            this.flpTheirDataFlow.Controls.Add(this.grpTheirData);
            this.flpTheirDataFlow.Controls.Add(this.grpTheirDataPreviewSize);
            this.flpTheirDataFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpTheirDataFlow.Location = new System.Drawing.Point(3, 3);
            this.flpTheirDataFlow.Name = "flpTheirDataFlow";
            this.flpTheirDataFlow.Size = new System.Drawing.Size(672, 99);
            this.flpTheirDataFlow.TabIndex = 4;
            // 
            // flpOurDataFlow
            // 
            this.flpOurDataFlow.Controls.Add(this.groOurData);
            this.flpOurDataFlow.Controls.Add(this.grpOurDataPreviewSize);
            this.flpOurDataFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpOurDataFlow.Location = new System.Drawing.Point(701, 3);
            this.flpOurDataFlow.Name = "flpOurDataFlow";
            this.flpOurDataFlow.Size = new System.Drawing.Size(673, 99);
            this.flpOurDataFlow.TabIndex = 5;
            // 
            // saveMergedData
            // 
            this.saveMergedData.Filter = "CSV|*.csv";
            // 
            // grpTheirDataPreviewSize
            // 
            this.grpTheirDataPreviewSize.Controls.Add(this.lbTheirDataPreviewSize);
            this.grpTheirDataPreviewSize.Dock = System.Windows.Forms.DockStyle.Right;
            this.grpTheirDataPreviewSize.Location = new System.Drawing.Point(209, 3);
            this.grpTheirDataPreviewSize.Name = "grpTheirDataPreviewSize";
            this.grpTheirDataPreviewSize.Size = new System.Drawing.Size(164, 96);
            this.grpTheirDataPreviewSize.TabIndex = 1;
            this.grpTheirDataPreviewSize.TabStop = false;
            this.grpTheirDataPreviewSize.Text = "Preview Size";
            // 
            // grpOurDataPreviewSize
            // 
            this.grpOurDataPreviewSize.Controls.Add(this.lbOurDataPreviewSize);
            this.grpOurDataPreviewSize.Dock = System.Windows.Forms.DockStyle.Right;
            this.grpOurDataPreviewSize.Location = new System.Drawing.Point(209, 3);
            this.grpOurDataPreviewSize.Name = "grpOurDataPreviewSize";
            this.grpOurDataPreviewSize.Size = new System.Drawing.Size(164, 93);
            this.grpOurDataPreviewSize.TabIndex = 3;
            this.grpOurDataPreviewSize.TabStop = false;
            this.grpOurDataPreviewSize.Text = "Preview Size";
            // 
            // lbTheirDataPreviewSize
            // 
            this.lbTheirDataPreviewSize.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTheirDataPreviewSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTheirDataPreviewSize.FormattingEnabled = true;
            this.lbTheirDataPreviewSize.ItemHeight = 24;
            this.lbTheirDataPreviewSize.Items.AddRange(new object[] {
            "None",
            "100",
            "1,000",
            "10,000",
            "100,000",
            "All"});
            this.lbTheirDataPreviewSize.Location = new System.Drawing.Point(6, 16);
            this.lbTheirDataPreviewSize.Name = "lbTheirDataPreviewSize";
            this.lbTheirDataPreviewSize.Size = new System.Drawing.Size(152, 76);
            this.lbTheirDataPreviewSize.TabIndex = 1;
            this.lbTheirDataPreviewSize.SelectedIndexChanged += new System.EventHandler(this.lbTheirDataPreviewSize_SelectedIndexChanged);
            // 
            // lbOurDataPreviewSize
            // 
            this.lbOurDataPreviewSize.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbOurDataPreviewSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbOurDataPreviewSize.FormattingEnabled = true;
            this.lbOurDataPreviewSize.ItemHeight = 24;
            this.lbOurDataPreviewSize.Items.AddRange(new object[] {
            "None",
            "100",
            "1,000",
            "10,000",
            "100,000",
            "All"});
            this.lbOurDataPreviewSize.Location = new System.Drawing.Point(6, 16);
            this.lbOurDataPreviewSize.Name = "lbOurDataPreviewSize";
            this.lbOurDataPreviewSize.Size = new System.Drawing.Size(152, 76);
            this.lbOurDataPreviewSize.TabIndex = 2;
            this.lbOurDataPreviewSize.SelectedIndexChanged += new System.EventHandler(this.lbOurDataPreviewSize_SelectedIndexChanged);
            // 
            // frmDataMerge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1377, 977);
            this.Controls.Add(this.tlpMainLayout);
            this.MinimumSize = new System.Drawing.Size(850, 550);
            this.Name = "frmDataMerge";
            this.Text = "DataMerge";
            this.grpTheirData.ResumeLayout(false);
            this.groOurData.ResumeLayout(false);
            this.tlpMainLayout.ResumeLayout(false);
            this.grpOurDataPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOurData)).EndInit();
            this.grpTheirDataPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTheirData)).EndInit();
            this.flpTheirDataFlow.ResumeLayout(false);
            this.flpOurDataFlow.ResumeLayout(false);
            this.grpTheirDataPreviewSize.ResumeLayout(false);
            this.grpOurDataPreviewSize.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog theirDataDialog;
        private System.Windows.Forms.OpenFileDialog ourDataDialog;
        private System.Windows.Forms.GroupBox grpTheirData;
        private System.Windows.Forms.Button btnTheirData;
        private System.Windows.Forms.GroupBox groOurData;
        private System.Windows.Forms.Button btnOurData;
        private System.Windows.Forms.TableLayoutPanel tlpMainLayout;
        private System.Windows.Forms.GroupBox grpOurDataPreview;
        private System.Windows.Forms.DataGridView dgvOurData;
        private System.Windows.Forms.Button btnSaveMergedData;
        private System.Windows.Forms.GroupBox grpTheirDataPreview;
        private System.Windows.Forms.DataGridView dgvTheirData;
        private System.Windows.Forms.SaveFileDialog saveMergedData;
        private System.Windows.Forms.FlowLayoutPanel flpTheirDataFlow;
        private System.Windows.Forms.FlowLayoutPanel flpOurDataFlow;
        private System.Windows.Forms.GroupBox grpTheirDataPreviewSize;
        private System.Windows.Forms.GroupBox grpOurDataPreviewSize;
        private System.Windows.Forms.ListBox lbTheirDataPreviewSize;
        private System.Windows.Forms.ListBox lbOurDataPreviewSize;
    }
}

