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
using Common;
using errorLog;
using System.IO;
using DBLibrary;

public partial class Module_HRMS_Controls_YearlyLeave : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            lblMode.Text = "Save";
            bindComboLeaveTran();
            bindGvLeaveTransaction();
            txtYear.Text = System.DateTime.Now.Year.ToString();
            tdFind.Visible = true;
            chkActive.Checked = true;
            tdUpdate.Visible = false;
            tdClear.Visible = true;
            bindDDLLeave();
            ddlLeave.Visible = false;
        }
       
    }
    private void bindDDLLeave()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.HR_LV_TRN.SelectLeaveTransaction();
            ddlLeave.DataSource = dt;
            ddlLeave.DataValueField = "LV_ID";
            ddlLeave.DataTextField = "LV_NAME";
            ddlLeave.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;
        }

    }
    private void bindComboLeaveTran()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.HR_LV_MST.SelectLeaveMaster();
            cmbLeaveTran.DataSource = dt;
            cmbLeaveTran.DataValueField = "LV_ID";
            cmbLeaveTran.DataTextField = "LV_NAME";
            cmbLeaveTran.DataBind();
          
        }

        catch (Exception ex)
        {
            throw ex;
        }
        
    }
    
    private void bindGvLeaveTransaction()
    {

        try
        {
            SaitexDM.Common.DataModel.HR_LV_TRN oHR_LV_TRN = new SaitexDM.Common.DataModel.HR_LV_TRN();
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.HR_LV_TRN.SelectLeaveTransaction();
            Grid1.DataSource = dt;
            Grid1.DataBind();
            
        }


        catch (Exception ex)
        {
            throw ex;
        }

    }
    private void BlankControls()
    {
        txtLeaveDays.Text = "";
        txtPeriod.Text = "";
        txtRemarks.Text = "";
        cmbLeaveTran.SelectedIndex = -1;
        radCarrying_forward_leave.SelectedIndex = 0;
    }
    private void Insertdata()
    {
        try
        {
            if (ddlLeave.SelectedValue.Trim() != null && txtYear.Text != null)
            {
                int iRecordFound = 0;
                SaitexDM.Common.DataModel.HR_LV_TRN oHR_LV_TRN = new SaitexDM.Common.DataModel.HR_LV_TRN();
                oHR_LV_TRN.LV_ID = Convert.ToInt32(cmbLeaveTran.SelectedValue.Trim());
                oHR_LV_TRN.YEAR = Convert.ToInt32(txtYear.Text.Trim());
                oHR_LV_TRN.REMARKS = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
                oHR_LV_TRN.LV_DAY = CommonFuction.funFixQuotes(txtLeaveDays.Text.Trim());
                oHR_LV_TRN.LV_FRWD = CommonFuction.funFixQuotes(radCarrying_forward_leave.SelectedValue.Trim());
                oHR_LV_TRN.LV_PRD_TYPE = CommonFuction.funFixQuotes(txtPeriod.Text.Trim());
                oHR_LV_TRN.LV_PRD_VAL = CommonFuction.funFixQuotes(radLeaveApplicable.SelectedValue.Trim());
                oHR_LV_TRN.STATUS = chkActive.Checked;
                oHR_LV_TRN.TDATE = System.DateTime.Now;
                oHR_LV_TRN.TUSER = Session["urLoginId"].ToString().Trim();
                bool bResult = SaitexBL.Interface.Method.HR_LV_TRN.InsertLeaveTransaction(oHR_LV_TRN, out iRecordFound);

                if (bResult)
                {

                    BlankControls();
                    bindGvLeaveTransaction();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Saved Successfully');", true);
                }

                else if (iRecordFound > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Exists :Pls Enter Another Record');", true);
                }

                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Saved');", true);
                }
            }
        }

        catch (Exception ex)
        {
            throw ex;
        }
}
    
    protected void imgbtnSave_Click1(object sender, ImageClickEventArgs e)
    {
        Insertdata();
    }
    private void Updatedata()
    {
        try
        {
           
                int iRecordFound = 0;
                SaitexDM.Common.DataModel.HR_LV_TRN oHR_LV_TRN = new SaitexDM.Common.DataModel.HR_LV_TRN();
                oHR_LV_TRN.LV_ID = Convert.ToInt32(ViewState["LV_ID"]);
                oHR_LV_TRN.YEAR = Convert.ToInt32(txtYear.Text.Trim());
                oHR_LV_TRN.REMARKS = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
                oHR_LV_TRN.LV_DAY = CommonFuction.funFixQuotes(txtLeaveDays.Text.Trim());
                oHR_LV_TRN.LV_FRWD = CommonFuction.funFixQuotes(radCarrying_forward_leave.SelectedValue.Trim());
                oHR_LV_TRN.LV_PRD_TYPE = CommonFuction.funFixQuotes(txtPeriod.Text.Trim());
                oHR_LV_TRN.LV_PRD_VAL = CommonFuction.funFixQuotes(radLeaveApplicable.SelectedValue.Trim());
                oHR_LV_TRN.STATUS = chkActive.Checked;
                oHR_LV_TRN.TDATE = System.DateTime.Now;
                oHR_LV_TRN.TUSER = Session["urLoginId"].ToString().Trim();
                bool bResult = SaitexBL.Interface.Method.HR_LV_TRN.UpdateLeaveTransaction(oHR_LV_TRN, out iRecordFound);
                if (bResult)
                {

                    BlankControls();
                    ddlLeave.Visible = false;
                    tdUpdate.Visible = false;
                    tdSave.Visible = true;
                    lblMode.Text = "Save";
                    bindGvLeaveTransaction();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Updated Successfully');", true);
                }

                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Updated');", true);
                }
            }
    
        catch (Exception ex)
        {
            throw ex;
        }
}
    protected DataTable GetMItems(string text, int startOffset, int numberOfItems)
    {

        string whereClause = " WHERE LV_ID like :SearchQuery And DEL_STATUS = '0'";
        string sortExpression = " ORDER BY LV_ID";
        string commandText = "SELECT * FROM HR_LV_MST";
        string sPO = "";
        DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
        return dt;

    }
    protected int GetMItemsCount(string text)
    {

        string CommandText = "SELECT COUNT(*) FROM HR_LV_MST WHERE LV_ID like :SearchQuery And DEL_STATUS = '0' ";
        return SaitexBL.Interface.Method.HR_LV_TRN.GetCountForLOV(CommandText, text + '%', "");


    }
    
    protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    {

        string whereClause = " where m.LV_ID=t.LV_ID and m.LV_NAME like :SearchQuery And t.DEL_STATUS = '0'";
            string sortExpression = " ORDER BY t.LV_ID";
            string commandText = "select distinct m.LV_NAME,t.LV_ID,t.LV_DAY,t.YEAR,t.LV_FRWD,t.REMARKS,t.LV_PRD_TYPE,t.LV_PRD_VAL from HR_LV_MST m,HR_LV_TRN t";
            string sPO = "";
            DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
            return dt;
       
    }
    protected int GetItemsCount(string text)
    {
        
            string CommandText = "SELECT COUNT(*) FROM HR_LV_TRN WHERE LV_ID like :SearchQuery And DEL_STATUS = '0' or YEAR like :SearchQuery And DEL_STATUS = '0'";
            return SaitexBL.Interface.Method.HR_LV_TRN.GetCountForLOV(CommandText, text + '%', "");
      
        
    }
    protected void cmbLeaveTran_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        DataTable data = new DataTable();
        data = GetMItems(e.Text, e.ItemsOffset, 10);
           
        cmbLeaveTran.Items.Clear();
        cmbLeaveTran.DataSource = data;
        cmbLeaveTran.DataBind();

        // Calculating the numbr of items loaded so far in the ComboBox
        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

        // Getting the total number of items that start with the typed text
        e.ItemsCount = GetMItemsCount(e.Text);
    }
    protected void radLeaveApplicable_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Grid1_Select(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        DataTable data = new DataTable();
        data = GetItems("", 0, 10);

        cmbLeaveTran.Items.Clear();
        cmbLeaveTran.DataSource = data;
        cmbLeaveTran.DataBind();

        ArrayList ar = Grid1.SelectedRecords;

        lblMode.Text = "Update";
        txtYear.Enabled = false;
        tdUpdate.Visible = true;
        tdSave.Visible = false;
        Hashtable ht = (Hashtable)ar[0];
        ViewState["LV_ID"] = ht["LV_ID"].ToString().Trim();
        cmbLeaveTran.SelectedValue = ht["LV_ID"].ToString().Trim();
        txtLeaveDays.Text = ht["LV_DAY"].ToString().Trim();
        txtPeriod.Text = ht["LV_PRD_TYPE"].ToString().Trim();
        txtRemarks.Text = ht["REMARKS"].ToString().Trim();
        txtYear.Text = ht["YEAR"].ToString().Trim();
        radCarrying_forward_leave.SelectedValue = ht["LV_FRWD"].ToString().Trim();

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
    
    protected void ddlLeave_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {

            SaitexDM.Common.DataModel.HR_LV_TRN oHR_LV_TRN = new SaitexDM.Common.DataModel.HR_LV_TRN();
            int LV_ID = Convert.ToInt32(ddlLeave.SelectedValue.Trim());
            DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.SelectLeave(LV_ID);
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["LV_ID"] = dt.Rows[0]["LV_ID"].ToString();
                cmbLeaveTran.SelectedValue = dt.Rows[0]["LV_ID"].ToString();
                txtRemarks.Text = dt.Rows[0]["REMARKS"].ToString();
                txtYear.Text = dt.Rows[0]["YEAR"].ToString();
                txtPeriod.Text = dt.Rows[0]["LV_PRD_TYPE"].ToString();
                txtLeaveDays.Text = dt.Rows[0]["LV_DAY"].ToString();

                if (dt.Rows[0]["LV_PRD_VAL"].ToString().Trim() == "PE")
                    radLeaveApplicable.SelectedValue = "PE";
                else if (dt.Rows[0]["LV_PRD_VAL"].ToString().Trim() == "NP")
                    radLeaveApplicable.SelectedValue = "NP";
                else
                    radLeaveApplicable.SelectedIndex = -1;
                             
                tdSave.Visible = false;
                tdUpdate.Visible = true;
                lblMode.Text = "Update";

            }
            else
            {
                tdSave.Visible = true;
                tdUpdate.Visible = false;
                lblMode.Text = "Save";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        Updatedata();
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        BlankControls();
        tdUpdate.Visible = false;
        tdSave.Visible = true;
        lblMode.Text = "Save";
        ddlLeave.Visible = false;

    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string URL = "YearlyLeaveReport.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','top=2,left=2,toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=1000,height=800');", true); 

    }
    
    protected void ddlLeave_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        DataTable data = new DataTable();
        data = GetItems(e.Text, e.ItemsOffset, 10);
        ddlLeave.Items.Clear();
        ddlLeave.DataSource = data;
        ddlLeave.DataBind();

        // Calculating the numbr of items loaded so far in the ComboBox
        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

        // Getting the total number of items that start with the typed text
        e.ItemsCount = GetItemsCount(e.Text);
    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
       
        tdSave.Visible = false;
        tdUpdate.Visible = true;
        lblMode.Text = "Update";
        ddlLeave.SelectedIndex = -1;
        ddlLeave.Visible = true;
    }
}

