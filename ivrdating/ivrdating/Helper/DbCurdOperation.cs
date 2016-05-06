using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace ivrdating.Helper
{
    public class DbCurdOperation
    {
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
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
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

        public string UpdateDB(string commandtext, MySqlParameter[] _MySqlParameter = null)
        {
            string rtn = "Error";
            using (MySqlConnection con = OpenConnection())
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                MySqlCommand cmd = new MySqlCommand(commandtext, con);
                if (_MySqlParameter != null)
                {
                    cmd.Parameters.AddRange(_MySqlParameter);
                }
                if (cmd.ExecuteNonQuery() > 0)
                {
                    rtn = "OK";
                }
            }
                return rtn;

        }
    }
}