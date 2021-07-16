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

public partial class Module_HRMS_Controls_LeaveMapping : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                lblMode.Text = "Save";
                bindComboLeave();
                bindComboMappedLeave();
                bindGvLeaveMapping();
                chkActive.Checked = true;
                tdUpdate.Visible = false;
                tdClear.Visible = true;
                Grid1.AutoPostBackOnSelect = false;

            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Page loading.\r\nSee error log for detail."));
        }
       
    }
    private void bindComboLeave()
    {
        try
        {

            SaitexDM.Common.DataModel.HR_LV_TRN oHR_LV_TRN = new SaitexDM.Common.DataModel.HR_LV_TRN();
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.HR_LV_TRN.SelectLeaveTransaction();
            cmbLeave.DataValueField = "LV_ID";
            cmbLeave.DataTextField = "LV_NAME";
            cmbLeave.DataBind();

        }

        catch 
        {
            throw;
        }

    }
    private void bindComboMappedLeave()
    {
        try
        {
            SaitexDM.Common.DataModel.HR_LV_TRN oHR_LV_TRN = new SaitexDM.Common.DataModel.HR_LV_TRN();
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.HR_LV_TRN.SelectLeaveTransaction();
            cmbMappedLeave.DataValueField = "LV_ID";
            cmbMappedLeave.DataTextField = "LV_NAME";
            cmbMappedLeave.DataBind();

        }

        catch 
        {
            throw;
        }

    }
    private void bindGvLeaveMapping()
    {

        try
        {
            SaitexDM.Common.DataModel.HR_LV_MPP oHR_LV_MPP = new SaitexDM.Common.DataModel.HR_LV_MPP();
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.HR_LV_MPP.SelectLeaveMapping();
            Grid1.DataSource = dt;
            Grid1.DataBind();
            
        }


        catch 
        {
            throw;
        }        

    }
    private void Insertdata()
    {
        try
        {
            if (cmbLeave.SelectedValue.Trim() != null && cmbMappedLeave.SelectedValue.Trim() !=null)
            {
                if (cmbLeave.SelectedValue.Trim() != cmbMappedLeave.SelectedValue.Trim())
                {
                int iRecordFound = 0;
                int iInvalidCheck = 0;
                string strNewLeaveMappId = "";
                SaitexDM.Common.DataModel.HR_LV_MPP oHR_LV_MPP = new SaitexDM.Common.DataModel.HR_LV_MPP();
                strNewLeaveMappId = SaitexBL.Interface.Method.HR_LV_MPP.GetNewLeaveMappId();
                oHR_LV_MPP.LV_MPP_ID = Convert.ToInt32(strNewLeaveMappId);
                oHR_LV_MPP.LV_ID_1 = Convert.ToInt32(cmbLeave.SelectedValue.Trim());
                oHR_LV_MPP.LV_ID_2 = Convert.ToInt32(cmbMappedLeave.SelectedValue.Trim());
                oHR_LV_MPP.STATUS = chkActive.Checked;
                oHR_LV_MPP.TDATE = System.DateTime.Now;
                oHR_LV_MPP.TUSER = Session["urLoginId"].ToString().Trim();
                bool bResult = SaitexBL.Interface.Method.HR_LV_MPP.InsertLeaveMapping(oHR_LV_MPP, out iRecordFound,out iInvalidCheck);
                if (bResult)
                {

                    cmbLeave.SelectedIndex = 0;
                    cmbMappedLeave.SelectedIndex = 0;
                    bindGvLeaveMapping();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Saved Successfully');", true);
                }
                else if (iRecordFound > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Exists :Pls Enter Another Record');", true);
                }
                else if (iInvalidCheck > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Invalid Mapping :Pls Enter Another Record');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Saved');", true);
                }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Invalid Mapping :Pls Enter Another Record');", true);
                }
            }
        }
        catch 
        {
            throw ;
        }
    }
    private void Updatedata()
    {
        try
        {

            if (cmbLeave.SelectedValue.Trim() != null && cmbMappedLeave.SelectedValue.Trim() != null)
            {
                if (cmbLeave.SelectedValue.Trim() != cmbMappedLeave.SelectedValue.Trim())
                {
                    int iRecordFound = 0;
                    int iInvalidCheck = 0;
                    SaitexDM.Common.DataModel.HR_LV_MPP oHR_LV_MPP = new SaitexDM.Common.DataModel.HR_LV_MPP();
                    oHR_LV_MPP.LV_MPP_ID = Convert.ToInt32(ViewState["LV_MPP_ID"]);
                    oHR_LV_MPP.LV_ID_1 = Convert.ToInt32(cmbLeave.SelectedValue.Trim());
                    oHR_LV_MPP.LV_ID_2 = Convert.ToInt32(cmbMappedLeave.SelectedValue.Trim());
                    oHR_LV_MPP.STATUS = chkActive.Checked;
                    oHR_LV_MPP.TDATE = System.DateTime.Now;
                    oHR_LV_MPP.TUSER = Session["urLoginId"].ToString().Trim();
                    bool bResult = SaitexBL.Interface.Method.HR_LV_MPP.UpdateLeaveMapping(oHR_LV_MPP, out iRecordFound, out iInvalidCheck);
                    if (bResult)
                    {
                        cmbLeave.SelectedIndex = -1;
                        cmbMappedLeave.SelectedIndex = -1;
                        bindGvLeaveMapping();
                        Grid1.AutoPostBackOnSelect = false;
                        tdSave.Visible = true;
                        tdUpdate.Visible = false;
                        lblMode.Text = "Save";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Updated Successfully');", true);
                    }
                    else if (iInvalidCheck > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Invalid Mapping :Pls Enter Another Record');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Update');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Invalid Mapping :Pls Enter Another Record');", true);
                   
                }
            }
        }
        catch 
        {
            throw;
        }
    }
    
    protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    {

        string whereClause = " WHERE LV_ID like :SearchQuery And DEL_STATUS = '0' or LV_NAME like :SearchQuery And DEL_STATUS = '0'";
        string sortExpression = " ORDER BY LV_ID";
        string commandText = "SELECT * FROM HR_LV_MST";
        string sPO = "";
        DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
        return dt;

    }
    protected int GetItemsCount(string text)
    {

        string CommandText = "SELECT COUNT(*) FROM HR_LV_MST WHERE LV_ID like :SearchQuery And DEL_STATUS = '0' or LV_NAME like :SearchQuery And DEL_STATUS = '0'";
        return SaitexBL.Interface.Method.HR_LV_TRN.GetCountForLOV(CommandText, text + '%', "");


    }
   
    protected void cmbLeave_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
         try
        {

        DataTable data = new DataTable();
        data = GetItems(e.Text, e.ItemsOffset, 10);

        cmbLeave.Items.Clear();
        cmbLeave.DataSource = data;
        cmbLeave.DataBind();
      
        // Calculating the numbr of items loaded so far in the ComboBox
        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

        // Getting the total number of items that start with the typed text
        e.ItemsCount = GetItemsCount(e.Text);
        }
         catch (Exception ex)
         {
             Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Leave Loading.\r\nSee error log for detail."));
         }
    }
    protected void cmbMappedLeave_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
         try
        {

        DataTable data = new DataTable();
        data = GetItems(e.Text, e.ItemsOffset, 10);

        cmbMappedLeave.Items.Clear();
        cmbMappedLeave.DataSource = data;
        cmbMappedLeave.DataBind();

        // Calculating the numbr of items loaded so far in the ComboBox
        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

        // Getting the total number of items that start with the typed text
        e.ItemsCount = GetItemsCount(e.Text);
            }
         catch (Exception ex)
         {
             Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Leave.\r\nSee error log for detail."));
         }
    }
    protected void imgbtnSave_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {

        Insertdata();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Leave Saving.\r\nSee error log for detail."));
        }
    }
    protected void Grid1_Select(object sender, Obout.Grid.GridRecordEventArgs e)
    {
         try
        {

        DataTable data = new DataTable();
        data = GetItems("", 0, 10);
        cmbLeave.Items.Clear();
        cmbLeave.DataSource = data;
        cmbLeave.DataBind();
        cmbMappedLeave.Items.Clear();
        cmbMappedLeave.DataSource = data;
        cmbMappedLeave.DataBind();
        ArrayList ar = Grid1.SelectedRecords;
        lblMode.Text = "Update";
        tdUpdate.Visible = true;
        tdSave.Visible = false;
        Hashtable ht = (Hashtable)ar[0];
        ViewState["LV_MPP_ID"] = ht["LV_MPP_ID"].ToString().Trim();
        cmbLeave.SelectedValue = ht["LV_ID_1"].ToString().Trim();
        cmbMappedLeave.SelectedValue = ht["LV_ID_2"].ToString().Trim();
          }
       
       catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Selecting.\r\nSee error log for detail."));
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Leave Exit.\r\nSee error log for detail."));
        }
    }
   
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
    try
        {

       Updatedata();
      }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Leave updating.\r\nSee error log for detail."));
        }
    
                
    }
    protected void imgbtnDelete_Click2(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
     try
        {

        string URL = "LeaveMappingReport.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','top=2,left=2,toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=1000,height=800');", true);
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
        cmbMappedLeave.SelectedIndex = -1;
        cmbLeave.SelectedIndex = -1;

        lblMode.Text = "Save";
        tdUpdate.Visible = false;
        tdSave.Visible = true;
       }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
     try
        {
        Grid1.AutoPostBackOnSelect = true;
        lblMode.Text = "Update";
        tdUpdate.Visible = true;
        tdSave.Visible = false;
       }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
