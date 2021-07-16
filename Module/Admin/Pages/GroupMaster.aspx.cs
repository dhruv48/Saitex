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
public partial class Admin_GroupMaster : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = new SaitexDM.Common.DataModel.UserLoginDetail();
    string user = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            user = oUserLoginDetail.Username;

            lblMode.Text = "Save";
            if (!IsPostBack)
            {
                tdUpdate.Visible = false;
                tdDelete.Visible = false;

                txtGroupName.Visible = true;
                BlanksControls();
                chk_Status.Checked = true;
                bindGvGroupMaster();
                ddlfind.Visible = false;
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
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
        }

    }
    private void BlanksControls()
    {
        try
        {
            ddlfind.Visible = false;

            txtGroupCode.Visible = true;
            txtGroupCode.Text = "";
            txtGroupName.Text = "";
            txtRemarks.Text = "";
            chk_Status.Checked = false;

        }
        catch
        {
            throw;
        }
    }
    private void GetFindData(string sGroupCode)
    {
        try
        {
            DataTable dt = (DataTable)ViewState["dtGroup"];

            DataView dv = new DataView(dt);
            dv.RowFilter = "GRP_CODE='" + CommonFuction.funFixQuotes(sGroupCode) + "'";

            if (dv.Count > 0)
            {
                txtGroupName.Text = dv[0]["GRP_NAME"].ToString();
                txtRemarks.Text = dv[0]["GRP_DESC"].ToString();
                txtGroupCode.Text = dv[0]["GRP_CODE"].ToString();

                if (dv[0]["STATUS"].ToString() == "1")
                    chk_Status.Checked = true;
                else
                    chk_Status.Checked = false;

            }

        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            gvGrpMaster.AutoPostBackOnSelect = true;
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            tdFind.Visible = false;
            lblMode.Text = "Update";
            ddlfind.Visible = true;
            txtGroupCode.Visible = false;
            imgbtnClear.Visible = true;
            txtGroupCode.ReadOnly = true;

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in edit mode.\r\nSee error log for detail."));

        }
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (txtGroupCode.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Pls Select find item.');", true);
                }
                else
                {
                    UpdateData();
                }
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in updating group.\r\nSee error log for detail."));
        }
    }
    private void UpdateData()
    {
        try
        {
            int iRecordFound = 0;

            bool Result = SaitexBL.Interface.Method.CM_GroupMaster.Update(txtGroupName.Text.ToUpper().Trim(), txtGroupCode.Text.ToUpper().Trim(), txtRemarks.Text, chk_Status.Checked, user, out iRecordFound);
            if (Result)
            {

                bindGvGroupMaster();

                lblMode.Text = "Save";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "fun", "window.confirm('Group Update Successfully.');", true);
                BlanksControls();
                gvGrpMaster.AutoPostBackOnSelect = false;
                tdUpdate.Visible = false;
                tdDelete.Visible = false;
                tdSave.Visible = true;
                tdFind.Visible = true;
                txtGroupCode.Enabled = true;

            }
            else if (iRecordFound > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "fun", "window.confirm('data already exists with provided values');", true);

                tdUpdate.Visible = false;
                tdDelete.Visible = false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "fun", "window.confirm('Updating Failed');", true);
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
            if (txtGroupCode.Text != "")
            {
                deletedata();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('First search Record to delete');", true);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Group Deletion.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("/Saitex/Module/Admin/Pages/groupmaster.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page reloading.\r\nSee error log for detail."));
        }
    }
    private void deletedata()
    {
        try
        {
            int iRecordfound = 0;
            bool Result = SaitexBL.Interface.Method.CM_GroupMaster.Delete(txtGroupCode.Text, out iRecordfound);
            if (Result)
            {
                tdUpdate.Visible = false;
                tdDelete.Visible = false;
                tdSave.Visible = true;
                tdFind.Visible = true;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "fun", "window.confirm('Data Deleted Successfully.');", true);
                ddlfind.Visible = false;
                gvGrpMaster.AutoPostBackOnSelect = true;
                gvGrpMaster.Enabled = false;
                lblMode.Text = "Save";
                bindGvGroupMaster();
                BlanksControls();

            }
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
            string URL = "../Reports/Grp_mst_rpt.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting group print.\r\nSee error log for detail."));

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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in leaving page.\r\nSee error log for detail."));

        }
    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            HelpData();

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting help.\r\nSee error log for detail."));

        }
    }
    private void PrintData()
    {
        try
        {
        }
        catch
        {
            throw;
        }

    }
    private void HelpData()
    {
        try
        {
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
            DataTable dt = SaitexBL.Interface.Method.CM_GroupMaster.Select();
            ViewState["dtGroup"] = dt;
            gvGrpMaster.DataSource = dt;
            gvGrpMaster.DataBind();

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
            if (Page.IsValid)
            {
                SaveGroup();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in saving page.\r\nSee error log for detail."));

        }
    }
    protected void SaveGroup()
    {
        try
        {

            int iRecordFound = 0;
            bool Result = SaitexBL.Interface.Method.CM_GroupMaster.Insert(txtGroupName.Text.ToUpper().Trim(), txtGroupCode.Text.ToUpper().Trim(), txtRemarks.Text, chk_Status.Checked, user, out iRecordFound);

            if (Result)
            {
                BlanksControls();
                bindGvGroupMaster();

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "fun", "window.confirm('Saved Successfully. ');", true);
            }
            else if (iRecordFound > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "fun", "window.confirm('data already exists ');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "fun", "window.confirm('Saving Failed');", true);
            }

        }
        catch
        {
            throw;
        }
    }
    private void findGroupName()
    {
        try
        {
            ddlfind.Items.Clear();


            DataTable dt = SaitexBL.Interface.Method.CM_GroupMaster.Select();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlfind.DataValueField = "GRP_CODE";
                ddlfind.DataTextField = "GRP_NAME";

                ddlfind.DataSource = dt;
                ddlfind.DataBind();

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
            findGroupName();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting group for updation.\r\nSee error log for detail."));

        }
    }
    protected void ddlfind_SelectedIndexChanged1(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            string strGRPCode = "";
            strGRPCode = ddlfind.SelectedValue.ToString();
            GetFindData(strGRPCode);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting group for updation.\r\nSee error log for detail."));
        }
    }
    protected void gvGrpMaster_Select(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            tdFind.Visible = false;
            txtGroupCode.Enabled = false;
            txtGroupCode.Visible = true;
            lblMode.Text = "Update";
            gvGrpMaster.AutoPostBackOnSelect = true;
            ddlfind.Visible = false;
            ArrayList ar = gvGrpMaster.SelectedRecords;
            Hashtable ht = (Hashtable)ar[0];
            txtGroupCode.Text = ht["GRP_CODE"].ToString().Trim();
            txtGroupName.Text = ht["GRP_NAME"].ToString().Trim();
            txtRemarks.Text = ht["GRP_DESC"].ToString().Trim();

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting group for updation.\r\nSee error log for detail."));

        }
    }
}
