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

public partial class Module_FA_Controls_AssetGrpMst : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.FA_ASSETS_GRP_MST oFA_ASSETS_GRP_MST;

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
                    Common.CommonFuction.ShowMessage("Asset Group Saved Sucessfully !");
                }
                if (Request.QueryString["cId"].ToString().Trim() == "U")
                {
                    Common.CommonFuction.ShowMessage("Asset Group Updated Sucessfully !");
                }
                if (Request.QueryString["cId"].ToString().Trim() == "D")
                {
                    Common.CommonFuction.ShowMessage("Record Deleted successfully '");
                }
                Session["saveStatus"] = 0;
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }

    /// <summary>
    /// Use of Intialization. Means blank controls.
    /// </summary>
    private void InitialisePage()
    {
        try
        {
            cmbGroupCode.Visible = false;
            lblMode.Text = "Save";
            tdSave.Visible = true;
            tdDelete.Visible = false;
            tdUpdate.Visible = false;
            txtGroupCode.ReadOnly = true;

            BindMaxAssetGroupCode();
            BindAssetGroupGrid();
            BindAssetGroupCombo();
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// It returns Maximum (Asset Group Code) count from FA_ASSETS_GRP_MST Table.
    /// </summary>
    private void BindMaxAssetGroupCode()
    {
        try
        {
            string x = "";
            int y = 0;

            DataTable dt = SaitexBL.Interface.Method.FA_ASSETS_GRP_MST.GetMaxAssetGroupCode();
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
                        txtGroupCode.Text = y.ToString();
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private void BindAssetGroupGrid()
    {
        try
        {
            oFA_ASSETS_GRP_MST = new SaitexDM.Common.DataModel.FA_ASSETS_GRP_MST();
            oFA_ASSETS_GRP_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_ASSETS_GRP_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;

            DataTable dt = SaitexBL.Interface.Method.FA_ASSETS_GRP_MST.GetAssetGroupDetailByComp(oFA_ASSETS_GRP_MST);
            if (dt != null && dt.Rows.Count > 0)
            {
                grdAssetGroup.DataSource = dt;
                grdAssetGroup.DataBind();
            }
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving.\r\nSee error log for detail."));
        }
    }

    private void InsertData()
    {
        try
        {
            double dblWDVRate = 0, dblSLMRate = 0;

            if (CheckValidation())
            {
                if (CheckDuplicateDate())
                {
                    oFA_ASSETS_GRP_MST = new SaitexDM.Common.DataModel.FA_ASSETS_GRP_MST();

                    oFA_ASSETS_GRP_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                    oFA_ASSETS_GRP_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                    oFA_ASSETS_GRP_MST.ASSET_GRP_CODE = txtGroupCode.Text.ToUpper().Trim();
                    oFA_ASSETS_GRP_MST.ASSET_GRP_NAME = txtGroupName.Text.ToUpper().Trim();
                    oFA_ASSETS_GRP_MST.FROM_DT = txtFromDT.Text.Trim();
                    oFA_ASSETS_GRP_MST.TO_DT = txtToDate.Text.Trim();

                    double.TryParse(txtWDVRate.Text.Trim(), out dblWDVRate);
                    oFA_ASSETS_GRP_MST.WDV_DEPR_RATE = dblWDVRate;

                    double.TryParse(txtSLMRate.Text.Trim(), out dblSLMRate);
                    oFA_ASSETS_GRP_MST.SLM_DEPR_RATE = dblSLMRate;

                    oFA_ASSETS_GRP_MST.DESCRIPTION = txtDescription.Text.Trim();
                    oFA_ASSETS_GRP_MST.TUSER = oUserLoginDetail.UserCode;

                    int iRecordFound = 0;
                    bool bResult = SaitexBL.Interface.Method.FA_ASSETS_GRP_MST.Insert(oFA_ASSETS_GRP_MST, out iRecordFound);
                    if (bResult)
                    {
                        Session["saveStatus"] = 1;
                        Response.Redirect("./AssetsGrpMaster.aspx?cId=S", false);
                    }
                    else if (iRecordFound > 0)
                    {
                        Common.CommonFuction.ShowMessage("This Record is already saved.. Please enter another.");
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Details Saving failed..");
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("The Date duration is already found in DataBase.. FromDate OR ToDate is already saved with another Asset Group.. Please select different date period or update previous date..");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Dear! Please provide some required field denoted with(*) mark..");
            }
        }
        catch
        {
            throw;
        }
    }

    private bool CheckValidation()
    {
        try
        {
            bool IsValidation = false;

            if (txtGroupCode.Text != "")
            {
                if (txtGroupName.Text != "")
                {
                    if (txtFromDT.Text != "")
                    {
                        if (txtToDate.Text != "")
                        {
                            if (txtWDVRate.Text != "")
                            {
                                if (txtSLMRate.Text != "")
                                {
                                    if (DateTime.Parse(txtToDate.Text.Trim()) >= DateTime.Parse(txtFromDT.Text.Trim()))
                                    {
                                        IsValidation = true;
                                    }
                                    else
                                    {
                                        IsValidation = false;
                                        Common.CommonFuction.ShowMessage("Dear! From_Date should not be greater than To_Date..");
                                    }
                                }
                                else
                                {
                                    IsValidation = false;
                                    Common.CommonFuction.ShowMessage("Dear! Please enter SLM Depreciation Rate..");
                                }
                            }
                            else
                            {
                                IsValidation = false;
                                Common.CommonFuction.ShowMessage("Dear! Please enter WDV Depreciation Rate..");
                            }
                        }
                        else
                        {
                            IsValidation = false;
                            Common.CommonFuction.ShowMessage("Dear! Please enter To Date..");
                        }
                    }
                    else
                    {
                        IsValidation = false;
                        Common.CommonFuction.ShowMessage("Dear! Please enter From Date..");
                    }
                }
                else
                {
                    IsValidation = false;
                    Common.CommonFuction.ShowMessage("Dear! Please enter Asset Group Name..");
                }
            }
            else
            {
                IsValidation = false;
                Common.CommonFuction.ShowMessage("Dear! Please provide Asset Group Code..");
            }
            return IsValidation;
        }
        catch
        {
            return false;
        }
    }

    private bool CheckDuplicateDate()
    {
        try
        {
            bool IsValidation = false;

            if (txtFromDT.Text != "" && txtToDate.Text != "")
            {
                DataTable dt = SaitexBL.Interface.Method.FA_ASSETS_GRP_MST.CheckDuplicateDateEntry(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, txtFromDT.Text.Trim(), txtToDate.Text.Trim(), txtGroupCode.Text.Trim(), txtGroupName.Text.Trim());
                if (dt != null && dt.Rows.Count > 0)
                {
                    IsValidation = false;
                }
                else
                {
                    IsValidation = true;
                }
            }
            else
            {
                IsValidation = false;
                Common.CommonFuction.ShowMessage("Dear! Please Dates..");
            }
            return IsValidation;
        }
        catch
        {
            return false;
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtGroupCode.Visible = false;
            cmbGroupCode.Visible = true;
            //grdAssetGroup.AutoPostBackOnSelect = true;
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

    private void UpdateData()
    {
        try
        {
            double dblWDVRate = 0, dblSLMRate = 0;
            if (CheckValidation())
            {
                if (CheckDuplicateDateForUpdate())
                {
                    oFA_ASSETS_GRP_MST = new SaitexDM.Common.DataModel.FA_ASSETS_GRP_MST();
                    if (txtGroupCode.Visible == true)
                    {
                        oFA_ASSETS_GRP_MST.ASSET_GRP_CODE = txtGroupCode.Text.ToUpper().Trim();
                    }
                    else
                    {
                        oFA_ASSETS_GRP_MST.ASSET_GRP_CODE = cmbGroupCode.SelectedValue.ToString().Trim();
                    }
                    oFA_ASSETS_GRP_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                    oFA_ASSETS_GRP_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                    oFA_ASSETS_GRP_MST.ASSET_GRP_NAME = txtGroupName.Text.ToUpper().Trim();
                    oFA_ASSETS_GRP_MST.FROM_DT = txtFromDT.Text.Trim();
                    oFA_ASSETS_GRP_MST.TO_DT = txtToDate.Text.Trim();

                    double.TryParse(txtWDVRate.Text.Trim(), out dblWDVRate);
                    oFA_ASSETS_GRP_MST.WDV_DEPR_RATE = dblWDVRate;

                    double.TryParse(txtSLMRate.Text.Trim(), out dblSLMRate);
                    oFA_ASSETS_GRP_MST.SLM_DEPR_RATE = dblSLMRate;

                    oFA_ASSETS_GRP_MST.DESCRIPTION = txtDescription.Text.Trim();

                    int iRecordFound = 0;
                    bool bResult = SaitexBL.Interface.Method.FA_ASSETS_GRP_MST.Update(oFA_ASSETS_GRP_MST, out iRecordFound);
                    if (bResult)
                    {
                        Session["saveStatus"] = 1;
                        Response.Redirect("./AssetsGrpMaster.aspx?cId=U", false);
                    }
                    else if (iRecordFound > 0)
                    {
                        Common.CommonFuction.ShowMessage("This Record is already saved.. Please enter another.");
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Details Saving failed..");
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("The Date duration is already found in DataBase.. FromDate OR ToDate is already saved with another Asset Group.. Please select different date period or update previous date..");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Dear! Please provide some required field denoted with(*) mark..");
            }
        }
        catch
        {
            throw;
        }
    }

    private bool CheckDuplicateDateForUpdate()
    {
        try
        {
            bool IsValidation = false;
            string strGroupCode = string.Empty;
            if (txtGroupCode.Visible == true)
            {
                strGroupCode = txtGroupCode.Text.Trim();
            }
            else
            {
                strGroupCode = cmbGroupCode.SelectedValue.ToString().Trim();
            }
            if (txtFromDT.Text != "" && txtToDate.Text != "")
            {
                DataTable dt = SaitexBL.Interface.Method.FA_ASSETS_GRP_MST.CheckDuplicateDateEntryForUpdate(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, txtFromDT.Text.Trim(), txtToDate.Text.Trim(), strGroupCode, txtGroupName.Text.Trim());
                if (dt != null && dt.Rows.Count > 0)
                {
                    IsValidation = false;
                }
                else
                {
                    IsValidation = true;
                }
            }
            else
            {
                IsValidation = false;
                Common.CommonFuction.ShowMessage("Dear! Please Dates..");
            }
            return IsValidation;
        }
        catch
        {
            return false;
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
            Response.Redirect("./AssetsGrpMaster.aspx", false);
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
            string URL = "../Reports/There is no report available right now";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
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

    //protected void grdAssetGroup_Select(object sender, Obout.Grid.GridRecordEventArgs e)
    //{
    //    try
    //    {
    //        string strGroupCode = string.Empty;

    //        cmbGroupCode.Visible = false;
    //        txtGroupCode.Visible = true;
    //        txtGroupCode.ReadOnly = true;

    //        ArrayList ar = grdAssetGroup.SelectedRecords;

    //        lblMessage.Text = "";
    //        tdClear.Visible = true;
    //        tdDelete.Visible = true;
    //        tdUpdate.Visible = true;
    //        tdSave.Visible = false;

    //        Hashtable ht = (Hashtable)ar[0];

    //        strGroupCode = ht["ASSET_GRP_CODE"].ToString().Trim();
    //        FillAssetsGroupDetails(strGroupCode);
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Select Event..\r\nSee error log for detail."));
    //    }
    //}

    private void FillAssetsGroupDetails(string strGroupCode)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.FA_ASSETS_GRP_MST.GetAssetGroupDetail(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "ASSET_GRP_CODE = " + strGroupCode;
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        txtGroupCode.Text = dv[iLoop]["ASSET_GRP_CODE"].ToString();
                        cmbGroupCode.SelectedValue = dv[iLoop]["ASSET_GRP_CODE"].ToString();
                        txtGroupName.Text = dv[iLoop]["ASSET_GRP_NAME"].ToString();
                        txtFromDT.Text = dv[iLoop]["FROM_DT"].ToString();
                        txtToDate.Text = dv[iLoop]["TO_DT"].ToString();
                        txtWDVRate.Text = dv[iLoop]["WDV_DEPR_RATE"].ToString();
                        txtSLMRate.Text = dv[iLoop]["SLM_DEPR_RATE"].ToString();
                        txtDescription.Text = dv[iLoop]["DESCRIPTION"].ToString();
                    }
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

    protected void cmbGroupCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            string strGroupCode = string.Empty;
            strGroupCode = cmbGroupCode.SelectedValue.ToString().Trim();
            FillAssetsGroupDetails(strGroupCode);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading Assets Groups..\r\nSee error log for detail."));
        }
    }

    private void BindAssetGroupCombo()
    {
        try
        {
            cmbGroupCode.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.FA_ASSETS_GRP_MST.GetAssetGroupDetail(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                cmbGroupCode.DataValueField = "ASSET_GRP_CODE";
                cmbGroupCode.DataTextField = "ASSET_GRP_CODE";
                cmbGroupCode.DataSource = dt;
                cmbGroupCode.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    protected void cmbGroupCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            BindAssetGroupCombo();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Assets Groups..\r\nSee error log for detail."));
        }
    }
    protected void grdAssetGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //BindData();
            grdAssetGroup.PageIndex = e.NewPageIndex;
            grdAssetGroup.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}