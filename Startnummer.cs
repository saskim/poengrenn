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
    public partial class StartnummerForm : Form
    {
        public StartnummerForm()
        {
            InitializeComponent();

            string connectionString = Properties.Settings.Default.ConnectionString;
            string queryString = "SELECT Alder, Kjønn, FørsteStartNummer, StigendeRekkefølge FROM dbo.Startnummere ORDER BY Alder ASC, Kjønn DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    DataGridViewRow row;
                    while (reader.Read())
                    {
                        row = (DataGridViewRow)StartNummerGV.Rows[0].Clone();
                        row.Cells[0].Value = reader.GetInt32(0);    // Alder
                        row.Cells[1].Value = reader.GetInt32(2);    // FørsteStartNummer
                        if (reader.GetBoolean(3))
                        {
                            row.Cells[2].Value = "Stigende";
                        }
                        else
                        {
                            row.Cells[2].Value = "Synkende";
                        }
                        reader.Read();
                        row.Cells[3].Value = reader.GetInt32(2);    // FørsteStartNummer
                        if (reader.GetBoolean(3))
                        {
                            row.Cells[4].Value = "Stigende";
                        }
                        else
                        {
                            row.Cells[4].Value = "Synkende";
                        }

                        StartNummerGV.Rows.Add(row);
                        StartNummerGV.AllowUserToAddRows = false;
                        StartNummerGV.AllowUserToDeleteRows = false;
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void OppdaterBtn_Click(object sender, EventArgs e)
        {
            DataGridViewRow row;
            string connectionString = Properties.Settings.Default.ConnectionString;
            //string queryString;
            //string firstString = "UPDATE dbo.Startnummere SET FørsteStartNummer = ";
            //string lastString = "UPDATE dbo.Startnummere SET SisteStartNummer = ";
            int age;
            int firstGirl, lastGirl;
            int prevFirstBoy, firstBoy, lastBoy;
            prevFirstBoy = 0;
            for (int i = 0; i <= 9; i++)
            {
                row = (DataGridViewRow)StartNummerGV.Rows[i];
                age = Convert.ToInt32(row.Cells[0].Value);
                firstGirl = Convert.ToInt32(row.Cells[1].Value);
                firstBoy = Convert.ToInt32(row.Cells[3].Value);
                if (firstBoy <= firstGirl)
                {
                    MessageBox.Show("Første startnummer for gutter " + age.ToString() + " år (" + firstBoy + ") må være større enn for jenter (" + firstGirl + ")");
                    return;
                }
                if (firstGirl <= prevFirstBoy)
                {
                    MessageBox.Show("Første startnummer for jenter " + age.ToString() + " år (" + firstGirl + ") må være større enn for gutter " + (age-1).ToString() + " (" + prevFirstBoy + ")");
                    return;
                }
                lastGirl = firstBoy - 1;
                if (i == 9)
                {
                    lastBoy = 300;
                }
                else
                {                                        
                    row = (DataGridViewRow)StartNummerGV.Rows[i+1];
                    lastBoy = Convert.ToInt32(row.Cells[1].Value) - 1; 
                }
                prevFirstBoy = firstBoy;

                //queryString = firstString + firstGirl + " WHERE Alder = " + age + " AND Kjønn = 'Jente'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(updateString(firstGirl, lastGirl, age, "Jente"), connection);
                    command.ExecuteReader();
                }

                //queryString = firstString + Convert.ToInt32(row.Cells[2].Value) + " WHERE Alder = " + i + 8 + " AND Kjønn = 'Gutt'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(updateString(firstBoy, lastBoy, age, "Gutt"), connection);
                    command.ExecuteReader();
                }
            }
            this.Close();
        }


        private string increasingString(int increasing, int age, string gender)
        {
            return "UPDATE dbo.Startnummere SET StigendeRekkefølge = " + increasing.ToString() +
                " WHERE Alder = " + age.ToString() +
                " AND Kjønn = '" + gender + "'";
        }

        private string updateString(int first, int last, int age, string gender)
        {
            return "UPDATE dbo.Startnummere SET FørsteStartNummer = " + first.ToString() +
                ", SisteStartNummer = " + last.ToString() +
                " WHERE Alder = " + age.ToString() +
                " AND Kjønn = '" + gender + "'";
        }



        private void RekkefølgeBtn_Click(object sender, EventArgs e)
        {

            DataGridViewRow row;
            string connectionString = Properties.Settings.Default.ConnectionString;
            int increasing;
            Random rnd = new Random();
            for (int i = 0; i <= 9; i++)
            {
                row = (DataGridViewRow)StartNummerGV.Rows[i];
               increasing = rnd.Next(0, 2);
                if (increasing == 0)
                {
                    row.Cells[2].Value = "Synkende";
                }
                else
                {
                    row.Cells[2].Value = "Stigende";
                }
               using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(increasingString(increasing, i+8, "Jente"), connection);
                    command.ExecuteReader();
                }

               increasing = rnd.Next(0, 2);
               if (increasing == 0)
               {
                   row.Cells[4].Value = "Synkende";
               }
               else
               {
                   row.Cells[4].Value = "Stigende";
               }
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(increasingString(increasing, i + 8, "Gutt"), connection);
                    command.ExecuteReader();
                }
            }
        }

    }

}
