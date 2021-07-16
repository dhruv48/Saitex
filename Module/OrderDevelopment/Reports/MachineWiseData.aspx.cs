using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

public partial class Module_OrderDevelopment_Reports_MachineWiseData : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    public static string FDATE = string.Empty;
    public static string TDATE = string.Empty;
    public static string machine = string.Empty;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        if (!IsPostBack)
        {
            Initial_Control();
         
        }
    }

    public DataTable BindGrid()
    {
        try
        {
            if (TxtFdate.Text.ToString() != null && TxtFdate.Text.ToString() != string.Empty)
            {
                FDATE = TxtFdate.Text.ToString();

            }
            else
            {
                FDATE = string.Empty;
            }

            if (TxtTdate.Text.ToString() != null && TxtTdate.Text.ToString() != string.Empty)
            {
                TDATE = TxtTdate.Text.Trim().ToString();
            }
            else
            {
                TDATE = string.Empty;
            }
            if (ddlmachine.SelectedValue != null && ddlmachine.SelectedValue != string.Empty)
            {
                machine = ddlmachine.SelectedValue.ToString();
            }
            else
            {
                machine = string.Empty;
            }
           

           DataTable  dt = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetDataForReportMachineWise(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, FDATE, TDATE, machine);
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    OrderCapt_Grid.DataSource = dt;
            //    OrderCapt_Grid.DataBind();
            //    lblTotalRecord.Text = dt.Rows.Count.ToString();
            //}
            //else
            //{
            //    OrderCapt_Grid.DataSource = null;
            //    OrderCapt_Grid.DataBind();
            //    Common.CommonFuction.ShowMessage("Data not found by selected item.");
            //    lblTotalRecord.Text = "0";
            //}

           return dt;

        }
        catch 
        {
            throw;
        }
    }

    private void Initial_Control()
    {
        try
        {
            getMachines();
            imgbtnClear.Visible = true;
            imgbtnExit.Visible = true;
            imgbtnHelp.Visible = true;
           
            ddlmachine.SelectedIndex = -1;
           
            TxtFdate.Text = string.Empty;
            TxtTdate.Text = string.Empty;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public void getMachines()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.OD_SHADE_FAMILY.GetMachineCode();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlmachine.Items.Clear();
                ddlmachine.DataSource = dt;
                ddlmachine.DataTextField = "MACHINE_CODE";
                ddlmachine.DataValueField = "MACHINE_CODE";
                ddlmachine.DataBind();
                ddlmachine.Items.Insert(0, new ListItem("------SELECT----", ""));
            }
        }
        catch
        {
            throw;
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
        //  HttpContext.Current.ApplicationInstance.CompleteRequest();
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnExit_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btnview_Click(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    protected void ddlmachine_SelectedIndexChanged(object sender, EventArgs e)
    {
       // BindGrid();
        //try
        //{
        //    DataTable dt = SaitexBL.Interface.Method.OD_SHADE_FAMILY.GetMachineCode();
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        ddlmachine.Items.Clear();
        //        ddlmachine.DataSource = dt;
        //        ddlmachine.DataTextField = "MACHINE_CAPACITY";
        //        ddlmachine.DataValueField = "MACHINE_CODE";
        //        ddlmachine.DataBind();
        //       ddlmachine.Items.Insert(0, new ListItem("------SELECT----", "0"));
        //    }
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
    }
    protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string strFilename = "Sale_Order_Production_Machine_For_Yarn_Dyeing_" + DateTime.Now.ToShortDateString() + ".xls";


            ExporttoExcel(BindGrid(), strFilename, "Sale Order Production Machine For Yarn Dyeing");
        }
        catch(Exception eX) 
        { 
            throw eX;
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Initial_Control();
       
    }
    protected void imgbtnPrint_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                string msg = "";
               

                //if (TxtFdate.Text.Trim() == "")
                //    msg += "Enter the From Date.</ br>";
                //if (TxtTdate.Text.Trim() == "")
                //    msg += "Enter the To Date</ br>";
                if (msg == "")
                {
                    string QueryString = "";
                    QueryString += "?FDate=" + TxtFdate.Text.Trim();
                    QueryString += "&TDate=" + TxtTdate.Text.Trim();
                    QueryString += "&Machine=" + ddlmachine.SelectedValue.ToString();
                    string URL = "../Reports/Batch_Make_Planning.aspx" + QueryString;
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
}