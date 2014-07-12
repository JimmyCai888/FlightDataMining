using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace HangBan
{
    static class Program
    {
        public static String CHECK_PROGRAM_EXIT_KEY = "CheckProgramExit";
        public static String CHECK_PROGRAM_TASK_KEY = "CheckProgramTask";
        public static String CHECK_PROGRAM_OTHER_KEY = "CheckProgramOther";
        public static String CHECK_NOT_RETRY_PROMPT = "CheckNotRetryPrompt";

        public static INIFileManager mINIFileManager = new INIFileManager();
        public static String PROGRAM_PATH = "";
        public static String INI_FILE_PATH = "";
        public static String INIFILE_NAME = "hangban.ini";
        public static String INI_SQLTAB = "MySQL";
        public static String INI_SCRATCHTAB = "Scratch";

        /// <summary>
        /// MySQL Server Settings
        /// </summary>
        public static String MySQL_ADDR = "";
        public static String MySQL_USER = "";
        public static String MySQL_PWD = "";
        public static String MySQL_DBNAME = "";
        public static String MySQL_TBLNAME = "";

        /// <summary>
        /// Auto Scratch Settings
        /// </summary>
        public static bool Scratch_Auto = true;
        public static int Scratch_Interval = 12;

        public static bool isShowFlightMan = false;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!LicenseService.CheckTrialVersion())
            {
                MessageBox.Show("系统到期了，已使用了30天！", "温馨提示");
                Application.Exit();
                return;
            }

            Program.PROGRAM_PATH = Application.StartupPath; //can use instead of this: System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0])
            Directory.SetCurrentDirectory(Program.PROGRAM_PATH);
            Program.INI_FILE_PATH = Program.PROGRAM_PATH + "\\" + Program.INIFILE_NAME;
            try
            {
                Program.MySQL_ADDR = Program.mINIFileManager.GetINIValue(Program.INI_SQLTAB, "ADDR", Program.INI_FILE_PATH);
                Program.MySQL_USER = Program.mINIFileManager.GetINIValue(Program.INI_SQLTAB, "USER", Program.INI_FILE_PATH);
                Program.MySQL_PWD = Program.mINIFileManager.GetINIValue(Program.INI_SQLTAB, "PWD", Program.INI_FILE_PATH);
                Program.MySQL_DBNAME = Program.mINIFileManager.GetINIValue(Program.INI_SQLTAB, "DBNAME", Program.INI_FILE_PATH);
                Program.MySQL_TBLNAME = Program.mINIFileManager.GetINIValue(Program.INI_SQLTAB, "TBLNAME", Program.INI_FILE_PATH);
                Program.Scratch_Auto = Program.mINIFileManager.GetINIValue(Program.INI_SCRATCHTAB, "IsAutoScratch", Program.INI_FILE_PATH) == "1" ? true : false;
                Program.Scratch_Interval = int.Parse(Program.mINIFileManager.GetINIValue(Program.INI_SCRATCHTAB, "ScratchInterval", Program.INI_FILE_PATH));
                Program.isShowFlightMan = Program.mINIFileManager.GetINIValue(Program.INI_SCRATCHTAB, "ShowFlightMan", Program.INI_FILE_PATH) == "1" ? true : false;
            }
            catch (System.Exception ex)
            {

            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SingleApplication.Run(new frmMain());
        }
    }
}
