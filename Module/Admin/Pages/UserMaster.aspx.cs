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

using SaitexBL.Interface.Method;
using System.Data.OracleClient;
using errorLog;
using Common;
using DBLibrary;
public partial class Admin_UserMaster : System.Web.UI.Page
{


    //protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;

    //OracleConnection con = null;
    //OracleCommand cmd = null;
    //OracleParameter param = null;
    //csSaitex obj = null;
    //DataSet ds = null;
    //OracleDataReader dr = null;

    protected void Page_Load(object sender, EventArgs e)
    {


        lblMode.Text = "Save";
        // Grid1.IsCallback = false;
        if (!IsPostBack)
        {
            Grid1.AutoPostBackOnSelect = false;
            radUserType.SelectedIndex = -1;
            chk_Status.Checked = true;
            bindGvUser();
            tdUpdate.Visible = false;
            tdComboBoxID.Visible = false;
            ValidationSummary2.Visible = true;
            ddlItemCode.Visible = false;



        }

        if (Convert.ToInt16(Session["saveStatus"]) == 1)
        {
            if (Request.QueryString["cId"].ToString().Trim() == "S")
            {
                lblMessage.Text = "Record Saved successfully";

            }
            if (Request.QueryString["cId"].ToString().Trim() == "U")
            {
                lblMessage.Text = "Record Updated successfully";
            }

            if (Request.QueryString["cId"].ToString().Trim() == "D")
            {
                lblMessage.Text = "Record Deleted successfully";
            }

            Session["saveStatus"] = 0;
        }
    }
    protected void SaveUserMaster()
    {

        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            SaitexDM.Common.DataModel.CM_USER_MST oCM_USER_MST = new SaitexDM.Common.DataModel.CM_USER_MST();
            oCM_USER_MST.USER_CODE = CommonFuction.funFixQuotes(txtUserCode.Text.Trim());
            oCM_USER_MST.USER_LOG_ID = CommonFuction.funFixQuotes(txtLoginId.Text.Trim());
            oCM_USER_MST.USER_NAME = CommonFuction.funFixQuotes(txtUserName.Text.Trim());
            //oCM_USER_MST.USER_PASS = CommonFuction.base64Encode(CommonFuction.funFixQuotes(txtPassword.Text.ToString().Trim()));
            oCM_USER_MST.USER_PASS = CommonFuction.funFixQuotes(txtPassword.Text.ToString().Trim());
            oCM_USER_MST.USER_REMARKS = CommonFuction.funFixQuotes(txtRemarks.Text.ToString().Trim());
            oCM_USER_MST.USER_TYPE = CommonFuction.funFixQuotes(radUserType.SelectedValue.Trim());
            oCM_USER_MST.TUSER = oUserLoginDetail.UserCode;
            oCM_USER_MST.STATUS = true;
            oCM_USER_MST.DEL_STATUS = false;
            oCM_USER_MST.TDATE = DateTime.Now ;
            int iRecordFound = 0;
            bool Result = SaitexBL.Interface.Method.CM_USER_MST.Insert(oCM_USER_MST, out iRecordFound);
            if (Result)
            {
                // InitialisePage();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('User Master saved successfully');", true);
                BlanksControls();

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('User Master Duplicate Entry');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('" + ex.Message + "');", true);
        }

    }
    protected void UpdateUserMaster()
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            SaitexDM.Common.DataModel.CM_USER_MST oCM_USER_MST = new SaitexDM.Common.DataModel.CM_USER_MST();

            oCM_USER_MST.USER_ID = int.Parse(ViewState["vsEdit"].ToString().Trim());
            oCM_USER_MST.USER_CODE = CommonFuction.funFixQuotes(txtUserCode.Text.Trim());
            oCM_USER_MST.USER_LOG_ID = CommonFuction.funFixQuotes(txtLoginId.Text.Trim());
            oCM_USER_MST.USER_NAME = CommonFuction.funFixQuotes(txtUserName.Text.Trim());
            oCM_USER_MST.USER_PASS = CommonFuction.funFixQuotes(txtPassword.Text.Trim());       
            oCM_USER_MST.USER_REMARKS = CommonFuction.funFixQuotes(txtRemarks.Text.ToString().Trim());
            oCM_USER_MST.USER_TYPE = CommonFuction.funFixQuotes(radUserType.SelectedItem.Value.ToString().Trim());
            oCM_USER_MST.TUSER = oUserLoginDetail.UserCode;
            oCM_USER_MST.STATUS = true;
            oCM_USER_MST.DEL_STATUS = false;
            oCM_USER_MST.TDATE = DateTime.Now;
            int iRecordFound = 0;
            bool Result = SaitexBL.Interface.Method.CM_USER_MST.Update(oCM_USER_MST, out iRecordFound);
            if (Result)
            {
                //ViewState["UserPassword"] = null;
                // InitialisePage();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('User Master Update successfully');", true);
                BlanksControls();
                ddlItemCode.Visible = false;
                Grid1.AutoPostBackOnSelect = false;
                tdSave.Visible = true;
                tdUpdate.Visible = false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('User Master  can not be change Login ID');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('" + ex.Message + "');", true);

        }

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Saitex/Admin/UserMaster.aspx", false);

    }
    private void bindGvUser()
    {
        DataTable dt;
        try
        {
            dt = SaitexBL.Interface.Method.CM_USER_MST.PrintReport();

            //    string strSQL = "";
            //    strSQL = "select USER_ID, USER_CODE, USER_NAME,USER_PASS,USER_LOG_ID,USER_REMARKS, STATUS, USER_TYPE,   TDATE from CM_USER_MST where DEL_STATUS='0' ";// when  'EE' then 'Employee' when 'AT' then 'Finincial Acounting' when 'ST' then 'Stock' else 'Not Defined' end usertype, to_char(DT_CREATED,'DD-MON-YYYY') DT_CREATED from tblUserMaster where CH_DELETESTATUS='0'";
            //    obj = new csSaitex();
            //    ds = obj.getDataSet(strSQL, CommandType.Text);
            //    Grid1.DataSource = ds;
            //    Grid1.DataBind();
            //    //lblTotalRecord.Text = ds.Tables[0].Rows.Count.ToString().Trim();


            Grid1.DataSource = dt;
            Grid1.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;
        }

    }
    //protected void gvUserMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    lblMessage.Text = "";
    //    lblErrorMessage.Text = "";
    //    gvUserMaster.PageIndex = e.NewPageIndex;
    //    bindGvUser();

    //}
    protected void gvUserMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //lblMessage.Text = "";
        //lblErrorMessage.Text = "";
        if (e.CommandName == "recEdit")
        {
            imgbtnSave.Visible = false;
            imgbtnUpdate.Visible = true;
            ViewState["vsEdit"] = e.CommandArgument.ToString().Trim();

            getGvUserMaster(Convert.ToInt16(e.CommandArgument));

        }

        if (e.CommandName == "recDelete")
        {
            deleteGVUserMaster(Convert.ToInt32(e.CommandArgument));

        }

    }
    private void getGvUserMaster(int User_Id)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.CM_USER_MST.GetUserMasterByUserId(User_Id);

            if (dt != null && dt.Rows.Count > 0)
            {
                radUserType.SelectedValue = dt.Rows[0]["USER_TYPE"].ToString();
                txtUserCode.Text = dt.Rows[0]["USER_CODE"].ToString();
                //txtUserCode.Enabled = false;
                txtUserName.Text = dt.Rows[0]["USER_NAME"].ToString();
                //txtLoginId.Text = dt.Rows[0]["USER_LOG_ID"].ToString();
                txtPassword.Text = dt.Rows[0]["USER_PASS"].ToString();

                // txtPassword.Attributes.Add("value", dt.Rows[0]["USER_PASS"].ToString());
                txtRemarks.Text = dt.Rows[0]["USER_REMARKS"].ToString();


            }



        }

        catch (OracleException ex)
        {
            throw ex;

        }

        catch (Exception ex)
        {
            throw ex;

        }



        finally
        {
            //if (obj != null)
            //{
            //    obj = null;
            //}

        }
    }
    private void deleteGVUserMaster(int iUserId)
    {
        try
        {
            lblMessage.Text = "Child Record exist ! So this record can not be deleted";
        }

        catch (OracleException ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);

        }


        catch (Exception ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);

        }



    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        if (Page.IsValid)
        {

            SaveUserMaster();
            bindGvUser();

        }
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        UpdateUserMaster();
        bindGvUser();


    }
    //deleteGVUserMaster(int.Parse(ViewState["vsEdit"].ToString()));
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
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string URL = "/Saitex/Module/Admin/Reports/UserMaster_rpt.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);


    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("/Saitex/Module/Admin/Pages/UserMaster.aspx", false);
    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {

        ddlItemCode.Visible = true; ;
        lblMode.Text = "Update";
        tdFind.Visible = true;
        tdUpdate.Visible = true;
        tdprint.Visible = true;
        tdExit.Visible = true;
        tdComboBoxID.Visible = true;
        radUserType.Enabled = true;
        tdSave.Visible = false;
        tdLoginId1.Visible = true;
        tdLoginId2.Visible = true;
        tdLoginId3.Visible = true;


        Grid1.AutoPostBackOnSelect = true;


    }
    private void BlanksControls()
    {

        try
        {
            //radUserType.Items.Clear();
            txtUserCode.Text = "";
            txtUserName.Text = "";
            txtLoginId.Text = "";
            txtPassword.Text = "";
            txtRemarks.Text = "";
            chk_Status.Text = "";
            txtPassword.Attributes.Clear();
        }
        catch (Exception ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
        }
    }
    //   protected override void OnPreRender(EventArgs e)
    //{
    //    base.OnPreRender(e);
    //onclientclick = "if (!confirm('Are you sure you want to Save this record ? ')) { return false; }";
    //onclientclick = "if (!confirm('Are you sure you want to Update this record?')) { return false; }";
    //onclientclick = "if (!confirm('Are you sure you want to Clear this record ?  ')) { return false; }";
    //onclientclick = "if (!confirm('Are you sure you want to Print this record ? ')) { return false; }";
    //onclientclick = "if (!confirm('Are you sure you want to Find Some Control ?')) { return false; }";
    //onclientclick = "if (!confirm('Are you sure you want to Exit ? ')) { return false; }";



    //imgbtnSave.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save this record ? ')");
    //     imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");

    //     imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
    //     imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
    //     imgbtnFind.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Find Some Control ? ')");
    //     imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ?')");

    //}
    //protected void txtUserCode_TextChanged(object sender, EventArgs e)
    //{
    //    if (txtUserCode.Text != "")
    //    {
    //        GetFindData(txtUserCode.Text.Trim());

    //        txtUserCode.Visible = true;


    //    }
    //    else
    //    {
    //        lblErrorMessage.Text = "Please enter Company Code";
    //    }
    //}
    //protected void radUserType_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}
    protected void Grid1_Select(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        txtLoginId.Enabled = false;
        txtPassword.Enabled = false;
        ArrayList ar = Grid1.SelectedRecords;

        lblMessage.Text = "";
        tdFind.Visible = true;
        tdUpdate.Visible = true;
        tdprint.Visible = true;
        tdExit.Visible = true;

        tdLoginId1.Visible = true;
        tdLoginId2.Visible = true;
        tdLoginId3.Visible = true;

        tdSave.Visible = false;

        Hashtable ht = (Hashtable)ar[0];

        ViewState["vsEdit"] = ht["USER_ID"].ToString().Trim();
        txtLoginId.Text = ht["USER_LOG_ID"].ToString().Trim();

        txtPassword.Attributes.Add("value", ht["USER_PASS"].ToString().Trim());

        //txtPassword.Text = ht["USER_PASS"].ToString().Trim();
        txtRemarks.Text = ht["USER_REMARKS"].ToString().Trim();
        txtUserCode.Text = ht["USER_CODE"].ToString().Trim();
        txtUserName.Text = ht["USER_NAME"].ToString().Trim();
        //radUserType.SelectedValue = ht["USER_TYPE"].ToString().Trim();
        // radUserType.SelectedItem.Text = ht["USER_TYPE"].ToString().Trim();

        radUserType.SelectedIndex = radUserType.Items.IndexOf(radUserType.Items.FindByText(ht["USER_TYPE"].ToString().Trim()));

    }
    protected void ddlItemCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        DataTable dt;
        //DataTable data = new DataTable();
        //data = GetItems(e.Text, e.ItemsOffset, 10);
        try
        {
            dt = SaitexBL.Interface.Method.CM_USER_MST.PrintReport();
            ddlItemCode.DataSource = dt;
            DataBind();

            //string strSQL = "";
            //strSQL = "select USER_ID, USER_CODE, USER_NAME from CM_USER_MST where DEL_STATUS='0' ";// when  'EE' then 'Employee' when 'AT' then 'Finincial Acounting' when 'ST' then 'Stock' else 'Not Defined' end usertype, to_char(DT_CREATED,'DD-MON-YYYY') DT_CREATED from tblUserMaster where CH_DELETESTATUS='0'";
            //obj = new csSaitex();
            //ds = obj.getDataSet(strSQL, CommandType.Text);
            //ddlItemCode.Items.Clear();

            //ddlItemCode.DataSource = ds;
            //ddlItemCode.DataBind();

            ////e.ItemsLoadedCount = data.Rows.Count;
            ////e.ItemsCount = data.Rows.Count;


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
        }
    }
    protected void ddlItemCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        txtUserCode.Enabled = false;
        txtLoginId.Enabled = true;
        txtPassword.Enabled = true;

        try
        {
            DataTable dt = SaitexBL.Interface.Method.CM_USER_MST.SelectLeaveTransaction(int.Parse(ddlItemCode.SelectedValue.Trim()));
            //DataTable dt = SaitexBL.Interface.Method.CM_USER_MST.PrintReport();
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["vsEdit"] = int.Parse(dt.Rows[0]["USER_ID"].ToString().Trim());
                txtLoginId.Text = dt.Rows[0]["USER_LOG_ID"].ToString().Trim();
                //txtPassword.Text = dt.Rows[0]["USER_PASS"].ToString().Trim();  
                txtPassword.Attributes.Add("value", dt.Rows[0]["USER_PASS"].ToString().Trim());
               //ViewState["UserPassword"] = CommonFuction.base64Decode(dt.Rows[0]["USER_PASS"].ToString().Trim());
                ViewState["UserPassword"] = dt.Rows[0]["USER_PASS"].ToString().Trim();

                txtRemarks.Text = dt.Rows[0]["USER_REMARKS"].ToString().Trim();
                txtUserCode.Text = dt.Rows[0]["USER_CODE"].ToString().Trim();
                txtUserName.Text = dt.Rows[0]["USER_NAME"].ToString().Trim();
                radUserType.SelectedValue = dt.Rows[0]["USER_TYPE"].ToString().Trim();
                // radUserType.SelectedIndex = radUserType.Items.IndexOf(radUserType.Items.FindByText(dt.Rows[0]["USER_TYPE"].ToString().Trim()));

            }
        }


        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('User can not selected Item bind in Field');", true);
        }
    }
    //protected void ddlItemCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    //{
    //    try
    //    {
    //        DataTable data = new DataTable();
    //        data = GetItems(e.Text, e.ItemsOffset, 10);

    //        ddlItemCode.Items.Clear();
    //        ddlItemCode.DataSource = data;
    //        ddlItemCode.DataBind();

    //        // Calculating the numbr of items loaded so far in the ComboBox
    //        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

    //        // Getting the total number of items that start with the typed text
    //        e.ItemsCount = GetItemsCount(e.Text);
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
    //    }
    //}
    //private void BindItemCodeCombo(string text)
    //{
    //    try
    //    {
    //        string CommandText = "select USER_ID, USER_CODE, USER_NAME from CM_USER_MST";
    //        string WhereClause = "  where USER_ID like :SearchQuery or USER_CODE like :SearchQuery or USER_NAME like :SearchQuery";
    //        string WhereClause = "  where USER_ID=:USER_ID";
    //        string SortExpression = " % ";
    //        string SearchQuery = text.ToUpper() + "%";
    //        DataTable data = SaitexBL.Interface.Method.CM_USER_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

    //        ddlItemCode.Items.Clear();

    //        ddlItemCode.DataSource = data;
    //        ddlItemCode.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
    //    }
    //}
    protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    {

        string whereClause = " WHERE USER_ID like :SearchQuery And DEL_STATUS = '0'";
        string sortExpression = " ORDER BY USER_ID";
        string commandText = "SELECT * FROM CM_USER_MST";
        string sPO = "";
        DataTable dt = SaitexBL.Interface.Method.CM_USER_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
        return dt;

    }
    protected int GetItemsCount(string text)
    {

        string CommandText = "select USER_ID, USER_CODE, USER_NAME from CM_USER_MST where DEL_STATUS='0'";
        return SaitexBL.Interface.Method.CM_USER_MST.GetCountForLOV(CommandText, text + '%', "");


    }
    //protected void cmbLeaveTran_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    //{
    //    DataTable data = new DataTable();
    //    data = GetItems(e.Text, e.ItemsOffset, 10);

    //    cmbLeaveTran.Items.Clear();
    //    cmbLeaveTran.DataSource = data;
    //    cmbLeaveTran.DataBind();

    //    // Calculating the numbr of items loaded so far in the ComboBox
    //    e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

    //    // Getting the total number of items that start with the typed text
    //    e.ItemsCount = GetItemsCount(e.Text);
    //}



    //protected void ddlLeave_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    //{
    //    try
    //    {

    //        SaitexDM.Common.DataModel.CM_USER_MST oHR_LV_TRN = new SaitexDM.Common.DataModel.CM_USER_MST();
    //        DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.SelectLeaveTransaction();
    //        if (dt != null && dt.Rows.Count > 0)
    //        {
    //            ViewState["LV_ID"] = dt.Rows[0]["LV_ID"].ToString();
    //            cmbLeaveTran.SelectedValue = dt.Rows[0]["LV_ID"].ToString();
    //            txtRemarks.Text = dt.Rows[0]["REMARKS"].ToString();
    //            txtYear.Text = dt.Rows[0]["YEAR"].ToString();
    //            txtPeriod.Text = dt.Rows[0]["LV_PRD_TYPE"].ToString();
    //            txtLeaveDays.Text = dt.Rows[0]["LV_DAY"].ToString();
    //            tdSave.Visible = false;
    //            tdUpdate.Visible = true;
    //            lblMode.Text = "Find";

    //        }
    //    }


    //}
}