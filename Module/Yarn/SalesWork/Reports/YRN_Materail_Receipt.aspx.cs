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
public partial class Module_Yarn_SalesWork_Reports_YRN_Materail_Receipt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet dt = GetData();
        GetReport(dt);
    }
    private void GetReport(DataSet dt)
    {
        if (Request.QueryString["TRN_TYPE"] != null)
        {
            ReportDocument rDoc = new ReportDocument();
            string ReportPath = string.Empty;
            if (Request.QueryString["TRN_TYPE"].ToString().Substring(0, 1) == "R")
            {
                if (Request.QueryString["TRN_TYPE"].ToString() != "RYS12" && Request.QueryString["TRN_TYPE"].ToString() != "RYJ01")
                {
                    if (Request.QueryString["CORTON"].ToString() == "Corton")
                    {
                        ReportPath = Server.MapPath(@"YRN_Material_Receipt1.rpt");
                    }
                    else if (Request.QueryString["CORTON"].ToString() == "Main")
                    {
                        ReportPath = Server.MapPath(@"YRN_Material_Receipt.rpt");
                    }
               
                    else if (Request.QueryString["CORTON"].ToString() == "List")
                    {
                        if (Request.QueryString["TRN_TYPE"].ToString() == "RYS11")
                        {
                            ReportPath = Server.MapPath(@"YRN_Material_Receipt5.rpt");
                        }

                        else
                        {
                            ReportPath = Server.MapPath(@"YRN_Material_Receipt2.rpt");
                        }
                    }
                }
                else if (Request.QueryString["TRN_TYPE"].ToString() == "RYS12") 
                {
                    if (Request.QueryString["CORTON"].ToString() == "Main")
                    {
                        ReportPath = Server.MapPath(@"YRN_Material_Receipt3.rpt");
                    }
                    else if (Request.QueryString["CORTON"].ToString() == "List")
                    {

                        ReportPath = Server.MapPath(@"YRN_Material_Receipt6.rpt");

                    }
                
                }

                else if (Request.QueryString["TRN_TYPE"].ToString() == "RYJ01")
                {
                    if (Request.QueryString["CORTON"].ToString() == "Main")
                    {
                        ReportPath = Server.MapPath(@"YRN_Material_Receipt3.rpt");
                    }
                    else if (Request.QueryString["CORTON"].ToString() == "List")
                    {

                        ReportPath = Server.MapPath(@"YRN_Material_Receipt7.rpt");

                    }

                }
                else
                {
                    if (Request.QueryString["CORTON"].ToString() == "Main")
                    {
                        ReportPath = Server.MapPath(@"YRN_Material_Receipt3.rpt");
                    }
                    else if (Request.QueryString["CORTON"].ToString() == "List")
                    {

                        ReportPath = Server.MapPath(@"YRN_Material_Receipt4.rpt");

                    }

                }

            }
            else if (Request.QueryString["TRN_TYPE"].ToString().Substring(0, 1) == "I" && Request.QueryString["TRN_TYPE"].ToString() != "IYS01" && Request.QueryString["TRN_TYPE"].ToString() != "IYS11")
            {
                ReportPath = Server.MapPath(@"YRN_Material_Issue.rpt");
            }
            else if (Request.QueryString["TRN_TYPE"].ToString() == "IYS01")
            {
                ReportPath = Server.MapPath(@"YRN_Yarn_Issue_Against_Pa.rpt");
            }
            else if (Request.QueryString["TRN_TYPE"].ToString() == "IYS11")
            {
                ReportPath = Server.MapPath(@"YRN_Issue_Against_Work_Order.rpt");
            }

            rDoc.Load(ReportPath);
            rDoc.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rDoc;
        }
    }
    private DataSet GetData()
    {
        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            int From = 0;
            int To = 0;
            string TRN_TYPE = "";

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
            }
            DataSet dt = SaitexBL.Interface.Method.YRN_IR_MST.GetDataForReport(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, From, To, TRN_TYPE);
            dt.Tables[0].TableName = "Material_Receipt";
            dt.Tables[1].TableName = "Material_Receipt_Sub";
            dt.Tables[2].TableName = "Material_Receipt_Sub_Carton";
            if (!dt.Tables[0].Columns.Contains("VALUE"))
                dt.Tables[0].Columns.Add("VALUE", typeof(double));
            //if (!dt.Tables[0].Columns.Contains("TITLE"))
            //    dt.Tables[0].Columns.Add("TITLE", typeof(string));
            foreach (DataRow dr in dt.Tables[0].Rows)
            {
                dr["VALUE"] = double.Parse(dr["TRN_QTY"].ToString()) * double.Parse(dr["FINAL_RATE"].ToString());
                //    if (TRN_TYPE == "RCR")
                //        dr["TITLE"] = "Material Receipt Credit Note- RCR";
                //    else if (TRN_TYPE == "RCC")
                //        dr["TITLE"] = "Material Receipt Cash Note- RCC";
            }

            //if (dt.Tables[0].Columns["COMP_NAME"] == null)
            //    dt.Tables[0].Columns.Add("COMP_NAME", typeof(string));

            //if (dt.Tables[0].Columns["BRANCH_NAME"] == null)
            //    dt.Tables[0].Columns.Add("BRANCH_NAME", typeof(string));

            //if (dt.Tables[0].Columns["USER_NAME"] == null)
            //    dt.Tables[0].Columns.Add("USER_NAME", typeof(string));



            //foreach (DataRow dr in dt.Tables[0].Rows)
            //{
            //    dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
            //    dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
            //    dr["USER_NAME"] = oUserLoginDetail.Username;
            //    dt.AcceptChanges();
            //}

            return dt;

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }
    }
}
