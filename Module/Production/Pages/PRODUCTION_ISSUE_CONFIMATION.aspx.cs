using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;

public partial class Module_Production_Pages_PRODUCTION_ISSUE_CONFIMATION : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["TYPE"] != null)
        {
            ProductionIssueConfirmation.PRODUCT_TYPE = Request.QueryString["TYPE"];
        }
        

    }

   }
