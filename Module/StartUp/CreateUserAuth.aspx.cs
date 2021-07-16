using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Module_StartUp_CreateUserAuth : System.Web.UI.Page
{
    private static DataSet dsDatabase;
    private static string UserCode;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblErrorMessage.Text = "";
        lblMessage.Text = "";

        if (!IsPostBack)
        {
            GetData();
            BindCompany(null);
            BindDepartment();
        }
       

    }
    private void GetData()
    {
        try
        {
            dsDatabase = SaitexBL.Interface.Method.UserAuthorisation.GetUserAuthorisationRight();
        }
        catch (Exception ex)
        { 
            lblErrorMessage.Text = ex.Message;
        }
    }
    private void BindCompany(TreeNode tn)
    {
        try
        {
            var dvCompany = new DataView(dsDatabase.Tables["CompanyMaster"]);

            if (dvCompany.Count > 0)
            {
                for (int iLoop = 0; iLoop < dvCompany.Count; iLoop++)
                {
                    var tnn = new TreeNode(dvCompany[iLoop]["COMP_NAME"].ToString(), dvCompany[iLoop]["COMP_CODE"].ToString());
                    BindBranch(tnn, dvCompany[iLoop]["COMP_CODE"].ToString());
                    if (tn == null)
                        trvCompanyBranch.Nodes.Add(tnn);
                    else
                        tn.ChildNodes.Add(tnn);
                }
            }
        }
        catch (Exception ex)
        { lblErrorMessage.Text = ex.Message; }
    }
    private void BindBranch(TreeNode tn, string CompanyId)
    {
        try
        {
            var dvBranch = new DataView(dsDatabase.Tables["BranchMaster"]);
            dvBranch.RowFilter = "COMP_CODE='" + CompanyId + "'";
            if (dvBranch.Count > 0)
            {
                for (int iLoop = 0; iLoop < dvBranch.Count; iLoop++)
                {
                    var tnn = new TreeNode(dvBranch[iLoop]["BRANCH_NAME"].ToString(), dvBranch[iLoop]["BRANCH_CODE"].ToString());
                    if (tn == null)
                        trvCompanyBranch.Nodes.Add(tnn);
                    else
                        tn.ChildNodes.Add(tnn);
                }
            }
        }
        catch (Exception ex)
        { lblErrorMessage.Text = ex.Message; }
    }
    private void BindDepartment()
    {
        try
        {
            var dvDepartment = new DataView(dsDatabase.Tables["DepartmentMaster"]);

            if (dvDepartment.Count > 0)
            {
                for (int iLoop = 0; iLoop < dvDepartment.Count; iLoop++)
                {
                    var tn = new TreeNode(dvDepartment[iLoop]["DEPT_NAME"].ToString(), dvDepartment[iLoop]["DEPT_CODE"].ToString());
                    trvDepartment.Nodes.Add(tn);
                }
            }
        }
        catch (Exception ex)
        { lblErrorMessage.Text = ex.Message; }
    }
    private void FillDataByUserCode(string UserCode)
    {
        UnCheckAll();

        // to fill company and branch detail by user code
        var dvUserCompanyAuthorisation = new DataView(dsDatabase.Tables["UserCompanyAuthorisation"]);
        dvUserCompanyAuthorisation.RowFilter = "USER_CODE='" + UserCode + "'";

        if (dvUserCompanyAuthorisation.Count > 0)
        {
            for (int iLoop = 0; iLoop < dvUserCompanyAuthorisation.Count; iLoop++)
            {
                string CompanyCode = dvUserCompanyAuthorisation[iLoop]["COMP_CODE"].ToString();
                string BranchCode = dvUserCompanyAuthorisation[iLoop]["BRANCH_CODE"].ToString();
                CheckComanyBranchTreeByUser(CompanyCode, BranchCode);               

            }
        }

        // to fill department detail by user code
        var dvUserDepartmentAuthorisation = new DataView(dsDatabase.Tables["UserDepartmentAuthorisation"]);
        dvUserDepartmentAuthorisation.RowFilter = "USER_CODE='" + UserCode + "'";

        if (dvUserDepartmentAuthorisation.Count > 0)
        {
            for (int iLoop = 0; iLoop < dvUserDepartmentAuthorisation.Count; iLoop++)
            {
                string DepartmentCode = dvUserDepartmentAuthorisation[iLoop]["DEPT_CODE"].ToString();
                CheckDepartmentTreeByUser(DepartmentCode);
                
            }
        }
    }
    private void CheckComanyBranchTreeByUser(string ComanyCode, string BranchCode)
    {
        if (trvCompanyBranch.Nodes.Count > 0)
        {
            for (int iLoop = 0; iLoop < trvCompanyBranch.Nodes.Count; iLoop++)
            {
                string sCompCode = trvCompanyBranch.Nodes[iLoop].Value.Trim();
                if (sCompCode == ComanyCode)
                {
                    trvCompanyBranch.Nodes[iLoop].Checked = true;

                    if (trvCompanyBranch.Nodes[iLoop].ChildNodes.Count > 0)
                    {
                        for (int jLoop = 0; jLoop < trvCompanyBranch.Nodes[iLoop].ChildNodes.Count; jLoop++)
                        {
                            string sBranchCode = trvCompanyBranch.Nodes[iLoop].ChildNodes[jLoop].Value.Trim();
                            if (sBranchCode == BranchCode)
                            {
                                trvCompanyBranch.Nodes[iLoop].ChildNodes[jLoop].Checked = true;
                            }
                        }
                    }
                }
            }
        }
    }
    private void CheckDepartmentTreeByUser(string DepartmentCode)
    {
        if (trvDepartment.Nodes.Count > 0)
        {
            for (int iLoop = 0; iLoop < trvDepartment.Nodes.Count; iLoop++)
            {
                string sDeptCode = trvDepartment.Nodes[iLoop].Value.Trim();
                if (sDeptCode == DepartmentCode)
                {
                    trvDepartment.Nodes[iLoop].Checked = true;
                }
            }
        }
    }
    private void UnCheckAll()
    {
        UncheckAllCompanyBranch();
        UncheckAllDepartment();
    }
    private void UncheckAllCompanyBranch()
    {
        if (trvCompanyBranch.Nodes.Count > 0)
        {
            for (int iLoop = 0; iLoop < trvCompanyBranch.Nodes.Count; iLoop++)
            {
                trvCompanyBranch.Nodes[iLoop].Checked = false;
                if (trvCompanyBranch.Nodes[iLoop].ChildNodes.Count > 0)
                {
                    for (int jLoop = 0; jLoop < trvCompanyBranch.Nodes[iLoop].ChildNodes.Count; jLoop++)
                    {

                        trvCompanyBranch.Nodes[iLoop].ChildNodes[jLoop].Checked = false;
                    }
                }
            }
        }
    }
    private void UncheckAllDepartment()
    {
        if (trvDepartment.Nodes.Count > 0)
        {
            for (int iLoop = 0; iLoop < trvDepartment.Nodes.Count; iLoop++)
            {
                trvDepartment.Nodes[iLoop].Checked = false;
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            bool cStatus = false;
            bool dStatus = false;
            if (validate(out cStatus, out dStatus))
            {
                string sResult = SaveData();               
            }
            else
            {
                //  select * from tblEmployeeMaster where ch_EmployeeMasterId like 'b%' or ch_FirstName like 'b%' or ch_EmployeeCode like 'b%' or ch_DepartmentId like 'b%' or ch_DesignationId like 'b%';
                if (cStatus == false && dStatus == false)
                {
                    lblErrorMessage.Text = "Pls select Company and branch/ department first";
                }
                else if (cStatus == false && dStatus == true)
                {
                    lblErrorMessage.Text = "Pls select Company and branch first";
                }
                else if (dStatus == false && cStatus == true)
                {
                    lblErrorMessage.Text = "Pls select Department first";
                }
            }

        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
        }
    }
    private bool validate(out bool cStatus, out bool dStatus)
    {
        try
        {
            cStatus = false;
            dStatus = false;
            bool IsValidated = false;
            if (trvCompanyBranch.Nodes.Count > 0)
            {
                foreach (TreeNode tnCompany in trvCompanyBranch.Nodes)
                {
                    if (tnCompany.Checked == true)
                    {
                        foreach (TreeNode tnBranch in tnCompany.ChildNodes)
                            if (tnBranch.Checked == true)
                                cStatus = true;
                    }
                }
            }
            if (trvDepartment.Nodes.Count > 0)
            {
                foreach (TreeNode tnDept in trvDepartment.Nodes)
                {
                    if (tnDept.Checked == true)
                        dStatus = true;
                }
            }
            if (cStatus == true && dStatus == true)
            {
                IsValidated = true;
            }
            return IsValidated;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private string SaveData()
    {
        try
        {
            string sCompResult = string.Empty;
            string sDeptResult = string.Empty;
            string UserCode = Common.CommonFuction.funFixQuotes(txtUserName.Text.Trim());
            SaitexDM.Common.DataModel.UserAuthorisation oUserAuthorisation = new SaitexDM.Common.DataModel.UserAuthorisation();
            oUserAuthorisation.VC_USERCODE = UserCode;

            // code to save Company And Branch Integration
            SaitexBL.Interface.Method.UserAuthorisation.DeleteUserCompanyAuthorisation(oUserAuthorisation, "");
            foreach (TreeNode tnCompany in trvCompanyBranch.Nodes)
            {
                if (tnCompany.Checked == true)
                {
                    string CompanyId = tnCompany.Value.Trim();
                    if (tnCompany.ChildNodes.Count > 0)
                    {
                        foreach (TreeNode tnBranch in tnCompany.ChildNodes)
                        {
                            if (tnBranch.Checked == true)
                            {
                                string BranchCode = tnBranch.Value.Trim();
                                oUserAuthorisation.DT_CREATED = System.DateTime.Now.Date;
                                oUserAuthorisation.COMP_CODE = CompanyId;
                                oUserAuthorisation.CH_BRANCHCODE = BranchCode;
                                // oUserAuthorisation.CH_STATUS = chk_Comp_Status.Checked;

                                int iRecordFound = 0;
                                string InsertBy = "";
                                bool BStatus = SaitexBL.Interface.Method.UserAuthorisation.InsertUserCompanyAuthorisation(oUserAuthorisation, out iRecordFound, InsertBy);
                                if (BStatus)
                                {
                                    sCompResult = sCompResult + "Record saved for '" + CompanyId + "'/'" + BranchCode + "' successfully<br />";
                                }
                                else
                                {
                                    sCompResult = sCompResult + "Record saving failed for '" + CompanyId + "'/'" + BranchCode + "'<br />";
                                }
                            }
                        }
                    }
                }
            }

            // code to save Department Integration
            SaitexBL.Interface.Method.UserAuthorisation.DeleteUserDepartmentAuthorisation(oUserAuthorisation, "");
            foreach (TreeNode tnDepartment in trvDepartment.Nodes)
            {
                if (tnDepartment.Checked == true)
                {
                    string DepartmentCode = tnDepartment.Value.Trim();

                    oUserAuthorisation.DT_CREATED = System.DateTime.Now.Date;
                    oUserAuthorisation.VC_DEPARTMENTCODE = DepartmentCode;
                    //oUserAuthorisation.CH_STATUS = chk_Dept_Status.Checked;

                    int iRecordFound = 0;
                    string InsertBy = "";
                    bool BStatus = SaitexBL.Interface.Method.UserAuthorisation.InsertUserDepartmentAuthorisation(oUserAuthorisation, out iRecordFound, InsertBy);
                    if (BStatus)
                    {
                        sDeptResult = sDeptResult + "Record saved for '" + DepartmentCode + "' successfully<br />";
                    }
                    else
                    {
                        sDeptResult = sDeptResult + "Record saving failed for '" + DepartmentCode + "'<br />";
                    }
                }
            }
            return sCompResult + sDeptResult;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void txtUserName_TextChanged1(object sender, EventArgs e)
    {
        if (txtUserName.Text != "")
        {
            FillDataByUserCode(Common.CommonFuction.funFixQuotes(txtUserName.Text.Trim()));
        }
    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            bool cStatus = false;
            bool dStatus = false;
            if (validate(out cStatus, out dStatus))
            {
                string sResult = SaveData();
                Common.CommonFuction.ShowMessage(sResult);
                Response.Redirect("~/Default.aspx", false);
            }
            else
            {
                //  select * from tblEmployeeMaster where ch_EmployeeMasterId like 'b%' or ch_FirstName like 'b%' or ch_EmployeeCode like 'b%' or ch_DepartmentId like 'b%' or ch_DesignationId like 'b%';
                if (cStatus == false && dStatus == false)
                {
                    lblErrorMessage.Text = "Pls select Company and branch/ department first";
                }
                else if (cStatus == false && dStatus == true)
                {
                    lblErrorMessage.Text = "Pls select Company and branch first";
                }
                else if (dStatus == false && cStatus == true)
                {
                    lblErrorMessage.Text = "Pls select Department first";
                }
            }
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
        }
    }  
    protected void ddlUserMaster_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            var data = GetUserData(e.Text.ToUpper());
            ddlUserMaster.Items.Clear();
            ddlUserMaster.DataSource = data;
            ddlUserMaster.DataBind();
            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {

        }
    }
    private DataTable GetUserData(string Text)
    {
        try
        {
            string CommandText = "select * from v_cm_user_mst ";
            string WhereClause = "  where USER_CODE like :SearchQuery or USER_NAME like :SearchQuery or USER_LOG_ID like :SearchQuery or USER_TYPE like :SearchQuery";
            string SortExpression = " order by USER_CODE asc";
            string SearchQuery = Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
           
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void ddlUserMaster_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            txtUserName.Text = ddlUserMaster.SelectedValue.ToString();
            FillDataByUserCode(Common.CommonFuction.funFixQuotes(txtUserName.Text.Trim()));
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, "");
        }
    }
}
