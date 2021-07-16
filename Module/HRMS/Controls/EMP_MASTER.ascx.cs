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
using System.Data.OracleClient;
using errorLog;
using System.IO;
using Common;
using DBLibrary;
using Obout.ComboBox;

public partial class Module_HRMS_Controls_EMP_MASTER : System.Web.UI.UserControl
{
    public static string strCompanyCode = string.Empty;
    public static string strDepartmentName = string.Empty;
    public static string strBranchCode = string.Empty;
    public static string strBranchName = string.Empty;
    public static string SelectedEmpCode = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected override void OnPreRender(EventArgs e)
    {
        try
        {
            base.OnPreRender(e);
            imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");
            imgbtnDelete.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
            imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
            imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
            imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit')");
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (Session["usrNames"] != null)
            {
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                this.txtPassword.Attributes.Add("value", this.txtPassword.Text);
                if (!IsPostBack)
                {
                    strCompanyCode = oUserLoginDetail.COMP_CODE.Trim().ToString();
                    strBranchCode = oUserLoginDetail.CH_BRANCHCODE.Trim().ToString();
                    Page_Initial();
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Page Loading"));
        }
    }
    private void Page_Initial()
    {
        try
        {
            string EmpCode = SaitexBL.Interface.Method.EmployeeMaster.Get_Max_EMP_Code(strCompanyCode);
            txtEmployeeCode.Text = EmpCode.Trim().ToString();
            bindddlBrachName();
            Bind_DropDown("EMP_LEVEL", DDLLevel);
            Bind_DropDown("EMP_CADDER", ddlCadderCode);
            Bind_DropDown("EMP_GRADE", ddlGrade);
            Bind_DropDown("EMP_SALUTION", ddlsalutation);
            Bind_DropDown("EMP_RELEGION", DDLReligion);
            Bind_DropDown("EMP_TYPE", DDLEMP_Type);
            Bind_DropDown("EMP_PAYMENTMODE", DDLPayment_Mode);
            Bind_DropDown("EMP_RELATIONSHIP", DDLRelation);
            Bind_DropDown("EMP_MARTIALSTATUS", DDLMarital_Status);
            Bind_DropDown("EMP_NATIONALITY", DDLNationality);
            Bind_Postion();
            bindddlEmployeeShift();
            getEmployeeDepartment();
            getEmployeeDesignation();
            Initial_Control();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    private void Initial_Control()
    {
        try
        {
            tdDelete.Visible = false;
            tdUpdate.Visible = false;
            tdSave.Visible = true;            
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            ddlEmployee.Visible = false;
            txtEmployeeCode.Visible = true;
            txtEmployeeCode.AutoPostBack = false;
            txtEmployeeCode.Enabled = false;
            BlanksControls();
            lblMode.Text = "Save";
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }

    public void Bind_DropDown(string MST_NAME, DropDownList DDL)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {

                DDL.Items.Clear();
                DDL.DataSource = dt;
                DDL.DataTextField = "MST_DESC";
                DDL.DataValueField = "MST_CODE";
                DDL.DataBind();
                DDL.Items.Insert(0, new ListItem("--------------Select---------------", "0"));

            }
        }
        catch
        {
            throw;
        }
    }
    private void Bind_Drop_Down(DropDownList DDL, string Search_Keyword, string TableName)
    {
        try
        {
            DataTable DT = new DataTable();
            DT.Rows.Clear();
            DT = SaitexBL.Interface.Method.EmployeeMaster.Load_Drop_Down_Control(Search_Keyword.ToUpper().ToString(), TableName, oUserLoginDetail.CH_BRANCHCODE.Trim().ToString(), oUserLoginDetail.COMP_CODE.Trim().ToString());
            DDL.DataSource = DT;
            DDL.DataTextField = Search_Keyword;
            DDL.DataValueField = Search_Keyword;
            DDL.DataBind();
            DDL.Items.Insert(0, "-------Select-------");

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void Bind_Postion()
    {
        try
        {
            //////////////////////////// Bind Branch Name//////////////////////////////////////
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.Bind_Position();
            DDLPosition.DataSource = dt;
            DDLPosition.DataValueField = "POSITION_CODE";
            DDLPosition.DataTextField = "POSITION_NAME";
            DDLPosition.DataBind();
            DDLPosition.Items.Insert(0, new ListItem("------------Select------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    private void bindddlBrachName()
    {
        try
        {
            //////////////////////////// Bind Branch Name//////////////////////////////////////

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(strCompanyCode);
            //DataView DV = new DataView(dt);
            //DV.RowFilter = "BRANCH_CODE='" + strBranchCode.Trim() + "'";
            ddlBranchName.DataSource = dt;
            ddlBranchName.DataValueField = "BRANCH_CODE";
            ddlBranchName.DataTextField = "BRANCH_NAME";
            ddlBranchName.DataBind();
            ddlBranchName.Items.Insert(0, new ListItem("------------Select------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }

    private void bindddlEmployeeShift()
    {
        try
        {
            //////////////////////////// Bind Branch Name//////////////////////////////////////
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getgetEmployeeShift();
            ddlShiftName.DataSource = dt;
            ddlShiftName.DataValueField = "sft_Id";
            ddlShiftName.DataTextField = "sft_Name";
            ddlShiftName.DataBind();
            ddlShiftName.Items.Insert(0, new ListItem("------------Select------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }

    private void getEmployeeDepartment()
    {
        try
        {
            ////////////////////////// Bind Branch Name//////////////////////////////////////
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getEmployeeDepartment();
            ddlDepartment.DataSource = dt;
            ddlDepartment.DataValueField = "DEPT_CODE";
            ddlDepartment.DataTextField = "DEPT_NAME";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("------------Select------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    private void getEmployeeDesignation()
    {
        try
        {
            //////////////////////////// Bind Branch Name//////////////////////////////////////
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getEmployeeDesignation();
            ddlDesignation.DataSource = dt;
            ddlDesignation.DataValueField = "desig_Code";
            ddlDesignation.DataTextField = "desig_Name";
            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0, new ListItem("------------Select------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    public void bindlstReportTo(string POSITION_CODE, string EMP_CODE)
    {
        try
        {

            LstReportTo1.Items.Clear();
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getEmployeeReprotTo(POSITION_CODE, EMP_CODE);
            LstReportTo1.DataSource = dt;
            LstReportTo1.DataValueField = "POSITION_CODE";
            LstReportTo1.DataTextField = "POSITION_NAME";
            LstReportTo1.DataBind();

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    private void BlanksControls()
    {

        try
        {

            ddlsalutation.SelectedIndex = 1;
            ddlShiftName.SelectedIndex = 1;
            ddlWeeeklyOff.SelectedIndex = 1;
            DDLReligion.SelectedIndex = 1;
            DDLRelation.SelectedIndex = 1;
            DDLPayment_Mode.SelectedIndex = 1;
            DDLEMP_Type.SelectedIndex = 1;
            DDLLevel.SelectedIndex = 1;
            DDLMarital_Status.SelectedIndex = 1;
            DDLNationality.SelectedIndex = 1;
            DDLPayment_Mode.SelectedIndex = 1;

            ddlCadderCode.SelectedIndex = 1;
            ddlGrade.SelectedIndex = 1;
            DDLPosition.SelectedIndex = -1;
            DDLPosition.SelectedIndex = -1;

            TxtLeavingDate.Text = string.Empty;
            TxtTerminationDate.Text = string.Empty;
            TxtSuspendingDate.Text = string.Empty;

            txtMarriageDate.Text = string.Empty;
            txtCardNo.Text = string.Empty;
            txtUserName.Text = string.Empty;
            txtUserName.Text = string.Empty;
            txtPassword.Text = "";
            txtFirstName.Text = string.Empty;
            txtMiddleName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtDateOfBirth.Text = string.Empty;
            txtFatherName.Text = string.Empty;
            txtJoiningDate.Text = string.Empty;
            txtSkill.Text = string.Empty;
            txtConfirmation.Text = string.Empty;
            txtLastIncrement.Text = string.Empty;
            txtLastPromotion.Text = string.Empty;
            txtEmailId.Text = string.Empty;            
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "EmpMaster_OPT.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=600,height=300');", true);
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
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
                Response.Redirect("~/Module/Admin/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message));
        }
    }
    protected void imgbtnUpdate_Click1(object sender, ImageClickEventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                UpdateEmployeeMasterData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Please Enter mendatory Record');", true);
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Page_Initial();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear the record"));
        }
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int iRecordFound = 0;
            SaitexDM.Common.DataModel.EmployeeMaster oEM = new SaitexDM.Common.DataModel.EmployeeMaster();
            oEM.EMP_CODE = txtEmployeeCode.Text.Trim();
            bool bResult = SaitexBL.Interface.Method.EmployeeMaster.DeleteEmployeeMaster(oEM, out iRecordFound);

            if (bResult)
            {
                Session["saveStatus"] = 1;
                Response.Redirect("~/Module/HRMS/Pages/EmployeeMaster.aspx?cId=D", false);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Deleting the record"));
        }

    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ddlEmployee.Visible = true;
            txtEmployeeCode.AutoPostBack = true;
            txtEmployeeCode.Focus();
            txtEmployeeCode.Text = string.Empty;

            lblMode.Text = "Find";
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            tdFind.Visible = false;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {

        if (Page.IsValid)
        {
            try
            {
                InsertEmployeeMasterData();
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.Message);
                Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving the record"));
            }
        }
    }

    private void InsertEmployeeMasterData()
    {
        try
        {
            int iRecordFound = 0;
            SaitexDM.Common.DataModel.EmployeeMaster oEM = new SaitexDM.Common.DataModel.EmployeeMaster();
            if (Can_save_update())
            {
                if (txtEmployeeCode.Text.Trim() != "" && ddlCadderCode.SelectedValue.Trim() != "" && txtFirstName.Text.Trim() != "" && ddlBranchName.Text.Trim() != "")
                {
                    oEM.EMP_CODE = CommonFuction.funFixQuotes(txtEmployeeCode.Text.Trim());
                    oEM.CADDER_CODE = CommonFuction.funFixQuotes(ddlCadderCode.SelectedValue.Trim());
                    oEM.CARD_NO = CommonFuction.funFixQuotes(txtCardNo.Text.Trim());
                    oEM.USER_NAME = CommonFuction.funFixQuotes(txtUserName.Text.Trim());
                    oEM.PWD = CommonFuction.funFixQuotes(txtPassword.Text.Trim());
                    oEM.F_NAME = CommonFuction.funFixQuotes(txtFirstName.Text.Trim());
                    oEM.M_NAME = CommonFuction.funFixQuotes(txtMiddleName.Text.Trim());
                    oEM.L_NAME = CommonFuction.funFixQuotes(txtLastName.Text.Trim());
                    oEM.EMAIL_ID = CommonFuction.funFixQuotes(txtEmailId.Text.Trim());
                    oEM.GENDER = CommonFuction.funFixQuotes(radGender.SelectedValue.Trim());


                    oEM.NATION = CommonFuction.funFixQuotes(DDLNationality.SelectedValue.Trim());

                    oEM.DOB = DateTime.Parse(txtDateOfBirth.Text.Trim().ToString());

                    oEM.F_H_NAME = CommonFuction.funFixQuotes(txtFatherName.Text.Trim());
                    oEM.RELATIONSHIP = CommonFuction.funFixQuotes(DDLRelation.SelectedValue.Trim());
                    oEM.MRTL_STATUS = CommonFuction.funFixQuotes(DDLMarital_Status.SelectedValue.Trim());
                    oEM.RELIGION = CommonFuction.funFixQuotes(DDLReligion.SelectedValue.Trim());
                    oEM.GRADE_ID = CommonFuction.funFixQuotes(ddlGrade.SelectedValue.Trim());
                    oEM.SFT_ID = Convert.ToInt16(ddlShiftName.SelectedValue.Trim());

                    oEM.STATUS = DDLStatus.SelectedValue.Trim().ToString();
                    if (DDLStatus.SelectedValue.Trim().ToString() == "A")
                    {
                        oEM.DEL_STATUS = "0";
                    }
                    else
                    {
                        oEM.DEL_STATUS = "1";
                    }                    
                    oEM.IsESI = DDLIsESI.SelectedValue.Trim().ToString();
                    oEM.IsPF = DDLIsPF.SelectedValue.Trim().ToString();

                    oEM.COMP_CODE = strCompanyCode;
                    oEM.BRANCH_CODE = CommonFuction.funFixQuotes(ddlBranchName.SelectedValue.Trim());
                    oEM.DEPT_CODE = CommonFuction.funFixQuotes(ddlDepartment.SelectedValue.Trim());

                    FileUpload img = (FileUpload)imgUpload;
                    Byte[] imgByte = null;
                    if (img.HasFile && img.PostedFile != null)
                    {
                        //To create a PostedFile
                        HttpPostedFile File = imgUpload.PostedFile;
                        //Create byte Array with file len
                        imgByte = new Byte[File.ContentLength];
                        //force the control to load data in array
                        File.InputStream.Read(imgByte, 0, File.ContentLength);
                        oEM.SUB_IMG = imgByte;
                        oEM.SUB_CONT_TYPE = img.PostedFile.ContentType;
                        oEM.POSTED_LEN = img.PostedFile.ContentLength;
                    }
                    else
                    {
                        oEM.SUB_CONT_TYPE = string.Empty;
                        oEM.POSTED_LEN = 0; 
                    }

                    oEM.SKILL = CommonFuction.funFixQuotes(txtSkill.Text.Trim());
                    oEM.JOIN_DT = Convert.ToDateTime(txtJoiningDate.Text.Trim());
                    oEM.CONF = CommonFuction.funFixQuotes(txtConfirmation.Text.Trim());
                    oEM.LAST_INC = CommonFuction.funFixQuotes(txtLastIncrement.Text.Trim());
                    oEM.LAST_PROMO = CommonFuction.funFixQuotes(txtLastPromotion.Text.Trim());
                    oEM.DESIG_CODE = CommonFuction.funFixQuotes(ddlDesignation.SelectedValue.Trim());
                    ///Changed in 21-02-2011
                    oEM.EMPLEVEL = CommonFuction.funFixQuotes(DDLLevel.SelectedValue.Trim());
                    oEM.POSITION = CommonFuction.funFixQuotes(DDLPosition.SelectedValue.Trim());
                    ///

                    oEM.EMP_TYPE = DDLEMP_Type.SelectedValue.Trim();
                    oEM.PAY_MODE = CommonFuction.funFixQuotes(DDLPayment_Mode.SelectedValue.Trim());
                    oEM.SALUTATION = ddlsalutation.SelectedValue.Trim();
                    oEM.DEAR = string.Empty;
                    
                    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                    oEM.TUSER = oUserLoginDetail.UserCode;

                    oEM.WEEK_OFF = ddlWeeeklyOff.SelectedItem.ToString();
                    if (txtMarriageDate.Text.Trim() != string.Empty && txtMarriageDate.Text.Trim() != "")
                    {
                        oEM.MARRIAGE_DATE = DateTime.Parse(txtMarriageDate.Text.Trim());
                    }
                    else
                    {
                        oEM.MARRIAGE_DATE = DateTime.MinValue;
                    }

                    if (TxtTerminationDate.Text.Trim() != string.Empty && TxtTerminationDate.Text.Trim() != "")
                    {
                        oEM.TERMINATION_DATE = DateTime.Parse(TxtTerminationDate.Text.Trim());
                    }
                    else
                    {
                        oEM.TERMINATION_DATE = DateTime.MinValue;
                    }

                    if (TxtLeavingDate.Text.Trim() != string.Empty && TxtLeavingDate.Text.Trim() != "")
                    {
                        oEM.LEAVING_DATE = DateTime.Parse(TxtLeavingDate.Text.Trim());
                    }
                    else
                    {
                        oEM.LEAVING_DATE = DateTime.MinValue;
                    }

                    if (TxtSuspendingDate.Text.Trim() != string.Empty && TxtSuspendingDate.Text.Trim() != "")
                    {
                        oEM.SUSPENDING_DATE = DateTime.Parse(TxtSuspendingDate.Text.Trim());
                    }
                    else
                    {
                        oEM.SUSPENDING_DATE = DateTime.MinValue;
                    }
                    bool bResult = SaitexBL.Interface.Method.EmployeeMaster.InsertEmployeeMaster(oEM, out iRecordFound);

                    if (bResult)
                    {
                        InsertEmployeeReportTo();
                        SAVE_SHIFT_ROTATION();
                        Session["saveStatus"] = 1;
                        Common.CommonFuction.ShowMessage("Record Save Sucessfully");
                        tdEmpInfo.Visible = true;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('This record is already saved Please save another Record');", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    private void UpdateEmployeeMasterData()
    {
        try
        {
            int iRecordFound = 0;
            SaitexDM.Common.DataModel.EmployeeMaster oEM = new SaitexDM.Common.DataModel.EmployeeMaster();
            if (Can_save_update())
            {

                if (txtEmployeeCode.Text.Trim() != "" && ddlCadderCode.SelectedValue.Trim() != "" && txtFirstName.Text.Trim() != "" && ddlBranchName.Text.Trim() != "")
                {
                    oEM.EMP_CODE = CommonFuction.funFixQuotes(txtEmployeeCode.Text.Trim());
                    oEM.CADDER_CODE = CommonFuction.funFixQuotes(ddlCadderCode.SelectedValue.Trim());
                    oEM.CARD_NO = CommonFuction.funFixQuotes(txtCardNo.Text.Trim());
                    oEM.USER_NAME = CommonFuction.funFixQuotes(txtUserName.Text.Trim());
                    oEM.PWD = CommonFuction.funFixQuotes(txtPassword.Text.Trim());
                    oEM.F_NAME = CommonFuction.funFixQuotes(txtFirstName.Text.Trim());
                    oEM.M_NAME = CommonFuction.funFixQuotes(txtMiddleName.Text.Trim());
                    oEM.L_NAME = CommonFuction.funFixQuotes(txtLastName.Text.Trim());
                    oEM.EMAIL_ID = CommonFuction.funFixQuotes(txtEmailId.Text.Trim());
                    oEM.GENDER = CommonFuction.funFixQuotes(radGender.SelectedValue.Trim());


                    oEM.NATION = CommonFuction.funFixQuotes(DDLNationality.SelectedValue.Trim());
                    oEM.DOB = Convert.ToDateTime(txtDateOfBirth.Text.Trim());
                    oEM.F_H_NAME = CommonFuction.funFixQuotes(txtFatherName.Text.Trim());
                    oEM.RELATIONSHIP = CommonFuction.funFixQuotes(DDLRelation.SelectedValue.Trim());
                    oEM.MRTL_STATUS = CommonFuction.funFixQuotes(DDLMarital_Status.SelectedValue.Trim());
                    oEM.RELIGION = CommonFuction.funFixQuotes(DDLReligion.SelectedValue.Trim());
                    oEM.GRADE_ID = CommonFuction.funFixQuotes(ddlGrade.SelectedValue.Trim());
                    oEM.SFT_ID = Convert.ToInt16(ddlShiftName.SelectedValue.Trim());

                    oEM.STATUS = DDLStatus.SelectedValue.Trim().ToString();
                    if (DDLStatus.SelectedValue.Trim().ToString() == "A")
                    {
                        oEM.DEL_STATUS = "0";
                    }
                    else
                    {
                        oEM.DEL_STATUS = "1";
                    }        
                    oEM.IsESI = DDLIsESI.SelectedValue.Trim().ToString();
                    oEM.IsPF = DDLIsPF.SelectedValue.Trim().ToString();

                    oEM.COMP_CODE = strCompanyCode;
                    oEM.BRANCH_CODE = CommonFuction.funFixQuotes(ddlBranchName.SelectedValue.Trim());
                    oEM.DEPT_CODE = CommonFuction.funFixQuotes(ddlDepartment.SelectedValue.Trim());

                    FileUpload img = (FileUpload)imgUpload;
                    Byte[] imgByte = null;
                    if (img.HasFile && img.PostedFile != null)
                    {
                        //To create a PostedFile
                        HttpPostedFile File = imgUpload.PostedFile;
                        //Create byte Array with file len
                        imgByte = new Byte[File.ContentLength];
                        //force the control to load data in array
                        File.InputStream.Read(imgByte, 0, File.ContentLength);
                        oEM.SUB_IMG = imgByte;
                        oEM.SUB_CONT_TYPE = img.PostedFile.ContentType;
                        oEM.POSTED_LEN = img.PostedFile.ContentLength;

                    } 
                    else
                    {
                        oEM.SUB_CONT_TYPE = string.Empty;
                        oEM.POSTED_LEN = 0; ;

                    }
                    ///Changed in 21-02-2011
                    oEM.EMPLEVEL = CommonFuction.funFixQuotes(DDLLevel.SelectedValue.Trim());
                    oEM.POSITION = CommonFuction.funFixQuotes(DDLPosition.SelectedValue.Trim());
                    ///
                    oEM.SKILL = CommonFuction.funFixQuotes(txtSkill.Text.Trim());
                    string JOIN_DT = String.Format("{0:d}", CommonFuction.funFixQuotes(txtJoiningDate.Text.Trim()));// CommonFuction.funFixQuotes(txtJoiningDate.Text.Trim());

                    oEM.JOIN_DT = Convert.ToDateTime(JOIN_DT);
                    oEM.CONF = CommonFuction.funFixQuotes(txtConfirmation.Text.Trim());
                    oEM.LAST_INC = CommonFuction.funFixQuotes(txtLastIncrement.Text.Trim());
                    oEM.LAST_PROMO = CommonFuction.funFixQuotes(txtLastPromotion.Text.Trim());
                    oEM.DESIG_CODE = CommonFuction.funFixQuotes(ddlDesignation.SelectedValue.Trim());
                    oEM.EMP_TYPE = DDLEMP_Type.SelectedValue.Trim();
                    oEM.PAY_MODE = CommonFuction.funFixQuotes(DDLPayment_Mode.SelectedValue.Trim());
                    oEM.SALUTATION = ddlsalutation.SelectedValue.Trim();
                    oEM.DEAR = CommonFuction.funFixQuotes(txtDear.Text.Trim());
                    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                    oEM.TUSER = oUserLoginDetail.UserCode;
                    oEM.WEEK_OFF = ddlWeeeklyOff.SelectedValue.ToString();
                    if (txtMarriageDate.Text.Trim() != string.Empty && txtMarriageDate.Text.Trim() != "")
                    {
                        oEM.MARRIAGE_DATE = DateTime.Parse(txtMarriageDate.Text.Trim());
                    }
                    else
                    {
                        oEM.MARRIAGE_DATE = DateTime.MinValue;
                    }
                    if (txtMarriageDate.Text.Trim() != string.Empty && txtMarriageDate.Text.Trim() != "")
                    {
                        oEM.MARRIAGE_DATE = DateTime.Parse(txtMarriageDate.Text.Trim());
                    }
                    else
                    {
                        oEM.MARRIAGE_DATE = DateTime.MinValue;
                    }

                    if (TxtTerminationDate.Text.Trim() != string.Empty && TxtTerminationDate.Text.Trim() != "")
                    {
                        oEM.TERMINATION_DATE = DateTime.Parse(TxtTerminationDate.Text.Trim());
                    }
                    else
                    {
                        oEM.TERMINATION_DATE = DateTime.MinValue;
                    }

                    if (TxtLeavingDate.Text.Trim() != string.Empty && TxtLeavingDate.Text.Trim() != "")
                    {
                        oEM.LEAVING_DATE = DateTime.Parse(TxtLeavingDate.Text.Trim());
                    }
                    else
                    {
                        oEM.LEAVING_DATE = DateTime.MinValue;
                    }

                    if (TxtSuspendingDate.Text.Trim() != string.Empty && TxtSuspendingDate.Text.Trim() != "")
                    {
                        oEM.SUSPENDING_DATE = DateTime.Parse(TxtSuspendingDate.Text.Trim());
                    }
                    else
                    {
                        oEM.SUSPENDING_DATE = DateTime.MinValue;
                    }
                    bool bResult = SaitexBL.Interface.Method.EmployeeMaster.UpdateEmployeeMaster(oEM, out iRecordFound);

                    if (bResult)
                    {
                        UpdateEmployeeReportTo();
                        SAVE_SHIFT_ROTATION();
                        Session["saveStatus"] = 1;
                        Common.CommonFuction.ShowMessage("Record Update Sucessfully");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    private bool Can_save_update()
    {
        bool Res = false;
        try
        {
            if (DDLStatus.SelectedValue == "T")
            {
                if (TxtTerminationDate.Text.Trim().ToString() != "" && TxtLeavingDate.Text.Trim().ToString() != "")
                {
                    Res = true;
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Please enter Termination date and Leaving Date");
                }                
            }
            else if (DDLStatus.SelectedValue == "S")
            {
                if (TxtSuspendingDate.Text.Trim().ToString() != "" && TxtLeavingDate.Text.Trim().ToString() != "")
                {
                    Res = true;
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Please enter Suspending date and Leaving Date");
                }                  
            }
            else if (DDLStatus.SelectedValue == "L")
            {
                if (TxtLeavingDate.Text.Trim().ToString() != "")
                {
                    Res = true;
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Please enter Leaving Date");
                }                
            }
            else 
            {
                Res = true;
            }
            return Res;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message));
            throw;
        }
    }
    private void SAVE_SHIFT_ROTATION()
    {
        bool bResult;
        try
        {
            SaitexDM.Common.DataModel.HR_SHIFT_ROTATION SR = new SaitexDM.Common.DataModel.HR_SHIFT_ROTATION();
            SR.EMP_CODE = CommonFuction.funFixQuotes(txtEmployeeCode.Text.Trim());
            SR.BRANCH_ID = ddlBranchName.SelectedValue.ToString();
            SR.SIFT_ID = ddlShiftName.SelectedValue.ToString();
            SR.FROM_DATE = DateTime.Parse(txtJoiningDate.Text.Trim().ToString());
            SR.TO_DATE = DateTime.Parse(txtJoiningDate.Text.Trim()).AddDays(15);
            SR.TOTAL_DAYS = 15;
            bResult = SaitexBL.Interface.Method.HR_SHIFT_ROTATION.Save_Shift(SR);
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    private void InsertEmployeeReportTo()
    {
        try
        {
            int iRecordFound = 0;
            bool bResult = false;
            SaitexDM.Common.DataModel.EmployeeMaster oEM = new SaitexDM.Common.DataModel.EmployeeMaster();

            foreach (ListItem ln in LstReportTo1.Items)
            {
                if (ln.Selected == true)
                {
                    oEM.EMP_CODE = CommonFuction.funFixQuotes(txtEmployeeCode.Text.Trim());
                    oEM.DESIG_CODE = ln.Value.ToString();
                    oEM.STATUS = "1";
                    oEM.DEL_STATUS = "0";
                    oEM.TDATE = System.DateTime.Now;
                    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                    oEM.TUSER = oUserLoginDetail.UserCode;
                    bResult = SaitexBL.Interface.Method.EmployeeMaster.InsertEmployeeReportTo(oEM, out iRecordFound);
                }
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    private void UpdateEmployeeReportTo()
    {
        try
        {
            int iRecordFound = 0;
            SaitexDM.Common.DataModel.EmployeeMaster oEM = new SaitexDM.Common.DataModel.EmployeeMaster();
            if (LstReportTo1.SelectedValue != "" && LstReportTo1.SelectedValue != null)
            {
                bool Rs = SaitexBL.Interface.Method.EmployeeMaster.DeleteReportTo(txtEmployeeCode.Text.Trim().ToString());
                if (Rs)
                {
                    bool bResult = false;
                    foreach (ListItem ln in LstReportTo1.Items)
                    {
                        if (ln.Selected == true)
                        {
                            oEM.EMP_CODE = CommonFuction.funFixQuotes(txtEmployeeCode.Text.Trim());
                            oEM.DESIG_CODE = ln.Value.ToString();
                            oEM.STATUS = "1";
                            oEM.DEL_STATUS = "0";
                            oEM.TDATE = System.DateTime.Now;
                            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                            oEM.TUSER = oUserLoginDetail.UserCode;
                            bResult = SaitexBL.Interface.Method.EmployeeMaster.UpdateEmployeeReportTo(oEM, out iRecordFound);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }

    private void getEmployeeMasterData(string EMP_CODE)
    {

        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getEmployeeMasterData(EMP_CODE);
            if (dt.Rows.Count > 0)
            {
                if (tdEmpInfo.Visible == false)
                {
                    tdEmpInfo.Visible = true;
                }
            }
            txtEmployeeCode.Text = dt.Rows[0]["EMP_CODE"].ToString().Trim();
            txtEmployeeCode.Enabled = false;
            ddlCadderCode.SelectedValue = dt.Rows[0]["CADDER_CODE"].ToString().Trim();
            txtCardNo.Text = dt.Rows[0]["CARD_NO"].ToString().Trim();
            txtUserName.Text = dt.Rows[0]["USER_NAME"].ToString().Trim();
            txtPassword.Attributes.Add("value", dt.Rows[0]["PWD"].ToString().Trim());
            txtFirstName.Text = dt.Rows[0]["F_NAME"].ToString().Trim();
            txtMiddleName.Text = dt.Rows[0]["M_NAME"].ToString().Trim();
            txtLastName.Text = dt.Rows[0]["L_NAME"].ToString().Trim();
            txtEmailId.Text = dt.Rows[0]["EMAIL_ID"].ToString().Trim();
            radGender.Text = dt.Rows[0]["GENDER"].ToString().Trim();
            DDLNationality.SelectedValue = dt.Rows[0]["NATION"].ToString().Trim();
            DDLReligion.SelectedValue = dt.Rows[0]["RELIGION"].ToString().Trim();
            ddlsalutation.SelectedValue = dt.Rows[0]["SALUTATION"].ToString().Trim();
            txtDear.Text = dt.Rows[0]["DEAR"].ToString().Trim();

            string strDOB = string.Empty;
            strDOB = dt.Rows[0]["DOB"].ToString().Trim();

            txtDateOfBirth.Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(strDOB).ToString("dd/MM/yyyy"));

            txtFatherName.Text = dt.Rows[0]["F_H_NAME"].ToString().Trim();
            DDLRelation.Text = dt.Rows[0]["RELATIONSHIP"].ToString().Trim();
            DDLMarital_Status.Text = dt.Rows[0]["MRTL_STATUS"].ToString().Trim();
            ddlGrade.Text = dt.Rows[0]["GRADE_ID"].ToString().Trim();
            ddlShiftName.Text = dt.Rows[0]["SFT_ID"].ToString().Trim();
            string strJOD = string.Empty;
            strJOD = dt.Rows[0]["JOIN_DT"].ToString().Trim();
            txtJoiningDate.Text = String.Format("{0:dd/MM/yyyy}",Convert.ToDateTime(strJOD).ToString("dd/MM/yyyy"));

            DDLStatus.SelectedIndex = DDLStatus.Items.IndexOf(DDLStatus.Items.FindByValue(dt.Rows[0]["STATUS"].ToString().Trim()));

            if (DDLStatus.SelectedValue != "A")
            {
                TrEmpStatus.Visible = true;
            }
            TxtTerminationDate.Text = dt.Rows[0]["TERMINATION_DATE"].ToString().Trim();
            TxtLeavingDate.Text = dt.Rows[0]["LEAVING_DATE"].ToString().Trim();
            TxtSuspendingDate.Text = dt.Rows[0]["SUSPENDING_DATE"].ToString().Trim();

            IMG_USER.Src = "~/ImageHandler.ashx?EMP_CODE=" + dt.Rows[0]["EMP_CODE"].ToString().Trim();
            ddlBranchName.SelectedValue = dt.Rows[0]["BRANCH_CODE"].ToString().Trim();
            ddlDepartment.SelectedValue = dt.Rows[0]["DEPT_CODE"].ToString().Trim();
            txtSkill.Text = dt.Rows[0]["SKILL"].ToString().Trim();
            txtConfirmation.Text = dt.Rows[0]["CONF"].ToString().Trim();
            txtLastIncrement.Text = dt.Rows[0]["LAST_INC"].ToString().Trim();
            txtLastPromotion.Text = dt.Rows[0]["LAST_PROMO"].ToString().Trim();
            ddlDesignation.SelectedValue = dt.Rows[0]["DESIG_CODE"].ToString().Trim();

            DDLLevel.SelectedValue = dt.Rows[0]["EMPLEVEL"].ToString().Trim();
            DDLPosition.SelectedValue = dt.Rows[0]["POSITION"].ToString().Trim();

            DDLEMP_Type.Text = dt.Rows[0]["EMP_TYPE"].ToString().Trim();
            DDLPayment_Mode.Text = dt.Rows[0]["PAY_MODE"].ToString().Trim();

            ddlWeeeklyOff.SelectedValue = dt.Rows[0]["WEEK_OFF"].ToString().Trim();
            txtMarriageDate.Text = dt.Rows[0]["MARRIAGE_DATE"].ToString().Trim();
            DDLIsPF.SelectedValue = dt.Rows[0]["ISPF"].ToString().Trim();
            DDLIsESI.SelectedValue = dt.Rows[0]["ISESI"].ToString().Trim();
            

            dt.Dispose();
            bindlstReportTo(DDLPosition.SelectedValue, txtEmployeeCode.Text.Trim());

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }

    }
    protected void ddlEmployee_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = new DataTable();
            data = GetItems(e.Text, e.ItemsOffset, 10);
            ddlEmployee.Items.Clear();
            ddlEmployee.DataSource = data;
            ddlEmployee.DataBind();
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    {
        try
        {
            string sPO = "";
            string commandText = "SELECT * FROM (SELECT EMP_CODE, F_NAME, F_NAME || ' ' || M_NAME || ' ' || L_NAME AS EMPLOYEENAME,COMP_CODE, DEL_STATUS,STATUS FROM HR_EMP_MST WHERE UPPER(EMP_CODE) LIKE :SearchQuery OR UPPER(F_NAME) LIKE :SearchQuery ORDER BY EMP_CODE) WHERE COMP_CODE = :COMP_CODE AND DEL_STATUS = '0'  AND ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause = " and emp_code not in(SELECT emp_code FROM (SELECT EMP_CODE, F_NAME, F_NAME || ' ' || M_NAME || ' ' || L_NAME AS EMPLOYEENAME,COMP_CODE, DEL_STATUS,STATUS  FROM HR_EMP_MST WHERE UPPER(EMP_CODE) LIKE :SearchQuery OR UPPER(F_NAME) LIKE :SearchQuery ORDER BY EMP_CODE) WHERE COMP_CODE = :COMP_CODE AND DEL_STATUS = '0' AND ROWNUM <= '" + startOffset + "')";
            }
            string SortExpression = " ORDER BY EMP_CODE";
            DataTable dt = SaitexBL.Interface.Method.HR_EMP_COMP_INFO.GetDataForLOV(commandText, whereClause, SortExpression, "", text.ToUpper() + '%', strCompanyCode, string.Empty, sPO);
            return dt;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    protected int GetItemsCount(string text)
    {
        try
        {
            string CommandText = "SELECT count(*) FROM (SELECT EMP_CODE, F_NAME, F_NAME || ' ' || M_NAME || ' ' || L_NAME AS EMPLOYEENAME,COMP_CODE, DEL_STATUS,STATUS FROM HR_EMP_MST WHERE EMP_CODE LIKE :SearchQuery OR F_NAME LIKE :SearchQuery ORDER BY EMP_CODE) WHERE COMP_CODE = :COMP_CODE  AND DEL_STATUS = '0' ";
            return SaitexBL.Interface.Method.HR_EMP_COMP_INFO.GetCountForLOV(CommandText, text.ToUpper() + '%', strCompanyCode, string.Empty, "");
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    protected void ddlEmployee_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            getEmployeeMasterData(ddlEmployee.SelectedValue.Trim());
            ViewState["EMP_CODE"] = ddlEmployee.SelectedValue.Trim();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }

    }

    protected void DDLPosition_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DDLPosition.SelectedValue.Trim() != "")
            {
                getSrPosition(DDLPosition.SelectedValue.Trim());
            }
            else
            {
                LstReportTo1.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    private void getSrPosition(string Position_CODE)
    {
        try
        {
            LstReportTo1.Items.Clear();
            DataTable DTable = new DataTable();
            DTable = SaitexBL.Interface.Method.HR_POSITION_MST.getEmployeeSrPosition(Position_CODE.Trim());
            if (DTable.Rows.Count > 0)
            {
                LstReportTo1.DataSource = DTable;
                LstReportTo1.DataTextField = "POSITION_NAME";
                LstReportTo1.DataValueField = "POSITION_CODE";
                LstReportTo1.DataBind();
            }

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    protected void DDLStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DDLStatus.SelectedValue == "T")
            {
                TrEmpStatus.Visible = true;
                TxtTerminationDate.Enabled = true;
                TxtLeavingDate.Enabled = true;
                TxtSuspendingDate.Enabled = false;
            }
            else if (DDLStatus.SelectedValue == "S")
            {
                TrEmpStatus.Visible = true;
                TxtTerminationDate.Enabled = false;
                TxtLeavingDate.Enabled = true;
                TxtSuspendingDate.Enabled = true;
            }
            else if (DDLStatus.SelectedValue == "L")
            {
                TrEmpStatus.Visible = true;
                TxtTerminationDate.Enabled = true;
                TxtLeavingDate.Enabled = true;
                TxtSuspendingDate.Enabled = false;
            }
            else if (DDLStatus.SelectedValue == "A")
            {
                TrEmpStatus.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message));
        }
    }
}