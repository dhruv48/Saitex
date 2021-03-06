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
public partial class Module_Yarn_SalesWork_Controls_IndentClosing : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                lblMode.Text = "Find";
                bindYarnRestApprovedIndent();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nsee error log for detail"));
        }
    }
    
    private void BlanksConrols()
    {
        try
        {
            
            int totalRows = gvYarnIndentClosing.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvYarnIndentClosing.Rows[r];
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
        dtIndentDetail.Columns.Add("INDENT_TYPE", typeof(string));
        dtIndentDetail.Columns.Add("IND_NUMB", typeof(int));
        dtIndentDetail.Columns.Add("YARN_CODE", typeof(string));
        
        dtIndentDetail.Columns.Add("IND_CLOSE_FLAG", typeof(string));
        dtIndentDetail.Columns.Add("IND_CLOSE_BY", typeof(string));
        dtIndentDetail.Columns.Add("IND_CLOSE_DATE", typeof(string));
        dtIndentDetail.Columns.Add("IND_CLOSE_REMARKS", typeof(string));
        return dtIndentDetail;
    }
    
    protected void imgbtnUpdate_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            string msg = string.Empty;
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            DataTable dtIndentDetail = CreateDataTable();
            int totalRows = gvYarnIndentClosing.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvYarnIndentClosing.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblInd_NUMB = (Label)thisGridViewRow.FindControl("lblInd_NUMB");
                    Label lblItemCode = (Label)thisGridViewRow.FindControl("lblItemCode");
                    //TextBox lApprovedQty = (TextBox)thisGridViewRow.FindControl("TextBox1");

                   // TextBox ApprovedQty = (TextBox)thisGridViewRow.FindControl("txtApprovedQty");
                    CheckBox Approved = (CheckBox)thisGridViewRow.FindControl("chkApproved");
                    //TextBox ConfirmDate = (TextBox)thisGridViewRow.FindControl("txtConfirmDate");
                    //TextBox ConfirmBy = (TextBox)thisGridViewRow.FindControl("txtConfirmBy");
                    TextBox Remarks = (TextBox)thisGridViewRow.FindControl("txtRemarks");

                    if (Approved.Checked == true)
                    {
                       
                            //int iApprovedQty = Convert.ToInt32(ApprovedQty.Text.Trim());
                            //DateTime dConfirmDate = Convert.ToDateTime(ConfirmDate.Text.Trim());
                            //string strConfirmBy = ConfirmBy.Text.Trim();
                           // string strRemarks = Remarks.Text.Trim();

                            DataRow dr = dtIndentDetail.NewRow();

                            dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                            dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                            dr["INDENT_TYPE"] = lblItemCode.ToolTip.Trim();
                            dr["IND_NUMB"] = int.Parse(lblInd_NUMB.Text.Trim());
                            dr["YARN_CODE"] = lblItemCode.Text.Trim();
                            dr["IND_CLOSE_FLAG"] = "3";
                            dr["IND_CLOSE_BY"] = oUserLoginDetail.UserCode;
                            dr["IND_CLOSE_REMARKS"] = Remarks.Text.Trim();  
                            
                                

                            dtIndentDetail.Rows.Add(dr);
                            
                            Approved.Checked = false;
                           
                            Remarks.Text = "";
                        
                    }
                }
            }
            if (msg != string.Empty)
            {
                CommonFuction.ShowMessage(msg);
            }
            int iResult = SaitexBL.Interface.Method.YRN_INT_MST.Update_TransactionForClosing(oUserLoginDetail.UserCode, dtIndentDetail);
            if (iResult > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ds", "window.alert('Indent Closed Successfully');", true);

                lblMode.Text = "Find";
                bindYarnRestApprovedIndent();
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
        Response.Redirect("../Reports/YarnPrintIndent1.aspx");
        //string URL = "../Reports/YarnPrintIndent1.aspx";
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=300,height=300');", true);
    }
    
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/yarn/SalesWork/Pages/Yarn_Indent_Approval.aspx", false);
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
    
    private void bindYarnRestApprovedIndent()
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dt = SaitexBL.Interface.Method.YRN_INT_MST.YarnRestApprovedIndent(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            if (dt != null && dt.Rows.Count > 0)
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
                gvYarnIndentClosing.DataSource = dt;
                gvYarnIndentClosing.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
                
            {
                lblTotalRecord.Text = "No Indent for Closing";
                gvYarnIndentClosing.DataSource = null;
                gvYarnIndentClosing.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('" + ex.Message + "');", true);
        }
    }

    protected void imgbtnDelete_Click1(object sender, ImageClickEventArgs e)
    {

    }
    
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        //imgbtnUpdate.Attributes.Add("Onclick", "return confirm('Are you sure to update the record?');");
        //imgbtnDelete.Attributes.Add("Onclick", "return confirm('Are you sure to delete the record ?');");
        //imgbtnPrint.Attributes.Add("Onclick", "return confirm('Are you sure to print the record?');");
        //imgbtnClear.Attributes.Add("Onclick", "return confirm('Are you sure to clear the record ?');");
        //imgbtnExit.Attributes.Add("Onclick", "return confirm('Are you sure to exit the record?');");
        //imgbtnHelp.Attributes.Add("Onclick", "return confirm('Are you sure to see help?');");      

    }
}
