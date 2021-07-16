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
using Obout.ComboBox;

public partial class Module_HRMS_Controls_WebUserControl : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                InitialiseData();
            }
            if ((Convert.ToInt16(Session["saveStatus"]) == 1) && (Request.QueryString["cId"].ToString().Trim() == "S"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Saved successfully');", true);
            }
            else if ((Convert.ToInt16(Session["saveStatus"]) == 1) && (Request.QueryString["cId"].ToString().Trim() == "U"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Updated successfully');", true);
            }
            else if ((Convert.ToInt16(Session["saveStatus"]) == 1) && (Request.QueryString["cId"].ToString().Trim() == "N"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Exists :Pls Enter Another Record');", true);
            }
            Session["saveStatus"] = 0;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in page loading"));
        }
    }
    private void InitialiseData()
    {
        try
            {

                BlankControls();
                SaitexDM.Common.DataModel.HR_SFT_MST oHR_SFT_MST = new SaitexDM.Common.DataModel.HR_SFT_MST();
                string strNewShiftId = SaitexBL.Interface.Method.HR_SFT_MST.GetNewShiftId();
                txtShiftCode.Text = Convert.ToString(Convert.ToInt32(strNewShiftId));                    

                bindComboShiftName();
                bindGvShiftMaster();
               
            }
         catch 
         {
             throw;
         }
    }
    private void bindComboShiftName()
    {
        try
        {
            SaitexDM.Common.DataModel.HR_SFT_MST oHR_SFT_MST = new SaitexDM.Common.DataModel.HR_SFT_MST();
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.HR_SFT_MST.SelectShiftName();
            cmbShiftName.DataSource = dt;
            cmbShiftName.DataValueField = "SFT_ID";
            cmbShiftName.DataTextField = "SFT_NAME";
            cmbShiftName.DataBind();
        }
        catch 
        {
            throw;
        }
    }
    private void bindGvShiftMaster()
    {

        try
        {
            SaitexDM.Common.DataModel.HR_SFT_MST oHR_SFT_MST = new SaitexDM.Common.DataModel.HR_SFT_MST();
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.HR_SFT_MST.SelectShiftMaster();
            Grid1.DataSource = dt;
            Grid1.DataBind();
        }
        catch 
        {
            throw ;
        }
    }  
    private void Insertdata()
    {
        try
        {
            if (cmbShiftName.SelectedValue != null)
            {       
                int iRecordFound = 0;
                string strNewShiftId = string.Empty;
                           
                SaitexDM.Common.DataModel.HR_SFT_MST oHR_SFT_MST = new SaitexDM.Common.DataModel.HR_SFT_MST();
                strNewShiftId = SaitexBL.Interface.Method.HR_SFT_MST.GetNewShiftId();
                oHR_SFT_MST.SFT_ID = Convert.ToInt32(strNewShiftId);
                oHR_SFT_MST.SFT_IN_TIME = CommonFuction.funFixQuotes(txtInTime.Text.Trim());
                string sStr = txtShiftName.Text.ToUpper();
                txtShiftName.Text = sStr;
                oHR_SFT_MST.SFT_NAME = CommonFuction.funFixQuotes(txtShiftName.Text.Trim());
                oHR_SFT_MST.SFT_OUT_TIME = CommonFuction.funFixQuotes(txtOutTime.Text.Trim());
                oHR_SFT_MST.SFT_RLX_TIME = CommonFuction.funFixQuotes(txtRelaxation.Text.Trim());
                oHR_SFT_MST.SFT_OVR_TIME = CommonFuction.funFixQuotes(txtOverTime.Text.Trim());
                oHR_SFT_MST.SFT_MIN_WRK_HOUR = CommonFuction.funFixQuotes(txtMinWorkingHour.Text.Trim());
                oHR_SFT_MST.SFT_MIN_HLD_HOUR = CommonFuction.funFixQuotes(txtHourstobe_Hoilday.Text.Trim());
                oHR_SFT_MST.SFT_MIN_SHORT_DAY_HOUR = CommonFuction.funFixQuotes(txthourstobe_Shortleave.Text.Trim());
                oHR_SFT_MST.SFT_LNCH_TIME = CommonFuction.funFixQuotes(txtLunchTime.Text.Trim());
                          
                oHR_SFT_MST.STATUS = chkActive.Checked;
                oHR_SFT_MST.TDATE = System.DateTime.Now;
                oHR_SFT_MST.TUSER = Session["urLoginId"].ToString().Trim();
                bool bResult = SaitexBL.Interface.Method.HR_SFT_MST.InsertShiftMaster(oHR_SFT_MST, out iRecordFound);
                if (bResult == true)
                {
                    BlankControls();
                    cmbShiftName.Visible = true;
                    btnAddShiftName.Visible = true;
                    txtShiftName.Visible = false;
                    lnkBack.Visible = false;
                    lnkSave.Visible = false;
                    Grid1.AutoPostBackOnSelect = false;
                    bindGvShiftMaster();
                    Session["saveStatus"] = 1;
                    Response.Redirect("./ShiftMaster.aspx?cId=S", false);
                  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Saved Successfully');", true);
                }
                if( iRecordFound >0)
                {
                    Session["saveStatus"] = 1;
                    Response.Redirect("./ShiftMaster.aspx?cId=N", false);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Exists :Pls Enter Another Record');", true);
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
            
                int iRecordFound = 0;
                SaitexDM.Common.DataModel.HR_SFT_MST oHR_SFT_MST = new SaitexDM.Common.DataModel.HR_SFT_MST();
                oHR_SFT_MST.SFT_ID = Convert.ToInt32(cmbShiftName.SelectedValue.Trim());
                oHR_SFT_MST.SFT_IN_TIME = CommonFuction.funFixQuotes(txtInTime.Text.Trim());
                oHR_SFT_MST.SFT_NAME = CommonFuction.funFixQuotes(txtShiftName.Text.Trim().ToUpper());
                oHR_SFT_MST.SFT_OUT_TIME = CommonFuction.funFixQuotes(txtOutTime.Text.Trim());
                oHR_SFT_MST.SFT_RLX_TIME = CommonFuction.funFixQuotes(txtRelaxation.Text.Trim());
                oHR_SFT_MST.SFT_OVR_TIME = CommonFuction.funFixQuotes(txtOverTime.Text.Trim());
                oHR_SFT_MST.SFT_MIN_WRK_HOUR = CommonFuction.funFixQuotes(txtMinWorkingHour.Text.Trim());
                oHR_SFT_MST.SFT_MIN_HLD_HOUR = CommonFuction.funFixQuotes(txtHourstobe_Hoilday.Text.Trim());
                oHR_SFT_MST.SFT_MIN_SHORT_DAY_HOUR = CommonFuction.funFixQuotes(txthourstobe_Shortleave.Text.Trim());
                oHR_SFT_MST.SFT_LNCH_TIME = CommonFuction.funFixQuotes(txtLunchTime.Text.Trim());
                oHR_SFT_MST.STATUS = chkActive.Checked;               
                oHR_SFT_MST.TUSER = Session["urLoginId"].ToString().Trim();
                bool bResult = SaitexBL.Interface.Method.HR_SFT_MST.UpdateShiftMaster(oHR_SFT_MST, out iRecordFound);
              
                if (bResult)
                {
                    BlankControls();                                      
                    bindGvShiftMaster();
                    Session["saveStatus"] = 1;
                    Response.Redirect("./ShiftMaster.aspx?cId=U", false);                    
                }               
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Updated');", true);
                }
            }
       
        catch 
        {
            throw ;
        }
    } 
    private void BlankControls()
    {
        try
        {
            tdSave.Visible = true;
            lblMode.Text = "Save";
           
           
            lnkSave.Visible = false;            
            cmbShiftName.Visible = false;
            btnAddShiftName.Visible = false;
            lnkBack.Visible = false;
            txtShiftCode.Enabled = false;
            chkActive.Checked = true;
            tdUpdate.Visible = false;
            tdClear.Visible = true;

            txtShiftName.Text = string.Empty;

            cmbShiftName.SelectedIndex = -1;
            txtHourstobe_Hoilday.Text = string.Empty;
            txthourstobe_Shortleave.Text = string.Empty ;
            txtInTime.Text = string.Empty;
            txtLunchTime.Text = string.Empty;
            txtMinWorkingHour.Text = string.Empty;
            txtOutTime.Text = string.Empty;
            txtOverTime.Text = string.Empty;
            txtRelaxation.Text = string.Empty;
            txtShiftCode.Text = string.Empty;
        }
        catch 
        {
            throw ;
        }
    }
    protected void lnkSave_Click(object sender, EventArgs e)
    {
        try
        {
             bindComboShiftName();
        }
        catch (Exception ex)
        {
            throw ;
        }
    }
    protected void btnAddShiftName_Click(object sender, EventArgs e)
    {
      try
        {   
        cmbShiftName.Visible = false;
        btnAddShiftName.Visible = false;
        txtShiftName.Visible = true;
        lnkBack.Visible = true;        
      }
      catch (Exception ex)
      {
          errorLog.ErrHandler.WriteError(ex.Message);
      }
    }
    protected void lnkBack_Click(object sender, EventArgs e)
    {
        try
        {   
            btnAddShiftName.Visible = true;
            cmbShiftName.Visible = true;
            txtShiftName.Visible = false;
            lnkBack.Visible = false;        
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
         try
         {   
            Insertdata();
         }
         catch (Exception ex)
         {
             errorLog.ErrHandler.WriteError(ex.Message);
         }
    }
    protected void Grid1_Select(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {   
            ArrayList ar = Grid1.SelectedRecords;
            lblMode.Text = "Update";
            tdClear.Visible = true;
            tdUpdate.Visible = true;
            tdSave.Visible = false;
            Hashtable ht = (Hashtable)ar[0];
            cmbShiftName.SelectedValue = ht["SFT_ID"].ToString().Trim();
            cmbShiftName.SelectedText = ht["SFT_NAME"].ToString().Trim();
            txtShiftName.Text = ht["SFT_NAME"].ToString().Trim();
            txtHourstobe_Hoilday.Text = ht["SFT_MIN_HLD_HOUR"].ToString();
            txthourstobe_Shortleave.Text = ht["SFT_MIN_SHORT_DAY_HOUR"].ToString();
            txtInTime.Text = ht["SFT_IN_TIME"].ToString();
            txtLunchTime.Text = ht["SFT_LNCH_TIME"].ToString();
            txtMinWorkingHour.Text = ht["SFT_MIN_WRK_HOUR"].ToString();
            txtOutTime.Text = ht["SFT_OUT_TIME"].ToString();
            txtOverTime.Text = ht["SFT_OVR_TIME"].ToString();
            txtRelaxation.Text = ht["SFT_RLX_TIME"].ToString();
            txtShiftCode.Text = ht["SFT_ID"].ToString();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
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
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {   
            Updatedata();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
                
    }
    protected void  imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {   
            BlankControls();
            tdClear.Visible = false;
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            lblMode.Text = "Save";
         }
        catch (Exception ex)
        {
         errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    {
        string whereClause = " WHERE SFT_ID like :SearchQuery And DEL_STATUS = '0' or SFT_NAME like :SearchQuery And DEL_STATUS = '0'";
        string sortExpression = " ORDER BY SFT_ID";
        string commandText = "SELECT * FROM HR_SFT_MST";
        string sPO = string.Empty;
        DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, string.Empty, text + '%', sPO);
        return dt;
    }
    protected int GetItemsCount(string text)
    {
        try
        {
            string CommandText = "SELECT COUNT(*) FROM HR_SFT_MST WHERE SFT_ID like :SearchQuery And DEL_STATUS = '0' or SFT_NAME like :SearchQuery And DEL_STATUS = '0'";
            return SaitexBL.Interface.Method.HR_LV_TRN.GetCountForLOV(CommandText, text + '%', string.Empty);
        }
        catch 
        {
            throw; 
        }
    }
    protected void cmbShiftName_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {

            DataTable data = new DataTable();
            data = GetItems(e.Text, e.ItemsOffset, 10);
            cmbShiftName.Items.Clear();
            cmbShiftName.DataSource = data;
            cmbShiftName.DataBind();
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Shift Loading"));
        }
    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {    
        try
        {
            lblMode.Text = "Update";
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            txtShiftName.Visible = false;
            cmbShiftName.Visible = true;
            Grid1.AutoPostBackOnSelect = true;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Finding"));
        } 
    }
    protected void ddlShift_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {

            SaitexDM.Common.DataModel.HR_SFT_MST oHR_SFT_MST = new SaitexDM.Common.DataModel.HR_SFT_MST();
            DataTable dt = SaitexBL.Interface.Method.HR_SFT_MST.SelectShiftMaster();
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["SFT_ID"] = dt.Rows[0]["SFT_ID"].ToString();
                cmbShiftName.SelectedValue = dt.Rows[0]["SFT_ID"].ToString().Trim();
                txtHourstobe_Hoilday.Text = dt.Rows[0]["SFT_MIN_HLD_HOUR"].ToString();
                txthourstobe_Shortleave.Text = dt.Rows[0]["SFT_MIN_SHORT_DAY_HOUR"].ToString();
                txtInTime.Text = dt.Rows[0]["SFT_IN_TIME"].ToString();
                txtLunchTime.Text = dt.Rows[0]["SFT_LNCH_TIME"].ToString();
                txtMinWorkingHour.Text = dt.Rows[0]["SFT_MIN_WRK_HOUR"].ToString();
                txtOutTime.Text = dt.Rows[0]["SFT_OUT_TIME"].ToString();
                txtOverTime.Text = dt.Rows[0]["SFT_OVR_TIME"].ToString();
                txtRelaxation.Text = dt.Rows[0]["SFT_RLX_TIME"].ToString();
                txtShiftCode.Text = dt.Rows[0]["SFT_ID"].ToString();
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
    protected void ddlShift_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = new DataTable();
            data = GetItems(e.Text, e.ItemsOffset, 10);
            cmbShiftName.Items.Clear();
            cmbShiftName.DataSource = data;
            cmbShiftName.DataBind();
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch(Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Loading Shift"));
        }
    }
    protected void imgbtnPrint_Click1(object sender, ImageClickEventArgs e)
    {
         try
          {
             string URL = "ShiftMasterReport.aspx";
             ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=10000,height=800');", true);
          }
         catch (Exception ex)
         {
             throw ex;
         }
     }
    protected void imgbtnUpdate_Click1(object sender, ImageClickEventArgs e)
    {
         try
         {
                Updatedata();
         }
         catch (Exception ex)
         {
             Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in update the records"));
         }
    }
    protected void cmbShiftName_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {

            SaitexDM.Common.DataModel.HR_SFT_MST oHR_SFT_MST = new SaitexDM.Common.DataModel.HR_SFT_MST();
            string SftId = cmbShiftName.SelectedValue.Trim();
            DataTable dt = SaitexBL.Interface.Method.HR_SFT_MST.SelectShiftDetails(SftId);
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["SFT_ID"] = dt.Rows[0]["SFT_ID"].ToString();
                txtShiftCode.Text = dt.Rows[0]["SFT_ID"].ToString().Trim();
                txtHourstobe_Hoilday.Text = dt.Rows[0]["SFT_MIN_HLD_HOUR"].ToString();
                txthourstobe_Shortleave.Text = dt.Rows[0]["SFT_MIN_SHORT_DAY_HOUR"].ToString();
                txtInTime.Text = dt.Rows[0]["SFT_IN_TIME"].ToString();
                txtLunchTime.Text = dt.Rows[0]["SFT_LNCH_TIME"].ToString();
                txtMinWorkingHour.Text = dt.Rows[0]["SFT_MIN_WRK_HOUR"].ToString();
                txtOutTime.Text = dt.Rows[0]["SFT_OUT_TIME"].ToString();
                txtOverTime.Text = dt.Rows[0]["SFT_OVR_TIME"].ToString();
                txtRelaxation.Text = dt.Rows[0]["SFT_RLX_TIME"].ToString();
            }
            
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Shift Changing"));
        }
    }
    protected void imgbtnClear_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            BlankControls();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Clear the controls"));
        }

    }
}


