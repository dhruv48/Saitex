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


public partial class Module_OrderDevelopment_LabDip_Controls_RecipeEntry : System.Web.UI.UserControl
{
   SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
  
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            
            if (!IsPostBack)
            {
                if (Request.QueryString["From"] != null && Request.QueryString["To"] != null)
                {
                    
                    int fromRecipeNo=Convert.ToInt32(Request.QueryString["From"].ToString());
                    int toRecipeNo = Convert.ToInt32(Request.QueryString["To"].ToString());
                    txtFrom.Text = fromRecipeNo.ToString();
                    txtTo.Text = toRecipeNo.ToString();
                }
                else
                {
                    GetRecipeNo();
                }
            }
            
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    private void GetRecipeNo()
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        SaitexDM.Common.DataModel.OD_RECIPE_ENTRY_MST oOD_RECIPE_ENTRY_MST = new SaitexDM.Common.DataModel.OD_RECIPE_ENTRY_MST();
        oOD_RECIPE_ENTRY_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
        oOD_RECIPE_ENTRY_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
        oOD_RECIPE_ENTRY_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
        int RecipeNo = SaitexBL.Interface.Method.OD_RECIPE_ENTRY_MST.GetNewRECNO(oOD_RECIPE_ENTRY_MST);
        RecipeNo = RecipeNo - 1;
        txtFrom.Text = RecipeNo.ToString();
        txtTo.Text = RecipeNo.ToString();
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
         
            string URL = "../Reports/RecipeEntry.aspx?From=" + txtFrom.Text.Trim() + "&TO=" + txtTo.Text.Trim();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=1000,height=800');", true);
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
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
