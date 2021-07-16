using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//using System.Data.SqlClient;
using System.Data.Common;
using System.IO;
using System.Drawing;
/// <summary>
/// Summary description for saitexLogin
/// </summary>
public static class saitexLogin
{
    static saitexLogin()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static DataTable LoginCheck(string vc_LoginId, string ch_Password, string ch_Status)
    {
        //get a configured DbCommend object
        try
        {
            DbCommand comm = GenericDataAccess.CreateCommand();
            comm.CommandText = "select in_UserId,vc_LoginId,ch_Status from tblUserMaster where ltrim(rtrim(vc_LoginId))=@vc_LoginId and ltrim(rtrim(ch_Password))=@ch_Password and ltrim(rtrim(ch_Status))=@ch_Status";
            comm.CommandType = CommandType.Text;

            DbParameter param = comm.CreateParameter();
            param = comm.CreateParameter();
            param.ParameterName = "@vc_LoginId";
            param.DbType = DbType.String;
            param.Value = vc_LoginId.Trim();
            param.Size = 10;
            comm.Parameters.Add(param);

            param = comm.CreateParameter();
            param = comm.CreateParameter();
            param.ParameterName = "@ch_Password";
            param.DbType = DbType.String;
            param.Value = ch_Password.Trim();
            param.Size = 10;
            comm.Parameters.Add(param);

            param = comm.CreateParameter();
            param = comm.CreateParameter();
            param.ParameterName = "@ch_Status";
        
            param.DbType = DbType.String;
            param.Value = ch_Status.Trim();
            param.Size = 10;
            comm.Parameters.Add(param);
        
            return GenericDataAccess.ExecuteReader(comm);
        }
            catch (Exception ex)
            {

                throw ex;
            }
    }
}
