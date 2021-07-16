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

public partial class Module_Inventory_Reports_Fabric_Mst_Opt : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail ;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        if (!IsPostBack)
        {
            Initialize();
        }
    }
    protected void Initialize()
    {
        txtItemCodeRpt.SelectedIndex = -1;
        txtCatCodeRpt.SelectedIndex = -1;
        txtBranchCode.SelectedIndex = -1;
        BindFabricCode();
        BindFabricType();
        BindBranch();

    }
    protected void BindFabricCode()
    {
        try
        {
            DataTable DT = SaitexBL.Interface.Method.TX_FABRIC_MST.GetFabric();
            txtItemCodeRpt.DataSource = DT;
            txtItemCodeRpt.DataTextField = "FABR_CODE";
            txtItemCodeRpt.DataValueField = "FABR_CODE";
            txtItemCodeRpt.DataBind();
            txtItemCodeRpt.Items.Insert(0, new ListItem("---------------All---------------", string.Empty));
            DT.Dispose();
            DT = null;
        }
        catch (Exception ere)
        {
            throw ere;
        }
    }
    protected void BindFabricType()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_FABRIC_MST.GetFabric();
            txtCatCodeRpt.DataSource = dt;
            txtCatCodeRpt.DataValueField = "FABR_TYPE";
            txtCatCodeRpt.DataTextField = "FABR_TYPE";
            txtCatCodeRpt.DataBind();
            txtCatCodeRpt.Items.Insert(0, new ListItem("---------------All---------------", string.Empty));
            dt.Dispose();
            dt = null;
        }
        catch(Exception exe)
        {
            throw exe;
        }
    }
    protected void BindBranch()
    {
        try
        {
            DataTable dt =  new DataTable();
            string strCompanyCode = oUserLoginDetail.COMP_CODE.Trim().ToString();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(strCompanyCode);
            DataView Dv = new DataView(dt);
            txtBranchCode.DataSource = Dv;
            txtBranchCode.DataValueField = "BRANCH_CODE";
            txtBranchCode.DataTextField = "BRANCH_NAME";
            txtBranchCode.DataBind();
            txtBranchCode.Items.Insert(0, new ListItem("---------------All---------------", string.Empty));
            dt.Dispose();
            dt = null;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnGetReport_Click(object sender, EventArgs e)
    {

        string BRANCH_CODE = "";
        string FABR_TYPE = "";
        string FABRIC_CODE = "";
        if (txtBranchCode.SelectedValue.ToString() != null && txtBranchCode.SelectedValue.ToString() != string.Empty)
        {
            BRANCH_CODE = txtBranchCode.SelectedValue.ToString();
        }
        else
        {
            BRANCH_CODE = string.Empty;
        }
        if (txtCatCodeRpt.SelectedValue.ToString() != null && txtCatCodeRpt.SelectedValue.ToString() != string.Empty)
        {
            FABR_TYPE = txtCatCodeRpt.SelectedValue.ToString();
        }
        else
        {
            FABR_TYPE = string.Empty;
        }
        if (txtItemCodeRpt.SelectedValue.ToString() != null && txtItemCodeRpt.SelectedValue.ToString() != string.Empty)
        {
            FABRIC_CODE = txtItemCodeRpt.SelectedValue.ToString();
        }
        else
        {
            FABRIC_CODE = string.Empty;
        }

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
        myDataColumn = new DataColumn();
        myDataColumn.DataType = Type.GetType("System.String");
        myDataColumn.ColumnName = "BRANCH_CODE";
        myDataTable.Columns.Add(myDataColumn);

        DataRow row;

        row = myDataTable.NewRow();
        row["FABR_CODE"] = FABRIC_CODE;
        row["FABR_TYPE"] = FABR_TYPE;
        row["BRANCH_CODE"] =BRANCH_CODE ;
        myDataTable.Rows.Add(row);
        Session["fabricreportdt"] = myDataTable;
        //inetpub\wwwroot\Saitex\Module\Fabric\FabricSaleWork\Reports\FabricMasterReport.aspx
        string URL = "../Reports/FabricMasterReport.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
 
    
    }
  
}

