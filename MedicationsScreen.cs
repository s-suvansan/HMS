using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS
{
    public partial class MedicationsScreen : Form
    {
        DBConnection con = new DBConnection();

        //arraylist to getter and setter data  
        private static ArrayList ListPatientID = new ArrayList();
        private static ArrayList ListPatientName = new ArrayList();

        private static ArrayList ListID = new ArrayList();
        private static ArrayList ListPatId = new ArrayList();
        private static ArrayList ListAmount = new ArrayList();
        private static ArrayList ListStatus = new ArrayList();
        private static ArrayList ListDate = new ArrayList();
        public MedicationsScreen()
        {
            InitializeComponent();
            GetPatients();
            GetData();
        }

        private void GetPatients()
        {
            try
            {
                con.Open();
                string query = "SELECT  PatientID, FirstName, LastName FROM hmsdb.patient;";

                MySqlDataReader row;
                row = con.ExecuteReader(query);
                if (row.HasRows)
                {
                    while (row.Read())
                    {
                        ListPatientID.Add(row["PatientID"].ToString());

                        ListPatientName.Add(row["FirstName"].ToString() + " " + row["LastName"].ToString());
                        string name = row["FirstName"].ToString() + " " + row["LastName"].ToString();
                        patient_box.Items.Add(name); ;

                    }
                    //patient_box.DataSource = ListPatientName;
                    patient_box.Text = "";

                }
                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }


        private void GetData()
        {
            try
            {
                con.Open();
                string query = "SELECT  PatientID, MedicationName, Dosage, Frequency, StartDate, EndDate  FROM hmsdb.medication;";

                MySqlDataReader row;
                row = con.ExecuteReader(query);
                if (row.HasRows)
                {
                    dataGridView1.Rows.Clear();

                    while (row.Read())
                    {
                        //ListID.Add(row["PatientID"].ToString());
                        //ListAmount.Add(row["Diagnosis"].ToString());
                        //ListPatId.Add(row["PatientID"].ToString());
                        //ListDate.Add(row["BillingDate"].ToString());
                        //ListStatus.Add(row["PaymentStatus"].ToString());

                        DataGridViewRow newRow = new DataGridViewRow();

                        var pateientNameIndex = ListPatientID.IndexOf(row["PatientID"].ToString());
                        var patientName = ListPatientName[pateientNameIndex];

                        newRow.CreateCells(dataGridView1);
                        newRow.Cells[0].Value = patientName;
                        newRow.Cells[1].Value = row["MedicationName"].ToString();
                        newRow.Cells[2].Value = row["Dosage"].ToString();
                        newRow.Cells[3].Value = row["Frequency"].ToString();
                        newRow.Cells[4].Value = dateFormater(row["StartDate"].ToString());
                        newRow.Cells[5].Value = dateFormater(row["EndDate"].ToString());




                        dataGridView1.Rows.Add(newRow);
                    }
                }
                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }


        private string dateFormater(String savedDate)
        {
            if (savedDate != null)
            {
                var date = DateTime.Parse(savedDate);
                return date.Year.ToString() + "-" + date.Month.ToString() + "-" + date.Day.ToString();
            }
            else
            {
                return "";
            }
        }

    }
}
