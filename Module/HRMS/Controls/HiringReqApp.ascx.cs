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
using Common;
using errorLog;
using System.IO;
using DBLibrary;

public partial class Module_HRMS_Controls_HiringReqApp : System.Web.UI.UserControl
{
    public static string strCompanyCode = string.Empty;
    public static string Position = string.Empty;
    public static string DeptCode = string.Empty;
    public static string strBranchCode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            strCompanyCode = oUserLoginDetail.COMP_CODE.Trim().ToString();
            strBranchCode = oUserLoginDetail.CH_BRANCHCODE.Trim().ToString();
            Position = Session["POSITION"].ToString();           
            
               Load_Records();
            
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Page Loading.\\r\\nSee error log for detail."));
        }
    }
    private void Load_Records()
    {
        try
        {
            DataTable DT = SaitexBL.Interface.Method.HiringReq.Load_Record("", Position );
            GrdViewHiring.DataSource = DT;
            GrdViewHiring.DataBind();
        }
        catch
        {
            throw;
        }
    }
    protected void GrdViewHiring_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Approv")
            {
                Update_Record(e.CommandArgument.ToString(),"2");
            }
            if (e.CommandName == "Reject")
            {
                Update_Record (e.CommandArgument.ToString(),"3");                
            }
            Load_Records();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Grid Command"));
        }
    }
    private bool Update_Record(string HIR_ID,string status)
    {
        bool Res = false;
        try
        {
            Res = SaitexBL.Interface.Method.HiringReq.Update_Record(HIR_ID,status );
            return Res;
        }
        catch
        {
            throw;
        }
    }
}
