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

public partial class Module_OrderDevelopment_Controls_Lot_Movemement_Conf : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in page loading.\r\nSee error log for detail."));
        }
    }

    private void InitialisePage()
    {
        try
        {
            DataTable dtLotConf = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetLot_Mov_for_Confirmation(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.VC_DEPARTMENTCODE);

            grdLOT_MOV_CONF.DataSource = dtLotConf;
            grdLOT_MOV_CONF.DataBind();

            lblTotalRecord.Text = dtLotConf.Rows.Count.ToString();
        }
        catch
        {
            throw;
        }
    }

    protected void grdLOT_MOV_CONF_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblToDept = (Label)e.Row.FindControl("lblToDept");
                CheckBox chkConf = (CheckBox)e.Row.FindControl("chkConf");
                CheckBox chkReject = (CheckBox)e.Row.FindControl("chkReject");

                if (lblToDept.Text.Trim().Equals(oUserLoginDetail.VC_DEPARTMENTCODE.Trim()))
                {
                    e.Row.BackColor = System.Drawing.Color.LightGreen;
                    chkConf.Enabled = true;
                    chkConf.Checked = false;
                    chkReject.Enabled = true;
                    chkReject.Checked = false;
                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.Red;
                    chkConf.Enabled = false;
                    chkConf.Checked = false;
                    chkReject.Enabled = false;
                    chkReject.Checked = false;
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in Data Loading.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dtLotConf = CreateDataTable();
            if (grdLOT_MOV_CONF.Rows.Count > 0)
            {
                foreach (GridViewRow grdRow in grdLOT_MOV_CONF.Rows)
                {
                    CheckBox chkConf = (CheckBox)grdRow.FindControl("chkConf");
                    CheckBox chkReject = (CheckBox)grdRow.FindControl("chkReject");

                    if (chkConf.Checked == true || chkReject.Checked == true)
                    {
                        //Label lblDeptCode = (Label)grdRow.FindControl("lblDeptCode");
                        Label lblDeptCode = (Label)grdRow.FindControl("lblToDept");
                        Label lblEntryNo = (Label)grdRow.FindControl("lblEntryNo");
                        Label lblEntryDate = (Label)grdRow.FindControl("lblEntryDate");
                        Label lblLotNo = (Label)grdRow.FindControl("lblLotNo");
                        Label lblEntryType = (Label)grdRow.FindControl("lblEntryType");
                        Label lblYear = (Label)grdRow.FindControl("lblYear");
                        DataRow dr = dtLotConf.NewRow();
                        dr["DEPT_CODE"] = lblDeptCode.Text.Trim();
                        dr["ENTRY_TYPE"] = lblEntryType.Text.Trim();
                        dr["ENTRY_NO"] = int.Parse(lblEntryNo.Text.Trim());
                        dr["ENTRY_DATE"] = DateTime.Parse(lblEntryDate.Text.Trim());
                        dr["LOT_NUMBER"] = lblLotNo.Text.Trim();
                        int year = 0;
                        int.TryParse(lblYear.Text, out year);
                        dr["YEAR"] = year;

                        if (chkConf.Checked)
                        {
                            dr["CONFIRMED"] = "1";
                        }
                        else if (chkReject.Checked)
                        {

                            dr["CONFIRMED"] = "2";
                        }

                        dtLotConf.Rows.Add(dr);
                    }
                }
            }
            string msg = string.Empty;
            int iResult = SaitexBL.Interface.Method.OD_CAPTURE_MST.CONF_Lot_Mov(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.VC_DEPARTMENTCODE, oUserLoginDetail.UserCode, dtLotConf, out msg); 

            CommonFuction.ShowMessage(msg);
            InitialisePage();

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in data saving.\r\nSee error log for detail."));
        }
    }

    private DataTable CreateDataTable()
    {
        try
        {
            DataTable dtLotConf = new DataTable();
            dtLotConf.Columns.Add("DEPT_CODE", typeof(string));
            dtLotConf.Columns.Add("ENTRY_TYPE", typeof(string));
            dtLotConf.Columns.Add("ENTRY_NO", typeof(int));
            dtLotConf.Columns.Add("ENTRY_DATE", typeof(DateTime));
            dtLotConf.Columns.Add("LOT_NUMBER", typeof(string));
            dtLotConf.Columns.Add("CONFIRMED", typeof(string));
            dtLotConf.Columns.Add("YEAR", typeof(int));
            return dtLotConf;
        }
        catch
        {
            throw;
        }
    }

    protected void chkConf_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow grdRow = (GridViewRow)((DataControlFieldCell)((CheckBox)sender).Parent).Parent;
        CheckBox chkConf = (CheckBox)grdRow.FindControl("chkConf");
        CheckBox chkReject = (CheckBox)grdRow.FindControl("chkReject");

        if (chkConf.Checked == true && chkReject.Checked == true)
        {
            CommonFuction.ShowMessage("Please uncheck reject checkbox to confirm that lot movement.");
        }
    }

    protected void chkReject_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow grdRow = (GridViewRow)((DataControlFieldCell)((CheckBox)sender).Parent).Parent;
        CheckBox chkConf = (CheckBox)grdRow.FindControl("chkConf");
        CheckBox chkReject = (CheckBox)grdRow.FindControl("chkReject");

        if (chkConf.Checked == true && chkReject.Checked == true)
        {
            CommonFuction.ShowMessage("Please uncheck confirm checkbox to reject that lot movement.");
        }
    }

    protected void imgbtnFindTop_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {


            //string YarnDesc = txtyarndesc.Text.Trim();
            string Query_String = string.Empty;


            string URL = "../../Production/Report/Production_Lot_Move_Report_Twist.aspx?Query_String =" + Query_String;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in getting print.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        InitialisePage();
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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in leaving page.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
}
