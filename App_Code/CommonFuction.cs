using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Data.Common;
using System.IO;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Summary description for CommonFuction
/// </summary>
namespace Common
{
    public class CommonFuction
    {

        public CommonFuction()
        {

        }
        public static string funFixQuotes(string strInput)
        {
            string strOutput;
            strOutput = strInput.Replace("'", "''");
            return strOutput;
        }
        private static string[] _dangerousTags = { "applet", "body", "embed", "iframe", "frame", "script", "frameset", "html", "htm", "style", "layer", "link", "ilayer", "meta", "object", "img", ".js" };
        public static string funFixScript(string strInput)
        {
            // unchecked text
            string strOutput = "";
            strOutput = strInput.Replace("'", "''");
            string text = strOutput.Trim();
            for (int i = 0; i < _dangerousTags.Length; ++i)
            {
                text = ReplaceTag(text, _dangerousTags[i], "");
                // now text is checked
            }
            return text;
        }
        public static string ReplaceTag(string aText, string aTagName, string aReplaceWith)
        {
            string result;
            // checks for <dangerTag />
            string pattern = @"<.*?" + aTagName + @".*?/>";
            result = Regex.Replace(aText, pattern, aReplaceWith, RegexOptions.IgnoreCase);
            // checks for <dangerTag> ... </dangerTag>
            pattern = @"<.*?" + aTagName + @".*?>.*?</.*?" + aTagName + @".*?>";
            result = Regex.Replace(result, pattern, aReplaceWith, RegexOptions.IgnoreCase);
            return result;
        }
        public static bool GetEmployeeCode(string in_UserId, out string EmpCode, out int EmployeeMasterID)
        {
            EmpCode = "";
            EmployeeMasterID = 0;
            //get a configured DbCommend object
            try
            {
                bool result = false;

                DbCommand comm = GenericDataAccess.CreateCommand();

                comm.CommandText = "select in_UserId,VC_USERNAME,CH_PASSWORD,vc_LoginId from tblUserMaster where in_UserId=" + in_UserId;
                comm.CommandType = CommandType.Text;
                DataTable dtn = GenericDataAccess.ExecuteReader(comm);
                if (dtn != null && dtn.Rows.Count > 0)
                {
                    string UName = dtn.Rows[0]["VC_USERNAME"].ToString();
                    string pwd = dtn.Rows[0]["CH_PASSWORD"].ToString();


                    comm = GenericDataAccess.CreateCommand();
                    comm.CommandText = "select CH_EMPLOYEEMASTERID,ch_EmployeeCode from tblEmployeeMaster where VC_USERNAME='" + UName.Trim() + "' and CH_PASSWORD='" + pwd.Trim() + "'";
                    comm.CommandType = CommandType.Text;

                    DataTable dt = GenericDataAccess.ExecuteReader(comm);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        EmpCode = dt.Rows[0]["ch_EmployeeCode"].ToString();
                        EmployeeMasterID = int.Parse(dt.Rows[0]["CH_EMPLOYEEMASTERID"].ToString());
                        result = true; ;
                    }
                    else
                    {
                        result = false;
                    }

                    // OracleConnection con = new OracleConnection();
                    // con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
                    // con.Open();
                    //strSQL = "select CH_EMPLOYEEMASTERID,vc_firstName,vc_MiddleName,vc_LastName from tblEmployeeMaster where ltrim(rtrim(VC_USERNAME))=@VC_USERNAME and ltrim(rtrim(CH_PASSWORD))=@CH_PASSWORD";
                    // OracleCommand cmd = new OracleCommand(strSQL, con);

                    // cmd = new OracleCommand(strSQL, con);

                    // OracleParameter param = new OracleParameter("@VC_USERNAME", OracleType.VarChar, 150);
                    // param.Direction = ParameterDirection.Input;
                    // param.Value = UName.Trim();
                    // cmd.Parameters.Add(param);

                    // param = new OracleParameter("@CH_PASSWORD", OracleType.Char, 10);
                    // param.Direction = ParameterDirection.Input;
                    // param.Value = pwd.Trim();
                    // cmd.Parameters.Add(param);

                    // OracleDataReader reader = cmd.ExecuteReader();
                    // DataTable dt = new DataTable();
                    // dt.Load(reader);
                    // if (dt != null && dt.Rows.Count > 0)
                    // {
                    //     EmpName = dt.Rows[0]["vc_firstName"] + " " + dt.Rows[0]["vc_MiddleName"] + " " + dt.Rows[0]["vc_LastName"];
                    //     EmployeeMasterID = int.Parse(dt.Rows[0]["CH_EMPLOYEEMASTERID"].ToString());
                    //     result = true; ;
                    // }
                    // else
                    //     result = false;
                }
                return result;
            }
            catch
            {
                throw;
            }
        }
        public static string SaveImageFile(FileUpload tPhoto, string Destination, string FileNamePrefix)
        {
            try
            {
                string FileExtension = Path.GetExtension(tPhoto.PostedFile.FileName.Trim());
                if (!Directory.Exists(Destination))
                {
                    Directory.CreateDirectory(Destination);
                }

                DirectoryInfo di = new DirectoryInfo(Destination);
                string SerchPattern = "*" + FileExtension;
                FileInfo[] rgFiles = di.GetFiles(SerchPattern);
                int TotalFile = rgFiles.Length;
                string ImageUrl = "";
                ImageUrl = GetFileName(TotalFile, FileNamePrefix, rgFiles);
                ImageUrl = Destination + "/" + ImageUrl + FileExtension;
                return ImageUrl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string SaveImageFile(HtmlInputFile tPhoto, string Destination, string FileNamePrefix)
        {
            try
            {
                string FileExtension = Path.GetExtension(tPhoto.PostedFile.FileName.Trim());
                if (!Directory.Exists(Destination))
                {
                    Directory.CreateDirectory(Destination);
                }

                DirectoryInfo di = new DirectoryInfo(Destination);
                string SerchPattern = "*" + FileExtension;
                FileInfo[] rgFiles = di.GetFiles(SerchPattern);
                int TotalFile = rgFiles.Length;
                string ImageUrl = "";
                ImageUrl = GetFileName(TotalFile, FileNamePrefix, rgFiles);
                ImageUrl = Destination + "/" + ImageUrl + FileExtension;
                return ImageUrl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static string GetFileName(int TotalFile, string FileNamePrefix, FileInfo[] rgFiles)
        {
            try
            {
                string ImageUrl = "";
                ImageUrl = FileNamePrefix + TotalFile.ToString();
                foreach (FileInfo fi in rgFiles)
                {
                    if (fi.Name.Trim() == ImageUrl)
                    {
                        TotalFile = TotalFile + 1;
                        ImageUrl = GetFileName(TotalFile, FileNamePrefix, rgFiles);
                    }
                }
                return ImageUrl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DateTime GetYearEndDate(DateTime YearStartDate)
        {
            try
            {
                DateTime EndDate = YearStartDate.AddYears(1);
                TimeSpan tm = new TimeSpan(1, 0, 0, 0);
                EndDate = EndDate.Subtract(tm);

                return EndDate;
            }
            catch (Exception ex)
            {
                errorLog.ErrHandler.WriteError(ex.Message);
                throw ex;
            }
        }

        public static void ShowMessage(string Message)
        {

            // Gets the executing web page
            Page page = HttpContext.Current.CurrentHandler as Page;

            ScriptManager.RegisterStartupScript(page, page.GetType(), "alertmsg", "window.alert('" + Message + "');", true);
        }
        //public static bool ToDouble(string value, string LogicalName, int Precision, int scale, out string msg, out double result)
        //{
        //    msg = string.Empty;
        //    result = 0;
        //    bool bReturn = false;

        //    try
        //    {
        //        bReturn = double.TryParse(value, out result);
        //        if (bReturn)
        //        {
        //            if (value.Length <= Precision)
        //            {

        //                char[] splitter = { '.' };
        //                string[] arrString = value.Split(splitter);
        //                string scl = string.Empty;

        //                string abs = arrString[0];
        //                int iAbs = Precision - scale;
        //                iAbs = iAbs - 1;
        //                if (abs.Length > iAbs)
        //                {
        //                    Precision = Precision - scale;
        //                    msg += @"\r\nInvalid Absolute Value entered in" + LogicalName;
        //                    msg += @"\r\n  " + (Precision - 1) + " Precision Allowed";
        //                    bReturn = false;
        //                }

        //                if (arrString.Length > 1)
        //                {
        //                    scl = arrString[1];
        //                    if (scl.Length > scale)
        //                    {
        //                        msg += @"\r\nInvalid scale Value entered in " + LogicalName;
        //                        msg += @"\r\n    " + scale + " Scale Allowed";
        //                        bReturn = false;
        //                    }
        //                }


        //            }
        //            else
        //            {
        //                msg += @"\r\nentered value is larger than allowed precision in " + LogicalName;
        //                bReturn = false;
        //            }

        //        }
        //        else
        //        {
        //            msg += @"\r\nOnly valid numeric value allowed in " + LogicalName;
        //        }
        //        return bReturn;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        /// <summary>
        /// updated by sandeep
        /// </summary>
        /// <param name="value"></param>
        /// <param name="LogicalName"></param>
        /// <param name="Precision"></param>
        /// <param name="scale"></param>
        /// <param name="msg"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool ToDouble(string value, string LogicalName, int Precision, int scale, out string msg, out double result)
        {
            msg = string.Empty;
            result = 0;
            bool bReturn = false;

            try
            {
                bReturn = double.TryParse(value, out result);
                if (bReturn)
                {
                    if (value.Length <= Precision)
                    {

                        char[] splitter = { '.' };
                        string[] arrString = value.Split(splitter);
                        string scl = string.Empty;

                        string abs = arrString[0];
                        int iAbs = Precision - scale;
                        iAbs = iAbs - 1;
                        if (abs.Length > iAbs)
                        {
                            Precision = Precision - scale;
                            msg += @"\r\n # Invalid Absolute Value entered in " + LogicalName.ToUpper();
                            msg += @"\r\n  " + (Precision - 1) + " Precision Allowed";
                            bReturn = false;
                        }

                        if (arrString.Length > 1)
                        {
                            scl = arrString[1];
                            if (scl.Length > scale)
                            {
                                msg += @"\r\n # Invalid scale Value entered in " + LogicalName.ToUpper();
                                msg += @"\r\n    " + scale + " Scale Allowed";
                                bReturn = false;
                            }
                        }


                    }
                    else
                    {
                        msg += @"\r\nentered value is larger than allowed precision in " + LogicalName.ToUpper();
                        bReturn = false;
                    }

                }
                else
                {
                    msg += @"\r\nOnly valid numeric value allowed in " + LogicalName.ToUpper();
                }
                return bReturn;
            }
            catch
            {
                throw;
            }
        }

        #region Code by bharat to encrypt and Decrypt a string
        public static string base64Encode(string sData)
        {
            try
            {
                byte[] encData_byte = new byte[sData.Length];

                encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);

                string encodedData = Convert.ToBase64String(encData_byte);

                return encodedData;

            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        public static string base64Decode(string sData)
        {

            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();

            System.Text.Decoder utf8Decode = encoder.GetDecoder();

            byte[] todecode_byte = Convert.FromBase64String(sData);

            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);

            char[] decoded_char = new char[charCount];

            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);

            string result = new String(decoded_char);

            return result;

        }

        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            // Get the key from config file

            string key = (string)settingsReader.GetValue("SecurityKey", typeof(String));
            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            //Get your key from config file to open the lock!
            string key = (string)settingsReader.GetValue("SecurityKey", typeof(String));

            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        #endregion

        public static Hashtable GetEnumForBind(Type enumeration)
        {
            string[] names = Enum.GetNames(enumeration);
            Array values = Enum.GetValues(enumeration);
            Hashtable ht = new Hashtable();
            for (int i = 0; i < names.Length; i++)
            {
                Int32 key = Convert.ToInt32(values.GetValue(i));
                string name = names[i].ToString();
                ht.Add(Convert.ToInt32(values.GetValue(i)).ToString(), names[i]);
            }
            return ht;
        }

        public static double GetFinalRateOfDisTaxMaterial(DataTable dtRate, string ITEM_CODE, double BasicRate)
        {
            try
            {
                double dFinalRate = BasicRate;
                if (dtRate != null)
                {
                    if (!dtRate.Columns.Contains("Amount"))
                    {
                        dtRate.Columns.Add("Amount", typeof(double));
                    }

                    foreach (DataRow dr in dtRate.Rows)
                    {
                        string sItem_Code = dr["ITEM_CODE"].ToString();
                        if (ITEM_CODE.Equals(sItem_Code, StringComparison.OrdinalIgnoreCase))
                        {
                            double dAmount = 0;

                            double cAmount = 0;
                            double rate = double.Parse(dr["Rate"].ToString());
                            if (dr["BASE_COMPO_CODE"].ToString().Equals("Basic Rate"))
                            {
                                cAmount = (BasicRate * rate) / 100;
                            }
                            else if (dr["BASE_COMPO_CODE"].ToString().Equals("Final Rate"))
                            {
                                cAmount = (dFinalRate * rate) / 100;
                            }
                            else if (dr["BASE_COMPO_CODE"].ToString().Equals("Flat Amount"))
                            {
                                cAmount = rate;
                            }
                            else
                            {
                                DataView dvv = new DataView(dtRate);
                                dvv.RowFilter = "COMPO_CODE='" + dr["BASE_COMPO_CODE"].ToString() + "' and ITEM_CODE='" + ITEM_CODE + "'";

                                if (dvv.Count > 0)
                                {
                                    if (dvv[0]["COMPO_CODE"].ToString() == "CGST" || dvv[0]["COMPO_CODE"].ToString() == "SGST")
                                    {
                                        dFinalRate = (dFinalRate + double.Parse(dvv[0]["Amount"].ToString()));
                                        cAmount = double.Parse(dvv[0]["Amount"].ToString());
                                    }

                                    else
                                    {
                                        dAmount = double.Parse(dvv[0]["Amount"].ToString());
                                        cAmount = (dAmount * rate) / 100;
                                    }
                                }
                               // cAmount = (dAmount * rate) / 100;
                            }

                            if (dr["COMPO_TYPE"].ToString().Equals("D"))
                            {
                                dFinalRate = dFinalRate - cAmount;
                            }
                            else
                            {
                                dFinalRate = dFinalRate + cAmount;
                            }
                            dr["Amount"] = cAmount;
                        }
                    }
                }
                return dFinalRate;
            }
            catch
            {
                throw;
            }
        }

        public static double GetFinalRateOfDisTaxYarn(DataTable dtRate, string FIBER_CODE, string SHADE_CODE, double BasicRate)
        {
            try
            {
                double dFinalRate = BasicRate;
                if (dtRate != null)
                {
                    if (!dtRate.Columns.Contains("Amount"))
                    {
                        dtRate.Columns.Add("Amount", typeof(double));
                    }

                    foreach (DataRow dr in dtRate.Rows)
                    {
                        string syarnCode = dr["FIBER_CODE"].ToString();
                        string sShadecode = dr["SHADE_CODE"].ToString();
                        if (SHADE_CODE.Equals(sShadecode, StringComparison.OrdinalIgnoreCase) && FIBER_CODE.Equals(syarnCode, StringComparison.OrdinalIgnoreCase))
                        {
                            double dAmount = 0;

                            double cAmount = 0;
                            double rate = double.Parse(dr["Rate"].ToString());
                            if (dr["BASE_COMPO_CODE"].ToString().Equals("Basic Rate"))
                            {
                                cAmount = (BasicRate * rate) / 100;
                            }
                            else if (dr["BASE_COMPO_CODE"].ToString().Equals("Final Rate"))
                            {
                                cAmount = (dFinalRate * rate) / 100;
                            }
                            else
                            {
                                DataView dvv = new DataView(dtRate);
                                dvv.RowFilter = "COMPO_CODE='" + dr["BASE_COMPO_CODE"].ToString() + "'";

                                if (dvv.Count > 0)
                                {
                                    double.TryParse(dvv[0]["Amount"].ToString(), out dAmount);
                                }
                                cAmount = (dAmount * rate) / 100;
                            }

                            if (dr["COMPO_TYPE"].ToString().Equals("D"))
                            {
                                dFinalRate = dFinalRate - cAmount;
                            }
                            else
                            {
                                dFinalRate = dFinalRate + cAmount;
                            }
                            dr["Amount"] = cAmount;

                        }

                    }
                }
                return Math.Round(dFinalRate,3);
            }
            catch
            {
                throw;
            }
        }
        public static double GetFinalRateOfDisTaxFABRIC(DataTable dtRate, string FABR_CODE, string SHADE_CODE, double BasicRate)
        {
            try
            {
                double dFinalRate = BasicRate;
                if (dtRate != null)
                {
                    if (!dtRate.Columns.Contains("Amount"))
                    {
                        dtRate.Columns.Add("Amount", typeof(double));
                    }

                    foreach (DataRow dr in dtRate.Rows)
                    {
                        string syarnCode = dr["FABR_CODE"].ToString();
                        string sShadecode = dr["SHADE_CODE"].ToString();
                        if (SHADE_CODE.Equals(sShadecode, StringComparison.OrdinalIgnoreCase) && FABR_CODE.Equals(syarnCode, StringComparison.OrdinalIgnoreCase))
                        {
                            double dAmount = 0;

                            double cAmount = 0;
                            double rate = double.Parse(dr["Rate"].ToString());
                            if (dr["BASE_COMPO_CODE"].ToString().Equals("Basic Rate"))
                            {
                                cAmount = (BasicRate * rate) / 100;
                            }
                            else if (dr["BASE_COMPO_CODE"].ToString().Equals("Final Rate"))
                            {
                                cAmount = (dFinalRate * rate) / 100;
                            }
                            else
                            {
                                DataView dvv = new DataView(dtRate);
                                dvv.RowFilter = "COMPO_CODE='" + dr["BASE_COMPO_CODE"].ToString() + "'";

                                if (dvv.Count > 0)
                                {
                                    double.TryParse(dvv[0]["Amount"].ToString(), out dAmount);
                                }
                                cAmount = (dAmount * rate) / 100;
                            }

                            if (dr["COMPO_TYPE"].ToString().Equals("D"))
                            {
                                dFinalRate = dFinalRate - cAmount;
                            }
                            else
                            {
                                dFinalRate = dFinalRate + cAmount;
                            }
                            dr["Amount"] = cAmount;

                        }

                    }
                }
                return Math.Round(dFinalRate,3);
            }
            catch
            {
                throw;
            }
        }
        public static double GetFinalRateOfDisTaxAPP_FABRIC(DataTable dtRate, string FABRIC_CODE, string COLOUR, double BasicRate)
        {
            try
            {
                double dFinalRate = BasicRate;
                if (dtRate != null)
                {
                    if (!dtRate.Columns.Contains("Amount"))
                    {
                        dtRate.Columns.Add("Amount", typeof(double));
                    }

                    foreach (DataRow dr in dtRate.Rows)
                    {
                        string syarnCode = dr["FABRIC_CODE"].ToString();
                        string sShadecode = dr["COLOUR"].ToString();
                        if (COLOUR.Equals(sShadecode, StringComparison.OrdinalIgnoreCase) && FABRIC_CODE.Equals(syarnCode, StringComparison.OrdinalIgnoreCase))
                        {
                            double dAmount = 0;

                            double cAmount = 0;
                            double rate = double.Parse(dr["Rate"].ToString());
                            if (dr["BASE_COMPO_CODE"].ToString().Equals("Basic Rate"))
                            {
                                cAmount = (BasicRate * rate) / 100;
                            }
                            else if (dr["BASE_COMPO_CODE"].ToString().Equals("Final Rate"))
                            {
                                cAmount = (dFinalRate * rate) / 100;
                            }
                            else
                            {
                                DataView dvv = new DataView(dtRate);
                                dvv.RowFilter = "COMPO_CODE='" + dr["BASE_COMPO_CODE"].ToString() + "'";

                                if (dvv.Count > 0)
                                {
                                    double.TryParse(dvv[0]["Amount"].ToString(), out dAmount);
                                }
                                cAmount = (dAmount * rate) / 100;
                            }

                            if (dr["COMPO_TYPE"].ToString().Equals("D"))
                            {
                                dFinalRate = dFinalRate - cAmount;
                            }
                            else
                            {
                                dFinalRate = dFinalRate + cAmount;
                            }
                            dr["Amount"] = cAmount;

                        }

                    }
                }
                return dFinalRate;
            }
            catch
            {
                throw;
            }
        }




        public static void ExporttoExcel(DataTable table, string name, string title, string companyName)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + name);

            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
              "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
              "style='font-size:10.0pt; font-family:Calibri; background:white;'>");
            //am getting my grid's column headers
            int columnscount = table.Columns.Count;
            HttpContext.Current.Response.Write("<TR>");
            HttpContext.Current.Response.Write("<TD style='font-size:14.0pt;' align='center' colspan=" + columnscount + ">");
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write(companyName);
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</TD>");
            HttpContext.Current.Response.Write("</TR>");

            HttpContext.Current.Response.Write("<TR>");
            HttpContext.Current.Response.Write("<TD style='font-size:12.0pt;' align='center' colspan=" + columnscount + ">");
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write(" " + title + " ");
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</TD>");
            HttpContext.Current.Response.Write("</TR>");

            HttpContext.Current.Response.Write("<TR>");
            HttpContext.Current.Response.Write("<TD  align='center' colspan=" + columnscount + ">");
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write("DATED:" + DateTime.Now.ToString() + "");
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</TD>");
            HttpContext.Current.Response.Write("</TR>");


            HttpContext.Current.Response.Write("<TR>");

            foreach (DataColumn dtcol in table.Columns)
            {
                HttpContext.Current.Response.Write("<Td>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write(dtcol.ColumnName.Replace("_", " "));
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

            }
            HttpContext.Current.Response.Write("</TR>");
            foreach (DataRow row in table.Rows)
            {//write in new row
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }


        public static double GetFinalRateOfDisTaxFIBER(DataTable dtRate, string FIBER_CODE, double BasicRate)
        {
            try
            {
                double dFinalRate = BasicRate;
                if (dtRate != null)
                {
                    if (!dtRate.Columns.Contains("Amount"))
                    {
                        dtRate.Columns.Add("Amount", typeof(double));
                    }

                    foreach (DataRow dr in dtRate.Rows)
                    {
                        string sFiberCode = dr["FIBER_CODE"].ToString();

                        if (FIBER_CODE.Equals(sFiberCode, StringComparison.OrdinalIgnoreCase))
                        {
                            double dAmount = 0;

                            double cAmount = 0;
                            double rate = double.Parse(dr["Rate"].ToString());
                            if (dr["BASE_COMPO_CODE"].ToString().Equals("Basic Rate"))
                            {
                                cAmount = (BasicRate * rate) / 100;
                            }
                            else if (dr["BASE_COMPO_CODE"].ToString().Equals("Final Rate"))
                            {
                                cAmount = (dFinalRate * rate) / 100;
                            }
                            else if (dr["BASE_COMPO_CODE"].ToString().Equals("Flat Amount"))
                            {
                                cAmount = rate;
                            }
                            else
                            {
                                DataView dvv = new DataView(dtRate);
                                dvv.RowFilter = "COMPO_CODE='" + dr["BASE_COMPO_CODE"].ToString() + "'";

                                if (dvv.Count > 0)
                                {
                                    double.TryParse(dvv[0]["Amount"].ToString(), out dAmount);
                                }
                                cAmount = (dAmount * rate) / 100;
                            }

                            if (dr["COMPO_TYPE"].ToString().Equals("D"))
                            {
                                dFinalRate = dFinalRate - cAmount;
                            }
                            else
                            {
                                dFinalRate = dFinalRate + cAmount;
                            }
                            dr["Amount"] = cAmount;
                        }
                    }
                }
                return Math.Round(dFinalRate,3);
            }
            catch
            {
                throw;
            }
        }
        public static string Fix_Zero(string str_InText)
        {
            string str_OutText;
            if (str_InText == "")
                str_OutText = "0";
            else
                str_OutText = str_InText;
            return str_OutText;
        }
        public static string Fix_Zero_Text(string str_InText)
        {
            string str_OutText;
            if (str_InText == "0")
                str_OutText = "";
            else
                str_OutText = str_InText;

            return str_OutText;
        }


    }

}
