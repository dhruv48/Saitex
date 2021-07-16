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

public partial class Module_HRMS_Controls_PAFAdmin : System.Web.UI.UserControl
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
            //txtEmpCode.Text = oUserLoginDetail.UserCode;
            //txtEmpName.Text = oUserLoginDetail.Username;
            //cmbEmpSelect.Visible = false;
            //cmbEmpCode.Visible = true;

            ClearControls();
            //ControlAccess();
            //BindKRANumber();
            RefreshGrid();
            ddlAppraisalType.Enabled = false;
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
            DataTable dt = SaitexBL.Interface.Method.HR_EMP_MST.Employee_Info_ByCode(cmbEmpSelect.SelectedValue.ToString().Trim());
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

    //private void BindKRANumber()
    //{
    //    try
    //    {
    //        cmbKRANumber.Items.Clear();
    //        DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("KRA_NO", oUserLoginDetail.COMP_CODE);
    //        if (dt != null && dt.Rows.Count > 0)
    //        {
    //            cmbKRANumber.DataSource = dt;
    //            cmbKRANumber.DataTextField = "MST_CODE";
    //            cmbKRANumber.DataValueField = "MST_CODE";
    //            cmbKRANumber.DataBind();
    //        }
    //        else
    //        {
    //            CommonFuction.ShowMessage("There is no KRA found in Database..");
    //        }
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

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
            //cmbKRANumber.SelectedIndex = -1;
            //txtEmpAchievement.Text = string.Empty;
            //txtEmpWeightage.Text = string.Empty;
            //txtEmpRatingPoint.Text = string.Empty;
            //txtEmpWR.Text = string.Empty;
            //txtApprAchievement.Text = string.Empty;
            //txtApprWeightage.Text = string.Empty;
            //txtApprRatingPoint.Text = string.Empty;
            //txtApprWR.Text = string.Empty;
            ViewState["UNIQUE_ID"] = null;
        }
        catch
        {
            throw;
        }
    }

    //private void BindEmployeeDTL()
    //{
    //    try
    //    {
    //        cmbKRANumber.Items.Clear();
    //        DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("KRA_NO", oUserLoginDetail.COMP_CODE);
    //        cmbKRANumber.DataSource = dt;
    //        cmbKRANumber.DataTextField = "MST_CODE";
    //        cmbKRANumber.DataValueField = "MST_CODE";
    //        cmbKRANumber.DataBind();
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

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
            foreach (GridViewRow thisGridViewRow in grdAppraisal.Rows)
            {
                Label lblUNIQUE_ID = (Label)thisGridViewRow.FindControl("lblUNIQUE_ID");
                TextBox lblApprAchievement = (TextBox)thisGridViewRow.FindControl("lblApprAchievement");
                TextBox lblApprWeightage = (TextBox)thisGridViewRow.FindControl("lblApprWeightage");
                TextBox lblApprRatingPoint = (TextBox)thisGridViewRow.FindControl("lblApprRatingPoint");
                TextBox lblApprWP = (TextBox)thisGridViewRow.FindControl("lblApprWP");
                string lblUNIQUE_ID1 = lblUNIQUE_ID.Text.Trim();
                string lblApprAchievement1 = lblApprAchievement.Text.Trim();
                string lblApprWeightage1 = lblApprWeightage.Text.Trim();
                string lblApprRatingPoint1 = lblApprRatingPoint.Text.Trim();
                string lblApprWP1 = lblApprWP.Text.Trim();

                UpdateDataTable(lblUNIQUE_ID1, lblApprAchievement1, lblApprWeightage1, lblApprRatingPoint1, lblApprWP1);
            }

            if (ViewState["dtPAF"] != null)
                dtPAF = (DataTable)ViewState["dtPAF"];

            if (dtPAF.Rows.Count > 0)
            {
                int iCount = 0;
                iCount = SaitexBL.Interface.Method.HR_PAF_MST.InsertPAFAdmin(dtPAF);
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
                    cmbEmpSelect.SelectedIndex = -1;
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

    private void UpdateDataTable(string lblUNIQUE_ID, string lblApprAchievement, string lblApprWeightage, string lblApprRatingPoint, string lblApprWP)
    {
        try
        {
            if (ViewState["dtPAF"] != null)
                dtPAF = (DataTable)ViewState["dtPAF"];

            DataView dvEdit = new DataView(dtPAF);
            dvEdit.RowFilter = "UNIQUE_ID = '" + lblUNIQUE_ID + "'";
            if (dvEdit.Count > 0)
            {
                dvEdit[0]["APP_FUNCTION"] = txtFunction.Text.Trim();
                dvEdit[0]["APP_SUB_FUNCTION"] = txtSubFunction.Text.Trim();
                dvEdit[0]["APPRAISER"] = txtAppraiser.Text.Trim();
                dvEdit[0]["REVIEWER"] = txtReviewer.Text.Trim();
                dvEdit[0]["HOD"] = txtHOD.Text.Trim();
                dvEdit[0]["APPR_ACHIEVEMENT"] = double.Parse(lblApprAchievement.Trim());
                dvEdit[0]["APPR_WEIGHTAGE"] = double.Parse(lblApprWeightage.Trim());
                dvEdit[0]["APPR_RATING_POINT"] = double.Parse(lblApprRatingPoint.Trim());
                dvEdit[0]["APPR_WP"] = double.Parse(lblApprWP.Trim());
                dvEdit[0]["IS_APPROVED"] = "1";
                dvEdit[0]["TUSER"] = oUserLoginDetail.UserCode;
                dtPAF.AcceptChanges();
            }
            ViewState["dtPAF"] = dtPAF;
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
            Response.Redirect("./PAFAdmin.aspx", false);
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

    //protected void cmbKRANumber_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    //{
    //    try
    //    {
    //        BindKRANumber();
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading KRA Number.\r\nSee error log for detail."));
    //        lblMode.Text = ex.ToString();
    //    }
    //}

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

    //protected void btnSaveDetail_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (CheckValidation())
    //        {
    //            if (ViewState["dtPAF"] != null)
    //                dtPAF = (DataTable)ViewState["dtPAF"];

    //            if (dtPAF == null)
    //                CreateDataTable();

    //            DataRow dr = dtPAF.NewRow();
    //            dr["UNIQUE_ID"] = dtPAF.Rows.Count + 1;
    //            dr["EMP_CODE"] = cmbEmpSelect.SelectedValue.ToString().Trim();
    //            //dr["EMP_CODE"] = txtEmpCode.Text.Trim();
    //            dr["APPRAISAL_TYPE"] = ddlAppraisalType.SelectedValue.ToString().Trim();
    //            dr["ASSESSMENT_YEAR"] = Convert.ToInt32(txtAssessmentYr.Text.Trim());
    //            dr["APP_FUNCTION"] = txtFunction.Text.Trim();
    //            dr["APP_SUB_FUNCTION"] = txtSubFunction.Text.Trim();
    //            dr["APPRAISER"] = txtAppraiser.Text.Trim();
    //            dr["REVIEWER"] = txtReviewer.Text.Trim();
    //            dr["HOD"] = txtHOD.Text.Trim();
    //            dr["KRA_NO"] = cmbKRANumber.SelectedValue.ToString().Trim();
    //            dr["EMP_ACHIEVEMENT"] = txtEmpAchievement.Text.Trim();
    //            dr["EMP_WEIGHTAGE"] = txtEmpWeightage.Text.Trim();
    //            dr["EMP_RATING_POINT"] = txtEmpRatingPoint.Text.Trim();
    //            dr["EMP_WP"] = txtEmpWR.Text.Trim();
    //            dr["DEL_STATUS"] = "0";
    //            dr["TDATE"] = System.DateTime.Now;
    //            dr["TUSER"] = oUserLoginDetail.UserCode;
    //            dr["IS_APPROVED"] = "0";
    //            dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;

    //            if (Session["ReportTo"] != null)
    //            {
    //                dr["REPORT_TO"] = Session["ReportTo"].ToString();
    //            }
    //            {
    //                dr["REPORT_TO"] = "NOT FOUND";
    //            }
    //            dtPAF.Rows.Add(dr);

    //            ViewState["dtPAF"] = dtPAF;
    //            BindGridFromTable();
    //            RefreshGrid();
    //        }
    //        else
    //        {
    //            Common.CommonFuction.ShowMessage("Please.. Enter Some Mendatory Fields Followed By *");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving.\r\nSee error log for detail."));
    //        lblMode.Text = ex.ToString();
    //    }
    //}

    //private bool CheckValidation()
    //{
    //    try
    //    {
    //        bool IsValidation = false;
    //        //if (cmbEmpCode.SelectedIndex > -1)
    //        //{
    //        if (txtAssessmentYr.Text != string.Empty)
    //        {
    //            if (cmbKRANumber.SelectedIndex > -1)
    //            {
    //                if (txtEmpAchievement.Text != string.Empty)
    //                {
    //                    if (txtEmpWeightage.Text != string.Empty)
    //                    {
    //                        if (txtEmpRatingPoint.Text != string.Empty)
    //                        {
    //                            if (txtEmpWR.Text != string.Empty)
    //                            {
    //                                IsValidation = true;
    //                            }
    //                            else
    //                            {
    //                                Common.CommonFuction.ShowMessage("Please.. enter Rating Point.");
    //                            }
    //                        }
    //                        else
    //                        {
    //                            Common.CommonFuction.ShowMessage("Please.. enter Rating Point.");
    //                        }
    //                    }
    //                    else
    //                    {
    //                        Common.CommonFuction.ShowMessage("Please.. enter Weightage.");
    //                    }
    //                }
    //                else
    //                {
    //                    Common.CommonFuction.ShowMessage("Please.. enter Achievement.");
    //                }
    //            }
    //            else
    //            {
    //                Common.CommonFuction.ShowMessage("Please.. select KRA Number.");
    //            }
    //        }
    //        else
    //        {
    //            Common.CommonFuction.ShowMessage("Please.. enter Assessment Year.");
    //        }
    //        //}
    //        //else
    //        //{
    //        //    Common.CommonFuction.ShowMessage("Please.. select Employee first.");
    //        //}
    //        return IsValidation;
    //    }
    //    catch
    //    {
    //        return false;
    //    }
    //}

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

    //protected void btnCancel_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        RefreshGrid();
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Cancelling.\r\nSee error log for detail."));
    //        lblMode.Text = ex.ToString();
    //    }
    //}

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

    //protected void txtEmpRatingPoint_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (txtEmpWeightage.Text != string.Empty)
    //        {
    //            if (txtEmpRatingPoint.Text != string.Empty)
    //            {
    //                double dblEmpWeightage = 0;
    //                double dblEmpRatingPoint = 0;
    //                double.TryParse(txtEmpWeightage.Text.Trim(), out dblEmpWeightage);
    //                double.TryParse(txtEmpRatingPoint.Text.Trim(), out dblEmpRatingPoint);
    //                txtEmpWR.Text = (dblEmpWeightage * dblEmpRatingPoint).ToString();
    //            }
    //            else
    //            {
    //                Common.CommonFuction.ShowMessage("Please.. enter Rating Point.");
    //            }
    //        }
    //        else
    //        {
    //            Common.CommonFuction.ShowMessage("Please.. enter Weightage first.");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Rating Point Textchanged.\r\nSee error log for detail."));
    //        lblMode.Text = ex.ToString();
    //    }
    //}

    protected void cmbEmpSelect_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            if (ViewState["dtPAF"] != null)
            {
                dtPAF = (DataTable)ViewState["dtPAF"];
                dtPAF.Clear();
            }

            if (dtPAF == null)
                CreateDataTable();

            DataTable dt = SaitexBL.Interface.Method.HR_PAF_MST.SelectPAFEmployee();
            if (dt != null && dt.Rows.Count > 0)
            {
                string strEmp = dt.Rows[0]["EMP_CODE"].ToString();
                DataTable dt1 = SaitexBL.Interface.Method.HR_EMP_MST.Employee_Info_ByCode(strEmp);
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    txtDepartment.Text = dt1.Rows[0]["DEPT_NAME"].ToString();
                    txtDesignation.Text = dt1.Rows[0]["DESIG_NAME"].ToString();
                    txtGrade.Text = dt1.Rows[0]["GRADE"].ToString();
                    txtAssessmentYr.Text = oUserLoginDetail.DT_STARTDATE.Year.ToString();
                    txtBranch.Text = dt1.Rows[0]["BRANCH_NAME"].ToString();
                }
                else
                {
                    CommonFuction.ShowMessage("There is no Detail found against this employee..");
                }

                DataTable dt2 = SaitexBL.Interface.Method.HR_PAF_MST.SelectPAFEmployeeByCode(strEmp);
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    DataView dv = new DataView(dt2);
                    if (dv.Count > 0)
                    {
                        for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                        {
                            DataRow dr = dtPAF.NewRow();
                            dr["UNIQUE_ID"] = dtPAF.Rows.Count + 1;
                            dr["EMP_CODE"] = dv[iLoop]["EMP_CODE"].ToString();
                            //dr["EMP_CODE"] = txtEmpCode.Text.Trim();
                            dr["APPRAISAL_TYPE"] = dv[iLoop]["APPRAISAL_TYPE"].ToString();
                            ddlAppraisalType.SelectedIndex = ddlAppraisalType.Items.IndexOf(ddlAppraisalType.Items.FindByValue(dv[iLoop]["APPRAISAL_TYPE"].ToString()));
                            dr["ASSESSMENT_YEAR"] = Convert.ToInt32(dv[iLoop]["ASSESSMENT_YEAR"].ToString());
                            dr["APP_FUNCTION"] = dv[iLoop]["APP_FUNCTION"].ToString();
                            txtFunction.Text = dv[iLoop]["APP_FUNCTION"].ToString();
                            dr["APP_SUB_FUNCTION"] = dv[iLoop]["APP_SUB_FUNCTION"].ToString();
                            txtSubFunction.Text = dv[iLoop]["APP_SUB_FUNCTION"].ToString();
                            dr["APPRAISER"] = dv[iLoop]["APPRAISER"].ToString();
                            txtAppraiser.Text = dv[iLoop]["APPRAISER"].ToString();
                            dr["REVIEWER"] = dv[iLoop]["REVIEWER"].ToString();
                            txtReviewer.Text = dv[iLoop]["REVIEWER"].ToString();
                            dr["HOD"] = dv[iLoop]["HOD"].ToString();
                            txtHOD.Text = dv[iLoop]["HOD"].ToString();
                            dr["KRA_NO"] = dv[iLoop]["KRA_NO"].ToString();
                            dr["EMP_ACHIEVEMENT"] = Convert.ToDouble(dv[iLoop]["EMP_ACHIEVEMENT"].ToString());
                            dr["EMP_WEIGHTAGE"] = Convert.ToDouble(dv[iLoop]["EMP_WEIGHTAGE"].ToString());
                            dr["EMP_RATING_POINT"] = Convert.ToDouble(dv[iLoop]["EMP_RATING_POINT"].ToString());
                            dr["EMP_WP"] = Convert.ToDouble(dv[iLoop]["EMP_WP"].ToString());
                            dr["DEL_STATUS"] = dv[iLoop]["DEL_STATUS"].ToString();
                            dr["TDATE"] = System.DateTime.Now;
                            dr["TUSER"] = dv[iLoop]["TUSER"].ToString();
                            dr["IS_APPROVED"] = dv[iLoop]["IS_APPROVED"].ToString();
                            string strApproved = dv[iLoop]["IS_APPROVED"].ToString();
                            dr["COMP_CODE"] = dv[iLoop]["COMP_CODE"].ToString();
                            if (strApproved == "1")
                            {
                                dr["APPR_ACHIEVEMENT"] = Convert.ToDouble(dv[iLoop]["APPR_ACHIEVEMENT"].ToString());
                                dr["APPR_WEIGHTAGE"] = Convert.ToDouble(dv[iLoop]["APPR_WEIGHTAGE"].ToString());
                                dr["APPR_RATING_POINT"] = Convert.ToDouble(dv[iLoop]["APPR_RATING_POINT"].ToString());
                                dr["APPR_WP"] = Convert.ToDouble(dv[iLoop]["APPR_WP"].ToString());
                            }
                            dtPAF.Rows.Add(dr);

                            ViewState["dtPAF"] = dtPAF;
                        }
                    }
                }
                else
                {
                    CommonFuction.ShowMessage("There is no Detail found against this employee..");
                }
                grdAppraisal.DataSource = dtPAF;
                grdAppraisal.DataBind();
            }
            else
            {
                CommonFuction.ShowMessage("There is no Employee Info found in Database..");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Employee..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void cmbEmpSelect_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            BindPAFEmployee();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading Employee Details.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void BindPAFEmployee()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.HR_PAF_MST.SelectPAFEmployee();
            if (dt != null && dt.Rows.Count > 0)
            {
                //cmbEmpSelect.Items.Clear();
                cmbEmpSelect.DataSource = dt;
                cmbEmpSelect.DataBind();
            }
            else
            {
                CommonFuction.ShowMessage("There is no record found in DataBase..");
            }
        }
        catch
        {
            throw;
        }
    }

    protected void lblApprRatingPoint_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double dblApprWeightage = 0;
            double dblApprRatingPoint = 0;
            double dblApprWP = 0;

            if (ViewState["dtPAF"] != null)
            {
                dtPAF = (DataTable)ViewState["dtPAF"];
            }

            foreach (GridViewRow thisGridViewRow in grdAppraisal.Rows)
            {
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblUNIQUE_ID = (Label)thisGridViewRow.FindControl("lblUNIQUE_ID");
                    TextBox lblApprWeightage = (TextBox)thisGridViewRow.FindControl("lblApprWeightage");
                    TextBox lblApprRatingPoint = (TextBox)thisGridViewRow.FindControl("lblApprRatingPoint");
                    TextBox lblApprWP = (TextBox)thisGridViewRow.FindControl("lblApprWP");
                    double.TryParse(lblApprWeightage.Text.Trim(), out dblApprWeightage);
                    double.TryParse(lblApprRatingPoint.Text.Trim(), out dblApprRatingPoint);
                    dblApprWP = dblApprWeightage * dblApprRatingPoint;
                    lblApprWP.Text = dblApprWP.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Rating Text Changed.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
}
