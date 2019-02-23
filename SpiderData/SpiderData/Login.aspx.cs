using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpiderData
{
    public partial class Login : System.Web.UI.Page
    {
        private string connectionString = "Data Source=140.143.230.185;Initial Catalog=SpiderData;Persist Security Info=True;User ID=sa;Password=huang@123456";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            bool Authenticated = false;
            Authenticated = ValidateLogin(Login1.UserName, Login1.Password);
            e.Authenticated = Authenticated;
            if (Authenticated == true)
            {
                Response.Redirect("Control.aspx");
            }
        }
        private bool ValidateLogin(string UserName, string Password)
        {
            bool result = false;

            SqlConnection myConnection = new SqlConnection(connectionString);
            myConnection.Open();
            string selectcommand = "select Count(*) from [user] where username='" + UserName + "' and password='" + Password + "'"; ;
            SqlCommand myCommand = new SqlCommand(selectcommand, myConnection);
            int nums = (int)myCommand.ExecuteScalar();
            if (nums > 0)
            {
                result = true;
                Session["User"] = UserName;
                selectcommand = "select admin from [user] where username='" + UserName+"'";
                myCommand = new SqlCommand(selectcommand, myConnection);
                Session["User"] = myCommand.ExecuteScalar();
            }

            return result;
        }
    }
}