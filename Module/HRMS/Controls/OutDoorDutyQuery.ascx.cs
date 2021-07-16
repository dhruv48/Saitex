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

public partial class Module_HRMS_Controls_OutDoorDutyQuery : System.Web.UI.UserControl
{
    private static SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            try
            {
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                string COMP_CODE = oUserLoginDetail.COMP_CODE;
                string BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                bindGvOD(COMP_CODE, BRANCH_CODE);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
    private void bindGvOD(string COMP_CODE, string BRANCH_CODE)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EMPOUTDOORDUTY.ODQuery(COMP_CODE, BRANCH_CODE);
            Grid1.DataSource = dt;
            Grid1.DataBind();

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
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
}
