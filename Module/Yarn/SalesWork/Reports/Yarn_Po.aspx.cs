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
using CrystalDecisions.ReportSource;
using CrystalDecisions.CrystalReports.Engine;
using Common;
using errorLog;
public partial class Module_Yarn_SalesWork_Reports_Yarn_Po : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ViewState["dtRate1"] = null;
            ViewState["dtRate2"] = null;
            DataSet dt = GetData();
            GetReport(dt);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Page Loading.\r\nSee error log for detail."));

        }
    }

    private void GetReport(DataSet dt)
    {
        try
        {
            ReportDocument rDoc = new ReportDocument();
            rDoc.Load(Server.MapPath(@"Tx_Yarn_PU_MST.rpt"));
            rDoc.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rDoc;
        }
        catch
        {
            throw;

        }

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
            DataSet dt = SaitexBL.Interface.Method.YRN_PU_MST.GetDataForReport(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, From, To, PO_TYPE, oUserLoginDetail.DT_STARTDATE.Year);
             if (ViewState["dtRate2"] == null)
                CreateDataTablePriYarn();

            DataTable pri=(DataTable)ViewState["dtRate2"];

            foreach (DataRow dr in dt.Tables[0].Rows)
            {

                var tx = SaitexBL.Interface.Method.YRN_PU_MST.GetDatapri(dr["YARN_CODE"].ToString(), dr["PO_NUMB"].ToString(), dr["COMP_CODE"].ToString(), dr["BRANCH_CODE"].ToString(), DateTime.Parse(dr["PO_DATE"].ToString()));


               for (int i = 0; i < tx.Rows.Count; i++)
               {
                   DataRow dr2 = pri.NewRow();
                   dr2["PO_NUMB"] = dr["PO_NUMB"].ToString();
                   dr2["COMP_CODE"] = dr["COMP_CODE"].ToString();
                   dr2["BRANCH_CODE"] = dr["BRANCH_CODE"].ToString();
                   dr2["PRI_YEAR"] = tx.Rows[i]["PRI_YEAR"].ToString();
                   dr2["PRI_PO_NUMB"] = tx.Rows[i]["PRI_PO_NUMB"].ToString();
                   dr2["PRI_PO_DATE"] = tx.Rows[i]["PRI_PO_DATE"].ToString();
                   dr2["YARN_CODE"] = tx.Rows[i]["YARN_CODE"].ToString();
                   dr2["YARN_DESC"] = tx.Rows[i]["YARN_DESC"].ToString();
                   dr2["ASS_YARN_DESC"] = tx.Rows[i]["ASS_YARN_DESC"].ToString();
                   dr2["PRI_LOT_NO"] = tx.Rows[i]["PRI_LOT_NO"].ToString();
                   dr2["PRI_GRADE"] = tx.Rows[i]["PRI_GRADE"].ToString();
                   dr2["RATE_CGST"] = tx.Rows[i]["RATE_CGST"].ToString();
                   dr2["RATE_SGST"] = tx.Rows[i]["RATE_SGST"].ToString();
                   dr2["RATE_IGST"] = tx.Rows[i]["RATE_IGST"].ToString();
                   dr2["CGST"] = tx.Rows[i]["CGST"].ToString();
                   dr2["SGST"] = tx.Rows[i]["SGST"].ToString();
                   dr2["IGST"] = tx.Rows[i]["IGST"].ToString();
                   dr2["PRI_UOM"] = tx.Rows[i]["PRI_UOM"].ToString();
                   dr2["PRI_BASIC_RATE"] = tx.Rows[i]["PRI_BASIC_RATE"].ToString();
                   dr2["PRI_FINAL_RATE"] = tx.Rows[i]["PRI_FINAL_RATE"].ToString();
                   dr2["PRI_ORD_QTY"] = tx.Rows[i]["PRI_ORD_QTY"].ToString();
                   dr2["PRI_FINAL_AMT"] = tx.Rows[i]["PRI_FINAL_AMT"];
                   pri.Rows.Add(dr2);

               }

            }

            //******************************* ADDED BY NISHANT RAI FOR TRN TAX&DISCOUNT **********************************//
            if (ViewState["dtRate1"] == null)
                CreateDataTable();
            DataTable taxdistable = (DataTable)ViewState["dtRate1"];
            DataView recDataTable = dt.Tables[2].DefaultView;

            foreach (DataRow dr in dt.Tables[0].Rows)
            {
                recDataTable.RowFilter = "PO_NUMB='" + dr["PO_NUMB"].ToString() + "' AND YARN_CODE= '" + dr["YARN_CODE"].ToString() + "' AND SHADE_CODE= '" + dr["SHADE_CODE"].ToString() + "'  AND SHADE_FAMILY= '" + dr["SHADE_FAMILY"].ToString() + "'";
                var rtable = GETDATA(recDataTable.ToTable(), Convert.ToDouble(dr["IRATE"].ToString()));



                for (int i = 0; i < rtable.Rows.Count; i++)
                {
                    DataRow dr1 = taxdistable.NewRow();
                    dr1["UNIQUEID"] = taxdistable.Rows.Count + 1;
                    dr1["COMP_CODE"] = rtable.Rows[i]["COMP_CODE"].ToString();
                    dr1["BRANCH_CODE"] = rtable.Rows[i]["BRANCH_CODE"].ToString();
                    dr1["PO_TYPE"] = rtable.Rows[i]["PO_TYPE"].ToString();
                    dr1["PO_NUMB"] = rtable.Rows[i]["PO_NUMB"];
                    dr1["YARN_CODE"] = rtable.Rows[i]["YARN_CODE"].ToString();
                    dr1["SHADE_CODE"] = rtable.Rows[i]["SHADE_CODE"].ToString();
                    dr1["SHADE_FAMILY"] = rtable.Rows[i]["SHADE_FAMILY"].ToString();
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
            dt.Tables[3].TableName = "PO_DELV_SCH";
            dt.Tables[4].TableName = "PO_DIS_ADJ";
            dt.Tables.Add(pri);
            dt.Tables[5].TableName = "PRI_YARN_PURCAHSE";


            if (dt.Tables[0].Columns["WORKS_ADDRESS"] == null)
            {
                dt.Tables[0].Columns.Add("DEVELOPER_COMP", typeof(string));
                dt.Tables[0].Columns.Add("DEVELOPER_WEB", typeof(string));
                dt.Tables[0].Columns.Add("WORKS_ADDRESS", typeof(string));
            }
            foreach (DataRow dr in dt.Tables[0].Rows)
            {

                dr["DEVELOPER_COMP"] = oUserLoginDetail.DEVELOPER_COMP;
                dr["DEVELOPER_WEB"] = oUserLoginDetail.DEVELOPER_WEB;
                dr["WORKS_ADDRESS"] = oUserLoginDetail.WORKS_ADDRESS;
            }
            

            return dt;
        }
        catch
        {
            throw;
           
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
            dtRate1.Columns.Add("YARN_CODE", typeof(string));
            dtRate1.Columns.Add("SHADE_CODE", typeof(string));
            dtRate1.Columns.Add("SHADE_FAMILY", typeof(string));
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






    private void CreateDataTablePriYarn()
    {
        try
        {
            var dtRate2 = new DataTable();
            dtRate2.Columns.Add("PO_NUMB", typeof(int));
            dtRate2.Columns.Add("PRI_YEAR", typeof(string));
            dtRate2.Columns.Add("COMP_CODE", typeof(string));
            dtRate2.Columns.Add("BRANCH_CODE", typeof(string));
            dtRate2.Columns.Add("PRI_PO_NUMB", typeof(string));
            dtRate2.Columns.Add("PRI_PO_DATE", typeof(string));
            dtRate2.Columns.Add("YARN_CODE", typeof(string));
            dtRate2.Columns.Add("YARN_DESC", typeof(string));
            dtRate2.Columns.Add("PRI_UOM", typeof(string));
            dtRate2.Columns.Add("ASS_YARN_DESC", typeof(string));
            dtRate2.Columns.Add("PRI_LOT_NO", typeof(string));
            dtRate2.Columns.Add("PRI_GRADE", typeof(string));
            dtRate2.Columns.Add("RATE_CGST", typeof(double));
            dtRate2.Columns.Add("RATE_SGST", typeof(double));
            dtRate2.Columns.Add("RATE_IGST", typeof(double));
            dtRate2.Columns.Add("CGST", typeof(string));
            dtRate2.Columns.Add("SGST", typeof(string));
            dtRate2.Columns.Add("IGST", typeof(string));
            dtRate2.Columns.Add("PRI_BASIC_RATE", typeof(double));
            dtRate2.Columns.Add("PRI_FINAL_RATE", typeof(double));
            dtRate2.Columns.Add("PRI_ORD_QTY", typeof(double));
            dtRate2.Columns.Add("PRI_FINAL_AMT", typeof(double));
            ViewState["dtRate2"] = dtRate2;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


}
