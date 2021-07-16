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

public partial class Module_OrderDevelopment_LabDip_Reports_Customer_Request_For_Yarn : System.Web.UI.Page
{
    private static string PRODUCT_TYPE = "YARN_DYEING";
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        if (!IsPostBack)
        {
           
            
            InitialiseData();
        }
    }
    private void InitialiseData()
    {
        try
        {
            bindBusinessType();
            bindOrderType();
            BindOrderNo();

        }
        catch
        {
            throw;
        }

    }
    private void bindBusinessType()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("BUSINESS_TYPE", oUserLoginDetail.COMP_CODE);
            ddlBusinessType.DataSource = dt;
            ddlBusinessType.DataValueField = "MST_CODE";
            ddlBusinessType.DataTextField = "MST_DESC";
            ddlBusinessType.DataBind();
            //ddlBusinessType.SelectedIndex = 2;
        }
        catch
        {
            throw;

        }
    }
    private void bindOrderType()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("ORDER_TYPE", oUserLoginDetail.COMP_CODE);
            ddlOrderType.Items.Clear();
           // ddlOrderType.Items.Insert(0, "---Select---");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string sType = dr["MST_CODE"].ToString();
                    if (string.Compare(sType, "Development", true) == 0)
                    {
                        ddlOrderType.Items.Add(new ListItem(dr["MST_DESC"].ToString(), dr["MST_CODE"].ToString()));
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }
    protected void ddlBusinessType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindOrderNo();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Business type selection.\r\nSee error log for detail."));

            
        }
    }


    private void BindOrderNo()
    {
        try
        {

            string CRLocationPrefix = string.Empty;
            string CRType = string.Empty;
            string ORDER_CAT = string.Empty;
            string msg = string.Empty;
            String REPORTFOR = string.Empty;
            CRLocationPrefix = oUserLoginDetail.SEQ_PREFIX.ToString();
            CRType = ddlOrderType.SelectedItem.ToString();
            ORDER_CAT = ddlBusinessType.SelectedValue.ToString();
            REPORTFOR = ddlReportFor.SelectedItem.Text.ToString();
            string OrderNo = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.GetNewSTOrderNo4(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, PRODUCT_TYPE, CRLocationPrefix, CRType, ORDER_CAT, REPORTFOR);

            

            txtOrderNoFrom.Text = OrderNo;
            txtOrderNoTo.Text = OrderNo;
        }
        catch
        {
            throw;
        }
    }

   
  
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnExit_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btnGetReport_Click1(object sender, EventArgs e)
    {
        if (ddlReportFor.SelectedItem.Text == "Customer Request For Yarn (Lab Dip)")
        {
            if (txtOrderNoFrom.Text != "" || txtOrderNoTo.Text != "")
            {
                string No = txtOrderNoFrom.Text;
                string Value = No.Substring(6, 6);
                if (Value != "000000")
                {

                    string QueryString = "";
                    bool flag = false;
                    if (txtOrderNoFrom.Text != "")
                    {
                        if (flag)
                            QueryString = QueryString + "&";
                        else
                            QueryString = QueryString + "?";

                        QueryString = QueryString + "From=" + txtOrderNoFrom.Text;
                        flag = true;
                    }
                    if (txtOrderNoTo.Text != "")
                    {
                        if (flag)
                            QueryString = QueryString + "&";
                        else
                            QueryString = QueryString + "?";

                        QueryString = QueryString + "To=" + txtOrderNoTo.Text;
                        flag = true;
                    }
                    string URL = "../Reports/Customer_Request_For_Yarn_Report.aspx" + QueryString;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=1200,height=800');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.close();", true);
                }


                else
                {
                    Common.CommonFuction.ShowMessage("There is no Record");

                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please Enter the Order No");
            }
        }
        else 
        {


            if (txtOrderNoFrom.Text != "" || txtOrderNoTo.Text != "")
            {
                string No = txtOrderNoFrom.Text;
                string Value = No.Substring(6, 6);
                if (Value != "000000")
                {

                    string QueryString = "";
                    bool flag = false;
                    if (txtOrderNoFrom.Text != "")
                    {
                        if (flag)
                            QueryString = QueryString + "&";
                        else
                            QueryString = QueryString + "?";

                        QueryString = QueryString + "From=" + txtOrderNoFrom.Text;
                        flag = true;
                    }
                    if (txtOrderNoTo.Text != "")
                    {
                        if (flag)
                            QueryString = QueryString + "&";
                        else
                            QueryString = QueryString + "?";

                        QueryString = QueryString + "To=" + txtOrderNoTo.Text;
                        flag = true;
                    }
                    string URL = "../Reports/RecipeEntry.aspx" + QueryString;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=800');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.close();", true);
                }


                else
                {
                    Common.CommonFuction.ShowMessage("There is no Record");

                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please Enter the Order No");
            }
         
        
        }
    }




    protected void ddlReportFor_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindOrderNo();
    }
}
    

