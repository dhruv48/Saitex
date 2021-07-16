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

public partial class Module_FA_Controls_TaxMaster : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.FA_TAX_MST oFA_TAX_MST;
    SaitexDM.Common.DataModel.FA_LGR_MST oFA_LGR_MST;
    private static string LedgerCode = string.Empty;

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

    private void InitialisePage()
    {
        try
        {
            BindTaxGroupDropDown("");
            activateSaveMode();
            clearControls();
        }
        catch
        {
            throw;
        }
    }

    private void activateSaveMode()
    {
        try
        {
            tdDelete.Visible = false;
            tdUpdate.Visible = false;
            tdSave.Visible = true;

            ddlTaxCode.Visible = false;
            txtTaxCode.Visible = true;

            lblMode.Text = "Save";

            txtTaxCode.ReadOnly = false;
        }
        catch
        {
            throw;
        }
    }

    private void activateUpdateMode()
    {
        try
        {
            tdDelete.Visible = true;
            tdUpdate.Visible = true;
            tdSave.Visible = false;

            ddlTaxCode.Visible = true;
            txtTaxCode.Visible = false;

            lblMode.Text = "Update";

            txtTaxCode.ReadOnly = true;
        }
        catch
        {
            throw;
        }
    }

    private void clearControls()
    {
        try
        {
            txtTaxCode.Text = string.Empty;
            txtTaxdescription.Text = string.Empty;

            ddlTaxGRPCode.SelectedIndex = 0;
            ddlTaxCode.SelectedIndex = -1;
        }
        catch
        {
            throw;
        }
    }

    private void BindTaxGroupDropDown(string Text)
    {
        try
        {
            string CommandText = "select MST_CODE from tx_Master_TRN where Del_Status=0 and MST_NAME=:MST_NAME and comp_code='" + oUserLoginDetail.COMP_CODE + "' ";
            string WhereClause = " and MST_CODE like :SearchQuery";
            string SortExpression = " order by MST_CODE asc";
            string SearchQuery = Text + "%";
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "FA_TAX_GRP_CODE", oUserLoginDetail.COMP_CODE);

            ddlTaxGRPCode.Items.Clear();
            ddlTaxGRPCode.DataSource = dt;
            ddlTaxGRPCode.DataTextField = "MST_CODE";
            ddlTaxGRPCode.DataValueField = "MST_CODE";
            ddlTaxGRPCode.DataBind();
            ddlTaxGRPCode.Items.Insert(0, new ListItem("---- Select Tax Group ----", "0"));
        }
        catch { throw; }
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

    private void InsertData()
    {
        try
        {
            string msg = string.Empty;
            LedgerCode = string.Empty;

            if (ValidateDataForInsert(out msg))
            {
                oFA_TAX_MST = new SaitexDM.Common.DataModel.FA_TAX_MST();
                oFA_TAX_MST.TAX_CODE = txtTaxCode.Text.Trim().ToUpper();

                bool IsDuplicate = SaitexBL.Interface.Method.FA_TAX_MST.CheckDuplicateTaxMaster(oFA_TAX_MST);

                if (IsDuplicate)
                {
                    MaxLedgerCode();
                    oFA_TAX_MST.STATUS = true;
                    oFA_TAX_MST.TAX_CODE = txtTaxCode.Text.Trim().ToUpper();
                    oFA_TAX_MST.TAX_DESC = txtTaxdescription.Text.Trim();
                    oFA_TAX_MST.TAX_GRP_CODE = ddlTaxGRPCode.SelectedValue.Trim();
                    oFA_TAX_MST.LDGR_CODE = LedgerCode;
                    oFA_TAX_MST.TUSER = oUserLoginDetail.UserCode;

                    bool IsSaved = SaitexBL.Interface.Method.FA_TAX_MST.Insert(oFA_TAX_MST);

                    if (IsSaved)
                    {
                        GenerateLedger();
                        InitialisePage();
                        CommonFuction.ShowMessage(@"Record Saved Successfully.");
                    }
                    else
                    {
                        CommonFuction.ShowMessage(@"Record insertion failed.");
                    }
                }
                else
                {
                    CommonFuction.ShowMessage(@"Record Already Exists.. Either Ledger Master OR Tax Master.. Enter another TAX Code..");
                }
            }
            else
            {
                CommonFuction.ShowMessage(msg);
            }

        }
        catch { throw; }
    }

    private bool ValidateDataForInsert(out string msg)
    {
        try
        {
            bool result = false;

            msg = string.Empty;

            int iCount = 0;

            if (txtTaxCode.Text.Equals(string.Empty))
            {
                msg += @"\r\nPlease provide Tax Code";
            }
            else
            {
                iCount += 1;
            }

            if (txtTaxdescription.Text.Equals(string.Empty))
            {
                msg += @"\r\nPlease provide Tax Description";
            }
            else
            {
                iCount += 1;
            }

            if (ddlTaxGRPCode.SelectedIndex == 0)
            {
                msg += @"\r\nPlease select Tax Group";
            }
            else
            {
                iCount += 1;
            }

            if (iCount == 3)
            {
                result = true;
            }

            return result;
        }
        catch { throw; }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            clearControls();
            activateUpdateMode();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in update mode.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in updating data.\r\nSee error log for detail."));
        }
    }

    private void UpdateData()
    {
        try
        {
            string msg = string.Empty;
            if (ValidateDataForInsert(out msg))
            {
                oFA_TAX_MST = new SaitexDM.Common.DataModel.FA_TAX_MST();

                oFA_TAX_MST.STATUS = true;
                oFA_TAX_MST.TAX_CODE = txtTaxCode.Text.Trim().ToUpper();
                oFA_TAX_MST.TAX_DESC = txtTaxdescription.Text.Trim();
                oFA_TAX_MST.TAX_GRP_CODE = ddlTaxGRPCode.SelectedValue.Trim();
                oFA_TAX_MST.TUSER = oUserLoginDetail.UserCode;

                bool IsSaved = SaitexBL.Interface.Method.FA_TAX_MST.Update(oFA_TAX_MST);

                if (IsSaved)
                {
                    InitialisePage();
                    CommonFuction.ShowMessage(@"Record updated successfully.");
                }
                else
                {
                    CommonFuction.ShowMessage(@"Record updation failed.");
                }
            }
            else
            {
                CommonFuction.ShowMessage(msg);
            }

        }
        catch { throw; }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DeleteData();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in deleting data.\r\nSee error log for detail."));
        }
    }

    private void DeleteData()
    {
        try
        {
            string msg = string.Empty;
            LedgerCode = string.Empty;

            if (ValidateDataForInsert(out msg))
            {
                oFA_TAX_MST = new SaitexDM.Common.DataModel.FA_TAX_MST();

                oFA_TAX_MST.DEL_STATUS = true;
                oFA_TAX_MST.TAX_CODE = txtTaxCode.Text.Trim();
                oFA_TAX_MST.TUSER = oUserLoginDetail.UserCode;

                DataTable dtTaxMaster = SaitexBL.Interface.Method.FA_TAX_MST.SelectByTAX_CODE(oFA_TAX_MST);

                if (dtTaxMaster != null && dtTaxMaster.Rows.Count > 0)
                {
                    LedgerCode = dtTaxMaster.Rows[0]["LDGR_CODE"].ToString();
                }
                oFA_TAX_MST.LDGR_CODE = LedgerCode;

                bool IsSaved = SaitexBL.Interface.Method.FA_TAX_MST.Delete(oFA_TAX_MST);

                if (IsSaved)
                {
                    InitialisePage();
                    CommonFuction.ShowMessage(@"Record deleted successfully.");
                }
                else
                {
                    CommonFuction.ShowMessage(@"Record deletion failed.");
                }
            }
            else
            {
                CommonFuction.ShowMessage(msg);
            }

        }
        catch { throw; }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InitialisePage();
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
            string URL = "../Reports/There is no report available right now";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Printing the data.\r\nSee error log for detail."));
        }
        //try
        //{
            
        //}
        //catch (Exception ex)
        //{
        //    CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in printing page.\r\nSee error log for detail."));
        //}

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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in getting help.\r\nSee error log for detail."));
        }
    }

    protected void ddlTaxCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            BindTaxDropDown(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in getting Data for updation.\r\nSee error log for detail."));
        }
    }

    private void BindTaxDropDown(string Text)
    {
        try
        {
            string CommandText = "select * from ( select * from v_fa_tax_mst) ";
            string WhereClause = " where TAX_CODE like :SearchQuery and TAX_DESC like :SearchQuery and TAX_GRP_CODE like :SearchQuery";
            string SortExpression = " order by TAX_CODE asc";
            string SearchQuery = Text + "%";
            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

            ddlTaxCode.Items.Clear();
            ddlTaxCode.DataSource = dt;
            ddlTaxCode.DataTextField = "TAX_CODE";
            ddlTaxCode.DataValueField = "TAX_CODE";
            ddlTaxCode.DataBind();
        }
        catch { throw; }
    }

    protected void ddlTaxCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            string Tax_Code = ddlTaxCode.SelectedValue.Trim();
            FillDataForEdit(Tax_Code);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in getting Data for updation.\r\nSee error log for detail."));
        }
    }

    private void FillDataForEdit(string TAX_CODE)
    {
        try
        {
            oFA_TAX_MST = new SaitexDM.Common.DataModel.FA_TAX_MST();
            oFA_TAX_MST.TAX_CODE = TAX_CODE;

            DataTable dtTaxMaster = SaitexBL.Interface.Method.FA_TAX_MST.SelectByTAX_CODE(oFA_TAX_MST);

            if (dtTaxMaster != null && dtTaxMaster.Rows.Count > 0)
            {
                txtTaxCode.Text = dtTaxMaster.Rows[0]["TAX_CODE"].ToString();
                txtTaxdescription.Text = dtTaxMaster.Rows[0]["TAX_DESC"].ToString();
                ddlTaxGRPCode.SelectedValue = dtTaxMaster.Rows[0]["TAX_GRP_CODE"].ToString();
            }
        }
        catch
        {
            throw;
        }
    }

    private void GenerateLedger()
    {
        try
        {
            oFA_LGR_MST = new SaitexDM.Common.DataModel.FA_LGR_MST();

            oFA_LGR_MST.STATUS = true;
            oFA_LGR_MST.IS_DR_OP = true;
            oFA_LGR_MST.LDGR_CODE = LedgerCode;
            oFA_LGR_MST.LDGR_NAME = txtTaxCode.Text.Trim().ToUpper();
            oFA_LGR_MST.GRP_CODE = "35";
            oFA_LGR_MST.PRINT_NAME = txtTaxCode.Text.Trim().ToUpper();
            oFA_LGR_MST.LDGR_DESC = txtTaxdescription.Text.Trim();
            oFA_LGR_MST.DEL_STATUS = false;
            oFA_LGR_MST.TUSER = oUserLoginDetail.UserCode;
            oFA_LGR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_LGR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_LGR_MST.OP_BAL = 0;
            oFA_LGR_MST.LDGR_TYPE = "NOMINAL";
            oFA_LGR_MST.LDGR_Group = "BANK";
            oFA_LGR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            int iRecordFound = 0;

            bool bResult = SaitexBL.Interface.Method.FA_LGR_MST.InsertLedgerMaster(oFA_LGR_MST, out iRecordFound);

            if (bResult)
            {

            }
            else if (iRecordFound > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Ledger is already saved.. Please enter another.');", true);
            }
        }
        catch { throw; }
    }

    private void MaxLedgerCode()
    {
        string x = "";
        int y = 0;
        try
        {
            DataTable dt = SaitexBL.Interface.Method.FA_LGR_MST.GetMaxLedgerCode();

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
                        LedgerCode = y.ToString();
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
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }
}