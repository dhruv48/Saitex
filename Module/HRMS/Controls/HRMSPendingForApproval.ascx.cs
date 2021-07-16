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
using errorLog; 

public partial class Module_HRMS_Controls_HRMSPendingForApproval : System.Web.UI.UserControl
{
    private static string User_Code = string.Empty;
    private static string POSITION = string.Empty;
    private static string OpenYear = string.Empty;

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["LoginDetail"] != null)
            {
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                {
                    initial_page();
                    if (!IsPostBack)
                    {
                        User_Code = oUserLoginDetail.UserCode;
                        if (Session["POSITION"] != null)
                        {
                            POSITION = Session["POSITION"].ToString();
                            CountPending();
                        }
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(Ex, "Problem In page loading"));
        }

    }
    private void initial_page()
    {
        try
        {
            trLeave.Visible = false;
            trLOAN.Visible = false;
            trOD.Visible = false;
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
            throw ex;
        }
    }
    private void CountPending()
    {
        try
        {
            DataTable DT = SaitexBL.Interface.Method.HR_EMP_LV.CountPending(POSITION);
            if (DT.Rows[0]["LEAVE"].ToString() != "0")
            {
                trLeave.Visible = true;
                LblLeaveApproval.Text = "PENDING LEAVE FOR APPROVAL <b>(" + DT.Rows[0]["LEAVE"].ToString() + ")</b>";
            }
            else
            {
                trLeave.Visible = false;

            }
            if (DT.Rows[0]["OD"].ToString() != "0")
            {
                trOD.Visible = true ;
                LblODApproval.Text = "PENDING OUT DOOR DUITY FOR APPROVAL <b>(" + DT.Rows[0]["OD"].ToString() + ")</b>";
            }
            else
            {
                trOD.Visible = false;

            }
            if (DT.Rows[0]["LOAN"].ToString() != "0")
            {
                trLOAN.Visible = true ;
                LblLoanApproval.Text = "PENDING LOAN FOR APPROVAL <b>(" + DT.Rows[0]["LOAN"].ToString() + ")</b>";
            }
            else
            {
                trLOAN.Visible = false;

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.ToString());
            throw ex;
        }

    }
}
