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
using Common;
using errorLog;

public partial class Inventory_Trn_MSt_Opt : System.Web.UI.Page
{
    //OracleConnection con = null;
    //OracleCommand cmd = null;
    //OracleParameter param = null;
    //OracleDataReader dr = null; 
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    // private void bindMasterName()
    //{
    //    try
    //    {
    //        con = new OracleConnection();
    //        con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
    //        con.Open();

    //        string strSQL = "";
    //        strSQL = "select MST_NAME from TX_MASTER_MST  order by MST_NAME asc";
    //        cmd = new OracleCommand(strSQL, con);
    //        ddlMasterNameRpt.DataTextField = "MST_NAME";
    //        ddlMasterNameRpt.DataSource = cmd.ExecuteReader();
    //        ddlMasterNameRpt.DataSource = cmd.ExecuteReader();
    //        ddlMasterNameRpt.DataBind();
    //        ddlMasterNameRpt.Items.Insert(0, new ListItem("---------Select----------", ""));
    //    }

    //    catch (OracleException ex)
    //    {
            
            
    //        ErrHandler.WriteError(ex.Message);

    //    }

       

    //    finally
    //    {
    //        if (con != null)
    //        {
    //            con.Close();
    //            con.Dispose();
    //            con = null;
    //        }

    //        if (cmd != null)
    //        {
    //            cmd.Dispose();
    //            cmd = null;
    //        }
    //    }
    //}
    protected void btnGetReport_Click(object sender, EventArgs e)
    {
        
        string QueryString = "";
        bool flag = false;
        if (txtMstCodeRpt.Text != "")
        {
            if (flag)
                QueryString = QueryString + "";
            else
                QueryString = QueryString + "?";
            QueryString = QueryString + "MST_CODE=" + txtMstCodeRpt.Text;
            flag = true;
        }
        // if (ddlMasterNameRpt .SelectedValue .Trim ()!= "")
        //{
        //    if (flag)
        //        QueryString = QueryString + "";
        //    else
        //        QueryString = QueryString + "?";
        //    QueryString = QueryString + "MST_NAME=" + ddlMasterNameRpt .SelectedValue .Trim ();
        //    flag = true;
        //}


        string URL = "Trn_MST_RPT.aspx" + QueryString;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
    }
    
}
