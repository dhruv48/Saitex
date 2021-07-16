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

public partial class Module_HRMS_Pages_EmpOptionalLeaveDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpCode"] != null)
        {
            if (!Page.IsPostBack)
            {
                Load_Optional_Leave();
                Load_Employee_Optional_Leave();
            }
        }
        else
        {
            Response.Redirect("/Saitex/Module/HRMS/Pages/Default.aspx", false);
        }
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnInsert .Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit')");
        imgbtnHelp.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Help')");
    }
    private void Load_Optional_Leave()
    {
        DataTable DTable = SaitexBL.Interface.Method.HR_EMP_OPT_LV_DTL.Optional_Leave_Detail();
        if (DTable.Rows.Count>0)
        {
            lstOptionalLeave.DataSource = DTable;
            lstOptionalLeave.DataValueField = "HLD_ID";
            lstOptionalLeave.DataTextField = "HLD_NAME";
            lstOptionalLeave.DataBind();
        }
    }
    private void Load_Employee_Optional_Leave()
    {
        DataTable DTable = SaitexBL.Interface.Method.HR_EMP_OPT_LV_DTL.EMP_Optional_Leave_Detail(Session["EmpCode"].ToString());
        
        if (DTable.Rows.Count > 0)
        {
            gvOptionalLeaveMaster.DataSource = DTable;
            gvOptionalLeaveMaster.DataBind();
        }
    }
    protected void imgbtnInsert_Click(object sender, ImageClickEventArgs e)
    {
        if (Save_Record())
        {
            Common.CommonFuction.ShowMessage("Record save Sucessfully");
            Load_Employee_Optional_Leave();
        }
        else
        {

            Common.CommonFuction.ShowMessage("Duplicate records/More then 3 records");
        }
        
    }
    private bool Save_Record()
    {
        bool Res = false;
        try
        {
            int i = 0;
            foreach (ListItem lt in lstOptionalLeave.Items)
            {
                if (lt.Selected == true)
                {
                    if (i < 3)
                    {
                        bool Result = SaitexBL.Interface.Method.HR_EMP_OPT_LV_DTL.Save_Option_Leave_Record(0, Session["EmpCode"].ToString(), int.Parse(lstOptionalLeave.SelectedValue.ToString()), "ABCD");
                        if (Result)
                        {
                            i = i + 1;
                            Res = true;
                        }                        
                    }
                    else
                    {
                        Res = false;
                        break;                        
                    }

                }
            }
            return Res;
        }
        catch (Exception Ex)
        {
            return false;
            Common.CommonFuction.ShowMessage(Ex.Message.ToString());
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string URL = "";
        URL = "EmpOptionalLeaveRpt.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=600,height=300');", true);
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
                Response.Redirect("/Saitex/Module/HRMS/Pages/EmployeeHomePage.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }

    }
}
