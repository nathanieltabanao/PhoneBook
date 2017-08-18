using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Phonebook
{
    


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        

        DataClasses1DataContext db = new DataClasses1DataContext();
        Has aa = new Has();

        //some variables i might need latur
        DateTime date;
        string datetext;
        string gender;
        int age;

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            //initiate hashing
           

            if (rdomale.Checked)
            {
                gender = "Male";
            }
            else
            {
                gender = "Female";
            }
           db.sp_Insert(txtID.Text,lblusername.Text, aa.HashPass(txtPassword.Text), txtLastname.Text, txtFirstname.Text, txtMiddlename.Text, lbladdress.Text,int.Parse(txtage.Text), gender, lblcontact.Text, date);
            IdChange();

            dgvviewer.DataSource = db.sp_mview();
            btnConfirm.Enabled = false;
            Clear();
        }

        ErrorMessages er = new ErrorMessages();

        public void IdChange()
        {
            // for student id blank space
            if (student() == 1)
            {
                txtID.Text = "st-" + (student().ToString().PadLeft(5, '0'));
            }
            else
            {
                txtID.Text = "st-" + (student() + 1).ToString().PadLeft(5, '0');
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //display the table
            //dgvviewer.DataSource = db.sp_mview();
            Showstuff();


            // for student id blank space
            IdChange();

            //rdomale = checked;
            Clear();
        }

        //student ID generator
        public int student()
        {
            return Convert.ToInt32(db.sp_StudentID());
        }

        public void Clear()
        {
            txtAddress.Text = null;
            txtage.Text = null;
            txtContactnum.Text = null;
            txtFirstname.Text = null;
            txtID.Text = null;
            txtLastname.Text = null;
            txtMiddlename.Text = null;
            txtPassword.Text = null;
            txtSearch.Text = null;
            txtUsername.Text = null;
            //date = null;
            datetext = null;
            gender = null;
            //age = null;
            lbladdress.Text = null;
            lblage.Text = null;
            lblbirhdate.Text = null;
            lblcontact.Text = null;
            lblgender.Text = null;
            lblID.Text = null;
            lblname.Text = null;
            lblpassword.Text = null;
            lblusername.Text = null;
            btnUpdate.Enabled = false;
            txtUsername.Enabled = false;

            // for student id blank space
            IdChange();

            btnSubmit.Enabled = true;
            btnDelete.Enabled = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Showstuff()
        {
            dgvviewer.DataSource = db.sp_mview();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            age = int.Parse(txtage.Text);

            if (string.IsNullOrWhiteSpace(txtAddress.Text) || string.IsNullOrWhiteSpace(txtage.Text) || string.IsNullOrWhiteSpace(txtContactnum.Text) ||
                string.IsNullOrWhiteSpace(txtFirstname.Text) || string.IsNullOrWhiteSpace(txtLastname.Text) || string.IsNullOrWhiteSpace(txtMiddlename.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text)/* || string.IsNullOrWhiteSpace(txtUsername.Text)*/)
            {
                er.NotComplete();
            }

            else if (age<=0)
            {
                MessageBox.Show("Invalid Age");
            }
            else
            {

                date = DateTime.Parse(dtpbday.Text);
                datetext = dtpbday.Text;
                

                //gender setting
                if (rdomale.Checked)
                {
                    gender = "Male";
                }
                else
                {
                    gender = "Female";
                }

                //db.sp_Insert(txtUsername.Text, txtPassword.Text, txtLastname.Text, txtFirstname.Text, txtMiddlename.Text, txtAddress.Text, int.Parse(txtage.Text), gender, txtContactnum.Text, dtpbday.Value);
                //dgvviewer.DataSource = db.sp_view();
                
                // for student id blank space
                IdChange();

                //some subtring shat
                string username = txtLastname.Text.Substring(0) + txtFirstname.Text.Substring(0, 3);
                txtUsername.Text = username.ToString();
                MessageBox.Show("Your Username is: "+username);

                //transferring of data
                lbladdress.Text = txtAddress.Text;
                lblage.Text = txtage.Text;
                lblbirhdate.Text = datetext;
                lblcontact.Text = txtContactnum.Text;
                lblgender.Text = gender;
                lblID.Text = txtID.Text;
                lblname.Text = txtLastname.Text + ", " + txtFirstname.Text + " " + txtMiddlename.Text;
                lblpassword.Text = txtPassword.Text;
                lblusername.Text = username;

                //showing stuff
                Showstuff();

                btnConfirm.Enabled = true;
            }
        }

        private void dtpbday_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan age = DateTime.Now - dtpbday.Value;
            int Years = DateTime.Now.Year - dtpbday.Value.Year;
            if (dtpbday.Value.AddYears(Years) > DateTime.Now) Years--;
            txtage.Text = Years.ToString();

            
        }

        private void dgvviewer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dgvviewer.DataSource = db.sp_Search(txtSearch.Text);
        }

        private void dgvviewer_DoubleClick(object sender, EventArgs e)
        {
            txtUsername.Enabled = true;
            btnUpdate.Enabled = true;
            txtID.Text = dgvviewer.CurrentRow.Cells[0].Value.ToString();
            txtUsername.Text = dgvviewer.CurrentRow.Cells[1].Value.ToString();
            txtLastname.Text = dgvviewer.CurrentRow.Cells[2].Value.ToString();
            txtFirstname.Text = dgvviewer.CurrentRow.Cells[3].Value.ToString();
            txtMiddlename.Text = dgvviewer.CurrentRow.Cells[4].Value.ToString();
            txtAddress.Text = dgvviewer.CurrentRow.Cells[5].Value.ToString();
            txtage.Text = dgvviewer.CurrentRow.Cells[6].Value.ToString();
            //gendershat
            gender = dgvviewer.CurrentRow.Cells[7].Value.ToString();
            if (gender=="Male")
            {
                rdomale.Checked = true;
            }
            else
            {
                rdofmale.Checked = true;
            }
            txtContactnum.Text = dgvviewer.CurrentRow.Cells[8].Value.ToString();
            //dtpbday.Value = dgvviewer.CurrentRow.Cells[0].Value.ToString();
            btnSubmit.Enabled = false;
            btnConfirm.Enabled = false;
            btnDelete.Enabled = true;

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAddress.Text) || string.IsNullOrWhiteSpace(txtage.Text) || string.IsNullOrWhiteSpace(txtContactnum.Text) ||
                string.IsNullOrWhiteSpace(txtFirstname.Text) || string.IsNullOrWhiteSpace(txtLastname.Text) || string.IsNullOrWhiteSpace(txtMiddlename.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) || string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                er.NotComplete();
            }
            else
            {
                db.sp_update(txtID.Text, txtUsername.Text, aa.HashPass(txtPassword.Text), txtLastname.Text, txtFirstname.Text, txtMiddlename.Text, txtAddress.Text, int.Parse(txtage.Text), gender, txtContactnum.Text);
                Showstuff();
                Clear();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            db.sp_delete(txtID.Text);
            Showstuff();
            Clear();

        }
    }
}
