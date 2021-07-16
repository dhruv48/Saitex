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
using Common;
using errorLog;
public partial class Module_Fiber_Controls_FiberSubCatControl : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    private string _FormHeading;
    public string FormHeading
    {
        get { return _FormHeading; }
        set { _FormHeading = value; }
    }
    private string _MasterName;
    public string MasterName
    {
        get { return _MasterName; }
        set { _MasterName = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
               // oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetails"];
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
            txtSubCatName.Text = "";
            // txtSubCatCode.Text = "";
            txtSubCatDesc.Text = "";
            lblMessage.Text = string.Empty;
            lblErrorMessage.Text = string.Empty;
            lblFormHeading.Text = _FormHeading;
            tdSave.Visible = true;
            tdFind.Visible = true;
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            lblMode.Text = "Save";
            imgbtnClear.Visible = true;
            // txtSubCatCode.ReadOnly = false;
            BindControls();
           // ddlMasterName.SelectedIndex = -1;
            ddlMasterCode.SelectedIndex = -1;

        }
        catch
        {
            throw;
        }

    }
    private void SaveTransationData()
    {
        try
        {
            //if (ddlMasterName.SelectedValue.ToString()!= "" && ddlMasterName.SelectedValue.ToString()!= null)
            if(_MasterName != "")
            {
                SaitexDM.Common.DataModel.TX_FIBER_MST_SUBCAT oTX_FIBER_MST_SUBCAT = new SaitexDM.Common.DataModel.TX_FIBER_MST_SUBCAT();

               
                //oTX_FIBER_MST_SUBCAT.MST_NAME = ddlMasterName.SelectedValue.ToString();

                //_MasterName = oTX_FIBER_MST_SUBCAT.MST_NAME;
                oTX_FIBER_MST_SUBCAT.MST_NAME = _MasterName;
                oTX_FIBER_MST_SUBCAT.MST_CODE = ddlMasterCode.SelectedValue.ToString().Trim();
                oTX_FIBER_MST_SUBCAT.FIBR_SUBCAT = CommonFuction.funFixQuotes(txtSubCatName.Text.ToUpper().Trim());
                // oTX_FIBER_MST_SUBCAT.FIBR_SUBCAT_CODE = CommonFuction.funFixQuotes(txtSubCatCode.Text.ToUpper().Trim());
                oTX_FIBER_MST_SUBCAT.FIBR_SUBCAT_DESC = CommonFuction.funFixQuotes(txtSubCatDesc.Text.ToUpper().Trim());
                oTX_FIBER_MST_SUBCAT.TUSER = oUserLoginDetail.UserCode;
                oTX_FIBER_MST_SUBCAT.COMP_CODE = oUserLoginDetail.COMP_CODE;

                bool iRecordEffected = SaitexBL.Interface.Method.TX_FIBER_MST_SUBCAT.Insert(oTX_FIBER_MST_SUBCAT);
                if (iRecordEffected)
                {
                    InitializePage();
                    CommonFuction.ShowMessage("data saved successfully");
                }
                else
                {
                    CommonFuction.ShowMessage("data saving failed");
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
            URL += "?FormHeading=" + _FormHeading;
            URL += "&MasterName=" + _MasterName;

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
    private void UpdateTrnMaster()
    {
        try
        {
            if (_MasterName != "")
            {
                SaitexDM.Common.DataModel.TX_FIBER_MST_SUBCAT oTX_FIBER_MST_SUBCAT = new SaitexDM.Common.DataModel.TX_FIBER_MST_SUBCAT();
                oTX_FIBER_MST_SUBCAT.TUSER = oUserLoginDetail.UserCode;
               // oTX_FIBER_MST_SUBCAT.MST_NAME = ddlMasterName.SelectedValue.ToString();
                oTX_FIBER_MST_SUBCAT.MST_NAME = _MasterName;
                oTX_FIBER_MST_SUBCAT.MST_CODE = ddlMasterCode.SelectedValue.ToString();
                oTX_FIBER_MST_SUBCAT.FIBR_SUBCAT = CommonFuction.funFixQuotes(txtSubCatName.Text.ToUpper().Trim());
                oTX_FIBER_MST_SUBCAT.FIBR_SUBCAT_DESC = CommonFuction.funFixQuotes(txtSubCatDesc.Text.ToUpper().Trim());
                oTX_FIBER_MST_SUBCAT.COMP_CODE = oUserLoginDetail.COMP_CODE;
                bool iRecordEffected = SaitexBL.Interface.Method.TX_FIBER_MST_SUBCAT.Update(oTX_FIBER_MST_SUBCAT);
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
            UpdateTrnMaster();
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
            if (txtSubCatName.Text != "")
            {
                SaveTransationData();
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

    protected void DeleteTrnMaster()
    {
        try
        {
            if (_MasterName != "")
            {
                SaitexDM.Common.DataModel.TX_FIBER_MST_SUBCAT oTX_FIBER_MST_SUBCAT = new SaitexDM.Common.DataModel.TX_FIBER_MST_SUBCAT();

                oTX_FIBER_MST_SUBCAT.TUSER = Session["urLoginId"].ToString().Trim();
                oTX_FIBER_MST_SUBCAT.MST_NAME = _MasterName;
                oTX_FIBER_MST_SUBCAT.FIBR_SUBCAT = CommonFuction.funFixQuotes(txtSubCatName.Text.ToUpper().Trim());
                oTX_FIBER_MST_SUBCAT.FIBR_SUBCAT_DESC = CommonFuction.funFixQuotes(txtSubCatDesc.Text.ToUpper().Trim());
                oTX_FIBER_MST_SUBCAT.COMP_CODE = oUserLoginDetail.COMP_CODE;

                bool iRecordEffected = SaitexBL.Interface.Method.TX_FIBER_MST_SUBCAT.Delete(oTX_FIBER_MST_SUBCAT);
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
            if (txtSubCatName.Text != "")
            {
                DeleteTrnMaster();

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
    protected void GetFindData()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_FIBER_MST_SUBCAT.SelectByMST_CODE(CommonFuction.funFixQuotes(cmbFind.SelectedText.Trim()), cmbFind.SelectedValue.Trim(), oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {

                txtSubCatName.Text = dt.Rows[0]["FIBR_SUBCAT"].ToString();
                txtSubCatDesc.Text = dt.Rows[0]["FIBR_SUBCAT_DESC"].ToString();
                tdSave.Visible = false;
                tdUpdate.Visible = true;
                tdDelete.Visible = true;
                lblMode.Text = "Find";
                cmbFind.Visible = false;


            }
            else
            {

                txtSubCatName.ReadOnly = false;
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
            imgbtnDelete.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
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
        cmbFind.DataTextField = "MST_NAME";
        cmbFind.DataValueField = "MST_CODE";
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
            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV("select MST_NAME,MST_CODE,MST_DESC from ( select * from TX_MASTER_TRN where MST_NAME='" + _MasterName + "' and COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' and Del_status='0' ) asd ", " WHERE MST_CODE like :SearchQuery  or MST_DESC like :SearchQuery ", " ORDER BY MST_CODE", "", text + "%", "");
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
    //private void bindMasterName()
    //{
    //    try
    //    {
    //        DataTable dt = SaitexBL.Interface.Method.TX_MASTER_MST.Select(oUserLoginDetail.COMP_CODE);

    //        ddlMasterName.DataTextField = "MST_NAME";
    //        ddlMasterName.DataSource = dt;
    //        ddlMasterName.DataBind();
    //        ddlMasterName.Items.Insert(0, new ListItem("---------Select----------", ""));
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonFuction.ShowMessage(ex.Message);
    //        ErrHandler.WriteError(ex.Message);
    //    }
    //}
    private void BindFiberCat(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {

                ddlMasterCode.Items.Clear();
                ddlMasterCode.DataSource = dt;
                ddlMasterCode.DataTextField = "MST_CODE";
                ddlMasterCode.DataValueField = "MST_CODE";
                ddlMasterCode.DataBind();
                ddlMasterCode.Items.Insert(0, new ListItem("------Select------", "0"));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void BindControls()
    {
        //bindMasterName();
        BindFiberCat("FIBER_MASTER");
    }

    //protected void ddlMasterName_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    _MasterName = ddlMasterName.SelectedValue.ToString();
    //}
}
