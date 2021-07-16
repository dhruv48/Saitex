using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using DBLibrary;
using errorLog;
using Common;

public partial class Module_Inventory_Controls_MaterialIndentQueryForm : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                InitialBind();
                try
                {
                    bindGvGroupMaster();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading Page.\r\nSee error log for detail."));
        }
    }

    private void InitialBind()
    {
        try
        {
            getItemType();
            getItemCategory();
            getBrachName();
            getDepartment();
            BindDropDown(ddllocation);
            BindDepartment(ddlstore);
        }
        catch
        {
            throw;
        }
    }

    private void bindGvGroupMaster()
    {
        try
        {
            string DEPT_CODE = string.Empty;
            string BRANCH_CODE = string.Empty;
            string ITEM_TYPE = string.Empty;
            string ITEM_CATE = string.Empty;
            string LOCATION = string.Empty;
            string STORE = string.Empty;

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

            if (ddllocation.SelectedValue.ToString() != null && ddllocation.SelectedValue.ToString() != string.Empty)
            {
                LOCATION = ddllocation.SelectedValue.ToString();
            }
            else
            {
                LOCATION = string.Empty;
            }

            if (ddlstore.SelectedValue.ToString() != null && ddlstore.SelectedValue.ToString() != string.Empty)
            {
                STORE = ddlstore.SelectedValue.ToString();
            }
            else
            {
                STORE = string.Empty;
            }

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_ITEM_IND_MST.GetItemIndMst1(BRANCH_CODE, DEPT_CODE, ITEM_TYPE, ITEM_CATE,LOCATION,STORE);
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
                Common.CommonFuction.ShowMessage("Record Not Available For Selected Parameter");
            }
        }
        catch
        {
            throw;
        }
    }




    private DataTable bindGvGroupMaster1()
    {
        try
        {
            string DEPT_CODE = string.Empty;
            string BRANCH_CODE = string.Empty;
            string ITEM_TYPE = string.Empty;
            string ITEM_CATE = string.Empty;
            string LOCATION = string.Empty;
            string STORE = string.Empty;

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

            if (ddllocation.SelectedValue.ToString() != null && ddllocation.SelectedValue.ToString() != string.Empty)
            {
                LOCATION = ddllocation.SelectedValue.ToString();
            }
            else
            {
                LOCATION = string.Empty;
            }

            if (ddlstore.SelectedValue.ToString() != null && ddlstore.SelectedValue.ToString() != string.Empty)
            {
                STORE = ddlstore.SelectedValue.ToString();
            }
            else
            {
                STORE = string.Empty;
            }

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_ITEM_IND_MST.GetItemIndMst1(BRANCH_CODE, DEPT_CODE, ITEM_TYPE, ITEM_CATE, LOCATION, STORE);
            return dt;
        }
        catch
        {
            throw;
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
        catch
        {
            throw;
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
        catch
        {
            throw;
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
        catch
        {
            throw;
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

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Help Msg');", true);
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

        string DEPT_CODE = string.Empty;
        string BRANCH_CODE = string.Empty;
        string ITEM_TYPE = string.Empty;
        string ITEM_CATE = string.Empty;
        string LOCATION = string.Empty;
        string STORE = string.Empty;
        try
        {

            DataTable myDataTable = new DataTable();
            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "DEPT_CODE";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "BRANCH_CODE";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "ITEM_TYPE";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "ITEM_CATE";
            myDataTable.Columns.Add(myDataColumn);


            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "LOCATION";
            myDataTable.Columns.Add(myDataColumn);


            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "STORE";
            myDataTable.Columns.Add(myDataColumn);

            DataRow row;
            row = myDataTable.NewRow();
            row["DEPT_CODE"] = ddlDepartment.SelectedValue.ToString();
            row["BRANCH_CODE"] = ddlBranch.SelectedValue.ToString();
            row["ITEM_TYPE"] = ddlItemType.SelectedValue.ToString();
            row["ITEM_CATE"] = ddlItemCate.SelectedValue.ToString();
            row["LOCATION"] = ddllocation.SelectedValue.ToString();
            row["STORE"] = ddlstore.SelectedValue.ToString();

            myDataTable.Rows.Add(row);
            Session["IndentQueryReport"] = myDataTable;
            //Response.Redirect("~/Module/Inventory/Reports/Billqueryreport.aspx", false);
            string URL = "../Reports/IndentQueryForm_Mst.aspx";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=1000,height=800');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in getting print.\r\nSee error log for detail."));
        }
    }

    protected void Grid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            bindGvGroupMaster();

            Grid1.PageIndex = e.NewPageIndex;
            Grid1.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnGetData_Click(object sender, EventArgs e)
    {
        try
        {
            bindGvGroupMaster();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    private void BindDropDown(DropDownList ddllocation)
    {
        try
        {
        DataTable dt = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("WAREHOUSE_LOCATION", oUserLoginDetail.COMP_CODE);
        //if (dt != null && dt.Rows.Count > 0)
        //{
        

            ddllocation.DataSource = dt;
            ddllocation.DataValueField = "MST_DESC";
            ddllocation.DataTextField = "MST_DESC";
            ddllocation.DataBind();
            ddllocation.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
        //}
        //else
        //{
        //    ddllocation.DataSource = null;
        //    ddllocation.DataBind();
        //    ddllocation.Items.Insert(0, new ListItem(oUserLoginDetail.VC_BRANCHNAME, oUserLoginDetail.VC_BRANCHNAME));

        //}
        //ddllocation.SelectedIndex = ddllocation.Items.IndexOf(ddllocation.Items.FindByText(oUserLoginDetail.VC_BRANCHNAME));

    }
    private void BindDepartment(DropDownList ddlstore)
    {
        try
        {
            ddlstore.Items.Clear();
            DataTable dtDepartment = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
            //if (dtDepartment != null && dtDepartment.Rows.Count > 0)
            //{

                ddlstore.DataSource = dtDepartment;
                ddlstore.DataValueField = "DEPT_NAME";
                ddlstore.DataTextField = "DEPT_NAME";
                ddlstore.DataBind();
                ddlstore.Items.Insert(0, new ListItem("---------------All---------------", ""));
                dtDepartment.Dispose();
                dtDepartment = null;
            //}
            //ddlstore.SelectedIndex = ddlstore.Items.IndexOf(ddlstore.Items.FindByText(oUserLoginDetail.VC_DEPARTMENTNAME));
        }
        catch
        {
            throw;
        }
    }


    protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
    {
        string strFilename = "Material_Indent_Query_Form_" + DateTime.Now.ToString() + ".xls";
        ExporttoExcel(bindGvGroupMaster1(), strFilename, "Material Indent Query Form");

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
