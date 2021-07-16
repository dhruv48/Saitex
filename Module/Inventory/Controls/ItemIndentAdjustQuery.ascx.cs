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


public partial class Module_Inventory_Controls_ItemIndentAdjustQuery : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            int YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            string COMP_CODE = Request.QueryString["COMP_CODE"].ToString();
            string BRANCH_CODE = Request.QueryString["BRANCH_CODE"].ToString();
            string PO_TYPE = Request.QueryString["PO_TYPE"].ToString();
            int PO_NUMB = Convert.ToInt32(Request.QueryString["PO_NUMB"]);
            bindGvItemIndentAdjust(YEAR,COMP_CODE,BRANCH_CODE,PO_TYPE,PO_NUMB);
        }

    }
    private void bindGvItemIndentAdjust(int YEAR,string COMP_CODE, string BRANCH_CODE, string PO_TYPE, int PO_NUMB)
    {

        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_ITEM_IND_PO_ADJUST.SelectItemIndentAdjust(YEAR,COMP_CODE, BRANCH_CODE, PO_TYPE, PO_NUMB);
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

    protected void Grid1_Select(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        ArrayList ar = Grid1.SelectedRecords;
        Hashtable ht = (Hashtable)ar[0];
        ViewState["COMP_CODE"] = ht["COMP_CODE"].ToString().Trim();
        string COMPCODE = ViewState["COMP_CODE"].ToString();
        ViewState["BRANCH_CODE"] = ht["BRANCH_CODE"].ToString().Trim();
        string BRANCHCODE = ViewState["BRANCH_CODE"].ToString();
        ViewState["IND_TYPE"] = ht["IND_TYPE"].ToString().Trim();
        string POTYPE = ViewState["IND_TYPE"].ToString();
        ViewState["IND_NUMB"] = ht["IND_NUMB"].ToString().Trim();
        int PONUMB = Convert.ToInt32(ViewState["IND_NUMB"].ToString());
        //string URL = "../Queries/ItemIndentAdjustQuery.aspx?COMP_CODE=" + COMPCODE + "?BRANCH_CODE=" + BRANCHCODE + "?PO_TYPE=" + POTYPE + "?PO_NUMB=" + PONUMB + "?ITEM_CODE=" + ITEMCODE;
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=1000,height=800');", true);
        Response.Redirect("../Queries/IndentQueryForm.aspx?COMP_CODE=" + COMPCODE + "&BRANCH_CODE=" + BRANCHCODE + "&PO_TYPE=" + POTYPE + "&PO_NUMB=" + PONUMB);
    }
}
