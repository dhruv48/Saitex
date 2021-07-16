using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using errorLog;

public partial class Module_Fiber_Controls_FiberMasterQuery : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (!IsPostBack)
            {
                InitialControls();
                BindControls();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void BindControls()
    {
        try
        {
            GetBranchName();
            GetFiberCat();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void GetBranchName()
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            string strCompanyCode = oUserLoginDetail.COMP_CODE.Trim().ToString();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(strCompanyCode);
            DataView Dv = new DataView(dt);
            ddlBranchCode.DataSource = Dv;
            ddlBranchCode.DataValueField = "BRANCH_CODE";
            ddlBranchCode.DataTextField = "BRANCH_NAME";
            ddlBranchCode.DataBind();
            ddlBranchCode.Items.Insert(0, new ListItem("---------------All---------------", string.Empty));
            dt.Dispose();
            dt = null;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void GetFiberCat()
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            string Mst_Name = "FIBER_MASTER";
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.GetFiberCat(Mst_Name);
            DataView Dv = new DataView(dt);
            ddlFiberCat.DataSource = Dv;
            ddlFiberCat.DataValueField = "MST_CODE";
            ddlFiberCat.DataTextField = "MST_CODE";
            ddlFiberCat.DataBind();
            ddlFiberCat.Items.Insert(0, new ListItem(".............ALL..............", string.Empty));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void GetFiberSubCat()
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            string Mst_Name = "FIBER_MASTER";
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.GetFiberCat(Mst_Name);
            DataView Dv = new DataView(dt);
            ddlFiberCat.DataSource = Dv;
            ddlFiberCat.DataValueField = "MST_CODE";
            ddlFiberCat.DataTextField = "MST_CODE";
            ddlFiberCat.DataBind();
            ddlFiberCat.Items.Insert(0, new ListItem(".............ALL..............", string.Empty));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void GetUserQuery()
    {
        string BRANCH_CODE = string.Empty;
        string FIBER_CAT = string.Empty;

        try 
	{	        
		if(ddlBranchCode.SelectedValue.ToString() !=null && ddlBranchCode.SelectedValue.ToString() !=string.Empty)
        {
            BRANCH_CODE = ddlBranchCode.SelectedValue.ToString();
        }
            else
        {
            BRANCH_CODE = string.Empty;
        }
        if (ddlFiberCat.SelectedValue.ToString() != null && ddlFiberCat.SelectedValue.ToString() != string.Empty)
        {
            FIBER_CAT = ddlFiberCat.SelectedValue.ToString();
        }
        else
        {
            FIBER_CAT = string.Empty;
        }
	
     
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_FIBER_MST.GetUserQuery(BRANCH_CODE, FIBER_CAT);
            if (dt.Rows.Count > 0)
            {

                grdFiberMasterQuery.DataSource = dt;
                grdFiberMasterQuery.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                grdFiberMasterQuery.Visible = true;
            }
            else
            {
                grdFiberMasterQuery.DataSource = null;
                grdFiberMasterQuery.DataBind();
                Common.CommonFuction.ShowMessage("Data not found by selected item");
                lblTotalRecord.Text = "0";
            }
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
            InitialControls();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void InitialControls()
    {
        try
        {
            imgbtnClear.Visible = true;
            imgbtnExit.Visible = true;
            imgbtnHelp.Visible = true;

            ddlBranchCode.SelectedIndex = -1;
            ddlFiberCat.SelectedIndex = -1;
            grdFiberMasterQuery.SelectedIndex = -1;
             grdFiberMasterQuery.Visible = false;
            lblTotalRecord.Text = "0";
            GetUserQuery();
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
            throw ex;
        }
    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    { }
    protected void btngetrecord_Click1(object sender, EventArgs e)
    {
        try
        {
            GetUserQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void grUserMasterQuery_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //try
        //{
        //    grdFiberMasterQuery.PageIndex = e.NewPageIndex;
        //    grdFiberMasterQuery.DataBind();
        //    GetUserQuery();
        //}
        //catch
        //{
        //}
        try
        {
            GetUserQuery();

            grdFiberMasterQuery.PageIndex = e.NewPageIndex;
            grdFiberMasterQuery.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void ddlBranchCode_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void ddlFiberCat_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void imgbtnPrint_Click1(object sender, ImageClickEventArgs e)
    {
        DataTable myDataTable = new DataTable();
        DataColumn myDataColumn;      

        myDataColumn = new DataColumn();
        myDataColumn.DataType = Type.GetType("System.String");
        myDataColumn.ColumnName = "FIBER_CAT";
        myDataTable.Columns.Add(myDataColumn);

        myDataColumn = new DataColumn();
        myDataColumn.DataType = Type.GetType("System.String");
        myDataColumn.ColumnName = "BRANCH_CODE";
        myDataTable.Columns.Add(myDataColumn);

        DataRow row;

        row = myDataTable.NewRow();       
        row["FIBER_CAT"] = ddlFiberCat.Text;
        row["BRANCH_CODE"] = ddlBranchCode.Text;
        myDataTable.Rows.Add(row);
        Session["fiberreportdt"] = myDataTable;

        string URL = "../Reports/FiberMasterReport.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
    }
   
}
  

    

  



   
  

