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
using System.Drawing;
using System.Drawing.Printing;

public partial class Module_OrderDevelopment_Reports_SaleInvoicePlant : System.Web.UI.Page
{
    private Font verdana10Font;
    private StreamReader reader;

    protected void Page_Load(object sender, EventArgs e)
    {
        GetData();
    }

    private void GetData()
    {
        try
        {
            double amount = 0;
            double Finalamount = 0;
            string TargetStringFilePath;
            TargetStringFilePath = @"C:\aaa.txt";

            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            string From = string.Empty;
            string To = string.Empty;
            string InvoiceFrom = string.Empty;
            string InvoiceTo = string.Empty;
            string BUSINESS_TYPE = string.Empty;
            string PRODUCT_TYPE = string.Empty;
            string ORDER_CAT = string.Empty;
            string ORDER_TYPE = string.Empty;
            string ORDER_PREFIX = string.Empty;

            if (Request.QueryString["FromNo"] != null)
            {
                From = Request.QueryString["FromNo"].ToString().Trim();
            }
            if (Request.QueryString["ToNo"] != null)
            {
                To = Request.QueryString["ToNo"].ToString().Trim();
            }

            if (Request.QueryString["BUSINESS_TYPE"] != null && Request.QueryString["BUSINESS_TYPE"] != "")
            {
                BUSINESS_TYPE = Request.QueryString["BUSINESS_TYPE"].ToString();
            }

            if (Request.QueryString["PRODUCT_TYPE"] != null && Request.QueryString["PRODUCT_TYPE"] != "")
            {
                PRODUCT_TYPE = Request.QueryString["PRODUCT_TYPE"].ToString();
            }

            if (Request.QueryString["ORDER_CAT"] != null && Request.QueryString["ORDER_CAT"] != "")
            {
                ORDER_CAT = Request.QueryString["ORDER_CAT"].ToString();
            }

            if (Request.QueryString["ORDER_TYPE"] != null && Request.QueryString["ORDER_TYPE"] != "")
            {
                ORDER_TYPE = Request.QueryString["ORDER_TYPE"].ToString();
            }

            if (Request.QueryString["INVOICE_FROM"] != null && Request.QueryString["INVOICE_FROM"] != "")
            {
                InvoiceFrom = Request.QueryString["INVOICE_FROM"].ToString();
            }

            if (Request.QueryString["INVOICE_TO"] != null && Request.QueryString["INVOICE_TO"] != "")
            {
                InvoiceTo = Request.QueryString["INVOICE_TO"].ToString();
            }

            SaitexDM.Common.DataModel.OD_CAPTURE_MST oOD_CAPTURE_MST = new SaitexDM.Common.DataModel.OD_CAPTURE_MST();

            oOD_CAPTURE_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oOD_CAPTURE_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oOD_CAPTURE_MST.BUSINESS_TYPE = BUSINESS_TYPE;
            oOD_CAPTURE_MST.PRODUCT_TYPE = PRODUCT_TYPE;
            oOD_CAPTURE_MST.ORDER_CAT = ORDER_CAT;
            oOD_CAPTURE_MST.ORDER_TYPE = ORDER_TYPE;

            DataTable dt = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetDataForReport_SaleInvoice(oOD_CAPTURE_MST, From, To, InvoiceFrom, InvoiceTo, "");

            using (StreamWriter sw = File.CreateText(TargetStringFilePath))
            {
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();

                DateTime dtdt = System.DateTime.Now;

                sw.WriteLine(String.Format("\t\t\t\t\t\t\t\t\t\t\t{0,-10}\t{1,10}", dt.Rows[0]["INVOICE_NO"].ToString(), DateTime.Parse(dt.Rows[0]["INVOICE_DATE"].ToString()).ToShortDateString()));
                sw.WriteLine(String.Format("\t\t\t{0,-60}\t{1,-20}", dt.Rows[0]["PRTY_CODE"].ToString() + "  " + dt.Rows[0]["PRTY_NAME"].ToString(), dt.Rows[0]["PARTY_ECC_NO"].ToString()));
                sw.WriteLine(String.Format("\t{0,-60}\t{1,-20}", dt.Rows[0]["PRTY_ADD1"].ToString(), dt.Rows[0]["PARTY_CSTTIN_NO"].ToString()));
                sw.WriteLine(String.Format("\t{0,-60}\t{1,-20}", dt.Rows[0]["PRTY_ADD2"].ToString(), dt.Rows[0]["ORDER_NO"].ToString()));
                sw.WriteLine(String.Format("\t{0,-60}\t{1,-20}", dt.Rows[0]["PRTY_CITY"].ToString() + "  " + dt.Rows[0]["PRTY_STATE"].ToString(), dt.Rows[0]["LR_NUMB"].ToString() + "  " + dt.Rows[0]["LR_DATE"].ToString()));

                sw.WriteLine();
                sw.WriteLine(String.Format("\t\t\t{0,-60}\t{1,-20}", dt.Rows[0]["CONSIGNEE_CODE"].ToString() + "  " + dt.Rows[0]["CONSIGNEE_NAME"].ToString(), dt.Rows[0]["LORY_NUMB"].ToString()));
                sw.WriteLine(String.Format("\t{0,-60}\t{1,-20}", dt.Rows[0]["CONSIGNEE_ADD1"].ToString() + " " + dt.Rows[0]["CONSIGNEE_ADD1"].ToString(), ""));
                sw.WriteLine(String.Format("\t{0,-60}\t{1,-20}", dt.Rows[0]["CONSIGNEE_CITY"].ToString() + " " + dt.Rows[0]["CONSIGNEE_STATE"].ToString(), dt.Rows[0]["DESTINATION"].ToString()));

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();

                int iRowCount = 1;
                foreach (DataRow drow in dt.Rows)
                {
                    String str = String.Format("\t{0,-40}{1,-10}{2,-12}{3,10}{4,12}{5,12}{6,12}", drow["ARTICLE_CODE"].ToString() + ", " + drow["ARTICLE_DESC"].ToString(), ",", drow["LOT_NO"].ToString(), drow["NO_OF_UNIT"].ToString(), drow["WEIGHT_OF_UNIT"].ToString(), drow["RATE"].ToString(), drow["AMOUNT"].ToString());
                    amount += Convert.ToDouble((drow["AMOUNT"]));
                    sw.WriteLine(str);
                    iRowCount += 1;
                }
                int iLoop = 0;
                for (iLoop = iRowCount; iLoop < 35; iLoop++)
                {
                    sw.WriteLine();
                }
                sw.WriteLine();
                sw.WriteLine();

                sw.WriteLine(String.Format("\t\t\t\t{0,-40}\t\t\t\t{1,12}", dt.Rows[0]["INSU_PLICY_NO"].ToString(), amount.ToString()));
                sw.WriteLine(String.Format("\t\t\t\t{0,-40}\t\t\t\t{1,12}", dt.Rows[0]["LC_NO"].ToString() + "" + dt.Rows[0]["LC_DATE"].ToString(), dt.Rows[0]["EXCISE_TOTAL_AMOUNT"].ToString()));
                sw.WriteLine(String.Format("\t\t\t\t{0,-40}\t\t\t\t{1,12}", dt.Rows[0]["SALE_AGAINST"].ToString(), dt.Rows[0]["FREIGHT"].ToString()));
                sw.WriteLine(String.Format("\t\t\t\t{0,-40}\t\t\t\t{1,12}", dt.Rows[0]["EXCISE_NOTIFI_NO"].ToString() + "" + dt.Rows[0]["EXCISE_NOTIFI_DATE"].ToString(), dt.Rows[0]["INSURANCE_AMOUNT"].ToString()));
                sw.WriteLine(String.Format("\t\t\t\t\t\t\t\t\t\t\t\t\t{0,12}", dt.Rows[0]["SALE_TAX_AMOUNT"].ToString()));
                Finalamount = amount + double.Parse(dt.Rows[0]["EXCISE_TOTAL_AMOUNT"].ToString()) + double.Parse(dt.Rows[0]["FREIGHT"].ToString()) + double.Parse(dt.Rows[0]["INSURANCE_AMOUNT"].ToString()) + double.Parse(dt.Rows[0]["SALE_TAX_AMOUNT"].ToString());
                AmountToWords.RupeesToWord ortw = new AmountToWords.RupeesToWord();
                string rInw = ortw.changeNumericToWords(Finalamount);

                sw.WriteLine(String.Format("\t\t{0,-100}", rInw));
                sw.WriteLine(String.Format("\t\t\t\t\t\t\t\t\t\t\t\t{0,12}", Finalamount));
                sw.WriteLine(String.Format("\t\t{0,12}\t{1,12}\t{2,12}\t{3,12}", dt.Rows[0]["EXCISE_BASE_AMOUNT"].ToString(), dt.Rows[0]["EXCISE_CESS_AMOUNT_1"].ToString(), dt.Rows[0]["EXCISE_CESS_AMOUNT_2"].ToString(), dt.Rows[0]["EXCISE_TOTAL_AMOUNT"].ToString()));

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("\f");
                sw.Close();
            }

            // code to print the text file
            using (reader = new StreamReader(TargetStringFilePath))
            {
                verdana10Font = new Font("Verdana", 10);

                PrintDocument pd = new PrintDocument();

                pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);

                pd.Print();

                if (reader != null)
                    reader.Close();
            }

            /************* code to create print file on client system ************/

            System.IO.FileInfo _file = new System.IO.FileInfo(@"d:\aaa.txt");
            if (_file.Exists)
            {
                Response.Clear();
                Response.ContentType = "text/plain";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + _file.Name);
                Response.AddHeader("Content-Length", _file.Length.ToString());
                Response.WriteFile(_file.FullName);
                Response.End();
            }

            /************* code to create print file on client system ************/

            using (StreamWriter sw1 = File.CreateText(@"d:\aaa.txt"))
            {
                sw1.WriteLine();
                sw1.Close();
            }
        }

        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    void pd_PrintPage(object sender, PrintPageEventArgs ppeArgs)
    {
        //Get the Graphics object
        Graphics g = ppeArgs.Graphics;
        float linesPerPage = 0;
        float yPos = 0;
        int count = 0;
        //Read margins from PrintPageEventArgs
        float leftMargin = ppeArgs.MarginBounds.Left;
        float topMargin = ppeArgs.MarginBounds.Top;
        string line = null;
        //Calculate the lines per page on the basis of the height of the page and the height of the font
        linesPerPage = ppeArgs.MarginBounds.Height /
        verdana10Font.GetHeight(g);
        //Now read lines one by one, using StreamReader
        while (count < linesPerPage && ((line = reader.ReadLine()) != null))
        {
            //Calculate the starting position
            yPos = topMargin + (count *
            verdana10Font.GetHeight(g));
            //Draw text
            g.DrawString(line, verdana10Font, Brushes.Black,
            leftMargin, yPos, new StringFormat());
            //Move to next line
            count++;
        }
        //If PrintPageEventArgs has more pages to print
        if (line != null)
        {
            ppeArgs.HasMorePages = true;
        }
        else
        {
            ppeArgs.HasMorePages = false;
        }
    }
}
