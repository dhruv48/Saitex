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


public partial class Module_Inventory_Controls_POCashQuery : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

        if (!IsPostBack)
        {
            Grid2.Visible = false;
            bindGvPO();

        }

    }
    private void bindGvPO()
    {

        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.Material_Purchase_Order.SelectPO(oUserLoginDetail.DT_STARTDATE.Year,oUserLoginDetail.COMP_CODE,oUserLoginDetail.CH_BRANCHCODE);
            Grid1.DataSource = dt;
            Grid1.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;
        }

    }
    private void bindGvPOTRN(int YEAR, string COMPCODE, string BRANCHCODE, string POTYPE, int PONUMB)
    {

        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.Material_Purchase_Order.SelectPOTRN(YEAR, COMPCODE, BRANCHCODE, POTYPE, PONUMB);
            Grid2.DataSource = dt;
            Grid2.DataBind();

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
        ViewState["YEAR"] = ht["YEAR"].ToString().Trim();
        int YEAR = int.Parse(ViewState["YEAR"].ToString());
        ViewState["COMP_CODE"] = ht["COMP_CODE"].ToString().Trim();
        string COMPCODE = ViewState["COMP_CODE"].ToString();
        ViewState["BRANCH_CODE"] = ht["BRANCH_CODE"].ToString().Trim();
        string BRANCHCODE = ViewState["BRANCH_CODE"].ToString();
        ViewState["PO_TYPE"] = ht["PO_TYPE"].ToString().Trim();
        string POTYPE = ViewState["PO_TYPE"].ToString();
        ViewState["PO_NUMB"] = ht["PO_NUMB"].ToString().Trim();
        int PONUMB = Convert.ToInt32(ViewState["PO_NUMB"].ToString());
        bindGvPOTRN(YEAR, COMPCODE, BRANCHCODE, POTYPE, PONUMB);
        Grid2.Visible = true;

    }
    protected void Grid2_Select(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        ArrayList ar = Grid2.SelectedRecords;
        Hashtable ht = (Hashtable)ar[0];
        ViewState["COMP_CODE"] = ht["COMP_CODE"].ToString().Trim();
        string COMPCODE = ViewState["COMP_CODE"].ToString();
        ViewState["BRANCH_CODE"] = ht["BRANCH_CODE"].ToString().Trim();
        string BRANCHCODE = ViewState["BRANCH_CODE"].ToString();
        ViewState["PO_TYPE"] = ht["PO_TYPE"].ToString().Trim();
        string POTYPE = ViewState["PO_TYPE"].ToString();
        ViewState["PO_NUMB"] = ht["PO_NUMB"].ToString().Trim();
        int PONUMB = Convert.ToInt32(ViewState["PO_NUMB"].ToString());
        Response.Redirect("../Queries/ItemIndentAdjustQuery.aspx?COMP_CODE=" + COMPCODE + "&BRANCH_CODE=" + BRANCHCODE + "&PO_TYPE=" + POTYPE + "&PO_NUMB=" + PONUMB);
    }
}
