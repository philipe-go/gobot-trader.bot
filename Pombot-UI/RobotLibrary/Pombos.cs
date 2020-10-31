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
        private static List<string> adminUsers = new List<string> {"philipeng"};
        internal static string updateURL = "";
        internal static string appversion = "";
        internal static PomBot loginForm;
        internal static Register registerForm;
        private static SqlConnection dbconnect;
        private static string path = "Data Source=pombosaws.cexwfry315ii.ca-central-1.rds.amazonaws.com;Initial Catalog=usersDB;Persist Security Info=True;User ID=philipeng;Password=philgo1419";
        #endregion

        static Pombos()
        {

        }

        internal static void ConnectPombosDB()
        {
            try
            {
                dbconnect = new SqlConnection(path);
                dbconnect.Open();
            }
            catch (SqlException)
            {
                loginForm.WrongUser("404, TRY AGAIN LATER");
            }
        }

        internal static void DisconnectPombosDB()
        {
            dbconnect.Close();
        }

        internal static bool CheckUser(string user, string pass)
        {
            string query = "SELECT * FROM loginTable WHERE UserName = '" + user.Trim() + "'AND Password = '" + pass.Trim() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, dbconnect);
            DataTable tbl = new DataTable();
            sda.Fill(tbl);
            DisconnectPombosDB();

            if (tbl.Rows.Count == 1)
            {
                LogUser();
                return true;
            }
            else return false;
        }

        internal static void AddUser()
        {
            using (dbconnect = new SqlConnection(path))
            {
                ConnectPombosDB();
                SqlCommand cmd = new SqlCommand("UserAdd", dbconnect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserFullName", registerForm.nameTB.Text.Trim());
                cmd.Parameters.AddWithValue("@Username", registerForm.userTB.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", registerForm.mailTB.Text.Trim());
                cmd.Parameters.AddWithValue("@Password", registerForm.passwordTB.Text);
                cmd.ExecuteNonQuery();
                registerForm.FeedbackTxt("Registration Request Sent", Color.Lime);
                DisconnectPombosDB();
            }
        }

        private static void LogUser()
        {
            bool isAdmin = false;
            foreach (string name in adminUsers)
            {
                isAdmin = (name == loginForm.UserNameTB.Text) ? true : false;
                if (isAdmin == true) break;
            }
            if (!isAdmin)
            {
                using (dbconnect = new SqlConnection(path))
                {
                    ConnectPombosDB();
                    SqlCommand cmd = new SqlCommand("LogUser", dbconnect);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", loginForm.UserNameTB.Text);
                    cmd.Parameters.AddWithValue("@Datalogged", DateTime.Now.ToString("@ dd/MMM/yy HH:mm").Trim()+$" - App Version:{Program.appVersion.ToString()}");
                    cmd.Parameters.AddWithValue("@loggedIP", GetIP());
                    cmd.ExecuteNonQuery();
                }
            }
            DisconnectPombosDB();
        }

        private static string GetIP()
        {
            string url = "http://checkip.dyndns.org";
            System.Net.WebRequest req = System.Net.WebRequest.Create(url);
            System.Net.WebResponse resp = req.GetResponse();
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            string response = sr.ReadToEnd().Trim();
            string[] a = response.Split(':');
            string a2 = a[1].Substring(1);
            string[] a3 = a2.Split('<');
            string a4 = a3[0];
            return a4;
        }

        internal static bool CheckUpdate()
        {
            ConnectPombosDB();
            string query = "SELECT * FROM updateTable";
            SqlDataAdapter sda = new SqlDataAdapter(query, dbconnect);
            DataTable tbl = new DataTable();
            sda.Fill(tbl);
            
            if (tbl.Rows.Count == 1)
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM updateTable WHERE controlID = 1", dbconnect);
                cmd.CommandType = CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    appversion = reader["updateVersion"].ToString();
                    updateURL = reader["updateURL"].ToString();
                }
            }
            DisconnectPombosDB();
            if (appversion != Program.appVersion.ToString()) return true;
            return false;
        }
    }
}
