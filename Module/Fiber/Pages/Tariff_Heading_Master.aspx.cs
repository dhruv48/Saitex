using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;


public partial class Module_Fiber_Pages_Tariff_Heading_Master : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;


    public string FormHeading
    {
        get ;
        set ;
    }
    public string MasterName
    {
        get ;
        set ;
    }

     private int _Length = 50;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            FormHeading = "Tariff Heading Master";
            MasterName = "TARIFF_HEADING";
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            if (!IsPostBack)
            {
                GetMaxLengthOfEntry();
                InitializePage();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }

    private void InitializePage()
    {
        try
        {

            cmbFind.Visible = false;
            cmbFind.SelectedIndex = -1;

            txtMstCode.Text = "";
            txtMstDesc.Text = "";
            ddlpallettype.SelectedIndex = -1;
            ddlpallettype.SelectedIndex = -1;
            lblMessage.Text = string.Empty;
            lblErrorMessage.Text = string.Empty;
            lblFormHeading.Text = FormHeading;
            txtMstCode.MaxLength = _Length;
            txtMstDesc.MaxLength = 150;

            tdSave.Visible = true;
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            tdFind.Visible = true;
            lblMode.Text = "Save";
            imgbtnClear.Visible = true;
            txtMstCode.ReadOnly = false;

            txtCodePrefix.Text = string.Empty;
        }
        catch
        {
            throw;
        }
    }

    private void GetMaxLengthOfEntry()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_MST.SelectByMST_NAME(MasterName, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                int Len = 0;
                int.TryParse(dt.Rows[0]["MAX_FLD_LNGTH"].ToString(), out Len);

                if (Len > 0)
                    _Length = Len;
            }
        }
        catch
        {
            throw;
        }
    }

    private void SaveTransactionData()
    {
        try
        {
            if (MasterName != "")
            {
                SaitexDM.Common.DataModel.TX_MASTER_TRN oTX_MASTER_TRN = new SaitexDM.Common.DataModel.TX_MASTER_TRN();

                oTX_MASTER_TRN.TUSER = oUserLoginDetail.UserCode;
                oTX_MASTER_TRN.MST_NAME = MasterName;
                oTX_MASTER_TRN.MST_CODE = CommonFuction.funFixQuotes(txtMstCode.Text.ToUpper().Trim());
                oTX_MASTER_TRN.MST_DESC = CommonFuction.funFixQuotes(txtMstDesc.Text.ToUpper().Trim());
                oTX_MASTER_TRN.CODE_PREFIX = CommonFuction.funFixQuotes(txtCodePrefix.Text.Trim());
                oTX_MASTER_TRN.COMP_CODE = oUserLoginDetail.COMP_CODE;

                bool iRecordEffected = SaitexBL.Interface.Method.TX_MASTER_TRN.InsertForFabricForm(oTX_MASTER_TRN);
                if (iRecordEffected)
                {
                    InitializePage();
                    CommonFuction.ShowMessage("Data saved successfully");
                }
                else
                {
                    CommonFuction.ShowMessage("Data saving failed");
                }
            }
            else
            {
                CommonFuction.ShowMessage("Please select Master name");
            }
        }
        catch
        {
            throw;
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exiting.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InitializePage();
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
            string URL = "../../Inventory/reports/Trn_MST_RPT.aspx";
            URL += "?FormHeading=" + FormHeading;
            URL += "&MasterName=" + MasterName;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=500,height=500');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            cmbFind.Visible = true;
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            lblMode.Text = "Find";
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Enabling Update Mode.\r\nSee error log for detail."));
        }
    }

    private void updateTrnMaster()
    {
        try
        {
            if (MasterName != "")
            {
                SaitexDM.Common.DataModel.TX_MASTER_TRN oTX_MASTER_TRN = new SaitexDM.Common.DataModel.TX_MASTER_TRN();

                oTX_MASTER_TRN.TUSER = oUserLoginDetail.UserCode;
                oTX_MASTER_TRN.MST_NAME = MasterName;
                oTX_MASTER_TRN.MST_CODE = CommonFuction.funFixQuotes(txtMstCode.Text.ToUpper().Trim());
                oTX_MASTER_TRN.MST_DESC = CommonFuction.funFixQuotes(txtMstDesc.Text.ToUpper().Trim());
                oTX_MASTER_TRN.CODE_PREFIX = CommonFuction.funFixQuotes(txtCodePrefix.Text.Trim());
                oTX_MASTER_TRN.COMP_CODE = oUserLoginDetail.COMP_CODE;

                bool iRecordEffected = SaitexBL.Interface.Method.TX_MASTER_TRN.UpdateForFabricForm(oTX_MASTER_TRN);
                if (iRecordEffected)
                {
                    InitializePage();
                    CommonFuction.ShowMessage("data updated successfully");
                }
                else
                {
                    CommonFuction.ShowMessage("data updation failed");
                }
            }
            else
            {
                CommonFuction.ShowMessage("Please select Master name");
            }
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            updateTrnMaster();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating Data.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (txtMstCode.Text != "")
            {
                SaveTransactionData();
            }
            else
            {
                lblMessage.Text = "Please Enter Master Code";
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving Data.\r\nSee error log for detail."));
        }
    }

    private void deleteTrnMaster()
    {
        try
        {
            if (MasterName != "")
            {
                SaitexDM.Common.DataModel.TX_MASTER_TRN oTX_MASTER_TRN = new SaitexDM.Common.DataModel.TX_MASTER_TRN();

                oTX_MASTER_TRN.TUSER = Session["urLoginId"].ToString().Trim();
                oTX_MASTER_TRN.MST_NAME = MasterName;
                oTX_MASTER_TRN.MST_CODE = CommonFuction.funFixQuotes(txtMstCode.Text.Trim());
                oTX_MASTER_TRN.MST_DESC = CommonFuction.funFixQuotes(txtMstDesc.Text.Trim());
                oTX_MASTER_TRN.COMP_CODE = oUserLoginDetail.COMP_CODE;

                bool iRecordEffected = SaitexBL.Interface.Method.TX_MASTER_TRN.Delete(oTX_MASTER_TRN);
                if (iRecordEffected)
                {
                    InitializePage();
                    CommonFuction.ShowMessage("data deleted successfully");
                }
                else
                {
                    CommonFuction.ShowMessage("data deletion failed");
                }
            }
            else
            {
                CommonFuction.ShowMessage("Please select Master name");
            }
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (txtMstCode.Text != "")
            {
                deleteTrnMaster();
            }
            else
            {
                lblMessage.Text = "Find a record to Delete";
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Deleting Data.\r\nSee error log for detail."));
        }
    }

    private void GetFindData()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_CODE(CommonFuction.funFixQuotes(cmbFind.SelectedText.Trim()), cmbFind.SelectedValue.Trim(), oUserLoginDetail.COMP_CODE);

            if (dt != null && dt.Rows.Count > 0)
            {

                txtMstCode.Text = dt.Rows[0]["MST_CODE"].ToString();
                txtMstDesc.Text = dt.Rows[0]["MST_DESC"].ToString();
                txtCodePrefix.Text = dt.Rows[0]["CODE_PREFIX"].ToString();
                tdSave.Visible = false;
                tdUpdate.Visible = true;
                tdDelete.Visible = true;
                lblMode.Text = "Find";
                cmbFind.Visible = false;
                txtMstCode.ReadOnly = true;

            }
            else
            {
                txtMstCode.ReadOnly = false;
                cmbFind.Visible = true;
                cmbFind.SelectedIndex = -1;
            }

        }
        catch
        {
            throw;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        try
        {
            base.OnPreRender(e);
            imgbtnSave.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save this record ? ')");
            imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");
            imgbtnDelete.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Delete this record ? ')");
            imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
            imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
            imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading Page.\r\nSee error log for detail."));
        }
    }

    protected void cmbFind_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        DataTable data = GetTrn(e.Text.ToUpper().Trim());

        cmbFind.DataSource = data;
        cmbFind.DataTextField = "MST_CODE";
        cmbFind.DataValueField = "MST_NAME";
        cmbFind.DataBind();

        // Calculating the numbr of items loaded so far in the ComboBox
        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

        // Getting the total number of items that start with the typed text
        e.ItemsCount = data.Rows.Count;
    }

    protected DataTable GetTrn(string text)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV("select MST_NAME,MST_CODE,MST_DESC from ( select * from TX_MASTER_TRN where MST_NAME='" + MasterName + "' and COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' and Del_status='0' ) asd ", " WHERE MST_CODE like :SearchQuery  or MST_DESC like :SearchQuery ", " ORDER BY MST_CODE", "", text + "%", "");
            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected void cmbFind_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        GetFindData();
    }
}
