using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;

public partial class Module_Inventory_Pages_vendor_master_approval : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
       oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
       if (!Page.IsPostBack)
       {         
           BindPrtyCity();
           Bindvendorcode();
           Bindvendorcategory();
           ddlstatus.SelectedValue = "0";
           bindVendorListForApproval();
       }
    }

  
    private void BindPrtyCity()
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_VENDOR_MST.SelectVendorMst();
            ddlprtycity.Items.Clear();
            ddlprtycity.DataSource = dt;
            ddlprtycity.DataValueField = "PRTY_CITY";
            ddlprtycity.DataTextField = "PRTY_CITY";
            ddlprtycity.DataBind();
            ddlprtycity.Items.Insert(0, new ListItem("--------SELECT-------", string.Empty));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void Bindvendorcode()
    {
        try
        {

            ddlvendorcode.Items.Clear();
            DataTable dt = GET_MOM_DATA("", "PRTY_GRP_CODE");
            ddlvendorcode.DataSource = dt;
            ddlvendorcode.DataValueField = "PRTY_GRP_CODE";
            ddlvendorcode.DataTextField = "PRTY_GRP_CODE";
            ddlvendorcode.DataBind();
            ddlvendorcode.Items.Insert(0, new ListItem("--------SELECT-------", string.Empty));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private DataTable GET_MOM_DATA(string Text, string PRTY_GRP_CODE)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            string CommandText = "select DISTINCT PRTY_GRP_CODE from TX_VENDOR_MST where Del_Status=0 and  comp_code='" + oUserLoginDetail.COMP_CODE + "'  ";
            string WhereClause = " and PRTY_GRP_CODE like :SearchQuery";
            string SortExpression = " order by PRTY_GRP_CODE asc";
            string SearchQuery = Text + "%";
            DataTable dt = SaitexBL.Interface.Method.TX_VENDOR_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, PRTY_GRP_CODE, oUserLoginDetail.COMP_CODE);
            return dt;
        }
        catch (Exception EX)
        {
            throw EX;
        }
    }

    private void Bindvendorcategory()
    {
        try
        {


            ddlcategory.Items.Clear();
            DataTable dt = GET_VEND_DATA("", "VENDOR_CAT_CODE");
            ddlcategory.DataSource = dt;
            ddlcategory.DataValueField = "VENDOR_CAT_CODE";
            ddlcategory.DataTextField = "VENDOR_CAT_CODE";
            ddlcategory.DataBind();
            ddlcategory.Items.Insert(0, new ListItem("--------SELECT-------", string.Empty));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private DataTable GET_VEND_DATA(string Text, string PRTY_GRP_CODE)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            string CommandText = "select DISTINCT VENDOR_CAT_CODE from TX_VENDOR_MST where Del_Status=0 and  comp_code='" + oUserLoginDetail.COMP_CODE + "'  ";
            string WhereClause = " and VENDOR_CAT_CODE like :SearchQuery";
            string SortExpression = " order by VENDOR_CAT_CODE asc";
            string SearchQuery = Text + "%";
            DataTable dt = SaitexBL.Interface.Method.TX_VENDOR_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, PRTY_GRP_CODE, oUserLoginDetail.COMP_CODE);
            return dt;
        }
        catch (Exception EX)
        {
            throw EX;
        }
    }



    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {


        string URL = "../Reports/vendor_master_approvalRPT.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string msg = string.Empty;           

            DataTable dtDetail = CreateDataTable();
            int totalRows = gvPartyList.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvPartyList.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblParty = (Label)thisGridViewRow.FindControl("lblParty");             
                    CheckBox Approved = (CheckBox)thisGridViewRow.FindControl("chkApproved");
                    CheckBox Reject = (CheckBox)thisGridViewRow.FindControl("chkReject");           
                    TextBox Remarks = (TextBox)thisGridViewRow.FindControl("txtRemarks");

                    if (Approved.Checked || Reject.Checked)
                    {
                        DataRow dr = dtDetail.NewRow();
                            dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                            dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                            dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                            dr["PRTY_CODE"] = lblParty.ToolTip;
                            dr["PRTY_NAME"] = lblParty.Text;
                        
                        if(Approved.Checked)
                            dr["CONF_FLAG"] = "1";
                       else if (Reject.Checked)
                            dr["CONF_FLAG"] = "3";
                        else
                            dr["CONF_FLAG"] = "0";
                            dr["CONF_DATE"] = DateTime.Now;
                            dr["CONF_BY"] = oUserLoginDetail.Username;
                            dr["CONF_REMARKS"] = Remarks.Text;
                            dtDetail.Rows.Add(dr);

                       
                        
                    }
                }
            }

            if (msg != string.Empty)
                CommonFuction.ShowMessage(msg);

            int iResult = SaitexBL.Interface.Method.TX_VENDOR_MST.Update_Vendor_Approval(dtDetail);
            if (iResult > 0)
            {             
                CommonFuction.ShowMessage("Approved Successfully.");
                bindVendorListForApproval();
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
                Response.Redirect("~/Module/Admin/pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");      
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");

    }


    private DataTable CreateDataTable()
    {
        try
        {
            DataTable dtIndentDetail = new DataTable();
            dtIndentDetail.Columns.Add("YEAR", typeof(int));
            dtIndentDetail.Columns.Add("COMP_CODE", typeof(string));
            dtIndentDetail.Columns.Add("BRANCH_CODE", typeof(string));
            dtIndentDetail.Columns.Add("PRTY_CODE", typeof(string));
            dtIndentDetail.Columns.Add("PRTY_NAME", typeof(string));           
            dtIndentDetail.Columns.Add("CONF_DATE", typeof(DateTime));
            dtIndentDetail.Columns.Add("CONF_BY", typeof(string));
            dtIndentDetail.Columns.Add("CONF_REMARKS", typeof(string));
            dtIndentDetail.Columns.Add("CONF_FLAG", typeof(string));
            return dtIndentDetail;
        }
        catch
        {
            throw;
        }
    }

    protected void btngetdata_Click(object sender, EventArgs e)
    {
        try
        {
            bindVendorListForApproval();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    private void bindVendorListForApproval()
    {       
        try
        {
            SaitexDM.Common.DataModel.TX_VENDOR_MST oTX_VENDOR_MST = new SaitexDM.Common.DataModel.TX_VENDOR_MST();
            oTX_VENDOR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_VENDOR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_VENDOR_MST.PRTY_CODE = txtPartyName.Text.Trim();
            oTX_VENDOR_MST.PRTY_CITY = ddlprtycity.SelectedValue.ToString();
            oTX_VENDOR_MST.REGION = ddlregion.SelectedValue.ToString(); 
            oTX_VENDOR_MST.VENDOR_CODE = ddlvendorcode.SelectedValue.ToString();
            oTX_VENDOR_MST.VENDOR_CAT = ddlcategory.SelectedValue.ToString();
            oTX_VENDOR_MST.V_STATUS = ddlstatus.SelectedValue; 
            oTX_VENDOR_MST.CREDIT_LIMIT = txtCredit.Text.Trim();
            int pinCode = 0;
            int.TryParse(txtpincode.Text.Trim(), out pinCode);
            oTX_VENDOR_MST.PIN_CODE = pinCode;          
            oTX_VENDOR_MST.MOBILE_NO = txtmobile.Text.Trim();
            DataTable dt = SaitexBL.Interface.Method.TX_VENDOR_MST.GetVendorDataForApproval(oTX_VENDOR_MST);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    gvPartyList.DataSource = dt;
                    gvPartyList.DataBind();
                    lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                }
                else
                {
                    lblTotalRecord.Text = "No Party for approval";
                    gvPartyList.DataSource = null;
                    gvPartyList.DataBind();
                    lblTotalRecord.Text = "0";
                }
            }
            else
            {
                lblTotalRecord.Text = "No Party for approval";
                gvPartyList.DataSource = null;
                gvPartyList.DataBind();
                lblTotalRecord.Text = "0";
            }
        }
        catch
        {
            throw;
        }
    }

    protected void gvPartyList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            bindVendorListForApproval();

            gvPartyList.PageIndex = e.NewPageIndex;
            gvPartyList.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
