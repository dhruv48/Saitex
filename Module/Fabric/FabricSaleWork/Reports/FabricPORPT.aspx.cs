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
using System.Data.OracleClient;
public partial class Module_Inventory_Reports_FabricPORPT : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["dtRate1"] = null;
        DataSet dt = GetData();
        GetReport(dt);
    }
    private void GetReport(DataSet dt)
    {  
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"FabricPOCreditReport.rpt"));
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;

    }
    private DataSet GetData()
    {
        try
        {
            int From = 0;
            int To = 0;
            string PO_TYPE = "";
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (Request.QueryString["FromNo"] != null)
            {
                From = int.Parse(Request.QueryString["FromNo"].ToString().Trim());
            }
            if (Request.QueryString["ToNo"] != null)
            {
                To = int.Parse(Request.QueryString["ToNo"].ToString().Trim());
            }
            if (Request.QueryString["PO_TYPE"] != null)
            {
                PO_TYPE = Request.QueryString["PO_TYPE"].ToString().Trim();
            }
            
            //DataSet dt = SaitexBL.Interface.Method.Material_Purchase_Order.GetDataForReport(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, From, To, PO_TYPE);
            DataSet dt = SaitexBL.Interface.Method.TX_FABRIC_PU_MST.GetDataForReport(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, From, To, PO_TYPE);


            //******************************* ADDED BY NISHANT RAI FOR TRN TAX&DISCOUNT **********************************//
            if (ViewState["dtRate1"] == null)
                CreateDataTable();
            DataTable taxdistable = (DataTable)ViewState["dtRate1"];
            DataView recDataTable = dt.Tables[2].DefaultView;

            foreach (DataRow dr in dt.Tables[0].Rows)
            {
                recDataTable.RowFilter = "PO_NUMB='" + dr["PO_NUMB"].ToString() + "' AND FABR_CODE= '" + dr["FABR_CODE"].ToString() + "' ";
                var rtable = GETDATA(recDataTable.ToTable(), Convert.ToDouble(dr["IRATE"].ToString()));

                for (int i = 0; i < rtable.Rows.Count; i++)
                {
                    DataRow dr1 = taxdistable.NewRow();
                    dr1["UNIQUEID"] = taxdistable.Rows.Count + 1;
                    dr1["COMP_CODE"] = rtable.Rows[i]["COMP_CODE"].ToString();
                    dr1["BRANCH_CODE"] = rtable.Rows[i]["BRANCH_CODE"].ToString();
                    dr1["PO_TYPE"] = rtable.Rows[i]["PO_TYPE"].ToString();
                    dr1["PO_NUMB"] = rtable.Rows[i]["PO_NUMB"];
                    dr1["FABR_CODE"] = rtable.Rows[i]["FABR_CODE"].ToString();
                    dr1["COMPO_CODE"] = rtable.Rows[i]["COMPO_CODE"].ToString();
                    dr1["COMPO_VALUE"] = rtable.Rows[i]["COMPO_VALUE"].ToString();
                    dr1["COMPO_SL"] = rtable.Rows[i]["COMPO_SL"];
                    dr1["COMPO_TYPE"] = rtable.Rows[i]["COMPO_TYPE"].ToString();
                    dr1["AMOUNT"] = rtable.Rows[i]["Amount"];
                    dr1["BASE_COMPO_CODE"] = rtable.Rows[i]["BASE_COMPO_CODE"].ToString();
                    taxdistable.Rows.Add(dr1);
                }

            }

            dt.Tables.Add(taxdistable);

            //******************************* ADDED BY NISHANT RAI FOR TRN TAX&DISCOUNT **********************************//

            dt.Tables[0].TableName = "PO_REPORT";
            dt.Tables[1].TableName = "PO_IND_ADJUST";
            dt.Tables[2].TableName = "PO_DIS_ADJ_OLD";
            dt.Tables[3].TableName = "PO_DIS_ADJ";
            return dt;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }
    }

    private void CreateDataTable()
    {
        try
        {
            var dtRate1 = new DataTable();
            dtRate1.Columns.Add("UNIQUEID", typeof(int));
            dtRate1.Columns.Add("COMP_CODE", typeof(string));
            dtRate1.Columns.Add("BRANCH_CODE", typeof(string));
            dtRate1.Columns.Add("PO_TYPE", typeof(string));
            dtRate1.Columns.Add("PO_NUMB", typeof(int));
            dtRate1.Columns.Add("FABR_CODE", typeof(string));
            dtRate1.Columns.Add("COMPO_CODE", typeof(string));
            dtRate1.Columns.Add("COMPO_VALUE", typeof(double));
            dtRate1.Columns.Add("COMPO_SL", typeof(int));
            dtRate1.Columns.Add("COMPO_TYPE", typeof(string));
            dtRate1.Columns.Add("AMOUNT", typeof(double));
            dtRate1.Columns.Add("BASE_COMPO_CODE", typeof(string));
            ViewState["dtRate1"] = dtRate1;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GETDATA(DataTable dtRate1, double StartFinalAmount)
    {
        // double FinalAmount = 0;                  

        // if (dtRate1 != null)                      
        //dtRate1 = (DataTable)Session["dtDicRate"];

        if (!dtRate1.Columns.Contains("Amount"))
        {
            dtRate1.Columns.Add("Amount", typeof(double));
        }

        if (ViewState["StartFinalAmount"] != null)
        {
            StartFinalAmount = (Double)ViewState["StartFinalAmount"];
        }

        double dFinalRate = StartFinalAmount;
        foreach (DataRow dr in dtRate1.Rows)
        {
            double dAmount = 0;

            double cAmount = 0;
            double rate = double.Parse(dr["COMPO_VALUE"].ToString());
            if (dr["BASE_COMPO_CODE"].ToString().Equals("Basic Rate"))
            {
                cAmount = (StartFinalAmount * rate) / 100;
            }
            else if (dr["BASE_COMPO_CODE"].ToString().Equals("Final Rate"))
            {
                cAmount = (dFinalRate * rate) / 100;
            }
            else if (dr["BASE_COMPO_CODE"].ToString().Equals("Flat Amount"))
            {
                cAmount = rate;
            }
            else
            {
                DataView dvv = new DataView(dtRate1);
                dvv.RowFilter = "COMPO_CODE='" + dr["BASE_COMPO_CODE"].ToString() + "'";

                if (dvv.Count > 0)
                {
                    double.TryParse(dvv[0]["Amount"].ToString(), out dAmount);
                }
                cAmount = (dAmount * rate) / 100;
            }

            if (dr["COMPO_TYPE"].ToString().Equals("D"))
            {
                dFinalRate = dFinalRate - cAmount;
            }
            else
            {
                dFinalRate = dFinalRate + cAmount;
            }
            dr["Amount"] = cAmount;

        }
        ViewState["dtRate1"] = dtRate1;
        return dtRate1;

    }
}
