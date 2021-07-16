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
using Common;
using errorLog;

public partial class Module_OrderDevelopment_CustomerRequest_Reports_SALE_MACHINE_PRINT : System.Web.UI.Page
{
    DataTable dt=null;
    DataSet ds = new DataSet();
    string BRANCH_CODE = string.Empty;
    string PRTY_CODE = string.Empty;
    string QUALITY = string.Empty;
    string BUSINESS = string.Empty;
    string SHADE_CODE = string.Empty;
    string DTCRFrom = string.Empty;
    string DTCRTo = string.Empty;
    string SHADE_GROUP = string.Empty;
    string ORDER_NO = string.Empty;
    string SHADE_NATURE = string.Empty;
    string MACHINE_NO = string.Empty;

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
             DataTable dt = GetData();
             ds.Tables.Add(dt);
             ds.Tables[0].TableName = "SALE_MACHINE_PRINT";
             GetReport(ds);
        }
        catch  {  }
        
    }



    private void GetReport(DataSet ds)
    {
        try
        {
            var rDoc = new ReportDocument();
            rDoc.Load(Server.MapPath(@"SALE_MACHINE_PRINT.rpt"));
            rDoc.SetDataSource(ds);
            CrystalReportViewer1.ReportSource = rDoc;           
        }
        catch
        {
            throw;
        }
    }
   private DataTable GetData()
    {
        try
        {

            if (Request.QueryString["PRTY_CODE"] != null && Request.QueryString["PRTY_CODE"] != "")
            {
                PRTY_CODE = Request.QueryString["PRTY_CODE"].ToString();

            }
            else { PRTY_CODE = string.Empty; }

            if (Request.QueryString["BRANCH"].ToString() != null && Request.QueryString["BRANCH"].ToString() != "")
            {
                BRANCH_CODE = Request.QueryString["BRANCH"].ToString();

            }
            else
            {

                BRANCH_CODE = string.Empty;

            }
                        
            if (Request.QueryString["QUALITY"].ToString() != null && Request.QueryString["QUALITY"].ToString() != "")
            {
                QUALITY = Request.QueryString["QUALITY"].ToString();
            }
            else
            {
                QUALITY = string.Empty;
            }

            if (Request.QueryString["BUSINESS"].ToString() != null && Request.QueryString["BUSINESS"].ToString() != "")
            {
                BUSINESS = Request.QueryString["BUSINESS"].ToString();
            }
            else
            {
                BUSINESS = string.Empty;
            }

            if (Request.QueryString["SHADE_CODE"].ToString() != null && Request.QueryString["SHADE_CODE"].ToString() != "")
            {
                SHADE_CODE = Request.QueryString["SHADE_CODE"].ToString();

            }
            else
            {
                SHADE_CODE = string.Empty;

            }
            
            if (Request.QueryString["DTCRFrom"].ToString() != null && Request.QueryString["DTCRFrom"].ToString() != "")
            {
                DTCRFrom = Request.QueryString["DTCRFrom"].ToString();

            }
            else
            {

                DTCRFrom = string.Empty;

            }
            
            if (Request.QueryString["DTCRTo"].ToString() != null && Request.QueryString["DTCRTo"].ToString() != "")
            {
                DTCRTo = Request.QueryString["DTCRTo"].ToString();

            }
            else
            {

                DTCRTo = string.Empty;

            }

            if (Request.QueryString["SHADE_GROUP"].ToString() != null && Request.QueryString["SHADE_GROUP"].ToString() != "")
            {
                SHADE_GROUP = Request.QueryString["SHADE_GROUP"].ToString();

            }
            else
            {

                SHADE_GROUP = string.Empty;

            }
            
            if (Request.QueryString["ORDER_NO"].ToString() != null && Request.QueryString["ORDER_NO"].ToString() != "")
            {
                ORDER_NO = Request.QueryString["ORDER_NO"].ToString();

            }
            else
            {

                ORDER_NO = string.Empty;

            }
            
            if (Request.QueryString["MACHINE_NO"].ToString() != null && Request.QueryString["MACHINE_NO"].ToString() != "")
            {
                MACHINE_NO = Request.QueryString["MACHINE_NO"].ToString();

            }
            else
            {

                MACHINE_NO = string.Empty;

            }
            
            if (Request.QueryString["SHADE_NATURE"].ToString() != null && Request.QueryString["SHADE_NATURE"].ToString() != "")
            {
                SHADE_NATURE = Request.QueryString["SHADE_NATURE"].ToString();

            }
            else
            {

                SHADE_NATURE = string.Empty;

            }
            
            DataTable dt = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.GetCRForSalesMAchine(oUserLoginDetail.COMP_CODE, BRANCH_CODE , oUserLoginDetail.DT_STARTDATE.Year, PRTY_CODE, QUALITY, SHADE_CODE, DTCRFrom, DTCRTo, "", ORDER_NO, BUSINESS, MACHINE_NO, SHADE_NATURE);
            
            dt.Columns.Add("SHADE_GROUP");
            dt.Columns.Add("SHADE_SEQ");
            for (int y = 0; y < dt.Rows.Count; y++)
            {
                DataRow dr = dt.Rows[y];
                if (dr["SHADE_FAMILY"].ToString() == "BEIGE" || dr["SHADE_FAMILY"].ToString() == "GOLD" || dr["SHADE_FAMILY"].ToString() == "DK CREAM" || dr["SHADE_FAMILY"].ToString() == "DK CHIKOO" || dr["SHADE_FAMILY"].ToString() == "DK GOLD" || dr["SHADE_FAMILY"].ToString() == "CREAM" || dr["SHADE_FAMILY"].ToString() == "YELLOW" || dr["SHADE_FAMILY"].ToString() == "LT BEIGE" || dr["SHADE_FAMILY"].ToString() == "LT YELLOW" || dr["SHADE_FAMILY"].ToString() == "IVORY" || dr["SHADE_FAMILY"].ToString() == "CHIKOO" || dr["SHADE_FAMILY"].ToString() == "LT GOLD" || dr["SHADE_FAMILY"].ToString() == "LT CHIKOO" || dr["SHADE_FAMILY"].ToString() == "NEON YELLOW" || dr["SHADE_FAMILY"].ToString() == "LEMMON" || dr["SHADE_FAMILY"].ToString() == "LT .YELLOW")
                {
                    dt.Rows[y]["SHADE_GROUP"] = "YELLOW";
                    dt.Rows[y]["SHADE_SEQ"] = "2";
                    dt.AcceptChanges();


                }


                else if (dr["SHADE_FAMILY"].ToString() == "ASH GREY" || dr["SHADE_FAMILY"].ToString() == "BLUE GREY" || dr["SHADE_FAMILY"].ToString() == "LT STEEL GREY" || dr["SHADE_FAMILY"].ToString() == "STEEL GREY" || dr["SHADE_FAMILY"].ToString() == "DK GREY" || dr["SHADE_FAMILY"].ToString() == "LT GREY" || dr["SHADE_FAMILY"].ToString() == "SILVER" || dr["SHADE_FAMILY"].ToString() == "GREY")
                {
                    dt.Rows[y]["SHADE_GROUP"] = "GREY";
                    dt.Rows[y]["SHADE_SEQ"] = "4";
                    dt.AcceptChanges();

                }



                else if (dr["SHADE_FAMILY"].ToString() == "WHITE" || dr["SHADE_FAMILY"].ToString() == "BLAECH WHITE" || dr["SHADE_FAMILY"].ToString() == "OFF WHITE" || dr["SHADE_FAMILY"].ToString() == "RAW WHITE" || dr["SHADE_FAMILY"].ToString() == "KORA")
                {
                    dt.Rows[y]["SHADE_GROUP"] = "WHITE";
                    dt.Rows[y]["SHADE_SEQ"] = "1";
                    dt.AcceptChanges();

                }
                else if (dr["SHADE_FAMILY"].ToString() == "BLACK")
                {
                    dt.Rows[y]["SHADE_GROUP"] = "BLACK";
                    dt.Rows[y]["SHADE_SEQ"] = "11";
                    dt.AcceptChanges();

                }

                else if (dr["SHADE_FAMILY"].ToString() == "COFEE" || dr["SHADE_FAMILY"].ToString() == "WINE" || dr["SHADE_FAMILY"].ToString() == "LT COFEE" || dr["SHADE_FAMILY"].ToString() == "LT RUST" || dr["SHADE_FAMILY"].ToString() == "BROWN" || dr["SHADE_FAMILY"].ToString() == "MUSTERD" || dr["SHADE_FAMILY"].ToString() == "RUST" || dr["SHADE_FAMILY"].ToString() == "CHOCO" || dr["SHADE_FAMILY"].ToString() == "LT BROWN" || dr["SHADE_FAMILY"].ToString() == "DK BROWN" || dr["SHADE_FAMILY"].ToString() == "SUNF" || dr["SHADE_FAMILY"].ToString() == "DK WINE" || dr["SHADE_FAMILY"].ToString() == "COPPER" || dr["SHADE_FAMILY"].ToString() == "DK RUST")
                {
                    dt.Rows[y]["SHADE_GROUP"] = "BROWN";
                    dt.Rows[y]["SHADE_SEQ"] = "6";
                    dt.AcceptChanges();

                }

                else if (dr["SHADE_FAMILY"].ToString() == "RAMA" || dr["SHADE_FAMILY"].ToString() == "Y GREEN" || dr["SHADE_FAMILY"].ToString() == "MEHENDI" || dr["SHADE_FAMILY"].ToString() == "GREEN" || dr["SHADE_FAMILY"].ToString() == "DK GREEN" || dr["SHADE_FAMILY"].ToString() == "LT GREEN" || dr["SHADE_FAMILY"].ToString() == "LT RAMA" || dr["SHADE_FAMILY"].ToString() == "KD MEHANDI" || dr["SHADE_FAMILY"].ToString() == "DK RAMA" || dr["SHADE_FAMILY"].ToString() == "P GREEN" || dr["SHADE_FAMILY"].ToString() == "PARROT GREEN" || dr["SHADE_FAMILY"].ToString() == "LT MEHANDI" || dr["SHADE_FAMILY"].ToString() == "BOTTLE GREEN" || dr["SHADE_FAMILY"].ToString() == "OLIVE" || dr["SHADE_FAMILY"].ToString() == "NEON GREEN" || dr["SHADE_FAMILY"].ToString() == "DK PISTA" || dr["SHADE_FAMILY"].ToString() == "PISTA" || dr["SHADE_FAMILY"].ToString() == "LEMON GREEN")
                {
                    dt.Rows[y]["SHADE_GROUP"] = "GREEN";
                    dt.Rows[y]["SHADE_SEQ"] = "5";
                    dt.AcceptChanges();

                }


                else if (dr["SHADE_FAMILY"].ToString() == "MAROON" || dr["SHADE_FAMILY"].ToString() == "MAGENTA" || dr["SHADE_FAMILY"].ToString() == "LT MAROON" || dr["SHADE_FAMILY"].ToString() == "DK MAROON" || dr["SHADE_FAMILY"].ToString() == "BLOOD RED" || dr["SHADE_FAMILY"].ToString() == "DK RED" || dr["SHADE_FAMILY"].ToString() == "LT RED" || dr["SHADE_FAMILY"].ToString() == "BURGENDY")
                {
                    dt.Rows[y]["SHADE_GROUP"] = "RED";
                    dt.Rows[y]["SHADE_SEQ"] = "4";
                    dt.AcceptChanges();

                }
                else if (dr["SHADE_FAMILY"].ToString() == "DUSTY BLUE" || dr["SHADE_FAMILY"].ToString() == "LT NAVY" || dr["SHADE_FAMILY"].ToString() == "LT BLUE" || dr["SHADE_FAMILY"].ToString() == "PURPLE" || dr["SHADE_FAMILY"].ToString() == "DK NAVY" || dr["SHADE_FAMILY"].ToString() == "VOILET" || dr["SHADE_FAMILY"].ToString() == "NAVY" || dr["SHADE_FAMILY"].ToString() == "DK BLUE" || dr["SHADE_FAMILY"].ToString() == "BLUE" || dr["SHADE_FAMILY"].ToString() == "LT PURPLE" || dr["SHADE_FAMILY"].ToString() == "ROAL BLUE" || dr["SHADE_FAMILY"].ToString() == "DK PURPLE" || dr["SHADE_FAMILY"].ToString() == "LT LAVENDAR" || dr["SHADE_FAMILY"].ToString() == "LAVENDAR" || dr["SHADE_FAMILY"].ToString() == "NEON BLUE")
                {
                    dt.Rows[y]["SHADE_GROUP"] = "BLUE";
                    dt.Rows[y]["SHADE_SEQ"] = "7";
                    dt.AcceptChanges();

                }



                else if (dr["SHADE_FAMILY"].ToString() == "DK ROSE" || dr["SHADE_FAMILY"].ToString() == "DK PINK" || dr["SHADE_FAMILY"].ToString() == "NEON PINK" || dr["SHADE_FAMILY"].ToString() == "NEON RANI" || dr["SHADE_FAMILY"].ToString() == "DK RANI" || dr["SHADE_FAMILY"].ToString() == "LT PINK" || dr["SHADE_FAMILY"].ToString() == "RANI" || dr["SHADE_FAMILY"].ToString() == "PINK" || dr["SHADE_FAMILY"].ToString() == "LT RANI")
                {
                    dt.Rows[y]["SHADE_GROUP"] = "RANI";
                    dt.Rows[y]["SHADE_SEQ"] = "8";
                    dt.AcceptChanges();

                }


                else if (dr["SHADE_FAMILY"].ToString() == "LT GAJRI" || dr["SHADE_FAMILY"].ToString() == "NEON ORANGE" || dr["SHADE_FAMILY"].ToString() == "DK ORANGE" || dr["SHADE_FAMILY"].ToString() == "GAJRI" || dr["SHADE_FAMILY"].ToString() == "ORANGE" || dr["SHADE_FAMILY"].ToString() == "DK GAJRI" || dr["SHADE_FAMILY"].ToString() == "LT ORANDE" || dr["SHADE_FAMILY"].ToString() == "PEACH" || dr["SHADE_FAMILY"].ToString() == "LT PEACH" || dr["SHADE_FAMILY"].ToString() == "DK PEACH" || dr["SHADE_FAMILY"].ToString() == "SINDURY")
                {
                    dt.Rows[y]["SHADE_GROUP"] = "ORANGE";
                    dt.Rows[y]["SHADE_SEQ"] = "3";
                    dt.AcceptChanges();

                }


                else if (dr["SHADE_FAMILY"].ToString() == "SEA GREEN" || dr["SHADE_FAMILY"].ToString() == "SKY BLUE" || dr["SHADE_FAMILY"].ToString() == "LT SEA GREEN" || dr["SHADE_FAMILY"].ToString() == "LT SILVER" || dr["SHADE_FAMILY"].ToString() == "TURQUOISE" || dr["SHADE_FAMILY"].ToString() == "FIROZI" || dr["SHADE_FAMILY"].ToString() == "LT FIOZI")
                {
                    dt.Rows[y]["SHADE_GROUP"] = "SKY BLUE";
                    dt.Rows[y]["SHADE_SEQ"] = "3";
                    dt.AcceptChanges();

                }



            }

            if (SHADE_GROUP != "" || SHADE_GROUP != null || SHADE_GROUP != string.Empty)
            {
                dt.DefaultView.Sort = "SHADE_SEQ asc";
            }
            else
            {


                DataView dv = new DataView(dt);
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    dv.RowFilter = "SHADE_GROUP='" + SHADE_GROUP + "'";

                }

                dt = dv.ToTable();


               
            }
            return dt;

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }

    }

}
