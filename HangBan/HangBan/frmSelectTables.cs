using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace HangBan
{
    public partial class frmSelectTables : Form
    {
        OleDbConnection oledbConn;
        public string excelfile = "";

        public frmSelectTables()
        {
            InitializeComponent();
        }

        public frmSelectTables(string filename)
        {
            InitializeComponent();
            excelfile = filename;
            GetColumnList();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSelectTables_Load(object sender, EventArgs e)
        {
            this.AcceptButton = btnOK;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (listColumn.Items.Count > 0)
            {
                int selindex = listColumn.SelectedItems[0].Index;
                GetExcelData(excelfile, selindex);
            }
            else
            {
                this.Close();
            }

        }

        private List<string> GetColumnList()
        {

            List<string> rst = new List<string>();
            try
            {
                // need to pass relative path after deploying on server
                /* connection string  to work with excel file. HDR=Yes - indicates 
                   that the first row contains columnnames, not data. HDR=No - indicates 
                   the opposite. "IMEX=1;" tells the driver to always read "intermixed" 
                   (numbers, dates, strings etc) data columns as text. 
                Note that this option might affect excel sheet write access negative. */

                if (Path.GetExtension(excelfile) == ".xls")
                {
                    oledbConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + excelfile + ";Extended Properties=\"Excel 8.0;HDR=No;IMEX=2\"");
                }
                else if (Path.GetExtension(excelfile) == ".xlsx")
                {
                    oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelfile + ";Extended Properties='Excel 12.0;HDR=No;IMEX=1;';");
                }
                else if (Path.GetExtension(excelfile) == ".csv")
                {
                    oledbConn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + excelfile + ";Extended Properties='text;'");
                }
                oledbConn.Open();
                var dtSchema = oledbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                string Sheet1 = dtSchema.Rows[0].Field<string>("TABLE_NAME");

                OleDbCommand cmd = new OleDbCommand();
                OleDbDataAdapter oleda = new OleDbDataAdapter();

                // passing list to drop-down list

                // selecting distict list of Slno 
                cmd.Connection = oledbConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT top 1 * FROM [" + Sheet1 + "]";
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        rst.Add(reader.GetValue(i).ToString());
                        listColumn.Items.Add(reader.GetValue(i).ToString());
                    }
                }
                reader.Close();

            }
            // need to catch possible exceptions
            catch (Exception ex)
            {
            }
            finally
            {
                oledbConn.Close();
            }

            return rst;
        }

        public List<string> GetExcelData(string path, int nIndex)
        {
            List<string> rst = new List<string>();
            try
            {
                // need to pass relative path after deploying on server
                /* connection string  to work with excel file. HDR=Yes - indicates 
                   that the first row contains columnnames, not data. HDR=No - indicates 
                   the opposite. "IMEX=1;" tells the driver to always read "intermixed" 
                   (numbers, dates, strings etc) data columns as text. 
                Note that this option might affect excel sheet write access negative. */

                if (Path.GetExtension(path) == ".xls")
                {
                    oledbConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=No;IMEX=2\"");
                }
                else if (Path.GetExtension(path) == ".xlsx")
                {
                    oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0;HDR=No;IMEX=1;';");
                }
                oledbConn.Open();
                var dtSchema = oledbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                string Sheet1 = dtSchema.Rows[0].Field<string>("TABLE_NAME");

                OleDbCommand cmd = new OleDbCommand();
                OleDbDataAdapter oleda = new OleDbDataAdapter();

                // passing list to drop-down list

                // selecting distict list of Slno 
                cmd.Connection = oledbConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [" + Sheet1 + "]";
                var reader = cmd.ExecuteReader();

                //Optional (comment by CSC)
                UCHBList.hblist.Clear();

                while (reader.Read())
                {
                    string evalue = reader.GetValue(nIndex).ToString();
                    if (!UCHBList.hblist.Contains(evalue))
                    {
                        UCHBList.hblist.Add(evalue);
                    }
                }
                reader.Close();
            }
            // need to catch possible exceptions
            catch (Exception ex)
            {
            }
            finally
            {
                oledbConn.Close();
            }

            return rst;
        }

    }
}
