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
public partial class Module_Production_Queries_ProductionEntry4YS : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static string PROS_CODE = string.Empty;
    private static string ORDER_NO = string.Empty;
    private static string MACHINE_CODE = string.Empty;
    private static string DEPT_CODE = string.Empty;
    private static string LOT_NUMBER = string.Empty;
    private static string DYED_LOT_NO = string.Empty;
    private static string SFT_ID = string.Empty;
    private static string FromDate;
    private static string ToDate;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                lblMode.Text = "You are in Print Mode";
                txtformdate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
                txtTodate.Text = System.DateTime.Now.ToShortDateString();
                tdPI_Type.Visible = false;
                bindddlshiftid();
                GridProductEntrys();
                bindddlProcessno();
                bindddlOrderNO();
                bindddlMachinCode();
                BindddlDyedLotNo();
                bindddlDeptCode();
                bindddlLotNo();
                ddlPAType();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Page Load.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }  
    }
    private void BindddlDyedLotNo()
    {
        try
        {
            ddldyedlotno.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetDyedlotno();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddldyedlotno.DataTextField = "DYED_LOT_NO";
                ddldyedlotno.DataValueField = "DYED_LOT_NO";
                ddldyedlotno.DataSource = dt;
                ddldyedlotno.DataBind();
            }
            ddldyedlotno.Items.Insert(0, new ListItem("Select", ""));
        }
        catch
        {
            throw;
        }
    }
    private void GridProductEntrys()
    {
        try
        {
            if (ddlprocessno.SelectedValue.ToString() != null && ddlprocessno.SelectedValue.ToString() != string.Empty)
            {
                PROS_CODE = ddlprocessno.SelectedValue.ToString();
            }
            else
            {
                PROS_CODE = string.Empty;
            }

            if (ddlOrderNo.SelectedValue.ToString() != null && ddlOrderNo.SelectedValue.ToString() != string.Empty)
            {
                ORDER_NO = ddlOrderNo.SelectedValue.ToString();
            }
            else
            {
                ORDER_NO = string.Empty;
            }

            if (ddlMachin.SelectedValue.ToString() != null && ddlMachin.SelectedValue.ToString() != string.Empty)
            {
                MACHINE_CODE = ddlMachin.SelectedValue.ToString();
            }
            else
            {
                MACHINE_CODE = string.Empty;
            }

            if (ddldept.SelectedValue.ToString() != null && ddldept.SelectedValue.ToString() != string.Empty)
            {
                DEPT_CODE = ddldept.SelectedValue.ToString();
            }
            else
            {
                DEPT_CODE = string.Empty;
            }

            if (ddllotno.SelectedValue.ToString() != null && ddllotno.SelectedValue.ToString() != string.Empty)
            {
                LOT_NUMBER = ddllotno.SelectedValue.ToString();
            }
            else
            {
                LOT_NUMBER = string.Empty;
            }

            if (ddldyedlotno.SelectedValue.ToString() != null && ddldyedlotno.SelectedValue.ToString() != string.Empty)
            {
                DYED_LOT_NO = ddldyedlotno.SelectedValue.ToString();
            }
            else
            {
                DYED_LOT_NO = string.Empty;
            }

            if (ddlshift.SelectedValue.ToString() != null && ddlshift.SelectedValue.ToString() != string.Empty)
            {
                SFT_ID = ddlshift.SelectedValue.ToString();
            }
            else
            {
                SFT_ID = string.Empty;
            }

            if (txtformdate.Text.ToString() != null && txtformdate.Text.ToString() != string.Empty)
            {
                FromDate = txtformdate.Text.ToString();
            }
            else
            {
                FromDate = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
            }

            if (txtTodate.Text.ToString() != null && txtTodate.Text.ToString() != string.Empty)
            {
                ToDate = txtTodate.Text.ToString();
            }
            else
            {
                ToDate = System.DateTime.Now.Date.ToShortDateString();
            }

            DataTable DT = SaitexBL.Interface.Method.YRN_PROD_MST.GetProductEntry(MACHINE_CODE, ORDER_NO, PROS_CODE, DEPT_CODE, LOT_NUMBER, DYED_LOT_NO, SFT_ID, FromDate, ToDate);
            if (DT != null && DT.Rows.Count > 0)
            {
                GridProductEntry.DataSource = DT;
                GridProductEntry.DataBind();
                lblTotalRecord.Text = DT.Rows.Count.ToString().Trim();
            }
            else
            {
                GridProductEntry.DataSource = null;
                GridProductEntry.DataBind();
                lblTotalRecord.Text = DT.Rows.Count.ToString().Trim();
                CommonFuction.ShowMessage("Data not Found by selected Item .");
            }
        }
        catch
        {
            throw;
        }
    }
    private void ddlPAType()
    {
        try
        {
            ddlPItype.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetPItype();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlPItype.DataTextField = "PI_TYPE";
                ddlPItype.DataValueField = "PI_TYPE";
                ddlPItype.DataSource = dt;
                ddlPItype.DataBind();

            }
            ddlPItype.Items.Insert(0, new ListItem("Select", ""));
        }
        catch
        {
            throw;
        }
    }

    private void bindddlOrderNO()
    {
        try
        {
           
                ddlOrderNo.Items.Clear();
                DataTable dt = SaitexBL.Interface.Method.TX_MAC_PROC_MST.Get_OrderNoYS();
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddlOrderNo.DataValueField = "ORDER_NO";
                    ddlOrderNo.DataTextField = "ORDER_NO";
                    ddlOrderNo.DataSource = dt;
                    ddlOrderNo.DataBind();
                }
                ddlOrderNo.Items.Insert(0, new ListItem("Select", ""));
 
          
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void bindddlLotNo()
    {
        try
        {
            ddllotno.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetLotNumber();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddllotno.DataValueField = "LOT_NUMBER";
                ddllotno.DataTextField = "LOT_NUMBER";
                ddllotno.DataSource = dt;
                ddllotno.DataBind();
            }
            ddllotno.Items.Insert(0, new ListItem("Select", ""));
        }
        catch
        {
            throw;
        }
    }
    private void bindddlshiftid()
    {
        try
        {
            ddlshift.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.HR_SFT_MST.SelectShiftName();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlshift.DataValueField = "SFT_ID";
                ddlshift.DataTextField = "SFT_NAME";
                ddlshift.DataSource = dt;
                ddlshift.DataBind();
            }
            ddlshift.Items.Insert(0, new ListItem("Select", ""));
        }
        catch
        {
            throw;
        }
    }
    private void bindddlProcessno()
    {
        try
        {
            ddlprocessno.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.TX_MAC_PROC_MST.Get_PROCODE();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlprocessno.DataValueField = "PROS_CODE";
                ddlprocessno.DataTextField = "PROS_DESC";
                ddlprocessno.DataSource = dt;
                ddlprocessno.DataBind();
            }
            ddlprocessno.Items.Insert(0, new ListItem("Select", ""));
        }
        catch
        {
            throw;
        }
    }
    private void bindddlMachinCode()
    {
        try
        {
            ddlMachin.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.MC_MACHINE_MASTER.SelectMachineCode();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlMachin.DataValueField = "MACHINE_CODE";
                ddlMachin.DataTextField = "MACHINE_CODE";
                ddlMachin.DataSource = dt;
                ddlMachin.DataBind();
            }
            ddlMachin.Items.Insert(0, new ListItem("Select", ""));
        }
        catch
        {
            throw;
        }

    }
    private void bindddlDeptCode()
    {
        try
        {
            ddldept.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddldept.DataTextField = "DEPT_NAME";
                ddldept.DataValueField = "DEPT_CODE";
                ddldept.DataSource = dt;
                ddldept.DataBind();

            }
            ddldept.Items.Insert(0, new ListItem("Select", ""));
        }
        catch
        {
            throw;
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {

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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void ddlOrderNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridProductEntrys();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Shift Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void ddlprocessno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridProductEntrys();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Shift Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void ddlMachin_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridProductEntrys();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Shift Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridProductEntrys();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Shift Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void ddllotno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridProductEntrys();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Shift Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void ddldyedlotno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridProductEntrys();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Shift Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void ddlshift_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridProductEntrys();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Shift Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void txtformdate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GridProductEntrys();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in From Date Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void txtTodate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtformdate.Text == null || txtformdate.Text == string.Empty)
            {
                CommonFuction.ShowMessage("Please enter From Date first..");
            }
            else
            {
                if (DateTime.Parse(txtformdate.Text) > DateTime.Parse(txtTodate.Text))
                {
                    CommonFuction.ShowMessage("Please From Date should not be greater than To Date..");
                }
                else
                {

                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in To Date TextBox.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void GridProductEntry_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridProductEntry.PageIndex = e.NewPageIndex;
            GridProductEntrys();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in GridView PageIndex Changed Event..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
}
