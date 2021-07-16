using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TestImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btn_Click(object sender, EventArgs e)
    {

    }
    protected void btnGetImage_Click(object sender, EventArgs e)
    {
        string URL = "CommonControls/ImageGenerator.aspx";
        URL = URL + "?ImageContrilId=" + btnSetImage.ClientID;

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);

    }
    protected void btnSetImage_Click(object sender, EventArgs e)
    {
        Image1.ImageUrl = @"~/CommonImages/ImageResizer/New.jpg";
    }
}
