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

public partial class Module_FA_Controls_GroupInt : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static DataTable dtGroupInt;

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
                Session["saveStatus"] = 0;
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
            lblMode.Text = "Save";
            lblErrorMessage.Text = "";
            lblMessage.Text = "";
            BindCompanyDropdown();
        }
        catch
        {
            throw;
        }
    }

    private void BindCompanyDropdown()
    {
        try
        {
            ddlCompany.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.CM_COMP_MST.GetCompanyMaster();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlCompany.DataSource = dt;
                ddlCompany.DataValueField = "COMP_CODE";
                ddlCompany.DataTextField = "COMP_NAME";
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, new ListItem("----------- Select Company -----------", "0"));
            }
        }
        catch
        {
            throw;
        }
    }

    private void FillGroupCheckBoxList(string strCompCode)
    {
        try
        {
            string strGRP_Code = string.Empty;
            DataTable dt = SaitexBL.Interface.Method.FA_GRP_MST.GetGroupMasterForIntegrationFromMaster();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        strCompCode = "";
                        strGRP_Code = "";

                        strCompCode = ddlCompany.SelectedValue.Trim();
                        strGRP_Code = dv[iLoop]["GRP_CODE"].ToString().Trim();

                        if (CheckIntegration(strCompCode, strGRP_Code))
                        {
                            chkListGroup.Items.Add(new ListItem(dv[iLoop]["GRP_NAME"].ToString().Trim(), dv[iLoop]["GRP_CODE"].ToString().Trim()));
                            chkListGroup.Items[iLoop].Selected = true;
                        }
                        else
                        {
                            chkListGroup.Items.Add(new ListItem(dv[iLoop]["GRP_NAME"].ToString().Trim(), dv[iLoop]["GRP_CODE"].ToString().Trim()));
                            chkListGroup.Items[iLoop].Selected = false;
                        }
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlCompany_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            BindCompanyDropdown();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Company Name..\r\nSee error log for detail."));
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (dtGroupInt == null)
                CreateDataTable();

            if (dtGroupInt.Rows.Count > 0)
            {
                dtGroupInt.Rows.Clear();
            }

            if (chkListGroup.Items.Count > 0)
            {
                for (int iLoop = 0; iLoop < chkListGroup.Items.Count; iLoop++)
                {
                    if (chkListGroup.Items[iLoop].Selected == true)
                    {
                        DataRow dr = dtGroupInt.NewRow();
                        dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                        dr["GRP_CODE"] = chkListGroup.Items[iLoop].Value.ToString();
                        dr["STATUS"] = "1";
                        dr["DEL_STATUS"] = "0";
                        dr["TDATE"] = System.DateTime.Now;
                        dr["TUSER"] = oUserLoginDetail.UserCode;

                        dtGroupInt.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = dtGroupInt.NewRow();
                        dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                        dr["GRP_CODE"] = chkListGroup.Items[iLoop].Value.ToString();
                        dr["STATUS"] = "0";
                        dr["DEL_STATUS"] = "1";
                        dr["TDATE"] = System.DateTime.Now;
                        dr["TUSER"] = oUserLoginDetail.UserCode;

                        dtGroupInt.Rows.Add(dr);
                    }
                }
                bool bResult = SaitexBL.Interface.Method.FA_GRP_MST.Insert_GroupInt(dtGroupInt);

                if (bResult)
                {
                    Session["saveStatus"] = 1;
                    Response.Redirect("./GroupIntegration.aspx?cId=S", false);
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Error.. in saving..");
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating the data.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("/Saitex/Module/FA/Pages/GroupIntegration.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in clearing the data.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }

    private bool CheckIntegration(string strCompCode, string strGRP_Code)
    {
        try
        {
            bool IsIntegration = false;
            DataTable dt = SaitexBL.Interface.Method.FA_GRP_MST.GetGroupMasterForIntegration(ddlCompany.SelectedValue.Trim());
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "GRP_CODE='" + strGRP_Code + "'";
                if (dv.Count > 0)
                {
                    IsIntegration = true;
                }
                else
                {
                    IsIntegration = false;
                }
            }
            return IsIntegration;
        }
        catch
        {
            throw;
        }
    }

    private void CreateDataTable()
    {
        try
        {
            dtGroupInt = new DataTable();
            dtGroupInt.Columns.Add("COMP_CODE", typeof(string));
            dtGroupInt.Columns.Add("GRP_CODE", typeof(string));
            dtGroupInt.Columns.Add("STATUS", typeof(string));
            dtGroupInt.Columns.Add("DEL_STATUS", typeof(string));
            dtGroupInt.Columns.Add("TDATE", typeof(DateTime));
            dtGroupInt.Columns.Add("TUSER", typeof(string));
        }
        catch
        {
            throw;
        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string strCompCode = string.Empty;
            chkListGroup.Items.Clear();
            strCompCode = ddlCompany.SelectedValue.Trim();
            FillGroupCheckBoxList(strCompCode);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Company Name..\r\nSee error log for detail."));
        }
    }
}