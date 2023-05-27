using System;
using System.IO;

namespace ApproACI.Utils
{
    public static class Helpers
    {
        public static string DEFAULT_RSV_ACCES_RIGHT = "RSV";
        public static string DEFAULT_ADMIN_ACCES_RIGHT = "ADMIN";
        public static string DEFAULT_COMPUTER_SCIENCE_ACCES_RIGHT = "COMPUTER_SCIENCE";
        public static string DEFAULT_CONTROL_AGENCY_ACCES_RIGHT = "CONTROL_AGENCY";
        public static string DEFAULT_CONTROL_PRINCIPAL_ACCES_RIGHT = "CONTROL_PRINCIPAL";
        public static string DEFAULT_HEAD_AGENCY_ACCES_RIGHT = "HEAD_OF_AGENCY";
        public static string PASSWORD_FOR_ENCRYPTION_AND_DECRYPTION = "pa@ddgterssé$ùfjword#]*";
        public static bool IS_SEND_MAIL_SCEDULED = false;



        public static string RacineFolderForRecettes = "DossierAppRecettes";
        public static string FolderNameForLogs = "Logs";



        public static void WriteLog(string strLog)
        {
            StreamWriter log;
            FileStream fileStream = null;
            DirectoryInfo logDirInfo = null;
            FileInfo logFileInfo;

            string logFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + RacineFolderForRecettes + "\\" + FolderNameForLogs + "\\";
            logFilePath = logFilePath + "Log-" + DateTime.Now.ToString("dd-MM-yyyy HH'h'mm'min'ss'sec'") + "." + "txt";
            logFileInfo = new FileInfo(logFilePath);
            logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists) logDirInfo.Create();
            if (!logFileInfo.Exists)
            {
                fileStream = logFileInfo.Create();
            }
            else
            {
                fileStream = new FileStream(logFilePath, FileMode.Append);
            }
            log = new StreamWriter(fileStream);
            log.WriteLine(strLog);
            log.Close();
        }

        public static int GetWeekNumberOfMonth(DateTime date)
        {
            date = date.Date;
            DateTime firstMonthDay = new DateTime(date.Year, date.Month, 1);
            DateTime firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            if (firstMonthMonday > date)
            {
                firstMonthDay = firstMonthDay.AddMonths(-1);
                firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            }
            return (date - firstMonthMonday).Days / 7 + 1;
        }

    }
}
