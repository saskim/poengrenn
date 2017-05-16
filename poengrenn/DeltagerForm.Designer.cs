namespace RennApplication
{
    partial class DeltagerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeltagerForm));
            this.FornavnTB = new System.Windows.Forms.TextBox();
            this.EtternavnTB = new System.Windows.Forms.TextBox();
            this.FødselsÅrTB = new System.Windows.Forms.TextBox();
            this.EpostTB = new System.Windows.Forms.TextBox();
            this.GuttRB = new System.Windows.Forms.RadioButton();
            this.JenteRB = new System.Windows.Forms.RadioButton();
            this.BetaltCB = new System.Windows.Forms.CheckBox();
            this.FornavlLbl = new System.Windows.Forms.Label();
            this.EtternavnLbl = new System.Windows.Forms.Label();
            this.FødselsårLbl = new System.Windows.Forms.Label();
            this.EPostLbl = new System.Windows.Forms.Label();
            this.StartNummerLbl = new System.Windows.Forms.Label();
            this.KjønnGB = new System.Windows.Forms.GroupBox();
            this.OppdaterBtn = new System.Windows.Forms.Button();
            this.HentBtn = new System.Windows.Forms.Button();
            this.KlasseLbl = new System.Windows.Forms.Label();
            this.StartnummerTB = new System.Windows.Forms.TextBox();
            this.KlasseCB = new System.Windows.Forms.ComboBox();
            this.PremieCB = new System.Windows.Forms.CheckBox();
            this.BrukerStatus = new System.Windows.Forms.ToolStrip();
            this.BrukerTB = new System.Windows.Forms.ToolStripTextBox();
            this.SlettBtn = new System.Windows.Forms.Button();
            this.KjønnGB.SuspendLayout();
            this.BrukerStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // FornavnTB
            // 
            this.FornavnTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FornavnTB.Location = new System.Drawing.Point(98, 12);
            this.FornavnTB.Name = "FornavnTB";
            this.FornavnTB.Size = new System.Drawing.Size(274, 26);
            this.FornavnTB.TabIndex = 0;
            // 
            // EtternavnTB
            // 
            this.EtternavnTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EtternavnTB.Location = new System.Drawing.Point(98, 45);
            this.EtternavnTB.Name = "EtternavnTB";
            this.EtternavnTB.Size = new System.Drawing.Size(274, 26);
            this.EtternavnTB.TabIndex = 1;
            // 
            // FødselsÅrTB
            // 
            this.FødselsÅrTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FødselsÅrTB.Location = new System.Drawing.Point(98, 77);
            this.FødselsÅrTB.MaxLength = 4;
            this.FødselsÅrTB.Name = "FødselsÅrTB";
            this.FødselsÅrTB.Size = new System.Drawing.Size(274, 26);
            this.FødselsÅrTB.TabIndex = 2;
            this.FødselsÅrTB.TextChanged += new System.EventHandler(this.FødselsÅrTB_TextChanged);
            // 
            // EpostTB
            // 
            this.EpostTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EpostTB.Location = new System.Drawing.Point(98, 109);
            this.EpostTB.Name = "EpostTB";
            this.EpostTB.Size = new System.Drawing.Size(274, 26);
            this.EpostTB.TabIndex = 3;
            // 
            // GuttRB
            // 
            this.GuttRB.AutoSize = true;
            this.GuttRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GuttRB.Location = new System.Drawing.Point(6, 25);
            this.GuttRB.Name = "GuttRB";
            this.GuttRB.Size = new System.Drawing.Size(59, 24);
            this.GuttRB.TabIndex = 4;
            this.GuttRB.TabStop = true;
            this.GuttRB.Text = "Gutt";
            this.GuttRB.UseVisualStyleBackColor = true;
            this.GuttRB.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // JenteRB
            // 
            this.JenteRB.AutoSize = true;
            this.JenteRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JenteRB.Location = new System.Drawing.Point(6, 55);
            this.JenteRB.Name = "JenteRB";
            this.JenteRB.Size = new System.Drawing.Size(67, 24);
            this.JenteRB.TabIndex = 5;
            this.JenteRB.TabStop = true;
            this.JenteRB.Text = "Jente";
            this.JenteRB.UseVisualStyleBackColor = true;
            this.JenteRB.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // BetaltCB
            // 
            this.BetaltCB.AutoSize = true;
            this.BetaltCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BetaltCB.Location = new System.Drawing.Point(425, 122);
            this.BetaltCB.Name = "BetaltCB";
            this.BetaltCB.Size = new System.Drawing.Size(70, 24);
            this.BetaltCB.TabIndex = 7;
            this.BetaltCB.Text = "Betalt";
            this.BetaltCB.UseVisualStyleBackColor = true;
            // 
            // FornavlLbl
            // 
            this.FornavlLbl.AutoSize = true;
            this.FornavlLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FornavlLbl.Location = new System.Drawing.Point(12, 15);
            this.FornavlLbl.Name = "FornavlLbl";
            this.FornavlLbl.Size = new System.Drawing.Size(67, 20);
            this.FornavlLbl.TabIndex = 8;
            this.FornavlLbl.Text = "Fornavn";
            // 
            // EtternavnLbl
            // 
            this.EtternavnLbl.AutoSize = true;
            this.EtternavnLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EtternavnLbl.Location = new System.Drawing.Point(12, 48);
            this.EtternavnLbl.Name = "EtternavnLbl";
            this.EtternavnLbl.Size = new System.Drawing.Size(78, 20);
            this.EtternavnLbl.TabIndex = 10;
            this.EtternavnLbl.Text = "Etternavn";
            // 
            // FødselsårLbl
            // 
            this.FødselsårLbl.AutoSize = true;
            this.FødselsårLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FødselsårLbl.Location = new System.Drawing.Point(12, 80);
            this.FødselsårLbl.Name = "FødselsårLbl";
            this.FødselsårLbl.Size = new System.Drawing.Size(79, 20);
            this.FødselsårLbl.TabIndex = 11;
            this.FødselsårLbl.Text = "Fødselsår";
            // 
            // EPostLbl
            // 
            this.EPostLbl.AutoSize = true;
            this.EPostLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EPostLbl.Location = new System.Drawing.Point(14, 112);
            this.EPostLbl.Name = "EPostLbl";
            this.EPostLbl.Size = new System.Drawing.Size(51, 20);
            this.EPostLbl.TabIndex = 12;
            this.EPostLbl.Text = "Epost";
            // 
            // StartNummerLbl
            // 
            this.StartNummerLbl.AutoSize = true;
            this.StartNummerLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartNummerLbl.Location = new System.Drawing.Point(8, 178);
            this.StartNummerLbl.Name = "StartNummerLbl";
            this.StartNummerLbl.Size = new System.Drawing.Size(102, 20);
            this.StartNummerLbl.TabIndex = 13;
            this.StartNummerLbl.Text = "Startnummer";
            // 
            // KjønnGB
            // 
            this.KjønnGB.Controls.Add(this.GuttRB);
            this.KjønnGB.Controls.Add(this.JenteRB);
            this.KjønnGB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KjønnGB.Location = new System.Drawing.Point(419, 12);
            this.KjønnGB.Name = "KjønnGB";
            this.KjønnGB.Size = new System.Drawing.Size(85, 91);
            this.KjønnGB.TabIndex = 15;
            this.KjønnGB.TabStop = false;
            this.KjønnGB.Text = "Kjønn";
            // 
            // OppdaterBtn
            // 
            this.OppdaterBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OppdaterBtn.Location = new System.Drawing.Point(148, 225);
            this.OppdaterBtn.Name = "OppdaterBtn";
            this.OppdaterBtn.Size = new System.Drawing.Size(100, 50);
            this.OppdaterBtn.TabIndex = 17;
            this.OppdaterBtn.Text = "Oppdater";
            this.OppdaterBtn.UseVisualStyleBackColor = true;
            this.OppdaterBtn.Click += new System.EventHandler(this.OppdaterBtn_Click);
            // 
            // HentBtn
            // 
            this.HentBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HentBtn.Location = new System.Drawing.Point(12, 225);
            this.HentBtn.Name = "HentBtn";
            this.HentBtn.Size = new System.Drawing.Size(100, 50);
            this.HentBtn.TabIndex = 18;
            this.HentBtn.Text = "Hent";
            this.HentBtn.UseVisualStyleBackColor = true;
            this.HentBtn.Click += new System.EventHandler(this.HentBtn_Click);
            // 
            // KlasseLbl
            // 
            this.KlasseLbl.AutoSize = true;
            this.KlasseLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KlasseLbl.Location = new System.Drawing.Point(14, 144);
            this.KlasseLbl.Name = "KlasseLbl";
            this.KlasseLbl.Size = new System.Drawing.Size(56, 20);
            this.KlasseLbl.TabIndex = 20;
            this.KlasseLbl.Text = "Klasse";
            // 
            // StartnummerTB
            // 
            this.StartnummerTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartnummerTB.Location = new System.Drawing.Point(148, 175);
            this.StartnummerTB.MaxLength = 3;
            this.StartnummerTB.Name = "StartnummerTB";
            this.StartnummerTB.Size = new System.Drawing.Size(224, 26);
            this.StartnummerTB.TabIndex = 19;
            // 
            // KlasseCB
            // 
            this.KlasseCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KlasseCB.FormattingEnabled = true;
            this.KlasseCB.Items.AddRange(new object[] {
            "Gutter/jenter 0-7 år",
            "Jenter 8 år",
            "Gutter 8 år",
            "Jenter 9 år",
            "Gutter 9 år",
            "Jenter 10 år",
            "Gutter 10 år",
            "Jenter 11 år",
            "Gutter 11 år",
            "Jenter 12 år",
            "Gutter 12 år",
            "Jenter 13 år",
            "Gutter 13 år",
            "Jenter 14 år",
            "Gutter 14 år",
            "Jenter 15 år",
            "Gutter 15 år",
            "Jenter 16 år",
            "Gutter 16 år",
            "Jenter 17+ år",
            "Gutter 17+ år"});
            this.KlasseCB.Location = new System.Drawing.Point(98, 141);
            this.KlasseCB.Name = "KlasseCB";
            this.KlasseCB.Size = new System.Drawing.Size(274, 28);
            this.KlasseCB.TabIndex = 21;
            this.KlasseCB.SelectedIndexChanged += new System.EventHandler(this.KlasseCB_SelectedIndexChanged);
            // 
            // PremieCB
            // 
            this.PremieCB.AutoSize = true;
            this.PremieCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PremieCB.Location = new System.Drawing.Point(425, 152);
            this.PremieCB.Name = "PremieCB";
            this.PremieCB.Size = new System.Drawing.Size(77, 24);
            this.PremieCB.TabIndex = 22;
            this.PremieCB.Text = "Premie";
            this.PremieCB.UseVisualStyleBackColor = true;
            // 
            // BrukerStatus
            // 
            this.BrukerStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BrukerStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BrukerTB});
            this.BrukerStatus.Location = new System.Drawing.Point(0, 291);
            this.BrukerStatus.Name = "BrukerStatus";
            this.BrukerStatus.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.BrukerStatus.Size = new System.Drawing.Size(537, 25);
            this.BrukerStatus.TabIndex = 23;
            this.BrukerStatus.Text = "toolStrip1";
            // 
            // BrukerTB
            // 
            this.BrukerTB.Name = "BrukerTB";
            this.BrukerTB.Size = new System.Drawing.Size(450, 25);
            // 
            // SlettBtn
            // 
            this.SlettBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SlettBtn.Location = new System.Drawing.Point(272, 225);
            this.SlettBtn.Name = "SlettBtn";
            this.SlettBtn.Size = new System.Drawing.Size(100, 50);
            this.SlettBtn.TabIndex = 24;
            this.SlettBtn.Text = "Slett";
            this.SlettBtn.UseVisualStyleBackColor = true;
            this.SlettBtn.Click += new System.EventHandler(this.SlettBtn_Click);
            // 
            // DeltagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(537, 316);
            this.Controls.Add(this.SlettBtn);
            this.Controls.Add(this.BrukerStatus);
            this.Controls.Add(this.PremieCB);
            this.Controls.Add(this.KlasseCB);
            this.Controls.Add(this.KlasseLbl);
            this.Controls.Add(this.StartnummerTB);
            this.Controls.Add(this.HentBtn);
            this.Controls.Add(this.OppdaterBtn);
            this.Controls.Add(this.KjønnGB);
            this.Controls.Add(this.StartNummerLbl);
            this.Controls.Add(this.EPostLbl);
            this.Controls.Add(this.FødselsårLbl);
            this.Controls.Add(this.EtternavnLbl);
            this.Controls.Add(this.FornavlLbl);
            this.Controls.Add(this.BetaltCB);
            this.Controls.Add(this.EpostTB);
            this.Controls.Add(this.FødselsÅrTB);
            this.Controls.Add(this.EtternavnTB);
            this.Controls.Add(this.FornavnTB);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DeltagerForm";
            this.Text = "Deltagerinfo";
            this.KjønnGB.ResumeLayout(false);
            this.KjønnGB.PerformLayout();
            this.BrukerStatus.ResumeLayout(false);
            this.BrukerStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.CheckBox BetaltCB;
        private System.Windows.Forms.Label FornavlLbl;
        private System.Windows.Forms.Label EtternavnLbl;
        private System.Windows.Forms.Label FødselsårLbl;
        private System.Windows.Forms.Label EPostLbl;
        private System.Windows.Forms.Label StartNummerLbl;
        private System.Windows.Forms.Button OppdaterBtn;
        private System.Windows.Forms.Button HentBtn;
        public System.Windows.Forms.TextBox FornavnTB;
        public System.Windows.Forms.TextBox EtternavnTB;
        public System.Windows.Forms.TextBox FødselsÅrTB;
        public System.Windows.Forms.TextBox EpostTB;
        private System.Windows.Forms.Label KlasseLbl;
        public System.Windows.Forms.TextBox StartnummerTB;
        public System.Windows.Forms.RadioButton GuttRB;
        public System.Windows.Forms.RadioButton JenteRB;
        public System.Windows.Forms.GroupBox KjønnGB;
        public System.Windows.Forms.ComboBox KlasseCB;
        public System.Windows.Forms.CheckBox PremieCB;
        private System.Windows.Forms.ToolStrip BrukerStatus;
        private System.Windows.Forms.ToolStripTextBox BrukerTB;
        private System.Windows.Forms.Button SlettBtn;
    }
}

