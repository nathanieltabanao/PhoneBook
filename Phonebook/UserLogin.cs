using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;



namespace Phonebook
{
    public partial class UserLogin : Form
    {
        public UserLogin()
        {
            InitializeComponent();
        }

        DataClasses1DataContext db = new DataClasses1DataContext();
        Has a = new Has();

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //    SqlConnection con = new SqlConnection(@"Data Source=NATHANIEL;Initial Catalog=GONZALES_LIMANA_ESCONDE_TABANAO;Integrated Security=True"); // making connection   
            //    SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM tblUser_registration WHERE username='" + txtUsername.Text + "' AND password='" + txtPassword.Text + "'", con);
            //    /* in above line the program is selecting the whole data from table and the matching it with the user name and password provided by user. */
            //    DataTable dt = new DataTable(); //this is creating a virtual table  
            //    sda.Fill(dt);



            //    if (dt.Rows[0][0].ToString() == "1")
            //    {
            //        this.Hide();
            //        Form1 a = new Form1();
            //        a.Show();
            //    }
            //    else
            //    {
            //        MessageBox.Show("The username or password is incorrect", "Incorrect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        //LoginAttempts++;
            //        //if (LoginAttempts > 3)
            //        //{
            //        //    lnkLblForget.Text = "Forget Password?";
            //        //}
            //    }

            int result;
            result = db.sp_login(txtUsername.Text, a.HashPass(txtPassword.Text)).Count();

            if (result==0)
            {

            }
            else
            {
                Form1 n = new Form1();
                this.Hide();
                n.Show();
            }
        }

        private void UserLogin_Load(object sender, EventArgs e)
        {
            //int result;
            //result = db.sp_login(txtUsername.Text, a.HashPass(txtPassword.Text)).Count();

            //if (result==0)
            //{
            //    Form1 l = new Form1();
                
               
            //}
        }

        
    }

    
}
