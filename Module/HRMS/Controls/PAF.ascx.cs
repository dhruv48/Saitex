using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using errorLog;
using Common;
using DBLibrary;
using System.IO;

public partial class Module_HRMS_Controls_PAF : System.Web.UI.UserControl
{
    private DataTable dtPAF;
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void InitialisePage()
    {
        try
        {
            lblMode.Text = "You are in Save Mode";
            tdSave.Visible = true;
            tdDelete.Visible = false;
            tdUpdate.Visible = false;
            txtEmpCode.Text = oUserLoginDetail.UserCode;
            txtEmpName.Text = oUserLoginDetail.Username;
            //cmbEmpSelect.Visible = false;
            //cmbEmpCode.Visible = true;

            ClearControls();
            ControlAccess();
            BindKRANumber();
            RefreshGrid();
        }
        catch
        {
            throw;
        }
    }

    private void ControlAccess()
    {
        try
        {
            txtFunction.Enabled = false;
            txtSubFunction.Enabled = false;
            txtAppraiser.Enabled = false;
            txtReviewer.Enabled = false;
            txtHOD.Enabled = false;
            txtApprAchievement.Enabled = false;
            txtApprWeightage.Enabled = false;
            txtApprRatingPoint.Enabled = false;
            txtApprWR.Enabled = false;

            DataTable dt = SaitexBL.Interface.Method.HR_EMP_MST.Employee_Info_ByCode(txtEmpCode.Text.Trim());
            if (dt != null && dt.Rows.Count > 0)
            {
                txtDepartment.Text = dt.Rows[0]["DEPT_NAME"].ToString();
                txtDesignation.Text = dt.Rows[0]["DESIG_NAME"].ToString();
                txtGrade.Text = dt.Rows[0]["GRADE"].ToString();
                txtAssessmentYr.Text = oUserLoginDetail.DT_STARTDATE.Year.ToString();
                txtBranch.Text = dt.Rows[0]["BRANCH_NAME"].ToString();
            }
            else
            {
                CommonFuction.ShowMessage("There is no Employee Info found in Database..");
            }
        }
        catch
        {
            throw;
        }
    }

    private void BindKRANumber()
    {
        try
        {
            cmbKRANumber.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("KRA_NO", oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                cmbKRANumber.DataSource = dt;
                cmbKRANumber.DataTextField = "MST_CODE";
                cmbKRANumber.DataValueField = "MST_CODE";
                cmbKRANumber.DataBind();
            }
            else
            {
                CommonFuction.ShowMessage("There is no KRA found in Database..");
            }
        }
        catch
        {
            throw;
        }
    }

    private void ClearControls()
    {
        try
        {
            //cmbEmpCode.SelectedIndex = -1;
            txtDepartment.Text = string.Empty;
            txtDesignation.Text = string.Empty;
            txtGrade.Text = string.Empty;
            txtAssessmentYr.Text = string.Empty;
            txtBranch.Text = string.Empty;
            ddlAppraisalType.SelectedIndex = 0;
            txtFunction.Text = string.Empty;
            txtSubFunction.Text = string.Empty;
            txtAppraiser.Text = string.Empty;
            txtReviewer.Text = string.Empty;
            txtHOD.Text = string.Empty;
        }
        catch
        {
            throw;
        }
    }

    private void RefreshGrid()
    {
        try
        {
            cmbKRANumber.SelectedIndex = -1;
            txtEmpAchievement.Text = string.Empty;
            txtEmpWeightage.Text = string.Empty;
            txtEmpRatingPoint.Text = string.Empty;
            txtEmpWR.Text = string.Empty;
            txtApprAchievement.Text = string.Empty;
            txtApprWeightage.Text = string.Empty;
            txtApprRatingPoint.Text = string.Empty;
            txtApprWR.Text = string.Empty;
            ViewState["UNIQUE_ID"] = null;
        }
        catch
        {
            throw;
        }
    }

    private void BindEmployeeDTL()
    {
        try
        {
            cmbKRANumber.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("KRA_NO", oUserLoginDetail.COMP_CODE);
            cmbKRANumber.DataSource = dt;
            cmbKRANumber.DataTextField = "MST_CODE";
            cmbKRANumber.DataValueField = "MST_CODE";
            cmbKRANumber.DataBind();
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
            InsertData();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving the Data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void InsertData()
    {
        try
        {
            if (ViewState["dtPAF"] != null)
                dtPAF = (DataTable)ViewState["dtPAF"];

            if (dtPAF.Rows.Count > 0)
            {
                int iCount = 0;
                iCount = SaitexBL.Interface.Method.HR_PAF_MST.Insert(dtPAF);
                if (iCount > 0)
                {
                    CommonFuction.ShowMessage("Record Saved Successfully!");
                    InitialisePage();

                    if (ViewState["dtPAF"] != null)
                    {
                        dtPAF = (DataTable)ViewState["dtPAF"];
                        dtPAF.Clear();
                        ViewState["dtPAF"] = dtPAF;
                        grdAppraisal.DataSource = dtPAF;
                        grdAppraisal.DataBind();
                    }
                }
                else
                {
                    CommonFuction.ShowMessage("Error in Saving..");
                }
            }
            else
            {
                CommonFuction.ShowMessage("Please enter Performance Appraisal Form..");
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
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            lblMode.Text = "You are in Update Mode";
            //cmbEmpSelect.Visible = true;
            //cmbEmpCode.Visible = false;
            //BindPAFEmployee();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in finding the data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating the Data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in deletion.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./PAF.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clearing the data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //Response.Redirect("./JournalVoucherReport.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing the data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in help.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
                Response.Redirect("~/Module/Admin/Pages/welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    //protected void cmbEmpCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    //{
    //    try
    //    {
    //        DataTable dt = SaitexBL.Interface.Method.HR_EMP_MST.Employee_Info_ByCode(cmbEmpCode.SelectedValue.ToString().Trim());
    //        if (dt != null && dt.Rows.Count > 0)
    //        {
    //            txtDepartment.Text = dt.Rows[0]["DEPT_NAME"].ToString();
    //            txtDesignation.Text = dt.Rows[0]["DESIG_NAME"].ToString();
    //            txtGrade.Text = dt.Rows[0]["GRADE"].ToString();
    //            txtAssessmentYr.Text = oUserLoginDetail.DT_STARTDATE.Year.ToString();
    //            txtBranch.Text = dt.Rows[0]["BRANCH_NAME"].ToString();
    //        }
    //        else
    //        {
    //            CommonFuction.ShowMessage("There is no Employee Info found in Database..");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Employee Code.\r\nSee error log for detail."));
    //        lblMode.Text = ex.ToString();
    //    }
    //}

    //protected void cmbEmpCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    //{
    //    try
    //    {
    //        DataTable data = new DataTable();
    //        data = GetItems(e.Text, e.ItemsOffset, 10);
    //        cmbEmpCode.Items.Clear();
    //        cmbEmpCode.DataSource = data;
    //        cmbEmpCode.DataBind();
    //        // Calculating the numbr of items loaded so far in the ComboBox
    //        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
    //        // Getting the total number of items that start with the typed text
    //        e.ItemsCount = GetItemsCount(e.Text);
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading Employee Code.\r\nSee error log for detail."));
    //        lblMode.Text = ex.ToString();
    //    }
    //}

    //protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    //{
    //    try
    //    {
    //        string sPO = "";

    //        string commandText = "SELECT * FROM (SELECT EMP_CODE, F_NAME, F_NAME || ' ' || M_NAME || ' ' || L_NAME AS EMPLOYEENAME,COMP_CODE, DEL_STATUS FROM HR_EMP_MST WHERE UPPER(EMP_CODE) LIKE :SearchQuery OR UPPER(F_NAME) LIKE :SearchQuery ORDER BY EMP_CODE) WHERE COMP_CODE = :COMP_CODE AND DEL_STATUS = '0' AND ROWNUM <= 15 ";
    //        string whereClause = string.Empty;
    //        if (startOffset != 0)
    //        {
    //            whereClause = " and emp_code not in(SELECT emp_code FROM (SELECT EMP_CODE, F_NAME, F_NAME || ' ' || M_NAME || ' ' || L_NAME AS EMPLOYEENAME,COMP_CODE, DEL_STATUS FROM HR_EMP_MST WHERE UPPER(EMP_CODE) LIKE :SearchQuery OR UPPER(F_NAME) LIKE :SearchQuery ORDER BY EMP_CODE) WHERE COMP_CODE = :COMP_CODE AND DEL_STATUS = '0' AND ROWNUM <= '" + startOffset + "')";
    //        }
    //        string SortExpression = " ORDER BY EMP_CODE";
    //        DataTable dt = SaitexBL.Interface.Method.HR_EMP_COMP_INFO.GetDataForLOV(commandText, whereClause, SortExpression, "", text.ToUpper() + '%', oUserLoginDetail.COMP_CODE, string.Empty, sPO);
    //        return dt;
    //    }
    //    catch (Exception ex)
    //    {
    //        errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
    //        throw;
    //    }
    //}

    //protected int GetItemsCount(string text)
    //{
    //    try
    //    {
    //        string CommandText = "SELECT count(*) FROM (SELECT EMP_CODE, F_NAME, F_NAME || ' ' || M_NAME || ' ' || L_NAME AS EMPLOYEENAME,COMP_CODE, DEL_STATUS FROM HR_EMP_MST WHERE EMP_CODE LIKE :SearchQuery OR F_NAME LIKE :SearchQuery ORDER BY EMP_CODE) WHERE COMP_CODE = :COMP_CODE AND DEL_STATUS = '0' ";
    //        return SaitexBL.Interface.Method.HR_EMP_COMP_INFO.GetCountForLOV(CommandText, text.ToUpper() + '%', oUserLoginDetail.COMP_CODE, string.Empty, "");
    //    }
    //    catch (Exception ex)
    //    {
    //        errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
    //        throw;
    //    }
    //}

    protected void cmbKRANumber_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            BindKRANumber();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading KRA Number.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }

    private void CreateDataTable()
    {
        try
        {
            dtPAF = new DataTable();
            dtPAF.Columns.Add("UNIQUE_ID", typeof(int));
            dtPAF.Columns.Add("EMP_CODE", typeof(string));
            dtPAF.Columns.Add("APPRAISAL_TYPE", typeof(string));
            dtPAF.Columns.Add("ASSESSMENT_YEAR", typeof(int));
            dtPAF.Columns.Add("APP_FUNCTION", typeof(string));
            dtPAF.Columns.Add("APP_SUB_FUNCTION", typeof(string));
            dtPAF.Columns.Add("APPRAISER", typeof(string));
            dtPAF.Columns.Add("REVIEWER", typeof(string));
            dtPAF.Columns.Add("HOD", typeof(string));
            dtPAF.Columns.Add("KRA_NO", typeof(string));
            dtPAF.Columns.Add("EMP_ACHIEVEMENT", typeof(double));
            dtPAF.Columns.Add("EMP_WEIGHTAGE", typeof(double));
            dtPAF.Columns.Add("EMP_RATING_POINT", typeof(double));
            dtPAF.Columns.Add("EMP_WP", typeof(double));
            dtPAF.Columns.Add("APPR_ACHIEVEMENT", typeof(double));
            dtPAF.Columns.Add("APPR_WEIGHTAGE", typeof(double));
            dtPAF.Columns.Add("APPR_RATING_POINT", typeof(double));
            dtPAF.Columns.Add("APPR_WP", typeof(double));
            dtPAF.Columns.Add("DEL_STATUS", typeof(string));
            dtPAF.Columns.Add("TDATE", typeof(DateTime));
            dtPAF.Columns.Add("TUSER", typeof(string));
            dtPAF.Columns.Add("IS_APPROVED", typeof(string));
            dtPAF.Columns.Add("COMP_CODE", typeof(string));
            dtPAF.Columns.Add("REPORT_TO", typeof(string));
        }
        catch
        {
            throw;
        }
    }

    protected void btnSaveDetail_Click(object sender, EventArgs e)
    {
        try
        {
            if (CheckValidation())
            {
                if (ViewState["dtPAF"] != null)
                    dtPAF = (DataTable)ViewState["dtPAF"];

                if (dtPAF == null)
                    CreateDataTable();

                DataRow dr = dtPAF.NewRow();
                dr["UNIQUE_ID"] = dtPAF.Rows.Count + 1;
                //dr["EMP_CODE"] = cmbEmpCode.SelectedValue.ToString().Trim();
                dr["EMP_CODE"] = txtEmpCode.Text.Trim();
                dr["APPRAISAL_TYPE"] = ddlAppraisalType.SelectedValue.ToString().Trim();
                dr["ASSESSMENT_YEAR"] = Convert.ToInt32(txtAssessmentYr.Text.Trim());
                dr["APP_FUNCTION"] = txtFunction.Text.Trim();
                dr["APP_SUB_FUNCTION"] = txtSubFunction.Text.Trim();
                dr["APPRAISER"] = txtAppraiser.Text.Trim();
                dr["REVIEWER"] = txtReviewer.Text.Trim();
                dr["HOD"] = txtHOD.Text.Trim();
                dr["KRA_NO"] = cmbKRANumber.SelectedValue.ToString().Trim();
                dr["EMP_ACHIEVEMENT"] = txtEmpAchievement.Text.Trim();
                dr["EMP_WEIGHTAGE"] = txtEmpWeightage.Text.Trim();
                dr["EMP_RATING_POINT"] = txtEmpRatingPoint.Text.Trim();
                dr["EMP_WP"] = txtEmpWR.Text.Trim();
                dr["DEL_STATUS"] = "0";
                dr["TDATE"] = System.DateTime.Now;
                dr["TUSER"] = oUserLoginDetail.UserCode;
                dr["IS_APPROVED"] = "0";
                dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;

                if (Session["ReportTo"] != null)
                {
                    dr["REPORT_TO"] = Session["ReportTo"].ToString();
                }
                {
                    dr["REPORT_TO"] = "NOT FOUND";
                }
                dtPAF.Rows.Add(dr);

                ViewState["dtPAF"] = dtPAF;
                BindGridFromTable();
                RefreshGrid();
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please.. Enter Some Mendatory Fields Followed By *");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private bool CheckValidation()
    {
        try
        {
            bool IsValidation = false;
            //if (cmbEmpCode.SelectedIndex > -1)
            //{
            if (txtAssessmentYr.Text != string.Empty)
            {
                if (cmbKRANumber.SelectedIndex > -1)
                {
                    if (txtEmpAchievement.Text != string.Empty)
                    {
                        if (txtEmpWeightage.Text != string.Empty)
                        {
                            if (txtEmpRatingPoint.Text != string.Empty)
                            {
                                if (txtEmpWR.Text != string.Empty)
                                {
                                    IsValidation = true;
                                }
                                else
                                {
                                    Common.CommonFuction.ShowMessage("Please.. enter Rating Point.");
                                }
                            }
                            else
                            {
                                Common.CommonFuction.ShowMessage("Please.. enter Rating Point.");
                            }
                        }
                        else
                        {
                            Common.CommonFuction.ShowMessage("Please.. enter Weightage.");
                        }
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Please.. enter Achievement.");
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Please.. select KRA Number.");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please.. enter Assessment Year.");
            }
            //}
            //else
            //{
            //    Common.CommonFuction.ShowMessage("Please.. select Employee first.");
            //}
            return IsValidation;
        }
        catch
        {
            return false;
        }
    }

    private void BindGridFromTable()
    {
        try
        {
            if (ViewState["dtPAF"] != null)
                dtPAF = (DataTable)ViewState["dtPAF"];

            grdAppraisal.DataSource = dtPAF;
            grdAppraisal.DataBind();
        }
        catch
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            RefreshGrid();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Cancelling.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void grdAppraisal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int Unique_id = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "EditTRN")
            {
                //EditJournalTRNRow(Unique_id);
            }
            else if (e.CommandName == "DeleteTRN")
            {
                //DeleteJournalTRNRow(Unique_id);
                //BindGridFromTable();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Row Command.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtEmpRatingPoint_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtEmpWeightage.Text != string.Empty)
            {
                if (txtEmpRatingPoint.Text != string.Empty)
                {
                    double dblEmpWeightage = 0;
                    double dblEmpRatingPoint = 0;
                    double.TryParse(txtEmpWeightage.Text.Trim(), out dblEmpWeightage);
                    double.TryParse(txtEmpRatingPoint.Text.Trim(), out dblEmpRatingPoint);
                    txtEmpWR.Text = (dblEmpWeightage * dblEmpRatingPoint).ToString();
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Please.. enter Rating Point.");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please.. enter Weightage first.");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Rating Point Textchanged.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    //protected void cmbEmpSelect_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    //{
    //    try
    //    {

    //    }
    //    catch (Exception ex)
    //    {
    //        CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Employee..\r\nSee error log for detail."));
    //        lblMode.Text = ex.ToString();
    //    }
    //}

    //protected void cmbEmpSelect_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    //{
    //    try
    //    {
    //        BindPAFEmployee();
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading Employee Details.\r\nSee error log for detail."));
    //        lblMode.Text = ex.ToString();
    //    }
    //}

    //private void BindPAFEmployee()
    //{
    //    try
    //    {
    //        DataTable dt = SaitexBL.Interface.Method.HR_PAF_MST.SelectPAFEmployee();
    //        if (dt != null && dt.Rows.Count > 0)
    //        {
    //            cmbEmpSelect.Items.Clear();
    //            cmbEmpSelect.DataSource = dt;
    //            cmbEmpSelect.DataBind();
    //        }
    //        else
    //        {
    //            CommonFuction.ShowMessage("There is no record found in DataBase..");
    //        }
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    protected void cmbKRANumber_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            if (!CheckDuplicateRow(cmbKRANumber.SelectedValue.ToString().Trim()))
            {

            }
            else
            {
                Common.CommonFuction.ShowMessage("KRA already included, Choose another...");
                RefreshGrid();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selecting KRA Number..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private bool CheckDuplicateRow(string strKRA)
    {
        try
        {
            bool IsDuplicate = false;

            if (ViewState["dtPAF"] != null)
                dtPAF = (DataTable)ViewState["dtPAF"];

            if (dtPAF != null)
            {
                DataView dv = new DataView(dtPAF);
                dv.RowFilter = "KRA_NO = '" + strKRA + "'";
                if (dv.Count > 0)
                {
                    IsDuplicate = true;
                }
            }
            return IsDuplicate;
        }
        catch
        {
            throw;
        }
    }
}
