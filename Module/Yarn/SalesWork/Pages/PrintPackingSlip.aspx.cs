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
using Common;

public partial class Module_Yarn_SalesWork_Pages_PrintPackingSlip : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    //private string TRNTYPE = "";
    //public string TRN_TYPE
    //{
    //    get { return TRNTYPE; }
    //    set { TRNTYPE = value; }
    //}

    //private static string TRNTYPE1 = "";
    //public string TRN_TYPE1
    //{
    //    get { return TRNTYPE1; }
    //    set { TRNTYPE1 = value; }
    //}
    int From = 0;
    int To = 0;
    string url = "";
    string TRN_TYPE = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["TRN_TYPE"] != null)
        {
            TRN_TYPE = Request.QueryString["TRN_TYPE"].ToString();
        }
        if (!IsPostBack)
        {
           
            if (Request.QueryString["TRN_NUMB"] != null)
            {
                int PackingSlipNo = 0;
                PackingSlipNo = int.Parse(Request.QueryString["TRN_NUMB"].ToString());
                txtFrom.Text = PackingSlipNo.ToString();
                txtTo.Text = PackingSlipNo.ToString();
            }
            else
            {
                lastPackingSlipNo();
            }
          }
    }
        private void lastPackingSlipNo()
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            int NewPackingSlipNo = int.Parse(SaitexBL.Interface.Method.YRN_IR_MST.GetNewPackingSlipNo(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRN_TYPE));
          txtFrom.Text = (NewPackingSlipNo - 1).ToString();
        txtTo.Text = (NewPackingSlipNo - 1).ToString();
        }
        protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
        {
            if (txtFrom.Text != null && txtFrom.Text != string.Empty)
            {
                From = int.Parse(txtFrom.Text.Trim());
            }
            else
            {
                From = 0;
            }
            if (txtTo.Text != null && txtTo.Text != string.Empty)
            {
                To = int.Parse(txtTo.Text.Trim());
            }
            else
            {
                To = 0;
            }
            try
            {
                url = "../Reports/YarnPackingSlipReport.aspx?From=" + From + "&To=" + To + "&TRN_TYPE=" + TRN_TYPE + "&REPORT_TYPE=" + ddltype.SelectedItem.Text; 

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + url + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=1000,height=600');", true);
            }
            catch (Exception ex)
            {
                CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing the data.\r\nSee error log for detail."));
                //lblMode.Text = ex.ToString();
            }
        }

        protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
        {

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
                    Response.Redirect("~/Module/Admin/pages/Welcome.aspx", false);
                }
            }
            catch (Exception ex)
            {
                CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in leaving page.\r\nSee error log for detail."));
                //lblMode.Text = ex.ToString();
            }
        }
        protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
        {

        }
        protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetFromAndTo();
        }
        private void SetFromAndTo()
        {
            //try
            //{
            //    SaitexDM.Common.DataModel.TX_ITEM_IR_MST oTX_ITEM_IR_MST = new SaitexDM.Common.DataModel.TX_ITEM_IR_MST();
            //    if (string.IsNullOrEmpty(TRNTYPE))
            //    {
            //        if (ddltype.SelectedIndex > 0)
            //        {
            //            oTX_ITEM_IR_MST.TRN_TYPE = ddltype.SelectedValue.Trim();
            //        }
            //        else
            //        {
            //            Common.CommonFuction.ShowMessage("Select type");
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        oTX_ITEM_IR_MST.TRN_TYPE = TRNTYPE;
            //    }
            //    oTX_ITEM_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            //    oTX_ITEM_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            //    oTX_ITEM_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            //    string TRN_NUMB = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetNewTRNNumber(oTX_ITEM_IR_MST);
            //    txtFrom.Text = (int.Parse(TRN_NUMB) - 1).ToString();
            //    txtTo.Text = (int.Parse(TRN_NUMB) - 1).ToString();
            //    if (TRNTYPE == "RYS31")
            //        lblTRNType.Text = "In Production Slip";
            //    else if (TRNTYPE == "RYJ32")
            //        lblTRNType.Text = "In Job Work Slip";
              
            //}
            //catch
            //{
            //}
        }
}

