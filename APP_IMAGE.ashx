<%@ WebHandler Language="C#" Class="APP_IMAGE" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.OracleClient;
using System.IO;
using System.Data;

public class APP_IMAGE : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        try
        {

            var id = context.Request.QueryString["MST_CODE"];
            OracleConnection CON = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["csTextile"].ToString());
            OracleCommand cmd = new OracleCommand(" SELECT SUB_IMG FROM APP_MASTER_IMG_TRN WHERE MST_CODE='" + id + "'", CON);
            CON.Open();
            byte[] image = (byte[])cmd.ExecuteScalar();
            context.Response.ContentType = "image/jpeg";
            context.Response.BinaryWrite(image);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
          
            //context.Response.End();
        }
        catch(Exception ex)
        {
            throw ex;
        }   
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}