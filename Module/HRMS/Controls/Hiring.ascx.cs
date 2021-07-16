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
using System.IO;
using DBLibrary;

public partial class Module_HRMS_Controls_Hiring : System.Web.UI.UserControl
{
    public static string strCompanyCode = "";
    public static string strBranchCode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            strCompanyCode = oUserLoginDetail.COMP_CODE.Trim().ToString();
            strBranchCode = oUserLoginDetail.CH_BRANCHCODE.Trim().ToString();
            if (!Page.IsPostBack)
            {
                Initial_Control();
                trOldDetail.Visible = false;
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
            Load_Department();
            Bind_Designation();
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
    private void Load_Department()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getEmployeeDepartment();
            DDLDepartment.DataSource = dt;
            DDLDepartment.DataValueField = "DEPT_CODE";
            DDLDepartment.DataTextField = "DEPT_NAME";
            DDLDepartment.DataBind();
            DDLDepartment.Items.Insert(0, new ListItem("----------SELECT----------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }
    private void Bind_Designation()
    {
        try
        {
            //////////////////////////// Bind Branch Name//////////////////////////////////////
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getEmployeeDesignation();
            DDLOldPDesig.DataSource = dt;
            DDLOldPDesig.DataValueField = "desig_Code";
            DDLOldPDesig.DataTextField = "desig_Name";
            DDLOldPDesig.DataBind();
            DDLOldPDesig.Items.Insert(0, new ListItem("------------SELECT------------", ""));
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
            DDLBreakup.SelectedIndex=0;
            DDLDepartment.SelectedIndex = -1;
            DDLLocation.SelectedIndex = -1;
            DDLOldPDesig.SelectedIndex = -1;
            DDLPosition.SelectedIndex = -1;
            DDLSelSource.SelectedIndex = 0;
            TxtCandidatename.Text = string.Empty;
            TxtCanJoinDate.Text = string.Empty;
            txtDOE.Text = string.Empty;
            TxtDOJ.Text = string.Empty;
            TxtDRR.Text = string.Empty;
            TxtOfferDate.Text = string.Empty;
            TxtOldPerson.Text = string.Empty;
            TxtRemarks.Text = string.Empty;
            TxtTTF.Text = string.Empty;
            TxtTTS.Text = string.Empty;
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex,"Problem in Exit.\\r\\nSee error log for detail."));
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
        if (Record_Insert())
        {
            Common.CommonFuction.ShowMessage("Record Update Sucessfully");
        }
        else
        {
            Common.CommonFuction.ShowMessage("Unable to Insert");
        }
    }
    private bool Record_Insert()
    {
        try
        {
            SaitexDM.Common.DataModel.HR_HIRING_PRO HP = new SaitexDM.Common.DataModel.HR_HIRING_PRO();
            HP.BREAKPS = DDLBreakup.SelectedValue.ToString().Trim();
            if (TxtCanJoinDate.Text.Trim() != string.Empty)
            {
                HP.CANDJOIN = DateTime.Parse(TxtCanJoinDate.Text.Trim());
            }
            else
            {
                HP.CANDJOIN = DateTime.MinValue ;
            }
            HP.DEPT_ID = DDLDepartment.SelectedValue.ToString();
            HP.DRR = TxtDRR.Text.Trim();
            if (TxtHireID.Text.Trim() == "NEW")
            {
                HP.HIR_ID = "0";
            }
            else
            {
                HP.HIR_ID=TxtHireID.Text.Trim();
            }
            if (ChkReplace.Checked)
            {
                HP.IS_REP = "Y";
            }
            else
            {
                 HP.IS_REP ="N";
            }
            HP.LOC_ID = DDLLocation.SelectedValue.ToString();
            HP.NOV = RBNOV.SelectedValue.ToString();
            HP.NPNAME = TxtCandidatename.Text.Trim();
            if (TxtOfferDate.Text.Trim() != string.Empty)
            {
                HP.OFFER_DATE =DateTime.Parse(TxtOfferDate.Text.Trim());
            }
            else
            {
                HP.OFFER_DATE = DateTime.MinValue;
            }     
           
            HP.OPDEG_CODE = DDLOldPDesig.SelectedValue.ToString();
            if (txtDOE.Text.Trim() != string.Empty)
            {
                HP.OPDOE = DateTime.Parse(txtDOE.Text.Trim());
            }
            else
            {
                HP.OPDOE = DateTime.MinValue;
            }           
            HP.OPNAME = TxtOldPerson.Text.Trim();
            HP.POSITION = DDLPosition.SelectedValue.ToString();
            HP.RECTYPE = RBReqType.SelectedValue.ToString();
            HP.RMARKS = TxtRemarks.Text.Trim();
            HP.SELECT_SOURCE = DDLSelSource.SelectedValue.ToString();
            if (TxtDOJ.Text.Trim() != string.Empty)
            {
                HP.TDOJ = DateTime.Parse(TxtDOJ.Text.Trim());
            }
            else
            {
                HP.TDOJ = DateTime.MinValue;
            }          
            HP.TTF = TxtTTF.Text.Trim().ToString();
            HP.TTS = TxtTTS.Text.Trim();
            bool Res = SaitexBL.Interface.Method.HR_HIRING_PRO.Insert_Record(HP);
           return Res;
             
        }
        catch
        {
            throw;
        }
    }   
    protected void ChkReplace_CheckedChanged(object sender, EventArgs e)
    {try
        {
            if (ChkReplace.Checked)
            {
                trOldDetail.Visible = true;
                txtDOE.ReadOnly = false;
                TxtOldPerson.ReadOnly = false;
                DDLOldPDesig.Enabled = true;
            }
            else
            {
                trOldDetail.Visible = false;
                txtDOE.ReadOnly = true ;
                TxtOldPerson.ReadOnly = true;
                DDLOldPDesig.Enabled = false ;
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex,"Problem in Select Changes"));
        }

    }

    private void Load_Hiring()
    {
        try
        {
            DataTable DT = new DataTable();
            DT = SaitexBL.Interface.Method.HR_HIRING_PRO.Load_Record();
            DDLHiringRecord.DataSource = DT;
            DDLHiringRecord.DataTextField = "NPNAME";
            DDLHiringRecord.DataValueField = "HIR_ID";
            DDLHiringRecord.DataBind();
            DDLHiringRecord.Items.Insert(0, "--------SELECT-------");
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
            lblMode.Text  = "Update";
            trFind.Visible = true;
            tdUpdate.Visible = true;
            tdClear.Visible = false;
            tdDelete.Visible = true;
            tdSave.Visible = false;
            Load_Hiring();
        }
        catch(Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Finding Records"));
        }

    }
    protected void DDLHiringRecord_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            TxtHireID.Text = DDLHiringRecord.SelectedValue.ToString().Trim();
            Load_Records(DDLHiringRecord.SelectedValue.Trim().ToString());
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex,"Error in Record Searching"));
        }
    }
    private void Load_Records(string HIR_ID)
    {
        try
        {
            DataTable DT = SaitexBL.Interface.Method.HR_HIRING_PRO.Load_Record_ByID(HIR_ID);
            if (DT.Rows.Count > 0)
            {
                TxtCandidatename.Text = DT.Rows[0]["NPNAME"].ToString();
                TxtCanJoinDate.Text = DT.Rows[0]["CANDJOIN"].ToString();
                txtDOE.Text = DT.Rows[0]["OPDOE"].ToString();
                TxtDOJ.Text = DT.Rows[0]["TDOJ"].ToString();
                TxtDRR.Text = DT.Rows[0]["DRR"].ToString();
                TxtOfferDate.Text = DT.Rows[0]["OFFER_DATE"].ToString();
                TxtOldPerson.Text = DT.Rows[0]["OPNAME"].ToString();
                TxtRemarks.Text = DT.Rows[0]["RMARKS"].ToString();
                if (DT.Rows[0]["IS_REP"].ToString().Trim() == "Y")
                {
                    ChkReplace.Checked = true;
                }
                else
                {
                    ChkReplace.Checked = false;
                }
                TxtTTF.Text = DT.Rows[0]["TTS"].ToString();
                TxtTTS.Text = DT.Rows[0]["TTF"].ToString();
                DDLBreakup.SelectedValue = DT.Rows[0]["BREAKPS"].ToString();
                DDLDepartment.SelectedValue = DT.Rows[0]["DEPT_ID"].ToString();
                DDLLocation.SelectedValue = DT.Rows[0]["LOC_ID"].ToString();
                DDLOldPDesig.SelectedValue = DT.Rows[0]["OPDEG_CODE"].ToString();
                DDLPosition.SelectedValue = DT.Rows[0]["POSITION"].ToString();
                DDLSelSource.SelectedValue = DT.Rows[0]["SELECT_SOURCE"].ToString();
                RBNOV.SelectedValue = DT.Rows[0]["NOV"].ToString();
                RBReqType.SelectedValue = DT.Rows[0]["RECTYPE"].ToString();

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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex,"Error in Deleting"));
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
            Res = SaitexBL.Interface.Method.HR_HIRING_PRO.Delete_Record(HIR_ID );
            return Res;
        }
        catch
        {
            throw;
        }
    }
}
