namespace MainApp
{
    partial class TestPython
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestPython));
            this.SelectFileButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.analyzeErrorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Identifier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LineNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ViolatedRule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResultGridView = new System.Windows.Forms.DataGridView();
            this.openPythonFile = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.analyzeErrorBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResultGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // SelectFileButton
            // 
            this.SelectFileButton.AccessibleDescription = "SelectFileButton";
            this.SelectFileButton.AccessibleName = "SelectFileButton";
            this.SelectFileButton.BackColor = System.Drawing.Color.Transparent;
            this.SelectFileButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SelectFileButton.BackgroundImage")));
            this.SelectFileButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SelectFileButton.Cursor = System.Windows.Forms.Cursors.PanSouth;
            this.SelectFileButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.SelectFileButton.FlatAppearance.BorderSize = 0;
            this.SelectFileButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.SelectFileButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.SelectFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectFileButton.Font = new System.Drawing.Font("Arial Black", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SelectFileButton.ForeColor = System.Drawing.Color.White;
            this.SelectFileButton.Location = new System.Drawing.Point(48, 128);
            this.SelectFileButton.Margin = new System.Windows.Forms.Padding(4);
            this.SelectFileButton.Name = "SelectFileButton";
            this.SelectFileButton.Size = new System.Drawing.Size(409, 86);
            this.SelectFileButton.TabIndex = 2;
            this.SelectFileButton.Text = "Select File";
            this.SelectFileButton.UseVisualStyleBackColor = false;
            this.SelectFileButton.Click += new System.EventHandler(this.SelectFileButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Location = new System.Drawing.Point(1121, 10);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(159, 138);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // analyzeErrorBindingSource
            // 
            this.analyzeErrorBindingSource.DataSource = typeof(Analyzer.AnalyzeError);
            // 
            // Identifier
            // 
            this.Identifier.DataPropertyName = "Identifier";
            this.Identifier.HeaderText = "Identifier";
            this.Identifier.MinimumWidth = 6;
            this.Identifier.Name = "Identifier";
            this.Identifier.ReadOnly = true;
            this.Identifier.Width = 98;
            // 
            // LineNumber
            // 
            this.LineNumber.DataPropertyName = "LineNumber";
            this.LineNumber.HeaderText = "LineNumber";
            this.LineNumber.MinimumWidth = 6;
            this.LineNumber.Name = "LineNumber";
            this.LineNumber.ReadOnly = true;
            this.LineNumber.Width = 119;
            // 
            // ViolatedRule
            // 
            this.ViolatedRule.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ViolatedRule.DataPropertyName = "ViolatedRule";
            this.ViolatedRule.HeaderText = "ViolatedRule";
            this.ViolatedRule.MinimumWidth = 6;
            this.ViolatedRule.Name = "ViolatedRule";
            this.ViolatedRule.ReadOnly = true;
            // 
            // ResultGridView
            // 
            this.ResultGridView.AllowUserToAddRows = false;
            this.ResultGridView.AllowUserToDeleteRows = false;
            this.ResultGridView.AllowUserToOrderColumns = true;
            this.ResultGridView.AutoGenerateColumns = false;
            this.ResultGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.ResultGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ResultGridView.BackgroundColor = System.Drawing.Color.DarkSlateGray;
            this.ResultGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ResultGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Identifier,
            this.LineNumber,
            this.ViolatedRule});
            this.ResultGridView.DataSource = this.analyzeErrorBindingSource;
            this.ResultGridView.Location = new System.Drawing.Point(58, 224);
            this.ResultGridView.Margin = new System.Windows.Forms.Padding(4);
            this.ResultGridView.Name = "ResultGridView";
            this.ResultGridView.RowHeadersWidth = 51;
            this.ResultGridView.Size = new System.Drawing.Size(1187, 546);
            this.ResultGridView.TabIndex = 0;
            // 
            // openPythonFile
            // 
            this.openPythonFile.FileName = "openPythonFile";
            // 
            // TestPython
            // 
            this.AccessibleDescription = "";
            this.AccessibleName = "";
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1284, 783);
            this.Controls.Add(this.ResultGridView);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.SelectFileButton);
            this.Cursor = System.Windows.Forms.Cursors.PanSouth;
            this.Name = "TestPython";
            this.Text = "TestPython";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.analyzeErrorBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResultGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SelectFileButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView ResultGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Identifier;
        private System.Windows.Forms.DataGridViewTextBoxColumn LineNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ViolatedRule;
        public System.Windows.Forms.BindingSource analyzeErrorBindingSource;
        private System.Windows.Forms.OpenFileDialog openPythonFile;
    }
}