using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data; 
public partial class Module_OrderDevelopment_Pages_ddt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("ORDER_TYPE", "C00001");
        DropDownList1.Items.Clear();
        DropDownList1.DataSource = dt;
        DropDownList1.DataValueField = "MST_DESC";
        DropDownList1.DataTextField = "MST_CODE";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, new ListItem("----Select---", ""));
        dt.Dispose();
        dt = null;



     // int a;
       // Response.Write(a);  
    }
    private void fnMultiply(string x, string y)
    {
        //return x + y;
    }
    public int fnMultiply(int x, int y, int z)
    {
        return x * y * z;
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
