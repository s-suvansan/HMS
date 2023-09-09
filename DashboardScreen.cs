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
    public partial class DashboardScreen : Form
    {
        DoctorScreen doctorScreen = new DoctorScreen() { TopLevel = false, TopMost = true, Dock = DockStyle.Fill };
        PatientScreen patientScreen = new PatientScreen() { TopLevel = false, TopMost = true, Dock = DockStyle.Fill };
        AppointmentScreen appointmentScreen = new AppointmentScreen() { TopLevel = false, TopMost = true, Dock = DockStyle.Fill };
        BillingScreen billingScreen = new BillingScreen() { TopLevel = false, TopMost = true, Dock = DockStyle.Fill };
        MedicalRecordScreen medicalRecordScreen = new MedicalRecordScreen() { TopLevel = false, TopMost = true, Dock = DockStyle.Fill };
        MedicationsScreen medicationsScreen = new MedicationsScreen() { TopLevel = false, TopMost = true, Dock = DockStyle.Fill };
        RoomScreen roomScreen = new RoomScreen() { TopLevel = false, TopMost = true, Dock = DockStyle.Fill };
        UserScreen userScreen = new UserScreen() { TopLevel = false, TopMost = true, Dock = DockStyle.Fill };

        public DashboardScreen(MySqlDataReader data)
        {
            while (data.Read())
            {
                InitializeComponent();
                name_lbl.Text = data["FirstName"].ToString() + " " + data["LastName"].ToString();
                role_lbl.Text = data["Role"].ToString();
                user_btn.Hide();
                if (role_lbl.Text == "Admin")
                {
                    user_btn.Show();
                }
            }
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            doctorScreen.FormBorderStyle = FormBorderStyle.None;

            panel1.Controls.Add(doctorScreen);
            doctorScreen.Show();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            doctorScreen.Hide();
            //patientScreen.Hide();
            appointmentScreen.Hide();
            billingScreen.Hide();
            medicalRecordScreen.Hide();
            medicationsScreen.Hide();
            roomScreen.Hide();
            userScreen.Hide();


            patientScreen.FormBorderStyle = FormBorderStyle.None;

            panel1.Controls.Add(patientScreen);
            patientScreen.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            //doctorScreen.Hide();
            patientScreen.Hide();
            appointmentScreen.Hide();
            billingScreen.Hide();
            medicalRecordScreen.Hide();
            medicationsScreen.Hide();
            roomScreen.Hide();
            userScreen.Hide();

            doctorScreen.FormBorderStyle = FormBorderStyle.None;

            panel1.Controls.Add(doctorScreen);
            doctorScreen.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want log out?", "Logout...", MessageBoxButtons.YesNo);
            if(result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            doctorScreen.Hide();
            patientScreen.Hide();
            //appointmentScreen.Hide();
            billingScreen.Hide();
            medicalRecordScreen.Hide();
            medicationsScreen.Hide();
            roomScreen.Hide();
            userScreen.Hide();

            appointmentScreen.FormBorderStyle = FormBorderStyle.None;

            panel1.Controls.Add(appointmentScreen);
            appointmentScreen.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            doctorScreen.Hide();
            patientScreen.Hide();
            appointmentScreen.Hide();
            //billingScreen.Hide();
            medicalRecordScreen.Hide();
            medicationsScreen.Hide();
            roomScreen.Hide();
            userScreen.Hide();

            billingScreen.FormBorderStyle = FormBorderStyle.None;

            panel1.Controls.Add(billingScreen);
            billingScreen.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            doctorScreen.Hide();
            patientScreen.Hide();
            appointmentScreen.Hide();
            billingScreen.Hide();
            //medicalRecordScreen.Hide();
            medicationsScreen.Hide();
            roomScreen.Hide();
            userScreen.Hide();

            medicalRecordScreen.FormBorderStyle = FormBorderStyle.None;

            panel1.Controls.Add(medicalRecordScreen);
            medicalRecordScreen.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            doctorScreen.Hide();
            patientScreen.Hide();
            appointmentScreen.Hide();
            billingScreen.Hide();
            medicalRecordScreen.Hide();
            //medicationsScreen.Hide();
            roomScreen.Hide();
            userScreen.Hide();

            medicationsScreen.FormBorderStyle = FormBorderStyle.None;

            panel1.Controls.Add(medicationsScreen);
            medicationsScreen.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            doctorScreen.Hide();
            patientScreen.Hide();
            appointmentScreen.Hide();
            billingScreen.Hide();
            medicalRecordScreen.Hide();
            medicationsScreen.Hide();
            //roomScreen.Hide();
            userScreen.Hide();

            roomScreen.FormBorderStyle = FormBorderStyle.None;

            panel1.Controls.Add(roomScreen);
            roomScreen.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            doctorScreen.Hide();
            patientScreen.Hide();
            appointmentScreen.Hide();
            billingScreen.Hide();
            medicalRecordScreen.Hide();
            medicationsScreen.Hide();
            roomScreen.Hide();

            userScreen.FormBorderStyle = FormBorderStyle.None;

            panel1.Controls.Add(userScreen);
            userScreen.Show();
        }
    }
}
