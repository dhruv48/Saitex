using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;


public partial class Module_Production_Controls_Dyeing_Production : System.Web.UI.UserControl
{
    private static DateTime Sdate;
    private static DateTime Edate;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginDetail"] != null)
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                Sdate = oUserLoginDetail.DT_STARTDATE;
                Edate = Common.CommonFuction.GetYearEndDate(Sdate);
              //  txtFrom.Text = (Sdate.ToShortDateString()).ToString();
              //  txtTo.Text = (Edate.ToShortDateString()).ToString();
                //BindExel();
                //BindBatchCode();
            }
        }
    }


    private void BindBatchCode()
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            string Trn_No = SaitexBL.Interface.Method.YRN_PROD_MST.GetNewTRNNUM(oUserLoginDetail.COMP_CODE,oUserLoginDetail.CH_BRANCHCODE,oUserLoginDetail.DT_STARTDATE.Year.ToString());
            txtFrom.Text = Trn_No.ToString();
            txtTo.Text = Trn_No.ToString();
        }
        catch
        {
            throw;
        }
    }

    private DataTable BindExel()
    {
        try
        { 
            DataTable dt = new DataTable();
            string From ="";
            string To= "";

            From =txtFrom.Text;
            To=txtTo.Text;

            if(txtFrom.Text.ToString()!="" && txtTo.Text.ToString()!="")
                  dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetExelDataForDyeing(oUserLoginDetail.COMP_CODE.ToString(), oUserLoginDetail.CH_BRANCHCODE.ToString(), oUserLoginDetail.DT_STARTDATE.Year.ToString(), From, To);

            return dt;
        }
        catch(Exception ex) 
        {
            throw ex;

        }
    }

    private void ExporttoExcel(DataTable table, string name, string title)
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
        HttpContext.Current.Response.Write(oUserLoginDetail.VC_COMPANYNAME);
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
                HttpContext.Current.Response.Write("<Td align=left valign=Top>");

                HttpContext.Current.Response.Write(row[i].ToString());
                //******************************************//

                //***********************************************//   

                HttpContext.Current.Response.Write("</Td>");

            }

            HttpContext.Current.Response.Write("</TR>");

        }
        HttpContext.Current.Response.Write("</Table>");
        HttpContext.Current.Response.Write("</font>");
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
         // HttpContext.Current.ApplicationInstance.CompleteRequest();
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                string msg = "";
                string From = "";
                string To = "";
                if (txtFrom.Text.Trim()=="")
                    msg += "Enter the Trn Number.</ br>";
                if (txtTo.Text.Trim()=="")
                    msg += "Enter the Trn Number.</ br>";
                if (msg == "")
                {
                    string QueryString = "";
                    QueryString += "?FromNo=" + txtFrom.Text.Trim();
                    QueryString += "&ToNo=" + txtTo.Text.Trim();


                    string URL = "../Report/Production_Dyeing_Report.aspx" + QueryString;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
                }
                else
                    Common.CommonFuction.ShowMessage(msg);
            }
        }
        catch
        {

        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //if (Session["LoginDetail"] != null)
            //{
            //    TRNTYPE = "RYS01";
            //    if (Request.QueryString["TRN_TYPE"] != null && Request.QueryString["TRN_TYPE"] != "")
            //    {
            //        TRNTYPE = Request.QueryString["TRN_TYPE"].ToString();
            //    }
            //    SetFromAndTo();
            //}
        }
        catch
        {

        }
    }
    protected void imgbtnExit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Session["RedirectURL"] != null)
            {
                Response.Redirect(Session["RedirectURL"].ToString(), false);
                Session["RedirectURL"] = null;
            }
            else
            {
                Response.Redirect("~/Module/Admin/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }

    }
     
    protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string strFilename = "Provisional Dyeing Production" + DateTime.Now.ToShortDateString() + ".xls";
            ExporttoExcel(BindExel(), strFilename, "Provisional Dyeing Production");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
