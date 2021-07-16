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

public partial class Module_OrderDevelopment_LabDip_Reports_LR_APPROVAL_REPORT : System.Web.UI.Page
{
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
           
            BindOrderNo();

        }
        catch
        {
            throw;
        }

    }


    private void BindOrderNo()
    {
        try
        {


            string OrderNo = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.GetApprovedOrder(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year);



            txtOrderNoFrom.Text = OrderNo;
            txtOrderNoTo.Text = OrderNo;
        }
        catch
        {
            throw;
        }
    }



    protected void btnGetReport_Click1(object sender, EventArgs e)
    {
          if (txtOrderNoFrom.Text != "" || txtOrderNoTo.Text != "")
            {
                string No = txtOrderNoFrom.Text;
               
                

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
                    string URL = "../Reports/LR_PRINT_REPORT.aspx" + QueryString;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=1200,height=4000');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.close();", true);
                
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please Enter the Order No");
            }
      
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        InitialiseData();
    }
    //protected void imgbtnExit_Click(object sender, ImageClickEventArgs e)
    //{

    //}
    //protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    //{

    //}
}
