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
using DBLibrary;
using Common;
using errorLog;
using Obout.ComboBox;
public partial class Module_HRMS_Controls_Employee_Qualification : System.Web.UI.UserControl
{
    private static DataTable DTEmpTable = null;
    private static DataTable DTLangTable = null;
    public static string strCompanyCode = string.Empty;
    public static string strBranchCode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["urLoginId"] != null)
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            strCompanyCode = oUserLoginDetail.COMP_CODE.Trim().ToString();
            strBranchCode = oUserLoginDetail.CH_BRANCHCODE.Trim().ToString();
            if (!Page.IsPostBack)
            {
                InitialisePage();
                if (Request.QueryString["EMP_CODE"] != null && Request.QueryString["EMP_CODE"] != "")
                {
                    string EMP_Code = Request.QueryString["EMP_CODE"].ToString();
                    DDLEmployee.SelectedValue = EMP_Code;
                    getEmployeeQualiData(EMP_Code);
                    getEmployeeLanguageData(EMP_Code);

                }
                else
                {
                    lblMode.Text = "Save";
                }
                bindEmpCode();
            }
        }
        else
        {
            Response.Redirect("/Saitex/Default.aspx", false);
        }

    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnSave.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save this record ? ')");
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");
        imgbtnDelete.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        //imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit')");
        imgbtnHelp.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Help')");
    }
    private void bindEmpCode()
    {
        try
        {
            DataTable data = new DataTable();
            data = GetItems("", 0, 10);
            DDLEmployee .DataSource = data;
            DDLEmployee.DataBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
            throw ex;
        }
    }
    private void InitialisePage()
    {
        try
        {
            lblMode.Text = "Save";
            RefreshDetailRow();
            //-----------------------For Employee Qualification Record-----------------------
            if (DTEmpTable == null || DTEmpTable.Rows.Count == 0)
                CreateEmpQualificationDetailTable();
            DTEmpTable.Rows.Clear();
            //-----------------------For Employee Language Record-----------------------
            if (DTLangTable == null || DTLangTable.Rows.Count == 0)
                CreateEmpLanguageDetailTable();
            DTLangTable.Rows.Clear();
            //-----------------------------------------------------------------
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", "window.alert('" + ex.Message + "');", true);
        }
    }

    private void CreateEmpQualificationDetailTable()
    {
        DTEmpTable = new DataTable();
        DTEmpTable.Columns.Add("UniqueId", typeof(int));
        DTEmpTable.Columns.Add("EMP_QUAL_ID", typeof(int));
        DTEmpTable.Columns.Add("EMP_CODE", typeof(string));
        DTEmpTable.Columns.Add("EXAM", typeof(string));
        DTEmpTable.Columns.Add("SCH_COL", typeof(string));
        DTEmpTable.Columns.Add("PASS_YEAR", typeof(string));
        DTEmpTable.Columns.Add("BOARD_UNIV", typeof(string));
        DTEmpTable.Columns.Add("GRADE", typeof(string));
        DTEmpTable.Columns.Add("PERCENT", typeof(string));


    }
    private void RefreshDetailRow()
    {
        TxtExam.Text = "";
        TxtBoardUniv.Text = "";
        TxtCollege.Text = "";
        TxtPassingYear.Text = "";
        TxtGrade.Text = "";
        TxtPercentage.Text = "";
        ViewState["UniqueId"] = null;
    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        if (Page.IsValid)
        {
            if (DTEmpTable.Rows.Count > 0)
            {
                if (Save_Qualification_Detail())
                {
                    if (DTLangTable.Rows.Count > 0)
                    {
                        bool Res = SaitexBL.Interface.Method.HR_EMP_LANG.Save_Language_Detail(DTLangTable);
                        if (Res)
                        {
                            Common.CommonFuction.ShowMessage("Record Save Sucessfully");
                        }
                        else
                        {
                            Common.CommonFuction.ShowMessage("Unable to save!try agin");
                        }
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Record Save Sucessfully");
                    }

                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("No Items selected. Please enter item detail");
            }
        }
    }
    protected void grdEmpQualiDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "EmpEdit")
            {
                FillDetailByGrid(UniqueId);
            }
            else if (e.CommandName == "EmpDelete")
            {
                DeleteQualificationDetailRow(UniqueId);
                BindDetailGrid();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", "window.alert('" + ex.Message + "');", true);
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    private void BindDetailGrid()
    {
        try
        {
            grdEmpQualiDetail.DataSource = DTEmpTable;
            grdEmpQualiDetail.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void FillDetailByGrid(int UniqueId)
    {
        try
        {
            DataView dv = new DataView(DTEmpTable);
            dv.RowFilter = "UniqueId=" + UniqueId;
            if (dv.Count > 0)
            {
                TxtExam.Text = dv[0]["EXAM"].ToString();
                TxtBoardUniv.Text = dv[0]["BOARD_UNIV"].ToString();
                TxtCollege.Text = dv[0]["SCH_COL"].ToString();
                TxtPassingYear.Text = dv[0]["PASS_YEAR"].ToString();
                TxtGrade.Text = dv[0]["GRADE"].ToString();
                TxtPercentage.Text = dv[0]["PERCENT"].ToString();
                ViewState["UniqueId"] = UniqueId;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void lbtnsavedetail_Click(object sender, EventArgs e)
    {
        try
        {
            if (DDLLanguage.SelectedValue != "" && DDLLanguage.SelectedValue != null)
            {
                if (Can_Bind_Exam())
                {
                    int UniqueId = 0;
                    if (ViewState["UniqueId"] != null)
                        UniqueId = int.Parse(ViewState["UniqueId"].ToString());
                    DataRow dr;
                    dr = DTEmpTable.NewRow();
                    dr["UniqueId"] = DTEmpTable.Rows.Count + 1;
                    dr["EMP_CODE"] = DDLEmployee.SelectedValue.ToString();
                    dr["EXAM"] = TxtExam.Text.Trim();
                    dr["BOARD_UNIV"] = TxtBoardUniv.Text.Trim();
                    dr["SCH_COL"] = TxtCollege.Text.Trim();
                    dr["PASS_YEAR"] = TxtPassingYear.Text;
                    dr["GRADE"] = TxtGrade.Text.Trim();
                    dr["PERCENT"] = TxtPercentage.Text.Trim();
                    DTEmpTable.Rows.Add(dr);
                    RefreshDetailRow();
                }
                BindDetailGrid();
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please Select Employee");
                DDLEmployee.Focus();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }
    private bool Can_Bind_Exam()
    {
        bool Res = true;
        try
        {
            if (TxtExam.Text != "" && TxtBoardUniv.Text != "" && TxtCollege.Text != "" && TxtPassingYear.Text != "" && TxtGrade.Text != "" && TxtPercentage.Text != "")
            {
                if (DTEmpTable.Rows.Count > 0)
                {
                    foreach (DataRow Drow in DTEmpTable.Rows)
                    {
                        if (TxtExam.Text.Trim() == Drow["EXAM"].ToString())
                        {
                            Common.CommonFuction.ShowMessage("Duplicate Exame!Please change Exame Name");
                            Res = false;
                        }
                    }
                }
                else
                {
                    Res = true;
                }
            }
            else
            {
                Res = false;
            }
            return Res;

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
            return false;
        }
    }
    protected void DDLEmployee_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        InitialisePage();
        getEmployeeQualiData(Convert.ToString(DDLEmployee.SelectedValue.Trim()));
        getEmployeeLanguageData(Convert.ToString(DDLEmployee.SelectedValue.Trim()));
        ViewState["iEMP_CODE"] = DDLEmployee.SelectedValue.Trim();
    }
    private void getEmployeeQualiData(string iEMPMasterCode)
    {
        try
        {
            DataTable DTable = SaitexBL.Interface.Method.HR_EMP_QUAL.GetQualiDetail(iEMPMasterCode);
            foreach (DataRow Drow in DTable.Rows)
            {
                DataRow dr;
                dr = DTEmpTable.NewRow();
                dr["UniqueId"] = DTEmpTable.Rows.Count + 1;
                dr["EMP_QUAL_ID"] = Drow["EMP_QUAL_ID"];
                dr["EMP_CODE"] = Drow["EMP_CODE"].ToString();
                dr["EXAM"] = Drow["EXAM"].ToString();
                dr["SCH_COL"] = Drow["SCH_COL"].ToString();
                dr["PASS_YEAR"] = Drow["PASS_YEAR"].ToString();
                dr["BOARD_UNIV"] = Drow["BOARD_UNIV"].ToString();
                dr["GRADE"] = Drow["GRADE"].ToString();
                dr["PERCENT"] = Drow["PERCENT"].ToString();
                DTEmpTable.Rows.Add(dr);
            }
            grdEmpQualiDetail.DataSource = DTEmpTable;
            grdEmpQualiDetail.DataBind();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
            ErrHandler.WriteError(ex.Message);
        }

    }
    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        RefreshDetailRow();
    }
    private bool Save_Qualification_Detail()
    {
        bool Res = SaitexBL.Interface.Method.HR_EMP_QUAL.Save_Qualification_Detail(DTEmpTable);
        return Res;
    }
    protected void DDLEmployee_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        // Getting the items
        DataTable data = GetItems(e.Text.ToString().Trim(), Convert.ToInt32(e.ItemsOffset), 10);
        DDLEmployee.DataSource = data;
        DDLEmployee.DataBind();
        // Calculating the numbr of items loaded so far in the ComboBox
        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
    }
    protected DataTable GetItems(string strFirstName, int startOffset, int numberOfItems)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.HR_EMP_MST.GetItems(strFirstName, strCompanyCode, strBranchCode);
            return dt;
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
            throw ex;
        }

    }
    protected int GetItemsCount(string text)
    {
        int Res = 0;
        Res = SaitexBL.Interface.Method.HR_EMP_MST.TotalCount(text, strCompanyCode, strBranchCode);
        return Res;
    }
    private void DeleteQualificationDetailRow(int UniqueId)
    {
        try
        {
            if (grdEmpQualiDetail.Rows.Count == 1)
            {
                DTEmpTable.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in DTEmpTable.Rows)
                {
                    int iUniqueId = int.Parse(dr["UniqueId"].ToString());
                    if (iUniqueId == UniqueId)
                    {
                        DTEmpTable.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in DTEmpTable.Rows)
                {
                    iCount = iCount + 1;
                    dr["UniqueId"] = iCount;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private bool FillDataTableByGrid()
    {
        try
        {
            bool result = true;
            if (grdEmpQualiDetail.Rows.Count > 0)
            {
                DTEmpTable.Rows.Clear();
                foreach (GridViewRow grdRow in grdEmpQualiDetail.Rows)
                {
                    Label LblExam = (Label)grdRow.FindControl("LblExam");
                    Label LblBoard = (Label)grdRow.FindControl("LblBoard");
                    Label LblSchool = (Label)grdRow.FindControl("LblSchool");
                    Label LblPassingYear = (Label)grdRow.FindControl("LblPassingYear");
                    Label LblGrade = (Label)grdRow.FindControl("LblGrade");
                    Label LblPercentage = (Label)grdRow.FindControl("LblPercentage");


                    if (LblExam.Text != "" && LblBoard.Text != "" && LblSchool.Text != "" && LblPassingYear.Text != "" && LblGrade.Text != "" && LblPercentage.Text != "")
                    {
                        DataRow dr = DTEmpTable.NewRow();
                        dr["UniqueId"] = DTEmpTable.Rows.Count + 1;
                        dr["EXAM"] = LblExam.Text;
                        dr["SCH_COL"] = LblSchool.Text.Trim();
                        dr["PASS_YEAR"] = LblPassingYear.Text.Trim();
                        dr["BOARD_UNIV"] = LblBoard.Text.Trim();
                        dr["GRADE"] = LblGrade.Text.Trim();
                        dr["PERCENT"] = LblPercentage.Text.Trim();
                        DTEmpTable.Rows.Add(dr);
                    }
                }
            }
            return result;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //---------------------------------Employee Language data------------------------------
    private void CreateEmpLanguageDetailTable()
    {
        DTLangTable = new DataTable();
        DTLangTable.Columns.Add("UniqueLangId", typeof(int));
        DTLangTable.Columns.Add("EMP_LANG_ID", typeof(int));
        DTLangTable.Columns.Add("EMP_CODE", typeof(string));
        DTLangTable.Columns.Add("EMP_LANG", typeof(string));
        DTLangTable.Columns.Add("EMP_READ", typeof(string));
        DTLangTable.Columns.Add("EMP_SPEAK", typeof(string));
        DTLangTable.Columns.Add("EMP_WRITE", typeof(string));


    }
    private void RefreshLanguageDetailRow()
    {
        DDLLanguage.SelectedIndex = -1;
        ChkRead.Checked = false;
        ChkSpeak.Checked = false;
        ChkWrite.Checked = false;
        ViewState["UniqueLangId"] = null;
    }
    private void DeleteLanguageDetailRow(int UniqueId)
    {
        try
        {
            if (GVLanguage.Rows.Count == 1)
            {
                DTLangTable.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in DTLangTable.Rows)
                {
                    int iUniqueId = int.Parse(dr["UniqueLangId"].ToString());
                    if (iUniqueId == UniqueId)
                    {
                        DTLangTable.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in DTLangTable.Rows)
                {
                    iCount = iCount + 1;
                    dr["UniqueLangId"] = iCount;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    
    protected void LbLangSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (DDLLanguage.SelectedValue != "" && DDLLanguage.SelectedValue != null)
            {
                string StrWrite = "";
                string StrRead = "";
                string StrSpeak = "";
                if (ChkRead.Checked)
                {
                    StrRead = "READ";
                }
                if (ChkSpeak.Checked)
                {
                    StrSpeak = "SPEAK";
                }
                if (ChkWrite.Checked)
                {
                    StrWrite = "WRITE";
                }
                int UniqueId = 0;
                if (ViewState["UniqueLangId"] != null)
                    UniqueId = int.Parse(ViewState["UniqueLangId"].ToString());
                DataRow dr;
                dr = DTLangTable.NewRow();
                dr["UniqueLangId"] = DTLangTable.Rows.Count + 1;
                dr["EMP_CODE"] = DDLEmployee.SelectedValue.ToString();
                dr["EMP_LANG"] = DDLLanguage.SelectedValue.ToString();
                dr["EMP_READ"] = StrRead.ToString();
                dr["EMP_WRITE"] = StrWrite.ToString();
                dr["EMP_SPEAK"] = StrSpeak.ToString();
                DTLangTable.Rows.Add(dr);
                RefreshLanguageDetailRow();
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please Select Employee");
                DDLEmployee.Focus();
            }
            BindLanguageDetailGrid();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void LbLangCancel_Click(object sender, EventArgs e)
    {
        RefreshLanguageDetailRow();
    }
    private void BindLanguageDetailGrid()
    {
        try
        {
            GVLanguage.DataSource = DTLangTable;
            GVLanguage.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void getEmployeeLanguageData(string iEMPMasterCode)
    {
        try
        {
            DataTable DTable = SaitexBL.Interface.Method.HR_EMP_LANG.GetLanguageDetail(iEMPMasterCode);
            foreach (DataRow Drow in DTable.Rows)
            {
                string StrWrite = "";
                string StrRead = "";
                string StrSpeak = "";
                if (Drow["EMP_READ"].ToString() == "R")
                {
                    StrRead = "READ";
                }
                if (Drow["EMP_SPEAK"].ToString() == "S")
                {
                    StrSpeak = "SPEAK";
                }
                if (Drow["EMP_WRITE"].ToString() == "W")
                {
                    StrWrite = "WRITE";
                }
                DataRow dr;
                dr = DTLangTable.NewRow();
                dr["UniqueLangId"] = DTLangTable.Rows.Count + 1;
                dr["EMP_LANG_ID"] = Drow["EMP_LANG_ID"];
                dr["EMP_CODE"] = Drow["EMP_CODE"].ToString();
                dr["EMP_LANG"] = Drow["EMP_LANG"].ToString();
                dr["EMP_READ"] = StrRead.ToString();
                dr["EMP_SPEAK"] = StrSpeak.ToString();
                dr["EMP_WRITE"] = StrWrite.ToString();

                DTLangTable.Rows.Add(dr);
            }
            GVLanguage.DataSource = DTLangTable;
            GVLanguage.DataBind();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
            ErrHandler.WriteError(ex.Message);
        }

    }
    
    protected void GVLanguage_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "EmplangEdit")
            {
                //FillDtlByGrid(UniqueId);
            }
            else if (e.CommandName == "EmpLangDelete")
            {
                DeleteLanguageDetailRow(UniqueId);
                BindLanguageDetailGrid();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", "window.alert('" + ex.Message + "');", true);
            errorLog.ErrHandler.WriteError(ex.Message);
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
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
}

