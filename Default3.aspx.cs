using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        lblName.Text = txtName.Text;
        lblName.ToolTip = "test by Shiv Kumar";
        if (uploadPan.HasFile)
        {
            uploadPan.PostedFile.SaveAs(Server.MapPath(@"~\CommonImages\VendorDetails_Images\" + uploadPan.FileName.Trim()));
           
        }
    }
}
