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
using System.IO;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.ReportSource;
using CrystalDecisions.CrystalReports.Engine;

public partial class Module_Inventory_Reports_NewPO : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet ds = GetData();
        GetReport(ds);
    }
    private void GetReport(DataSet ds)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"NewPO1.rpt"));
        rDoc.SetDataSource(ds);
        CrystalReportViewer1.ReportSource = rDoc;

    }
    
    private DataSet GetData()
    {
        try
        {
            int From = 0;
            int To = 0;
            double amount=0;
            string PO_TYPE = "";
            string TargetStringFilePath;
            TargetStringFilePath = @"d:\aaa.txt";
            
            
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
            DataSet ds = SaitexBL.Interface.Method.Material_Purchase_Order.GetDataForPO(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, From, To, PO_TYPE);
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("COMP_CODE", typeof(string));
            dt2.Columns.Add("COMP_NAME", typeof(String));
            dt2.Columns.Add("BRANCH_CODE", typeof(string));
            dt2.Columns.Add("BRANCH_NAME", typeof(string));
            dt2.Columns.Add("USERNAME", typeof(string));
            DataRow dr = dt2.NewRow();
            dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
            dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
            dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
            dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
            dr["USERNAME"] = oUserLoginDetail.Username;
            dt2.Rows.Add(dr);
            ds.Tables.Add(dt2);
            ds.Tables[0].TableName = "PO_Header";
            ds.Tables[1].TableName = "PO_Detail";
            ds.Tables[2].TableName = "Branch_Detail";
            ds.Tables[3].TableName = "ReportHeader";
            StreamWriter sw = File.CreateText(TargetStringFilePath);
            sw.WriteLine("\n");
            sw.WriteLine("\n");
            sw.WriteLine("\n");
            sw.WriteLine("\n");
            sw.WriteLine("Report Printed on : " + DateTime.Now.ToString());
            sw.WriteLine("\n");
            sw.WriteLine("=====================================================================================================\n");
            sw.WriteLine(String.Format("{0,-10} | {1,-50} | {2,-5} | {3,10} | {4,12} |", "Item Code", "Item Description", "UOM", "Order Qty.", "Basic_Rate"));
            sw.WriteLine("=====================================================================================================\n");
            foreach(DataRow drow in ds.Tables[1].Rows)
            {               
                String str = String.Format("{0,-10} | {1,-50} | {2,-5} | {3,10} | {4,12} |", drow["ITEM_CODE"].ToString(), drow["ITEM_DESC"].ToString(), drow["UOM"].ToString(), drow["ORD_QTY"].ToString(), drow["BASIC_RATE"].ToString());
                amount += Convert.ToDouble((drow["BASIC_RATE"]));
                sw.WriteLine(str);
            }
            sw.WriteLine("=====================================================================================================\n");
            sw.WriteLine("Total Amount :{0,-72},{1,12} ","",amount);
            sw.WriteLine("=====================================================================================================\n");
            sw.WriteLine("\f");
            sw.Close();
            return ds;
          
        }
              
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }
    }

    
}
