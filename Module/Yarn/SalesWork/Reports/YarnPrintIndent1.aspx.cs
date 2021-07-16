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


public partial class Module_Yarn_SalesWork_Reports_YarnPrintIndent1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["IND_NUMB"] != null)
            {
                int IndentNumber = 0;
                int.TryParse(Request.QueryString["IND_NUMB"].ToString(), out IndentNumber);
                txtFrom.Text = IndentNumber.ToString();
                txtTo.Text = IndentNumber.ToString();
            }
            else
            {
                GetLastIndentNo();
            }
        }
    }

    private void GetLastIndentNo()
    {
        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        int NewIndentNo = int.Parse(SaitexBL.Interface.Method.YRN_INT_MST.GetNewIndentNumber(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, "GEN",oUserLoginDetail.DT_STARTDATE.Year  ));     
        txtFrom.Text = (NewIndentNo - 1).ToString();
        txtTo.Text = (NewIndentNo - 1).ToString();
    }
  



    protected void imgbtnPrint_Click1(object sender, ImageClickEventArgs e)
    {
        string QueryString = "";
        bool flag = false;
        if (txtFrom.Text != "")
        {
            if (flag)
                QueryString = QueryString + "&";
            else
                QueryString = QueryString + "?";

            QueryString = QueryString + "From=" + txtFrom.Text;
            flag = true;
        }
        if (txtTo.Text != "")
        {
            if (flag)
                QueryString = QueryString + "&";
            else
                QueryString = QueryString + "?";

            QueryString = QueryString + "To=" + txtTo.Text;
            flag = true;
        }
        string URL = "../Reports/YarnIndentReport1.aspx" + QueryString;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.close();", true);

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
                Response.Redirect("~/Module/Admin/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {

    }
}
