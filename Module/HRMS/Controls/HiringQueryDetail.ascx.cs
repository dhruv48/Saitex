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

public partial class Module_HRMS_Controls_HiringQueryDetail : System.Web.UI.UserControl
{
    public static string strCompanyCode = "";
    public static string strBranchCode = string.Empty;
    private static DateTime Sdate;
    private static DateTime Edate;

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            if (!IsPostBack)
            {
                Sdate = oUserLoginDetail.DT_STARTDATE;
                Edate = Common.CommonFuction.GetYearEndDate(Sdate);
                TxtFromdate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
                TxtToDate.Text = Common.CommonFuction.GetYearEndDate(Sdate).ToShortDateString();
                InitialControls();
                //ddlLeaveType.Items.Insert(0, new ListItem("-----------Select---------", string.Empty));
                //ddlLeaveStatus.Items.Insert(0, new ListItem("-----------Select---------", string.Empty));
                BindControls();
                //ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));

            }
        }
        catch (Exception ex)
        {
            throw ex;
            //Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in page Loading"));
        }
    }
    protected void BindControls()
    {
        try
        {
            Bind_Postion();
            Load_BrachName();
            Load_Department();
            //BindLV_TO_DATE();
            //BindLV_FROM_DATE();
            ////BindPurpose();

            //BindUserName();

        }
        catch (Exception ex)
        {
            throw ex;
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
            //DataTable dt = null;
            //dt = new DataTable();
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(oUserLoginDetail.COMP_CODE);
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
    protected void TxtFromdate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtFromdate.Text.Trim() != string.Empty)
            {
                DateTime StartDate = DateTime.Parse(TxtFromdate.Text.Trim().ToString());

                if (StartDate >= Sdate && StartDate <= Edate)
                {

                }
                else
                {
                    Common.CommonFuction.ShowMessage("Please Select Date within Financial Year");
                    TxtFromdate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
                }
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    protected void DTCRToDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtFromdate.Text.Trim() != string.Empty && TxtToDate.Text.Trim().ToString() != string.Empty)
            {
                DateTime EndDate = DateTime.Parse(TxtToDate.Text.Trim().ToString());
                if (EndDate > Edate || EndDate < Sdate)
                {
                    Common.CommonFuction.ShowMessage("Please Select Date within Financial Year");
                    TxtToDate.Text = Edate.ToShortDateString().ToString();
                }
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    //private void bindFromToDate()
    //{
    //   string FIN_YEAR_CODE = ddlyear.SelectedValue.ToString();
    //    DataTable dt = SaitexBL.Interface.Method.CM_FIN_YEAR_MST.FinYear(FIN_YEAR_CODE);
    //    if (dt != null && dt.Rows.Count > 0)
    //    {
    //        DataView dv = new DataView(dt);
    //        //dv.RowFilter = "FIN_YEAR_CODE='" + ddlYear.SelectedValue.ToString() + "'";
    //        if (dv.Count > 0)
    //        {
    //            for (int iLoop = 0; iLoop < dv.Count; iLoop++)
    //            {
    //                DTCRFromDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dv[iLoop]["START_DATE"].ToString()));
    //                DTCRToDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dv[iLoop]["END_DATE"].ToString()));
    //            }
    //        }
    //    }
    //}
    //protected void BindLV_TYPE()
    //{
    //    try
    //    {
    //        ddlLeaveType.Items.Clear();
    //        DataTable dtLeaveType = SaitexBL.Interface.Method.HR_EMP_MST.GetUserDetail();
    //        ddlLeaveType.Items.Clear();
    //        ddlLeaveType.DataSource = dtLeaveType;
    //        ddlLeaveType.DataTextField = "LV_TYPE";
    //        ddlLeaveType.DataValueField = "LV_TYPE";
    //        ddlLeaveType.DataBind();
    //        ddlLeaveType.Items.Insert(0, new ListItem("-----------Select---------", string.Empty));

    //        dtLeaveType.Dispose();
    //        dtLeaveType = null;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //protected void BindLV_STATUS()
    //{
    //    try
    //    {
    //        ddlLeaveStatus.Items.Clear();
    //        DataTable dtLeaveStatus = SaitexBL.Interface.Method.HR_EMP_MST.GetUserDetail();
    //        ddlLeaveStatus.Items.Clear();
    //        ddlLeaveStatus.DataSource = dtLeaveStatus;
    //        ddlLeaveStatus.DataTextField = "LV_STATUS";
    //        ddlLeaveStatus.DataValueField = "LV_STATUS";
    //        ddlLeaveStatus.DataBind();
    //        ddlLeaveStatus.Items.Insert(0, new ListItem("-----------Select---------", string.Empty));

    //        dtLeaveStatus.Dispose();
    //        dtLeaveStatus = null;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //protected void BindUserName()
    //{
    //    try 
    //    {
    //        string usertype = ddlUserType.SelectedValue.ToString();
    //        //ddlUserName.Items.Clear();
    //        DataTable dtUserName = SaitexBL.Interface.Method.CM_USER_MST.SelectByUSR_NAME(usertype);
    //        //if (dtUserName.Rows.Count > 0)
    //        //{
    //        //DataView Dv = new DataView(dtUserName);
    //        //Dv.RowFilter = usertype;
    //        //ddlUserName.DataSource = Dv;

    //            //ddlUserName.Items.Clear();
    //            ddlUserName.DataSource = dtUserName;
    //            ddlUserName.DataTextField = "USER_NAME";
    //            ddlUserName.DataValueField = "USER_NAME";
    //            ddlUserName.DataBind();

    //            ddlUserName.Items.Insert(0, new ListItem("-----------Select---------", string.Empty));
    //      //  }
    //       // ddlUserName.SelectedIndex = ddlUserName.Items.IndexOf(ddlUserType.Items.FindByValue("USER_TYPE"));
    //    }
    //    catch (Exception ex)
    //    {
    //    throw ex;
    //    }
    //}
    protected void GetUserData()
    {
        string LOC_ID = string.Empty;
        string DEPT_ID = string.Empty;
        string POSITION = string.Empty;
        string Fromdate = string.Empty;
        string ToDate = string.Empty;
        string OnFrom = string.Empty;
        string OnTo = string.Empty;
        
        try
        {
            if (DDLLocation.SelectedValue.ToString() != null && DDLLocation.SelectedValue.ToString() != string.Empty)
            {
                LOC_ID = DDLLocation.SelectedValue.ToString();
            }
            else
            {
                LOC_ID = string.Empty;
            }
            if (DDLDepartment.SelectedValue.ToString() != null && DDLDepartment.SelectedValue.ToString() != string.Empty)
            {
                DEPT_ID = DDLDepartment.SelectedValue.ToString();
            }
            else
            {
                DEPT_ID = string.Empty;
            }
            if (DDLPosition.SelectedValue.ToString() != null && DDLPosition.SelectedValue.ToString() != string.Empty)
            {
                POSITION = DDLPosition.SelectedValue.ToString();
            }
            else
            {
                POSITION = string.Empty;
            }
            if (TxtFromdate.Text != null && TxtFromdate.Text != string.Empty && TxtToDate.Text != null && TxtToDate.Text != string.Empty)
            {
                Fromdate = TxtFromdate.Text.Trim();
                ToDate = TxtToDate.Text.Trim();
            }
            else
            {
                Fromdate = string.Empty;
                ToDate = string.Empty;
            }
            if (TxtOnFrom.Text != null && TxtOnFrom.Text != string.Empty && TxtOnTo.Text != null && TxtOnTo.Text != string.Empty)
            {
                OnFrom = TxtOnFrom.Text.Trim();
                OnTo = TxtOnTo.Text.Trim();
            }
            else
            {
                OnFrom = string.Empty;
                OnTo = string.Empty;
            }
           

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.HR_HIRING_PRO.GetUserData(LOC_ID, DEPT_ID, POSITION, Fromdate, ToDate, OnFrom, OnTo);

            if (dt.Rows.Count > 0)
            {
                HiringQueryDetail.DataSource = dt;
                HiringQueryDetail.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                HiringQueryDetail.Visible = true;
            }
            else
            {
                //grUserMasterQuery.DataSource = null;
                //grUserMasterQuery.DataBind();
                Common.CommonFuction.ShowMessage("Data not found by selected item");
                lblTotalRecord.Text = "0";
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            InitialControls();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void InitialControls()
    {
        try
        {
            imgbtnClear.Visible = true;
            imgbtnExit.Visible = true;
            imgbtnHelp.Visible = true;

            //ddlEmpName.SelectedIndex = -1;
            // ddlUserType.SelectedIndex = -1;
            HiringQueryDetail.Visible = false;
            lblTotalRecord.Text = "0";

        }
        catch (Exception ex)
        {
            throw ex;
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
            throw ex;
        }
    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    { }


    protected void btngetrecord_Click1(object sender, EventArgs e)
    {
        try
        {
            GetUserData();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void HiringQueryDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GetUserData();
            HiringQueryDetail.PageIndex = e.NewPageIndex;
            HiringQueryDetail.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    
}
