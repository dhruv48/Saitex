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
using System.Data.OracleClient;
using errorLog;
using Common;


public partial class Module_WorkOrder_Controls_WorkOrderQueryForm : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                InitialControl();
                BindBranch();
                ddlbranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
                tdtran_id.Visible = false;
                wo_no();
                party();
                 BindWoGrid();
                
            }
           
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void InitialControl()
    {
        try
        {
            imgbtnClear.Visible = true;
            imgbtnExit.Visible = true;
            imgbtnHelp.Visible = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    

    private void BindWoGrid()
    {
        string BRANCH_CODE = string.Empty;
        string WO_NUM = string.Empty;
        string PRTY_CODE = string.Empty;
        string FROM_DATE = string.Empty;
        string TO_DATE = string.Empty;

        try
        {

            if (ddlbranch.SelectedValue.ToString() != null && ddlbranch.SelectedValue.ToString() != string.Empty)
            {
                BRANCH_CODE = ddlbranch.SelectedValue.ToString();
            }
            else
            {
                BRANCH_CODE = string.Empty;
            }
            if (ddwo.SelectedValue.ToString() != null && ddwo.SelectedValue.ToString() != string.Empty)
            {
                WO_NUM = ddwo.SelectedValue.ToString();
            }
            else
            {
                WO_NUM = string.Empty;
            }
            if (ddprty.SelectedValue.ToString() != null && ddprty.SelectedValue.ToString() != string.Empty)
            {
                PRTY_CODE = ddprty.SelectedValue.ToString();
            }
            else
            {
                PRTY_CODE = string.Empty;
            }
            if (txtfrom.Text != null && txtfrom.Text != string.Empty && txtTo.Text != null && txtTo.Text != string.Empty)
            {
                FROM_DATE = txtfrom.Text;
                TO_DATE = txtTo.Text;
            }
            else
            {
                FROM_DATE = string.Empty;
                TO_DATE = string.Empty;
            }
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.OD_WO_MST.BindWoGrid(BRANCH_CODE, WO_NUM, PRTY_CODE,FROM_DATE,TO_DATE);
            if (dt != null && dt.Rows.Count > 0)
            {
                Get_WO_Detail.DataSource = dt;
                Get_WO_Detail.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                Get_WO_Detail.Visible = true;
            }
            else
            {
                Get_WO_Detail.DataSource = null;
                Get_WO_Detail.DataBind();
                Common.CommonFuction.ShowMessage("Record Not Available For This Parameter...");
                lblTotalRecord.Text = "0";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindWoTrn(string Branch_Code, string Wo_numb)
    {

        //try
        //{

        //    DataTable dt = new DataTable();
        //    dt = SaitexBL.Interface.Method.OD_WO_MST.BindWoTrnGrd(Branch_Code, Wo_numb);
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        Grd_Wo_trn.DataSource = dt;
        //        Grd_Wo_trn.DataBind();
        //        lblTotalRecord.Text = dt.Rows.Count.ToString();
        //        tdtran_id.Visible = true;
        //        Grd_Wo_trn.Visible = true;
        //    }
        //    else
        //    {
        //        Grd_Wo_trn.DataSource = null;
        //        Grd_Wo_trn.DataBind();
        //        lblTotalRecord.Text = "0";
        //        tdtran_id.Visible = false;
        //        Grd_Wo_trn.Visible = false;
        //        Common.CommonFuction.ShowMessage("Transaction Record Not Available For This Parameter...");
        //    }
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
    }

    

    protected void btngetbranch_Click(object sender, EventArgs e)
    {
        try
        {
            BindWoGrid();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InitialControl();
            Get_WO_Detail.Visible = false;
           // Grd_Wo_trn.Visible = false;
            tdtran_id.Visible = false;
        }
        catch (Exception ex)
        {
            throw ex;
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
                Response.Redirect("~/Module/Admin/pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in leaving page.\r\nSee error log for detail."));

        }
    }

    protected void Get_WO_Detail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            BindWoGrid();

            Get_WO_Detail.PageIndex = e.NewPageIndex;
            Get_WO_Detail.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void Get_WO_Detail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //GridViewRow grdRow = e.Row;

            int index = int.Parse(e.CommandArgument.ToString());
            GridViewRow row = Get_WO_Detail.Rows[index];

            Label lbl_BRANCH_CODE = (Label)row.FindControl("lbl_BRANCH_CODE");
            Label lblPRODUCT_TYPE = (Label)row.FindControl("lblPRODUCT_TYPE");
            Label lblWO_TYPE = (Label)row.FindControl("lblWO_TYPE");
            Label lblWO_NUMB = (Label)row.FindControl("lblWO_NUMB");
            Label lblWO_DATE = (Label)row.FindControl("lblWO_DATE");
            Label lblPRTY_CODE = (Label)row.FindControl("lblPRTY_CODE");
            Label lblJOB_WORK_CAT = (Label)row.FindControl("lblJOB_WORK_CAT");
            Label lblPAYMENT_TERMS = (Label)row.FindControl("lblPAYMENT_TERMS");
            Label lblDELIVERY_LOCATION = (Label)row.FindControl("lblDELIVERY_LOCATION");
            Label lblTRSP_CODE = (Label)row.FindControl("lblTRSP_CODE");

            string Branch_Code = lbl_BRANCH_CODE.Text;
            string Wo_numb = lblWO_NUMB.Text;

            BindWoTrn(Branch_Code, Wo_numb);
        }
        catch
        {
            throw;
        }
    }

    protected void Grd_Wo_trn_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    

    protected void Get_WO_Detail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string COMP_CODE = oUserLoginDetail.COMP_CODE.ToString();
                string BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE.ToString();
                int YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                GridViewRow grdRow = e.Row;
                Label lblwo_numb = (Label)e.Row.FindControl("lblWO_NUMB");
                Label lblARTICLE_CODE = (Label)e.Row.FindControl("lblARTICLE_CODE");
                Label lblSHADE_CODE = (Label)e.Row.FindControl("lblPRODUCT_TYPE");

                string wo_numb = lblwo_numb.Text;
                //string Article_code = lblARTICLE_CODE.Text;
                string PR_TYPE = lblSHADE_CODE.Text;

                DataSet ds = BindePoSupGrid(wo_numb, PR_TYPE, COMP_CODE, BRANCH_CODE, YEAR);
                ViewState["dt_trn"] = ds.Tables[0];
                ViewState["dt_bom"] = ds.Tables[1];
                ViewState["dt_tax"] = ds.Tables[2];
                GridView Grd_Wo_trn = (GridView)e.Row.FindControl("Grd_Wo_trn");
                GridView grd_bom = (GridView)e.Row.FindControl("grd_bom");
                if (ds != null && ds.Tables.Count > 0)
                {
                    Grd_Wo_trn.DataSource = ds.Tables[0];
                    Grd_Wo_trn.DataBind();
                }
                if (ds != null && ds.Tables.Count > 0)
                {
                    grd_bom.DataSource = ds.Tables[1];
                    grd_bom.DataBind();
                }
                GridView grd_tax = (GridView)e.Row.FindControl("grd_adjustment");
                if (ds != null && ds.Tables.Count > 0)
                {
                    grd_tax.DataSource = ds.Tables[2];
                    grd_tax.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //protected void Grd_Wo_trn_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    try
    //    {
    //        if (e.Row.RowType == DataControlRowType.DataRow)
    //        {
    //            string COMP_CODE = oUserLoginDetail.COMP_CODE.ToString();
    //            string BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE.ToString();
    //            int YEAR = oUserLoginDetail.DT_STARTDATE.Year;
    //            Label lblwo_numb = (Label)e.Row.FindControl("lblwo_numb");
    //          //  Label lblARTICLE_CODE = (Label)e.Row.FindControl("lblPRODUCT_TYPE");
    //            Label lblSHADE_CODE = (Label)e.Row.FindControl("PRODUCT_TYPE");
                
    //            string wo_numb = lblwo_numb.Text;
    //            //string Article_code = lblARTICLE_CODE.Text;
    //            string PR_TYPE = lblSHADE_CODE.Text;

    //            //DataSet ds = BindePoSupGrid(wo_numb, PR_TYPE, COMP_CODE, BRANCH_CODE,YEAR);

    //            GridView grd_bom = (GridView)e.Row.FindControl("grd_bom");
    //            //if (ds != null && ds.Tables.Count > 0)
    //            //{
    //            //    grd_bom.DataSource = ds.Tables[1];
    //            //    grd_bom.DataBind();
    //            //}
    //            //GridView grd_adjustment = (GridView)e.Row.FindControl("grd_adjustment");
    //            //if (ds != null && ds.Tables.Count > 0)
    //            //{
    //            //    grd_adjustment.DataSource = ds.Tables[2];
    //            //    grd_adjustment.DataBind();
    //            //}
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    protected DataSet BindePoSupGrid(string wo_numb, string PR_TYPE, string COMP_CODE, string BRANCH_CODE, int YEAR)
    {
        try
        {
            DataSet ds = SaitexBL.Interface.Method.OD_WO_MST.GetPopUpTrnData(wo_numb,PR_TYPE,COMP_CODE,BRANCH_CODE,YEAR);
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            InitialControl();
             BindWoGrid();
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void ddwo_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindWoGrid();

    }
    private void BindBranch()
    {
        try
        {

            DataTable dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(oUserLoginDetail.COMP_CODE);
            ddlbranch.Items.Clear();
            DataView dv = new DataView(dt);
            ddlbranch.DataSource = dv;
            ddlbranch.DataValueField = "BRANCH_CODE";
            ddlbranch.DataTextField = "BRANCH_NAME";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, new ListItem("--------Select---------", string.Empty));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }

    public void wo_no()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.OD_WO_MST.getwo_no(oUserLoginDetail.COMP_CODE);
            ddwo.Items.Clear();
            ddwo.DataSource = dt;
            ddwo.DataValueField = "WO_NUMB";
            ddwo.DataTextField = "WO_NUMB";
            ddwo.DataBind();
            ddwo.Items.Insert(0, new ListItem("-------Select-----", string.Empty));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void party()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.OD_WO_MST.getparty(oUserLoginDetail.COMP_CODE);
            ddprty.Items.Clear();
            ddprty.DataSource = dt;
            ddprty.DataTextField = "PRTY_NAME";
            ddprty.DataValueField = "JOBER_PARTY";
            ddprty.DataBind();
            ddprty.Items.Insert(0, new ListItem("-------select--------", string.Empty));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    
    }

    protected void ddprty_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindWoGrid();
    }
    protected void txtfrom_TextChanged(object sender, EventArgs e)
    {
        BindWoGrid();
    }
    protected void txtTo_TextChanged(object sender, EventArgs e)
    {
        BindWoGrid();
    }


                         /////////////////////////////////    Import Excel ////////////////////////////////
    protected void ExportDataTableToExcel(DataTable table, string title,DataTable dt_trn,DataTable dt_bom, DataTable dt_tax)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");        //HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + name);

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
                if (i == 11)
                {

                    HttpContext.Current.Response.Write("<Td align=left valign=Top>");

                    DataView dvBASEQUALITY = new DataView(dt_trn);
                    dvBASEQUALITY.RowFilter = "WO_NUMB='" + row["WO_NUMB"].ToString() + "' AND PRODUCT_TYPE='" + row["PRODUCT_TYPE"].ToString()+"'";

                    if (dvBASEQUALITY.Count > 0)
                    {
                        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
          "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
          "style='font-size:10.0pt; font-family:Calibri; background:white;'>");
                        HttpContext.Current.Response.Write("<TR>");

                        foreach (DataColumn dtcol in dt_trn.Columns)
                        {
                            HttpContext.Current.Response.Write("<Td bgcolor=silver>");
                            HttpContext.Current.Response.Write("<B>");
                            HttpContext.Current.Response.Write(dtcol.ColumnName.Replace("_", " "));
                            HttpContext.Current.Response.Write("</B>");
                            HttpContext.Current.Response.Write("</Td>");

                        }
                        HttpContext.Current.Response.Write("</TR>");
                        for (int j = 0; j < dvBASEQUALITY.Count; j++)
                        {
                            HttpContext.Current.Response.Write("<Tr>");
                            for (int i1 = 0; i1 < dvBASEQUALITY.Table.Columns.Count; i1++)
                            {
                                HttpContext.Current.Response.Write("<Td >");
                                HttpContext.Current.Response.Write(dvBASEQUALITY[j][i1].ToString());
                                HttpContext.Current.Response.Write("</Td>");

                            }
                            HttpContext.Current.Response.Write("</Tr>");
                        }
                        HttpContext.Current.Response.Write("</Table>");
                    }
                    HttpContext.Current.Response.Write("</Td>");

                }
                if (i == 11)
                {

                    HttpContext.Current.Response.Write("<Td align=left valign=Top>");

                    DataView dtbom = new DataView(dt_bom);
                    dtbom.RowFilter = "WO_NUMB='" + row["WO_NUMB"].ToString() + "' AND PRODUCT_TYPE='" + row["PRODUCT_TYPE"].ToString() + "'";

                    if (dtbom.Count > 0)
                    {
                        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
          "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
          "style='font-size:10.0pt; font-family:Calibri; background:white;'>");
                        HttpContext.Current.Response.Write("<TR>");

                        foreach (DataColumn dtcol in dt_bom.Columns)
                        {
                            HttpContext.Current.Response.Write("<Td bgcolor=silver>");
                            HttpContext.Current.Response.Write("<B>");
                            HttpContext.Current.Response.Write(dtcol.ColumnName.Replace("_", " "));
                            HttpContext.Current.Response.Write("</B>");
                            HttpContext.Current.Response.Write("</Td>");

                        }
                        HttpContext.Current.Response.Write("</TR>");
                        for (int j = 0; j < dtbom.Count; j++)
                        {
                            HttpContext.Current.Response.Write("<Tr>");
                            for (int i1 = 0; i1 < dtbom.Table.Columns.Count; i1++)
                            {
                                HttpContext.Current.Response.Write("<Td >");
                                HttpContext.Current.Response.Write(dtbom[j][i1].ToString());
                                HttpContext.Current.Response.Write("</Td>");

                            }
                            HttpContext.Current.Response.Write("</Tr>");
                        }
                        HttpContext.Current.Response.Write("</Table>");
                    }
                    HttpContext.Current.Response.Write("</Td>");
                
                }
                if (i == 11)
                {

                    HttpContext.Current.Response.Write("<Td align=left valign=Top>");

                    DataView dttax = new DataView(dt_tax);
                    dttax.RowFilter = "WO_NUMB='" + row["WO_NUMB"].ToString() + "' AND PRODUCT_TYPE='" + row["PRODUCT_TYPE"].ToString() + "'";

                    if (dttax.Count > 0)
                    {
                        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
          "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
          "style='font-size:10.0pt; font-family:Calibri; background:white;'>");
                        HttpContext.Current.Response.Write("<TR>");

                        foreach (DataColumn dtcol in dt_tax.Columns)
                        {
                            HttpContext.Current.Response.Write("<Td bgcolor=silver>");
                            HttpContext.Current.Response.Write("<B>");
                            HttpContext.Current.Response.Write(dtcol.ColumnName.Replace("_", " "));
                            HttpContext.Current.Response.Write("</B>");
                            HttpContext.Current.Response.Write("</Td>");

                        }
                        HttpContext.Current.Response.Write("</TR>");
                        for (int j = 0; j < dttax.Count; j++)
                        {
                            HttpContext.Current.Response.Write("<Tr>");
                            for (int i1 = 0; i1 < dttax.Table.Columns.Count; i1++)
                            {
                                HttpContext.Current.Response.Write("<Td >");
                                HttpContext.Current.Response.Write(dttax[j][i1].ToString());
                                HttpContext.Current.Response.Write("</Td>");

                            }
                            HttpContext.Current.Response.Write("</Tr>");
                        }
                        HttpContext.Current.Response.Write("</Table>");
                    }
                    HttpContext.Current.Response.Write("</Td>");

                }
            }

            HttpContext.Current.Response.Write("</TR>");
        }
        HttpContext.Current.Response.Write("</Table>");
        HttpContext.Current.Response.Write("</font>");
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
        //  HttpContext.Current.ApplicationInstance.CompleteRequest();
    }

    protected void ImportExcel_Click(object sender, ImageClickEventArgs e)
    {
        
        string BRANCH_CODE = string.Empty;
        string WO_NUM = string.Empty;
        string PRTY_CODE = string.Empty;
        string FROM_DATE = string.Empty;
        string TO_DATE = string.Empty;
        string PR_TYPE = "YARN";
        string COMP_CODE = string.Empty;
        int YEAR = 0;

        try
        {
            COMP_CODE = oUserLoginDetail.COMP_CODE;
            YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            if (ddlbranch.SelectedValue.ToString() != null && ddlbranch.SelectedValue.ToString() != string.Empty)
            {
                BRANCH_CODE = ddlbranch.SelectedValue.ToString();
            }
            else
            {
                BRANCH_CODE = string.Empty;
            }
            if (ddwo.SelectedValue.ToString() != null && ddwo.SelectedValue.ToString() != string.Empty)
            {
                WO_NUM = ddwo.SelectedValue.ToString();
            }
            else
            {
                WO_NUM = string.Empty;
            }
            if (ddprty.SelectedValue.ToString() != null && ddprty.SelectedValue.ToString() != string.Empty)
            {
                PRTY_CODE = ddprty.SelectedValue.ToString();
            }
            else
            {
                PRTY_CODE = string.Empty;
            }
            if (txtfrom.Text != null && txtfrom.Text != string.Empty && txtTo.Text != null && txtTo.Text != string.Empty)
            {
                FROM_DATE = txtfrom.Text;
                TO_DATE = txtTo.Text;
            }
            else
            {
                FROM_DATE = string.Empty;
                TO_DATE = string.Empty;
            }
            DataTable dt_trn = new DataTable();
            DataTable dt_bom = new DataTable();
            DataTable dt_tax = new DataTable();
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.OD_WO_MST.BindWoGrid(BRANCH_CODE, WO_NUM, PRTY_CODE, FROM_DATE, TO_DATE);
            DataSet ds = BindePoSupGrid(WO_NUM, PR_TYPE, COMP_CODE, BRANCH_CODE, YEAR);
            if (dt.Rows.Count > 0)
                dt_trn = ds.Tables[0];
             dt_bom =    ds.Tables[1];

             dt_tax = ds.Tables[2];
            ExportDataTableToExcel(dt, "Work Order Entry",dt_trn,dt_bom,dt_tax);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }



}