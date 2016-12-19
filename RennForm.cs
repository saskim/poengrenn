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
    public partial class RennForm : Form
    {
        public MainForm mainForm;
        int year;
        int race;
        int startInterval;
        string style;

        private RadioButton selectedRB;

        public RennForm()
        {
            InitializeComponent();

            string connectionString = Properties.Settings.Default.ConnectionString;

            // Retrieves the latest specification for a race
            string queryString = "SELECT År, Nummer, Stilart, StartIntervall FROM dbo.Poengrenn ORDER BY År DESC, Nummer DESC;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(queryString, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    if (!reader.IsDBNull(0))
                    {
                        PoengrennRB.Select();
                        ÅrTB.Text = reader.GetInt32(0).ToString();
                        PoengrennTB.Text = reader.GetInt32(1).ToString();
                        switch (reader.GetString(2).Trim())
                        {
                            case "Klassisk":
                                KlassiskRB.Select();
                                break;
                            case "Fristil":
                                FristilRB.Select();
                                break;
                            case "Skicross":
                                SkicrossRB.Select();
                                break;
                            default:
                                MessageBox.Show("Ukjent stilart " + reader.GetString(2).Trim() + " lagret i databasen");
                                break;
                        }

                        switch (reader.GetInt32(3))
                        {
                            case 15:
                                RB15.Select();
                                break;
                            case 30:
                                RB30.Select();
                                break;
                            default:
                                MessageBox.Show("Ukjent startintervall " + reader.GetInt32(3) + " lagret i databasen");
                                break;
                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fant ikke siste poengrenn\nFeilmelding: " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void setCurrentRace()
        {
            try
            {
                year = Convert.ToInt32(ÅrTB.Text);
                mainForm.currentYear = year;
                mainForm.ÅrValue.Text = year.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Ikke gyldig år!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (RB15.Checked)
            {
                mainForm.startInterval = TimeSpan.FromSeconds(15);
            }
            else
            {
                mainForm.startInterval = TimeSpan.FromSeconds(30);
            }
            if (PoengrennRB.Checked)
            {
                try
                {
                    race = Convert.ToInt32(PoengrennTB.Text);
                    mainForm.currentRace = race;
                    mainForm.currentRaceType = raceType.Poengrenn;
                    mainForm.RennValue.Text = race.ToString();
                }
                catch (Exception)
                {
                    MessageBox.Show("Ikke gyldig renn!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (KlubbmesterskapRB.Checked)
            {
                mainForm.currentRaceType = raceType.Klubbmesterskap;
                mainForm.RennValue.Text = "Klubbmesterskap";
            }
        }

        
        private void LagreBtn_Click(object sender, EventArgs e)
        {
            setCurrentRace();

            if (!(KlassiskRB.Checked || FristilRB.Checked || SkicrossRB.Checked))
            {
                MessageBox.Show("Ingen stilart er valgt!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (KlassiskRB.Checked)
            {
                style = KlassiskRB.Text;
            }
            else if (FristilRB.Checked)
            {
                style = FristilRB.Text;
            }
            else if (SkicrossRB.Checked)
            {
                style = SkicrossRB.Text;
            }
            

            if (!(RB15.Checked || RB30.Checked))
            {
                MessageBox.Show("Startintervall er ikke valgt!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (RB15.Checked)
            {
                startInterval = 15;
            }
            else if (RB30.Checked)
            {
                startInterval = 30;
            }
            raceType rType = mainForm.currentRaceType;

            if (rType != raceType.Poengrenn && rType != raceType.Klubbmesterskap)
            {
                MessageBox.Show("Hverken poengrenn eller klubbmesterskap er valgt!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string connectionString = Properties.Settings.Default.ConnectionString;
            string queryString;
            try
            {
                // Check whether the race has been previously specified
                if (rType == raceType.Poengrenn)
                {
                    queryString = "SELECT År FROM dbo.Poengrenn WHERE År = " + year + " AND Nummer = " + race + ";";
                }
                else
                {
                    queryString = "SELECT År FROM dbo.Klubbmesterskap WHERE År = " + year + ";";
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(queryString, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    if (reader.HasRows)
                    {
                        if (rType == raceType.Poengrenn)
                        {
                            queryString = "UPDATE dbo.Poengrenn SET Stilart = '" + style + "', StartIntervall = " + startInterval.ToString() + " WHERE År = " + year + " AND Nummer = " + race + ";";
                        }
                        else
                        {
                            queryString = "UPDATE dbo.Klubbmesterskap SET Stilart = '" + style + "', StartIntervall = " + startInterval.ToString() + " WHERE År = " + year + ";";
                        }
                    }
                    else
                    {
                        if (rType == raceType.Poengrenn)
                        {
                            queryString = "INSERT INTO dbo.Poengrenn (År, Nummer, Stilart, StartIntervall) VALUES (" +
                                year + ", " + race + ", '" + style + "', " + startInterval.ToString() + ");";
                        }
                        else
                        {
                            queryString = "INSERT INTO dbo.Klubbmesterskap (År, Stilart, StartIntervall) VALUES (" +
                                year + ", '" + style + "', " + startInterval.ToString() + ");";
                        }
                    }
                }
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verdiene er ikke overført til databasen!\nFeilmelding: " + ex.Message, "",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Close();
        }


        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (rb == null)
            {
                MessageBox.Show("Sender is not a RadioButton");
                return;
            }

            // Ensure that the RadioButton.Checked property changed to true. 
            if (rb.Checked)
            {
                // Keep track of the selected RadioButton by saving a reference to it.
                selectedRB = rb;
            }
        }

        private void velgBtn_Click(object sender, EventArgs e)
        {
            setCurrentRace();
            this.Close();
        }



        private void KlubbmesterskapRB_CheckedChanged(object sender, EventArgs e)
        {
            if (KlubbmesterskapRB.Checked)
            {
                PoengrennTB.Enabled = false;
                SkicrossRB.Enabled = false;
            }
            else
            {
                PoengrennTB.Enabled = true;
                SkicrossRB.Enabled = true;
            }
        }

    }
}
 