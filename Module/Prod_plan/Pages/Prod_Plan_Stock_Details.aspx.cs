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
using Obout.ComboBox;
using errorLog;
using DBLibrary;
using Common;

public partial class Module_Prod_plan_Pages_Prod_Plan_Stock_Details : System.Web.UI.Page
{
    private DataTable dtTRN_SUB = null;

    private static string COMP_CODE;
    private static string BRANCH_CODE;
    private static string txtQTY;
    private double lblMaxQTY = 0;
    private static string PRODUCT_TYPE;
    private static string SHADE_FAMILY;
    private static string SHADE_CODE;
    private static string YARN_CODE;
    private static string PI_TYPE;
    private static string BUSINESS_TYPE;
    private static string CR_ORDER_CAT;
    private static string ORDER_TYPE;
    private static string MAC_TRN_NUMB;
    private static string ORDER_NO;
    private static string CR_ST_ORDER_NO;
    private static int YEAR;
    private static string STOCK ;
    private static string PARTY_CODE;

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {

                if (Request.QueryString["COMP_CODE"] != null)
                {
                    COMP_CODE = Request.QueryString["COMP_CODE"].Trim();
                }
                if (Request.QueryString["BRANCH_CODE"] != null)
                {
                    BRANCH_CODE = Request.QueryString["BRANCH_CODE"].Trim();
                }
                if (Request.QueryString["TRN_NUMB"] != null)
                {
                    MAC_TRN_NUMB = Request.QueryString["TRN_NUMB"].Trim();
                }
                if (Request.QueryString["PI_TYPE"] != null)
                {
                    PI_TYPE = Request.QueryString["PI_TYPE"].ToString();
                }
                if (Request.QueryString["PRODUCT_TYPE"] != null)
                {
                    PRODUCT_TYPE = Request.QueryString["PRODUCT_TYPE"].ToString();
                }
                if (Request.QueryString["SHADE_CODE"] != null)
                {
                    SHADE_CODE = Request.QueryString["SHADE_CODE"].ToString().Trim();
                    txtShadeCode.Text = SHADE_CODE;
                }
                if (Request.QueryString["SHADE_FAMILY"] != null)
                {
                    SHADE_FAMILY = Request.QueryString["SHADE_FAMILY"].ToString().Trim();
                    txtShadeFamily.Text = SHADE_FAMILY;
                }
                if (Request.QueryString["txtQTY"] != null)
                {
                    txtQTY = Request.QueryString["txtQTY"].ToString();
                }
                if (Request.QueryString["lblMaxQTY"] != null)
                {
                    lblMaxQTY = double.Parse(Request.QueryString["lblMaxQTY"].Trim());
                }
               // lblAssQty.Text = lblMaxQTY.ToString();

                if (Request.QueryString["BUSINESS_TYPE"] != null)
                {
                    BUSINESS_TYPE = Request.QueryString["BUSINESS_TYPE"].ToString();
                }
                if (Request.QueryString["ORDER_CAT"] != null)
                {
                    CR_ORDER_CAT = Request.QueryString["ORDER_CAT"].ToString();
                }
                if (Request.QueryString["ORDER_TYPE"] != null)
                {
                    ORDER_TYPE = Request.QueryString["ORDER_TYPE"].ToString();
                }

                if (Request.QueryString["ORDER_NO"] != null)
                {
                    ORDER_NO = Request.QueryString["ORDER_NO"].ToString();
                    CR_ST_ORDER_NO = Request.QueryString["ORDER_NO"].ToString();
                }
                if (Request.QueryString["YEAR"] != null)
                {
                    int year = 0;
                    int.TryParse(Request.QueryString["YEAR"].ToString(), out year);
                    YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                }

                if (Request.QueryString["STOCK"] != null)
                {
                    STOCK = Request.QueryString["STOCK"].ToString();

                }
                if (Request.QueryString["PARTY_CODE"] != null)
                {
                    PARTY_CODE = Request.QueryString["PARTY_CODE"].ToString();
                }
                if (Request.QueryString["YARN_CODE"] != null)
                {
                    YARN_CODE = Request.QueryString["YARN_CODE"].Trim();
                    lblQualityCode.Text = YARN_CODE;
                }
                if (!IsPostBack)
                {
                    //if (Session["dtTRN_SUB"] != null)
                    //{
                    //    dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];
                    //}
                    //else
                    //{
                    //    grdsub_trn.DataSource = null;
                    //    grdsub_trn.DataBind();
                    //    if (dtTRN_SUB != null && dtTRN_SUB.Rows.Count > 0)
                    //    {
                    //        dtTRN_SUB.Clear();
                    //    }
                    //}
                }



                BindMacCodeTRN();
                BindLOTSTOCK();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nsee error log for detail."));
        }
    }


    private void BindMacCodeTRN()
    {
        try
        {
            DataTable data = SaitexDL.Interface.Method.OD_CAPTURE_MST.GetRMPlanningBalanceData(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, BUSINESS_TYPE.ToString(), YARN_CODE.ToString(), PARTY_CODE.ToString());

              txtStockReal.Text = STOCK.ToString();
              if (data.Rows[0]["QTY_REM"].ToString() != string.Empty)
              {
                  txtPlannedQty.Text = data.Rows[0]["QTY_REM"].ToString();
              }
              else 
              {
                  txtPlannedQty.Text = "0";
              
              }
              txtAvlStock.Text = ((double.Parse(txtStockReal.Text.ToString())) - double.Parse(txtPlannedQty.Text.ToString())).ToString();
                        
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

     private void BindLOTSTOCK()
    {
        try
        {



            DataTable data = SaitexDL.Interface.Method.OD_CAPTURE_MST.GetRMPlanningStockData(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, BUSINESS_TYPE.ToString(), PARTY_CODE.ToString());

            if (data != null && data.Rows.Count > 0)
            {
                DataView dv = new DataView(data);
                dv.RowFilter = "YARN_CODE='" + YARN_CODE + "' ";
                DataTable dt = (DataTable)dv.ToTable();
                dt.Columns.Add("PO_RECIVE_QTY", typeof(string));
                dt.Columns.Add("PO_ORDER_QTY", typeof(string));
                dt.Columns.Add("PO_NUMB", typeof(string));
                dt.Columns.Add("BALANCE", typeof(string));
                if (BUSINESS_TYPE == "SALE WORK")
                {
                    for (int i = 0; i < dv.Count; i++)
                    {
                        DataTable dataPO = SaitexDL.Interface.Method.OD_CAPTURE_MST.GetRMPlanningPoData(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, dt.Rows[i]["YARN_CODE"].ToString(), dt.Rows[i]["LOT_NO"].ToString());

                        if (dataPO.Rows.Count != 0)
                        {
                            dt.Rows[i]["PO_RECIVE_QTY"] = dataPO.Rows[0]["PO_RECIVE_QTY"];
                            dt.Rows[i]["PO_ORDER_QTY"] = dataPO.Rows[0]["PO_ORDER_QTY"];
                            dt.Rows[i]["PO_NUMB"] = dataPO.Rows[0]["PO_NUMB"];
                            dt.Rows[i]["BALANCE"] = dataPO.Rows[0]["BALANCE"];
                            dt.AcceptChanges();
                        }
                    }
                }
              //  GridView grdPOTRN = (GridView)grdRow.FindControl("grdPOTRN");
                grdPOTRN.DataSource = dt;
                grdPOTRN.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }




}

