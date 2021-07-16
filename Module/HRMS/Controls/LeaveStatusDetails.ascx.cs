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

public partial class Module_HRMS_Controls_Leave_Status_Details : System.Web.UI.UserControl
{
    private static DateTime Sdate;
    private static DateTime Edate;
    
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail ;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
              {
             oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
      
            if (!IsPostBack)
            {
                Sdate = oUserLoginDetail.DT_STARTDATE;
                Edate = Common.CommonFuction.GetYearEndDate(Sdate);
                DTCRFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
                DTCRToDate.Text = Common.CommonFuction.GetYearEndDate(Sdate).ToShortDateString();
                InitialControls();
                ddlLeaveType.Items.Insert(0, new ListItem("-----------Select---------", string.Empty));
                ddlLeaveStatus.Items.Insert(0, new ListItem("-----------Select---------", string.Empty));
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
            BindEMP_CODE();
           // BindLV_TYPE();
            //BindLV_STATUS();
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
    protected void BindEMP_CODE()
    {
        try
        {
            ddlEmpName.Items.Clear();
            DataTable dtEmpName = SaitexBL.Interface.Method.HR_EMP_MST.Employee_Info();
            ddlEmpName.Items.Clear();
            ddlEmpName.DataSource = dtEmpName;
            ddlEmpName.DataTextField = "EMPLOYEENAME";
            ddlEmpName.DataValueField = "EMP_CODE";
            ddlEmpName.DataBind();
            ddlEmpName.Items.Insert(0, new ListItem("-----------Select---------", string.Empty));

            dtEmpName.Dispose();
            dtEmpName = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void DTCRFromDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (DTCRFromDate.Text.Trim() != string.Empty)
            {
                DateTime StartDate = DateTime.Parse(DTCRFromDate.Text.Trim().ToString());

                if (StartDate >= Sdate && StartDate <= Edate)
                {

                }
                else
                {
                    Common.CommonFuction.ShowMessage("Please Select Date within Financial Year");
                    DTCRFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
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
            if (DTCRFromDate.Text.Trim() != string.Empty && DTCRToDate.Text.Trim().ToString() != string.Empty)
            {
                DateTime EndDate = DateTime.Parse(DTCRToDate.Text.Trim().ToString());
                if (EndDate > Edate || EndDate < Sdate)
                {
                    Common.CommonFuction.ShowMessage("Please Select Date within Financial Year");
                    DTCRToDate.Text = Edate.ToShortDateString().ToString();
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
    protected void GetUserDetail()
    {
        string EMP_CODE = string.Empty;
        string LV_TYPE = string.Empty;
        string LV_STATUS = string.Empty;
        string LV_TO_DATE = string.Empty;
        string LV_FROM_DATE = string.Empty;
        
       try
        {
            if (ddlEmpName.SelectedValue.ToString() != null && ddlEmpName.SelectedValue.ToString() != string.Empty)
            {
                EMP_CODE = ddlEmpName.SelectedValue.ToString();
            }
            else
            {
                EMP_CODE = string.Empty;
            }
            if (ddlLeaveType.SelectedValue.ToString() != null && ddlLeaveType.SelectedValue.ToString() != string.Empty)
            {
                LV_TYPE = ddlLeaveType.SelectedValue.ToString();
            }
            else
            {
                LV_TYPE = string.Empty;
            }
            if (ddlLeaveStatus.SelectedValue.ToString() != null && ddlLeaveStatus.SelectedValue.ToString() != string.Empty)
            {
                LV_STATUS = ddlLeaveStatus.SelectedValue.ToString();
            }
            else
            {
                LV_STATUS = string.Empty;
            }
            if (DTCRFromDate.Text != null && DTCRFromDate.Text != string.Empty && DTCRToDate.Text != null && DTCRToDate.Text != string.Empty)
            {
                LV_FROM_DATE = DTCRFromDate.Text.Trim();
                LV_TO_DATE = DTCRToDate.Text.Trim();
            }
            else
            {
                LV_FROM_DATE = string.Empty;
                LV_TO_DATE = string.Empty;
            }
            //if (DTCRToDate.Text.ToString() != null && DTCRToDate.Text.ToString() != string.Empty)
            //{
            //    LV_TO_DATE = DTCRToDate.Text.ToString();
            //}
            //else
            //{
            //    LV_TO_DATE = string.Empty;
            //}
            //if (DTCRFromDate.Text.ToString() != null && DTCRFromDate.Text.ToString() != string.Empty)
            //{
            //    LV_FROM_DATE = DTCRFromDate.Text.ToString();
            //}
            //else
            //{
            //    LV_FROM_DATE = string.Empty;
            //}
           
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.HR_EMP_MST.GetUserDetail(EMP_CODE, LV_TYPE, LV_STATUS,  LV_TO_DATE , LV_FROM_DATE);

            if (dt.Rows.Count > 0)
            {
                grLeave_Status_Details.DataSource = dt;
                grLeave_Status_Details.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                grLeave_Status_Details.Visible = true;
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

            ddlEmpName.SelectedIndex = -1;
            // ddlUserType.SelectedIndex = -1;
            grLeave_Status_Details.Visible = false;
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
            GetUserDetail();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void grLeave_Status_Details_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GetUserDetail();

            grLeave_Status_Details.PageIndex = e.NewPageIndex;
            grLeave_Status_Details.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}
