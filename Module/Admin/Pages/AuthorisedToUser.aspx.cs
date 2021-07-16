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
using DBLibrary;
using Common;
using errorLog;


public partial class Admin_AuthorisedToUser : System.Web.UI.Page
{
    OracleConnection con = null;
    OracleCommand cmd = null;
    OracleParameter param = null;
    OracleDataReader dr = null;
     
    protected void Page_Load(object sender, EventArgs e)
    {
     
    }
    
}
