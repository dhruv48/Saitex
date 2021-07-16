using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Module_Fabric_FabricSaleWork_Reports_Fabric_Master_Report : System.Web.UI.Page
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
        myDataColumn.ColumnName = "DEPT_CODE";
        myDataTable.Columns.Add(myDataColumn);

        myDataColumn = new DataColumn();
        myDataColumn.DataType = Type.GetType("System.String");
        myDataColumn.ColumnName = "FABR_TYPE";
        myDataTable.Columns.Add(myDataColumn);

        myDataColumn = new DataColumn();
        myDataColumn.DataType = Type.GetType("System.String");
        myDataColumn.ColumnName = "BRANCH_CODE";
        myDataTable.Columns.Add(myDataColumn);

        DataRow row;

        row = myDataTable.NewRow();
        row["FABR_CODE"] = txtYarnCodeRpt.Text;
        row["DEPT_CODE"] = txtDeptCodeRpt.Text;
        row["FABR_TYPE"] = txtCatCodeRpt.Text;
        row["BRANCH_CODE"] = txtBranchCodeRpt.Text;
        myDataTable.Rows.Add(row);
        Session["fabricreportdt"] = myDataTable;

        string URL = "../Reports/FabricMasterReport.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);

    }
}
