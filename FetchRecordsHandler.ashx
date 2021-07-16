<%@ WebHandler Language="C#" Class="FetchRecordsHandler" %>

using System;
using System.Web;
using System.Data;
using System.Text;

public class FetchRecordsHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        //Delay to create loading effect.
        System.Threading.Thread.Sleep(500);

        int lastProductId = Convert.ToInt32(context.Request.QueryString["lastProductId"]);

        context.Response.Write(GetNavDetail(lastProductId));

    }

    private string GetNavDetail(int lastProductId)
    {
        //Fetch records from the DB
        DataTable dtProducts = SaitexBL.Interface.Method.CM_NAV_MST.FetchNextLinks(lastProductId, 10);

        StringBuilder sb = new StringBuilder();
        System.Data.DataRow rowProduct;
        if (dtProducts.Rows.Count > 0)
        {
            for (int rowIndex = 0; rowIndex < dtProducts.Rows.Count; rowIndex++)
            {
                //The below logic is for applying alternate row style
                if (rowIndex % 2 != 0)
                    sb.Append("<tr>");
                else
                    sb.Append("<tr style='color:#333333;background-color:#FFFBD6;'>");

                rowProduct = dtProducts.Rows[rowIndex];
                for (int j = 0; j < dtProducts.Columns.Count; j++)
                {
                    sb.Append("<td>" + rowProduct[j] + "</td>");
                }
                sb.Append("</tr>");
            }
        }
        return sb.ToString();
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}