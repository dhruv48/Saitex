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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;

public partial class Module_Inventory_Reports_Materail_Receipt : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["dtRate1"] = null;
        ViewState["dtRateMST1"] = null;
        DataSet dt = GetData();
        GetReport(dt);
    }

    private void GetReport(DataSet dt)
    {
        ReportDocument rDoc = new ReportDocument();
        if (Request.QueryString["TRN_TYPE"].ToString() == "RMS01" || Request.QueryString["TRN_TYPE"].ToString() == "RMS02" || Request.QueryString["TRN_TYPE"].ToString() == "IMS01" || Request.QueryString["TRN_TYPE"].ToString() == "IMS02" || Request.QueryString["TRN_TYPE"].ToString() == "IMS05") 
        {
            rDoc.Load(Server.MapPath(@"Material_Receipt.rpt")); 
        }       
        else if (Request.QueryString["TRN_TYPE"].ToString() == "IMS06" && Request.QueryString["TRN_TYPE1"] != null)
        {
            rDoc.Load(Server.MapPath(@"Material_Receipt1.rpt"));
        }
        else if (Request.QueryString["TRN_TYPE"].ToString() == "IMS03" || Request.QueryString["TRN_TYPE"].ToString() == "RMS03" || Request.QueryString["TRN_TYPE"].ToString() == "RMS30")
        {
            rDoc.Load(Server.MapPath(@"Material_Receipt4.rpt"));
        }
        else if (Request.QueryString["TRN_TYPE"].ToString() == "GIM01" )
        {
            rDoc.Load(Server.MapPath(@"Material_Receipt2.rpt"));
        }
        else 
        {
            rDoc.Load(Server.MapPath(@"Material_Receipt3.rpt"));
        }       
        rDoc.SetDataSource(dt);        
        CrystalReportViewer1.ReportSource = rDoc;
    }

    private DataSet GetData()
    {

        try
        {
            int From = 0;
            int To = 0;
            string TRN_TYPE = "";
            string TRN_TYPE1 = "";
            string PRINT_INVOICE = string.Empty;
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataSet dt = null;
            if (Request.QueryString["FromNo"] != null)
            {
                From = int.Parse(Request.QueryString["FromNo"].ToString().Trim());
            }
            if (Request.QueryString["ToNo"] != null)
            {
                To = int.Parse(Request.QueryString["ToNo"].ToString().Trim());
            }

            if (Request.QueryString["TRN_TYPE"] != null)
            {
                TRN_TYPE = Request.QueryString["TRN_TYPE"].ToString().Trim();
                TRN_TYPE1 = Request.QueryString["TRN_TYPE1"].ToString().Trim();
            }

            if (Request.QueryString["PRINT_INVOICE"] != null && Request.QueryString["PRINT_INVOICE"] != "")
            {
                PRINT_INVOICE = Request.QueryString["PRINT_INVOICE"].ToString();
                PRINT_INVOICE = PRINT_INVOICE.Replace("$", "'");
            }


            if (Request.QueryString["TRN_TYPE"].ToString() == "RMS01" || Request.QueryString["TRN_TYPE"].ToString() == "RMS02" || Request.QueryString["TRN_TYPE"].ToString() == "IMS01" || Request.QueryString["TRN_TYPE"].ToString() == "IMS02" || Request.QueryString["TRN_TYPE"].ToString() == "IMS03" || Request.QueryString["TRN_TYPE"].ToString() == "IMS05")
            {
                dt = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetDataForReport(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, From, To, TRN_TYPE);
            }
            else if (Request.QueryString["TRN_TYPE"].ToString() == "IMS06" && Request.QueryString["TRN_TYPE1"] != null)
            {
                dt = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetDataForReportForGatePass(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, From, To, TRN_TYPE, PRINT_INVOICE);
            }
            else
            {
                dt = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetDataForReportForGatePass(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, From, To, TRN_TYPE, PRINT_INVOICE );
            }

            //******************************* ADDED BY NISHANT RAI FOR TRN TAX&DISCOUNT **********************************//

           if( ViewState["dtRate1"]==null)
               CreateDataTable();
            DataTable taxdistable=(DataTable)ViewState["dtRate1"];
            DataView recDataTable = dt.Tables[2].DefaultView;
           
            foreach (DataRow dr in dt.Tables[0].Rows)
            {
                recDataTable.RowFilter = "PO_NUMB='" + dr["PO_NUMB"].ToString() + "' AND ITEM_CODE= '" + dr["ITEM_CODE"].ToString() + "' ";
                var rtable = GETDATA(recDataTable.ToTable(), Convert.ToDouble(dr["BASIC_RATE"].ToString()));   
          
                for(int i=0;i<rtable.Rows.Count ;i++)
                {
                            DataRow dr1 = taxdistable.NewRow();
                            dr1["UNIQUEID"] = taxdistable.Rows.Count + 1;
                            dr1["PO_COMP_CODE"] = rtable.Rows[i]["PO_COMP_CODE"].ToString();
                            dr1["PO_BRANCH"] = rtable.Rows[i]["PO_BRANCH"].ToString();   
                            dr1["PO_TYPE"] =rtable.Rows[i]["PO_TYPE"].ToString();
                            dr1["PO_NUMB"] = rtable.Rows[i]["PO_NUMB"]; 
                            dr1["TRN_NUMB"] = rtable.Rows[i]["TRN_NUMB"];
                            dr1["TRN_TYPE"] = rtable.Rows[i]["TRN_TYPE"].ToString();
                            dr1["ITEM_CODE"] =rtable.Rows[i]["ITEM_CODE"].ToString();
                            dr1["COMPO_CODE"] = rtable.Rows[i]["COMPO_CODE"].ToString();
                            dr1["RATE"] = rtable.Rows[i]["RATE"].ToString();
                            dr1["COMPO_SL"] =rtable.Rows[i]["COMPO_SL"];
                            dr1["COMPO_TYPE"] =rtable.Rows[i]["COMPO_TYPE"].ToString();
                            dr1["AMOUNT"] = rtable.Rows[i]["Amount"];
                            dr1["IS_PO"] =null ;
                            dr1["BASE_COMPO_CODE"] = rtable.Rows[i]["BASE_COMPO_CODE"].ToString();
                            taxdistable.Rows.Add(dr1);
                
                }

            }


           

            //******************************* ADDED BY NISHANT RAI FOR TRN TAX&DISCOUNT **********************************//

            //******************************* ADDED BY NISHANT RAI FOR MASTER TAX&DISCOUNT  **********************************//
            if (ViewState["dtRateMST1"] == null)
                CreateDataTableMST();
            DataTable taxdistableMST = (DataTable)ViewState["dtRateMST1"];
            DataView recDataTableMST = dt.Tables[1].DefaultView;
            //foreach (DataRow dr in dt.Tables[1].Rows)            
            //{

                var rtable1 = GETDATA(recDataTableMST.ToTable(), Convert.ToDouble(dt.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString()));

                for (int i = 0; i < rtable1.Rows.Count; i++)
                {
                    DataRow dr1 = taxdistableMST.NewRow();
                    dr1["UNIQUEID"] = taxdistable.Rows.Count + 1;
                    dr1["TRN_NUMB"] = rtable1.Rows[i]["TRN_NUMB"];
                    dr1["TRN_TYPE"] = rtable1.Rows[i]["TRN_TYPE"].ToString();
                    dr1["COMPO_CODE"] = rtable1.Rows[i]["COMPO_CODE"].ToString();
                    dr1["RATE"] = rtable1.Rows[i]["RATE"].ToString();
                    dr1["COMPO_SL"] = rtable1.Rows[i]["COMPO_SL"];
                    dr1["COMPO_TYPE"] = rtable1.Rows[i]["COMPO_TYPE"].ToString();
                    dr1["AMOUNT"] = rtable1.Rows[i]["Amount"];
                    dr1["BASE_COMPO_CODE"] = rtable1.Rows[i]["BASE_COMPO_CODE"].ToString();
                    taxdistableMST.Rows.Add(dr1);
                    
                }
            //}


            dt.Tables.Add(taxdistable);
            dt.Tables.Add(taxdistableMST);
          
            //******************************* ADDED BY NISHANT RAI FOR MASTER TAX&DISCOUNT  **********************************//
                       
            dt.Tables[0].TableName = "Material_Receipt";
            dt.Tables[1].TableName = "Met_Rec_Tex_Dis_OLD";
            dt.Tables[2].TableName = "Met_Rec_Tex_Dis_TRN_OLD";
            dt.Tables[3].TableName = "Met_Rec_Tex_Dis_TRN_LATEST";
            dt.Tables[4].TableName = "Met_Rec_Tex_Dis";

            if (!dt.Tables[0].Columns.Contains("VALUE"))
            {
                dt.Tables[0].Columns.Add("VALUE", typeof(double));
                foreach (DataRow dr in dt.Tables[0].Rows)
                {
                    dr["VALUE"] = double.Parse(dr["TRN_QTY"].ToString()) * double.Parse(dr["FINAL_RATE"].ToString());
                }
            }
            if (!dt.Tables[0].Columns.Contains("TITLE"))
            {
                dt.Tables[0].Columns.Add("TITLE", typeof(string));
                foreach (DataRow dr in dt.Tables[0].Rows)
{
                    if (TRN_TYPE1 == "RCR")
                        dr["TITLE"] = "Material Receipt Credit Note- RCR";
                    else if (TRN_TYPE1 == "RCC")
                        dr["TITLE"] = "Material Receipt Cash Note- RCC";
                }
            }
            if (dt.Tables[0].Columns["COMP_NAME"] == null)
                dt.Tables[0].Columns.Add("COMP_NAME", typeof(string));

            if (dt.Tables[0].Columns["BRANCH_NAME"] == null)
                dt.Tables[0].Columns.Add("BRANCH_NAME", typeof(string));

            if (dt.Tables[0].Columns["USER_NAME"] == null)
                dt.Tables[0].Columns.Add("USER_NAME", typeof(string));

            foreach (DataRow dr in dt.Tables[0].Rows)
            {
                dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
                dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                dr["USER_NAME"] = oUserLoginDetail.Username;
                dt.AcceptChanges();
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
            var  dtRate1 = new DataTable();
            dtRate1.Columns.Add("UNIQUEID", typeof(int));
            dtRate1.Columns.Add("PO_COMP_CODE", typeof(string));
            dtRate1.Columns.Add("PO_BRANCH", typeof(string));
            dtRate1.Columns.Add("PO_TYPE", typeof(string));
            dtRate1.Columns.Add("TRN_NUMB", typeof(double));
            dtRate1.Columns.Add("TRN_TYPE", typeof(string));
            dtRate1.Columns.Add("PO_NUMB", typeof(int));
            dtRate1.Columns.Add("ITEM_CODE", typeof(string));
            dtRate1.Columns.Add("COMPO_CODE", typeof(string));
            dtRate1.Columns.Add("RATE", typeof(double));
            dtRate1.Columns.Add("COMPO_SL", typeof(int));
            dtRate1.Columns.Add("COMPO_TYPE", typeof(string));
            dtRate1.Columns.Add("AMOUNT", typeof(double));
            dtRate1.Columns.Add("BASE_COMPO_CODE", typeof(string));
            dtRate1.Columns.Add("IS_PO", typeof(string));
            ViewState["dtRate1"] = dtRate1;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    private void CreateDataTableMST()
    {
        try
        {
            var dtRateMST1 = new DataTable();
            dtRateMST1.Columns.Add("UNIQUEID", typeof(int));            
            dtRateMST1.Columns.Add("TRN_NUMB", typeof(double));
            dtRateMST1.Columns.Add("TRN_TYPE", typeof(string));          
            dtRateMST1.Columns.Add("COMPO_CODE", typeof(string));
            dtRateMST1.Columns.Add("RATE", typeof(double));
            dtRateMST1.Columns.Add("COMPO_SL", typeof(int));
            dtRateMST1.Columns.Add("COMPO_TYPE", typeof(string));
            dtRateMST1.Columns.Add("AMOUNT", typeof(double));
            dtRateMST1.Columns.Add("BASE_COMPO_CODE", typeof(string));           
            ViewState["dtRateMST1"] = dtRateMST1;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public DataTable GETDATA(DataTable dtRate1,double StartFinalAmount)
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
                        double rate = double.Parse(dr["Rate"].ToString());
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
                            cAmount = rate ;
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
