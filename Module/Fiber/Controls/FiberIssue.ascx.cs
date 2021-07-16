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
using Common;
using errorLog;
public partial class Module_Fiber_Controls_FiberIssue : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    string yr = string.Empty;
    string catcode = string.Empty;
    private static DateTime Sdate;
    private static DateTime Edate;
    string sdate1 = string.Empty;
    string edate1 = string.Empty;

    protected void Page_Load(object sender, EventArgs e)  
   {
       oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        if (!IsPostBack)
        {
            Initial_Control();
            Maxtrnno();          
            bindddltemtype();
            Bind_Year_ddlYear();
            bindgridfiberissue();
        }
    }

    private void Initial_Control()
    {
        try
        {

            //Sdate = oUserLoginDetail.DT_STARTDATE;
            //if (DateTime.IsLeapYear(Sdate.AddDays(364).Year))
            //{
            //    Edate = Sdate.AddDays(365);
            //}
            //else
            //{
            //    Edate = Sdate.AddDays(364);
            //}

            
            //txtDate1.Text  = Sdate.Date.ToString("dd/MM/yyyy");
            //txtDate2.Text = Edate.Date.ToString("dd/MM/yyyy");
            txtDate1.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtDate2.Text = DateTime.Now.ToString("dd/MM/yyyy");
            ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));

        }
        catch
        {
            throw;
        }
    }
    private void Bind_Year_ddlYear()
    {
        try
        {

            ddlYear.Items.Clear();
            string BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            var dt = SaitexBL.Interface.Method.CM_FIN_YEAR_MST.Get_YearCodeYear(BRANCH_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlYear.DataTextField = "YEAR";
                ddlYear.DataValueField = "FIN_YEAR_CODE";
                ddlYear.DataSource = dt;
                ddlYear.DataBind();

            }
            ddlYear.Items.Insert(0, new ListItem("SELECT", ""));
        }
        catch
        {
            throw;
        }
    }
    private DataTable bindgridfiberissue()
    {
        try
        {

            string trn;
            string item;
            int from;
            int to;
            DateTime StDate;
            DateTime EnDate;
            if (DDLTrnType.SelectedValue.ToString() != null && DDLTrnType.SelectedValue.ToString() != string.Empty)
            {
                trn = DDLTrnType.SelectedValue.ToString();
            }
            else
            {
                trn = string.Empty;
            }


            if (ddlitem.SelectedValue.ToString() != null && ddlitem.SelectedValue.ToString() != string.Empty)
            {
                item = ddlitem.SelectedValue.ToString();
            }
            else
            {
                item = string.Empty;
            }

            if (Txtfromno.Text.ToString() != null && Txtfromno.Text.ToString() != string.Empty)
            {
                from = int.Parse(Txtfromno.Text.ToString());

            }
            else
            {
                from = 1;

            }

            if (txttono.Text.ToString() != null && txttono.Text.ToString() != string.Empty)
            {
                to = int.Parse(txttono.Text.ToString());

            }
            else
            {
                to = 5;
            }

            if (txtDate1.Text != string.Empty && txtDate2.Text != string.Empty)
            {
                StDate = DateTime.Parse(txtDate1.Text);
                EnDate = DateTime.Parse(txtDate2.Text);
            }
            else
            {
                StDate = Sdate;
                EnDate = Edate;
            }

            DataTable dt = SaitexBL.Interface.Method.TX_FIBER_IR_MST.FiberIssue(trn, item, from, to, StDate, EnDate);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (DDLTrnType.SelectedValue == "I")
                {
                    GridView1.Visible = false;
                    grdIssue.Visible = true;
                    grdIssue.DataSource = dt;
                    grdIssue.DataBind();
                }
                else
                {
                    GridView1.Visible = true ;
                    grdIssue.Visible = false;
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                if (DDLTrnType.SelectedValue == "I")
                {
                    GridView1.Visible = false;
                    grdIssue.Visible = true;
                    grdIssue.DataSource = dt;
                    grdIssue.DataBind();
                }
                else
                {
                    GridView1.Visible = true;
                    grdIssue.Visible = false;
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                CommonFuction.ShowMessage("Data not Found by selected Item .");
           
            }
            return dt;
             }
        catch
        {
            throw;
        }
    }
    private void bindddltemtype()
    {
        try
        {

            ddlitem.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.TX_FIBER_MST.GetFiber();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlitem.DataTextField = "FIBER_DESC";
                ddlitem.DataValueField = "FIBER_CODE";
                ddlitem.DataSource = dt;
                ddlitem.DataBind();

            }
            ddlitem.Items.Insert(0, new ListItem("SELECT", ""));
        }
        catch
        {
            throw;
        }
    }
    protected void Txtfromno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            bindgridfiberissue();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));
        }
    }
    protected void txttono_TextChanged(object sender, EventArgs e)
    {
        try
        {
            bindgridfiberissue();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));
        }


    }
    protected void ddlitem_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            bindgridfiberissue();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));
        }
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
       // bindFromToDate();
    }

    private void bindFromToDate()
    {
        try
        {
            string FIN_YEAR_CODE = ddlYear.SelectedValue.ToString();
            DataTable dt = SaitexBL.Interface.Method.CM_FIN_YEAR_MST.FinYear(FIN_YEAR_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "FIN_YEAR_CODE='" + ddlYear.SelectedValue.ToString() + "'";
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        //TxtIndentDate.SelectedDate = DateTime.Parse(dv[iLoop]["START_DATE"].ToString()).Date;
                        //TxtIndentDate1.SelectedDate = DateTime.Parse(dv[iLoop]["END_DATE"].ToString()).Date;
                        txtDate1.Text = DateTime.Parse(dv[iLoop]["START_DATE"].ToString()).Date.ToString("dd/MM/yyyy");
                        txtDate2.Text = DateTime.Parse(dv[iLoop]["END_DATE"].ToString()).Date.ToString("dd/MM/yyyy");
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }

    protected void DDLTrnType_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            bindgridfiberissue();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));
        }

    }
    private void Maxtrnno()
    {
        try
        {
            int I = 1;
           Txtfromno.Text  = I.ToString();
            string x = "";
            int y = 0;
            string trn;
            DataTable dt;
            if (DDLTrnType.SelectedValue.ToString() != null && DDLTrnType.SelectedValue.ToString() != string.Empty)
            {
                trn = DDLTrnType.SelectedValue.ToString();
            }
            else
            {
                trn = string.Empty;
            }
            dt = SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetMaxtTRN(trn);
            if (trn == "I")
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataView dv = new DataView(dt);
                    if (dv.Count > 0)
                    {
                        for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                        {
                            x = dv[iLoop]["ISS_TRN_NUMB"].ToString();
                            y = int.Parse(x);

                            txttono.Text = y.ToString();
                        }
                    }
                }

            }

            if (trn == "R")
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataView dv = new DataView(dt);
                    if (dv.Count > 0)
                    {
                        for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                        {
                            x = dv[iLoop]["TRN_NUMB"].ToString();
                            y = int.Parse(x);
                            txttono.Text = y.ToString();
                        }
                    }
                }
            }
        }
        catch
        {
            throw;
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
                Response.Redirect("~/Module/Admin/Pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

        try
        {

            string trn = string.Empty;
            string item = string.Empty;
            string fromto = string.Empty;
            string toto = string.Empty;

            DataTable myDataTable = new DataTable();
            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "trn";
            myDataTable.Columns.Add(myDataColumn);



            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "item";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "fromto";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "toto";
            myDataTable.Columns.Add(myDataColumn);

            DataRow row;

            row = myDataTable.NewRow();
            row["trn"] = DDLTrnType.SelectedValue.ToString();
            row["item"] = ddlitem.SelectedValue.ToString();
            row["fromto"] = Txtfromno.Text;
            row["toto"] = txttono.Text;

            if (DDLTrnType.SelectedValue.ToString() != null && DDLTrnType.SelectedValue.ToString() != string.Empty)
            {
                row["trn"] = DDLTrnType.SelectedValue.ToString();
            }
            else
            {
                row["trn"] = string.Empty;
            }


            if (ddlitem.SelectedValue.ToString() != null && ddlitem.SelectedValue.ToString() != string.Empty)
            {
                row["item"] = ddlitem.SelectedValue.ToString();
            }
            else
            {
                row["item"] = string.Empty;
            }

            if (Txtfromno.Text.ToString() != null && Txtfromno.Text.ToString() != string.Empty)
            {
                row["fromto"] = int.Parse(Txtfromno.Text.ToString());

            }
            else
            {
                row["fromto"] = 1;


            }

            if (txttono.Text.ToString() != null && txttono.Text.ToString() != string.Empty)
            {
                row["toto"] = int.Parse(txttono.Text.ToString());

            }
            else
            {
                row["toto"] = 5;
            }

            myDataTable.Rows.Add(row);
            Session["Proceereport"] = myDataTable;
           // Response.Redirect("~/Module/Yarn/SalesWork/Reports/yrnissuereport.aspx", false);

            string URL = "../Reports/Fiberissuereport.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in getting print.\r\nSee error log for detail."));
        }         
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdIssue.PageIndex = e.NewPageIndex;
        bindgridfiberissue();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
    protected void btnGetReport_Click(object sender, EventArgs e)
    {
        bindgridfiberissue();
    }
    protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
    {
        string strFilename = "POY_Issue_Details_List_" + DateTime.Now.ToString() + ".xls";
        ExporttoExcel(bindgridfiberissue(), strFilename, "P.O.Y. Issue Details List");
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


}
