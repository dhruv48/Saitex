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

using System.Data.OracleClient;
using errorLog;
using Common;
using DBLibrary;
using System.IO;
using Obout.ComboBox;

public partial class FA_Controls_GroupMaster : System.Web.UI.UserControl
{
    OracleDataReader dr = null;
    csSaitex obj = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["urLoginId"] != null)
            {
                SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

                if (!IsPostBack)
                {
                    lblMessage.Text = "";
                    lblMode.Text = "Save";
                    //Grid1.AutoPostBackOnSelect = false;
                    tdSave.Visible = true;
                    tdClear.Visible = true;
                    tdUpdate.Visible = false;
                    tdDelete.Visible = false;
                    chkActive.Checked = true;
                    bindddlgroupmaster();
                    bindGvGroupMaster();
                    ddlfind.Visible = false;
                    MaxGrpCode();
                    txtGroupcode.ReadOnly = true;
                    txtGroupcode.Visible = true;
                }
                if (Convert.ToInt16(Session["saveStatus"]) == 1)
                {
                    if (Request.QueryString["cId"].ToString().Trim() == "S")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Saved Successfully');", true);
                    }
                    if (Request.QueryString["cId"].ToString().Trim() == "U")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Updated Successfully');", true);
                    }
                    if (Request.QueryString["cId"].ToString().Trim() == "D")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Deleted Successfully');", true);
                    }
                    Session["saveStatus"] = 0;
                }
            }
            else
            {
                Response.Redirect("/Saitex/default.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (txtGroupcode.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Pls Select find item.');", true);
                }
                UpdateGroup();
                lblMode.Text = "Update";
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in updating the data.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //lblMode.Text = "Delete";  // Commented By Rajesh 03 Feb 2011
            //deleteGroupMaster();
            lblMessage.Text = "Sorry! Dear you can't delete any record..";
            Common.CommonFuction.ShowMessage("Sorry! Dear you can't delete any record..");
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in deleting the data.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblMode.Text = "Update";
            txtGroupcode.Visible = false;
            tdSave.Visible = false;
            //Grid1.AutoPostBackOnSelect = true;
            tdDelete.Visible = true;
            ddlfind.Visible = true;
            tdUpdate.Visible = true;
            blank();
            ddlfindfagrp();
            tdClear.Visible = true;
            tdDelete.Visible = true;
            txtGroupname.Enabled = false;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in finding the data.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in exit.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "../Help/GroupMasterHelp.htm";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Helping the data.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "../Reports/Acgrp_Mst.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=800,height=400');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in printing the data.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("/Saitex/Module/FA/Pages/GroupMaster.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in clearing the data.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnNew_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                SaveGroup();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving the data.\r\nSee error log for detail."));
        }
    }

    private void SaveGroup()
    {
        try
        {
            if (txtGroupcode.Text != "")
            {
                int iRecordFound = 0;

                SaitexDM.Common.DataModel.FA_GRP_MST oFA_GRP_MST = new SaitexDM.Common.DataModel.FA_GRP_MST();
                SaitexDM.Common.DataModel.UserLoginDetail dtLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

                oFA_GRP_MST.COMP_CODE = dtLoginDetail.COMP_CODE;
                oFA_GRP_MST.TUSER = dtLoginDetail.UserCode;

                oFA_GRP_MST.GRP_CODE = CommonFuction.funFixQuotes(txtGroupcode.Text.ToUpper().Trim());
                oFA_GRP_MST.GRP_NAME = CommonFuction.funFixQuotes(txtGroupname.Text.ToUpper().Trim());
                string pCode = "";
                if (ddlprntcode.SelectedIndex != 0)
                    pCode = CommonFuction.funFixQuotes(ddlprntcode.Text.Trim());
                oFA_GRP_MST.PARENT_CODE = pCode;
                oFA_GRP_MST.GRP_DESC = CommonFuction.funFixQuotes(txtDesc.Text.Trim());
                oFA_GRP_MST.STATUS = chkActive.Checked;

                bool bResult = SaitexBL.Interface.Method.FA_GRP_MST.InsertGroupMaster(oFA_GRP_MST, out iRecordFound);

                if (bResult)
                {
                    Session["saveStatus"] = 1;
                    Response.Redirect("./GroupMaster.aspx?cId=S", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Duplicate Record.');", true);
                    blank();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Save. Provide Group Code');", true);
            }
        }
        catch
        {
            throw;
        }
    }

    private void bindGvGroupMaster()
    {
        try
        {
            SaitexDM.Common.DataModel.FA_GRP_MST oFA_GRP_MST = new SaitexDM.Common.DataModel.FA_GRP_MST();
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.FA_GRP_MST.GetGroupMasterWithOutStatus();
            ViewState["dtGroup"] = dt;
            //lblTotalRecord.Text = "Total Record is : " + dt.Rows.Count.ToString().Trim();
            Grid1.DataSource = dt;
            Grid1.DataBind();
        }
        catch
        {
            throw;
        }
    }

    private void bindddlgroupmaster()
    {
        try
        {
            ddlprntcode.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.FA_GRP_MST.Fa_GetGroupMaster();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlprntcode.DataSource = dt;
                ddlprntcode.DataValueField = "GRP_CODE";
                ddlprntcode.DataTextField = "GRP_NAME";
                ddlprntcode.DataBind();
            }
            ddlprntcode.Items.Insert(0, new ListItem("--------- Select Parent Group -----------", "0"));
        }
        catch
        {
            throw;
        }
    }

    private void UpdateGroup()
    {
        try
        {
            if (chkActive.Checked == false)
            {
                if (!ValidateStatus())
                {
                    if (txtGroupcode.Text != "")
                    {
                        int iRecordFound = 0;
                        SaitexDM.Common.DataModel.FA_GRP_MST oFA_GRP_MST = new SaitexDM.Common.DataModel.FA_GRP_MST();
                        SaitexDM.Common.DataModel.UserLoginDetail dtLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                        oFA_GRP_MST.COMP_CODE = dtLoginDetail.COMP_CODE;
                        oFA_GRP_MST.TUSER = dtLoginDetail.UserCode;

                        oFA_GRP_MST.GRP_CODE = CommonFuction.funFixQuotes(txtGroupcode.Text.Trim());
                        oFA_GRP_MST.GRP_NAME = CommonFuction.funFixQuotes(txtGroupname.Text.Trim());
                        string pCode = "";
                        if (ddlprntcode.SelectedIndex != 0)
                            pCode = CommonFuction.funFixQuotes(ddlprntcode.Text.Trim());
                        oFA_GRP_MST.PARENT_CODE = pCode;
                        oFA_GRP_MST.GRP_DESC = CommonFuction.funFixQuotes(txtDesc.Text.Trim());
                        oFA_GRP_MST.STATUS = chkActive.Checked;

                        bool bResult = SaitexBL.Interface.Method.FA_GRP_MST.UpdateGroupMaster(oFA_GRP_MST, out iRecordFound);

                        if (bResult)
                        {
                            Session["saveStatus"] = 1;
                            Response.Redirect("./GroupMaster.aspx?cId=U", false);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Duplicate Record.');", true);
                            blank();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Save. Provide Group Code');", true);
                    }
                }
                else
                {
                    //Common.CommonFuction.ShowMessage("Dear! Your Parent Group Code status is either disabled or contain any child with enabled, you can't update using this Parent Code.");
                    lblMessage.Text = "Dear! Your Parent Group Code status is either disabled or contain any child with enabled, you can't update using this Parent Code.";
                }
            }
            else
            {
                if (txtGroupcode.Text != "")
                {
                    int iRecordFound = 0;
                    SaitexDM.Common.DataModel.FA_GRP_MST oFA_GRP_MST = new SaitexDM.Common.DataModel.FA_GRP_MST();
                    SaitexDM.Common.DataModel.UserLoginDetail dtLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                    oFA_GRP_MST.COMP_CODE = dtLoginDetail.COMP_CODE;
                    oFA_GRP_MST.BRANCH_CODE = dtLoginDetail.CH_BRANCHCODE;
                    oFA_GRP_MST.TUSER = dtLoginDetail.UserCode;

                    oFA_GRP_MST.GRP_CODE = CommonFuction.funFixQuotes(txtGroupcode.Text.Trim());
                    oFA_GRP_MST.GRP_NAME = CommonFuction.funFixQuotes(txtGroupname.Text.Trim());
                    string pCode = "";
                    if (ddlprntcode.SelectedIndex != 0)
                        pCode = CommonFuction.funFixQuotes(ddlprntcode.Text.Trim());
                    oFA_GRP_MST.PARENT_CODE = pCode;
                    oFA_GRP_MST.GRP_DESC = CommonFuction.funFixQuotes(txtDesc.Text.Trim());
                    oFA_GRP_MST.STATUS = chkActive.Checked;

                    bool bResult = SaitexBL.Interface.Method.FA_GRP_MST.UpdateGroupMaster(oFA_GRP_MST, out iRecordFound);

                    if (bResult)
                    {
                        Session["saveStatus"] = 1;
                        Response.Redirect("./GroupMaster.aspx?cId=U", false);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Duplicate Record.');", true);
                        blank();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Save. Provide Group Code');", true);
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private void deleteGroupMaster()
    {
        try
        {
            if (txtGroupcode.Text != "")
            {
                SaitexDM.Common.DataModel.FA_GRP_MST oFA_GRP_MST = new SaitexDM.Common.DataModel.FA_GRP_MST();
                SaitexDM.Common.DataModel.UserLoginDetail dtLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

                oFA_GRP_MST.COMP_CODE = dtLoginDetail.COMP_CODE;
                oFA_GRP_MST.TUSER = dtLoginDetail.UserCode;

                oFA_GRP_MST.GRP_CODE = CommonFuction.funFixQuotes(txtGroupcode.Text.Trim());
                oFA_GRP_MST.GRP_NAME = CommonFuction.funFixQuotes(txtGroupname.Text.Trim());
                string pCode = "";
                if (ddlprntcode.SelectedIndex != 0)
                    pCode = CommonFuction.funFixQuotes(ddlprntcode.Text.Trim());
                oFA_GRP_MST.PARENT_CODE = pCode;
                oFA_GRP_MST.GRP_DESC = CommonFuction.funFixQuotes(txtDesc.Text.Trim());
                oFA_GRP_MST.STATUS = chkActive.Checked;

                bool bResult = SaitexBL.Interface.Method.FA_GRP_MST.DeleteGroupMaster(oFA_GRP_MST);
                if (bResult)
                {
                    Session["saveStatus"] = 1;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Delete Successfully');", true);
                    Response.Redirect("./GroupMaster.aspx?cId=D", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('No such record exits.! Pls enter valid Group Code.');", true);
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private void blank()
    {
        try
        {
            txtGroupname.Text = "";
            ddlprntcode.SelectedIndex = 0;
            txtDesc.Text = "";
        }
        catch
        {
            throw;
        }
    }


    private void ddlfindfagrp()
    {
        try
        {
            ddlfind.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.FA_GRP_MST.FindEmpCode();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlfind.DataSource = dt;
                ddlfind.DataValueField = "GRP_CODE";
                ddlfind.DataTextField = "GRP_NAME";
                ddlfind.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    private void MaxGrpCode()
    {
        try
        {
            string x = "";
            int y = 0;
            DataTable dt = SaitexBL.Interface.Method.FA_GRP_MST.GetMaxGrpCode();

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
                        txtGroupcode.Text = y.ToString();
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlfind_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            ddlfindfagrp();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Find Loading..\r\nSee error log for detail."));
        }
    }

    private void findbtn()
    {
        try
        {
            tdUpdate.Visible = true;
            string strSQL = "";
            ViewState["str"] = ddlfind.SelectedValue.Trim();
            strSQL = "select * from FA_GRP_MST where Grp_Code= '" + ViewState["str"] + "' and DEL_STATUS = '0'";
            obj = new csSaitex();
            dr = obj.getDataReader(strSQL, CommandType.Text);
            if (dr.Read())
            {
                txtGroupcode.Text = dr["GRP_CODE"].ToString().Trim();
                txtGroupname.Text = dr["GRP_NAME"].ToString().Trim();
                ddlprntcode.SelectedIndex = ddlprntcode.Items.IndexOf(ddlprntcode.Items.FindByValue(dr["PARENT_CODE"].ToString().Trim()));
                txtDesc.Text = dr["GRP_DESC"].ToString().Trim();
                if (dr["STATUS"].ToString() == "1")
                    chkActive.Checked = true;
                else
                    chkActive.Checked = false;
            }
            dr.Close();
            dr.Dispose();
            dr = null;
        }
        catch (OracleException ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('" + ex.Message + "');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('" + ex.Message + "');", true);
        }
        finally
        {

        }
    }

    protected void ddlfind_SelectedIndexChanged1(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            lblMessage.Text = "";
            findbtn();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in find selecting.\r\nSee error log for detail."));
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

    private bool ValidateStatus()
    {
        try
        {
            bool IsValidStatus = false;
            DataTable dt = SaitexBL.Interface.Method.FA_GRP_MST.Fa_GetGroupMaster();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);

                dv.RowFilter = "PARENT_CODE='" + txtGroupcode.Text.Trim() + "'";
                if (dv.Count > 0)
                {
                    IsValidStatus = true;
                }
                else if (ddlprntcode.SelectedIndex == 0)
                {
                    IsValidStatus = false;
                }
                else
                {
                    IsValidStatus = false;
                }
            }
            return IsValidStatus;
        }
        catch
        {
            return false;
        }
    }

    protected void lnkbtnGroupCode_Click(object sender, EventArgs e)
    {
        try
        {
            string URL = "GroupMasterView.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=350,height=500');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Loading Group Tree PopUp..\r\nSee error log for detail."));
        }
    }
    protected void Grid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //BindData();
            Grid1.PageIndex = e.NewPageIndex;
            Grid1.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}