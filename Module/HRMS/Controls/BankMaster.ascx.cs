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
using System.Web.UI.WebControls.Adapters;
using Common;
using errorLog;
using System.IO;
using DBLibrary;
public partial class Module_HRMS_Controls_BankMaster : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Initial_Control();
                bindGvBankMaster();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex,"Problem In Page Loading"));
        }
    }
    private void Initial_Control()
    {
        try
        {
            txtBankCode.Text = string.Empty;
            txtBankName.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            lblMode.Text = "Save";            
            tdSave.Visible = true;
            txtBankCode.Enabled = true;
            chkActive.Checked = true;
            tdUpdate.Visible = false;
            tdFind.Visible = true;
            tdDelete.Visible = false;
            tdClear.Visible = true;
            ddlBankCode.Visible = false;
            Grid1.AutoPostBackOnSelect = false ;
        }
        catch
        {
            throw;
        }
    }
    private void bindGvBankMaster()
    {

        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.HR_BANK_MST.SelectBank();
            Grid1.DataSource = dt;
            Grid1.DataBind();
        }
        catch 
        {
            throw ;
        }

    }
    protected void imgbntSave_Click(object sender, ImageClickEventArgs e)
    {
       try
        {
            Insertdata();
        }
       catch (Exception ex)
       {
           Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Saving Records"));
       }
    }
    private void Insertdata()
    {
        try
        {

            if (txtBankCode.Text.Trim() != string.Empty  && txtBankName.Text.Trim() != null)
            {
                int iRecordFound = 0;
                string strNewBankId = string.Empty;
                SaitexDM.Common.DataModel.HR_BANK_MST oHR_BANK_MST = new SaitexDM.Common.DataModel.HR_BANK_MST();
                strNewBankId = SaitexBL.Interface.Method.HR_BANK_MST.getNewBankId();
                oHR_BANK_MST.BANK_ID = Convert.ToInt32(strNewBankId);
                oHR_BANK_MST.BANK_CODE = CommonFuction.funFixQuotes(txtBankCode.Text.Trim());
                oHR_BANK_MST.BANK_NAME = CommonFuction.funFixQuotes(txtBankName.Text.Trim().ToUpper());
                oHR_BANK_MST.BANK_REMARKS = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
                oHR_BANK_MST.STATUS = chkActive.Checked;
                oHR_BANK_MST.TDATE = System.DateTime.Now;
                oHR_BANK_MST.TUSER = Session["urLoginId"].ToString().Trim();
                bool bResult = SaitexBL.Interface.Method.HR_BANK_MST.InsertBankMaster(oHR_BANK_MST, out iRecordFound);
                if (bResult)
                {
                    Initial_Control();
                    bindGvBankMaster();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Saved Successfully');", true);
                }
                else if (iRecordFound > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Exists :Pls Enter Another Record');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Pls provide Bank Code.');", true);
                }
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
    private void UpdateData()
    {
        try
        {
                           
                int iRecordFound = 0;
                SaitexDM.Common.DataModel.HR_BANK_MST oHR_BANK_MST = new SaitexDM.Common.DataModel.HR_BANK_MST();
                string sStr = txtBankName.Text.ToUpper();
                txtBankName.Text = sStr;
                oHR_BANK_MST.BANK_NAME = CommonFuction.funFixQuotes(txtBankName.Text.Trim());
                oHR_BANK_MST.BANK_CODE = CommonFuction.funFixQuotes(txtBankCode.Text.Trim());
                oHR_BANK_MST.BANK_REMARKS = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
                oHR_BANK_MST.STATUS = chkActive.Checked;
                oHR_BANK_MST.TDATE = System.DateTime.Now;
                oHR_BANK_MST.TUSER = Session["urLoginId"].ToString().Trim();
                bool bResult = SaitexBL.Interface.Method.HR_BANK_MST.UpdateBankMaster(oHR_BANK_MST, out iRecordFound);
                if (bResult)
                {

                    Initial_Control();
                    bindGvBankMaster();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Updated Successfully');", true);
                }
               
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Saved');", true);
                }
            }
       
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        if (txtBankCode.Text.Trim() != "" && txtBankName.Text.Trim() != "")
        {
            UpdateData();
        }
        else
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('First Enter BankCode to update.');", true);
        }
    }
    protected void imgbtnDelete_Click2(object sender, ImageClickEventArgs e)
    {
       try
        {
            DeleteData();                 
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void DeleteData()
    {
        try
        {
            
            int iRecordFound = 0;
            string BankCode = CommonFuction.funFixQuotes(txtBankCode.Text.Trim());
            SaitexDM.Common.DataModel.HR_BANK_MST oHR_BANK_MST = new SaitexDM.Common.DataModel.HR_BANK_MST();
            oHR_BANK_MST.BANK_CODE = BankCode;
            oHR_BANK_MST.TDATE = System.DateTime.Now;
            oHR_BANK_MST.TUSER = Session["urLoginId"].ToString().Trim();
            bool bResult = SaitexBL.Interface.Method.HR_BANK_MST.DeleteBankMaster(oHR_BANK_MST, out iRecordFound);
            if (bResult)
            {
                Initial_Control();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Deleted Successfully');", true);
             }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('No such record exits.! Pls enter valid Category Code.');", true);
            }
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Exit Form"));
        }
    }   
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Initial_Control();
            bindGvBankMaster();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Clear Records"));
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "BankMasterReport.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','top=2,left=2,toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=1000,height=800');", true);
         }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Records Printing"));
        }
    }   
    protected void Grid1_Select(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
        ArrayList ar = Grid1.SelectedRecords;
        lblMode.Text = "Update";
        tdClear.Visible = true;
        tdDelete.Visible = true;
        tdUpdate.Visible = true;
        tdSave.Visible = false;
        Hashtable ht = (Hashtable)ar[0];
        txtBankCode.Text = ht["BANK_CODE"].ToString().Trim();
        txtBankName.Text = ht["BANK_NAME"].ToString().Trim();
        txtRemarks.Text = ht["BANK_REMARKS"].ToString().Trim();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    {
        try
        {
            string whereClause = " WHERE LTRIM(RTRIM(DEL_STATUS)) = '0' AND LTRIM(RTRIM(BANK_CODE)) like :SearchQuery or LTRIM(RTRIM(BANK_NAME)) like :SearchQuery And LTRIM(RTRIM(DEL_STATUS)) = '0'";
            string sortExpression = " ORDER BY BANK_ID";
            string commandText = "SELECT * FROM HR_BANK_MST";
            string sPO = string.Empty;
            DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
            return dt;
        }
        catch
        {
            throw;
        }
    }
    protected int GetItemsCount(string text)
    {
        try
        {
            string CommandText = "SELECT COUNT(*) FROM HR_BANK_MST WHERE LTRIM(RTRIM(BANK_ID)) like :SearchQuery And LTRIM(RTRIM(DEL_STATUS)) = '0' or LTRIM(RTRIM(BANK_NAME)) like :SearchQuery And LTRIM(RTRIM(DEL_STATUS)) = '0'";
            return SaitexBL.Interface.Method.HR_LV_TRN.GetCountForLOV(CommandText, text + '%', "");

        }
        catch
        {
            throw;
        }
    }
    protected void ddlBankCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = new DataTable();
            data = GetItems(e.Text, e.ItemsOffset, 10);

            ddlBankCode.Items.Clear();
            ddlBankCode.DataSource = data;
            ddlBankCode.DataBind();
            ddlBankCode.EmptyText = "Find Bank";
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;            
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Loading Records"));
        }
    }
    protected void ddlBankCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            string BANKCODE = ddlBankCode.SelectedValue.Trim();
            DataTable dt = SaitexBL.Interface.Method.HR_BANK_MST.SelectBankMaster(BANKCODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                txtBankName.Text = dt.Rows[0]["BANK_NAME"].ToString();
                txtBankCode.Text = dt.Rows[0]["BANK_CODE"].ToString();
                txtRemarks.Text = dt.Rows[0]["BANK_REMARKS"].ToString();
                tdSave.Visible = false;
                tdUpdate.Visible = true;
                tdDelete.Visible = true;
                lblMode.Text = "Update";
                txtBankCode.Enabled = true;
            }
            else
            {
                Initial_Control();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Selecting Records"));
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtBankCode.Enabled = false;
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            ddlBankCode.Visible = true;
            ddlBankCode.SelectedIndex = 0;
            lblMode.Text = "Update";
            Grid1.AutoPostBackOnSelect = true;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Finding Records"));
        }
    }
}
