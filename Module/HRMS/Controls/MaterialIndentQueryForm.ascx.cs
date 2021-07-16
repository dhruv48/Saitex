using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Obout.Grid;
using Obout.Interface;
using System.Data.OracleClient;
using DBLibrary;

public partial class Inventory_Controls_MaterialIndentQueryForm : System.Web.UI.UserControl
{
    Grid grid1 = new Grid();
    csSaitex obj = null;

    //protected void Page_load(object sender, EventArgs e)
    //{
    //   bindGvGroupMaster();
       
    //}
   
    private void bindGvGroupMaster()
    {
        try
        {
           
            
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_ITEM_IND_MST.GetItemIndMst();
            
            Grid1.DataSource = dt;
            Grid1.DataBind();           

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('" + ex.Message + "');", true);

        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        bindGvGroupMaster();
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
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Help Msg');", true);
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string URL = "../Reports/IndentQueryForm_Mst.aspx";

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=1000,height=800');", true);
 
    }
}