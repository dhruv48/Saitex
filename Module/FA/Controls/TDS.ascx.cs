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

public partial class Module_FA_Controls_TDS : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.FA_CONTRACT_MST oFA_CONTRACT_MST;
    SaitexDM.Common.DataModel.FA_TDS_MST oFA_TDS_MST;

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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in saving data.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DeleteData();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in update mode..\r\nSee error log for detail."));
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            grdTDS.AutoPostBackOnSelect = true;
            tdSave.Visible = false;
            tdDelete.Visible = true;
            lblMode.Text = "Delete";
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in find.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./TDSMaster.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in clearing page.\r\nSee error log for detail."));
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
                Response.Redirect("~/Module/Admin/Pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in exiting page.\r\nSee error log for detail."));
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

    private void InitialisePage()
    {
        try
        {
            BlankControls();
            grdTDS.AutoPostBackOnSelect = false;
            ddlAccountCode.Enabled = true;
            ddlContractCode.Enabled = true;
            lblMode.Text = "Save";
            tdDelete.Visible = false;
            tdFind.Visible = true;
            bindLedgerDropdown();
            bindContractDropdown();
            bindTDSGrid();
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
            ddlAccountCode.SelectedIndex = -1;
            ddlContractCode.SelectedIndex = -1;
            lblMode.Text = "Save";
            lblMessage.Text = "";
        }
        catch
        {
            throw;
        }
    }

    private void bindLedgerDropdown()
    {
        try
        {
            ddlAccountCode.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.FA_LGR_MST.GetLedgersForAdvice();

            if (dt != null && dt.Rows.Count > 0)
            {
                ddlAccountCode.DataValueField = "LDGR_CODE";
                ddlAccountCode.DataTextField = "LDGR_NAME";
                ddlAccountCode.DataSource = dt;
                ddlAccountCode.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    private void bindContractDropdown()
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

    private void bindTDSGrid()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.FA_TDS_MST.SelectAll();

            if (dt != null && dt.Rows.Count > 0)
            {
                grdTDS.DataSource = dt;
                grdTDS.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlAccountCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            bindLedgerDropdown();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Account Code..\r\nSee error log for detail."));
        }
    }

    protected void ddlContractCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            bindContractDropdown();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Contract Code..\r\nSee error log for detail."));
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Update..\r\nSee error log for detail."));
        }
    }

    private void InsertData()
    {
        try
        {
            oFA_TDS_MST = new SaitexDM.Common.DataModel.FA_TDS_MST();

            oFA_TDS_MST.STATUS = true;
            oFA_TDS_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_TDS_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_TDS_MST.LDGR_CODE = ddlAccountCode.SelectedValue.Trim();
            oFA_TDS_MST.CONTRACT_CODE = ddlContractCode.SelectedValue.Trim();
            oFA_TDS_MST.TUSER = oUserLoginDetail.UserCode;

            int iRecordFound = 0;

            bool IsSaved = SaitexBL.Interface.Method.FA_TDS_MST.Insert(oFA_TDS_MST, out iRecordFound);

            if (IsSaved)
            {
                Session["saveStatus"] = 1;
                Response.Redirect("./TDSMaster.aspx?cId=S", false);
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
        catch { throw; }
    }

    private void DeleteData()
    {
        try
        {
            oFA_TDS_MST = new SaitexDM.Common.DataModel.FA_TDS_MST();

            oFA_TDS_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_TDS_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_TDS_MST.LDGR_CODE = ddlAccountCode.SelectedValue.Trim();
            oFA_TDS_MST.CONTRACT_CODE = ddlContractCode.SelectedValue.Trim();
            oFA_TDS_MST.TUSER = oUserLoginDetail.UserCode;

            bool IsSaved = SaitexBL.Interface.Method.FA_TDS_MST.Delete(oFA_TDS_MST);

            if (IsSaved)
            {
                Session["saveStatus"] = 1;
                Response.Redirect("./TDSMaster.aspx?cId=D", false);
            }
            else
            {
                Common.CommonFuction.ShowMessage("Details deletion failed..");
            }
        }
        catch { throw; }
    }

    protected void grdTDS_Select(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            ArrayList ar = grdTDS.SelectedRecords;

            lblMessage.Text = "";
            tdClear.Visible = true;
            tdDelete.Visible = true;
            tdSave.Visible = false;

            Hashtable ht = (Hashtable)ar[0];

            ddlAccountCode.SelectedValue = ht["LDGR_CODE"].ToString().Trim();
            ddlContractCode.SelectedValue = ht["CONTRACT_CODE"].ToString().Trim();
            ddlAccountCode.Enabled = false;
            ddlContractCode.Enabled = false;

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Select Event..\r\nSee error log for detail."));
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }
}