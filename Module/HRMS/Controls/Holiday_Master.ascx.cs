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
using Common;
using errorLog;

public partial class Module_HRMS_Controls_Holiday_Master : System.Web.UI.UserControl
{

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
        if (!IsPostBack)
        {
            txtYear.Text = System.DateTime.Now.Year.ToString();
            BindGVHoliday();
            BlanckControls();
        }
    }
    private void BlanckControls()
    {
        try
        {
            chkActive.Checked = true;
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            ddlHoliday.Visible = false;
            Grid1.AutoPostBackOnSelect = true;
            lblMode.Text = "Save";
            txtHoildayDate.Text = "";
            txtHoildayName.Text = "";
            txtYear.Text = "";
            radLeaveSelection.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (InsertHolidayMasterData())
            {
                CommonFuction.ShowMessage("Record Save Sucessfully");
            }
            else
            {
                CommonFuction.ShowMessage("Unable to Save Please try again");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (UpadateHolidayMasterData())
            {
                CommonFuction.ShowMessage("Record Update Sucessfully");
            }
            else
            {
                CommonFuction.ShowMessage("Unable to update Please try again");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
           
            SaitexDM.Common.DataModel.HolidayMaster oHM = new SaitexDM.Common.DataModel.HolidayMaster();
            oHM.HLD_ID = Convert.ToInt32(ViewState["iHolidayId"]);
            bool bResult = SaitexBL.Interface.Method.HolidayMaster.DeleteHolidayMaster(oHM);

            if (bResult)
            {
                Session["saveStatus"] = 1;
                Response.Redirect("/Saitex/Module/HRMS/Pages/Holiday.aspx?cId=D", false);
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
            ddlHoliday.Visible = true;

            Grid1.AutoPostBackOnSelect = true;
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            tdFind.Visible = false;
            lblMode.Text = "Find";
            lblMessage.Text = "";
            lblErrorMessage.Text = "";

        }

        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {

        System.Threading.Thread.Sleep(2000);
    }
    private void Load_Holiday_Record()
    {
        try
        {
        }
        catch
        {
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BindGVHoliday();
            Server.Transfer("Holiday.aspx");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string URL = "Holiday_OPT.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=600,height=300');", true);
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
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    private void BindGVHoliday()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.HolidayMaster.SelectHoliday();
            Grid1.DataSource = dt;
            Grid1.DataBind();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    private bool InsertHolidayMasterData()
    {
        bool bResult = false;

        try
        {
            SaitexDM.Common.DataModel.HolidayMaster oHM = new SaitexDM.Common.DataModel.HolidayMaster();
            int iRecordFound = 0;
            if (Page.IsValid)
            {
                if (txtYear.Text.Trim() != "" && radLeaveSelection.SelectedValue.Trim() != "" && txtHoildayName.Text.Trim() != "" && txtHoildayDate.Text.Trim() != "")
                {
                    oHM.HLD_NAME = CommonFuction.funFixQuotes(txtHoildayName.Text.Trim());
                    oHM.YEAR = CommonFuction.funFixQuotes(txtYear.Text.Trim());
                    oHM.HLD_DATE = Convert.ToDateTime(txtHoildayDate.Text.Trim());
                    oHM.OPT_LV = CommonFuction.funFixQuotes(radLeaveSelection.SelectedValue.Trim());
                    if (chkActive.Checked == true)
                    {
                        oHM.STATUS = "1";
                    }
                    else
                    {
                        oHM.STATUS = "0";
                    }
                    oHM.TUSER = Session["urLoginId"].ToString().Trim();
                    bResult = SaitexBL.Interface.Method.HolidayMaster.InsertHoliday(oHM, out iRecordFound);
                }
                else
                {
                    lblMessage.Text = "Pls enter mandatory fielde";
                }
            }
            return bResult;
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
            return false;
        }

    }
    private bool UpadateHolidayMasterData()
    {
        bool bResult = false;

        try
        {
            if (Page.IsValid)
            {

                if (txtYear.Text.Trim() != "" && radLeaveSelection.SelectedValue.Trim() != "" && txtHoildayName.Text.Trim() != "" && txtHoildayDate.Text.Trim() != "")
                {
                    int iRecordFound = 0;
                    SaitexDM.Common.DataModel.HolidayMaster oHM = new SaitexDM.Common.DataModel.HolidayMaster();
                    oHM.HLD_ID = Convert.ToInt32(ViewState["iHolidayId"]);
                    oHM.HLD_NAME = CommonFuction.funFixQuotes(txtHoildayName.Text.Trim());
                    oHM.YEAR = CommonFuction.funFixQuotes(txtYear.Text.Trim());
                    oHM.HLD_DATE = Convert.ToDateTime(txtHoildayDate.Text.Trim());
                    oHM.OPT_LV = CommonFuction.funFixQuotes(radLeaveSelection.SelectedValue.Trim());
                    if (chkActive.Checked == true)
                    {
                        oHM.STATUS = "1";
                    }
                    else
                    {
                        oHM.STATUS = "0";
                    }
                    oHM.TUSER = Session["urLoginId"].ToString().Trim();
                    bResult = SaitexBL.Interface.Method.HolidayMaster.UpdateHolidayMaster(oHM, out iRecordFound);

                }
                else
                {
                    lblMessage.Text = "Pls enter mandatory fielde";
                }
            }
            return bResult;
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
            return false;
        }
    }

    private void getHolidayMasterData(int iHolidayMasterId)
    {
        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.HolidayMaster.GetHolidayMaster(iHolidayMasterId);
            txtYear.Text = dt.Rows[0]["YEAR"].ToString().Trim();
            txtHoildayName.Text = dt.Rows[0]["HLD_NAME"].ToString().Trim();
            txtHoildayDate.Text = dt.Rows[0]["HLD_DATE"].ToString().Trim();
            radLeaveSelection.SelectedValue = dt.Rows[0]["OPT_LV"].ToString().Trim();
            if (dt.Rows[0]["STATUS"].ToString().Trim() == "1")
            {
                chkActive.Checked = true;
            }
            else
            {
                chkActive.Checked = false;
            }
            ddlHoliday.Visible = false;
            tblMainTable.Visible = true;
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void Grid1_Select(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            ArrayList ar = Grid1.SelectedRecords;
            lblMode.Text = "Find";
            tdDelete.Visible = true;
            tdClear.Visible = true;
            tdUpdate.Visible = true;
            tdSave.Visible = false;
            Hashtable ht = (Hashtable)ar[0];
            ViewState["HLD_ID"] = ht["HLD_ID"].ToString().Trim();
            txtHoildayName.Text = ht["HLD_NAME"].ToString().Trim();
            txtYear.Text = ht["YEAR"].ToString().Trim();
            txtHoildayDate.Text = ht["HLD_DATE"].ToString().Trim();
            radLeaveSelection.SelectedValue = ht["OPT_LV"].ToString().Trim();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void ddlHoliday_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            getHolidayMasterData(Convert.ToInt32(ddlHoliday.SelectedValue.Trim()));
            ViewState["iHolidayId"] = ddlHoliday.SelectedValue.Trim();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
