using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace RennApplication
{
    public enum raceType { Poengrenn, Klubbmesterskap };
    
    public partial class MainForm : Form
    {
        public int currentYear;
        public int currentRace;
        public raceType currentRaceType;
        public TimeSpan prevStartTime = TimeSpan.FromSeconds(-30);
        public TimeSpan startInterval = TimeSpan.FromSeconds(15);

        public MainForm()
        {
            InitializeComponent();
        }

        private void rennToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RennForm myForm = new RennForm();
            myForm.Show();
            myForm.mainForm = this;
        }

        private void deltagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeltagerForm myForm = new DeltagerForm();
            myForm.Show();
            myForm.mainForm = this;
        }

        private void startnummerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartnummerForm myForm = new StartnummerForm();
            myForm.Show();
        }


        private void lagreSomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (webBrowser.DocumentText == "")
            {
                MessageBox.Show("Ingen tekst å lagre!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string fileName;
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Lagre fil som";
                saveFileDialog.Filter = "HTML dokument (*.html)|*.html";
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if ((fileName = saveFileDialog.FileName) != null)
                    {
                        StreamWriter writer = File.CreateText(fileName);
                        writer.Write(webBrowser.DocumentText);
                        writer.Flush();
                        writer.Close();
                    }
                }
            }
        }

        private void detteRennetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] genders = new string[] { "Jenter", "Gutter" };
            string group;
            string connectionString = Properties.Settings.Default.ConnectionString;
            string queryString;
            string txt = returnHTMLHeader();
            int withTime = 0;
            int withoutTime = 0;

            if (currentRaceType == raceType.Poengrenn)
            {
                txt += "<h1>Resultat fra poengrenn " + currentRace + ", " + currentYear + "</h1>\n</header>\n";

                // Check participants in 'Gutter/jenter 0-7 år'
                queryString = "SELECT PersonID FROM dbo.Resultat WHERE År = " + currentYear +
                    " AND RennNummer = " + currentRace +
                    " AND Klasse = 'Gutter/jenter 0-7 år';";
            }
            else
            {
                txt += "<h1>Resultat fra klubbmesterskap " + currentYear + "</h1>\n</header>\n";

                // Check participants in 'Gutter/jenter 0-7 år'
                queryString = "SELECT PersonID FROM dbo.KlubbmesterskapResultat WHERE År = " + currentYear +
                    " AND Klasse = 'Gutter/jenter 0-7 år';";
            }

            txt += "<ul>\n";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(queryString, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        txt += "<li><a href = \"#Gutter/jenter 0-7 år\">Gutter/jenter 0-7 år</a>\n";
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Feil i generering av lenker til resultatlistene\nFeilmelding: " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            for (int i = 8; i <= 17; i++)
            {
                foreach (string gender in genders)
                {
                    if (i == 17)
                    {
                        group = gender + " " + i + "+ år";
                    }
                    else
                    {
                        group = gender + " " + i + " år";
                    }
                    if (currentRaceType == raceType.Poengrenn)
                    {
                        queryString = "SELECT PersonID FROM dbo.Resultat WHERE År = " + currentYear +
                            " AND RennNummer = " + currentRace +
                            " AND Klasse = '" + group + "';";
                    }
                    else
                    {
                        queryString = "SELECT PersonID FROM dbo.KlubbmesterskapResultat WHERE År = " + currentYear +
                            " AND Klasse = '" + group + "';";
                    }
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand(queryString, connection);
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                txt += "<li><a href = \"#" + group + "\">" + group + "</a>\n";
                            }
                            reader.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Feil i generering av lenker til resultatlistene\nFeilmelding: " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

            txt += "</ul>\n";

            // Generate the results list for all groups for the current race
            // No ranking, no time for kids up to 7 years
            if (currentRaceType == raceType.Poengrenn)
            {
                queryString = "SELECT Fornavn, Etternavn FROM dbo.Resultat INNER JOIN dbo.PersonInfo ON dbo.Resultat.PersonID = dbo.PersonInfo.ID WHERE År = " + currentYear +
                    " AND RennNummer = " + currentRace +
                    " AND Klasse = 'Gutter/jenter 0-7 år' ORDER BY Fornavn, Etternavn;";
            }
            else
            {
                queryString = "SELECT Fornavn, Etternavn FROM dbo.KlubbmesterskapResultat INNER JOIN dbo.PersonInfo ON dbo.KlubbmesterskapResultat.PersonID = dbo.PersonInfo.ID WHERE År = " + currentYear +
                    " AND Klasse = 'Gutter/jenter 0-7 år' ORDER BY Fornavn, Etternavn;";
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        txt += "<h2 id = \"Gutter/jenter 0-7 år\">Gutter/jenter 0-7 år</h2>\n";
                        txt += "<p>Ingen tidtaking, sortering etter fornavn og etternavn</p>\n";
                        txt += "<table id=\"Deltagere\">\n<thead>\n<tr>\n<th>Navn</th>\n</tr>\n</thead>\n<tbody>\n";
                        while (reader.Read())
                        {
                            txt += "<tr>\n<td>" + reader.GetString(0) + " " + reader.GetString(1) + "</td>\n</tr>\n";
                            withoutTime++;
                        }
                        txt += "</tbody>\n</table>\n";
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Feil i generering av resultatlister!\nFeilmelding: " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // No ranking, but time shown for kids up to 10 years                
                for (int i = 8; i <= 10; i++)
                {
                    foreach (string gender in genders)
                    {
                        group = gender + " " + i + " år";
                        if (currentRaceType == raceType.Poengrenn)
                        {
                            queryString = "SELECT Fornavn, Etternavn, Tidsforbruk FROM dbo.Resultat INNER JOIN dbo.PersonInfo ON dbo.Resultat.PersonID = dbo.PersonInfo.ID WHERE År = " + currentYear +
                                " AND RennNummer = " + currentRace +
                                " AND Klasse = '" + group + "' ORDER BY Fornavn, Etternavn;";
                        }
                        else
                        {
                            queryString = "SELECT Fornavn, Etternavn, Tidsforbruk FROM dbo.KlubbmesterskapResultat INNER JOIN dbo.PersonInfo ON dbo.KlubbmesterskapResultat.PersonID = dbo.PersonInfo.ID WHERE År = " + currentYear +
                                " AND Klasse = '" + group + "' ORDER BY Fornavn, Etternavn;";
                        }
                        try
                        {
                            SqlCommand command = new SqlCommand(queryString, connection);
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                txt += "<h2 id = \"" + group + "\">" + group + "</h2>\n";
                                txt += "<p>Tidtaking, men ingen rangering. Sortert på fornavn og etternavn</p>\n";
                                txt += "<table id=\"" + gender + "\"><thead><tr><th>Navn</th><th>Tid</th></tr></thead><tbody>";
                                while (reader.Read())
                                {
                                    if (reader.IsDBNull(2))
                                    {
                                        MessageBox.Show("Mangler tid for " + reader.GetString(0) + " " + reader.GetString(1), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    else
                                    {
                                        txt += "<tr>\n<td>" + reader.GetString(0) + " " + reader.GetString(1) + "</td>\n<td>" + reader.GetTimeSpan(2) + "</td>\n</tr>\n";
                                        withTime++;
                                    }
                                }
                                txt += "</tbody>\n</table>\n";
                            }
                            reader.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Feil i generering av resultatlister!\nFeilmelding: " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                for (int i = 11; i <= 17; i++)
                {
                    foreach (string gender in genders)
                    {
                        if (i == 17)
                        {
                            group = gender + " " + i + "+ år";
                        }
                        else
                        {
                            group = gender + " " + i + " år";
                        }
                        if (currentRaceType == raceType.Poengrenn)
                        {
                            queryString = "SELECT Fornavn, Etternavn, Tidsforbruk, StartNummer FROM dbo.Resultat INNER JOIN dbo.PersonInfo ON dbo.Resultat.PersonID = dbo.PersonInfo.ID WHERE År = " + currentYear +
                                " AND RennNummer = " + currentRace +
                                " AND Klasse = '" + group + "' ORDER BY Tidsforbruk, Fornavn, Etternavn;";
                        }
                        else
                        {
                            queryString = "SELECT Fornavn, Etternavn, Tidsforbruk, StartNummer FROM dbo.KlubbmesterskapResultat INNER JOIN dbo.PersonInfo ON dbo.KlubbmesterskapResultat.PersonID = dbo.PersonInfo.ID WHERE År = " + currentYear +
                                " AND Klasse = '" + group + "' ORDER BY Tidsforbruk, Fornavn, Etternavn;";
                        }
                        try
                        {
                            SqlCommand command = new SqlCommand(queryString, connection);
                            SqlDataReader reader = command.ExecuteReader();
                            int points = 101;
                            int equalTime = 0;
                            TimeSpan prevTime = new TimeSpan(0);
                            TimeSpan currentTime;
                            int startNumber;
                            if (reader.HasRows)
                            {
                                txt += "<h2 id = \"" + group + "\">" + group + "</h2>\n";
                                txt += "<p>Rangering etter tid</p>\n";
                                txt += "<table id=\"" + gender + "\">\n<thead>\n<tr>\n<th>Navn</th>\n<th>Tid</th>\n</tr>\n</thead>\n<tbody>\n";
                                while (reader.Read())
                                {
                                    if (currentRaceType == raceType.Poengrenn)
                                    {
                                        if (reader.IsDBNull(2))
                                        {
                                            MessageBox.Show("Mangler tid for " + reader.GetString(0) + " " + reader.GetString(1), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                        else
                                        {
                                            currentTime = reader.GetTimeSpan(2);
                                            startNumber = reader.GetInt32(3);
                                            if (currentTime > prevTime)
                                            {
                                                points -= equalTime;
                                                equalTime = 0;
                                                points--;
                                                prevTime = currentTime;
                                            }
                                            else
                                            {
                                                equalTime++;
                                            }
                                            queryString = "UPDATE dbo.Resultat SET Poeng = " + points + " WHERE År = " + currentYear +
                                                " AND RennNummer = " + currentRace +
                                                " AND StartNummer = " + startNumber + ";";
                                            SqlConnection connection2 = new SqlConnection(connectionString);
                                            connection2.Open();
                                            SqlCommand command2 = new SqlCommand(queryString, connection2);
                                            SqlDataReader reader2 = command2.ExecuteReader();
                                            reader2.Close();
                                        }
                                    }
                                    if (!reader.IsDBNull(2))
                                    {
                                        txt += "<tr>\n<td>" + reader.GetString(0) + " " + reader.GetString(1) + "</td>\n<td>" + reader.GetTimeSpan(2) + "</td>\n</tr>\n";
                                        withTime++;
                                    }

                                }
                                txt += "</tbody>\n</table>\n";
                            }
                            reader.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Feil i generering av resultatlister!\nFeilmelding: " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            txt += "<p>Uten tid: " + withoutTime + "<br>\n";
            txt += "Med tid: " + withTime + "<br>\n";
            txt += "Totalt: " + (withoutTime + withTime) + "</p>\n<p></p>\n</body>\n</html>\n";
            webBrowser.DocumentText = txt;
        }

        private void tidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RennTid myForm = new RennTid();
            myForm.Show();
            myForm.mainForm = this;
        }

        private void totaltForSerienToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string connectionString = Properties.Settings.Default.ConnectionString;
            int races = 0;
            // Determine the number of races this year
            string queryString = "SELECT COUNT(DISTINCT RennNummer) FROM dbo.Resultat WHERE År = " + currentYear + ";";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataReader reader;
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        races = reader.GetInt32(0);
                    }
                    else
                    {
                        MessageBox.Show("Ingen resultat for år " + currentYear);
                        return;
                    }
                    reader.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Feil i uthenting av antall renn i år " + currentYear, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Retrieve each person that has been registered this year
            int iD;
            queryString = "SELECT PersonID FROM dbo.PoengrennSerie WHERE År = " + currentYear + ";";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        iD = reader.GetInt32(0);

                        queryString = "SELECT Poeng FROM dbo.Resultat INNER JOIN dbo.PersonInfo ON dbo.Resultat.PersonID = dbo.PersonInfo.ID WHERE Poeng IS NOT NULL AND År = " + currentYear + " AND PersonID = " + iD +
                            " ORDER BY Poeng DESC;";
                        SqlConnection connection2 = new SqlConnection(connectionString);
                        SqlCommand command2 = new SqlCommand(queryString, connection2);
                        connection2.Open();
                        SqlDataReader reader2 = command2.ExecuteReader();
                        int totalPoints = 0;
                        int rows = 1;
                        try
                        {
                            while (reader2.Read())
                            {
                                if (rows == races) break;
                                totalPoints += reader2.GetInt32(0);
                                rows++;
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Feil i henting av poeng", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        reader2.Close();

                        queryString = "UPDATE dbo.PoengrennSerie SET Poeng = " + totalPoints + " WHERE År = " + currentYear + " AND PersonID = " + iD + ";";
                        connection2 = new SqlConnection(connectionString);
                        command2 = new SqlCommand(queryString, connection2);
                        connection2.Open();
                        reader2 = command2.ExecuteReader();
                        reader2.Close();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Feil i henting av poeng", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            string txt = returnHTMLHeader();
            txt += "<h1>Resultat fra alle poengrenn i " + currentYear + "</h1>\n</header>\n";
            txt += "<p>Det dårligste poengrennet er strøket</p>\n";
            // Get the groups where points have been given
            queryString = "SELECT DISTINCT Klasse FROM dbo.PoengrennSerie WHERE Klasse IS NOT NULL AND År = " + currentYear + " AND Poeng > 0 ORDER BY Klasse ASC;";
            string group;
            string gender;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        group = reader.GetString(0).Trim();
                        gender = group.Substring(0, 6);
                        txt += "<h2 id = \"" + group + "\">" + group + "</h2>\n";
                        txt += "<table id=\"" + gender + "\">\n<thead>\n<tr>\n<th>Navn</th>\n<th>Poeng</th>\n</tr>\n</thead>\n<tbody>\n";

                        queryString = "SELECT Fornavn, Etternavn, Poeng FROM dbo.PoengrennSerie INNER JOIN dbo.PersonInfo ON dbo.PoengrennSerie.PersonID = dbo.PersonInfo.ID WHERE Poeng IS NOT NULL AND År = " + currentYear +
                           " AND Klasse = '" + group + "' ORDER BY Poeng DESC;";

                        SqlConnection connection2 = new SqlConnection(connectionString);
                        SqlCommand command2 = new SqlCommand(queryString, connection2);
                        connection2.Open();
                        SqlDataReader reader2 = command2.ExecuteReader();
                        while (reader2.Read())
                        {
                            txt += "<tr>\n<td>" + reader2.GetString(0) + " " + reader2.GetString(1) + "</td>\n<td>" + reader2.GetInt32(2) + "</td>\n</tr>\n";
                        }

                        reader2.Close();
                        txt += "</tbody>\n</table>\n";
                    }
                    reader.Close();

                }
                catch (Exception)
                {
                    MessageBox.Show("Feil i henting av poeng", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            txt += "<p></p>\n</body>\n</html>\n";
            webBrowser.DocumentText = txt;
        }

        private string returnHTMLHeader()
        {
            string txt = "<!doctype html>\n";
            txt += "<html>\n<meta charset=\"utf-8\">\n<head>\n";
            txt += "<style>\n";
            txt += "body\n{\nfont-family:Arial, Helvetica, sans-serif;\n}\n";
            txt += "table \n{\nborder-collapse:collapse;\n}\n";
            txt += "th, td\n{\npadding-left:15px;\npadding-right:15px;\ntext-align:left;\n}\n";
            txt += "#Deltagere th, #Deltagere td\n{\nborder:1px solid YellowGreen;\n}\n";
            txt += "#Gutter th, #Gutter td\n{\nborder:1px solid CornflowerBlue;\n}\n";
            txt += "#Jenter th, #Jenter td\n{\nborder:1px solid Salmon;\n}\n";
            txt += "#Deltagere th \n{\nbackground-color:YellowGreen;\ncolor:White;\n}\n";
            txt += "#Gutter th \n{\nbackground-color:CornflowerBlue;\ncolor:White;\n}\n";
            txt += "#Jenter th \n{\nbackground-color:Salmon;\ncolor:White;\n}\n";
            txt += "</style>\n</head>\n<body>\n<header>\n";

            return txt;
        }


        private void deltagereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string txt = returnHTMLHeader();
            txt += "<h1>\nRegistrerte deltagere\n</h1>\n</header>\n";
            txt += "<table id =\"Deltagere\">\n<thead>\n<tr>\n<th>Navn</th>\n<th>Fødselsår</th>\n<th>EPost</th>\n</tr>\n</thead>\n<tbody>\n";
            string connectionString = "";
            connectionString = Properties.Settings.Default.ConnectionString;
            string queryString = "SELECT Fornavn, Etternavn, Fødselsår, EPost FROM dbo.PersonInfo ORDER BY Fornavn ASC, Etternavn ASC, Fødselsår ASC;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        txt += "<tr>\n<td>" + reader.GetString(0) + " " + reader.GetString(1) + "</td>\n<td>" + reader.GetInt32(2) + "</td>\n<td>";
                        if (!reader.IsDBNull(3))
                        {
                            txt += reader.GetString(3);
                        }
                        txt += "</td>\n</tr>";

                    }
                    reader.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Feil i generering av liste over alle registrerte deltagere!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            txt += "</tbody></table><p></p>";
            txt += "</body></html>";
            webBrowser.DocumentText = txt;
        }

        private void avsluttToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            string dBText = Properties.Settings.Default.ConnectionString.Replace("Data Source=.\\SQLEXPRESS;AttachDbFilename=", "");
            //dBText = Properties.Settings.Default.ConnectionString.Replace("Data Source=.\\SQLEXPRESS2012;AttachDbFilename=", "");
            dBText = dBText.Replace(";Integrated Security=True;User Instance=True", "");
            DBValue.Text = dBText;

            RennForm myForm = new RennForm();
            myForm.Show();
            myForm.BringToFront();
            myForm.mainForm = this;
        }

        private void databaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            dlg.Filter = "Databasefiler|*.mdf";
            dlg.Title = "Velg databasefil";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DBValue.Text = dlg.FileName;
                Properties.Settings.Default.ConnectionString = "Data Source=.\\SQLEXPRESS;AttachDbFilename=" + dlg.FileName + ";Integrated Security=True;User Instance=True";
                Properties.Settings.Default.Save();
                //Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\perew\Documents\Flatås\Poengrenn\Poengrenn\Database1.mdf;Integrated Security=True;User Instance=True
            }
        }

        private void betaltToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int counter = 0;
            string txt = returnHTMLHeader();
            txt += "<h1>Deltagere som har betalt for poengrenn i " + currentYear + "</h1>\n</header>\n";
            txt += "<table id =\"Deltagere\">\n<thead>\n<tr>\n<th>Navn</th>\n</tr>\n</thead>\n<tbody>\n";
            string connectionString = "";
            connectionString = Properties.Settings.Default.ConnectionString;
            string queryString = "SELECT Fornavn, Etternavn FROM dbo.PersonInfo INNER JOIN dbo.PoengrennSerie ON dbo.PersonInfo.ID = dbo.PoengrennSerie.PersonID WHERE Betalt = 'True' AND År = " + currentYear + " ORDER BY Fornavn ASC, Etternavn ASC, Fødselsår ASC;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        txt += "<tr>\n<td>" + reader.GetString(0) + " " + reader.GetString(1) + "</td>\n</tr>";
                        counter++;
                    }
                    reader.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Feil i generering av liste over deltagere som har betalt!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            txt += "</tbody></table>";
            txt += "<p>Antall betalte deltagere: " + counter + "</p>\n";
            txt += "</body></html>";
            webBrowser.DocumentText = txt;
        }

        private void startnummereToolStripMenuItem_Click(object sender, EventArgs e)
        {


            StreamWriter writer = File.CreateText("Startliste.txt");

            int counter = 0;
            int startNummer = 0;
  
            string txt = returnHTMLHeader();
            if (currentRaceType == raceType.Klubbmesterskap)
            {
                txt += "<h1>Startnummere for klubbmesterskap " + currentYear + "</h1>\n</header>\n";
            }
            else
            {
                txt += "<h1>Startnummere for poengrenn " + currentRace + ", " + currentYear + "</h1>\n</header>\n";
            }
            txt += "<table id =\"Deltagere\">\n<thead>\n<tr>\n<th>Startnummer</th>\n<th>Navn</th>\n</tr>\n</thead>\n<tbody>\n";
            string connectionString = "";
            connectionString = Properties.Settings.Default.ConnectionString;
            string queryString;
            if (currentRaceType == raceType.Klubbmesterskap)
            {
                queryString = "SELECT StartNummer, Fornavn, Etternavn FROM dbo.PersonInfo INNER JOIN dbo.KlubbmesterskapResultat ON dbo.PersonInfo.ID = dbo.KlubbmesterskapResultat.PersonID WHERE StartNummer IS NOT NULL AND År = " + currentYear + " ORDER BY StartNummer ASC;";
            }
            else
            {
                queryString = "SELECT StartNummer, Fornavn, Etternavn FROM dbo.PersonInfo INNER JOIN dbo.Resultat ON dbo.PersonInfo.ID = dbo.Resultat.PersonID WHERE StartNummer IS NOT NULL AND År = " + currentYear + " AND RennNummer = " + currentRace + " ORDER BY StartNummer ASC;";
            }

            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        startNummer = reader.GetInt32(0);
                        writer.WriteLine(startNummer);
                        writer.Flush();

                        txt += "<tr>\n<td>" + startNummer + "</td>\n<td>" + reader.GetString(1) + " " + reader.GetString(2) + "</td>\n</tr>";
                        counter++;
                    }
                    reader.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Feil i generering av liste over startnummere!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            txt += "</tbody></table>";
            txt += "<p>Antall startende: " + counter + "</p><p></p>\n";
            txt += "</body></html>";
            webBrowser.DocumentText = txt;
            writer.Close();
        }

        private void åretsDeltagereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int counter = 0;
            string txt = returnHTMLHeader();
            txt += "<h1>Deltagere som har deltatt i poengrenn i " + currentYear + "</h1>\n</header>\n";
            txt += "<table id =\"Deltagere\">\n<thead>\n<tr>\n<th>Navn</th>\n</tr>\n</thead>\n<tbody>\n";
            string connectionString = "";
            connectionString = Properties.Settings.Default.ConnectionString;
            string queryString = "SELECT Fornavn, Etternavn FROM dbo.PersonInfo INNER JOIN dbo.PoengrennSerie ON dbo.PersonInfo.ID = dbo.PoengrennSerie.PersonID WHERE År = " + currentYear + " ORDER BY Fornavn ASC, Etternavn ASC, Fødselsår ASC;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        txt += "<tr>\n<td>" + reader.GetString(0) + " " + reader.GetString(1) + "</td>\n</tr>";
                        counter++;
                    }
                    reader.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Feil i generering av liste over årets deltagere!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            txt += "</tbody></table>";
            txt += "<p>Antall deltagere: " + counter + "</p>\n";
            txt += "</body></html>";
            webBrowser.DocumentText = txt;
        }
    

    
        private void fjoråretsDeltagereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int counter = 0;
            string txt = returnHTMLHeader();
            txt += "<h1>Deltagere som har deltatt i poengrenn i " + (currentYear-1).ToString() + "</h1>\n</header>\n";
            txt += "<table id =\"Deltagere\">\n<thead>\n<tr>\n<th>Navn</th>\n</tr>\n</thead>\n<tbody>\n";
            string connectionString = "";
            connectionString = Properties.Settings.Default.ConnectionString;
            string queryString = "SELECT Fornavn, Etternavn, Fødselsår, Kjønn FROM dbo.PersonInfo INNER JOIN dbo.PoengrennSerie ON dbo.PersonInfo.ID = dbo.PoengrennSerie.PersonID WHERE År = " + (currentYear-1).ToString() + " ORDER BY Fødselsår DESC, Kjønn DESC;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        txt += "<tr>\n<td>" + reader.GetString(0) + " " + reader.GetString(1) + " " + (currentYear - reader.GetInt32(2)).ToString() + "</td>\n</tr>";
                        counter++;
                    }
                    reader.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Feil i generering av liste over fjorårets deltagere!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            txt += "</tbody></table>";
            txt += "<p>Antall deltagere: " + counter + "</p>\n";
            txt += "</body></html>";
            webBrowser.DocumentText = txt;
        }
    }
}
