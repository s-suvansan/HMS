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
    public partial class BillingScreen : Form
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

        private string selectedIndex = "";
        public BillingScreen()
        {
            clearData();
            selectedIndex = "";

            InitializeComponent();
            GetPatients();
            GetData();
            if (ListID.Count > 0)
            {
                updateDatagrid();
            }
        }

        private void clearData()
        {
            ListPatientID.Clear();
            ListPatientName.Clear();
            ListID.Clear();
            ListPatId.Clear();
            ListStatus.Clear();
            ListDate.Clear();
            ListAmount.Clear();
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
                string query = "SELECT  BillingID, PatientID, BillAmount, BillingDate, PaymentStatus FROM hmsdb.billing;";

                MySqlDataReader row;
                row = con.ExecuteReader(query);
                if (row.HasRows)
                {
                    while (row.Read())
                    {
                        ListID.Add(row["BillingID"].ToString());
                        ListAmount.Add(row["BillAmount"].ToString());
                        ListPatId.Add(row["PatientID"].ToString());
                        ListDate.Add(row["BillingDate"].ToString());
                        ListStatus.Add(row["PaymentStatus"].ToString());


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
            dataGridView2.Rows.Clear();
            for (int i = 0; i < ListID.Count; i++)
            {
                DataGridViewRow newRow = new DataGridViewRow();

                var pateientNameIndex = ListPatientID.IndexOf(ListPatId[i]);
                var patientName = ListPatientName[pateientNameIndex];

                newRow.CreateCells(dataGridView2);
                newRow.Cells[0].Value = patientName;
                newRow.Cells[1].Value = ListAmount[i];
                newRow.Cells[2].Value = dateFormater((String)ListDate[i]);
                newRow.Cells[3].Value = ListStatus[i];


                dataGridView2.Rows.Add(newRow);
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
