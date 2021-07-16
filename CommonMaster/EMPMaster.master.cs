using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Data.OracleClient;
public partial class CommonMaster_EMPMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    { 
       

        Page.Title = "Textiles Application Management System (TexAMS)";
        GetModuleDetails();
        GetNavigationDetail();
    }

    private void GetModuleDetails()
    {
        if (Session["LoginDetail"] != null)
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            OracleConnection con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            string QueryString = Request.Url.Query;
            string LocalPath = Request.Url.LocalPath;
            LocalPath = LocalPath.Replace("/Saitex", "~");

            if (QueryString != "")
            {
                int iModuleId = 0;
                int iChildModuleId = 0;


                if (Request.QueryString["ModuleId"] != null)
                {
                    iModuleId = Convert.ToInt32(Request.QueryString["ModuleId"].ToString().Trim());

                    string strSQL = "";
                    strSQL = "select MDL_NAME from CM_MODULE_MST where ltrim(rtrim(MDL_ID))=:MDL_ID and ltrim(rtrim(DEL_STATUS))='0'";

                    OracleCommand cmd = new OracleCommand(strSQL, con);

                    OracleParameter param = new OracleParameter(":MDL_ID", OracleType.Number);
                    param.Direction = ParameterDirection.Input;
                    param.Value = iModuleId;
                    cmd.Parameters.Add(param);

                    string ModuleName = cmd.ExecuteOracleScalar().ToString();
                    cmd.Dispose();
                    oUserLoginDetail.Modulename = ModuleName + " > ";
                }
                if (Request.QueryString["ChildModuleId"] != null)
                {
                    iChildModuleId = Convert.ToInt32(Request.QueryString["ChildModuleId"].ToString().Trim());

                    string strSQL = "";
                    strSQL = "select CHILD_MDL_NAME from CM_CHILD_MDL_MST where ltrim(rtrim(CHILD_MDL_ID))=:CHILD_MDL_ID and ltrim(rtrim(MDL_ID))=:MDL_ID and ltrim(rtrim(DEL_STATUS))='0'";

                    OracleCommand cmd = new OracleCommand(strSQL, con);

                    OracleParameter param = new OracleParameter(":MDL_ID", OracleType.Number);
                    param.Direction = ParameterDirection.Input;
                    param.Value = iModuleId;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter(":CHILD_MDL_ID", OracleType.Number);
                    param.Direction = ParameterDirection.Input;
                    param.Value = iChildModuleId;
                    cmd.Parameters.Add(param);

                    string ChildModuleName = cmd.ExecuteOracleScalar().ToString();
                    cmd.Dispose();
                    con.Close();
                    oUserLoginDetail.ChildModuleName = ChildModuleName + " > ";
                }
            }
            if (LocalPath.Length > 0)
            {
                if (string.Equals(LocalPath, "~/Module/Admin/Welcome.aspx", StringComparison.OrdinalIgnoreCase))
                {
                    oUserLoginDetail.Navigationname = "Welcome";
                }
                else
                {
                    string strSQL = "";
                    strSQL = "select NAV_NAME from CM_NAV_MST where ltrim(rtrim(NAV_URL))=:NAV_URL and ltrim(rtrim(DEL_STATUS))='0'";

                    OracleCommand cmd = new OracleCommand(strSQL, con);

                    OracleParameter param = new OracleParameter(":NAV_URL", OracleType.VarChar, 200);
                    param.Direction = ParameterDirection.Input;
                    param.Value = LocalPath;
                    cmd.Parameters.Add(param);

                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0 && dt != null)
                    {
                        string Navigationname = dt.Rows[0]["NAV_NAME"].ToString();

                        //dtUserLogInDetail.Rows[0]["Navigationname"] = Navigationname;
                        oUserLoginDetail.Navigationname = Navigationname;

                        AjaxControlToolkit.CollapsiblePanelExtender oCollapse = (AjaxControlToolkit.CollapsiblePanelExtender)GetUserMenu1.FindControl("cpe");
                        oCollapse.Collapsed = true;
                    }
                    cmd.Dispose();
                    da.Dispose();
                }
            }

            con.Close();
            Session["LoginDetail"] = oUserLoginDetail;
        }
    }
    private void GetNavigationDetail()
    {
        try
        {
            if (Session["LoginDetail"] == null)
                Response.Redirect("~/GetUserAuthorisation.aspx");
            else
            {
                SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

                lblModule.Text = oUserLoginDetail.Modulename;
                lblChildModule.Text = oUserLoginDetail.ChildModuleName;
                lblNavigation.Text = oUserLoginDetail.Navigationname;
                tdChildMoudule.Visible = true;
                tdLabel.Visible = true;
                tdModule.Visible = true;
                tdNavigation.Visible = true;

                if (Session["Collapse"] != null)
                {
                    UserControl GetUserMenu1 = (UserControl)this.Master.FindControl("GetUserMenu1");
                    if (GetUserMenu1 != null)
                    {
                        AjaxControlToolkit.CollapsiblePanelExtender oCollapse = (AjaxControlToolkit.CollapsiblePanelExtender)GetUserMenu1.FindControl(Session["Collapse"].ToString());
                        oCollapse.Collapsed = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

   
}
