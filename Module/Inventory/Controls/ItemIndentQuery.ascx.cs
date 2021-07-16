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


public partial class Module_Inventory_Controls_ItemIndentQuery : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            int YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            string COMPCODE = Request.QueryString["COMPCODE"].ToString();
            string BRANCHCODE = Request.QueryString["BRANCHCODE"].ToString();
            string INDTYPE = Request.QueryString["INDTYPE"].ToString();
            int INDNUMB = Convert.ToInt32(Request.QueryString["INDNUMB"]);
            bindGvItemIndentMst(YEAR,COMPCODE, BRANCHCODE, INDTYPE, INDNUMB);
        }
    }

    private void bindGvItemIndentMst(int YEAR, string COMPCODE, string BRANCHCODE, string INDTYPE, int INDNUMB)
    {

        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_ITEM_IND_MST.SelectItemIndentMst(YEAR, COMPCODE, BRANCHCODE, INDTYPE, INDNUMB);
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
