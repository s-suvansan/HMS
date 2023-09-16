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
    public partial class RoomScreen : Form
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
        public RoomScreen()
        {
            InitializeComponent();
            GetData();
        }

        private void GetData()
        {
            try
            {
                con.Open();
                string query = "SELECT  RoomNumber, Status, RoomType, BedCount, Floor  FROM hmsdb.room;";

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

                        
                        newRow.CreateCells(dataGridView1);
                        newRow.Cells[0].Value = row["RoomNumber"].ToString();
                        newRow.Cells[1].Value = row["Status"].ToString();
                        newRow.Cells[2].Value = row["RoomType"].ToString();
                        newRow.Cells[3].Value = row["BedCount"].ToString();
                        newRow.Cells[4].Value = row["Floor"].ToString();






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

    }

}
