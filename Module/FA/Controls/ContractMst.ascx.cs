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

using errorLog;
using Common;
using DBLibrary;
using System.IO;

public partial class Module_FA_Controls_ContractMst : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.FA_CONTRACT_MST oFA_CONTRACT_MST;
    private static DataTable dtDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                InitialisePage();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }
    
    protected void btnSaveDetail_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlTaxCode.SelectedIndex > -1 || ddlTaxCode.SelectedValue != "")
            {
                if (dtDetail == null)
                    CreateDataTable();

                string strContract_Code = txtContractCode.Text.ToUpper().Trim();
                string strTax_Code = ddlTaxCode.SelectedValue.ToUpper().ToString().Trim();
                float fRate = float.Parse(txtRate.Text.Trim());

                if (txtRate.Text != "" && txtRate.Text == "")
                {
                    Common.CommonFuction.ShowMessage("Please enter Rate first.");
                }
                else
                {
                    if (fRate > 0)
                    {
                        int UNIQUE_ID = 0;
                        if (ViewState["UNIQUE_ID"] != null)
                            UNIQUE_ID = int.Parse(ViewState["UNIQUE_ID"].ToString());

                        if (CheckDuplicateRow(strTax_Code, UNIQUE_ID))
                        {
                            Common.CommonFuction.ShowMessage("Tax already included");
                        }
                        else
                        {
                            if (UNIQUE_ID > 0)
                            {
                                DataView dvEdit = new DataView(dtDetail);
                                dvEdit.RowFilter = "TAX_CODE='" + strTax_Code + "' and UNIQUE_ID=" + UNIQUE_ID;
                                if (dvEdit.Count > 0)
                                {
                                    dvEdit[0]["CONTRACT_CODE"] = strContract_Code;
                                    dvEdit[0]["TAX_CODE"] = strTax_Code;
                                    dvEdit[0]["TAX_RATE"] = fRate;

                                    dtDetail.AcceptChanges();
                                }
                            }
                            else
                            {
                                DataRow dr = dtDetail.NewRow();
                                dr["UNIQUE_ID"] = dtDetail.Rows.Count + 1;
                                dr["CONTRACT_CODE"] = strContract_Code;
                                dr["TAX_CODE"] = strTax_Code;
                                dr["TAX_RATE"] = fRate;

                                dtDetail.Rows.Add(dr);
                            }
                            RefreshDetailRow();
                        }
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Please enter rate first.");
                    }
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please select tax first.");
            }
            BindGridFromTable();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Save Detail.\r\nSee error log for detail."));
        }
    }
    
    private bool CheckDuplicateRow(string strTax_Code, int UniqueId)
    {
        try
        {
            bool IsDuplicate = false;
            if (dtDetail != null)
            {
                DataView dv = new DataView(dtDetail);
                dv.RowFilter = "TAX_CODE='" + strTax_Code + "' and UNIQUE_ID<>" + UniqueId;
                if (dv.Count > 0)
                {
                    IsDuplicate = true;
                }
            }
            return IsDuplicate;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    
    private void RefreshDetailRow()
    {
        try
        {
            ddlTaxCode.SelectedIndex = -1;
            txtRate.Text = "";
            ViewState["UNIQUE_ID"] = null;
            ddlTaxCode.Focus();
        }
        catch
        {
            throw;
        }
    }
    
    private void BindGridFromTable()
    {
        try
        {
            if (dtDetail != null)
            {
                DataView dv = new DataView(dtDetail);
                dv.Sort = "TAX_CODE ASC";
                dtDetail.AcceptChanges();
            }

            grdTaxdetails.DataSource = dtDetail;
            grdTaxdetails.DataBind();
        }
        catch { throw; }
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            RefreshDetailRow();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Cancel Button..\r\nSee error log for detail."));
        }
    }
    
    protected void imgbtnNew_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            SaveContractEntry();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving.\r\nSee error log for detail."));
        }
    }
    
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            UpdateContractEntry();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating..\r\nSee error log for detail."));
        }
    }
    
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Deletion..\r\nSee error log for detail."));
        }
    }
    
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtContractCode.Visible = false;
            ddlContractCode.Visible = true;
            ddlContractCode.SelectedIndex = -1;
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            lblMode.Text = "Update";
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in find..\r\nSee error log for detail."));
        }
    }
    
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./ContractMaster.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear..\r\nSee error log for detail."));
        }
    }
    
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing..\r\nSee error log for detail."));
        }
    }
    
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Help..\r\nSee error log for detail."));
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
                Response.Redirect("~/Admin/Pages/welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit..\r\nSee error log for detail."));
        }
    }
    
    protected void ddlContractCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            bindContractCodeDropdown();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Contract Code..\r\nSee error log for detail."));
        }
    }
    
    protected void ddlContractCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            FillEditDataByContractCode(ddlContractCode.SelectedValue.Trim());
            
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Contract Code..\r\nSee error log for detail."));
        }
    }
    
    private void InitialisePage()
    {
        try
        {
            MaxContractCode();
            BlankControls();
            bindContractCodeDropdown();

            tdSave.Visible = true;
            tdDelete.Visible = false;
            tdUpdate.Visible = false;
            bindTaxDropdown();
        }
        catch
        {
            throw;
        }
    }
    
    private void BlankControls()
    {
        try
        {
            dtDetail = null;
            grdTaxdetails.DataSource = null;
            grdTaxdetails.DataBind();
            txtSection.Text = "";
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            txtDescription.Text = "";
            ddlTaxCode.SelectedIndex = -1;
            txtRate.Text = "";
            lblMode.Text = "Save";
            ddlContractCode.Visible = false;
            txtContractCode.Visible = true;
            lblMessage.Text = "";
        }
        catch
        {
            throw;
        }
    }
    
    private void MaxContractCode()
    {
        try
        {
            string x = "";
            int y = 0;
            DataTable dt = SaitexBL.Interface.Method.FA_CONTRACT_MST.SelectMaxContractCode();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        x = dv[iLoop]["MAX_ID"].ToString();
                        y = int.Parse(x);
                        y = y + 1;
                        txtContractCode.Text = y.ToString();
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }
    
    private void bindTaxDropdown()
    {
        try
        {
            ddlTaxCode.Items.Clear();

            DataTable dt = SaitexBL.Interface.Method.FA_TAX_MST.SelectAll();

            if (dt != null && dt.Rows.Count > 0)
            {
                ddlTaxCode.DataValueField = "TAX_CODE";
                ddlTaxCode.DataTextField = "TAX_CODE";
                ddlTaxCode.DataSource = dt;
                ddlTaxCode.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }
    
    protected void ddlTaxCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            bindTaxDropdown();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Tax Code..\r\nSee error log for detail."));
        }
    }
    
    protected void ddlTaxCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            txtRate.Focus();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Tax Code..\r\nSee error log for detail."));
        }
    }
    
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }
    
    private void CreateDataTable()
    {
        try
        {
            dtDetail = new DataTable();
            dtDetail.Columns.Add("UNIQUE_ID", typeof(int));
            dtDetail.Columns.Add("CONTRACT_CODE", typeof(string));
            dtDetail.Columns.Add("TAX_CODE", typeof(string));
            dtDetail.Columns.Add("TAX_RATE", typeof(float));
        }
        catch
        {
            throw;
        }
    }
    
    protected void grdTaxdetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int Unique_id = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "EditTRN")
            {
                EditTaxTRNRow(Unique_id);
            }
            else if (e.CommandName == "DeleteTRN")
            {
                DeleteTaxTRNRow(Unique_id);
                BindGridFromTable();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid RowCommand..\r\nSee error log for detail."));
        }
    }
    
    private void EditTaxTRNRow(int UNIQUEID)
    {
        try
        {
            DataView dv = new DataView(dtDetail);
            dv.RowFilter = "UNIQUE_ID=" + UNIQUEID;
            if (dv.Count > 0)
            {
                RefreshDetailRow();
                ddlTaxCode.SelectedValue = dv[0]["TAX_CODE"].ToString();
                txtRate.Text = dv[0]["TAX_RATE"].ToString();
                ViewState["UNIQUE_ID"] = UNIQUEID;
            }
        }
        catch
        {
            throw;
        }
    }
    
    private void DeleteTaxTRNRow(int UNIQUEID)
    {
        try
        {
            if (dtDetail.Rows.Count == 1)
            {
                dtDetail.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtDetail.Rows)
                {
                    int iUNIQUEID = int.Parse(dr["UNIQUE_ID"].ToString());
                    if (iUNIQUEID == UNIQUEID)
                    {
                        dtDetail.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtDetail.Rows)
                {
                    iCount = iCount + 1;
                    dr["UNIQUE_ID"] = iCount;
                }
            }
        }
        catch
        {
            throw;
        }
    }
    
    private void SaveContractEntry()
    {
        try
        {
            if (dtDetail != null && dtDetail.Rows.Count > 0)
            {
                SaitexDM.Common.DataModel.FA_CONTRACT_MST oFA_CONTRACT_MST = new SaitexDM.Common.DataModel.FA_CONTRACT_MST();

                oFA_CONTRACT_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oFA_CONTRACT_MST.CONTRACT_CODE = txtContractCode.Text.ToUpper().Trim();
                oFA_CONTRACT_MST.CONTRACT_DESC = txtDescription.Text.Trim();
                oFA_CONTRACT_MST.SECTION = txtSection.Text.ToUpper().Trim();
                oFA_CONTRACT_MST.START_DATE = DateTime.Parse(txtStartDate.Text.Trim());
                oFA_CONTRACT_MST.END_DATE = DateTime.Parse(txtEndDate.Text.Trim());
                oFA_CONTRACT_MST.TUSER = oUserLoginDetail.UserCode;

                bool bResult = SaitexBL.Interface.Method.FA_CONTRACT_MST.Insert(oFA_CONTRACT_MST, dtDetail);
                if (bResult)
                {
                    Common.CommonFuction.ShowMessage("Contract Entry Saved Successfully!!");
                    InitialisePage();
                    txtDescription.Text = "";
                    ddlTaxCode.SelectedIndex = -1;
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Contract Entry Saving failed.");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please provide Rate entries.");
            }
        }
        catch
        {
            throw;
        }
    }
    
    private void bindContractCodeDropdown()
    {
        try
        {
            ddlContractCode.Items.Clear();

            oFA_CONTRACT_MST = new SaitexDM.Common.DataModel.FA_CONTRACT_MST();

            oFA_CONTRACT_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;

            DataTable dt = SaitexBL.Interface.Method.FA_CONTRACT_MST.SelectMST_All(oFA_CONTRACT_MST);

            if (dt != null && dt.Rows.Count > 0)
            {
                ddlContractCode.DataValueField = "CONTRACT_CODE";
                ddlContractCode.DataTextField = "CONTRACT_CODE";
                ddlContractCode.DataSource = dt;
                ddlContractCode.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }
    
    private void FillEditDataByContractCode(string strContractCode)
    {
        try
        {
            oFA_CONTRACT_MST = new SaitexDM.Common.DataModel.FA_CONTRACT_MST();

            oFA_CONTRACT_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_CONTRACT_MST.CONTRACT_CODE = strContractCode;

            DataTable dt = SaitexBL.Interface.Method.FA_CONTRACT_MST.SelectMST_ByContractCode(oFA_CONTRACT_MST);

            if (dt != null && dt.Rows.Count > 0)
            {
                txtContractCode.Text = dt.Rows[0]["CONTRACT_CODE"].ToString();
                txtSection.Text = dt.Rows[0]["SECTION"].ToString();
                txtStartDate.Text = dt.Rows[0]["START_DATE"].ToString();
                txtEndDate.Text = dt.Rows[0]["END_DATE"].ToString();
                txtDescription.Text = dt.Rows[0]["CONTRACT_DESC"].ToString();

                DataTable dt1 = FillEditTRNDataByContractCode(strContractCode, oFA_CONTRACT_MST);
                MapDataTable(dt1);
                BindGridFromTable();
            }
        }
        catch
        {
            throw;
        }
    }
    
    private DataTable FillEditTRNDataByContractCode(string strContractCode, SaitexDM.Common.DataModel.FA_CONTRACT_MST oFA_CONTRACT_MST)
    {
        try
        {
            oFA_CONTRACT_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_CONTRACT_MST.CONTRACT_CODE = strContractCode;

            DataTable dt = SaitexBL.Interface.Method.FA_CONTRACT_MST.SelectTRNByCONTRACT_CODE(oFA_CONTRACT_MST);
            return dt;
        }
        catch
        {
            throw;
        }
    }
    
    private void MapDataTable(DataTable dt)
    {
        try
        {
            if (dtDetail == null)
                CreateDataTable();

            dtDetail.Rows.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                DataRow drNew = dtDetail.NewRow();
                drNew["UNIQUE_ID"] = dtDetail.Rows.Count + 1;

                drNew["CONTRACT_CODE"] = dr["CONTRACT_CODE"];
                drNew["TAX_CODE"] = dr["TAX_CODE"];
                drNew["TAX_RATE"] = dr["TAX_RATE"];

                dtDetail.Rows.Add(drNew);
            }
        }
        catch
        {
            throw;
        }
    }
    
    private void UpdateContractEntry()
    {
        try
        {
            if (dtDetail != null && dtDetail.Rows.Count > 0)
            {
                SaitexDM.Common.DataModel.FA_CONTRACT_MST oFA_CONTRACT_MST = new SaitexDM.Common.DataModel.FA_CONTRACT_MST();

                oFA_CONTRACT_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oFA_CONTRACT_MST.CONTRACT_CODE = txtContractCode.Text.ToUpper().Trim();
                oFA_CONTRACT_MST.CONTRACT_DESC = txtDescription.Text.Trim();
                oFA_CONTRACT_MST.SECTION = txtSection.Text.ToUpper().Trim();
                oFA_CONTRACT_MST.START_DATE = DateTime.Parse(txtStartDate.Text.Trim());
                oFA_CONTRACT_MST.END_DATE = DateTime.Parse(txtEndDate.Text.Trim());
                oFA_CONTRACT_MST.TUSER = oUserLoginDetail.UserCode;

                bool bResult = SaitexBL.Interface.Method.FA_CONTRACT_MST.Update(oFA_CONTRACT_MST, dtDetail);
                if (bResult)
                {
                    Common.CommonFuction.ShowMessage("Contract Entry Updated Successfully!!");
                    InitialisePage();
                    txtDescription.Text = "";
                    ddlTaxCode.SelectedIndex = -1;
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Contract Entry Updating failed.");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please provide Rate entries.");
            }
        }
        catch
        {
            throw;
        }
    }
}