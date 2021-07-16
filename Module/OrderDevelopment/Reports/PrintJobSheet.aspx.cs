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

public partial class Module_OrderDevelopment_Reports_PrintJobSheet : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.BATCH_CARD_MST oBATCH_CARD_MST;

    protected void Page_Load(object sender, EventArgs e)
    {
        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (Session["LoginDetail"] != null)
            {
                string QC = string.Empty;
                if (!IsPostBack)
                {
                    if (Request.QueryString["QC"] == null || Request.QueryString["QC"] == "")
                    {
                        txtFDATE.Visible = false;
                        txtTDATE.Visible = false;
                        txtFrom.Visible = true;
                        txtTo.Visible = true;
                        Label2.Text = "From";
                        Label3.Text = "To";
                        BindBatchCode();
                    }
                    else
                    {
                        QC = Request.QueryString["QC"].ToString();
                        txtFDATE.Visible = true;
                        txtTDATE.Visible = true;
                        txtFrom.Visible = false;
                        txtTo.Visible = false;

                        Label2.Text = "From Date";
                        Label3.Text = "To Date";
                        //BindBatchCode(QC);
                    }
                }
            }
        }
        catch
        {

        }
    }



    private void BindBatchCode()
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            oBATCH_CARD_MST = new SaitexDM.Common.DataModel.BATCH_CARD_MST();
            oBATCH_CARD_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oBATCH_CARD_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oBATCH_CARD_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            string BatchCode = SaitexBL.Interface.Method.BATCH_CARD_MST.GetNewBatchCode(oBATCH_CARD_MST);
            txtFrom.Text = (Convert.ToInt32(BatchCode) - 1).ToString();
            txtTo.Text = (Convert.ToInt32(BatchCode) - 1).ToString();
        }
        catch
        {
            throw;
        }
    }



    //private void BindBatchCode(string QC)
    //{
    //    try
    //    {
    //        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
    //        oBATCH_CARD_MST = new SaitexDM.Common.DataModel.BATCH_CARD_MST();
    //        oBATCH_CARD_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
    //        oBATCH_CARD_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
    //        oBATCH_CARD_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
    //        string BatchCode = SaitexBL.Interface.Method.BATCH_CARD_MST.GetNewBatchCodeQC(oBATCH_CARD_MST, QC);
    //        txtFrom.Text = (Convert.ToInt32(BatchCode) - 1).ToString();
    //        txtTo.Text = (Convert.ToInt32(BatchCode) - 1).ToString();
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (Request.QueryString["QC"] == null || Request.QueryString["QC"] == "")
                {
                    string QueryString = "";

                    QueryString += "?FromNo=" + txtFrom.Text.ToString();
                    QueryString += "&ToNo=" + txtTo.Text.ToString();

                    string URL = "JobCardPrintReport.aspx" + QueryString;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
                }

                else

                {
                    string msg = "";
                    string FromDate = string.Empty;
                    string ToDate = string.Empty;
                    if (txtFDATE.Text.ToString() != string.Empty && txtFDATE.Text.ToString() != "")
                        FromDate = txtFDATE.Text;
                    else
                    {
                        FromDate = string.Empty;
                        msg += "Invalid Starting Date.</ br>";
                    }
                    if (txtTDATE.Text.ToString() != string.Empty && txtTDATE.Text.ToString() != "")
                        ToDate = txtTDATE.Text;
                    else
                    {
                        ToDate = string.Empty;
                        msg += "Invalid End Number.</ br>";
                    }

                    if (msg == "")
                    {

                        string QueryString = "";
                        QueryString += "?FromDate=" + FromDate;
                        QueryString += "&ToDate=" + ToDate;

                        string URL = "JOB_CARD_QC.aspx" + QueryString;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);

                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage(msg);
                    }
                }
            }
        }
        catch
        {

        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //if (Session["LoginDetail"] != null)
            //{
            //    TRNTYPE = "RYS01";
            //    if (Request.QueryString["TRN_TYPE"] != null && Request.QueryString["TRN_TYPE"] != "")
            //    {
            //        TRNTYPE = Request.QueryString["TRN_TYPE"].ToString();
            //    }
            //    SetFromAndTo();
            //}

            txtFDATE.Text = "";
            txtTDATE.Text = "";
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
