using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.ReportSource;
using CrystalDecisions.CrystalReports.Engine;

public partial class Module_Inventory_Reports_PO : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = new SaitexDM.Common.DataModel.UserLoginDetail();
    protected void Page_Load(object sender, EventArgs e)
    {
         oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
         if (Session["urLoginId"] != null)
         {
             ViewState["dtRate1"] = null;
             DataSet dt = GetData();
             GetReport(dt);
         }
    }

    private void GetReport(DataSet dt)
    {
        string BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE.Trim().ToString();
        ReportDocument rDoc = new ReportDocument();
        //if (BRANCH_CODE == "SIT0001")
        //{
        //    rDoc.Load(Server.MapPath(@"TX_ITEM_PU_MST1.rpt"));
        //}
        //else if (BRANCH_CODE == "GRG001")
        //{
        //    rDoc.Load(Server.MapPath(@"TX_ITEM_PU_MST2.rpt"));
        //}
        rDoc.Load(Server.MapPath(@"TX_ITEM_PU_MST1.rpt"));      
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
            DataSet dt = SaitexBL.Interface.Method.Material_Purchase_Order.GetDataForReport(oUserLoginDetail.DT_STARTDATE.Year,oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, From, To, PO_TYPE);



            //******************************* ADDED BY NISHANT RAI FOR TRN TAX&DISCOUNT **********************************//
            if (ViewState["dtRate1"] == null)
                CreateDataTable();
            DataTable taxdistable = (DataTable)ViewState["dtRate1"];
            DataView recDataTable = dt.Tables[2].DefaultView;

            foreach (DataRow dr in dt.Tables[0].Rows)
            {
                recDataTable.RowFilter = "PO_NUMB='" + dr["PO_NUMB"].ToString() + "' AND ITEM_CODE= '" + dr["ITEM_CODE"].ToString() + "' ";
                var rtable = GETDATA(recDataTable.ToTable(), Convert.ToDouble(dr["IRATE"].ToString()));

                for (int i = 0; i < rtable.Rows.Count; i++)
                {
                    DataRow dr1 = taxdistable.NewRow();
                    dr1["UNIQUEID"] = taxdistable.Rows.Count + 1;
                    dr1["COMP_CODE"] = rtable.Rows[i]["COMP_CODE"].ToString();
                    dr1["BRANCH_CODE"] = rtable.Rows[i]["BRANCH_CODE"].ToString();
                    dr1["PO_TYPE"] = rtable.Rows[i]["PO_TYPE"].ToString();
                    dr1["PO_NUMB"] = rtable.Rows[i]["PO_NUMB"];                   
                    dr1["ITEM_CODE"] = rtable.Rows[i]["ITEM_CODE"].ToString();
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
            dt.Tables[4].TableName = "PO_DIS_ADJ";
            dt.Tables[3].TableName = "PO_DIS_DEL_DATE";

            
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
            dtRate1.Columns.Add("ITEM_CODE", typeof(string));
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
                string AMOUNT=string.Empty;
                for(int i=0;i< dtRate1.Rows.Count;i++)
                {
                  if(i==(dtRate1.Rows.Count)-2)
                  {
                      AMOUNT = dtRate1.Rows[i]["Amount"].ToString();
                  
                  }
                
                }
                dvv.RowFilter = "COMPO_CODE='" + dr["COMPO_CODE"].ToString() + "'";

                if (dvv.Count > 0)
                {
                    if (dvv[0]["BASE_COMPO_CODE"].ToString() == "CGST-A" || dvv[0]["COMPO_CODE"].ToString() == "SGST-A")
                    {
                        //dFinalRate = (dFinalRate + double.Parse(AMOUNT));
                        cAmount = double.Parse(AMOUNT);
                    }

                    else
                    {
                        double.TryParse(dvv[0]["Amount"].ToString(), out dAmount);
                        cAmount = (dAmount * rate) / 100;
                    }


                   // double.TryParse(dvv[0]["Amount"].ToString(), out dAmount);
                }
                //cAmount = (dAmount * rate) / 100;
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
