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

public partial class Module_HRMS_Controls_HROFFERLET : System.Web.UI.UserControl
{
    private static string OFF_REF_NO = "";
    protected void Page_Load(object sender, EventArgs e)
    {
         
        try
        {
            if (Session["LoginDetail"] != null)
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["OFF_REF_NO"] != null && Request.QueryString["OFF_REF_NO"] != "")
                    {
                        OFF_REF_NO= Request.QueryString["OFF_REF_NO"].ToString();
                    }
                    SetFromAndTo();
                }
            }
        }
        catch
        {

        }
    }

    private void SetFromAndTo()
    {
        try
        {
           
            int OFF_REF_NO = 0;

            OFF_REF_NO = SaitexBL.Interface.Method.HR_OFFER_LET.GetNewRefNo();

            txtFrom.Text = OFF_REF_NO.ToString();
            txtTo.Text = OFF_REF_NO.ToString();
                  
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
   
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("/Saitex/Module/HRMS/Reports/HROfferLettterR.aspx");            
        }
        catch
        {

        }

    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
         try
         {
             if (Session["LoginDetail"] != null)
             {
                 OFF_REF_NO = "text";
                 if (Request.QueryString["OFF_REF_NO"] != null && Request.QueryString["OFF_REF_NO"] != "")
                 {
                     OFF_REF_NO = Request.QueryString["OFF_REF_NO"].ToString();
                 }
                 SetFromAndTo();
             }
         }
         catch
         {

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
                    Response.Redirect("~/Module/Admin/Welcome.aspx", false);
                }
            }
            catch (Exception ex)
            {
                errorLog.ErrHandler.WriteError(ex.Message);
            }
        }
  
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    
}
