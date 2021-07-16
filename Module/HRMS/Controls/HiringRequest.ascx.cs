using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common;
using errorLog;
using System.IO;
using DBLibrary;

public partial class Module_HRMS_Controls_HiringRequest : System.Web.UI.UserControl
{

    public static string strCompanyCode = string.Empty ;
    public static string EmpCode = string.Empty ;
    public static string DeptCode = string.Empty;
    public static string strBranchCode = string.Empty;
    private static string ReportTo = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            strCompanyCode = oUserLoginDetail.COMP_CODE.Trim().ToString();
            strBranchCode = oUserLoginDetail.CH_BRANCHCODE.Trim().ToString();
            EmpCode= oUserLoginDetail.UserCode;        
            if (!Page.IsPostBack)
            {
                txtDept.Text = oUserLoginDetail.VC_DEPARTMENTNAME;               
                if (Session["ReportTo"].ToString() != string.Empty && Session["ReportTo"].ToString() != "")
                {
                    ReportTo = Session["ReportTo"].ToString();
                }
                else
                {
                    ReportTo = "SA0001";
                }               
                DeptCode = oUserLoginDetail.VC_DEPARTMENTCODE;
                Initial_Control();               
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Page Loading.\\r\\nSee error log for detail."));
        }
    }
    private void Initial_Control()
    {
        try
        {
            Clear_Control();
            Bind_Postion();           
            Load_BrachName();
                Load_Records();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Initial Control.\\r\\nSee error log for detail."));
        }
    }   
    private void Bind_Postion()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.Bind_Position();
            DDLPosition.DataSource = dt;
            DDLPosition.DataValueField = "POSITION_CODE";
            DDLPosition.DataTextField = "POSITION_NAME";
            DDLPosition.DataBind();
            DDLPosition.Items.Insert(0, new ListItem("----------SELECT----------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }
    private void Load_BrachName()
    {
        try
        {
            //////////////////////////// Bind Branch Name//////////////////////////////////////
            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(strCompanyCode);
            DDLLocation.DataSource = dt;
            DDLLocation.DataValueField = "BRANCH_CODE";
            DDLLocation.DataTextField = "BRANCH_NAME";
            DDLLocation.DataBind();
            DDLLocation.Items.Insert(0, new ListItem("----------SELECT----------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }       
    private void Clear_Control()
    {
        try
        {
            lblMode.Text = "Save";
            trFind.Visible = false;
            tdDelete.Visible = false;
            tdUpdate.Visible = false;
            tdSave.Visible = true;
            TxtTotalVac.Text = string.Empty;
            TxtRemarks.Text = string.Empty;
           
            DDLLocation.SelectedIndex = -1;
            DDLPosition.SelectedIndex = -1;
            DDLPriority.SelectedIndex = -1;
            DDLVacType.SelectedIndex = -1;
           
        }
        catch
        {
            throw;
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
                Response.Redirect("~/Module/Admin/Pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Exit.\\r\\nSee error log for detail."));
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Clear_Control();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in control clear "));
        }
    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Record_Insert())
            {
                Common.CommonFuction.ShowMessage("Record Insert Sucessfully");
                Clear_Control();
                Load_Records();
            }
            else
            {
                Common.CommonFuction.ShowMessage("Unable to Insert");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Record Insert \\r\\nShow Error log"));
        }
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Record_Insert())
            {
                Common.CommonFuction.ShowMessage("Record Insert Sucessfully");
                Clear_Control();
                Load_Records();
            }
            else
            {
                Common.CommonFuction.ShowMessage("Unable to Insert");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Record Insert \\r\\nShow Error log"));
        }
    }
    private void Load_Records_ByID(string HIR_ID)
    {
        try
        {
            DataTable DT = SaitexBL.Interface.Method.HiringReq.Load_Record_ByID(HIR_ID, DeptCode);
            if (DT.Rows.Count > 0)
            {
                TxtTotalVac.Text = DT.Rows[0]["TOTAL_VAC"].ToString();               
                DDLLocation.SelectedValue = DT.Rows[0]["LOC_ID"].ToString();
                DDLPriority.SelectedValue = DT.Rows[0]["PRIORITY"].ToString();
                DDLPosition.SelectedValue = DT.Rows[0]["POSITION"].ToString();
                DDLVacType.SelectedValue = DT.Rows[0]["VAC_TYPE"].ToString();
                TxtRemarks.Text = DT.Rows[0]["REMARKS"].ToString();
                TxtHireID.Text = DT.Rows[0]["HIR_RQ_ID"].ToString();
            }
            else
            {
                Common.CommonFuction.ShowMessage("No Record Find");
            }

        }
        catch
        {
            throw;
        }
    }
    private void Load_Records()
    {
        try
        {
            DataTable DT = SaitexBL.Interface.Method.HiringReq.Load_Record(EmpCode,"");
            GrdViewHiring.DataSource = DT;
            GrdViewHiring.DataBind();
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
            if (Can_Delete())
            {
                if (Delete_Record(TxtHireID.Text.Trim().ToString()))
                {
                    Common.CommonFuction.ShowMessage("Record has been Deleted");
                    Clear_Control();
                    Load_Records();
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Record not Deleted!please try again");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please select Record");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Error in Deleting"));
        }
    }
    private bool Can_Delete()
    {
        try
        {
            if (TxtHireID.Text.Trim().ToString() != "NEW")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            throw;
        }
    }
    private bool Delete_Record(string HIR_ID)
    {
        bool Res = false;
        try
        {
            Res = SaitexBL.Interface.Method.HiringReq.Delete_Record(HIR_ID);
            return Res;
        }
        catch
        {
            throw;
        }
    }
    private bool Record_Insert()
    {
        try
        {
            SaitexDM.Common.DataModel.HiringReq HP = new SaitexDM.Common.DataModel.HiringReq();
            HP.DEPT_CODE = DeptCode;
            if (TxtHireID.Text.Trim().ToString() != "NEW")
            {
                HP.HIR_RQ_ID = decimal.Parse(TxtHireID.Text.Trim().ToString());
            }
            else
            {
                HP.HIR_RQ_ID = 0;
            }            
            HP.LOC_ID = DDLLocation.SelectedValue.ToString();
            HP.POSITION = DDLPosition.SelectedValue.ToString();
            HP.PRIORITY = DDLPriority.SelectedValue.ToString();
            HP.REMARKS = TxtRemarks.Text.Trim().ToString();
            HP.REQ_BY = Session["urLoginId"].ToString();
            HP.APPROVE_BY = ReportTo;
            HP.TOTAL_VAC = decimal.Parse(TxtTotalVac.Text.Trim().ToString());
            HP.VAC_TYPE = DDLVacType.SelectedValue.ToString();
            bool Res = SaitexBL.Interface.Method.HiringReq.Insert_Record(HP);
            return Res;
        }
        catch
        {
            throw;
        }
    }

    protected void GrdViewHiring_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ImageEdit")
            {
                Load_Records_ByID(e.CommandArgument.ToString());
            }
            if (e.CommandName == "ImageDelete")
            {
                Delete_Record(Convert.ToString(e.CommandArgument));
                Load_Records();
            }
            
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Grid Command"));
        }
    }
}
