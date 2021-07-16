<%@ WebHandler Language="C#" Class="ImageHandler_" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.OracleClient;
using System.IO;
using System.Data;

public class ImageHandler_ : IHttpHandler
{


    public void ProcessRequest(HttpContext context)
    {
        try
        {
         
            var id = context.Request.QueryString["EMP_CODE"];
            OracleConnection CON = new OracleConnection("User Id=sai;Data Source=sai/192.168.1.29;Password=sai;Pooling=false;");
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