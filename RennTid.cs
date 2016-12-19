using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RennApplication
{
    public partial class RennTid : Form
    {
        public MainForm mainForm;

        public RennTid()
        {
            InitializeComponent();
        }



        private void LeggInnBtn_Click(object sender, EventArgs e)
        {
            if (StartNummerTB.Text == "" || (StartTB.Text == "" && SluttTB.Text == ""))
            {
                MessageBox.Show("Startnummer og enten starttid eller sluttid må spesifiseres", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string startTime = "StartTid = NULL";
            string endTime = "SluttTid = NULL";
            string elapsedTime = "Tidsforbruk = NULL";
            try
            {
                if (StartTB.Text != "")
                {
                    if (TimeSpan.Parse(StartTB.Text).TotalSeconds < 0 || TimeSpan.Parse(StartTB.Text).TotalDays >= 1)
                    {
                        MessageBox.Show("Starttid må være større enn 0 og mindre enn et døgn", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        startTime = "StartTid = '" + TimeSpan.Parse(StartTB.Text).ToString() + "'";
                        mainForm.prevStartTime = TimeSpan.Parse(StartTB.Text);
                    }
                }
                if (SluttTB.Text != "")
                {
                    if (TimeSpan.Parse(SluttTB.Text).TotalSeconds < 0 || TimeSpan.Parse(SluttTB.Text).TotalDays >= 1)
                    {
                        MessageBox.Show("Sluttid må være større enn 0 og mindre enn et døgn", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        endTime = "SluttTid = '" + TimeSpan.Parse(SluttTB.Text).ToString() + "'";
                    }
                }
                if (TidTB.Text != "")
                {
                    if (TimeSpan.Parse(TidTB.Text).TotalSeconds < 0 || TimeSpan.Parse(TidTB.Text).TotalDays >= 1)
                    {
                        MessageBox.Show("Tidsforbruk må være større enn 0 og mindre enn et døgn", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        elapsedTime = "Tidsforbruk = '" + TimeSpan.Parse(TidTB.Text).ToString() + "'";
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Sjekk format på start eller sluttid!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            string connectionString = Properties.Settings.Default.ConnectionString;
            string queryString;
            // Make sure the start number has been registered in the result table
            if (mainForm.currentRaceType == raceType.Poengrenn)
            {
                queryString = "SELECT År FROM dbo.Resultat WHERE År = " + mainForm.currentYear + " AND RennNummer = " + mainForm.currentRace + " AND StartNummer = " + StartNummerTB.Text + ";";
            }
            else
            {
                queryString = "SELECT År FROM dbo.KlubbmesterskapResultat WHERE År = " + mainForm.currentYear + " AND StartNummer = " + StartNummerTB.Text + ";";
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    if (mainForm.currentRaceType == raceType.Poengrenn)
                    {
                        queryString = "UPDATE dbo.Resultat SET " + startTime + ", " + endTime + ", " + elapsedTime + " WHERE År = " + mainForm.currentYear + " AND RennNummer = " + mainForm.currentRace + " AND StartNummer = " + StartNummerTB.Text + ";";
                    }
                    else
                    {
                        queryString = "UPDATE dbo.KlubbmesterskapResultat SET " + startTime + ", " + endTime + ", " + elapsedTime + " WHERE År = " + mainForm.currentYear + " AND StartNummer = " + StartNummerTB.Text + ";";
                    }
                }
                else
                {
                    if (mainForm.currentRaceType == raceType.Poengrenn)
                    {
                        MessageBox.Show("Startnummer " + StartNummerTB.Text + " ikke funnet for poengrenn " + mainForm.currentRace + " i år " + mainForm.currentYear, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Startnummer " + StartNummerTB.Text + " ikke funnet for klubbmesterskap i år " + mainForm.currentYear, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                }
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                command.ExecuteReader();
            }
            StatusTB.Text = "Sist lagret: " + StartNummerTB.Text;
            
            
            if (mainForm.currentRaceType == raceType.Klubbmesterskap)
            {
                queryString = "SELECT MIN(StartNummer) FROM dbo.KlubbmesterskapResultat WHERE StartNummer > " +  StartNummerTB.Text + " AND År = " + mainForm.currentYear + ";";
            }
            else
            {
                queryString = "SELECT MIN(StartNummer) FROM dbo.Resultat WHERE StartNummer > " + StartNummerTB.Text + " AND År = " + mainForm.currentYear + " AND RennNummer = " + mainForm.currentRace + ";";
            }


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    if (!reader.IsDBNull(0))
                    {
                        StartNummerTB.Text = reader.GetInt32(0).ToString();
                    }
                    else
                    {
                        MessageBox.Show("Siste startnummer lagt inn " , "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
                    }
                }
            }

        }

        private void CalculateTimeUsed()
        {
            if (StartTB.Text == "" || SluttTB.Text == "")
            {
                TidTB.Text = "";
                return;
            }
            try
            {
                TimeSpan startTime = TimeSpan.Parse(StartTB.Text);
                TimeSpan finishTime = TimeSpan.Parse(SluttTB.Text);
                TimeSpan usedTime = finishTime - startTime;
                TidTB.Text = usedTime.ToString();
            }
            catch (Exception)
            {
                return;
            }
        }

        private void timeChanged(object sender, EventArgs e)
        {
            CalculateTimeUsed();
        }

        private void hentTid(object sender, EventArgs e)
        {
            StartTB.Text = "";
            SluttTB.Text = "";
            TidTB.Text = "";

            if (StartNummerTB.Text == "") return;

            try
            {
                Convert.ToInt32(StartNummerTB.Text.Trim());
            }
            catch (Exception)
            {
                MessageBox.Show("Startnummer er ikke et tall!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = Properties.Settings.Default.ConnectionString;
            string queryString;
            if (mainForm.currentRaceType == raceType.Poengrenn)
            {
                queryString = "SELECT StartTid, SluttTid, Tidsforbruk FROM dbo.Resultat WHERE År = " + mainForm.currentYear + " AND RennNummer = " + mainForm.currentRace + " AND StartNummer = " + StartNummerTB.Text + ";";
            }
            else
            {
                queryString = "SELECT StartTid, SluttTid, Tidsforbruk FROM dbo.KlubbmesterskapResultat WHERE År = " + mainForm.currentYear + " AND StartNummer = " + StartNummerTB.Text + ";";
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    if (!reader.IsDBNull(0))
                    {
                        StartTB.Text = reader.GetTimeSpan(0).ToString();
                    }
                    else
                    {
                        StartTB.Text = (mainForm.prevStartTime + mainForm.startInterval).ToString();
                    }
                    if (!reader.IsDBNull(1)) SluttTB.Text = reader.GetTimeSpan(1).ToString();
                    if (!reader.IsDBNull(2)) TidTB.Text = reader.GetTimeSpan(2).ToString();
                }
            }
        }

        private void Add30Sec_Click(object sender, EventArgs e)
        {
            if (StartTB.Text == "")
            {
                StartTB.Text = TimeSpan.FromSeconds(30).ToString();
            }
            else
            {
                StartTB.Text = (TimeSpan.Parse(StartTB.Text) + TimeSpan.FromSeconds(30)).ToString();
            }
        }

        private void Add15Sec_Click(object sender, EventArgs e)
        {
            if (StartTB.Text == "")
            {
                StartTB.Text = TimeSpan.FromSeconds(15).ToString();
            }
            else
            {
                StartTB.Text = (TimeSpan.Parse(StartTB.Text) + TimeSpan.FromSeconds(15)).ToString();
            }
        }

    }
}
