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
    public partial class DeltagereForm : Form
    {
        public DeltagerForm userForm;

        public DeltagereForm()
        {
            InitializeComponent();
        }

        private void UsersDGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dGV = (DataGridView)sender;
            DataGridViewRow row = (DataGridViewRow)dGV.CurrentRow;
            userForm.userID = Convert.ToInt32(row.Cells[0].Value);
            userForm.FornavnTB.Text = row.Cells[1].Value.ToString();
            userForm.EtternavnTB.Text = row.Cells[2].Value.ToString();
            int yOB = Convert.ToInt32(row.Cells[3].Value);
            userForm.FødselsÅrTB.Text = yOB.ToString();
            userForm.EpostTB.Text = row.Cells[4].Value.ToString();
            string gender = row.Cells[5].Value.ToString();
            if (gender == "Jente")
            {
                userForm.JenteRB.Select();
            }
            else if (gender == "Gutt")
            {
                userForm.GuttRB.Select();
            }
            else
            {
                MessageBox.Show("Kjønn er ikke spesifisert!");
            }

            string group;
            int age = userForm.mainForm.currentYear - yOB;
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
            userForm.KlasseCB.Text = group;
            userForm.SetStartNumber();

            if (userForm.mainForm.currentRaceType == raceType.Poengrenn)
            {
                string queryString = "SELECT Betalt, Premie FROM dbo.PoengrennSerie WHERE PersonId = " + userForm.userID + " AND År = " + userForm.mainForm.currentYear + ";";
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
                            userForm.BetaltCB.Checked = reader.GetBoolean(0);
                            userForm.PremieCB.Checked = reader.GetBoolean(1);
                        }
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
            }
            if (dGV.RowCount < 3)
            {
                this.Close();
            }
            else
            {
                userForm.Select();
            }
        }
    }
}
