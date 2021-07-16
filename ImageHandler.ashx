<%@ WebHandler Language="C#" Class="ImageHandler" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.OracleClient;
using System.IO;
using System.Data;

public class ImageHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {       
        try
        {

            var id = context.Request.QueryString["EMP_CODE"];
            OracleConnection CON = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["csTextile"].ToString());
            //OracleConnection CON = new OracleConnection("User Id=sai;Data Source=sai;Password=sai;Pooling=false;");
            OracleCommand cmd = new OracleCommand(" SELECT SUB_IMG FROM HR_EMP_MST WHERE EMP_CODE='" + id + "'", CON);
            CON.Open();
            var image = (byte[])cmd.ExecuteScalar();
            context.Response.BinaryWrite(image);
            context.Response.End();
        }
        catch
        {
        }
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }


}