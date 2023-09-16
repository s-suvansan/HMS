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
using System.Windows.Controls;
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
        private static ArrayList ListDocId = new ArrayList();
        private static ArrayList ListPatId = new ArrayList();
        private static ArrayList ListStatus = new ArrayList();
        private static ArrayList ListDate = new ArrayList();

        private string selectedIndex = "";

        public AppointmentScreen()
        {
            clearData();
            selectedIndex = "";
            
            InitializeComponent();
            GetPatients();
            GetDoctors();
            GetData();
            if (ListID.Count > 0)
            {
                updateDatagrid();
            }
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
                        string name = row["FirstName"].ToString() + " " + row["LastName"].ToString();
                        patient_box.Items.Add(name);;

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

                        ListDoctorName.Add(row["FirstName"].ToString() + " " + row["LastName"].ToString());
                        string name = row["FirstName"].ToString() + " " + row["LastName"].ToString();
                        doc_box.Items.Add(name);

                    }
                    //doc_box.DataSource = ListDoctorName;
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

                        ListPatId.Add(row["PatientID"].ToString());
                        ListDocId.Add(row["DoctorID"].ToString());
                        ListDate.Add(row["AppointmentDate"].ToString());
                        ListStatus.Add(row["Status"].ToString());


                    }
                }
                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }

        private void updateDatagrid()
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i < ListID.Count; i++)
            {
                DataGridViewRow newRow = new DataGridViewRow();
                var doctorNameIndex = ListDoctorID.IndexOf(ListDocId[i]);
                var doctorName = ListDoctorName[doctorNameIndex];
                var pateientNameIndex = ListPatientID.IndexOf(ListPatId[i]);
                var patientName = ListPatientName[pateientNameIndex];

                newRow.CreateCells(dataGridView1);
                newRow.Cells[0].Value = doctorName;
                newRow.Cells[1].Value = patientName;
                newRow.Cells[2].Value = dateFormater((String)ListDate[i]);
                newRow.Cells[3].Value = ListStatus[i];


                dataGridView1.Rows.Add(newRow);
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

        private void clearData()
        {
            ListDoctorID.Clear();
            ListPatientID.Clear();
            ListDoctorName.Clear();
            ListPatientName.Clear();
            ListID.Clear();
            ListDocId.Clear();
            ListPatId.Clear();
            ListStatus.Clear();
            ListDate.Clear();
        }

        private void clearTextbox()
        {
            selectedIndex = "";
            doc_box.Text = "";
            patient_box.Text = "";
            status_box.Text = "";
            selectedPatientIndex = "";
            selectedDoctorIndex = "";


            appoint_date_box.Value = DateTime.Now;
        }

        private void saveData()
        {
            try
            {
                con.Open();
                string saveQry = "INSERT INTO hmsdb.appointment (PatientID, DoctorID, AppointmentDate, Status)VALUES('" + selectedPatientIndex + "','" + selectedDoctorIndex + "','" +  dateFormater(appoint_date_box.Text) + "','" + status_box.Text + "');";
                int affected = con.ExecuteNonQuery(saveQry);
                con.Close();
                if (affected != -1)
                {
                    clearTextbox();
                    GetPatients();
                    GetDoctors();
                    GetData();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        private void updateSingleData()
        {
            try
            {
                con.Open();
                string updateQuery = "UPDATE hmsdb.appointment SET " +
                    "PatientID = '" + selectedPatientIndex + "', " +
                    "DoctorID = '" + selectedDoctorIndex + "', " +
                    "AppointmentDate = '" + dateFormater(appoint_date_box.Text) + "', " +
                    "Status = '" + status_box.Text + "' " +
                    "WHERE AppointmentID = " + selectedIndex + ";";
                int affected = con.ExecuteNonQuery(updateQuery);
                con.Close();
                if (affected != -1)
                {
                    clearTextbox();
                    GetPatients();
                    GetDoctors();
                    GetData();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }


        private void deleteSingleData()
        {
            try
            {

                con.Open();
                string dltQry = "DELETE FROM hmsdb.appointment WHERE AppointmentID = '" + selectedIndex + "';";

                int affected = con.ExecuteNonQuery(dltQry);
                con.Close();
                if (affected != -1)
                {
                    clearTextbox();
                    GetPatients();
                    GetDoctors();
                    GetData();
                }

            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }


        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (ListID.Count > e.RowIndex)
            {
                var doctorNameIndex = ListDoctorID.IndexOf(ListDocId[e.RowIndex]);
                selectedDoctorIndex = ListDocId[e.RowIndex].ToString();
                var doctorName = ListDoctorName[doctorNameIndex];
                var pateientNameIndex = ListPatientID.IndexOf(ListPatId[e.RowIndex]);
                selectedPatientIndex = ListPatId[e.RowIndex].ToString();
                var patientName = ListPatientName[pateientNameIndex];
                selectedIndex = ListID[e.RowIndex].ToString();
                doc_box.Text = (string)doctorName;
                patient_box.Text = (string)patientName;
                status_box.Text = ListStatus[e.RowIndex].ToString();
                appoint_date_box.Text = ListDate[e.RowIndex].ToString();


            }
            else
            {
                selectedIndex = "";
                clearTextbox();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (doc_box.Text != "" && patient_box.Text != "" && status_box.Text != "" )
            {
                DialogResult result = MessageBox.Show("Do you want to add ?", "Save...", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    clearData();
                    saveData();
                    updateDatagrid();
                }
            }
            else
            {
                MessageBox.Show("Please enter the data...");
            }
        }

        string selectedDoctorIndex;
        string selectedPatientIndex;


        private void doc_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedDoctorIndex = ListDoctorID[doc_box.SelectedIndex].ToString();
        }

        private void patient_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedPatientIndex = ListPatientID[patient_box.SelectedIndex].ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (selectedIndex != "")
            {
                DialogResult result = MessageBox.Show("Do you want to update ?", "Edit...", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    clearData();
                    updateSingleData();
                    updateDatagrid();
                }

            }
            else
            {
                MessageBox.Show("Please setect a data row first..");

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (selectedIndex != "")
            {
                DialogResult result = MessageBox.Show("Do you want to delete ?", "Delete...", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    clearData();
                    deleteSingleData();
                    updateDatagrid();
                }

            }
            else
            {
                MessageBox.Show("Please setect a data row first..");

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clearTextbox();

        }
    }
}
