using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Module_OrderDevelopment_Reports_OrderVomReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //ddlPINO();
            //ddlBaseArtCode();
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "../Reports/CryOrderBomReport.aspx?PI_NO=" + txtPiNo.Text.ToString() + "&BASE_ARTICAL_CODE=" + txtBaseArtCode.Text.ToString(); 
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Open Report.\r\nSee error log for detail."));
        }
    }
    //private void ddlPINO()
    //{
    //    try
    //    {
    //        ddlPI_No.Items.Clear();
    //        DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetPItype();
    //        DataView dataView = new DataView(dt);
    //        dataView.Sort = " PI_NO ASC";
           
    //        if (dataView != null )
    //        {
    //            ddlPI_No.DataTextField = "PI_NO";
    //            ddlPI_No.DataValueField = "PI_NO";
    //            ddlPI_No.DataSource = dataView;
    //            ddlPI_No.DataBind();

    //        }
    //        ddlPI_No.Items.Insert(0, new ListItem("All", ""));
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    //private void ddlBaseArtCode()
    //{
    //    try
    //    {
    //        ddlBaseArtyCode.Items.Clear();
    //        DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetPItype();
    //        DataView dataView = new DataView(dt);

    //        dataView.Sort = " BASE_ARTICAL_CODE ASC";
    //        if (dataView != null )
    //        {
    //            ddlBaseArtyCode.DataTextField = "BASE_ARTICAL_CODE";
    //            ddlBaseArtyCode.DataValueField = "BASE_ARTICAL_CODE";
    //            ddlBaseArtyCode.DataSource = dataView;
    //            ddlBaseArtyCode.DataBind();

    //        }
    //        ddlBaseArtyCode.Items.Insert(0, new ListItem("All", ""));
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
       
    }
    protected void imgbtnExit_Click(object sender, ImageClickEventArgs e)
    {

    }
}
