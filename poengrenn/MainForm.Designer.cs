namespace RennApplication
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.filToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.lagreSomToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.avsluttToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redigerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deltagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startnummerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.poengrennToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resultatToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deltagereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.genererToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.totaltForSerienToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.betaltToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startnummereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.åretsDeltagereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.åretsDeltagerealderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hjelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.DBSS = new System.Windows.Forms.StatusStrip();
            this.DBStatusLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.DBValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.ÅrLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.ÅrValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.RennLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.RennValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.DBSS.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filToolStripMenuItem,
            this.redigerToolStripMenuItem,
            this.resultatToolStripMenuItem1,
            this.hjelpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(710, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // filToolStripMenuItem
            // 
            this.filToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.databaseToolStripMenuItem,
            this.toolStripSeparator2,
            this.lagreSomToolStripMenuItem1,
            this.toolStripSeparator1,
            this.avsluttToolStripMenuItem});
            this.filToolStripMenuItem.Name = "filToolStripMenuItem";
            this.filToolStripMenuItem.Size = new System.Drawing.Size(31, 20);
            this.filToolStripMenuItem.Text = "Fil";
            // 
            // databaseToolStripMenuItem
            // 
            this.databaseToolStripMenuItem.Name = "databaseToolStripMenuItem";
            this.databaseToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.databaseToolStripMenuItem.Text = "Database...";
            this.databaseToolStripMenuItem.Click += new System.EventHandler(this.databaseToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(135, 6);
            // 
            // lagreSomToolStripMenuItem1
            // 
            this.lagreSomToolStripMenuItem1.Name = "lagreSomToolStripMenuItem1";
            this.lagreSomToolStripMenuItem1.Size = new System.Drawing.Size(138, 22);
            this.lagreSomToolStripMenuItem1.Text = "Lagre som...";
            this.lagreSomToolStripMenuItem1.Click += new System.EventHandler(this.lagreSomToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(135, 6);
            // 
            // avsluttToolStripMenuItem
            // 
            this.avsluttToolStripMenuItem.Name = "avsluttToolStripMenuItem";
            this.avsluttToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.avsluttToolStripMenuItem.Text = "Avslutt";
            this.avsluttToolStripMenuItem.Click += new System.EventHandler(this.avsluttToolStripMenuItem_Click);
            // 
            // redigerToolStripMenuItem
            // 
            this.redigerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.poengrennToolStripMenuItem,
            this.startnummerToolStripMenuItem,
            this.deltagerToolStripMenuItem,
            this.tidToolStripMenuItem});
            this.redigerToolStripMenuItem.Name = "redigerToolStripMenuItem";
            this.redigerToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.redigerToolStripMenuItem.Text = "Rediger";
            // 
            // deltagerToolStripMenuItem
            // 
            this.deltagerToolStripMenuItem.Name = "deltagerToolStripMenuItem";
            this.deltagerToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.deltagerToolStripMenuItem.Text = "Deltager...";
            this.deltagerToolStripMenuItem.Click += new System.EventHandler(this.deltagerToolStripMenuItem_Click);
            // 
            // startnummerToolStripMenuItem
            // 
            this.startnummerToolStripMenuItem.Name = "startnummerToolStripMenuItem";
            this.startnummerToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.startnummerToolStripMenuItem.Text = "Startnummer...";
            this.startnummerToolStripMenuItem.Click += new System.EventHandler(this.startnummerToolStripMenuItem_Click);
            // 
            // poengrennToolStripMenuItem
            // 
            this.poengrennToolStripMenuItem.Name = "poengrennToolStripMenuItem";
            this.poengrennToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.poengrennToolStripMenuItem.Text = "Renn...";
            this.poengrennToolStripMenuItem.Click += new System.EventHandler(this.rennToolStripMenuItem_Click);
            // 
            // tidToolStripMenuItem
            // 
            this.tidToolStripMenuItem.Name = "tidToolStripMenuItem";
            this.tidToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.tidToolStripMenuItem.Text = "Tid...";
            this.tidToolStripMenuItem.Click += new System.EventHandler(this.tidToolStripMenuItem_Click);
            // 
            // resultatToolStripMenuItem1
            // 
            this.resultatToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.åretsDeltagereToolStripMenuItem,
            this.åretsDeltagerealderToolStripMenuItem,
            this.startnummereToolStripMenuItem,
            this.deltagereToolStripMenuItem,
            this.genererToolStripMenuItem,
            this.betaltToolStripMenuItem,
            this.totaltForSerienToolStripMenuItem});
            this.resultatToolStripMenuItem1.Name = "resultatToolStripMenuItem1";
            this.resultatToolStripMenuItem1.Size = new System.Drawing.Size(47, 20);
            this.resultatToolStripMenuItem1.Text = "Lister";
            // 
            // deltagereToolStripMenuItem
            // 
            this.deltagereToolStripMenuItem.Name = "deltagereToolStripMenuItem";
            this.deltagereToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.deltagereToolStripMenuItem.Text = "Deltagere";
            this.deltagereToolStripMenuItem.Click += new System.EventHandler(this.deltagereToolStripMenuItem_Click);
            // 
            // genererToolStripMenuItem
            // 
            this.genererToolStripMenuItem.Name = "genererToolStripMenuItem";
            this.genererToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.genererToolStripMenuItem.Text = "Dette rennet";
            this.genererToolStripMenuItem.Click += new System.EventHandler(this.detteRennetToolStripMenuItem_Click);
            // 
            // totaltForSerienToolStripMenuItem
            // 
            this.totaltForSerienToolStripMenuItem.Name = "totaltForSerienToolStripMenuItem";
            this.totaltForSerienToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.totaltForSerienToolStripMenuItem.Text = "Årets poengrenn";
            this.totaltForSerienToolStripMenuItem.Click += new System.EventHandler(this.totaltForSerienToolStripMenuItem_Click);
            // 
            // betaltToolStripMenuItem
            // 
            this.betaltToolStripMenuItem.Name = "betaltToolStripMenuItem";
            this.betaltToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.betaltToolStripMenuItem.Text = "Betalt";
            this.betaltToolStripMenuItem.Click += new System.EventHandler(this.betaltToolStripMenuItem_Click);
            // 
            // startnummereToolStripMenuItem
            // 
            this.startnummereToolStripMenuItem.Name = "startnummereToolStripMenuItem";
            this.startnummereToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.startnummereToolStripMenuItem.Text = "Startnummere";
            this.startnummereToolStripMenuItem.Click += new System.EventHandler(this.startnummereToolStripMenuItem_Click);
            // 
            // åretsDeltagereToolStripMenuItem
            // 
            this.åretsDeltagereToolStripMenuItem.Name = "åretsDeltagereToolStripMenuItem";
            this.åretsDeltagereToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.åretsDeltagereToolStripMenuItem.Text = "Årets deltagere";
            this.åretsDeltagereToolStripMenuItem.Click += new System.EventHandler(this.åretsDeltagereToolStripMenuItem_Click);
            // 
            // åretsDeltagerealderToolStripMenuItem
            // 
            this.åretsDeltagerealderToolStripMenuItem.Name = "åretsDeltagerealderToolStripMenuItem";
            this.åretsDeltagerealderToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.åretsDeltagerealderToolStripMenuItem.Text = "Fjorårets deltagere";
            this.åretsDeltagerealderToolStripMenuItem.Click += new System.EventHandler(this.fjoråretsDeltagereToolStripMenuItem_Click);
            // 
            // hjelpToolStripMenuItem
            // 
            this.hjelpToolStripMenuItem.Name = "hjelpToolStripMenuItem";
            this.hjelpToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.hjelpToolStripMenuItem.Text = "Hjelp";
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 24);
            this.webBrowser.Margin = new System.Windows.Forms.Padding(2);
            this.webBrowser.MinimumSize = new System.Drawing.Size(15, 16);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(710, 436);
            this.webBrowser.TabIndex = 1;
            // 
            // DBSS
            // 
            this.DBSS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DBStatusLbl,
            this.DBValue,
            this.ÅrLbl,
            this.ÅrValue,
            this.RennLbl,
            this.RennValue});
            this.DBSS.Location = new System.Drawing.Point(0, 438);
            this.DBSS.Name = "DBSS";
            this.DBSS.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.DBSS.Size = new System.Drawing.Size(710, 22);
            this.DBSS.TabIndex = 2;
            this.DBSS.Text = "Database";
            // 
            // DBStatusLbl
            // 
            this.DBStatusLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DBStatusLbl.Name = "DBStatusLbl";
            this.DBStatusLbl.Size = new System.Drawing.Size(64, 17);
            this.DBStatusLbl.Text = "Database: ";
            // 
            // DBValue
            // 
            this.DBValue.Name = "DBValue";
            this.DBValue.Size = new System.Drawing.Size(0, 17);
            // 
            // ÅrLbl
            // 
            this.ÅrLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ÅrLbl.Name = "ÅrLbl";
            this.ÅrLbl.Size = new System.Drawing.Size(26, 17);
            this.ÅrLbl.Text = "År: ";
            // 
            // ÅrValue
            // 
            this.ÅrValue.Name = "ÅrValue";
            this.ÅrValue.Size = new System.Drawing.Size(0, 17);
            // 
            // RennLbl
            // 
            this.RennLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RennLbl.Name = "RennLbl";
            this.RennLbl.Size = new System.Drawing.Size(42, 17);
            this.RennLbl.Text = "Renn: ";
            // 
            // RennValue
            // 
            this.RennValue.Name = "RennValue";
            this.RennValue.Size = new System.Drawing.Size(0, 17);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 460);
            this.Controls.Add(this.DBSS);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "Flatås IL langrenn";
            this.Shown += new System.EventHandler(this.Main_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.DBSS.ResumeLayout(false);
            this.DBSS.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem redigerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem poengrennToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deltagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hjelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startnummerToolStripMenuItem;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.ToolStripMenuItem resultatToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem genererToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tidToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem totaltForSerienToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deltagereToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lagreSomToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem avsluttToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem databaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.StatusStrip DBSS;
        private System.Windows.Forms.ToolStripStatusLabel DBStatusLbl;
        private System.Windows.Forms.ToolStripStatusLabel DBValue;
        private System.Windows.Forms.ToolStripStatusLabel ÅrLbl;
        private System.Windows.Forms.ToolStripStatusLabel RennLbl;
        protected internal System.Windows.Forms.ToolStripStatusLabel RennValue;
        protected internal System.Windows.Forms.ToolStripStatusLabel ÅrValue;
        private System.Windows.Forms.ToolStripMenuItem betaltToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startnummereToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem åretsDeltagereToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem åretsDeltagerealderToolStripMenuItem;
    }
}