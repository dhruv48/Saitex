using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.OracleClient;

using errorLog;
using Common;

public partial class Module_Inventory_Controls_ItemMasterQry : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                getItemType();
                getItemCategory();
                getBrachName();
                getDepartment();
                getISEXCISABLE();
                getCONSUME();
                getSTATUS();
                ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;


                try
                {
                    bindGvItemMaster();
                }
                catch (Exception ex)
                {
                    throw ex; 
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Page loading.\r\nSee error log for detail."));
        }
    }
   

    private void getBrachName()
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            string strCompanyCode = oUserLoginDetail.COMP_CODE.Trim().ToString();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(strCompanyCode);
            DataView Dv = new DataView(dt);
            ddlBranch.DataSource = Dv;
            ddlBranch.DataValueField = "BRANCH_CODE";
            ddlBranch.DataTextField = "BRANCH_NAME";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }

    private void getDepartment()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
            ddlDepartment.DataSource = dt;
            ddlDepartment.DataValueField = "DEPT_CODE";
            ddlDepartment.DataTextField = "DEPT_NAME";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (OracleException ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Fill Department.\r\nSee error log for detail."));
        }

        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Department.\r\nSee error log for detail."));
        }
    }

    private void getItemType()
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetItemType();
            DataView Dv = new DataView(dt);
            ddlItemType.DataSource = Dv;
            ddlItemType.DataValueField = "ITEM_TYPE";
            ddlItemType.DataTextField = "ITEM_TYPE";
            ddlItemType.DataBind();
            ddlItemType.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }

    private void getItemCategory()
    {
        try
        {

            DataTable dt = new DataTable();

            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetItemCate();
            ddlItemCate.DataSource = dt;
            ddlItemCate.DataValueField = "CAT_CODE";
            ddlItemCate.DataTextField = "CAT_CODE";
            ddlItemCate.DataBind();
            ddlItemCate.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (OracleException ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Fill Department.\r\nSee error log for detail."));
        }

        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Department.\r\nSee error log for detail."));
        }
    }
    private void getISEXCISABLE()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetISEXCISABLE();
            ddlISEXCISABLE.DataSource = dt;
            ddlISEXCISABLE.DataValueField = "IS_EXCISABLE";
            ddlISEXCISABLE.DataTextField = "IS_EXCISABLE";
            ddlISEXCISABLE.DataBind();
            ddlISEXCISABLE.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }

    private void getCONSUME()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetCONSUME();
            ddlCONSUME.DataSource = dt;
            ddlCONSUME.DataValueField = "CONSUME";
            ddlCONSUME.DataTextField = "CONSUME";
            ddlCONSUME.DataBind();
            ddlCONSUME.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }

    private void getSTATUS()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetSTATUS();
            ddlSTATUS.DataSource = dt;
            ddlSTATUS.DataValueField = "STATUS";
            ddlSTATUS.DataTextField = "STATUS";
            ddlSTATUS.DataBind();
            ddlSTATUS.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }


    private DataTable bindGvItemMaster()
    {
        string DEPT_CODE = string.Empty;
        string BRANCH_CODE = string.Empty;
        string ITEM_TYPE = string.Empty;
        string ITEM_CATE = string.Empty;
        string IS_EXCISABLE = string.Empty;
        string CONSUME = string.Empty;
        string STATUS = string.Empty;
      

        try
        {
           

            if (ddlBranch.SelectedValue.ToString() != null && ddlBranch.SelectedValue.ToString() != string.Empty)
            {
                BRANCH_CODE = ddlBranch.SelectedValue.ToString();
            }
            else
            {
                BRANCH_CODE = string.Empty;
            }


            if (ddlDepartment.SelectedValue.ToString() != null && ddlDepartment.SelectedValue.ToString() != string.Empty)
            {
                DEPT_CODE = ddlDepartment.SelectedValue.ToString();
            }
            else
            {
                DEPT_CODE = string.Empty;
            }

            if (ddlItemCate.SelectedValue.ToString() != null && ddlItemCate.SelectedValue.ToString() != string.Empty)
            {
                ITEM_CATE = ddlItemCate.SelectedValue.ToString();
            }
            else
            {
                ITEM_CATE = string.Empty;
            }

            if (ddlItemType.SelectedValue.ToString() != null && ddlItemType.SelectedValue.ToString() != string.Empty)
            {
                ITEM_TYPE = ddlItemType.SelectedValue.ToString();
            }
            else
            {
                ITEM_TYPE = string.Empty;
            }

            if (ddlISEXCISABLE.SelectedValue.ToString() != null && ddlISEXCISABLE.SelectedValue.ToString() != string.Empty)
            {
                string is_excis = ddlISEXCISABLE.SelectedValue.ToString();
                if (is_excis == "Yes")
                {
                    IS_EXCISABLE = "1";
                }
                else if (is_excis == "No")
                {
                    IS_EXCISABLE = "0";
                }
                else
                {
                    IS_EXCISABLE = string.Empty;
                }

            }

            if (ddlCONSUME.SelectedValue.ToString() != null && ddlCONSUME.SelectedValue.ToString() != string.Empty)
            {
                string consume = ddlCONSUME.SelectedValue.ToString();
                if (consume == "Yes")
                {
                    CONSUME = "1";
                }
                else if (consume == "No")
                {
                    CONSUME = "0";
                }

                else
                {
                    CONSUME = string.Empty;
                }
            }
            if (ddlSTATUS.SelectedValue.ToString() != null && ddlSTATUS.SelectedValue.ToString() != string.Empty)
            {
                string status = ddlSTATUS.SelectedValue.ToString();
                if (status == "Open")
                {
                    STATUS = "1";
                }
                else if (status == "Close")
                {
                    STATUS = "0";
                }

                else
                {
                    STATUS = string.Empty;
                }
            }

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.ItemMaster.SelectItemMaster1(BRANCH_CODE, DEPT_CODE, ITEM_TYPE, ITEM_CATE, IS_EXCISABLE,CONSUME,STATUS,ddlApprovalStatus.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                Grid1.DataSource = dt;
                Grid1.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                Grid1.Visible = true;
            }
            else
            {
                Grid1.DataSource = null;
                Grid1.DataBind();
                lblTotalRecord.Text = "0";
                Grid1.Visible = false;
                Common.CommonFuction.ShowMessage("Record Not Available By This Parameter.");

            }
            return dt;
        }

        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            bindGvItemMaster();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void Grid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            bindGvItemMaster();

            Grid1.PageIndex = e.NewPageIndex;
            Grid1.DataBind();
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
            string QueryString = "";
            bool flag = false;

            if (ddlBranch.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "CH_BRANCHCODE=" + ddlBranch.SelectedValue.Trim();
                flag = true;
            }
            if (ddlDepartment.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "DEPARTMENT=" + ddlDepartment.SelectedValue.Trim();
                flag = true;
            }
            if (ddlItemType.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "ITEM_TYPE=" + ddlItemType.SelectedValue.Trim();
                flag = true;
            }
            if (ddlItemCate.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "VC_CATEGORYCODE=" + ddlItemCate.SelectedValue.Trim();
                flag = true;
            }
            if (ddlSTATUS.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "STATUS=" + ddlSTATUS.SelectedValue.Trim();
                flag = true;
            }
            if (ddlISEXCISABLE.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "IS_EXCISABLE=" + ddlISEXCISABLE.SelectedValue.Trim();
                flag = true;
            }
            if (ddlCONSUME.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "CONSUME=" + ddlCONSUME.SelectedValue.Trim();
                flag = true;
            }
            if (ddlApprovalStatus.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "IS_APPROVED=" + ddlApprovalStatus.SelectedValue.Trim();
                flag = true;
            }
            
            string URL = "../Reports/ItemMasterReport.aspx" + QueryString;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Open Print Page.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./ItemMasterQuery.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Clear.\r\nSee error log for detail."));
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgBtnAddNew.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Add  record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }
    protected void imgBtnAddNew_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Inventory/Pages/ItemMaster.aspx");
    }
    protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlISEXCISABLE_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Grid1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlCONSUME_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlSTATUS_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
    {
        string strFilename = "Item_Master_List_" + DateTime.Now.ToString() + ".xls";
        ExporttoExcel(bindGvItemMaster(), strFilename, "Item Master List");

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

