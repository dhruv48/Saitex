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

using errorLog;
using Common;
using DBLibrary;
using System.IO;

public partial class CommonControls_FAConfirmAndApproval : System.Web.UI.UserControl
{
    private static DataTable dtNavId = null;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["LoginDetail"] != null)
            {
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                {
                    if (!IsPostBack)
                    {
                        Initilagepage();
                        CheckUserNavRight();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }
    private void Initilagepage()
    {
        try
        {
            CreateDatatableforNavId();
        }
        catch
        {
            throw;
        }
    }
    private void CheckUserNavRight()
    {
        try
        {
            string UserCode = oUserLoginDetail.UserCode.ToString();

            SaitexDM.Common.DataModel.FA_Journal_MST oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();

            oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            SaitexDM.Common.DataModel.FA_ADVANCED_ADVICE oFA_ADVANCED_ADVICE = new SaitexDM.Common.DataModel.FA_ADVANCED_ADVICE();

            oFA_ADVANCED_ADVICE.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_ADVANCED_ADVICE.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_ADVANCED_ADVICE.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            SaitexDM.Common.DataModel.UserAccessRight oUserAccessRight = new SaitexDM.Common.DataModel.UserAccessRight();
            oUserAccessRight.UserCode = oUserLoginDetail.UserCode;
            DataTable dtUser_Nav_Rgt = SaitexBL.Interface.Method.UserNavigationRight.GetUserNavigationRightByUserCode(oUserAccessRight);
            if (dtUser_Nav_Rgt != null && dtUser_Nav_Rgt.Rows.Count > 0)
            {
                for (int i = 0; i < dtNavId.Rows.Count; i++)
                {
                    DataView dv = new DataView(dtUser_Nav_Rgt);
                    string filter = string.Empty;
                    filter += "NAV_ID=" + int.Parse(dtNavId.Rows[i]["NavId"].ToString());
                    filter += " AND NAV_URL='" + dtNavId.Rows[i]["NAV_URL"].ToString() + "'";

                    dv.RowFilter = filter;
                    if (dv != null && dv.Count > 0)
                    {
                        if (dv[0]["NAV_URL"].ToString() == "~/Module/FA/Pages/VoucherConfirmation.aspx") // For Voucher Confirmation..
                        {
                            DataTable dtVoucherConfirmation = SaitexBL.Interface.Method.FA_Journal_DTL.SelectJournalMst(oFA_Journal_MST);
                            if (dtVoucherConfirmation != null && dtVoucherConfirmation.Rows.Count > 0)
                            {
                                lblVoucherConfirmation.Text = "Voucher Confirmation Pending" + "<B>(" + dtVoucherConfirmation.Rows.Count.ToString() + ")</B>";
                                lblVoucherConfirmation.Visible = true;
                                lnkbtnVoucherConfirmation.Visible = true;
                            }
                            else
                            {
                                lblVoucherConfirmation.Visible = false;
                                lnkbtnVoucherConfirmation.Visible = false;
                            }
                        }
                        if (dv[0]["NAV_URL"].ToString() == "~/Module/FA/Pages/VoucherApproval.aspx") // For Voucher Approval..
                        {
                            DataTable dtVoucherApproval = SaitexBL.Interface.Method.FA_Journal_DTL.SelectJournalMstVoucherApproval(oFA_Journal_MST);
                            if (dtVoucherApproval != null && dtVoucherApproval.Rows.Count > 0)
                            {
                                lblVoucherApproval.Text = "Voucher Approval Pending" + "<B>(" + dtVoucherApproval.Rows.Count.ToString() + ")</B>";
                                lnkbtnVoucherApproval.Visible = true;
                                lblVoucherApproval.Visible = true;
                            }
                            else
                            {
                                lblVoucherApproval.Visible = false;
                                lnkbtnVoucherApproval.Visible = false;
                            }
                        }
                        if (dv[0]["NAV_URL"].ToString() == "~/Module/FA/Pages/AdvancedAdviceConfirmation.aspx")   // For Advice Confirmation..
                        {
                            DataTable dtAdviceConfirm = SaitexBL.Interface.Method.FA_ADVANCED_ADVICE.SelectAdvicesWithFY(oFA_ADVANCED_ADVICE);
                            if (dtAdviceConfirm != null && dtAdviceConfirm.Rows.Count > 0)
                            {
                                lblAdviceConfirmation.Text = "Advanced Advice Confirmation Pending" + "<B>(" + dtAdviceConfirm.Rows.Count.ToString() + ")</B>";
                                lblAdviceConfirmation.Visible = true;
                                lnkbtnAdviceConfirmation.Visible = true;
                            }
                            else
                            {
                                lblAdviceConfirmation.Visible = false;
                                lnkbtnAdviceConfirmation.Visible = false;
                            }
                        }
                        if (dv[0]["NAV_URL"].ToString() == "~/Module/FA/Pages/AdvancedAdviceApproval.aspx")   // For Advice Approval..
                        {
                            DataTable dtAdviceApproval = SaitexBL.Interface.Method.FA_ADVANCED_ADVICE.SelectAdvicesWithFYOnlyConfirm(oFA_ADVANCED_ADVICE);
                            if (dtAdviceApproval != null && dtAdviceApproval.Rows.Count > 0)
                            {
                                lblAdviceApproval.Text = "Advanced Advice Approval Pending" + "<B>(" + dtAdviceApproval.Rows.Count.ToString() + ")</B>";
                                lblAdviceApproval.Visible = true;
                                lnkbtnAdviceApproval.Visible = true;
                            }
                            else
                            {
                                lblAdviceApproval.Visible = false;
                                lnkbtnAdviceApproval.Visible = false;
                            }
                        }
                    }
                }
            }
            else
            {
                lblmsg.Text = "There is no Pending Vouchers And Advices for Confirmation Or Approval..";
            }
        }
        catch
        {
            throw;
        }
    }
    private void CreateDatatableforNavId()
    {
        try
        {
            dtNavId = new DataTable();
            dtNavId.Columns.Add("id", typeof(int));
            dtNavId.Columns.Add("NavId", typeof(int));
            dtNavId.Columns.Add("UserCode", typeof(string));
            dtNavId.Columns.Add("NAVNAME", typeof(string));
            dtNavId.Columns.Add("NAV_URL", typeof(string));
            DataRow row1;
            row1 = dtNavId.NewRow();
            row1["id"] = 1;
            row1["NavId"] = 133;  // Static Nav id for Voucher Confirmation..
            row1["UserCode"] = oUserLoginDetail.UserCode.ToString();
            row1["NAVNAME"] = "Voucher Confirmation"; //Static Nav Name for Voucher Confirmation..
            row1["NAV_URL"] = "~/Module/FA/Pages/VoucherConfirmation.aspx";
            dtNavId.Rows.Add(row1);

            DataRow row2;
            row2 = dtNavId.NewRow();
            row2["id"] = 2;
            row2["NavId"] = 136;// Static Nav id for Voucher Approval..
            row2["UserCode"] = oUserLoginDetail.UserCode.ToString();
            row2["NAVNAME"] = "Voucher Approval";  //Static Nav Name for Voucher Approval..
            row2["NAV_URL"] = "~/Module/FA/Pages/VoucherApproval.aspx";
            dtNavId.Rows.Add(row2);

            DataRow row3;
            row3 = dtNavId.NewRow();
            row3["id"] = 3;
            row3["NavId"] = 202;// Static Nav id for Advice Confirmation..
            row3["UserCode"] = oUserLoginDetail.UserCode.ToString();
            row3["NAVNAME"] = "Advanced Advice Confirmation";  //Static Nav Name for Advice Confirmation..
            row3["NAV_URL"] = "~/Module/FA/Pages/AdvancedAdviceConfirmation.aspx";
            dtNavId.Rows.Add(row3);

            DataRow row4;
            row4 = dtNavId.NewRow();
            row4["id"] = 4;
            row4["NavId"] = 203;// Static Nav id for Advice Approval..
            row4["UserCode"] = oUserLoginDetail.UserCode.ToString();
            row4["NAVNAME"] = "Advanced Advice Approval";  //Static Nav Name for Advice Approval..
            row4["NAV_URL"] = "~/Module/FA/Pages/AdvancedAdviceApproval.aspx";
            dtNavId.Rows.Add(row4);
        }
        catch
        {
            throw;
        }
    }
}
