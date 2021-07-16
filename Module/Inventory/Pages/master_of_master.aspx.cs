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

public partial class Module_Inventory_Pages_master_of_master : System.Web.UI.Page
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
                //bindMasterName();
                txtMstName.Visible = true;
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
            if (!string.IsNullOrEmpty(txtMstName.Text))
            {
                var oTX_MASTER_TRN = new SaitexDM.Common.DataModel.TX_MASTER_TRN();
                oTX_MASTER_TRN.TUSER = Session["urLoginId"].ToString().Trim();              
                oTX_MASTER_TRN.MST_NAME = CommonFuction.funFixQuotes(txtMstName.Text.ToUpper().Trim());
                oTX_MASTER_TRN.MST_DESC = CommonFuction.funFixQuotes(txtMstDesc.Text.Trim());
                oTX_MASTER_TRN.COMP_CODE = oUserLoginDetail.COMP_CODE;


                bool iRecordEffected = SaitexBL.Interface.Method.TX_MASTER_TRN.Insert_MOM(oTX_MASTER_TRN);
                if (iRecordEffected)
                {
                    txtMstName.Text = string.Empty;
                    txtMstDesc.Text = string.Empty;
                    cmbFind.Visible = false;
                    cmbFind.SelectedIndex = -1;
                    txtMstName.ReadOnly = false; 
                    CommonFuction.ShowMessage("data saved successfully");
                }
                else
                {
                    CommonFuction.ShowMessage("data saving failed");
                }
            }
            else
            {
                txtMstName.Focus();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Please enter master name');", true);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ex.Message); ErrHandler.WriteError(ex.Message);
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
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Inventory/Pages/Master_Of_Master.aspx", false);
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        //string URL = "Trn_MSt_Opt.aspx";
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=500,height=500');", true);
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        cmbFind.Visible = true;
        tdSave.Visible = false;
        tdUpdate.Visible = true;
        tdDelete.Visible = true;
        lblMode.Text = "Find";
       
    }

    private void updateTrnMaster()
    {
        try
        {
            if (!string.IsNullOrEmpty(txtMstName.Text))
            {
                var oTX_MASTER_TRN = new SaitexDM.Common.DataModel.TX_MASTER_TRN();
                oTX_MASTER_TRN.TUSER = Session["urLoginId"].ToString().Trim();
                oTX_MASTER_TRN.MST_NAME = CommonFuction.funFixQuotes(txtMstName.Text.ToUpper().Trim());               
                oTX_MASTER_TRN.MST_DESC = CommonFuction.funFixQuotes(txtMstDesc.Text.Trim());
                oTX_MASTER_TRN.COMP_CODE = oUserLoginDetail.COMP_CODE;

                bool iRecordEffected = SaitexBL.Interface.Method.TX_MASTER_TRN.Update_MOM(oTX_MASTER_TRN);
                if (iRecordEffected)
                {
                    txtMstName.Text = "";                  
                    txtMstDesc.Text = "";
                    cmbFind.Visible = false;
                    cmbFind.SelectedIndex = -1;
                    txtMstName.ReadOnly = false; 
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
        if (!string.IsNullOrEmpty(txtMstName.Text))
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
        if (!string.IsNullOrEmpty(txtMstName.Text))
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
            if (!string.IsNullOrEmpty(txtMstName.Text))
            {
                var oTX_MASTER_TRN = new SaitexDM.Common.DataModel.TX_MASTER_TRN();
                oTX_MASTER_TRN.TUSER = Session["urLoginId"].ToString().Trim();
                oTX_MASTER_TRN.MST_NAME = CommonFuction.funFixQuotes(txtMstName.Text.Trim());              
                oTX_MASTER_TRN.MST_DESC = CommonFuction.funFixQuotes(txtMstDesc.Text.Trim());
                oTX_MASTER_TRN.COMP_CODE = oUserLoginDetail.COMP_CODE;

                bool iRecordEffected = SaitexBL.Interface.Method.TX_MASTER_TRN.Delete_MOM(oTX_MASTER_TRN);
                if (iRecordEffected)
                {
                    txtMstName.Text = "";                   
                    txtMstDesc.Text = "";
                    cmbFind.Visible = false;
                    cmbFind.SelectedIndex = -1;
                    txtMstName.ReadOnly = false; 

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
        if (txtMstName.Text != "")
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
            var dt = SaitexBL.Interface.Method.TX_MASTER_TRN.Select_MOM_BY_MST_NAME( cmbFind.SelectedValue.Trim(), oUserLoginDetail.COMP_CODE);

            if (dt != null && dt.Rows.Count > 0)
            {
               
                txtMstName.Text = dt.Rows[0]["MST_NAME"].ToString();
                txtMstDesc.Text = dt.Rows[0]["MST_DESC"].ToString();
                tdSave.Visible = false;
                tdUpdate.Visible = true;
                tdDelete.Visible = true;
                lblMode.Text = "Find";
                cmbFind.Visible = false;
                txtMstName.ReadOnly = true;               
            }
            else
            {
                txtMstName.ReadOnly = false; 
                cmbFind.Visible = true;
                cmbFind.SelectedIndex = -1;
            }

        }
        catch (Exception ex)
        { CommonFuction.ShowMessage(ex.Message); ErrHandler.WriteError(ex.Message); }

    }
   
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
        var data = GetTrn(e.Text.ToUpper().Trim());

        cmbFind.DataSource = data;
        cmbFind.DataTextField = "MST_NAME";
        cmbFind.DataValueField = "MST_NAME";
        cmbFind.DataBind();

        // Calculating the numbr of items loaded so far in the ComboBox
        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

        // Getting the total number of items that start with the typed text
        e.ItemsCount = data.Rows.Count;
    }

    protected DataTable GetTrn(string text)
    {
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV("select * from (select MST_NAME,MST_DESC from TX_MASTER_MST where COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' and del_status=0 ) asd ", " WHERE MST_NAME like :SearchQuery  or MST_DESC like :SearchQuery ", " ORDER BY MST_NAME", "", text + "%", "");
        
    }

    protected void cmbFind_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        GetFindData();
    }
}
