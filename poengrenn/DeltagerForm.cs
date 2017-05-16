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
    public partial class DeltagerForm : Form
    {
        public MainForm mainForm;
        public int userID;
        private RadioButton selectedGenderRB;

        public DeltagerForm()
        {
            InitializeComponent();
        }



        private void HentBtn_Click(object sender, EventArgs e)
        {
            string connectionString = Properties.Settings.Default.ConnectionString;
            string queryString = "SELECT ID, Fornavn, Etternavn, Fødselsår, EPost, Kjønn FROM dbo.PersonInfo";
            Boolean whereUsed = false;

            if (FornavnTB.Text != "" || EtternavnTB.Text != "" || FødselsÅrTB.Text != "" || EpostTB.Text != "")
            {
                queryString += " WHERE ";

                if (FornavnTB.Text != "")
                {
                    queryString += " Fornavn LIKE '%" + FornavnTB.Text + "%'";
                    whereUsed = true;
                }

                if (EtternavnTB.Text != "")
                {
                    if (whereUsed)
                    {
                        queryString += " AND";
                    }
                    queryString += " Etternavn LIKE '%" + EtternavnTB.Text + "%'";
                    whereUsed = true;
                }

                if (FødselsÅrTB.Text != "")
                {
                    if (whereUsed)
                    {
                        queryString += " AND";
                    }
                    queryString += " Fødselsår = " + FødselsÅrTB.Text + "";
                    whereUsed = true;
                }

                if (EpostTB.Text != "")
                {
                    if (whereUsed)
                    {
                        queryString += " AND";
                    }
                    queryString += " EPost LIKE '%" + EpostTB.Text + "%'";
                    whereUsed = true;
                }
            }

            queryString += " ORDER BY Fornavn ASC, Etternavn ASC, Fødselsår ASC;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    DeltagereForm dForm = new DeltagereForm();
                    dForm.Show();
                    dForm.userForm = this;
                    while (reader.Read())
                    {
                        DataGridViewRow row = (DataGridViewRow)dForm.UsersDGV.Rows[0].Clone();
                        if (!reader.IsDBNull(0))
                        {
                            row.Cells[0].Value = reader.GetInt32(0);
                        }
                        if (!reader.IsDBNull(1))
                        {
                            row.Cells[1].Value = reader.GetString(1);
                        }
                        if (!reader.IsDBNull(2))
                        {
                            row.Cells[2].Value = reader.GetString(2);
                        }
                        if (!reader.IsDBNull(3))
                        {
                            row.Cells[3].Value = reader.GetInt32(3);
                        }
                        if (reader.IsDBNull(4))
                        {
                            row.Cells[4].Value = "";
                        }
                        else
                        {
                            row.Cells[4].Value = reader.GetString(4);
                        }
                        if (!reader.IsDBNull(5))
                        {
                            row.Cells[5].Value = reader.GetString(5);
                        }
                        dForm.UsersDGV.Rows.Add(row);
                        dForm.UsersDGV.AllowUserToAddRows = false;
                        dForm.UsersDGV.AllowUserToDeleteRows = false;
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
        }

        private void OppdaterBtn_Click(object sender, EventArgs e)
        {
            if (FornavnTB.Text == "" || EtternavnTB.Text == "" || FødselsÅrTB.Text == "" || selectedGenderRB == null)
            {
                MessageBox.Show("Fornavn, etternavn, fødselsår og kjønn må spesifiseres", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = Properties.Settings.Default.ConnectionString;

            // Check if the person previously has been registered
            string queryString = "SELECT ID From dbo.PersonInfo WHERE ID = " + userID.ToString() + ";";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    reader.Close();
                    try
                    {
                        queryString = "UPDATE dbo.PersonInfo SET Fornavn = '" + FornavnTB.Text + "', Etternavn = '" + EtternavnTB.Text + "', Fødselsår = " + FødselsÅrTB.Text + ", EPost = '" + EpostTB.Text + "', Kjønn = '" + selectedGenderRB.Text + "' WHERE ID = " + userID + ";";
                        command = new SqlCommand(queryString, connection);
                        command.ExecuteReader();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("PersonInfo tabellen ble ikke oppdatert!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    reader.Close();
                    try
                    {
                        queryString = "SELECT MAX(ID) FROM dbo.PersonInfo;";
                        command = new SqlCommand(queryString, connection);
                        reader = command.ExecuteReader();
                        reader.Read();
                        userID = (int)reader[0];
                        reader.Close();

                        queryString = "INSERT INTO dbo.PersonInfo (ID, Fornavn, Etternavn, Fødselsår, EPost, Kjønn) VALUES (" +
                            ++userID + " , '" +
                            FornavnTB.Text + "', '" +
                            EtternavnTB.Text + "', " +
                            FødselsÅrTB.Text + ", '" +
                            EpostTB.Text + "', '" +
                            selectedGenderRB.Text + "');";
                        command = new SqlCommand(queryString, connection);
                        command.ExecuteReader();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ny deltager ble ikke overført til PersonInfo tabellen!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            if (mainForm.currentYear <= 0)
            {
                MessageBox.Show("Årstall må være > 0", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (mainForm.currentRaceType == raceType.Poengrenn)
            {
                // Check registration in PoengrennSerie
                queryString = "SELECT PersonID From dbo.PoengrennSerie WHERE PersonID = " + userID + " AND År = " + mainForm.currentYear + ";";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(queryString, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    if (reader.HasRows)
                    {
                        reader.Close();
                        try
                        {
                            queryString = "UPDATE dbo.PoengrennSerie SET År = " + mainForm.currentYear + ", PersonID = " + userID + ", Betalt = '" + BetaltCB.Checked + "', Premie = '" + PremieCB.Checked + "', Klasse = '" + KlasseCB.Text + "' WHERE PersonID = " + userID + " AND År = " + mainForm.currentYear + ";";
                            command = new SqlCommand(queryString, connection);
                            command.ExecuteReader();
                            reader.Close();
                        }

                        catch (Exception)
                        {
                            MessageBox.Show("Poengrennserietabellen ble ikke oppdatert!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        reader.Close();
                        try
                        {
                            queryString = "INSERT INTO dbo.PoengrennSerie(År, PersonID, Betalt, Premie, Klasse) VALUES (" +
                                mainForm.currentYear + ", " + userID + ", '" + BetaltCB.Checked + "', '" + PremieCB.Checked + "', '" + KlasseCB.Text + "');";
                            command = new SqlCommand(queryString, connection);
                            command.ExecuteReader();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Deltagerinfo ble ikke overført til poengrennserietabellen", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }

            if (StartnummerTB.Text == "" || KlasseCB.Text == "")
            {
                MessageBox.Show("Startnummer og klasse må være spesifisert!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (mainForm.currentRaceType == raceType.Poengrenn)
            {
                if (mainForm.currentRace <= 0)
                {
                    MessageBox.Show("Poengrenn må være > 0!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                queryString = "SELECT PersonID From dbo.Resultat WHERE År = " + mainForm.currentYear + " AND RennNummer = " + mainForm.currentRace + " AND PersonID = " + userID + ";";
            }
            else
            {
                queryString = "SELECT PersonID From dbo.KlubbmesterskapResultat WHERE År = " + mainForm.currentYear + " AND PersonID = " + userID + ";";
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    reader.Close();
                    try
                    {
                        if (mainForm.currentRaceType == raceType.Poengrenn)
                        {
                            queryString = "UPDATE dbo.Resultat SET StartNummer = " + StartnummerTB.Text + ", Klasse = '" + KlasseCB.Text + "' WHERE År = " + mainForm.currentYear + " AND RennNummer = " + mainForm.currentRace + " AND PersonID = " + userID + ";";
                        }
                        else
                        {
                            queryString = "UPDATE dbo.KlubbmesterskapResultat SET StartNummer = " + StartnummerTB.Text + ", Klasse = '" + KlasseCB.Text + "' WHERE År = " + mainForm.currentYear + " AND PersonID = " + userID + ";";
                        }
                        command = new SqlCommand(queryString, connection);
                        command.ExecuteReader();
                    }

                    catch (Exception)
                    {
                        MessageBox.Show("Resultattabellen ble ikke oppdatert", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    reader.Close();
                    try
                    {
                        if (mainForm.currentRaceType == raceType.Poengrenn)
                        {
                            queryString = "INSERT INTO dbo.Resultat(År, RennNummer, PersonID, StartNummer, Klasse) VALUES (" +
                                mainForm.currentYear + ", " +
                                mainForm.currentRace + ", " +
                                userID + ", " +
                                StartnummerTB.Text + ", '" +
                                KlasseCB.Text + "');"; ;
                        }
                        else
                        {
                            queryString = "INSERT INTO dbo.KlubbmesterskapResultat(År, PersonID, StartNummer, Klasse) VALUES (" +
                                mainForm.currentYear + ", " +
                                userID + ", " +
                                StartnummerTB.Text + ", '" +
                                KlasseCB.Text + "');"; ;
                        }
                        command = new SqlCommand(queryString, connection);
                        reader = command.ExecuteReader();
                        reader.Close();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Deltagerinfo ble ikke lagt til i resultattabellen", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            BrukerTB.Text = FornavnTB.Text + " " + EtternavnTB.Text + ": " + StartnummerTB.Text + " lagt til";
            ClearUserForm();
        }


        private void SetGroup()
        {
            if (selectedGenderRB != null && FødselsÅrTB.Text != "")
            {
                string gender = selectedGenderRB.Text;
                string group;
                int age = mainForm.currentYear - Convert.ToInt32(FødselsÅrTB.Text);
                if (age <= 7)
                {
                    group = "Gutter/jenter 0-7 år";
                }
                else if (age >= 17)
                {
                    if (gender == "Gutt")
                    {
                        group = "Gutter 17+ år";
                    }
                    else
                    {
                        group = "Jenter 17+ år";
                    }
                }
                else
                {
                    if (gender == "Gutt")
                    {
                        group = "Gutter " + age + " år";
                    }
                    else
                    {
                        group = "Jenter " + age + " år";
                    }
                }
                KlasseCB.Text = group;
            }
        }


        public void SetStartNumber()
        {
            Boolean increasing = true;
            int firstNumber = 0;
            int lastNumber = 0;
            int startNumber = 1;
            string queryString;

            // Check if the user has already been given a start number

            string connectionString = Properties.Settings.Default.ConnectionString;
            if (mainForm.currentRaceType == raceType.Poengrenn)
            {
                if (mainForm.currentYear <= 0 || mainForm.currentRace <= 0)
                {
                    MessageBox.Show("År og poengrenn må være valgt", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                queryString = "SELECT StartNummer FROM dbo.Resultat WHERE År = " +
                    mainForm.currentYear + " AND RennNummer = " +
                    mainForm.currentRace + " AND PersonID = " +
                    userID + ";";
            }
            else
            {
                if (mainForm.currentYear <= 0)
                {
                    MessageBox.Show("År må være valgt", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                queryString = "SELECT StartNummer FROM dbo.KlubbmesterskapResultat WHERE År = " +
                    mainForm.currentYear + " AND PersonID = " + userID + ";";
            }

            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        if (!reader.IsDBNull(0))
                        {
                            startNumber = reader.GetInt32(0);
                            StartnummerTB.Text = startNumber.ToString();
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Feil under sjekking om deltager har fått startnummer\nFeilmelding: " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (KlasseCB.Text.Contains("0-7"))
            {
                firstNumber = 1;
            }
            else
            {
                string gender = KlasseCB.Text.Substring(0, 3);
                string age = KlasseCB.Text.Substring(KlasseCB.Text.IndexOf(" ") + 1, KlasseCB.Text.LastIndexOf(" ") - KlasseCB.Text.IndexOf(" "));
                age=age.Replace("+", "");
                queryString = "SELECT StigendeRekkefølge, FørsteStartNummer, SisteStartNummer FROM Startnummere WHERE Alder = " + age + " AND Kjønn LIKE '" + gender + "%'";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {
                        reader.Read();
                        if (!reader.IsDBNull(0))
                        {
                            increasing = reader.GetBoolean(0);
                            firstNumber = reader.GetInt32(1);
                            lastNumber = reader.GetInt32(2);
                        }
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
            }


            if (mainForm.currentRaceType == raceType.Poengrenn)
            {
                queryString = "SELECT COUNT(StartNummer) FROM dbo.Resultat WHERE År = '" +
                    mainForm.currentYear + "' AND RennNummer = '" +
                    mainForm.currentRace + "' AND Klasse = '" +
                    KlasseCB.Text + "'";
            }
            else
            {
                queryString = "SELECT COUNT(StartNummer) FROM dbo.KlubbmesterskapResultat WHERE År = '" +
                    mainForm.currentYear + "' AND Klasse = '" +
                    KlasseCB.Text + "'";
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    reader.Read();
                    if (!reader.IsDBNull(0))
                    {
                        if (increasing)
                        {
                        startNumber = firstNumber + reader.GetInt32(0);
                        }
                        else
                        {
                        startNumber = lastNumber - reader.GetInt32(0);
                        }
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            StartnummerTB.Text = startNumber.ToString();
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
                selectedGenderRB = rb;

                if (FødselsÅrTB.Text != "")
                {
                    SetGroup();
                    SetStartNumber();
                }
            }
        }

        private void FødselsÅrTB_TextChanged(object sender, EventArgs e)
        {
            if (!(selectedGenderRB == null))
            {
                SetGroup();
                SetStartNumber();
            }
        }

        private void KlasseCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetStartNumber();
        }

        private void SlettBtn_Click(object sender, EventArgs e)
        {
            string connectionString = Properties.Settings.Default.ConnectionString;

            string deleteString = "Bekreft sletting av ";
            string fornavn = "";
            string etternavn = "";
            int year = 0;

            // Check if the person previously has been registered
            string queryString = "SELECT Fornavn, Etternavn, Fødselsår From dbo.PersonInfo WHERE ID = " + userID.ToString() + ";";
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
                        fornavn = reader.GetString(0);
                    }
                    if (!reader.IsDBNull(1))
                    {
                        etternavn = reader.GetString(1);
                    }
                    if (!reader.IsDBNull(2))
                    {
                        year = reader.GetInt32(2);
                    }
                    reader.Close();
                    deleteString += fornavn + " " + etternavn + " (" + year + ")";
                    if (MessageBox.Show(deleteString, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        try
                        {
                            queryString = "DELETE FROM dbo.PersonInfo WHERE ID = " + userID + ";";
                            command = new SqlCommand(queryString, connection);
                            command.ExecuteReader();
                            
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("PersonInfo tabellen ble ikke oppdatert!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        try
                        {
                            queryString = "DELETE FROM dbo.PoengrennSerie WHERE PersonID = " + userID + ";";
                            command = new SqlCommand(queryString, connection);
                            command.ExecuteReader();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Poengrennserietabellen ble ikke oppdatert!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        try
                        {
                            queryString = "DELETE FROM dbo.Resultat WHERE PersonID = " + userID + ";";
                            command = new SqlCommand(queryString, connection);
                            command.ExecuteReader();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Resultattabellen ble ikke oppdatert!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        try
                        {
                            queryString = "DELETE FROM dbo.KlubbmesterskapResultat WHERE PersonID = " + userID + ";";
                            command = new SqlCommand(queryString, connection);
                            command.ExecuteReader();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Klubbmesterskaptabellen ble ikke oppdatert!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }




                    }

                }
            }

            BrukerTB.Text = fornavn + " " + etternavn + "( " + year + ") slettet";
            ClearUserForm();
        }




        private void ClearUserForm()
        {
            FornavnTB.Clear();
            EtternavnTB.Clear();
            FødselsÅrTB.Clear();
            EpostTB.Clear();
            JenteRB.Checked = false;
            GuttRB.Checked = false;
            BetaltCB.Checked = false;
            PremieCB.Checked = false;
            KlasseCB.ResetText();
            StartnummerTB.Clear();
            userID = 0;
            selectedGenderRB = null;
        }
    }
}
