using MySql.Data.MySqlClient;
using System;
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
    public partial class RegisterScreen : Form
    {
        DBConnection con = new DBConnection();

        public RegisterScreen()
        {
            InitializeComponent();
        }





        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void signup_btn_Click(object sender, EventArgs e)
        {
            if (role_box.Text == "" || fn_box.Text == "" || ln_box.Text == "" || user_box.Text == "" || pwd_box.Text == "" ||  con_pwd_box.Text == "")
            {
                MessageBox.Show("Please fill all feilds.");
                return;
            }

            if(pwd_box.Text != con_pwd_box.Text)
            {
                MessageBox.Show("Confirm password is wrong.");
                return;
            }
            check_username();

        }

        private void check_username()
        {
            try
            {
                con.Open();
                string query = "SELECT  Username FROM hmsdb.staff WHERE Username = '" + user_box.Text  + "' ; ";

                MySqlDataReader row;
                row = con.ExecuteReader(query);
                if (row.HasRows)
                {
                    
                   MessageBox.Show("Username is already taken.");
                     

                }
                else
                {
                    register();

                }
                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        private void register()
        {
            try
            {
                con.Open();
                string saveQry = "INSERT INTO hmsdb.staff ( Username, Password, FirstName, LastName, Role)VALUES('" + user_box.Text + "','" + pwd_box.Text + "','" + fn_box.Text + "','" + ln_box.Text + "','" + role_box.Text +  "');";
                int affected = con.ExecuteNonQuery(saveQry);
                con.Close();
                if (affected != -1)
                {
                    con.Open();
                    string query = "SELECT StaffID, Username, FirstName, LastName, Role FROM hmsdb.staff WHERE Username = '" + user_box.Text + "' AND Password = '" + pwd_box.Text + "' ; ";

                    //MySqlDataReader row;  
                    MySqlDataReader row;
                    row = con.ExecuteReader(query);
                    if (row.HasRows)
                    {
                        this.Close();

                        DashboardScreen dashboard = new DashboardScreen(row);
                        dashboard.Show();
                    }
                    else
                    {
                        MessageBox.Show("Unable to find the account.");
                    }
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Something went wrong...");

                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }
    }
}
