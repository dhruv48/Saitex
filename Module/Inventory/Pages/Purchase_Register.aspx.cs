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
using System.Data.OracleClient;
using errorLog;
using Common;


public partial class Module_Inventory_Reports_Purchase_Register : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.HR_EMP_MST HR_EMP_MST = new SaitexDM.Common.DataModel.HR_EMP_MST();
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    String url = string.Empty;
    string trans_month = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        if (!Page.IsPostBack)
        {
            fillYear();
            getTransType();
            ddlMonth.SelectedValue = System.DateTime.Now.Month.ToString().Trim();
        }

    }
    private void fillYear()
    {
        for (int i = -15; i < 15; i++)
        {
            ddlYear.Items.Add(new ListItem(Convert.ToString((System.DateTime.Now.Year + i)), Convert.ToString((System.DateTime.Now.Year + i))));
        }
        // ddlYear.Items.Insert(0, new ListItem("---Select---", ""));
        ddlYear.SelectedValue = System.DateTime.Now.Year.ToString().Trim();

    }
    protected void btnView_Click(object sender, EventArgs e)
    {

        if (ddlMonth.SelectedValue.Trim() == "")
        {
            trans_month = "";
            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetPurchaseReportData(ddlTrntype.SelectedValue.Trim(), ddlYear.SelectedValue.Trim(), trans_month);
            if (dt.Rows.Count > 0)
            {
                url = "../Reports/Purchase_Register.aspx?TRANS_TYPE=" + ddlTrntype.SelectedValue.Trim() + "&&TRANS_YEAR=" + ddlYear.SelectedValue.Trim() + "&&PUR_REG_TYPE=" + selReport.SelectedValue.Trim();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + url + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
            }
            else
            {
                CommonFuction.ShowMessage("No Data Found for selected period");
            }
        }
        else
        {
            trans_month = " and to_number(to_char(m.trn_date,'MM'))=" + ddlMonth.SelectedValue.Trim();
            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetPurchaseReportData(ddlTrntype.SelectedValue.Trim(), ddlYear.SelectedValue.Trim(), trans_month);
            if (dt.Rows.Count > 0)
            {
                url = "../Reports/Purchase_Register.aspx?TRANS_TYPE=" + ddlTrntype.SelectedValue.Trim() + "&&TRANS_YEAR=" + ddlYear.SelectedValue.Trim() + "&&PUR_REG_TYPE=" + selReport.SelectedValue.Trim() + "&&TRANS_MONTH=" + ddlMonth.SelectedValue.Trim();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + url + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
            }
            else
            {
                CommonFuction.ShowMessage("No Data Found for selected period");
            }
        }

    }
    private void getTransType()
    {
        try
        {
            //------------------- Bind Transaction Types --------------------
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_ITEM_IR_MST.getTransType();
            ddlTrntype.DataSource = dt;
            ddlTrntype.DataValueField = "TRN_TYPE";
            ddlTrntype.DataTextField = "TRN_DESC";
            ddlTrntype.DataBind();
            dt.Dispose();
            dt = null;
        }
        catch (OracleException Oex)
        {
            throw Oex;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}
