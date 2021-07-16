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
using errorLog;
using Common;
using Obout.ComboBox;

public partial class Module_Yarn_SalesWork_Controls_YarnReturn_Against_PO : System.Web.UI.UserControl
{
    private static DateTime Sdate;
    private static DateTime Edate;
    private string TRN_TYPE = "IYS03";
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        if (!IsPostBack)
        {
            Initializing();
        }
    }
    private void Initializing()
    {
        ViewState["GridRecord"] = null;
        BindPodata();
        Sdate = oUserLoginDetail.DT_STARTDATE;
        Edate = Common.CommonFuction.GetYearEndDate(Sdate);
        TXTDATEFROM.Text = (Sdate.ToShortDateString()).ToString();
        TXTDATETO.Text = (Edate.ToShortDateString()).ToString();
        BindGrid();

    }
   private void BindPodata()
    {
        try
        {
            string qry = "SELECT   DISTINCT YT.PO_NUMB,                  M.YARN_DESC,                  (M.YARN_CODE || '@' || M.YARN_DESC) YRN_DESC,                  YT.YARN_CODE,                  YT.TRN_NUMB,                  YM.PRTY_NAME,                  YM.PRTY_CODE,                  (YM.PRTY_CODE || '@' || YM.PRTY_NAME) PARTY_NAME  FROM   YRN_IR_ISS_ADJ YA,         YRN_IR_TRN YT,         YRN_MST M,         YRN_IR_MST YM WHERE       YT.TRN_TYPE = YA.ISS_TRN_TYPE         AND YT.TRN_NUMB = YA.ISS_TRN_NUMB         AND YA.BRANCH_CODE = YT.BRANCH_CODE         AND YT.COMP_CODE = YA.COMP_CODE         AND M.YARN_CODE = YT.YARN_CODE         AND YT.YEAR = YA.YEAR         AND YT.TRN_TYPE = YM.TRN_TYPE         AND YT.TRN_NUMB = YM.TRN_NUMB         AND YA.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "'         AND YA.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "'         AND YA.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "'         AND YT.TRN_TYPE = 'IYS03'";
            //string STR = "SELECT DISTINCT   YT.PO_NUMB  FROM   YRN_IR_ISS_ADJ YA, YRN_IR_TRN YT WHERE       YT.TRN_TYPE = YA.ISS_TRN_TYPE         AND YT.TRN_NUMB = YA.ISS_TRN_NUMB         AND YA.BRANCH_CODE = YT.BRANCH_CODE         AND YT.COMP_CODE = YA.COMP_CODE         AND YT.YEAR = YA.YEAR         AND YA.BRANCH_CODE = '"+oUserLoginDetail.CH_BRANCHCODE+"'         AND YA.COMP_CODE = '"+oUserLoginDetail.COMP_CODE+"'         AND YA.YEAR = '"+oUserLoginDetail.DT_STARTDATE.Year+"'         AND YT.TRN_TYPE = 'IYS03'";
            DataTable dta = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(qry, "", "", "", "", "");
            // po number
            ddlPONumber.DataSource = dta;
            ddlPONumber.DataTextField = "PO_NUMB";
            ddlPONumber.DataValueField = "PO_NUMB";
            ddlPONumber.DataBind();
            ddlPONumber.Items.Insert(0,"------SELECT-------");
            // trn no
            ddltrnno.DataSource = dta;
            ddltrnno.DataTextField = "TRN_NUMB";
            ddltrnno.DataValueField = "TRN_NUMB";
            ddltrnno.DataBind();
            ddltrnno.Items.Insert(0, "------SELECT-------");

            //ddlparty

            ddlparty.DataSource = dta;
            ddlparty.DataTextField = "PARTY_NAME";
            ddlparty.DataValueField = "PRTY_CODE";
            ddlparty.DataBind();
            ddlparty.Items.Insert(0, "------SELECT-------");
            //ddlYarn
            ddlyarn.DataSource = dta;
            ddlyarn.DataTextField = "YRN_DESC";
            ddlyarn.DataValueField = "YARN_CODE";
            ddlyarn.DataBind();
            ddlyarn.Items.Insert(0, "------SELECT-------");
           
        }
        catch (Exception xx)
        { };
    }
   protected void ImagePrint_Click(object sender, ImageClickEventArgs e)
    {
        string BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
        string COMP_CODE = oUserLoginDetail.COMP_CODE;
        int PO_NUM = 0;
        int.TryParse(ddlPONumber.SelectedItem.Value.Trim(), out PO_NUM);
        int MRN_NUM = 0;
        int.TryParse(ddltrnno.SelectedItem.Value.Trim(), out MRN_NUM);
        string PRTY_CODE = string.Empty;
        if (ddlparty.SelectedIndex != 0)
        {
            PRTY_CODE = ddlparty.SelectedItem.Value.Trim();
        }
        string FROM_DATE = TXTDATEFROM.Text;
        string TO_DATE = TXTDATETO.Text;
        string YARN_CODE = string.Empty;
        if (ddlyarn.SelectedIndex != 0)
        {
            YARN_CODE = ddlyarn.SelectedItem.Value.Trim();
        }
        int YEAR = oUserLoginDetail.DT_STARTDATE.Year;
        string URL = "../Reports/Yarn_ReturnAgainst_PO.aspx?BRANCH_CODE=" + BRANCH_CODE;
        URL += "&COMP_CODE=" + COMP_CODE;
        URL += "&PO_NUM=" + PO_NUM;
        URL += "&MRN_NUM=" + MRN_NUM;
        URL += "&PRTY_CODE=" + PRTY_CODE;
        URL += "&FROM_DATE=" + FROM_DATE;
        URL += "&TO_DATE=" + TO_DATE;
        URL += "&YARN_CODE=" + YARN_CODE;
        URL += "&YEAR=" + YEAR;
        Response.Redirect(URL);
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {

    }
   


    private void ExporttoExcel(DataTable table, string name, string title)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + name);

        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
          "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
          "style='font-size:10.0pt; font-family:Calibri; background:white;'>");
        //am getting my grid's column headers
        int columnscount = table.Columns.Count;
        HttpContext.Current.Response.Write("<TR>");
        HttpContext.Current.Response.Write("<TD style='font-size:14.0pt;' align='center' colspan=" + columnscount + ">");
        HttpContext.Current.Response.Write("<B>");
        HttpContext.Current.Response.Write(oUserLoginDetail.VC_COMPANYNAME);
        HttpContext.Current.Response.Write("</B>");
        HttpContext.Current.Response.Write("</TD>");
        HttpContext.Current.Response.Write("</TR>");

        HttpContext.Current.Response.Write("<TR>");
        HttpContext.Current.Response.Write("<TD style='font-size:12.0pt;' align='center' colspan=" + columnscount + ">");
        HttpContext.Current.Response.Write("<B>");
        HttpContext.Current.Response.Write(" " + title + " ");
        HttpContext.Current.Response.Write("</B>");
        HttpContext.Current.Response.Write("</TD>");
        HttpContext.Current.Response.Write("</TR>");

        HttpContext.Current.Response.Write("<TR>");
        HttpContext.Current.Response.Write("<TD  align='center' colspan=" + columnscount + ">");
        HttpContext.Current.Response.Write("<B>");
        HttpContext.Current.Response.Write("DATED:" + DateTime.Now.ToString() + "");
        HttpContext.Current.Response.Write("</B>");
        HttpContext.Current.Response.Write("</TD>");
        HttpContext.Current.Response.Write("</TR>");


        HttpContext.Current.Response.Write("<TR>");

        foreach (DataColumn dtcol in table.Columns)
        {
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write(dtcol.ColumnName.Replace("_", " "));
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</Td>");

        }

        //HttpContext.Current.Response.Write("<Td align='center' valing='top'>");
        //HttpContext.Current.Response.Write("<B>");
        //HttpContext.Current.Response.Write("Recipe Details");
        //HttpContext.Current.Response.Write("</B>");
        //HttpContext.Current.Response.Write("</Td>");

        //HttpContext.Current.Response.Write("</TR>");
        foreach (DataRow row in table.Rows)
        {//write in new row
            HttpContext.Current.Response.Write("<TR>");
            for (int i = 0; i < table.Columns.Count; i++)
            {
                HttpContext.Current.Response.Write("<Td align=left valign=Top>");

                HttpContext.Current.Response.Write(row[i].ToString());
                //******************************************//
                //      if (i == 15)
                //      {

                //          HttpContext.Current.Response.Write("<Td align=left valign=Top>");

                //          DataView dvBASEQUALITY = new DataView(dtBASEQUALITY);
                //          dvBASEQUALITY.RowFilter = "CUSTOMER_REQ_NO='" + row["CUSTOMER_REQ_NO"].ToString() + "' and LAB_DIP_NO='" + row["LAB_DIP_NO"].ToString() + "' AND LR_OPTION='" + row["LR_OPTION"].ToString() + "'";

                //          if (dvBASEQUALITY.Count > 0)
                //          {
                //              HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
                //"borderColor='#000000' cellSpacing='0' cellPadding='0' " +
                //"style='font-size:10.0pt; font-family:Calibri; background:white;'>");
                //              HttpContext.Current.Response.Write("<TR>");

                //              foreach (DataColumn dtcol in dtBASEQUALITY.Columns)
                //              {
                //                  HttpContext.Current.Response.Write("<Td bgcolor=silver>");
                //                  HttpContext.Current.Response.Write("<B>");
                //                  HttpContext.Current.Response.Write(dtcol.ColumnName.Replace("_", " "));
                //                  HttpContext.Current.Response.Write("</B>");
                //                  HttpContext.Current.Response.Write("</Td>");

                //              }
                //              HttpContext.Current.Response.Write("</TR>");
                //              for (int j = 0; j < dvBASEQUALITY.Count; j++)
                //              {
                //                  HttpContext.Current.Response.Write("<Tr>");
                //                  for (int i1 = 0; i1 < dvBASEQUALITY.Table.Columns.Count; i1++)
                //                  {
                //                      HttpContext.Current.Response.Write("<Td >");
                //                      HttpContext.Current.Response.Write(dvBASEQUALITY[j][i1].ToString());
                //                      HttpContext.Current.Response.Write("</Td>");

                //                  }
                //                  HttpContext.Current.Response.Write("</Tr>");
                //              }
                //              HttpContext.Current.Response.Write("</Table>");
                //          }
                //          HttpContext.Current.Response.Write("</Td>");

                //      }



                //***********************************************//   

                HttpContext.Current.Response.Write("</Td>");

            }

            HttpContext.Current.Response.Write("</TR>");


        }
        HttpContext.Current.Response.Write("</Table>");
        HttpContext.Current.Response.Write("</font>");
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
        //  HttpContext.Current.ApplicationInstance.CompleteRequest();
    }
    protected void ImportExcel_Click(object sender, ImageClickEventArgs e)
    {
         string strFilename = "Yarn_Return_Against_PO_" + DateTime.Now.ToShortDateString() + ".xls";
        DateTime Sdate = DateTime.Parse(TXTDATEFROM.Text);
        DateTime Edate = DateTime.Parse(TXTDATETO.Text);

        DataTable data = (DataTable)ViewState["GridRecord"];

        ExporttoExcel(data, strFilename, "Yarn_Return_Against_PO");
    
    }
    protected void BTNRECORD_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    private void BindGrid()
    {
        try
        {
            string BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            string COMP_CODE = oUserLoginDetail.COMP_CODE;
            int PO_NUM = 0;
            int.TryParse( ddlPONumber.SelectedItem.Value.Trim(), out PO_NUM);
            int MRN_NUM = 0;
            int.TryParse(ddltrnno.SelectedItem.Value.Trim(),out MRN_NUM);
            string PRTY_CODE = string.Empty;
            if (ddlparty.SelectedIndex != 0)
            {
                PRTY_CODE = ddlparty.SelectedItem.Value.Trim();
            }
            string FROM_DATE = TXTDATEFROM.Text;
            string TO_DATE = TXTDATETO.Text;
            string YARN_CODE = string.Empty;
            if (ddlyarn.SelectedIndex != 0)
            {
                YARN_CODE = ddlyarn.SelectedItem.Value.Trim();
            } 
            int YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            DataTable DATA = SaitexBL.Interface.Method.YRN_IR_MST.RECORDRETURNAGAINSTPO(BRANCH_CODE, COMP_CODE, PO_NUM, MRN_NUM, PRTY_CODE, FROM_DATE, TO_DATE, YARN_CODE, YEAR);
             if(DATA.Rows.Count>0)
             {
                 lblrecd.Text="RECORD FOUND : "+ DATA.Rows.Count.ToString();
                 Get_WO_Detail.DataSource=DATA;
                 Get_WO_Detail.DataBind();
                 ViewState["GridRecord"] = DATA;
             }
            else
             {
                 Get_WO_Detail.DataSource=null;
                 Get_WO_Detail.DataBind();
                 ViewState["GridRecord"] = null;
             }
        }
        catch (Exception xx) { };
    }
    //protected void Get_WO_Detail_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        try
    //        {
    //            GridView gridsub = (GridView)e.Row.FindControl("Grdadj");
    //            Label lbltrnno = (Label)e.Row.FindControl("lbl_CHALLAN_NUMB");
    //            int MRN_NUM = int.Parse(lbltrnno.Text);
    //            DataTable DATA = SaitexBL.Interface.Method.YRN_IR_MST.SUBRECORDRETURNAGAINSTPO(oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.COMP_CODE, MRN_NUM, oUserLoginDetail.DT_STARTDATE.Year);
    //            if (DATA.Rows.Count > 0)
    //            {
    //                gridsub.DataSource = DATA;
    //                gridsub.DataBind();
    //            }
    //            else
    //            {
    //                gridsub.DataSource = null;
    //                gridsub.DataBind();
    //            }
    //        }

    //        catch (Exception xee) { };
    //    }
    //}
    protected void Get_WO_Detail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Get_WO_Detail.PageIndex = e.NewPageIndex;
        BindGrid();
        
    }
}