using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Module_Inventory_Reports_Debit_Note_Parameter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["NOTE_TYPE"] != null && Request.QueryString["NOTE_TYPE"] != "")
        {
            POPermrpt1.POTYPE = Request.QueryString["NOTE_TYPE"].ToString();
        }
        else
        {
            POPermrpt1.POTYPE = "DEBIT NOTE";
        }

    }
}
