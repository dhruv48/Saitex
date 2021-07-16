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

public partial class Module_Production_Pages_MachineStop : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    int PROD_PROS_ID_NO = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (Request.QueryString["PROD_PROS_ID_NO"] != null)
            {
                int.TryParse(Request.QueryString["PROD_PROS_ID_NO"].ToString(), out PROD_PROS_ID_NO);
            }
            if (!IsPostBack)
            {
                InitialiseData();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Machine Stopage Data.\r\nSee error log for detail."));
        }
    }

    private void GetQuerystringValues()
    {
        try
        {
            if (Request.QueryString["MAC_CODE"] != null && Request.QueryString["MAC_CODE"].ToString() != string.Empty)
            {
                txtMachineCode.Text = Request.QueryString["MAC_CODE"].ToString();
            }
            if (Request.QueryString["PROS_CODE"] != null && Request.QueryString["PROS_CODE"].ToString() != string.Empty)
            {
                hfPROS_CODE.Value = Request.QueryString["PROS_CODE"].ToString();
            }
            if (Request.QueryString["MAC_LOAD_TIME"] != null && Request.QueryString["MAC_LOAD_TIME"].ToString() != string.Empty)
            {
                txtMachineLoadTime.Text = Request.QueryString["MAC_LOAD_TIME"].ToString();
                lblValidateStartTime.Text = txtMachineLoadTime.Text;
            }
            if (Request.QueryString["MAC_UNLOAD_TIME"] != null && Request.QueryString["MAC_UNLOAD_TIME"].ToString() != string.Empty)
            {
                txtMachineUnLoadTime.Text = Request.QueryString["MAC_UNLOAD_TIME"].ToString();
                lblValidateEndTime.Text = txtMachineUnLoadTime.Text;
            }
            if (Request.QueryString["TextBoxId"] != null)
            {
                hfTextBoxId.Value = Request.QueryString["TextBoxId"].ToString();
            }
            
        }
        catch
        {
            throw;
        }
    }

    private void InitialiseData()
    {
        try
        {
            ClearPage();
            GetQuerystringValues();
            BindMachineStopReason();
            BindMACSTOPDetailGrid();
            GetvalidateTime();
        }
        catch
        {
            throw;
        }
    }

    private void ClearPage()
    {
        try
        {
            txtMachineCode.Text = string.Empty;
            txtMachineLoadTime.Text = string.Empty;
            txtMachineUnLoadTime.Text = string.Empty;
            txtMacStopBeginTime.Text = string.Empty;
            txtMacStopDuration.Text = string.Empty;
            txtMacStopEndTime.Text = string.Empty;
            ddlMacStopReason.SelectedIndex = -1;
        }
        catch
        {
            throw;
        }
    }

    private void BindMachineStopReason()
    {
        try
        {
            ddlMacStopReason.Items.Clear();
            ddlMacStopReason.Items.Add(new ListItem("Select", "Select"));

            DataTable dtMacreason = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("MAC_STOP_REASON", oUserLoginDetail.COMP_CODE);

            ddlMacStopReason.DataSource = dtMacreason;
            ddlMacStopReason.DataTextField = "MST_DESC";
            ddlMacStopReason.DataValueField = "MST_CODE";
            ddlMacStopReason.DataBind();
        }
        catch
        {
            throw;
        }
    }

    protected void btnMacStopSave_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = CalcProcessTime();

            if (Session["dtMachineStopDataTable"] == null)
            {
                CreateMachineStopageTable();
            }

            DataTable dtMachineStopDataTable = (DataTable)Session["dtMachineStopDataTable"];

            if (msg.Equals(string.Empty))
            {
                DataRow dr = dtMachineStopDataTable.NewRow();

                dr["UNIQUE_ID"] = dtMachineStopDataTable.Rows.Count + 1;
                dr["PROS_CODE"] = hfPROS_CODE.Value;
                dr["MAC_CODE"] = txtMachineCode.Text;
                dr["STOP_TIME_BEGIN"] = DateTime.Parse(txtMacStopBeginTime.Text);
                dr["STOP_TIME_END"] = DateTime.Parse(txtMacStopEndTime.Text);
                dr["STOP_TIME_DURATION"] = int.Parse(txtMacStopDuration.Text);
                dr["STOP_REASON"] = ddlMacStopReason.SelectedValue.Trim();
                dr["PROD_PROS_ID_NO"] = PROD_PROS_ID_NO;
                dtMachineStopDataTable.Rows.Add(dr);

                Session["dtMachineStopDataTable"] = dtMachineStopDataTable;
            }
            GetvalidateTime();
            BindMACSTOPDetailGrid();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in machine stopage time saving.\r\nCheck your data."));
        }
    }

    private void CreateMachineStopageTable()
    {
        try
        {
            DataTable dtMachineStopDataTable = new DataTable();
            dtMachineStopDataTable.Columns.Add("UNIQUE_ID", typeof(int));
            dtMachineStopDataTable.Columns.Add("MAC_CODE", typeof(string));
            dtMachineStopDataTable.Columns.Add("PROS_CODE", typeof(string));
            dtMachineStopDataTable.Columns.Add("TRN_TYPE", typeof(string));
            dtMachineStopDataTable.Columns.Add("STOP_TIME_BEGIN", typeof(DateTime));
            dtMachineStopDataTable.Columns.Add("STOP_TIME_END", typeof(DateTime));
            dtMachineStopDataTable.Columns.Add("STOP_TIME_DURATION", typeof(int));
            dtMachineStopDataTable.Columns.Add("STOP_REASON", typeof(string));
            dtMachineStopDataTable.Columns.Add("PROD_PROS_ID_NO", typeof(int));
            Session["dtMachineStopDataTable"] = dtMachineStopDataTable;
        }
        catch
        {
            throw;
        }
    }

    private void GetvalidateTime()
    {
        try
        {

            if (Session["dtMachineStopDataTable"] == null)
            {
                CreateMachineStopageTable();
            }

            int TotalMachineStop = 0;
            DataTable dtMachineStopDataTable = (DataTable)Session["dtMachineStopDataTable"];

            DateTime dtvalidStartTime = DateTime.Parse(txtMachineLoadTime.Text);
            DateTime dtvalidEndTime = DateTime.Parse(txtMachineUnLoadTime.Text);

            if (dtMachineStopDataTable != null && dtMachineStopDataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dtMachineStopDataTable.Rows)
                {
                    DateTime dtendDate = DateTime.Parse(dr["STOP_TIME_END"].ToString());
                    int MacStopage = int.Parse(dr["STOP_TIME_DURATION"].ToString());
                    TotalMachineStop += MacStopage;

                    if (dtendDate > dtvalidStartTime)
                    {
                        dtvalidStartTime = dtendDate;
                    }
                }
            }
            lblValidateStartTime.Text = dtvalidStartTime.ToString();
            lblValidateEndTime.Text = dtvalidEndTime.ToString();
            txtTotalMachineStop.Text = TotalMachineStop.ToString();

        }
        catch
        {
            throw;
        }
    }

    private void BindMACSTOPDetailGrid()
    {
        try
        {
            if (Session["dtMachineStopDataTable"] == null)
            {
                CreateMachineStopageTable();
            }

            DataTable dtMachineStopDataTable = (DataTable)Session["dtMachineStopDataTable"];

            DataView dvMacstop = new DataView(dtMachineStopDataTable);

            dvMacstop.RowFilter = "MAC_CODE='" + txtMachineCode.Text + "' and PROS_CODE='" + hfPROS_CODE.Value.Trim() + "' ";

            if (dvMacstop.Count > 0)
            {
                grdMachineStopage.DataSource = dvMacstop;
                grdMachineStopage.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    protected void btnMacStopCancel_Click(object sender, EventArgs e)
    {
        txtMacStopBeginTime.Text = string.Empty;
        txtMacStopDuration.Text = string.Empty;
        txtMacStopEndTime.Text = string.Empty;
        ddlMacStopReason.SelectedIndex = -1;
    }

    protected void grdMachineStopage_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UNIQUE_ID = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "DelRow")
            {
                DeleteStopDetailRow(UNIQUE_ID);
                BindMACSTOPDetailGrid();
                GetvalidateTime();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in machine stopage begin time.\r\nCheck your data."));
        }
    }

    private void DeleteStopDetailRow(int UNIQUE_ID)
    {
        try
        {
            if (Session["dtMachineStopDataTable"] == null)
            {
                CreateMachineStopageTable();
            }

            DataTable dtMachineStopDataTable = (DataTable)Session["dtMachineStopDataTable"];

            if (dtMachineStopDataTable.Rows.Count == 1)
            {
                dtMachineStopDataTable.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtMachineStopDataTable.Rows)
                {
                    int iUNIQUE_ID = int.Parse(dr["UNIQUE_ID"].ToString());
                    if (UNIQUE_ID == iUNIQUE_ID)
                    {
                        dtMachineStopDataTable.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtMachineStopDataTable.Rows)
                {
                    iCount = iCount + 1;
                    dr["UNIQUE_ID"] = iCount;
                }
            }
        }
        catch
        {
            throw;
        }
    }

    protected void txtMacStopBeginTime_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CalcProcessTime();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in machine stopage begin time.\r\nCheck your data."));
        }
    }

    protected void txtMacStopEndTime_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CalcProcessTime();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in machine stopage end time.\r\nCheck your data."));
        }
    }

    private string CalcProcessTime()
    {
        try
        {

            string msg = string.Empty;
            DateTime dtValidBegindate = DateTime.Parse(lblValidateStartTime.Text);
            DateTime dtValidEndDate = DateTime.Parse(lblValidateEndTime.Text);

            DateTime dtLoad = System.DateTime.Now.Date;
            if (!DateTime.TryParse(txtMacStopBeginTime.Text.Trim(), out dtLoad))
            {
                msg += "Please select Stopage Begin time.";
            }
            else if (dtLoad < dtValidBegindate || dtLoad > dtValidEndDate)
            {
                msg += "Please select valid Stopage Begin time.";
            }

            DateTime dtunLoad = System.DateTime.Now.Date;
            if (!DateTime.TryParse(txtMacStopEndTime.Text.Trim(), out dtunLoad))
            {
                msg += "Please select Stopage end time.";
            }
            else if (dtunLoad < dtValidBegindate || dtunLoad > dtValidEndDate)
            {
                msg += "Please select valid Stopage end time.";
            }

            if (msg.Equals(string.Empty))
            {

                TimeSpan tm = dtunLoad.Subtract(dtLoad);
                int diffday = tm.Days;
                int diffhour = tm.Hours;
                int diffminute = tm.Minutes;
                int Totaldiffminute = diffday * 24 * 60 + diffhour * 60 + diffminute;

                int TotalProcessTime = Totaldiffminute;

                if (TotalProcessTime < 0)
                {
                    Common.CommonFuction.ShowMessage("Please select stop begin time less than stop end time");
                    txtMacStopEndTime.Text = string.Empty;
                }
                txtMacStopDuration.Text = TotalProcessTime.ToString();
            }
            else
            {
                Common.CommonFuction.ShowMessage(msg);
            }
            return msg;
        }
        catch
        {
            throw;
        }
    }

    protected void btnMacStopSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            GetvalidateTime();

            int TotalStopage = int.Parse(txtTotalMachineStop.Text);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:GetRowValue('" + TotalStopage + "','" + hfTextBoxId.Value + "')", true);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for adjustment.\r\nSee error log for detail."));
        }
    }
}
