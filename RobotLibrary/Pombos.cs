using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Net.Sockets;
using System.Net;

namespace Pombot_UI.RobotLibrary
{
    internal class Pombos
    {
        #region attributes
        private static List<string> adminUsers = new List<string> {"admin"};
        internal static string updateURL = "";
        internal static string appversion = "";
        internal static PomBot loginForm;
        internal static Register registerForm;
        private static SqlConnection dbconnect;
        private static string path = /*Insert your remote database login and credentials here -> */ " ";
        #endregion

        static Pombos() { }

        //***UNCOMMENT here when inserting the database link on the class Pombos ***//
        //internal static void ConnectPombosDB()
        //{
        //    try
        //    {
        //        dbconnect = new SqlConnection(path);
        //        dbconnect.Open();
        //    }
        //    catch (SqlException)
        //    {
        //        loginForm.WrongUser("404, TRY AGAIN LATER");
        //    }
        //}

        internal static void DisconnectPombosDB()
        {
            dbconnect.Close();
        }

        internal static bool CheckUser(string user, string pass)
        {
            //***UNCOMMENT here when inserting the database link on the class Pombos ***//
            //string query = "SELECT * FROM <<tablename>> WHERE <<username>> = '" + user.Trim() + "'AND Password = '" + pass.Trim() + "'";
            //SqlDataAdapter sda = new SqlDataAdapter(query, dbconnect);
            //DataTable tbl = new DataTable();
            //sda.Fill(tbl);
            //DisconnectPombosDB();

            //if (tbl.Rows.Count == 1)
            //{
            //    LogUser();
            //    return true;
            //}
            //else return false;

            //***DELETE HERE when inserting the database link on the class Pombos ***//
            return user == "admin" && pass == "admin" ? true : false;
        }

        
        internal static void AddUser()
        {
            //***UNCOMMENT here when inserting the database link on the class Pombos ***//
            //using (dbconnect = new SqlConnection(path))
            //{
            //    ConnectPombosDB();
            //    SqlCommand cmd = new SqlCommand("UserAdd", dbconnect);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.AddWithValue("@UserFullName", registerForm.nameTB.Text.Trim());
            //    cmd.Parameters.AddWithValue("@Username", registerForm.userTB.Text.Trim());
            //    cmd.Parameters.AddWithValue("@Email", registerForm.mailTB.Text.Trim());
            //    cmd.Parameters.AddWithValue("@Password", registerForm.passwordTB.Text);
            //    cmd.ExecuteNonQuery();
            //    registerForm.FeedbackTxt("Registration Request Sent", Color.Lime);
            //    DisconnectPombosDB();
            //}
        }

        private static void LogUser()
        {
            //***UNCOMMENT here when inserting the database link on the class Pombos ***//
            //bool isAdmin = false;
            //foreach (string name in adminUsers)
            //{
            //    isAdmin = (name == loginForm.UserNameTB.Text) ? true : false;
            //    if (isAdmin == true) break;
            //}
            //if (!isAdmin)
            //{
            //    using (dbconnect = new SqlConnection(path))
            //    {
            //        ConnectPombosDB();
            //        SqlCommand cmd = new SqlCommand("LogUser", dbconnect);
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.Parameters.AddWithValue("@Username", loginForm.UserNameTB.Text);
            //        cmd.Parameters.AddWithValue("@Datalogged", DateTime.Now.ToString("@ dd/MMM/yy HH:mm").Trim() + $" - App Version:{Program.appVersion.ToString()}");
            //        cmd.ExecuteNonQuery();
            //    }
            //}
            //DisconnectPombosDB();
        }

        internal static bool CheckUpdate()
        {
            //***UNCOMMENT here when inserting the database link on the class Pombos ***//
            //ConnectPombosDB();
            //string query = "SELECT * FROM <<tablename>>";
            //SqlDataAdapter sda = new SqlDataAdapter(query, dbconnect);
            //DataTable tbl = new DataTable();
            //sda.Fill(tbl);

            //if (tbl.Rows.Count == 1)
            //{
            //    SqlCommand cmd = new SqlCommand("SELECT * FROM <<tablename>> WHERE <<controlID>> = 1", dbconnect);
            //    cmd.CommandType = CommandType.Text;
            //    SqlDataReader reader = cmd.ExecuteReader();
            //    while (reader.Read())
            //    {
            //        appversion = reader["updateVersion"].ToString();
            //        updateURL = reader["updateURL"].ToString();
            //    }
            //}
            //DisconnectPombosDB();
            if (appversion != Program.appVersion.ToString()) return true;
            return false;
        }
    }
}
