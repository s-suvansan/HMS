using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS
{
    public partial class LoginScreen : Form
    {
        DBConnection con = new DBConnection();
        public LoginScreen()
        {
            Thread t = new Thread(new ThreadStart(StartForm));
            t.Start();
            Thread.Sleep(15000);
            InitializeComponent();
            t.Abort();

            

        }

        public void StartForm()
        {
            Application.Run(new SplashScreen());
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterScreen register = new RegisterScreen();
            register.Show();
        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            if(username_box.Text == "" || password_box.Text == "")
            {
                MessageBox.Show("Please enter username and password.");
                return;
            }
            login();


        }

        private void login()
        {
            try
            {
                con.Open();
                string query = "SELECT StaffID, Username, FirstName, LastName, Role FROM hmsdb.staff WHERE Username = '"+ username_box.Text+"' AND Password = '"+ password_box.Text +"' ; ";

                //MySqlDataReader row;  
                MySqlDataReader row;
                row = con.ExecuteReader(query);
                if (row.HasRows)
                {
                    this.Hide();
                    DashboardScreen dashboard = new DashboardScreen(row);
                    dashboard.Show();
                }
                else
                {
                    MessageBox.Show("Unable to find the account.");
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
