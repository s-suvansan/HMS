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
    public partial class UserScreen : Form
    {
        DBConnection con = new DBConnection();
        private static ArrayList ListID = new ArrayList();
        private static ArrayList ListFirstname = new ArrayList();
        private static ArrayList ListLastname = new ArrayList();
        private static ArrayList ListUsername = new ArrayList();
        private static ArrayList ListRole = new ArrayList();
        private static ArrayList ListPwd = new ArrayList();

        private string selectedIndex = "";
        private string selectedPwd = "";


        public UserScreen()
        {
            clearData();
            selectedIndex = "";
            selectedPwd = "";
            InitializeComponent();
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
                string query = "SELECT  StaffID, Username,Password,  FirstName, LastName, Role FROM hmsdb.staff;";

                MySqlDataReader row;
                row = con.ExecuteReader(query);
                if (row.HasRows)
                {
                    while (row.Read())
                    {
                        ListID.Add(row["StaffID"].ToString());

                        ListFirstname.Add(row["FirstName"].ToString());
                        ListLastname.Add(row["LastName"].ToString());
                        ListUsername.Add(row["Username"].ToString());
                        ListRole.Add(row["Role"].ToString());
                        ListPwd.Add(row["Password"].ToString());
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
                newRow.Cells[2].Value = ListUsername[i];
                newRow.Cells[3].Value = ListRole[i];


                dataGridView1.Rows.Add(newRow);
            }
        }


        private void clearData()
        {
            ListID.Clear();

            ListFirstname.Clear();
            ListLastname.Clear();
            ListUsername.Clear();
            ListRole.Clear();
            ListPwd.Clear();
        }

        private void clearTextbox()
        {
            selectedIndex = "";
            fn_box.Clear();
            ln_box.Clear();
            user_box.Clear();
            role_box.Text = "";
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (ListID.Count > e.RowIndex)
            {
                selectedIndex = ListID[e.RowIndex].ToString();
                fn_box.Text = ListFirstname[e.RowIndex].ToString();
                ln_box.Text = ListLastname[e.RowIndex].ToString();
                user_box.Text = ListUsername[e.RowIndex].ToString();
                role_box.Text = ListRole[e.RowIndex].ToString();
                selectedPwd = ListPwd[e.RowIndex].ToString();

            }
            else
            {
                selectedIndex = "";
                selectedPwd = "";
                clearTextbox();
            }
        }


        
        private void updateSingleData()
        {
            try
            {
                con.Open();
                string updateQuery = "UPDATE Staff SET " +
                    "FirstName = '" + fn_box.Text + "', " +
                    "LastName = '" + ln_box.Text + "', " +
                    "Username = '" + user_box.Text + "', " +
                    "Password = '" + selectedPwd + "', " +
                    "Role = '" + role_box.Text + "'" +
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
                string dltQry = "DELETE FROM Staff WHERE StaffID = '" + selectedIndex + "';";

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

        private void edit_Click(object sender, EventArgs e)
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

        private void dalete_Click(object sender, EventArgs e)
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

        private void clear_Click(object sender, EventArgs e)
        {
            clearTextbox();

        }
    }
}
