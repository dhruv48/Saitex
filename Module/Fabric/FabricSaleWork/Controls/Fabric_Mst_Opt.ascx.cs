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

public partial class Module_Inventory_Controls_Fabric_Mst_Opt : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {


    }
    protected void btnGetReport_Click(object sender, EventArgs e)
    {
        DataTable myDataTable = new DataTable();
        DataColumn myDataColumn;

        myDataColumn = new DataColumn();
        myDataColumn.DataType = Type.GetType("System.String");
        myDataColumn.ColumnName = "FABR_CODE";
        myDataTable.Columns.Add(myDataColumn);

        myDataColumn = new DataColumn();
        myDataColumn.DataType = Type.GetType("System.String");
        myDataColumn.ColumnName = "FABR_TYPE";
        myDataTable.Columns.Add(myDataColumn);

        DataRow row;

        row = myDataTable.NewRow();
        row["FABR_CODE"] = txtItemCodeRpt.Text;
        row["FABR_TYPE"] = txtCatCodeRpt.Text;
       
        myDataTable.Rows.Add(row);
        Session["fabricreportdt"] = myDataTable;

        string URL = "../Reports/FabricMasterReprot.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
 
    
    }
}
