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
    public partial class AppointmentScreen : Form
    {
        DBConnection con = new DBConnection();

        //arraylist to getter and setter data  
        private static ArrayList ListPatientID = new ArrayList();
        private static ArrayList ListPatientName = new ArrayList();

        private static ArrayList ListDoctorID = new ArrayList();
        private static ArrayList ListDoctorName = new ArrayList();

        private static ArrayList ListID = new ArrayList();
        private static ArrayList ListFirstname = new ArrayList();
        private static ArrayList ListLastname = new ArrayList();
        private static ArrayList ListContactNumber = new ArrayList();
        private static ArrayList ListAddress = new ArrayList();
        private static ArrayList ListEmail = new ArrayList();
        private static ArrayList ListGender = new ArrayList();
        private static ArrayList ListDOB = new ArrayList();
        private string selectedIndex = "";

        public AppointmentScreen()
        {
            InitializeComponent();
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

                        ListPatientName.Add(row["FirstName"].ToString() +" " + row["LastName"].ToString());

                    }
                }
                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }

        private void GetDoctors()
        {
            try
            {
                con.Open();
                string query = "SELECT  DoctorID,FirstName, LastName FROM hmsdb.doctor;";

                //MySqlDataReader row;  
                MySqlDataReader row;
                row = con.ExecuteReader(query);
                if (row.HasRows)
                {
                    while (row.Read())
                    {
                        ListDoctorID.Add(row["DoctorID"].ToString());

                        ListDoctorName.Add(row["FirstName"].ToString() + " "+ row["LastName"].ToString());
                    }
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
                string query = "SELECT   AppointmentID, PatientID, DoctorID, AppointmentDate, Status FROM hmsdb.appointment;";

                MySqlDataReader row;
                row = con.ExecuteReader(query);
                if (row.HasRows)
                {
                    while (row.Read())
                    {
                        ListID.Add(row["AppointmentID"].ToString());

                        ListFirstname.Add(row["FirstName"].ToString());
                        ListLastname.Add(row["LastName"].ToString());
                        ListContactNumber.Add(row["ContactNumber"].ToString());
                        ListAddress.Add(row["Address"].ToString());
                        ListEmail.Add(row["Email"].ToString());
                        ListDOB.Add(row["DateOfBirth"].ToString());
                        ListGender.Add(row["Gender"].ToString());


                    }
                }
                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }
    }
}
