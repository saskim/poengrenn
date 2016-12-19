namespace RennApplication
{
    partial class DeltagereForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeltagereForm));
            this.UsersDGV = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fornavn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Etternavn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fødselsår = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Epost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kjønn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.UsersDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // UsersDGV
            // 
            this.UsersDGV.AllowUserToDeleteRows = false;
            this.UsersDGV.BackgroundColor = System.Drawing.SystemColors.Window;
            this.UsersDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UsersDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Fornavn,
            this.Etternavn,
            this.Fødselsår,
            this.Epost,
            this.Kjønn});
            this.UsersDGV.Location = new System.Drawing.Point(14, 15);
            this.UsersDGV.Name = "UsersDGV";
            this.UsersDGV.RowTemplate.Height = 24;
            this.UsersDGV.Size = new System.Drawing.Size(813, 491);
            this.UsersDGV.TabIndex = 0;
            this.UsersDGV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.UsersDGV_CellDoubleClick);
            this.UsersDGV.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.UsersDGV_CellDoubleClick);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 50;
            // 
            // Fornavn
            // 
            this.Fornavn.HeaderText = "Fornavn";
            this.Fornavn.Name = "Fornavn";
            this.Fornavn.Width = 120;
            // 
            // Etternavn
            // 
            this.Etternavn.HeaderText = "Etternavn";
            this.Etternavn.Name = "Etternavn";
            this.Etternavn.Width = 200;
            // 
            // Fødselsår
            // 
            this.Fødselsår.HeaderText = "Fødselsår";
            this.Fødselsår.Name = "Fødselsår";
            this.Fødselsår.Width = 80;
            // 
            // Epost
            // 
            this.Epost.HeaderText = "Epost";
            this.Epost.Name = "Epost";
            this.Epost.Width = 250;
            // 
            // Kjønn
            // 
            this.Kjønn.HeaderText = "Kjønn";
            this.Kjønn.Items.AddRange(new object[] {
            "Jente",
            "Gutt"});
            this.Kjønn.Name = "Kjønn";
            this.Kjønn.Width = 70;
            // 
            // DeltagereForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 540);
            this.Controls.Add(this.UsersDGV);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DeltagereForm";
            this.Text = "Søk etter deltagere";
            ((System.ComponentModel.ISupportInitialize)(this.UsersDGV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView UsersDGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fornavn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Etternavn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fødselsår;
        private System.Windows.Forms.DataGridViewTextBoxColumn Epost;
        private System.Windows.Forms.DataGridViewComboBoxColumn Kjønn;
    }
}