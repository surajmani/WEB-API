using ivrdating.ClassFile;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace ivrdating.Helper
{
    public class DbFuction
    {
        public string Validate_Incoming_Group_Prefix(string Function, string Group_Prefix)
        {

            string Group_Id = "0";
            if ((Function == "set_misc") || (Function == "read_misc") || (Function == "insert_login_log") || (Function == "update_login_log") || (Function == "getchargeamount") || (Function == "set_primary_apiserver") || (Function == "check_geo_location") || (Function == "get_node3_accesspoint_ip"))
            {
                Group_Id = "0";
            }
            else
            {
                string SQLQry830 = "SELECT Grp_Id FROM group_association WHERE Grp_Prefix = @Group_Prefix LIMIT 1";
                DataTable dt = RetrieveDataFromDB(SQLQry830, new MySqlParameter[] { new MySqlParameter("@Group_Prefix", Group_Prefix) });
                Group_Id = dt.Rows.Count > 0 ? dt.Rows[0]["Grp_Id"].ToString() : Convert.ToString(Errors.Unknown_Group_Prefix);
            }

            return Group_Id;
        }
        public string IsValidRequest(string AuthKey, string WS_UserName, string WS_Password)
        {
            string SQLQry1 = "SELECT Id, AuthKey, IP_Address FROM ws_agent ";
            SQLQry1 += "WHERE WS_UserName = @WS_UserName AND WS_Password = @WS_Password AND ";
            SQLQry1 += "IP_Address = '" + GetIp() + "' LIMIT 1";

            DataTable dt = RetrieveDataFromDB(SQLQry1, new MySqlParameter[] { new MySqlParameter("@WS_UserName", WS_UserName), new MySqlParameter("@WS_Password", WS_Password) });


            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["AuthKey"].ToString() != AuthKey)
                {
                    return Errors.Invalid_Authorization_Key.ToString();
                }
            }
            else
            {
                return Errors.WS_UserName_credentials_not_matching.ToString();
            }

            return "OK";
        }

        private MySqlConnection OpenConnection()
        {
            //string server = "localhost";
            //string database = "ivrdating";
            //string uid = "root";
            //string password = "";
            string connectionString;
            connectionString = ConfigurationManager.AppSettings["constr"].ToString();  //"SERVER=" + server + ";" + "DATABASE=" +
            //database + ";Convert Zero Datetime=True;" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            MySqlConnection _MySqlConnection = new MySqlConnection(connectionString);
            return _MySqlConnection;
        }

        public DataTable RetrieveDataFromDB(string commandtext, MySqlParameter[] _MySqlParameter = null)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(commandtext, con);
                if (_MySqlParameter != null)
                {
                    cmd.Parameters.AddRange(_MySqlParameter);
                }
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                adp.Fill(dt);
            }
            return dt;
        }

        private string GetIp()
        {
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return "127.0.0.1";
        }
    }
}