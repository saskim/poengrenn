namespace RennApplication
{
    partial class RennTid
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RennTid));
            this.StartnummerLbl = new System.Windows.Forms.Label();
            this.StartNummerTB = new System.Windows.Forms.TextBox();
            this.StartLbl = new System.Windows.Forms.Label();
            this.StartTB = new System.Windows.Forms.TextBox();
            this.TidLbl = new System.Windows.Forms.Label();
            this.TidTB = new System.Windows.Forms.TextBox();
            this.SluttLbl = new System.Windows.Forms.Label();
            this.SluttTB = new System.Windows.Forms.TextBox();
            this.LeggInnBtn = new System.Windows.Forms.Button();
            this.StatusLbl = new System.Windows.Forms.ToolStrip();
            this.StatusTB = new System.Windows.Forms.ToolStripTextBox();
            this.Add30Sec = new System.Windows.Forms.Button();
            this.Add15Sec = new System.Windows.Forms.Button();
            this.StatusLbl.SuspendLayout();
            this.SuspendLayout();
            // 
            // StartnummerLbl
            // 
            this.StartnummerLbl.AutoSize = true;
            this.StartnummerLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartnummerLbl.Location = new System.Drawing.Point(8, 7);
            this.StartnummerLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.StartnummerLbl.Name = "StartnummerLbl";
            this.StartnummerLbl.Size = new System.Drawing.Size(102, 20);
            this.StartnummerLbl.TabIndex = 14;
            this.StartnummerLbl.Text = "Startnummer";
            // 
            // StartNummerTB
            // 
            this.StartNummerTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartNummerTB.Location = new System.Drawing.Point(115, 4);
            this.StartNummerTB.Margin = new System.Windows.Forms.Padding(2);
            this.StartNummerTB.MaxLength = 3;
            this.StartNummerTB.Name = "StartNummerTB";
            this.StartNummerTB.Size = new System.Drawing.Size(151, 26);
            this.StartNummerTB.TabIndex = 13;
            this.StartNummerTB.TextChanged += new System.EventHandler(this.hentTid);
            // 
            // StartLbl
            // 
            this.StartLbl.AutoSize = true;
            this.StartLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartLbl.Location = new System.Drawing.Point(8, 37);
            this.StartLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.StartLbl.Name = "StartLbl";
            this.StartLbl.Size = new System.Drawing.Size(102, 20);
            this.StartLbl.TabIndex = 16;
            this.StartLbl.Text = "Start [t:min:s]";
            // 
            // StartTB
            // 
            this.StartTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartTB.Location = new System.Drawing.Point(115, 34);
            this.StartTB.Margin = new System.Windows.Forms.Padding(2);
            this.StartTB.Name = "StartTB";
            this.StartTB.Size = new System.Drawing.Size(151, 26);
            this.StartTB.TabIndex = 15;
            this.StartTB.TextChanged += new System.EventHandler(this.timeChanged);
            // 
            // TidLbl
            // 
            this.TidLbl.AutoSize = true;
            this.TidLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TidLbl.Location = new System.Drawing.Point(11, 97);
            this.TidLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TidLbl.Name = "TidLbl";
            this.TidLbl.Size = new System.Drawing.Size(30, 20);
            this.TidLbl.TabIndex = 18;
            this.TidLbl.Text = "Tid";
            // 
            // TidTB
            // 
            this.TidTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TidTB.Location = new System.Drawing.Point(115, 94);
            this.TidTB.Margin = new System.Windows.Forms.Padding(2);
            this.TidTB.Name = "TidTB";
            this.TidTB.ReadOnly = true;
            this.TidTB.Size = new System.Drawing.Size(151, 26);
            this.TidTB.TabIndex = 17;
            // 
            // SluttLbl
            // 
            this.SluttLbl.AutoSize = true;
            this.SluttLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SluttLbl.Location = new System.Drawing.Point(11, 67);
            this.SluttLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.SluttLbl.Name = "SluttLbl";
            this.SluttLbl.Size = new System.Drawing.Size(100, 20);
            this.SluttLbl.TabIndex = 20;
            this.SluttLbl.Text = "Slutt [t:min:s]";
            // 
            // SluttTB
            // 
            this.SluttTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SluttTB.Location = new System.Drawing.Point(115, 64);
            this.SluttTB.Margin = new System.Windows.Forms.Padding(2);
            this.SluttTB.Name = "SluttTB";
            this.SluttTB.Size = new System.Drawing.Size(151, 26);
            this.SluttTB.TabIndex = 19;
            this.SluttTB.TextChanged += new System.EventHandler(this.timeChanged);
            // 
            // LeggInnBtn
            // 
            this.LeggInnBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LeggInnBtn.Location = new System.Drawing.Point(278, 104);
            this.LeggInnBtn.Margin = new System.Windows.Forms.Padding(2);
            this.LeggInnBtn.Name = "LeggInnBtn";
            this.LeggInnBtn.Size = new System.Drawing.Size(117, 46);
            this.LeggInnBtn.TabIndex = 21;
            this.LeggInnBtn.Text = "Legg inn";
            this.LeggInnBtn.UseVisualStyleBackColor = true;
            this.LeggInnBtn.Click += new System.EventHandler(this.LeggInnBtn_Click);
            // 
            // StatusLbl
            // 
            this.StatusLbl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.StatusLbl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusTB});
            this.StatusLbl.Location = new System.Drawing.Point(0, 151);
            this.StatusLbl.Name = "StatusLbl";
            this.StatusLbl.Size = new System.Drawing.Size(406, 25);
            this.StatusLbl.TabIndex = 22;
            this.StatusLbl.Text = "Sist lagret";
            // 
            // StatusTB
            // 
            this.StatusTB.Name = "StatusTB";
            this.StatusTB.Size = new System.Drawing.Size(91, 25);
            // 
            // Add30Sec
            // 
            this.Add30Sec.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Add30Sec.Location = new System.Drawing.Point(278, 54);
            this.Add30Sec.Margin = new System.Windows.Forms.Padding(2);
            this.Add30Sec.Name = "Add30Sec";
            this.Add30Sec.Size = new System.Drawing.Size(117, 46);
            this.Add30Sec.TabIndex = 23;
            this.Add30Sec.Text = "+30 sek";
            this.Add30Sec.UseVisualStyleBackColor = true;
            this.Add30Sec.Click += new System.EventHandler(this.Add30Sec_Click);
            // 
            // Add15Sec
            // 
            this.Add15Sec.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Add15Sec.Location = new System.Drawing.Point(278, 4);
            this.Add15Sec.Margin = new System.Windows.Forms.Padding(2);
            this.Add15Sec.Name = "Add15Sec";
            this.Add15Sec.Size = new System.Drawing.Size(117, 46);
            this.Add15Sec.TabIndex = 24;
            this.Add15Sec.Text = "+15 sek";
            this.Add15Sec.UseVisualStyleBackColor = true;
            this.Add15Sec.Click += new System.EventHandler(this.Add15Sec_Click);
            // 
            // RennTid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(406, 176);
            this.Controls.Add(this.Add15Sec);
            this.Controls.Add(this.Add30Sec);
            this.Controls.Add(this.StatusLbl);
            this.Controls.Add(this.LeggInnBtn);
            this.Controls.Add(this.SluttLbl);
            this.Controls.Add(this.SluttTB);
            this.Controls.Add(this.TidLbl);
            this.Controls.Add(this.TidTB);
            this.Controls.Add(this.StartLbl);
            this.Controls.Add(this.StartTB);
            this.Controls.Add(this.StartnummerLbl);
            this.Controls.Add(this.StartNummerTB);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "RennTid";
            this.Text = "Renntid";
            this.StatusLbl.ResumeLayout(false);
            this.StatusLbl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label StartnummerLbl;
        private System.Windows.Forms.TextBox StartNummerTB;
        private System.Windows.Forms.Label StartLbl;
        private System.Windows.Forms.TextBox StartTB;
        private System.Windows.Forms.Label TidLbl;
        private System.Windows.Forms.TextBox TidTB;
        private System.Windows.Forms.Label SluttLbl;
        private System.Windows.Forms.TextBox SluttTB;
        private System.Windows.Forms.Button LeggInnBtn;
        private System.Windows.Forms.ToolStrip StatusLbl;
        private System.Windows.Forms.ToolStripTextBox StatusTB;
        private System.Windows.Forms.Button Add30Sec;
        private System.Windows.Forms.Button Add15Sec;
    }
}