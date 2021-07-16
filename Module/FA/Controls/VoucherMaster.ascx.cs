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

using errorLog;
using Common;
using DBLibrary;
using System.IO;

public partial class Module_FA_Controls_VoucherMaster : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.FA_VCHR_MST oFA_VCHR_MST;
    bool chStatus;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            if (!IsPostBack)
            {
                InitialisePage();
            }
            if (Convert.ToInt16(Session["saveStatus"]) == 1)
            {
                if (Request.QueryString["cId"].ToString().Trim() == "S")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Saved successfully');", true);
                }
                if (Request.QueryString["cId"].ToString().Trim() == "U")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Updated successfully');", true);
                }
                if (Request.QueryString["cId"].ToString().Trim() == "D")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Deleted successfully');", true);
                }
                Session["saveStatus"] = 0;
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }

    private void InitialisePage()
    {
        try
        {
            lblErrorMessage.Text = "";
            lblMessage.Text = "";
            grdVoucher.AutoPostBackOnSelect = false;
            lblMode.Text = "Save";
            txtVoucherCode.ReadOnly = true;
            cmbVoucherCode.Visible = false;
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            tdFind.Visible = true;
            chk_Status.Checked = true;

            BlankControls();
            MaxVoucherCode();
            BindVoucherType();
            BindVoucherCombo();
            BindGrid();
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
            txtVoucherName.Text = "";
            txtPrefix.Text = "";
            txtSuffix.Text = "";
            txtDescription.Text = "";
            cmbVoucherCode.SelectedIndex = -1;
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InsertData();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving the data.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblMessage.Text = "";
            txtVoucherCode.Visible = false;
            cmbVoucherCode.Visible = true;
            grdVoucher.AutoPostBackOnSelect = true;
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            lblMode.Text = "Update";
            BlankControls();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Finding the data..\r\nSee error log for detail."));
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            UpdateData();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating the data.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //DeleteData();
            Common.CommonFuction.ShowMessage("Sorry! Dear you can't delete any Voucher");
            lblMessage.Text = "Sorry! Dear you can't delete any Voucher";
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in deleting the data.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./VoucherMaster.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clearing the page.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
        }
    }

    private void BindVoucherType()
    {
        try
        {
            cmbVoucherType.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.FA_VCHR_MST.GetVoucherTypeMOM();
            if (dt != null && dt.Rows.Count > 0)
            {
                cmbVoucherType.DataSource = dt;
                cmbVoucherType.DataValueField = "MST_CODE";
                cmbVoucherType.DataTextField = "MST_CODE";
                cmbVoucherType.DataBind();
                cmbVoucherType.Items.Insert(0, new ListItem("--- Select Voucher Type ---", "0"));
            }
        }
        catch
        {
            throw;
        }
    }

    private void MaxVoucherCode()
    {
        try
        {
            string x = "";
            int y = 0;
            DataTable dt = SaitexBL.Interface.Method.FA_VCHR_MST.GetMaxVoucherCode();

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
                        txtVoucherCode.Text = y.ToString();
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "../Reports/Voucher_Mst_Rpt.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "../Help/VoucherMasterHelp.htm";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in helping.\r\nSee error log for detail."));
        }
    }

    protected void cmbVoucherCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            lblMessage.Text = "";
            DataTable dt = SaitexBL.Interface.Method.FA_VCHR_MST.GetVoucherMasterWithOutStatus();
            char chCheck;
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "VCHR_CODE='" + cmbVoucherCode.SelectedValue.ToString().Trim() + "'";
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        txtVoucherCode.Text = dv[iLoop]["VCHR_CODE"].ToString();
                        txtVoucherName.Text = dv[iLoop]["VCHR_NAME"].ToString();
                        cmbVoucherType.SelectedValue = dv[iLoop]["VCHR_TYPE"].ToString();
                        txtPrefix.Text = dv[iLoop]["VCHR_PREFIX"].ToString();
                        txtSuffix.Text = dv[iLoop]["VCHR_SUFFIX"].ToString();
                        txtDescription.Text = dv[iLoop]["VCHR_DESC"].ToString();
                        chCheck = char.Parse(dv[iLoop]["STATUS"].ToString());
                        if (chCheck == '1')
                        {
                            chk_Status.Checked = true;
                        }
                        else
                        {
                            chk_Status.Checked = false;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selection Voucher Code.\r\nSee error log for detail."));
        }
    }

    protected void cmbVoucherCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            BindVoucherCombo();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading Voucher Code.\r\nSee error log for detail."));
        }
    }

    private void InsertData()
    {
        try
        {
            if (txtVoucherCode.Text != "")
            {
                if (txtVoucherName.Text != "")
                {
                    if (cmbVoucherType.SelectedIndex != 0)
                    {
                        if (txtPrefix.Text != "")
                        {
                            oFA_VCHR_MST = new SaitexDM.Common.DataModel.FA_VCHR_MST();

                            if (chk_Status.Checked == true)
                            {
                                chStatus = true;
                            }
                            else
                            {
                                chStatus = false;
                            }
                            oFA_VCHR_MST.STATUS = chStatus;
                            oFA_VCHR_MST.VCHR_CODE = txtVoucherCode.Text.Trim();
                            oFA_VCHR_MST.VCHR_NAME = txtVoucherName.Text.ToUpper().Trim();
                            oFA_VCHR_MST.VCHR_TYPE = cmbVoucherType.SelectedValue.ToString().Trim();
                            oFA_VCHR_MST.VCHR_PREFIX = txtPrefix.Text.ToUpper().Trim();
                            oFA_VCHR_MST.VCHR_SUFFIX = txtSuffix.Text.ToUpper().Trim();
                            oFA_VCHR_MST.VCHR_DESC = txtDescription.Text.Trim();
                            oFA_VCHR_MST.DEL_STATUS = false;
                            oFA_VCHR_MST.TUSER = oUserLoginDetail.UserCode;
                            oFA_VCHR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                            oFA_VCHR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;

                            int iRecordFound = 0;

                            bool bResult = SaitexBL.Interface.Method.FA_VCHR_MST.InsertVoucherMaster(oFA_VCHR_MST, out iRecordFound);

                            if (bResult)
                            {
                                Session["saveStatus"] = 1;
                                Response.Redirect("./VoucherMaster.aspx?cId=S", false);
                            }
                            else if (iRecordFound > 0)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('This Record is already saved.. Please enter another.');", true);
                            }
                        }
                        else
                        {
                            Common.CommonFuction.ShowMessage("Dear! Please enter Prefix...");
                        }
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Dear! Please select Voucher Type...");
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Dear! Please enter Voucher Name...");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Dear! Please enter Voucher Code...");
            }
        }
        catch
        {
            throw;
        }
    }

    private void UpdateData()
    {
        try
        {
            bool chStatus;
            if (cmbVoucherCode.SelectedIndex != -1)
            {
                if (txtVoucherName.Text != "")
                {
                    if (cmbVoucherType.SelectedIndex != 0)
                    {
                        if (txtPrefix.Text != "")
                        {
                            oFA_VCHR_MST = new SaitexDM.Common.DataModel.FA_VCHR_MST();

                            if (chk_Status.Checked == true)
                            {
                                chStatus = true;
                            }
                            else
                            {
                                chStatus = false;
                            }
                            oFA_VCHR_MST.STATUS = chStatus;
                            oFA_VCHR_MST.VCHR_CODE = txtVoucherCode.Text.Trim();
                            oFA_VCHR_MST.VCHR_NAME = txtVoucherName.Text.ToUpper().Trim();
                            oFA_VCHR_MST.VCHR_TYPE = cmbVoucherType.SelectedValue.ToString().Trim();
                            oFA_VCHR_MST.VCHR_PREFIX = txtPrefix.Text.ToUpper().Trim();
                            oFA_VCHR_MST.VCHR_SUFFIX = txtSuffix.Text.ToUpper().Trim();
                            oFA_VCHR_MST.VCHR_DESC = txtDescription.Text.Trim();
                            oFA_VCHR_MST.DEL_STATUS = false;
                            oFA_VCHR_MST.TUSER = oUserLoginDetail.UserCode;
                            oFA_VCHR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                            oFA_VCHR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;

                            int iRecordFound = 0;
                            bool bResult = SaitexBL.Interface.Method.FA_VCHR_MST.UpdateVoucherMaster(oFA_VCHR_MST, out iRecordFound);

                            if (bResult)
                            {
                                Session["saveStatus"] = 1;
                                Response.Redirect("./VoucherMaster.aspx?cId=U", false);
                            }
                            else if (iRecordFound > 0)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('This Record is already saved.. Please enter another.');", true);
                            }
                        }
                        else
                        {
                            Common.CommonFuction.ShowMessage("Dear! Please enter Prefix...");
                        }
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Dear! Please select Voucher Type...");
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Dear! Please enter Voucher Name...");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Dear! Please Select Voucher Code...");
            }
        }
        catch
        {
            throw;
        }
    }

    private void DeleteData()
    {
        try
        {
            oFA_VCHR_MST = new SaitexDM.Common.DataModel.FA_VCHR_MST();

            oFA_VCHR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_VCHR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            if (txtVoucherCode.Visible == true)
            {
                oFA_VCHR_MST.VCHR_CODE = CommonFuction.funFixQuotes(txtVoucherCode.Text.ToString().Trim());
            }
            else
            {
                oFA_VCHR_MST.VCHR_CODE = CommonFuction.funFixQuotes(cmbVoucherCode.SelectedValue.ToString().Trim());
            }

            bool bResult = SaitexBL.Interface.Method.FA_VCHR_MST.DeleteVoucherMaster(oFA_VCHR_MST);

            if (bResult)
            {
                Session["saveStatus"] = 1;
                Response.Redirect("./VoucherMaster.aspx?cId=D", false);
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('No such record exits.! Pls enter valid Category Code.');", true);
        }
        catch
        {
            throw;
        }
    }

    private void BindGrid()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.FA_VCHR_MST.GetVoucherMasterWithOutStatus();
            if (dt != null && dt.Rows.Count > 0)
            {
                grdVoucher.DataSource = dt;
                grdVoucher.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    protected void grdVoucher_Select(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            cmbVoucherCode.Visible = false;
            txtVoucherCode.Visible = true;
            txtVoucherCode.ReadOnly = true;

            ArrayList ar = grdVoucher.SelectedRecords;

            lblMessage.Text = "";
            tdClear.Visible = true;
            tdDelete.Visible = true;
            tdUpdate.Visible = true;
            tdSave.Visible = false;

            Hashtable ht = (Hashtable)ar[0];
            txtVoucherCode.Text = ht["VCHR_CODE"].ToString().Trim();
            txtVoucherName.Text = ht["VCHR_NAME"].ToString().Trim();
            cmbVoucherType.SelectedValue = ht["VCHR_TYPE"].ToString().Trim();
            txtPrefix.Text = ht["VCHR_PREFIX"].ToString().Trim();
            txtSuffix.Text = ht["VCHR_SUFFIX"].ToString().Trim();
            txtDescription.Text = ht["VCHR_DESC"].ToString().Trim();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selecting data from Grid..\r\nSee error log for detail."));
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

    private void BindVoucherCombo()
    {
        try
        {
            cmbVoucherCode.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.FA_VCHR_MST.GetVoucherMasterWithOutStatus();
            if (dt != null && dt.Rows.Count > 0)
            {
                cmbVoucherCode.DataValueField = "VCHR_CODE";
                cmbVoucherCode.DataTextField = "VCHR_NAME";
                cmbVoucherCode.DataSource = dt;
                cmbVoucherCode.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }
}