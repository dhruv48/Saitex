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
using errorLog;


public partial class Module_Admin_Pages_Leave_OD_Detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }
    }
    private void Load_Control()
    {
        try
        {

        }
        catch(Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString());
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
        }
    }
}
