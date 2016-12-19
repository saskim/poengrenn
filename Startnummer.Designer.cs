namespace RennApplication
{
    partial class StartnummerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartnummerForm));
            this.StartNummerGV = new System.Windows.Forms.DataGridView();
            this.OppdaterBtn = new System.Windows.Forms.Button();
            this.RekkefølgeBtn = new System.Windows.Forms.Button();
            this.Age = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Girls = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RekkefølgeJenter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Boys = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sequence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.StartNummerGV)).BeginInit();
            this.SuspendLayout();
            // 
            // StartNummerGV
            // 
            this.StartNummerGV.AllowUserToDeleteRows = false;
            this.StartNummerGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.StartNummerGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Age,
            this.Girls,
            this.RekkefølgeJenter,
            this.Boys,
            this.Sequence});
            this.StartNummerGV.Location = new System.Drawing.Point(14, 15);
            this.StartNummerGV.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.StartNummerGV.Name = "StartNummerGV";
            this.StartNummerGV.RowTemplate.Height = 24;
            this.StartNummerGV.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.StartNummerGV.Size = new System.Drawing.Size(472, 249);
            this.StartNummerGV.TabIndex = 1;
            // 
            // OppdaterBtn
            // 
            this.OppdaterBtn.AutoSize = true;
            this.OppdaterBtn.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.OppdaterBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OppdaterBtn.Location = new System.Drawing.Point(502, 15);
            this.OppdaterBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OppdaterBtn.Name = "OppdaterBtn";
            this.OppdaterBtn.Size = new System.Drawing.Size(100, 30);
            this.OppdaterBtn.TabIndex = 18;
            this.OppdaterBtn.Text = "Oppdater";
            this.OppdaterBtn.UseVisualStyleBackColor = false;
            this.OppdaterBtn.Click += new System.EventHandler(this.OppdaterBtn_Click);
            // 
            // RekkefølgeBtn
            // 
            this.RekkefølgeBtn.AutoSize = true;
            this.RekkefølgeBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.RekkefølgeBtn.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.RekkefølgeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RekkefølgeBtn.Location = new System.Drawing.Point(502, 64);
            this.RekkefølgeBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RekkefølgeBtn.Name = "RekkefølgeBtn";
            this.RekkefølgeBtn.Size = new System.Drawing.Size(100, 30);
            this.RekkefølgeBtn.TabIndex = 19;
            this.RekkefølgeBtn.Text = "Rekkefølge";
            this.RekkefølgeBtn.UseVisualStyleBackColor = false;
            this.RekkefølgeBtn.Click += new System.EventHandler(this.RekkefølgeBtn_Click);
            // 
            // Age
            // 
            this.Age.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Age.HeaderText = "Alder";
            this.Age.Name = "Age";
            this.Age.ReadOnly = true;
            this.Age.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Age.Width = 52;
            // 
            // Girls
            // 
            this.Girls.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Girls.HeaderText = "Jenter";
            this.Girls.Name = "Girls";
            this.Girls.Width = 79;
            // 
            // RekkefølgeJenter
            // 
            this.RekkefølgeJenter.HeaderText = "Rekkefølge";
            this.RekkefølgeJenter.Name = "RekkefølgeJenter";
            this.RekkefølgeJenter.ReadOnly = true;
            // 
            // Boys
            // 
            this.Boys.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Boys.HeaderText = "Gutter";
            this.Boys.Name = "Boys";
            this.Boys.Width = 80;
            // 
            // Sequence
            // 
            this.Sequence.HeaderText = "Rekkefølge";
            this.Sequence.Name = "Sequence";
            this.Sequence.ReadOnly = true;
            // 
            // StartnummerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(612, 279);
            this.Controls.Add(this.RekkefølgeBtn);
            this.Controls.Add(this.OppdaterBtn);
            this.Controls.Add(this.StartNummerGV);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "StartnummerForm";
            this.Text = "Startnummer";
            ((System.ComponentModel.ISupportInitialize)(this.StartNummerGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView StartNummerGV;
        private System.Windows.Forms.Button OppdaterBtn;
        private System.Windows.Forms.Button RekkefølgeBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Age;
        private System.Windows.Forms.DataGridViewTextBoxColumn Girls;
        private System.Windows.Forms.DataGridViewTextBoxColumn RekkefølgeJenter;
        private System.Windows.Forms.DataGridViewTextBoxColumn Boys;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sequence;
    }
}