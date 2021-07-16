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
using Common;
using errorLog;
public partial class Module_Yarn_SalesWork_Reports_YarnDetailreport : System.Web.UI.Page
{
    string BRANCH = string.Empty;
    string YARNCAT = string.Empty;
    string PARTY = string.Empty;
    string SHADCODE = string.Empty;
    string COMP_NAME1 = string.Empty;
    string BRANCH_NAME1 = string.Empty;
    string USER_NAME = string.Empty;
    string COMP_ADD = string.Empty;
    string YARNTYPE = string.Empty;
    string BRANCH_CODE = string.Empty;
    string YARN_CAT = string.Empty;
    string YARN_TYPE = string.Empty;
    string PRTY_CODE = string.Empty;
    string SHADE_CODE = string.Empty;
    string YARN_CODE =string.Empty ;
    string LOCATION = string.Empty;
    string STORE = string.Empty;
    string TRN = string.Empty;
    DateTime StDate;
    DateTime EnDate;
    int chksumry = 0;

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    ReportDocument rDoc = new ReportDocument();
    protected void Page_unLoad(object sender, EventArgs e)
    {
        rDoc.Close();
        rDoc.Dispose();

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (Request.QueryString["BRANCH_CODE"] != null)
            {
                BRANCH_CODE = Request.QueryString["BRANCH_CODE"];
            }
            else
            {
                BRANCH_CODE = string.Empty;
            }
            if (Request.QueryString["YARN_CAT"] != null)
            {
                YARN_CAT = Request.QueryString["YARN_CAT"];
            }
            else
            {
                YARN_CAT = string.Empty;
            }
            if (Request.QueryString["YARN_TYPE"] != null)
            {
                YARN_TYPE = Request.QueryString["YARN_TYPE"];
            }
            else
            {
                YARN_TYPE = string.Empty;
            }
            if (Request.QueryString["PRTY_CODE"] != null)
            {
                PRTY_CODE = Request.QueryString["PRTY_CODE"];
            }
            else
            {
                PRTY_CODE = string.Empty;
            }
            if (Request.QueryString["SHADE_CODE"] != null)
            {
                SHADE_CODE = Request.QueryString["SHADE_CODE"];
            }
            else
            {
                SHADE_CODE = string.Empty;
            }
            if (Request.QueryString["BRANCH"] != null)
            {
                 BRANCH = Request.QueryString["BRANCH"];
            }
            else
            {
               BRANCH = string.Empty;
            }
            if (Request.QueryString["YARNCAT"] != null)
            {
                 YARNCAT = Request.QueryString["YARNCAT"];
            }
            else
            {
                string YARNCAT = string.Empty;
            }
            if (Request.QueryString["LOCATION"] != null)
            {
                LOCATION = Request.QueryString["LOCATION"];
            }
            else
            {
                LOCATION = string.Empty;
            }
            if (Request.QueryString["STORE"] != null)
            {
                STORE = Request.QueryString["STORE"];
            }
            else
            {
                STORE = string.Empty;
            }
            if (Request.QueryString["YARNTYPE"] != null)
            {
                YARNTYPE = Request.QueryString["YARNTYPE"];
            }
            else
            {
                YARNTYPE = string.Empty;
            }
            if (Request.QueryString["PARTY"] != null)
            {
               PARTY = Request.QueryString["PARTY"];
            }
            else
            {
               PARTY = string.Empty;
            }

            if (Request.QueryString["SHADCODE"] != null)
            {
                 SHADCODE = Request.QueryString["SHADCODE"];
            }
            else
            {
               SHADCODE = string.Empty;
            }      
            if (Request.QueryString["chk"] != null && Request.QueryString["chk"] != "")
            {
                chksumry = int.Parse(Request.QueryString["chk"].ToString());
            }            
            if (Request.QueryString["YARN_CODE"] != null && Request.QueryString["YARN_CODE"] != "")
            {
                YARN_CODE = Request.QueryString["YARN_CODE"];
            }
            else
            {
               YARN_CODE = string .Empty ;
            }
             if (Request.QueryString["StDate"] != null)
             {
                 DateTime.TryParse(Request.QueryString["StDate"].ToString(),out StDate);
             }
             else
             {
                 StDate = DateTime.MinValue;
             }
             if (Request.QueryString["EnDate"] != null)
             {                 
                 DateTime.TryParse(Request.QueryString["EnDate"].ToString(), out EnDate);
             }
             if (Request.QueryString["TRN"] != null && Request.QueryString["TRN"] != "")
             {
                 TRN = Request.QueryString["TRN"];
             }
             else
             {
                 TRN = string.Empty;
             }
             DataTable dtrportdat = GetData(BRANCH_CODE, YARN_CAT, YARN_TYPE, PRTY_CODE, SHADE_CODE, YARN_CODE, StDate, EnDate,LOCATION,STORE,TRN );
             GetReport(dtrportdat);
             dtrportdat.Dispose();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Report.\r\nSee error log for detail."));
        }
    }
    private void GetReport(DataTable dt)
    {
        try
        {           
            if (chksumry == 0)
            {
                rDoc.Load(Server.MapPath(@"YarnDetailStock_LotWise.rpt"));
            }
            else if (chksumry == 1)
            {
                rDoc.Load(Server.MapPath(@"YarnDetailStock_CartonWise.rpt"));
            }
            else if (chksumry == 2)
            {
                rDoc.Load(Server.MapPath(@"YarnDetailStock_CartonDetails.rpt"));
            }
            else if (chksumry == 3)
            {
                rDoc.Load(Server.MapPath(@"YarnDetailStock_PartyWise.rpt"));
            }
            else if (chksumry == 4)
            {
                rDoc.Load(Server.MapPath(@"YarnDetailStock_ChallanWise.rpt"));
            }

                rDoc.SetDataSource(dt);

                rDoc.SetParameterValue("BRANCH", BRANCH);
                rDoc.SetParameterValue("YARNCAT", YARNCAT);
                if (PARTY != string.Empty)
                {
                    rDoc.SetParameterValue("PARTY", PARTY);
                }
                else
                {
                    rDoc.SetParameterValue("PARTY", "ALL TYPE");
                }
                if (SHADCODE != string.Empty)
                {
                    rDoc.SetParameterValue("SHADCODE", SHADCODE);
                }
                else
                {
                    rDoc.SetParameterValue("SHADCODE", "ALL CATEGORY");
                }
                rDoc.SetParameterValue("COMP_NAME1", oUserLoginDetail.VC_COMPANYNAME);
                rDoc.SetParameterValue("BRANCH_NAME1", oUserLoginDetail.VC_BRANCHNAME);
                rDoc.SetParameterValue("USER_NAME", oUserLoginDetail.Username);
                rDoc.SetParameterValue("COMP_ADD", oUserLoginDetail.COMP_ADD);
                if (YARNTYPE != string.Empty)
                {
                    rDoc.SetParameterValue("YARNTYPE", YARNTYPE);
                }
                else
                {
                    rDoc.SetParameterValue("YARNTYPE", "YARNTYPE");
                }
                if (LOCATION != string.Empty)
                {
                    rDoc.SetParameterValue("LOCATION", LOCATION);
                }
                else
                {
                    rDoc.SetParameterValue("LOCATION", "All");
                } 
                if (STORE != string.Empty)
                {
                    rDoc.SetParameterValue("STORE", STORE);
                }
                else
                {
                    rDoc.SetParameterValue("STORE", "All");
                }

                if (TRN.Equals("R"))
                {
                    rDoc.SetParameterValue("TRN_TYPE", "Receiving");                  
                }
                else if (TRN.Equals("P"))
                {
                    rDoc.SetParameterValue("TRN_TYPE", "Packing");                    
                }
                else if (TRN.Equals("O"))
                {
                    rDoc.SetParameterValue("TRN_TYPE", "Opening"); 
                }
                else
                {
                    rDoc.SetParameterValue("TRN_TYPE", "All");
                }  
                CrystalReportViewer1.ReportSource = rDoc;     
        }
        catch
        {
            throw;
        }
    }
    private DataTable GetData(string BRANCH_CODE, string YARN_CAT, string YARN_TYPE, string PRTY_CODE, string SHADE_CODE , string YARN_CODE ,DateTime StDate, DateTime EnDate,string LOCATION,string STORE,string TRN)
    {
        try
        {
            DataTable dt = null;
            string TRN_TYPE = string.Empty;
            if(TRN.Equals("R"))
            {
                TRN_TYPE = "'RYS21','RYS06','RYS04','RYS03','RYS02','RYS01','RYJ01'";
            }
            else if (TRN.Equals("P"))
            {
                TRN_TYPE = "'RYS31','RYS32','RYS33','RYS34','RYS35','RYS36'";
            }
            else if (TRN.Equals("O"))
            {
                TRN_TYPE = "'OPB01','OJB01'";
            }           


            if (chksumry == 0)
            {
                dt = SaitexBL.Interface.Method.YRN_IR_MST.GetLotWiseYarnDetail(BRANCH_CODE, YARN_CAT, YARN_TYPE, PRTY_CODE, SHADE_CODE, YARN_CODE, StDate, EnDate, LOCATION, STORE, TRN_TYPE);
           
            }
            else if (chksumry == 1 || chksumry == 2)
            {
                dt = SaitexBL.Interface.Method.YRN_IR_MST.GetCartonWiseYarnDetail(BRANCH_CODE, YARN_CAT, YARN_TYPE, PRTY_CODE, SHADE_CODE, YARN_CODE, StDate, EnDate, LOCATION, STORE, TRN_TYPE);
              
            }            
            else if (chksumry == 3)
            {
                dt = SaitexBL.Interface.Method.YRN_IR_MST.GetPartyWiseYarnDetail(BRANCH_CODE, YARN_CAT, YARN_TYPE, PRTY_CODE, SHADE_CODE, YARN_CODE, StDate, EnDate, LOCATION, STORE, TRN_TYPE);
           
            }
            else if (chksumry == 4)
            {
                dt = SaitexBL.Interface.Method.YRN_IR_MST.GetYarnDetail(BRANCH_CODE, YARN_CAT, YARN_TYPE, PRTY_CODE, SHADE_CODE, YARN_CODE, StDate, EnDate, LOCATION, STORE,TRN_TYPE);
           
            }
            
            if (dt.Rows.Count == 0)
            {
                CommonFuction.ShowMessage("Data Not Found .");
            }
           
            return dt;
        }
        catch
        {
            throw;
        }
    }
}
