using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Windows.Forms;
using System.Data;
using System.Text.RegularExpressions;

namespace HangBan
{
    public class DBManager
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        private string tbl_hblist;
        public string tbl_flightno;
        public string tbl_hbquery;

        public DBManager()
        {
            Initialize();
        }

        private void Initialize()
        {
            //////////////////////////////////////////////////////////////////////////
            tbl_flightno = "tbl_flightno";
            tbl_hbquery = "tbl_hbquery";

        }

        public string InitConnection(int test)
        {
            server = Program.MySQL_ADDR;
            database = Program.MySQL_DBNAME;
            tbl_hblist = Program.MySQL_TBLNAME;
            uid = Program.MySQL_USER;
            password = Program.MySQL_PWD;

            try
            {
                string connectionString;
                connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";Charset=utf8;";
                connection = new MySqlConnection(connectionString);

                string connrst = OpenConnection(test);

                return connrst;
            }
            catch (System.Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        //open connection to database
        private string OpenConnection(int test)
        {
            string rst = "";
            try
            {
                if (connection.State == ConnectionState.Open)
                {
                    return "";
                }

                connection.Open();
                return "";
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        rst = "Cannot connect to server.  Contact administrator";
                        break;

                    case 1045:
                        rst = "Invalid username/password, please try again";
                        break;
                    default:
                        rst = "无法连接数据库";
                        break;
                }

                if (test == 1)
                {
                    MessageBox.Show(rst);
                }

                return rst;
            }
        }

        //Close connection
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static string RemoveLineBreaks(string pstr)
        {
            string rst = Regex.Replace(pstr, @"\t|\n|\r", "");
            return rst;
        }

        public void InsertTodayList(List<HangbanInfo> hblist)
        {
            try
            {
                string insertsql = "INSERT INTO " + tbl_hblist + " (leaveplan, flightno, airlines, terminal, destination, chkincounter, bordinggate, status, createtime) " +
                    "VALUES {0};";
                string valuesql = "('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}')";

                DateTime nowtime = DateTime.Now;
                string timestr = String.Format("{0:yyyy-MM-dd HH:mm:ss}", nowtime);

                //open connection
                if (String.IsNullOrEmpty(this.OpenConnection(0)))
                {
                    DeleteTodayData();

                    StringBuilder sb = new StringBuilder();
                    foreach (var item in hblist)
                    {
                        //create command and assign the query and connection from the constructor
                        string query = String.Format(valuesql,
                            GetUTF8Str(RemoveLineBreaks(item.leaveplan.Trim())),
                            GetUTF8Str(RemoveLineBreaks(item.flightno.Trim())),
                            GetUTF8Str(RemoveLineBreaks(item.airlines.Trim())),
                            GetUTF8Str(RemoveLineBreaks(item.terminal.Trim())),
                            GetUTF8Str(RemoveLineBreaks(item.destination.Trim())),
                            GetUTF8Str(RemoveLineBreaks(item.chkincounter.Trim())),
                            GetUTF8Str(RemoveLineBreaks(item.bordinggate.Trim())),
                            GetUTF8Str(RemoveLineBreaks(item.status.Trim())),
                            timestr);

                        if (!String.IsNullOrEmpty(sb.ToString()))
                        {
                            sb.Append(", ");
                        }
                        sb.Append(query);
                    }

                    string exeSql = String.Format(insertsql, sb.ToString());

                    MySqlCommand cmd = new MySqlCommand(exeSql, connection);
                    cmd.CommandTimeout = 3000;
                    //Execute command
                    cmd.ExecuteNonQuery();
                }
            }
            catch (System.Exception ex)
            {
                ScratchModel.WriteLogFile("__________DB__________", "InsertTodayList", ex.ToString());	
            }
        }

        public void InsertTodayQueryList(List<HBQueryInfo> hblist, DateTime scratchTime)
        {
            try
            {
                string insertsql = "INSERT INTO " + tbl_hbquery + " (flightno, sourcepos, destpos, takeoffplace, arriveplace, leaveplan, arriveplan, leavetime, arrivetime, estimatedleavetime, estimatedarrivetime, gate, status, flighttime) " +
                    "VALUES {0};";
                string valuesql = "('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}')";

                string timestr = String.Format("{0:yyyy-MM-dd HH:mm:ss}", scratchTime);

                //open connection
                if (String.IsNullOrEmpty(this.OpenConnection(0)))
                {
                    DeleteQueryData(scratchTime);

                    StringBuilder sb = new StringBuilder();
                    foreach (var item in hblist)
                    {
                        //create command and assign the query and connection from the constructor
                        string query = String.Format(valuesql,
                            GetUTF8Str(item.flightno.Trim()),
                            GetUTF8Str(item.sourcepos.Trim()),
                            GetUTF8Str(item.destpos.Trim()),
                            GetUTF8Str(item.takeoffplace.Trim()),
                            GetUTF8Str(item.arriveplace.Trim()),
                            GetUTF8Str(item.leaveplan.Trim()),
                            GetUTF8Str(item.arriveplan.Trim()),
                            GetUTF8Str(item.leavetime.Trim()),
                            GetUTF8Str(item.arrivetime.Trim()),
                            GetUTF8Str(item.estimatedleavetime.Trim()),
                            GetUTF8Str(item.estimatedarrivetime.Trim()),
                            GetUTF8Str(item.gate.Trim()),
                            GetUTF8Str(item.status.Trim()),
                            timestr);

                        if (!String.IsNullOrEmpty(sb.ToString()))
                        {
                            sb.Append(", ");
                        }
                        sb.Append(query);
                    }

                    string exeSql = String.Format(insertsql, sb.ToString());

                    MySqlCommand cmd = new MySqlCommand(exeSql, connection);
                    cmd.CommandTimeout = 3000;
                    //Execute command
                    cmd.ExecuteNonQuery();

                }
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        public void InsertFlightNoList(List<string> hblist)
        {
            try
            {
                string insertsql = "INSERT INTO " + tbl_flightno + " (flightno) " +
                    "VALUES('{0}')";

                DateTime nowtime = DateTime.Now;
                string timestr = String.Format("{0:yyyy-MM-dd HH:mm:ss}", nowtime);

                //open connection
                if (String.IsNullOrEmpty(this.OpenConnection(0)))
                {
                    DeleteOldFlightNo();

                    foreach (var item in hblist)
                    {
                        //create command and assign the query and connection from the constructor
                        string query = String.Format(insertsql,
                            GetUTF8Str(item.Trim()),
                            timestr);
                        MySqlCommand cmd = new MySqlCommand(query, connection);

                        //Execute command
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        //Update statement
        public void Update()
        {
            string query = "UPDATE tableinfo SET name='Joe', age='22' WHERE name='John Smith'";

            //Open connection
            if (String.IsNullOrEmpty(this.OpenConnection(0)))
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                //this.CloseConnection();
            }
        }

        //Delete statement
        public void DeleteTodayData()
        {
            try
            {
                DateTime nowtime = DateTime.Now;
                //string query = "DELETE FROM " + tbl_hblist + " WHERE createtime >= '" + nowtime.ToString("yyyy-MM-dd 00:00:00") + "' && createtime <= '" + nowtime.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'";
                string query = "DELETE FROM " + tbl_hblist;

                if (String.IsNullOrEmpty(this.OpenConnection(0)))
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    //this.CloseConnection();
                }
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        public void DeleteQueryData(DateTime deldate)
        {
            try
            {
                DateTime nowtime = DateTime.Now;
                //string query = "DELETE FROM " + tbl_hbquery + " WHERE flighttime >= '" + deldate.ToString("yyyy-MM-dd 00:00:00") + "' && flighttime <= '" + deldate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'";
                string query = "DELETE FROM " + tbl_hbquery;

                if (String.IsNullOrEmpty(this.OpenConnection(0)))
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    //this.CloseConnection();
                }
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        public void DeleteOldFlightNo()
        {
            try
            {
                DateTime nowtime = DateTime.Now;
                string query = "DELETE FROM " + tbl_flightno;

                if (String.IsNullOrEmpty(this.OpenConnection(0)))
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    //this.CloseConnection();
                }
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        public List<string>[] SelectHBInfo()
        {
            string query = "SELECT * FROM " + tbl_hblist;

            //Create a list to store the result
            List<string>[] list = new List<string>[3];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();

            //Open connection
            if (String.IsNullOrEmpty(this.OpenConnection(0)))
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list[0].Add(dataReader["id"] + "");
                    list[1].Add(dataReader["name"] + "");
                    list[2].Add(dataReader["age"] + "");
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                //this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }

        //Count statement
        public int Count()
        {
            string query = "SELECT Count(*) FROM tableinfo";
            int Count = -1;

            //Open Connection
            if (String.IsNullOrEmpty(this.OpenConnection(0)))
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                //this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }

        public static string GetUTF8Str(string str)
        {
            System.Text.Encoding utf_8 = System.Text.Encoding.UTF8;

            //string to utf
            byte[] utf = System.Text.Encoding.UTF8.GetBytes(str);

            //utf to string
            string s2 = System.Text.Encoding.UTF8.GetString(utf);

            return s2;
        }

        public List<string> SelectFlightNoList()
        {
            string query = "SELECT * FROM " + tbl_flightno;

            //Create a list to store the result
            List<string> flightlist = new List<string>();

            //Open connection
            if (String.IsNullOrEmpty(this.OpenConnection(0)))
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    flightlist.Add(dataReader["flightno"].ToString());
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                //this.CloseConnection();

                //return list to be displayed
                return flightlist;
            }
            else
            {
                return flightlist;
            }
        }

    }
}
