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

public partial class Module_OrderDevelopment_Controls_StopRunningMachine : System.Web.UI.UserControl
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
            DataTable dtMachine = SaitexBL.Interface.Method.MC_MACHINE_MASTER.GetRunningMachineDetails(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            grdMachine.DataSource = dtMachine;
            grdMachine.DataBind();
            lblTotalRecord.Text = dtMachine.Rows.Count.ToString();
             otherDetails(dtMachine);
           
        }
        catch
        {
            throw;
        }
    }

    protected void grdMachine_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblMachineStatus = (Label)e.Row.FindControl("lblMachineStatus");
                TextBox txtLoadingDate = (TextBox)e.Row.FindControl("txtLoadingDate");

                if (lblMachineStatus.Text.Trim().Equals("Idle"))
                {
                   // e.Row.BackColor = System.Drawing.Color.Red;
                    lblMachineStatus.ForeColor = System.Drawing.Color.Red;
                    txtLoadingDate.Enabled = false;                    
                }
                else
                {
                    //e.Row.BackColor = System.Drawing.Color.Green;
                    lblMachineStatus.ForeColor = System.Drawing.Color.Green;
                    txtLoadingDate.Enabled = true ;
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
            DataTable dtMachine = CreateDataTable();
            if (grdMachine.Rows.Count > 0)
            {
                foreach (GridViewRow grdRow in grdMachine.Rows)
                {
                    TextBox txtLoadingDate = (TextBox)grdRow.FindControl("txtLoadingDate");
                    if (!string.IsNullOrEmpty(txtLoadingDate.Text))
                    {
                      
                        Label lblMachineCode = (Label)grdRow.FindControl("lblMachineCode");
                        Label lblRunningLotNo = (Label)grdRow.FindControl("lblRunningLotNo");
                        TextBox LoadingDate = (TextBox)grdRow.FindControl("txtLoadingDate");
                        DateTime LOADTIME = DateTime.Now;
                        DateTime.TryParse(LoadingDate.Text, out LOADTIME);
                        DataRow dr = dtMachine.NewRow();
                        dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE ;
                        dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                        dr["MACHINE_CODE"] = lblMachineCode.Text.Trim();
                        dr["RUNNING_LOT_NO"] = lblRunningLotNo.Text.Trim();
                        dr["ULOAD_DATE"] = LOADTIME;
                        dr["TUSER"] = oUserLoginDetail.UserCode;
                        dr["TDATE"] = DateTime.Now;
                            

                        dtMachine.Rows.Add(dr);
                    }
                }
            }
            int   iResult = 0;
            bool resultFlag = SaitexBL.Interface.Method.MC_MACHINE_MASTER.UpdateOnMachineStop(dtMachine, out iResult);
            if (iResult > 0)
            {
                CommonFuction.ShowMessage("Machine Status Updated successfully.");
            }
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
            DataTable dtMachine = new DataTable();
            dtMachine.Columns.Add("COMP_CODE", typeof(string));
            dtMachine.Columns.Add("BRANCH_CODE", typeof(string));
            dtMachine.Columns.Add("MACHINE_CODE", typeof(string));
            dtMachine.Columns.Add("RUNNING_LOT_NO", typeof(string));
            dtMachine.Columns.Add("ULOAD_DATE", typeof(DateTime));
            dtMachine.Columns.Add("TUSER", typeof(string));
            dtMachine.Columns.Add("TDATE", typeof(DateTime));          
            return dtMachine;
        }
        catch
        {
            throw;
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
            //string Query_String = string.Empty;

            //string URL = "../../Production/Report/Production_Lot_Move_Report.aspx?Query_String =" + Query_String;
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);

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

    protected void otherDetails(DataTable dt)
    {
        DataView dv = new DataView(dt);
        dv.RowFilter = "(MACHINE_STATUS like 'Idle') ";
        lblIdleMachine.Text = (dv.Count).ToString();
        dv.RowFilter = "(MACHINE_STATUS like 'Running') ";
        lblRunningMachine.Text = (dv.Count).ToString();
    }
}
