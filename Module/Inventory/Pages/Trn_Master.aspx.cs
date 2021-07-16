using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Common;
using errorLog;

public partial class Inventory_Trn_Master : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {

        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        if (Session["urLoginId"] != null)
        {
            if (!IsPostBack)
            {
                cmbFind.Visible = false;
                cmbFind.SelectedIndex = -1;
                bindMasterName();
                txtMstCode.Visible = true;
                tdUpdate.Visible = false;
                tdDelete.Visible = false;
                tdFind.Visible = true;
                lblMode.Text = "Save";
                imgbtnClear.Visible = true;
            }
        }
        else
        {
            Response.Redirect("~/Default.aspx", false);
        }

    }
    private void SaveTransactionData()
    {
        try
        {
            if (ddlMasterName.SelectedValue.Trim() != "")
            {
                SaitexDM.Common.DataModel.TX_MASTER_TRN oTX_MASTER_TRN = new SaitexDM.Common.DataModel.TX_MASTER_TRN();

                oTX_MASTER_TRN.TUSER = Session["urLoginId"].ToString().Trim();
                oTX_MASTER_TRN.MST_NAME = ddlMasterName.SelectedItem.Text.ToUpper().Trim();
                oTX_MASTER_TRN.MST_CODE = CommonFuction.funFixQuotes(txtMstCode.Text.ToUpper().Trim());
                oTX_MASTER_TRN.MST_DESC = CommonFuction.funFixQuotes(txtMstDesc.Text.Trim());
                oTX_MASTER_TRN.COMP_CODE = oUserLoginDetail.COMP_CODE;
             

                bool iRecordEffected = SaitexBL.Interface.Method.TX_MASTER_TRN.Insert(oTX_MASTER_TRN);
                if (iRecordEffected)
                {
                    txtMstCode.Text = "";
                    ddlMasterName.SelectedIndex = -1;
                    txtMstDesc.Text = "";                
                    cmbFind.Visible = false;
                    cmbFind.SelectedIndex = -1;
                    txtMstCode.ReadOnly = false; ddlMasterName.Enabled = true;
                    CommonFuction.ShowMessage("data saved successfully");
                }
                else
                {
                    CommonFuction.ShowMessage("data saving failed");
                }
            }
            else
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Please select Master name');", true);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ex.Message); ErrHandler.WriteError(ex.Message);
        }

    }
    private void bindMasterName()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_MST.Select(oUserLoginDetail.COMP_CODE);

            ddlMasterName.DataTextField = "MST_NAME";
            ddlMasterName.DataSource = dt;
            ddlMasterName.DataBind();
            ddlMasterName.Items.Insert(0, new ListItem("---------Select----------", ""));
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ex.Message);
            ErrHandler.WriteError(ex.Message);
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
                Response.Redirect("~/Module/Admin/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Inventory/Pages/Trn_Master.aspx", false);
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string URL = "Trn_MSt_Opt.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=500,height=500');", true);
    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        cmbFind.Visible = true;
        tdSave.Visible = false;
        tdUpdate.Visible = true;
        tdDelete.Visible = true;
        lblMode.Text = "Find";
        ddlMasterName.Visible = false;
    }
    private void updateTrnMaster()
    {
        try
        {
            if (ddlMasterName.SelectedValue.Trim() != "")
            {
                SaitexDM.Common.DataModel.TX_MASTER_TRN oTX_MASTER_TRN = new SaitexDM.Common.DataModel.TX_MASTER_TRN();

                oTX_MASTER_TRN.TUSER = Session["urLoginId"].ToString().Trim();
                oTX_MASTER_TRN.MST_NAME = ddlMasterName.SelectedItem.Text.ToUpper().Trim();
                oTX_MASTER_TRN.MST_CODE = CommonFuction.funFixQuotes(txtMstCode.Text.ToUpper().Trim());
                oTX_MASTER_TRN.MST_DESC = CommonFuction.funFixQuotes(txtMstDesc.Text.Trim());              
                oTX_MASTER_TRN.COMP_CODE = oUserLoginDetail.COMP_CODE;

                bool iRecordEffected = SaitexBL.Interface.Method.TX_MASTER_TRN.Update(oTX_MASTER_TRN);
                if (iRecordEffected)
                {
                    txtMstCode.Text = "";
                    ddlMasterName.SelectedIndex = -1;
                    txtMstDesc.Text = "";               
                    cmbFind.Visible = false;
                    cmbFind.SelectedIndex = -1;
                    txtMstCode.ReadOnly = false; ddlMasterName.Enabled = true;
                    CommonFuction.ShowMessage("data updated successfully");
                }
                else
                {
                    CommonFuction.ShowMessage("data updation failed");
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Please select Master name');", true);

            }
        }
        catch (Exception ex)
        { CommonFuction.ShowMessage(ex.Message); ErrHandler.WriteError(ex.Message); }

    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlMasterName.SelectedIndex != '0')
        {
            updateTrnMaster();
        }
        else
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Pls Choose Master Code ');", true);

        }
    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        if (txtMstCode.Text != "")
        {
            SaveTransactionData();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Pls Enter Master Code');", true);
        }
    }
    private void deleteTrnMaster()
    {

        try
        {
            if (ddlMasterName.SelectedValue.Trim() != "")
            {
                SaitexDM.Common.DataModel.TX_MASTER_TRN oTX_MASTER_TRN = new SaitexDM.Common.DataModel.TX_MASTER_TRN();

                oTX_MASTER_TRN.TUSER = Session["urLoginId"].ToString().Trim();
                oTX_MASTER_TRN.MST_NAME = ddlMasterName.SelectedItem.Text.Trim();
                oTX_MASTER_TRN.MST_CODE = CommonFuction.funFixQuotes(txtMstCode.Text.Trim());
                oTX_MASTER_TRN.MST_DESC = CommonFuction.funFixQuotes(txtMstDesc.Text.Trim());
                oTX_MASTER_TRN.COMP_CODE = oUserLoginDetail.COMP_CODE;

                bool iRecordEffected = SaitexBL.Interface.Method.TX_MASTER_TRN.Delete(oTX_MASTER_TRN);
                if (iRecordEffected)
                {
                    txtMstCode.Text = "";
                    ddlMasterName.SelectedIndex = -1;
                    txtMstDesc.Text = "";
                    cmbFind.Visible = false;
                    cmbFind.SelectedIndex = -1;
                    txtMstCode.ReadOnly = false; ddlMasterName.Enabled = true;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('data deleted successfully');", true);
                }
                else
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Data deletion failed');", true);
                }
            }
            else
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Please select Master name');", true);

            }
        }
        catch (Exception ex)
        { CommonFuction.ShowMessage(ex.Message); ErrHandler.WriteError(ex.Message); }
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        if (txtMstCode.Text != "")
        {
            deleteTrnMaster();
        }
        else
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Find a record to Delete');", true);
        }

    }
    private void GetFindData()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_CODE(CommonFuction.funFixQuotes(cmbFind.SelectedText.Trim()), cmbFind.SelectedValue.Trim(), oUserLoginDetail.COMP_CODE);

            if (dt != null && dt.Rows.Count > 0)
            {
                ddlMasterName.SelectedIndex = ddlMasterName.Items.IndexOf(ddlMasterName.Items.FindByText(dt.Rows[0]["MST_NAME"].ToString().Trim()));

                txtMstCode.Text = dt.Rows[0]["MST_CODE"].ToString();
                txtMstDesc.Text = dt.Rows[0]["MST_DESC"].ToString();              
                tdSave.Visible = false;
                tdUpdate.Visible = true;
                tdDelete.Visible = true;
                lblMode.Text = "Find";
                cmbFind.Visible = false;
                txtMstCode.ReadOnly = true;
                ddlMasterName.Visible = false;
            }
            else
            {
                txtMstCode.ReadOnly = false; ddlMasterName.Enabled = true;
                cmbFind.Visible = true;
                cmbFind.SelectedIndex = -1;
            }

        }
        catch (Exception ex)
        { CommonFuction.ShowMessage(ex.Message); ErrHandler.WriteError(ex.Message); }

    }
    //protected void lnkSearch_Click(object sender, EventArgs e)
    //{
    //    string URL = "SearchTrn.aspx";

    //    URL = URL + "?MstCodeId=" + txtFind.ClientID;
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=700,height=500');", true);
    //    //string URL = "FindTrn.aspx";
    //    //URL = URL + "?MstCode=" + txtFind.ClientID;
    //    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=400,height=400');", true);
    //    tdSave.Visible = false;
    //    tdUpdate.Visible = true;
    //    tdDelete.Visible = true;
    //    lblMode.Text = "Find";
    //}
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnSave.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save this record ? ')");
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");
        imgbtnDelete.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");

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
        DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV("select * from (select MST_NAME,MST_CODE,MST_DESC,CODE_PREFIX from TX_MASTER_TRN where COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' and del_status='0' ) asd ", " WHERE MST_NAME like :SearchQuery or MST_CODE like :SearchQuery  or MST_DESC like :SearchQuery ", " ORDER BY MST_NAME", "", text + "%", "");
        return dt;
    }
    protected void cmbFind_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        //txtMstCode.Text = cmbFind.SelectedValue.Trim();
        //txtMstCode.ReadOnly = true;
        //ddlMasterName.SelectedIndex = ddlMasterName.Items.IndexOf(ddlMasterName.Items.FindByValue(cmbFind.SelectedValue.Trim()));
        GetFindData();
    }
}
