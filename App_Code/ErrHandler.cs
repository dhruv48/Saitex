using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Diagnostics;
using context = System.Web.HttpContext;

/// <summary>
/// Summary description for ErrHandler
/// </summary>
namespace errorLog
{
    public class ErrHandler
    {
        public ErrHandler()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void WriteError(string errorMessage)
        {
            try
            {
                string path = "~/Error/" + DateTime.Today.ToString("dd-MM-yyyy") + ".txt";
                if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
                {
                    File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
                }

                using (StreamWriter w = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
                {
                    w.WriteLine("\r\nLog Entry : ");
                    w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    string err = "Error in: " + System.Web.HttpContext.Current.Request.Url.ToString() +
                                  ". Error Message:" + errorMessage;
                    w.WriteLine(err);
                    w.WriteLine("__________________________");
                    w.Flush();
                    w.Close();
                }
            }
            catch (Exception ex)
            {
                WriteError(ex.Message);
            }

        }


        /// <summary> Log the error and return </summary>
        /// <param name="ee">The ee.</param>
        /// <param name="userFriendlyError">The user friendly error.</param>
        /// <returns></returns>
        public static string LogErrorToLogFile(Exception ee, string userFriendlyError)
        {

            try
            {

                string path = context.Current.Server.MapPath("~/Error/ErrorLogging/");

                // check if directory exists

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                path = path + DateTime.Today.ToString("dd-MM-yyyy") + ".log";

                // check if file exist

                if (!File.Exists(path))
                {

                    File.Create(path).Dispose();

                }

                // log the error now

                using (StreamWriter writer = File.AppendText(path))
                {

                    string error = "\r\nLog written at : " + DateTime.Now.ToString() +

                    "\r\nError occured on page : " + context.Current.Request.Url.ToString() +

                    "\r\n\r\nHere is the actual error :\n" + ee.ToString();

                    writer.WriteLine(error);

                    writer.WriteLine("==========================================");

                    writer.Flush();

                    writer.Close();

                }

                return userFriendlyError;

            }

            catch
            {

                throw;

            }

        }

        /// <summary>

        /// Logs the error in system event.

        /// </summary>

        /// <param name="ee">The ee.</param>

        /// <param name="userFriendlyError">The user friendly error.</param>

        /// <returns></returns>
        public static string LogErrorInSystemEvent(Exception ee, string userFriendlyError)
        {

            string eventLog = "SampleError";

            string eventSource = "ErrorLoggingSampleApp";

            // check if source exists

            if (!EventLog.SourceExists(eventSource))
            {

                System.Diagnostics.EventLog.CreateEventSource(eventSource, eventLog);

            }

            // create the instance of the EventLog and log the error

            using (EventLog myLog = new EventLog(eventLog))
            {

                myLog.Source = eventSource;

                string error = "\r\nLog written at : " + DateTime.Now.ToString() +

                "\r\nError occured on page : " + context.Current.Request.Url.ToString() +

                "\r\n\nHere is the actual error :\n" + ee.ToString();

                myLog.WriteEntry(error, EventLogEntryType.Error);

            }

            return userFriendlyError;

        }

        /// <summary>

        /// Logs the error.

        /// </summary>

        /// <param name="ee">The ee.</param>

        /// <param name="userFriendlyError">The user friendly error.</param>

        /// <returns></returns>
        public static string LogError(Exception ee, string userFriendlyError)
        {
            try
            {
                string logType = ConfigurationManager.AppSettings["ErrorLogType"].ToString();

                if (logType.Equals("1"))
                {
                    return LogErrorToLogFile(ee, userFriendlyError);
                }
                else
                {
                    return LogErrorInSystemEvent(ee, userFriendlyError);
                }
            }
            catch (Exception ex)
            {
                return LogError(ex, userFriendlyError);
            }

        }
    }
}
