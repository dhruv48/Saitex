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
using Common;
using errorLog;
using System.IO;



public partial class Module_OrderDevelopment_LabDip_Controls_LabDip_Liabrary : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    public static string SHADE = string.Empty;
    public static string DEPTH = string.Empty;
    public static string Fdate;
    public static string Tdate;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                Initial_Controls();
                GetLabDip_GridData();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nplease see error log"));
        }
    }

    private void Initial_Controls()
    {
        try
        {
            LABDIPLIBRARY.Visible = true;
            ddldepth.SelectedIndex = -1;
            ddlshade.SelectedIndex = -1;
            imgbtnClear.Visible = true;
            imgbtnExit.Visible = true;
            imgbtnHelp.Visible = true;
            imgbtnPrint.Visible = true;
            BindShade();
            BindDepth();

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));
        }
    }

    private void Blank_Control()
    {
        try
        {
            LABDIPLIBRARY.Visible = true;
            TxtFromDate.Text = "";
            TxtToDate.Text = "";
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));
        }
    }

    protected void ddldepth_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {


        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlshade_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            throw;
        }

    }

    private void BindShade()
    {
        try
        {
            /////////////////// BIND SHADE ////////////////////////
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("YARN_SHADETYPE", oUserLoginDetail.COMP_CODE);


            ddlshade.Items.Clear();
            ddlshade.DataSource = dt;
            ddlshade.DataValueField = "MST_CODE";
            ddlshade.DataTextField = "MST_DESC";
            ddlshade.DataBind();
            ddlshade.Items.Insert(0, new ListItem("----SELECT--------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }

    private void BindDepth()
    {
        try
        {
            /////////////////// BIND DEPTH////////////////////////

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("YARN_DEPTH", oUserLoginDetail.COMP_CODE);



            ddldepth.Items.Clear();
            ddldepth.DataSource = dt;
            ddldepth.DataTextField = "MST_DESC";
            ddldepth.DataValueField = "MST_CODE";
            ddldepth.DataBind();
            ddldepth.Items.Insert(0, new ListItem("----SELECT--------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }

    private void GetLabDip_GridData()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.GetLabDip_GridData();
            LABDIPLIBRARY.DataSource = dt;
            LABDIPLIBRARY.DataBind();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void LABDIPLIBRARY_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
        }
    }

    private void ViewInGrid()
    {
        try
        {
            if (ddlshade.SelectedValue.ToString() != null && ddlshade.SelectedValue.ToString() != string.Empty)
            {
                SHADE = ddlshade.SelectedValue.ToString();
            }
            else
            {
                SHADE = string.Empty;
            }

            if (ddldepth.SelectedValue.ToString() != null && ddldepth.SelectedValue.ToString() != string.Empty)
            {
                DEPTH = ddldepth.SelectedValue.ToString();
            }
            else
            {
                DEPTH = string.Empty;
            }

            if (TxtFromDate.Text.ToString() != null && TxtFromDate.Text.ToString() != string.Empty)
            {
                Fdate = TxtFromDate.Text.ToString();
            }
            else
            {
                Fdate = string.Empty;
            }

            if (TxtToDate.Text.ToString() != null && TxtToDate.Text.ToString() != string.Empty)
            {
                Tdate = TxtToDate.Text.Trim().ToString();
            }
            else
            {
                Tdate = string.Empty;
            }

            DataTable dt = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.ViewInGrid(DEPTH, SHADE, Fdate, Tdate);

            if (dt != null && dt.Rows.Count > 0)
            {
                LABDIPLIBRARY.DataSource = dt;
                LABDIPLIBRARY.DataBind();
            }
            else
            {
                LABDIPLIBRARY.DataSource = null;
                LABDIPLIBRARY.DataBind();
                Common.CommonFuction.ShowMessage("Data not Found by selected Item .");
            }
        }


        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));
        }
    }

    protected void Btnview_Click(object sender, EventArgs e)
    {
        try
        {

            ViewInGrid();


        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));

        }

    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Initial_Controls();
            Blank_Control();
            ViewInGrid();

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));
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
