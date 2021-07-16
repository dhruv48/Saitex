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


public partial class Module_HRMS_Controls_HR_PF_ESI_MST : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static  DataTable DTable;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Page.MaintainScrollPositionOnPostBack = true;
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {               
                Blank_Controls();
                Load_Location();
                LOAD_PF_ESI_RECORD();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Page Loading.\r\nSee error log for detail."));
        }

    }
    
    private void Blank_Controls()
    {
        try
        {
            TxtTrnValue.Text = string.Empty;
            TxtCOB.Text = string.Empty;
            DDLBranch.SelectedIndex = -1;
            tdDelete.Visible = false;
            tdUpdate.Visible = false;
            tdSave.Visible = true;
            tdClear.Visible = true;
            tdFind.Visible = true;
            tdExit.Visible = true;
        }
        catch
        {
            throw;
        }
    }

    private bool InsertData()
    {
        try
        {
            SaitexDM.Common.DataModel.HR_PF_ESI_MST P = new SaitexDM.Common.DataModel.HR_PF_ESI_MST();
            if (TxtTrnNO.Text != "New")
            {
                P.Trn_No = decimal.Parse(TxtTrnNO.Text.Trim());
            }
            else
            {
                P.Trn_No = 0;
            }
            P.PCT= int.Parse(CommonFuction.funFixQuotes(TxtTrnValue.Text.Trim().ToString()));
            P.CB = int.Parse(CommonFuction.funFixQuotes(TxtCOB.Text.Trim().ToString()));
            P.LOCATION = CommonFuction.funFixQuotes(DDLBranch.SelectedValue.Trim().ToString());
            P.YEAR = int.Parse(oUserLoginDetail.DT_STARTDATE.Year.ToString());
            P.TRN_TYPE= DDLTrnType.SelectedValue.Trim().ToString();
            P.TUSER = oUserLoginDetail.UserCode.ToString();
            P.COMP_CODE = oUserLoginDetail.COMP_CODE.ToString();
            P.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE.ToString();
           
            bool RES = SaitexBL.Interface.Method.HR_PF_ESI_MST.InsertData(P);
            
            return RES;
        }
        catch
        {
            throw;
        }
    }

    private void LOAD_PF_ESI_RECORD()
    {
        try
        {
            DTable = new DataTable();
            DTable = SaitexBL.Interface.Method.HR_PF_ESI_MST.Load_PFESI_Record();
            GridViewESIPF.DataSource = DTable;
            GridViewESIPF.DataBind();
        }
        catch
        {
            throw;
        }
    }
    private void Load_Location()
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(oUserLoginDetail.COMP_CODE);
            DDLBranch.DataSource = dt;
            DDLBranch.DataValueField = "BRANCH_CODE";
            DDLBranch.DataTextField = "BRANCH_NAME";
            DDLBranch.DataBind();
            DDLBranch.Items.Insert(0, new ListItem("------------SELECT------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }
    protected void GridViewESIPF_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            decimal UniqueId = decimal.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "EmpEdit")
            {
                tdSave.Visible = false;
                tdUpdate.Visible = true;
                FillDetailByGrid(UniqueId);
            }
            else if (e.CommandName == "EmpDelete")
            {
                Delete_record_by_ID(UniqueId.ToString());
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in deleting Record.\r\nSee error log for detail."));
        }

    }
    private void FillDetailByGrid(decimal UniqueId)
    {
        try
        {
            DataView dv = new DataView(DTable);
            dv.RowFilter = "TRN_NO=" + UniqueId;
            if (dv.Count > 0)
            {
                DDLTrnType.SelectedValue = dv[0]["TRN_TYPE"].ToString();
                TxtTrnValue.Text = dv[0]["TRN_VALUE"].ToString();
                TxtCOB.Text = dv[0]["CB"].ToString();
                DDLBranch.SelectedValue  = dv[0]["Location"].ToString();
                ViewState["TRN_NO"] = UniqueId;
            }
        }
        catch
        {
            throw;
        }
    }
    private void Delete_record_by_ID(string TRN_NO)
    {
        try
        {
            bool Res = SaitexBL.Interface.Method.HR_PF_ESI_MST.Delete_Record_By_Id(TRN_NO.ToString());
            if (Res)
            {
                Common.CommonFuction.ShowMessage("Record delete sucessfully");
                LOAD_PF_ESI_RECORD();
            }
            else
            {
                Common.CommonFuction.ShowMessage("Unable to delete,please try again");
            }
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
            if (InsertData())
            {
                CommonFuction.ShowMessage("data Insert sucessfully");
                LOAD_PF_ESI_RECORD();
                Blank_Controls(); 
            }
            else
            {
                CommonFuction.ShowMessage("Problem in inserting data");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in inserting data"));
        }
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (InsertData())
            {
                CommonFuction.ShowMessage("updating data sucessfully");
                LOAD_PF_ESI_RECORD();
            }
            else
            {
                CommonFuction.ShowMessage("Problem in updating data");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in inserting data"));
        }

    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Blank_Controls();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Clear The Data"));
        }

    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Helping.\r\nSee error log for detail."));
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
                Response.Redirect("~/Saitex/Module/Admin/Pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Exit Form"));
        }

    }    
}
