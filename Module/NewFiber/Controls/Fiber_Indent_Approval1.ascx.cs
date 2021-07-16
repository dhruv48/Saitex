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
using errorLog;
public partial class Module_Fiber_Controls_Fiber_Indent_Approval1 : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["urLoginid"] != null)
        {
            if (!IsPostBack)
            {
                lblMode.Text = "Find";
                bindFabricIndentApproval();
            }
                 
        }
        else
        {
            Response.Redirect("~/Default.aspx", false);
        }

        


    }

    private void BlanksControls()
    {
        try
        {
            int Totalrows = grdFiberIndentApproval.Rows.Count;
            for (int r = 0; r < Totalrows; r++)
            {
                GridViewRow thisGridViewRow = grdFiberIndentApproval.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label IndentId = (Label)thisGridViewRow.FindControl("lblIndentTrnId");
                    TextBox ApprovedQty = (TextBox)thisGridViewRow.FindControl("txtApprovedQty");
                    CheckBox Approved = (CheckBox)thisGridViewRow.FindControl("chkApproved");
                    TextBox ConfirmDate = (TextBox)thisGridViewRow.FindControl("txtConfirmDate");
                    TextBox ConfirmBy = (TextBox)thisGridViewRow.FindControl("txtConfirmBy");
                    TextBox Remarks = (TextBox)thisGridViewRow.FindControl("txtRemarks");

                    ApprovedQty.Text = "";
                    Approved.Checked = false;
                    ConfirmDate.Text = "";
                    ConfirmBy.Text = "";
                    Remarks.Text = "";
                }
            }
        }
        catch (Exception ex)
        {

            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
            
        }
    }
    protected void imgbtnFindTop_Click(object sender, ImageClickEventArgs e)
    {

    }

    private DataTable CreateDataTable()
    {
        DataTable dtIndentDetail = new DataTable();
        dtIndentDetail.Columns.Add("COMP_CODE", typeof(string));
        dtIndentDetail.Columns.Add("BRANCH_CODE", typeof(string));
        dtIndentDetail.Columns.Add("YEAR", typeof(string));
        dtIndentDetail.Columns.Add("IND_TYPE" , typeof(string));
        dtIndentDetail.Columns.Add("IND_NUMB", typeof(int));
        dtIndentDetail.Columns.Add("FIBER_CODE", typeof(string));
        dtIndentDetail.Columns.Add("APPR_QTY", typeof(double));
        dtIndentDetail.Columns.Add("PUR_CONF_DATE", typeof(DateTime));
        dtIndentDetail.Columns.Add("PUR_CONF_BY", typeof(string));
        dtIndentDetail.Columns.Add("PUR_REMARK", typeof(string));
        return dtIndentDetail;

    }
    protected void imgbtnUpdate_Click1(object sender, ImageClickEventArgs e)
    {

        try
        {
            string msg = string.Empty;
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dtIndentDetail = CreateDataTable();
            int TotalRows = grdFiberIndentApproval.Rows.Count;
            for (int r = 0; r < TotalRows; r++)
            {
                GridViewRow thisGridViewRow = grdFiberIndentApproval.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblInd_NUMB = (Label)thisGridViewRow.FindControl("lblInd_NUMB");
                    Label lblFiberCode = (Label)thisGridViewRow.FindControl("lblFiberCode");
                    Label lblApprovedQty = (Label)thisGridViewRow.FindControl("lblApprovedQty");
                    TextBox ApprovedQty = (TextBox)thisGridViewRow.FindControl("txtApprovedQty");
                    CheckBox Approved = (CheckBox)thisGridViewRow.FindControl("chkApproved");
                    TextBox ConfirmDate = (TextBox)thisGridViewRow.FindControl("txtConfirmDate");
                    TextBox ConfirmBy = (TextBox)thisGridViewRow.FindControl("txtConfirmBy");
                    TextBox Remarks = (TextBox)thisGridViewRow.FindControl("txtRemarks");


                    if (Approved.Checked == true)
                    {
                        double iApprovedQty = Convert.ToDouble(ApprovedQty.Text.Trim());
                        double ireqQty = Convert.ToDouble(lblApprovedQty.Text.Trim());
                        if (iApprovedQty <= ireqQty)
                        {


                            //  int iApprovedQty = Convert.ToInt32(ApprovedQty.Text.Trim());
                            DateTime dConfirmDate = Convert.ToDateTime(ConfirmDate.Text.Trim());
                            string strConfirmBy = ConfirmBy.Text.Trim();
                            string strRemarks = Remarks.Text.Trim();


                            DataRow dr = dtIndentDetail.NewRow();

                            dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                            dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                            dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                            dr["IND_TYPE"] = lblFiberCode.ToolTip.Trim();
                            dr["IND_NUMB"] = int.Parse(lblInd_NUMB.Text.Trim());
                            dr["FIBER_CODE"] = lblFiberCode.Text.Trim();
                            dr["APPR_QTY"] = iApprovedQty;
                            dr["PUR_CONF_DATE"] = dConfirmDate;
                            dr["PUR_CONF_BY"] = strConfirmBy;
                            dr["PUR_REMARK"] = strRemarks;

                            dtIndentDetail.Rows.Add(dr);
                            ApprovedQty.Text = "";
                            Approved.Checked = false;
                            ConfirmBy.Text = "";
                            ConfirmDate.Text = "";
                            Remarks.Text = "";
                        }

                        else
                        {
                            msg += "Approved Quantity can not be more than requested Quantity for Item Code : " + lblFiberCode.Text + " of Indent Number : " + lblInd_NUMB.Text;
                        }


                    }
                    //else
                    //{
                    //    msg += "Please Click the checkbox before update.";
                    //}
   
                }
            }
            

            if (msg != string.Empty)
            {
                CommonFuction.ShowMessage(msg);
            }

            
            int iResult = SaitexBL.Interface.Method.FIBER_IND_MST.Update_TransactionForApproval(oUserLoginDetail.UserCode, dtIndentDetail);
            if (iResult > 0)
            {
                lblMode.Text = "Find";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('Indent Approved Successfully');", true);
                bindFabricIndentApproval();
            }
        }
            catch (Exception ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
        
        }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Fiber/Pages/Fiber_IndentApproval.aspx.aspx", false);
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
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }

   
    protected void grdFiberIndentApproval_Pageing(object sender, GridViewPageEventArgs e)
    {
        grdFiberIndentApproval.PageIndex = e.NewPageIndex;
        bindFabricIndentApproval();

    }
    private void bindFabricIndentApproval()
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dt = SaitexBL.Interface.Method.FIBER_IND_MST.GetIndentDataForApproval(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
           if(dt!= null && dt.Rows.Count > 0)
           {
           if (!dt.Columns.Contains("CONF_DATE"))
                    dt.Columns.Add("CONF_DATE", typeof(DateTime));
                if (!dt.Columns.Contains("CONF_BY"))
                    dt.Columns.Add("CONF_BY", typeof(string));

                foreach (DataRow dr in dt.Rows)
                {
                    string ConfBy = dr["CONF_BY"].ToString();
                    if (ConfBy == "")
                        dr["CONF_BY"] = oUserLoginDetail.Username;
                    double APPR_QTY = double.Parse(dr["APPR_QTY"].ToString());
                    if (APPR_QTY <= 0)
                        dr["APPR_QTY"] = double.Parse(dr["RQST_QTY"].ToString());

                    dr["CONF_DATE"] = System.DateTime.Now.Date.ToShortDateString();
                }
               grdFiberIndentApproval.DataSource = dt;
               grdFiberIndentApproval.DataBind();
               lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
           }
             else
            {
                lblTotalRecord.Text = "No Indent for approval";
               grdFiberIndentApproval.DataSource = null;
               grdFiberIndentApproval.DataBind();
           }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('" + ex.Message + "');", true);
        }
    
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");
        imgbtnDelete.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");

    }


    
}
