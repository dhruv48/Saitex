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
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;

public partial class Module_Inventory_Reports_Purchase_Register : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.HR_EMP_MST HR_EMP_MST = new SaitexDM.Common.DataModel.HR_EMP_MST();
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    string mth = string.Empty;
    string yr = string.Empty;
    string ttype = string.Empty;
    string trans_month = string.Empty;
    string report_period = string.Empty;
    string reg_type = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        yr = Request.QueryString["TRANS_YEAR"];
        ttype = Request.QueryString["TRANS_TYPE"];
        reg_type = Request.QueryString["PUR_REG_TYPE"];
        mth = Request.QueryString["TRANS_MONTH"];

        if (mth != null)
        {
            trans_month = " and to_number(to_char(m.trn_date,'MM'))=" + mth;
            report_period = "Month :" + mth + " Year : " + yr;
        }
        else
        {
            trans_month = "";
            report_period = "Year : " + yr;
        }
        DataSet ds = GetPurchaseReportData(ttype, yr, trans_month);
        GetReport(ds);
    }

    private DataSet GetPurchaseReportData(String trans_type, String trans_year, String trans_month)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetPurchaseReportData(ttype, yr, trans_month);
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("COMP_CODE", typeof(string));
            dt1.Columns.Add("COMP_NAME", typeof(String));
            dt1.Columns.Add("BRANCH_CODE", typeof(string));
            dt1.Columns.Add("BRANCH_NAME", typeof(string));
            dt1.Columns.Add("USERNAME", typeof(string));
            DataRow dr = dt1.NewRow();
            dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
            dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
            dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
            dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
            dr["USERNAME"] = oUserLoginDetail.Username;
            dt1.Rows.Add(dr);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables.Add(dt1);
            ds.Tables[0].TableName = "PurchaseRegister";
            ds.Tables[1].TableName = "ReportHeader";
            return ds;

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }
    }
    private void GetReport(DataSet ds)
    {
        ReportDocument rDoc = new ReportDocument();
        if (reg_type == "1")
        {
            rDoc.Load(Server.MapPath(@"Purchase_Register.rpt"));
        }
        if (reg_type == "2")
        {
            rDoc.Load(Server.MapPath(@"Purchase_Register_CC.rpt"));
        }

        rDoc.SetDataSource(ds);
        ParameterFields myParams = new ParameterFields();
        ParameterField myParam = new ParameterField();
        ParameterDiscreteValue myDiscreteValue = new ParameterDiscreteValue();

        myParam.ParameterFieldName = "p_ReportPeriod";
        myDiscreteValue.Value = report_period;
        myParam.CurrentValues.Add(myDiscreteValue);

        myParams.Add(myParam);
        CrystalReportViewer1.ParameterFieldInfo = myParams;

        CrystalReportViewer1.ReportSource = rDoc;
    }

}
