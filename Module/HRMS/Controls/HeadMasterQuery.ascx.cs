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

public partial class Module_HRMS_Controls_HeadMasterQuery : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
         
            bindGvHeadNameMaster('y');
           
        }

    }
    private void bindGvHeadNameMaster(char chView)
    {

        try
        {

            SaitexDM.Common.DataModel.HR_HEAD_MST oHR_HEAD_MST = new SaitexDM.Common.DataModel.HR_HEAD_MST();
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.HR_HEAD_MST.SelectHeadNameMaster(chView);
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
