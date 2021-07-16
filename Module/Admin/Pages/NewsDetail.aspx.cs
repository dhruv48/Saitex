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
using System.Xml.Linq;

public partial class Module_Admin_Pages_NewsDetail : System.Web.UI.Page
{
    private static int Newsid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["NewsId"] != null)
        {
            Newsid = int.Parse(Request.QueryString["NewsId"].ToString());
            bindNewsDetail(Newsid); 
        }
    }
    private void bindNewsDetail(int Newsid)
    {
        var dt = SaitexBL.Interface.Method.CM_NEWS_MST.GetActiveNews();
        if (dt != null && dt.Rows.Count > 0)
        {
            var dv = new DataView(dt);
            dv.RowFilter = "NEWS_ID=" + Newsid;
            if (dv != null && dv.Count > 0)
            {
                lblHeading.Text = dv[0]["NEWS_HEAD"].ToString();
                lblDescription.Text = dv[0]["NEWS_DESC"].ToString();
                lblPostedDAte.Text = dv[0]["TDATE"].ToString();
                
            
            }
        
        }
    
    }
}
