namespace RennApplication
{
    partial class RennForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RennForm));
            this.ÅrLbl = new System.Windows.Forms.Label();
            this.ÅrTB = new System.Windows.Forms.TextBox();
            this.PoengrennLbl = new System.Windows.Forms.Label();
            this.PoengrennTB = new System.Windows.Forms.TextBox();
            this.LagreBtn = new System.Windows.Forms.Button();
            this.StyleGB = new System.Windows.Forms.GroupBox();
            this.SkicrossRB = new System.Windows.Forms.RadioButton();
            this.FristilRB = new System.Windows.Forms.RadioButton();
            this.KlassiskRB = new System.Windows.Forms.RadioButton();
            this.VelgBtn = new System.Windows.Forms.Button();
            this.RenntypeGB = new System.Windows.Forms.GroupBox();
            this.KlubbmesterskapRB = new System.Windows.Forms.RadioButton();
            this.PoengrennRB = new System.Windows.Forms.RadioButton();
            this.IntervalGB = new System.Windows.Forms.GroupBox();
            this.RB30 = new System.Windows.Forms.RadioButton();
            this.RB15 = new System.Windows.Forms.RadioButton();
            this.StyleGB.SuspendLayout();
            this.RenntypeGB.SuspendLayout();
            this.IntervalGB.SuspendLayout();
            this.SuspendLayout();
            // 
            // ÅrLbl
            // 
            this.ÅrLbl.AutoSize = true;
            this.ÅrLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ÅrLbl.Location = new System.Drawing.Point(14, 128);
            this.ÅrLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ÅrLbl.Name = "ÅrLbl";
            this.ÅrLbl.Size = new System.Drawing.Size(25, 20);
            this.ÅrLbl.TabIndex = 16;
            this.ÅrLbl.Text = "År";
            // 
            // ÅrTB
            // 
            this.ÅrTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ÅrTB.Location = new System.Drawing.Point(83, 125);
            this.ÅrTB.Margin = new System.Windows.Forms.Padding(2);
            this.ÅrTB.MaxLength = 4;
            this.ÅrTB.Name = "ÅrTB";
            this.ÅrTB.Size = new System.Drawing.Size(62, 26);
            this.ÅrTB.TabIndex = 15;
            // 
            // PoengrennLbl
            // 
            this.PoengrennLbl.AutoSize = true;
            this.PoengrennLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PoengrennLbl.Location = new System.Drawing.Point(14, 160);
            this.PoengrennLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PoengrennLbl.Name = "PoengrennLbl";
            this.PoengrennLbl.Size = new System.Drawing.Size(66, 20);
            this.PoengrennLbl.TabIndex = 18;
            this.PoengrennLbl.Text = "Renn nr";
            // 
            // PoengrennTB
            // 
            this.PoengrennTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PoengrennTB.Location = new System.Drawing.Point(83, 157);
            this.PoengrennTB.Margin = new System.Windows.Forms.Padding(2);
            this.PoengrennTB.MaxLength = 1;
            this.PoengrennTB.Name = "PoengrennTB";
            this.PoengrennTB.Size = new System.Drawing.Size(62, 26);
            this.PoengrennTB.TabIndex = 17;
            // 
            // LagreBtn
            // 
            this.LagreBtn.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.LagreBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LagreBtn.Location = new System.Drawing.Point(210, 214);
            this.LagreBtn.Margin = new System.Windows.Forms.Padding(2);
            this.LagreBtn.Name = "LagreBtn";
            this.LagreBtn.Size = new System.Drawing.Size(117, 46);
            this.LagreBtn.TabIndex = 22;
            this.LagreBtn.Text = "Lagre";
            this.LagreBtn.UseVisualStyleBackColor = false;
            this.LagreBtn.Click += new System.EventHandler(this.LagreBtn_Click);
            // 
            // StyleGB
            // 
            this.StyleGB.Controls.Add(this.SkicrossRB);
            this.StyleGB.Controls.Add(this.FristilRB);
            this.StyleGB.Controls.Add(this.KlassiskRB);
            this.StyleGB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StyleGB.Location = new System.Drawing.Point(210, 10);
            this.StyleGB.Margin = new System.Windows.Forms.Padding(2);
            this.StyleGB.Name = "StyleGB";
            this.StyleGB.Padding = new System.Windows.Forms.Padding(2);
            this.StyleGB.Size = new System.Drawing.Size(117, 111);
            this.StyleGB.TabIndex = 23;
            this.StyleGB.TabStop = false;
            this.StyleGB.Text = "Stilart";
            // 
            // SkicrossRB
            // 
            this.SkicrossRB.AutoSize = true;
            this.SkicrossRB.Location = new System.Drawing.Point(11, 79);
            this.SkicrossRB.Margin = new System.Windows.Forms.Padding(2);
            this.SkicrossRB.Name = "SkicrossRB";
            this.SkicrossRB.Size = new System.Drawing.Size(87, 24);
            this.SkicrossRB.TabIndex = 2;
            this.SkicrossRB.TabStop = true;
            this.SkicrossRB.Text = "Skicross";
            this.SkicrossRB.UseVisualStyleBackColor = true;
            this.SkicrossRB.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // FristilRB
            // 
            this.FristilRB.AutoSize = true;
            this.FristilRB.Location = new System.Drawing.Point(11, 51);
            this.FristilRB.Margin = new System.Windows.Forms.Padding(2);
            this.FristilRB.Name = "FristilRB";
            this.FristilRB.Size = new System.Drawing.Size(64, 24);
            this.FristilRB.TabIndex = 1;
            this.FristilRB.TabStop = true;
            this.FristilRB.Text = "Fristil";
            this.FristilRB.UseVisualStyleBackColor = true;
            this.FristilRB.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // KlassiskRB
            // 
            this.KlassiskRB.AutoSize = true;
            this.KlassiskRB.Location = new System.Drawing.Point(11, 23);
            this.KlassiskRB.Margin = new System.Windows.Forms.Padding(2);
            this.KlassiskRB.Name = "KlassiskRB";
            this.KlassiskRB.Size = new System.Drawing.Size(84, 24);
            this.KlassiskRB.TabIndex = 0;
            this.KlassiskRB.TabStop = true;
            this.KlassiskRB.Text = "Klassisk";
            this.KlassiskRB.UseVisualStyleBackColor = true;
            this.KlassiskRB.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // VelgBtn
            // 
            this.VelgBtn.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.VelgBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VelgBtn.Location = new System.Drawing.Point(48, 214);
            this.VelgBtn.Margin = new System.Windows.Forms.Padding(2);
            this.VelgBtn.Name = "VelgBtn";
            this.VelgBtn.Size = new System.Drawing.Size(117, 46);
            this.VelgBtn.TabIndex = 24;
            this.VelgBtn.Text = "Velg";
            this.VelgBtn.UseVisualStyleBackColor = false;
            this.VelgBtn.Click += new System.EventHandler(this.velgBtn_Click);
            // 
            // RenntypeGB
            // 
            this.RenntypeGB.Controls.Add(this.KlubbmesterskapRB);
            this.RenntypeGB.Controls.Add(this.PoengrennRB);
            this.RenntypeGB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RenntypeGB.Location = new System.Drawing.Point(9, 10);
            this.RenntypeGB.Margin = new System.Windows.Forms.Padding(2);
            this.RenntypeGB.Name = "RenntypeGB";
            this.RenntypeGB.Padding = new System.Windows.Forms.Padding(2);
            this.RenntypeGB.Size = new System.Drawing.Size(173, 88);
            this.RenntypeGB.TabIndex = 24;
            this.RenntypeGB.TabStop = false;
            this.RenntypeGB.Text = "Renntype";
            // 
            // KlubbmesterskapRB
            // 
            this.KlubbmesterskapRB.AutoSize = true;
            this.KlubbmesterskapRB.Location = new System.Drawing.Point(9, 51);
            this.KlubbmesterskapRB.Margin = new System.Windows.Forms.Padding(2);
            this.KlubbmesterskapRB.Name = "KlubbmesterskapRB";
            this.KlubbmesterskapRB.Size = new System.Drawing.Size(150, 24);
            this.KlubbmesterskapRB.TabIndex = 1;
            this.KlubbmesterskapRB.TabStop = true;
            this.KlubbmesterskapRB.Text = "Klubbmesterskap";
            this.KlubbmesterskapRB.UseVisualStyleBackColor = true;
            this.KlubbmesterskapRB.CheckedChanged += new System.EventHandler(this.KlubbmesterskapRB_CheckedChanged);
            // 
            // PoengrennRB
            // 
            this.PoengrennRB.AutoSize = true;
            this.PoengrennRB.Location = new System.Drawing.Point(9, 23);
            this.PoengrennRB.Margin = new System.Windows.Forms.Padding(2);
            this.PoengrennRB.Name = "PoengrennRB";
            this.PoengrennRB.Size = new System.Drawing.Size(105, 24);
            this.PoengrennRB.TabIndex = 0;
            this.PoengrennRB.TabStop = true;
            this.PoengrennRB.Text = "Poengrenn";
            this.PoengrennRB.UseVisualStyleBackColor = true;
            // 
            // IntervalGB
            // 
            this.IntervalGB.Controls.Add(this.RB30);
            this.IntervalGB.Controls.Add(this.RB15);
            this.IntervalGB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IntervalGB.Location = new System.Drawing.Point(210, 125);
            this.IntervalGB.Margin = new System.Windows.Forms.Padding(2);
            this.IntervalGB.Name = "IntervalGB";
            this.IntervalGB.Padding = new System.Windows.Forms.Padding(2);
            this.IntervalGB.Size = new System.Drawing.Size(117, 84);
            this.IntervalGB.TabIndex = 25;
            this.IntervalGB.TabStop = false;
            this.IntervalGB.Text = "Startintervall";
            // 
            // RB30
            // 
            this.RB30.AutoSize = true;
            this.RB30.Location = new System.Drawing.Point(11, 51);
            this.RB30.Margin = new System.Windows.Forms.Padding(2);
            this.RB30.Name = "RB30";
            this.RB30.Size = new System.Drawing.Size(57, 24);
            this.RB30.TabIndex = 1;
            this.RB30.TabStop = true;
            this.RB30.Text = "30 s";
            this.RB30.UseVisualStyleBackColor = true;
            // 
            // RB15
            // 
            this.RB15.AutoSize = true;
            this.RB15.Location = new System.Drawing.Point(11, 23);
            this.RB15.Margin = new System.Windows.Forms.Padding(2);
            this.RB15.Name = "RB15";
            this.RB15.Size = new System.Drawing.Size(57, 24);
            this.RB15.TabIndex = 0;
            this.RB15.TabStop = true;
            this.RB15.Text = "15 s";
            this.RB15.UseVisualStyleBackColor = true;
            // 
            // RennForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(398, 268);
            this.Controls.Add(this.IntervalGB);
            this.Controls.Add(this.RenntypeGB);
            this.Controls.Add(this.VelgBtn);
            this.Controls.Add(this.StyleGB);
            this.Controls.Add(this.LagreBtn);
            this.Controls.Add(this.PoengrennLbl);
            this.Controls.Add(this.PoengrennTB);
            this.Controls.Add(this.ÅrLbl);
            this.Controls.Add(this.ÅrTB);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "RennForm";
            this.Text = "Renninfo";
            this.StyleGB.ResumeLayout(false);
            this.StyleGB.PerformLayout();
            this.RenntypeGB.ResumeLayout(false);
            this.RenntypeGB.PerformLayout();
            this.IntervalGB.ResumeLayout(false);
            this.IntervalGB.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ÅrLbl;
        private System.Windows.Forms.TextBox ÅrTB;
        private System.Windows.Forms.Label PoengrennLbl;
        private System.Windows.Forms.TextBox PoengrennTB;
        private System.Windows.Forms.Button LagreBtn;
        private System.Windows.Forms.GroupBox StyleGB;
        private System.Windows.Forms.RadioButton SkicrossRB;
        private System.Windows.Forms.RadioButton FristilRB;
        private System.Windows.Forms.RadioButton KlassiskRB;
        private System.Windows.Forms.Button VelgBtn;
        private System.Windows.Forms.GroupBox RenntypeGB;
        private System.Windows.Forms.RadioButton KlubbmesterskapRB;
        private System.Windows.Forms.RadioButton PoengrennRB;
        private System.Windows.Forms.GroupBox IntervalGB;
        private System.Windows.Forms.RadioButton RB30;
        private System.Windows.Forms.RadioButton RB15;
    }
}