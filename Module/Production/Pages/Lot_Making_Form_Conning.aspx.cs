﻿using System;
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

public partial class Module_Production_Pages_Lot_Making_Form_Conning : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Lot_Making_Form1.PRODUCT_CATEGORY = "CONNING";
            
    }
}
