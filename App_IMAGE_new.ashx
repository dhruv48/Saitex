<%@ WebHandler Language="C#" Class="App_IMAGE_new" %>

using System;
using System.Web;
using System.Data.OracleClient;
using System.IO;
using System.Data;

public class App_IMAGE_new : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        try
        {
            var Id="";
            if (context.Request.QueryString["MST_CODE"] != null)
            {
                Id = context.Request.QueryString["MST_CODE"];
            }
            else
            {
                throw new ArgumentException("No parameter specified");
 
            }
            context.Response.ContentType = "image/jpeg";
            Stream strm = DisplayImage(Id);
            byte[] buffer=new byte[105442];
            int byteseq = strm.Read(buffer, 0, 105442);
            while (byteseq > 0)
            {
                //context.Response.WriteFile("C:/new.jpeg");
                context.Response.OutputStream.Write(buffer, 0, byteseq);
                byteseq = strm.Read(buffer, 0, 2048);
            }
            //OracleConnection CON = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["csTextile"].ToString());
            //OracleCommand cmd = new OracleCommand(" SELECT SUB_IMG FROM APP_MASTER_IMG_TRN WHERE MST_CODE='" + id + "'", CON);
            //CON.Open();
            
        }
        catch (Exception ex)
        {
            throw ex; 
        }
        
    }
    public Stream DisplayImage(string Id)
    {
        OracleConnection CON = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["csTextile"].ToString());
        OracleCommand cmd = new OracleCommand(" SELECT SUB_IMG FROM APP_MASTER_IMG_TRN WHERE MST_CODE='" + Id + "'", CON);
        CON.Open();
        object theImg = cmd.ExecuteScalar();
        try
        {
            return new MemoryStream((byte[])theImg);
        }
        catch
        {
            return null; 
        }  
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}