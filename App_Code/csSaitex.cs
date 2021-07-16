using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OracleClient;
/// <summary>
/// Summary description for csSaitex
/// </summary>
namespace DBLibrary
{
    public class csSaitex
    {
       

        #region Declaration of private members.
            public OracleConnection con = null;
            private OracleCommand cmd = null;
            private OracleDataAdapter da = null;
        #endregion

        public csSaitex()
        {
            con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();
        }

        #region Generic DataBase Function

        public int NonExecuteQuery(string strSQL, CommandType ct)
        {
            try
            {
                cmd = new OracleCommand(strSQL, con);
                cmd.CommandType = ct;
                return cmd.ExecuteNonQuery();
            }

            catch (OracleException ex)
            {
                throw ex;

            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                    con.Dispose();
                    con = null;
                }

                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd = null;
                }

            }



        }

        public string executeScalar(string strSQL, CommandType ct)
        {
            try
            {
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = strSQL;
                cmd.CommandType = ct;
                return Convert.ToString(cmd.ExecuteScalar());

            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {

                if (con != null)
                {
                    con.Close();
                    con.Dispose();
                    con = null;
                }
                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd = null;
                }

            }
        }


        public OracleDataReader getDataReader(string strSQL, CommandType ct)
        {
            try
            {
                cmd = new OracleCommand(strSQL, con);
                cmd.CommandType = ct;
                return cmd.ExecuteReader();
            }

            catch (OracleException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet getDataSet(string strSQL, CommandType ct)
        {
            DataSet dsVoice = null;

            try
            {
                cmd = new OracleCommand(strSQL, con);
                cmd.CommandType = ct;
                da = new OracleDataAdapter(cmd);
                dsVoice = new DataSet();
                da.Fill(dsVoice);
                return dsVoice;

            }

            catch (OracleException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                    con.Dispose();
                    con = null;
                }

                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd = null;
                }
                if (da != null)
                {
                    da.Dispose();
                    da = null;
                }
            }

        }


        public DataTable getDataTable(string strSQL, CommandType ct)
        {
            DataTable dtBar = new DataTable();
            try
            {
                cmd = new OracleCommand(strSQL, con);
                cmd.CommandType = ct;
                da = new OracleDataAdapter(cmd);
                da.Fill(dtBar);
                return dtBar;

            }
            catch (OracleException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                    con.Dispose();
                    con = null;
                }

                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd = null;
                }

            }

        }

        //public int maxIdofTable(string strSQL,CommandType ct)
        //{
        //    try
        //    {
        //        cmd = new OracleCommand(strSQL, con);
        //        cmd.CommandType = ct;
        //        string strMaxId= cmd.ExecuteReader();

        //        return Convert.ToInt16(strMaxId);
        //    }

        //    catch (OracleException ex)
        //    {
        //        throw ex;
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //        //ex.m
        //        //throw;
        //    }

        //}



        #endregion

        #region General Control Binding

        public void bindListBox(ListBox lst, string strSQL, CommandType ct, string strDataValueFields, string strDataTextFields)
        {
            try
            {
                lst.DataSource = getDataSet(strSQL, ct);
                lst.DataValueField = strDataValueFields;
                lst.DataTextField = strDataTextFields;
                lst.DataBind();



            }
            catch (OracleException ex)
            {
                //ErrHandler.WriteError(ex.Message);
                throw ex;
            }

            catch (Exception ex)
            {
                // ErrHandler.WriteError(ex.Message);
                throw ex;
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                    con.Dispose();
                    con = null;
                }
                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd.Dispose();
                    cmd = null;
                }
            }
        }

        public void bindRadioButtonList(RadioButtonList rad, string strSQL, CommandType ct, string strDataValueFields, string strDataTextFields)
        {
            try
            {
                rad.DataSource = getDataSet(strSQL, ct);
                rad.DataValueField = strDataValueFields;
                rad.DataTextField = strDataTextFields;
                rad.DataBind();
            }
            catch (OracleException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                    con.Dispose();
                    con = null;
                }
                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd.Dispose();
                    cmd = null;
                }
            }
        }

        public void bindDropDownList(DropDownList ddl, string strSQL, CommandType ct, string strDataValueFields, string strDataTextFields, bool strSelect)
        {
            try
            {
                ddl.DataSource = getDataSet(strSQL, ct);
                ddl.DataValueField = strDataValueFields;
                ddl.DataTextField = strDataTextFields;
                ddl.DataBind();

                if (strSelect)
                {

                    ddl.Items.Insert(0, new ListItem("---------Select----------", ""));

                }
            }
            catch (OracleException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                    con.Dispose();
                    con = null;
                }
                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd.Dispose();
                    cmd = null;
                }
            }
        }



        #endregion
    }

}
