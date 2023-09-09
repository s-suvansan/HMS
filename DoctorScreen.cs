using MaterialSkin;
using MaterialSkin.Controls;
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
    public partial class DoctorScreen : Form
    {
        DBConnection con = new DBConnection();

        //arraylist to getter and setter data  
        private static ArrayList ListID = new ArrayList();
        private static ArrayList ListFirstname = new ArrayList();
        private static ArrayList ListLastname = new ArrayList();
        private static ArrayList ListContactNumber = new ArrayList();
        private static ArrayList ListAddress = new ArrayList();
        private static ArrayList ListEmail = new ArrayList();
        private static ArrayList ListGender = new ArrayList();
        private static ArrayList ListSpeciallzation = new ArrayList();
        private string selectedIndex = "";


        public DoctorScreen()
        {
            InitializeComponent();

            // Create a material theme manager and add the form to manage (this)
            GetData();
            if (ListID.Count > 0)
            {
                updateDatagrid();
            }

        }


        private void GetData()
        {
            try
            {
                con.Open();
                string query = "SELECT  DoctorID,FirstName, LastName, Gender,Specialization, ContactNumber, Email, Address FROM hmsdb.doctor;";

                //MySqlDataReader row;  
                MySqlDataReader row;
                row = con.ExecuteReader(query);
                if (row.HasRows)
                {
                    while (row.Read())
                    {
                        ListID.Add(row["DoctorID"].ToString());

                        ListFirstname.Add(row["FirstName"].ToString());
                        ListLastname.Add(row["LastName"].ToString());
                        ListContactNumber.Add(row["ContactNumber"].ToString());
                        ListAddress.Add(row["Address"].ToString());
                        ListEmail.Add(row["Email"].ToString());
                        ListSpeciallzation.Add(row["Specialization"].ToString());
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

        private void updateDatagrid()
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i < ListID.Count; i++)
            {
                DataGridViewRow newRow = new DataGridViewRow();

                newRow.CreateCells(dataGridView1);
                newRow.Cells[0].Value = ListFirstname[i];
                newRow.Cells[1].Value = ListLastname[i];
                newRow.Cells[2].Value = ListAddress[i];
                newRow.Cells[3].Value = ListContactNumber[i];
                newRow.Cells[4].Value = ListEmail[i];
                newRow.Cells[5].Value = ListGender[i];
                newRow.Cells[6].Value = ListSpeciallzation[i];


                dataGridView1.Rows.Add(newRow);
            }
        }

        private void clearData()
        {
            ListID.Clear();

            ListFirstname.Clear();
            ListLastname.Clear();
            ListContactNumber.Clear();
            ListAddress.Clear();
            ListEmail.Clear();
            ListSpeciallzation.Clear();
            ListGender.Clear();
        }

        private void clearTextbox()
        {
            selectedIndex = "";
            fn_box.Clear();
            ln_box.Clear();
            address_box.Clear();
            phn_box.Clear();
            mail_box.Clear();
            gender_box.Text = "";
            spec_box.Clear();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (ListID.Count > e.RowIndex)
            {
                selectedIndex = ListID[e.RowIndex].ToString();
                fn_box.Text = ListFirstname[e.RowIndex].ToString();
                ln_box.Text = ListLastname[e.RowIndex].ToString();
                address_box.Text = ListAddress[e.RowIndex].ToString();
                phn_box.Text = ListContactNumber[e.RowIndex].ToString();
                mail_box.Text = ListEmail[e.RowIndex].ToString();
                gender_box.Text = ListGender[e.RowIndex].ToString();
                spec_box.Text = ListSpeciallzation[e.RowIndex].ToString();

                
            }
            else
            {
                selectedIndex = "";
                clearTextbox();
            }


        }

        private void saveData()
        {
            try
            {
                con.Open();
                string saveQry = "INSERT INTO Doctor (FirstName, LastName, Gender,Specialization, ContactNumber, Email, Address)VALUES('"+ fn_box.Text +"','"+ ln_box.Text + "','"+ gender_box.Text + "','" + spec_box.Text + "','" + phn_box.Text + "','" + mail_box.Text + "','"+ address_box.Text +"');";
                int affected = con.ExecuteNonQuery(saveQry);
                con.Close();
                if (affected != -1)
                {
                    clearTextbox();
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
                string updateQuery = "UPDATE Doctor SET " +
                    "FirstName = '" + fn_box.Text + "', " +
                    "LastName = '" + ln_box.Text + "', " +
                    "Gender = '" + gender_box.Text + "', " +
                    "Specialization = '" + spec_box.Text + "', " +
                    "ContactNumber = '" + phn_box.Text + "', " +
                    "Email = '" + mail_box.Text + "', " +
                    "Address = '" + address_box.Text + "' " +
                    "WHERE DoctorID = " + selectedIndex + ";";
                int affected = con.ExecuteNonQuery(updateQuery);
                con.Close();
                if (affected != -1)
                {
                    clearTextbox();
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
                string dltQry = "DELETE FROM Doctor WHERE DoctorID = '" + selectedIndex + "';";

                int affected = con.ExecuteNonQuery(dltQry);
                con.Close();
                if (affected != -1)
                {
                    clearTextbox();
                    GetData();
                }

            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (fn_box.Text != "" && ln_box.Text != "" && address_box.Text != "" && phn_box.Text != "" && mail_box.Text != "" && gender_box.Text != "" && spec_box.Text != "")
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
