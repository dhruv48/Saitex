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
using System.IO;
using DBLibrary;

public partial class Module_HRMS_Controls_PositionMaster : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnSave.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save this record ? ')");
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");
        imgbtnDelete.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit')");
        imgbtnHelp.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Help')");
    }

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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
        }
    }
    private void InitialisePage()
    {
        try
        {
            txtPositionCode.Text = string.Empty;
            txtPositionName.Text = string.Empty;
            txtRemarks.Text = "";
            ddlSRPosition.SelectedIndex = -1;

            ddlPosition.SelectedIndex = -1;
            ddlPosition.SelectedText = string.Empty;
            ddlPosition.SelectedValue = string.Empty;
            ddlPosition.Items.Clear();

            ddlPosition.Visible = false;
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            tdFind.Visible = true;
            tdSave.Visible=true;

            lblMode.Text = "Save";
            bindSRDDLPosition();
        }
        catch
        {
            throw;
        }
    }
    private void bindSRDDLPosition()
    {
        DataTable dt = SaitexBL.Interface.Method.HR_POSITION_MST.Select();
        ddlSRPosition.DataTextField = "Position_NAME";
        ddlSRPosition.DataValueField = "Position_CODE";
        ddlSRPosition.DataSource = dt;
        ddlSRPosition.DataBind();
        ddlSRPosition.Items.Insert(0, new ListItem("----------Select---------", ""));

    }

    private void InsertData()
    {
        try
        {
            if (txtPositionCode.Text != "")
            {
                int iRecordFound = 0;
                SaitexDM.Common.DataModel.HR_POSITION_MST oHR_POSITION_MST = new SaitexDM.Common.DataModel.HR_POSITION_MST();
                oHR_POSITION_MST.POSITION_CODE = CommonFuction.funFixQuotes(txtPositionCode.Text.Trim()).ToUpper();
                oHR_POSITION_MST.POSITION_NAME = CommonFuction.funFixQuotes(txtPositionName.Text.Trim()).ToUpper();
                oHR_POSITION_MST.POSITION_REMARKS = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
                oHR_POSITION_MST.SR_POSITION_CODE = CommonFuction.funFixQuotes(ddlSRPosition.SelectedValue.Trim());
                oHR_POSITION_MST.TUSER = oUserLoginDetail.UserCode;
                bool bResult = SaitexBL.Interface.Method.HR_POSITION_MST.Insert_HR_POSITION_MST(oHR_POSITION_MST, out iRecordFound);
                if (bResult)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Saved Successfully');", true);
                    InitialisePage();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Please Enter anather Record');", true);
            }
        }
        catch (Exception oEx)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", "window.alert('" + oEx.Message + "');", true);

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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving Data.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InitialisePage();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in refreshing the page.\r\nSee error log for detail."));
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
                Response.Redirect("~/GetUserAuthorisation.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ddlPosition.Visible = true;
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            tdFind.Visible = true;
            lblMode.Text = "Find";
        }

        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        UpadatePositionMasterData();
    }
    private void UpadatePositionMasterData()
    {
        if (Page.IsValid)
        {

            try
            {
                if (txtPositionName.Text.Trim() != "" && ddlPosition.SelectedValue.Trim() != "" && txtRemarks.Text.Trim() != "")
                {

                   int iRecordFound = 0;
                    SaitexDM.Common.DataModel.HR_POSITION_MST oHR_POSITION_MST = new SaitexDM.Common.DataModel.HR_POSITION_MST();
                    oHR_POSITION_MST.POSITION_CODE = ViewState["iPosition_CODE"].ToString().ToUpper();

                    oHR_POSITION_MST.POSITION_NAME = CommonFuction.funFixQuotes(txtPositionName.Text.Trim()).ToUpper();
                    oHR_POSITION_MST.POSITION_REMARKS = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
                    oHR_POSITION_MST.SR_POSITION_CODE = CommonFuction.funFixQuotes(ddlSRPosition.SelectedValue.Trim());
                    oHR_POSITION_MST.TUSER = oUserLoginDetail.UserCode;

                    bool bResult = SaitexBL.Interface.Method.HR_POSITION_MST.UpdatePOSITIONnMaster(oHR_POSITION_MST, out iRecordFound);

                    if (bResult)
                    {
                        InitialisePage();
                        CommonFuction.ShowMessage("Record Updated Successfully");
                    }
                    else if (iRecordFound > 0)
                    {
                        Common.CommonFuction.ShowMessage("This record is already saved Please save another Record");
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Pls enter mandatory fielde");
                }
            }
            catch (Exception ex)
            {
                Common.CommonFuction.ShowMessage(ex.Message.ToString());
                ErrHandler.WriteError(ex.Message);
            }

            finally
            {

            }
        }
    }
    protected void ddlPosition_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        // Getting the items
        DataTable data = GetItems(e.Text.ToUpper().ToString().Trim(), Convert.ToInt32(e.ItemsOffset), 10);
        ddlPosition.DataSource = data;
        ddlPosition.DataBind();
        // Calculating the numbr of items loaded so far in the ComboBox
        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
        // Getting the total number of items that start with the typed text
        e.ItemsCount = data.Rows.Count;

    }
    protected DataTable GetItems(string strPositionName, int startOffset, int numberOfItems)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.HR_POSITION_MST.GetItems(strPositionName);
            return dt;
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
            throw ex;
        }
    }

    private void getPositionMasterData(string iPositionMasterCode)
    {
        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.HR_POSITION_MST.GetPOSITIONMaster(iPositionMasterCode);
            txtPositionCode.Text = dt.Rows[0]["Position_CODE"].ToString().Trim();
            txtPositionName.Text = dt.Rows[0]["Position_NAME"].ToString().Trim();
            txtRemarks.Text = dt.Rows[0]["Position_REMARKS"].ToString().Trim();
            ddlSRPosition.SelectedValue = dt.Rows[0]["SR_Position_CODE"].ToString().Trim();

            ddlPosition.Visible = false;
            tblDesgMainTable.Visible = true;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
            ErrHandler.WriteError(ex.Message);
        }

    }
    protected void ddlPosition_SelectedIndexChanged1(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        getPositionMasterData(Convert.ToString(ddlPosition.SelectedValue.Trim()));
        ViewState["iPosition_CODE"] = ddlPosition.SelectedValue.Trim().ToUpper();
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        //string URL = "../Reports/Position_MASTER.aspx";
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);

    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            SaitexDM.Common.DataModel.HR_POSITION_MST oHR_POSITION_MST = new SaitexDM.Common.DataModel.HR_POSITION_MST();
            oHR_POSITION_MST.POSITION_CODE = ViewState["iPosition_CODE"].ToString().ToUpper();
            bool bResult = SaitexBL.Interface.Method.HR_POSITION_MST.DeletePOSITIONMaster(oHR_POSITION_MST);
            if (bResult)
            {
                InitialisePage();
                CommonFuction.ShowMessage("Record Deleted Successfully");
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);

        }
    }
}
