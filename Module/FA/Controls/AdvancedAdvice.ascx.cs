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

public partial class Module_FA_Controls_AdvancedAdvice : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.FA_ADVANCED_ADVICE oFA_ADVANCED_ADVICE;

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

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InsertData();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtAdviceNo.Visible = false;
            cmbAdviceNo.Visible = true;
            grdAdvice.AutoPostBackOnSelect = true;
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            lblMode.Text = "Update";
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in finding..\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating..\r\nSee error log for detail."));
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Common.CommonFuction.ShowMessage("Sorry! Dear.. No Deletion Allowed..");
            //DeleteData();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Deletion..\r\nSee error log for detail."));
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./AdvancedPaymentAdvice.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear.\r\nSee error log for detail."));
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

    protected void cmbAdviceNo_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            bindAdviceCombo();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Advice Number.\r\nSee error log for detail."));
        }
    }

    protected void cmbAdviceNo_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            string Adv_No = string.Empty;
            Adv_No = cmbAdviceNo.SelectedValue.ToString().Trim();
            FillAdviceDetails(Adv_No);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Advice Number..\r\nSee error log for detail."));
        }
    }

    protected void cmbPartyLedger_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            bindPatyLedgers();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Party Ledgers..\r\nSee error log for detail."));
        }
    }

    protected void grdAdvice_Select(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            string Adv_No = string.Empty;

            cmbAdviceNo.Visible = false;
            txtAdviceNo.Visible = true;
            txtAdviceNo.ReadOnly = true;

            ArrayList ar = grdAdvice.SelectedRecords;

            lblMessage.Text = "";
            tdClear.Visible = true;
            tdDelete.Visible = true;
            tdUpdate.Visible = true;
            tdSave.Visible = false;

            Hashtable ht = (Hashtable)ar[0];

            Adv_No = ht["ADV_NO"].ToString().Trim();
            FillAdviceDetails(Adv_No);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Select Event..\r\nSee error log for detail."));
        }
    }

    /// <summary>
    /// Use of Intialization. Means blank controls.
    /// </summary>
    private void InitialisePage()
    {
        try
        {
            if (txtAdviceDate.Text == "")
                txtAdviceDate.Text = System.DateTime.Now.Date.ToShortDateString();

            lblMode.Text = "Save";
            tdSave.Visible = true;
            tdDelete.Visible = false;
            tdUpdate.Visible = false;
            cmbAdviceNo.Visible = false;
            txtAdviceNo.Visible = true;
            txtAdviceNo.ReadOnly = true;
            cmbPartyLedger.SelectedText = "";
            cmbPartyLedger.SelectedIndex = -1;
            cmbPartyLedger.SelectedIndex = -1;
            txtAmount.Text = "";
            txtDescription.Text = "";
            grdAdvice.AutoPostBackOnSelect = false;

            bindMaxAdviceNo();
            bindPatyLedgers();
            bindAdviceGrid();
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// It returns Maximum (Advice No) count from FA_ADVANCED_ADVICE Table.
    /// </summary>
    private void bindMaxAdviceNo()
    {
        try
        {
            string x = "";
            int y = 0;

            DataTable dt = SaitexBL.Interface.Method.FA_ADVANCED_ADVICE.GetMaxAdviceNo();

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
                        txtAdviceNo.Text = y.ToString();
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private void bindPatyLedgers()
    {
        try
        {
            cmbPartyLedger.Items.Clear();

            DataTable dt = SaitexBL.Interface.Method.FA_LGR_MST.GetLedgersForAdvice();

            if (dt != null && dt.Rows.Count > 0)
            {
                cmbPartyLedger.DataValueField = "LDGR_CODE";
                cmbPartyLedger.DataTextField = "LDGR_NAME";
                cmbPartyLedger.DataSource = dt;
                cmbPartyLedger.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    private void bindAdviceGrid()
    {
        try
        {
            grdAdvice.DataSource = null;
            grdAdvice.DataBind();

            DataTable dt = SaitexBL.Interface.Method.FA_ADVANCED_ADVICE.GetAdviceDetail(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year);

            if (dt != null && dt.Rows.Count > 0)
            {
                grdAdvice.DataSource = dt;
                grdAdvice.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    private void InsertData()
    {
        try
        {
            if (txtAdviceNo.Text != "")
            {
                if (cmbPartyLedger.SelectedIndex != -1)
                {
                    if (txtAmount.Text != "")
                    {
                        if (txtAdviceDate.Text != "")
                        {
                            oFA_ADVANCED_ADVICE = new SaitexDM.Common.DataModel.FA_ADVANCED_ADVICE();
                            if (txtAdviceNo.Visible == true)
                            {
                                oFA_ADVANCED_ADVICE.ADV_NO = txtAdviceNo.Text.ToUpper().Trim();
                            }
                            else
                            {
                                oFA_ADVANCED_ADVICE.ADV_NO = cmbAdviceNo.SelectedValue.ToString().Trim();
                            }

                            oFA_ADVANCED_ADVICE.LEDGER_CODE = cmbPartyLedger.SelectedValue.ToString().Trim();
                            oFA_ADVANCED_ADVICE.ADV_AMT = double.Parse(txtAmount.Text.Trim());
                            oFA_ADVANCED_ADVICE.ADV_DATE = DateTime.Parse(txtAdviceDate.Text.Trim());
                            oFA_ADVANCED_ADVICE.DESCRIPTION = txtDescription.Text.Trim();
                            oFA_ADVANCED_ADVICE.COMP_CODE = oUserLoginDetail.COMP_CODE;
                            oFA_ADVANCED_ADVICE.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                            oFA_ADVANCED_ADVICE.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                            oFA_ADVANCED_ADVICE.TUSER = oUserLoginDetail.UserCode;

                            int iRecordFound = 0;

                            bool bResult = SaitexBL.Interface.Method.FA_ADVANCED_ADVICE.Insert(oFA_ADVANCED_ADVICE, out iRecordFound);

                            if (bResult)
                            {
                                Session["saveStatus"] = 1;
                                Response.Redirect("./AdvancedPaymentAdvice.aspx?cId=S", false);
                            }
                            else if (iRecordFound > 0)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('This Record is already saved.. Please enter another.');", true);
                            }
                            else
                            {
                                Common.CommonFuction.ShowMessage("Details Saving failed..");
                            }
                        }
                        else
                        {
                            Common.CommonFuction.ShowMessage("Dear! Please Select Advice Date..");
                        }
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Dear! Please Enter Amount..");
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Dear! Please select Party Ledger..");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Dear! Please provide Advice Number..");
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
            if (cmbPartyLedger.SelectedIndex != -1)
            {
                if (txtAmount.Text != "")
                {
                    if (txtAdviceDate.Text != "")
                    {
                        oFA_ADVANCED_ADVICE = new SaitexDM.Common.DataModel.FA_ADVANCED_ADVICE();
                        if (txtAdviceNo.Visible == true)
                        {
                            oFA_ADVANCED_ADVICE.ADV_NO = txtAdviceNo.Text.ToUpper().Trim();
                        }
                        else
                        {
                            oFA_ADVANCED_ADVICE.ADV_NO = cmbAdviceNo.SelectedValue.ToString().Trim();
                        }

                        oFA_ADVANCED_ADVICE.LEDGER_CODE = cmbPartyLedger.SelectedValue.ToString().Trim();
                        oFA_ADVANCED_ADVICE.ADV_AMT = double.Parse(txtAmount.Text.Trim());
                        oFA_ADVANCED_ADVICE.ADV_DATE = DateTime.Parse(txtAdviceDate.Text.Trim());
                        oFA_ADVANCED_ADVICE.DESCRIPTION = txtDescription.Text.Trim();
                        oFA_ADVANCED_ADVICE.COMP_CODE = oUserLoginDetail.COMP_CODE;
                        oFA_ADVANCED_ADVICE.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                        oFA_ADVANCED_ADVICE.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                        oFA_ADVANCED_ADVICE.TUSER = oUserLoginDetail.UserCode;

                        int iRecordFound = 0;

                        bool bResult = SaitexBL.Interface.Method.FA_ADVANCED_ADVICE.Update(oFA_ADVANCED_ADVICE, out iRecordFound);

                        if (bResult)
                        {
                            Session["saveStatus"] = 1;
                            Response.Redirect("./AdvancedPaymentAdvice.aspx?cId=U", false);
                        }
                        else if (iRecordFound > 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('This Record is already saved.. Please enter another.');", true);
                        }
                        else
                        {
                            Common.CommonFuction.ShowMessage("Details Updation failed..");
                        }
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Dear! Please Select Advice Date..");
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Dear! Please Enter Amount..");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Dear! Please select Party Ledger..");
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
            if (cmbPartyLedger.SelectedIndex != -1 || cmbPartyLedger.SelectedText != "")
            {
                oFA_ADVANCED_ADVICE = new SaitexDM.Common.DataModel.FA_ADVANCED_ADVICE();
                if (txtAdviceNo.Visible == true)
                {
                    oFA_ADVANCED_ADVICE.ADV_NO = txtAdviceNo.Text.Trim();
                }
                else
                {
                    oFA_ADVANCED_ADVICE.ADV_NO = cmbAdviceNo.SelectedValue.ToString().Trim();
                }

                oFA_ADVANCED_ADVICE.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oFA_ADVANCED_ADVICE.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oFA_ADVANCED_ADVICE.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oFA_ADVANCED_ADVICE.TUSER = oUserLoginDetail.UserCode;

                bool bResult = SaitexBL.Interface.Method.FA_ADVANCED_ADVICE.Delete(oFA_ADVANCED_ADVICE);
                if (bResult)
                {
                    Session["saveStatus"] = 1;
                    Response.Redirect("./AdvancedPaymentAdvice.aspx?cId=D", false);
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Details Deleted failed..");
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Please select party ledger.');", true);
            }
        }
        catch
        {
            throw;
        }
    }

    private void FillAdviceDetails(string Adv_No)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.FA_ADVANCED_ADVICE.GetAdviceDetail(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "ADV_NO = '" + Adv_No + "' and ADV_FLAG <> '1'";
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        txtAdviceNo.Text = dv[iLoop]["ADV_NO"].ToString();
                        cmbPartyLedger.SelectedValue = dv[iLoop]["LEDGER_CODE"].ToString();
                        txtAmount.Text = dv[iLoop]["ADV_AMT"].ToString();
                        txtAdviceDate.Text = dv[iLoop]["ADV_DATE"].ToString();
                        txtDescription.Text = dv[iLoop]["DESCRIPTION"].ToString();
                    }
                }
                else
                {
                    lblMessage.Text = "Sorry Dear! You can't update this Advice, because it is already paid.";
                    cmbAdviceNo.SelectedText = "";
                    txtAdviceDate.Text = "";
                    txtDescription.Text = "";
                    txtAmount.Text = "";
                }
            }
        }
        catch
        {
            throw;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnDelete.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Delete this Record ?')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }

    private void bindAdviceCombo()
    {
        try
        {
            cmbAdviceNo.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.FA_ADVANCED_ADVICE.GetAdviceDetail(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year);
            if (dt != null && dt.Rows.Count > 0)
            {
                cmbAdviceNo.DataValueField = "ADV_NO";
                cmbAdviceNo.DataTextField = "ADV_NO";
                cmbAdviceNo.DataSource = dt;
                cmbAdviceNo.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }
}