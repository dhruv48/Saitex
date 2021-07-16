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
using Common;
using errorLog;
using Obout.ComboBox;
using Obout.Interface;

public partial class Module_Waste_Controls_Waste_detail_Report : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {

        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (!IsPostBack)
            {
                GetAllDataItemDetail();
                InitialControls();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void InitialControls()
    {
        try
        {
            GrdItemQuery.Visible = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    private DataTable CreateDatatable()
    {
        try
        {
            DataTable dtMain = new DataTable();


            dtMain.Columns.Add("YEAR", typeof(int));
            dtMain.Columns.Add("ITEM_CODE", typeof(string));
            dtMain.Columns.Add("CAT_CODE", typeof(string));
            dtMain.Columns.Add("ITEM_TYPE", typeof(string));
            dtMain.Columns.Add("TRN_NUMB", typeof(string));
            dtMain.Columns.Add("TRN_TYPE", typeof(string));
            dtMain.Columns.Add("TRN_DATE", typeof(DateTime));
            dtMain.Columns.Add("LOT_NO", typeof(string));
            dtMain.Columns.Add("GRADE", typeof(string));
            dtMain.Columns.Add("BAL_QTY", typeof(double));
            dtMain.Columns.Add("FINAL_RATE", typeof(double));
            dtMain.Columns.Add("BAL_VALUE", typeof(double));
            dtMain.Columns.Add("PRTY_NAME", typeof(string));
            dtMain.Columns.Add("PO_TYPE", typeof(string));

            return dtMain;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void GetAllDataItemDetail()
    {
        try
        {
            GetMeterialData();        
            //DataTable dt = CreateDatatable();
            //DataRow dr = dt.NewRow();
            //dt.Rows.Add(dr);
            //GrdItemQuery.DataSource = dt;
            //GrdItemQuery.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }

    private void GetMeterialData()
    {
        string BRANCH_CODE = string.Empty;
        string CAT_CODE = string.Empty;
        string ITEM_TYPE = string.Empty;
        string PRTY_CODE = string.Empty;
        try
        {          
            
           
            
            DataTable dt = SaitexBL.Interface.Method.TX_WASTE_MASTER.GetMeterialData(BRANCH_CODE, CAT_CODE, ITEM_TYPE, PRTY_CODE);
            if (dt.Rows.Count > 0)
            {
                GrdItemQuery.DataSource = dt;
                GrdItemQuery.DataBind();
               
                GrdItemQuery.Visible = true;
            }
            else
            {
                GrdItemQuery.DataSource = null;
                GrdItemQuery.DataBind();
                Common.CommonFuction.ShowMessage("Record Not Available For These Parameter");
               
                GrdItemQuery.Visible = false;

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void AutoFillSearch(int Year, string ItemCode, string ItemCat, string ItemType, string TrnNo, string TrnType, string LotNo, string Grade, double BalQty, double FinalRate, double BalanceValue, string PrtyName, string PoType)
    {
        try
        {
            TextBox txtyear = (TextBox)GrdItemQuery.HeaderRow.FindControl("txtyear");
            TextBox txtitemcode = (TextBox)GrdItemQuery.HeaderRow.FindControl("txtitemcode");
            TextBox txtitemcat = (TextBox)GrdItemQuery.HeaderRow.FindControl("txtitemcat");
            TextBox txtitemtype = (TextBox)GrdItemQuery.HeaderRow.FindControl("txtitemtype");
            TextBox txttrnno = (TextBox)GrdItemQuery.HeaderRow.FindControl("txttrnno");
            TextBox txttrntype = (TextBox)GrdItemQuery.HeaderRow.FindControl("txttrntype");
            //TextBox txttrndate = (TextBox)GrdItemQuery.HeaderRow.FindControl("txttrndate");
            TextBox txtlotno = (TextBox)GrdItemQuery.HeaderRow.FindControl("txtlotno");
            TextBox txtgrade = (TextBox)GrdItemQuery.HeaderRow.FindControl("txtgrade");
            TextBox txtbalqty = (TextBox)GrdItemQuery.HeaderRow.FindControl("txtbalqty");
            TextBox txtfinalrate = (TextBox)GrdItemQuery.HeaderRow.FindControl("txtfinalrate");
            TextBox txtbalancevalue = (TextBox)GrdItemQuery.HeaderRow.FindControl("txtbalancevalue");
            TextBox txtprtyname = (TextBox)GrdItemQuery.HeaderRow.FindControl("txtprtyname");
            TextBox txtpotype = (TextBox)GrdItemQuery.HeaderRow.FindControl("txtpotype");

            txtyear.Text = Year.ToString();
            txtitemcode.Text = ItemCode;
            txtitemcat.Text = ItemCat;
            txtitemtype.Text = ItemType;
            txttrnno.Text = TrnNo;
            txttrntype.Text = TrnType;
            //  txttrndate.Text = TrnDate.ToShortDateString();
            txtlotno.Text = LotNo;
            txtgrade.Text = Grade;
            txtbalqty.Text = BalQty.ToString();
            txtfinalrate.Text = FinalRate.ToString();
            txtbalancevalue.Text = BalanceValue.ToString();
            txtprtyname.Text = PrtyName;
            txtpotype.Text = PoType;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void SearchByKeyword()
    {
        try
        {

            TextBox txtyear = GrdItemQuery.HeaderRow.FindControl("txtyear") as TextBox;
            int Year = 0;
            int.TryParse(txtyear.Text, out Year);

            string ItemCode = ((TextBox)GrdItemQuery.HeaderRow.FindControl("txtitemcode")).Text.Trim();
            string ItemCat = ((TextBox)GrdItemQuery.HeaderRow.FindControl("txtitemcat")).Text.Trim();
            string ItemType = ((TextBox)GrdItemQuery.HeaderRow.FindControl("txtitemtype")).Text.Trim();
            string TrnNo = ((TextBox)GrdItemQuery.HeaderRow.FindControl("txttrnno")).Text.Trim();
            string TrnType = ((TextBox)GrdItemQuery.HeaderRow.FindControl("txttrntype")).Text.Trim();

            string LotNo = ((TextBox)GrdItemQuery.HeaderRow.FindControl("txtlotno")).Text.Trim();
            string Grade = ((TextBox)GrdItemQuery.HeaderRow.FindControl("txtgrade")).Text.Trim();

            TextBox txtbalqty = GrdItemQuery.HeaderRow.FindControl("txtbalqty") as TextBox;
            double BalQty = 0;
            double.TryParse(txtbalqty.Text, out BalQty);

            TextBox txtfinalrate = GrdItemQuery.HeaderRow.FindControl("txtfinalrate") as TextBox;
            double FinalRate = 0;
            double.TryParse(txtfinalrate.Text, out FinalRate);

            TextBox txtbalancevalue = GrdItemQuery.HeaderRow.FindControl("txtbalancevalue") as TextBox;
            double BalanceValue = 0;
            double.TryParse(txtbalancevalue.Text, out BalanceValue);

            //TextBox txt = (TextBox)GrdItemQuery.HeaderRow.FindControl("txttrndate");
            // DateTime TrnDate = System.DateTime.Now.Date;
            // bool bb = DateTime.TryParse(txt.Text, out TrnDate);

            string PrtyName = ((TextBox)GrdItemQuery.HeaderRow.FindControl("txtprtyname")).Text.Trim();
            string PoType = ((TextBox)GrdItemQuery.HeaderRow.FindControl("txtpotype")).Text.Trim();

            DataTable dt = SaitexBL.Interface.Method.TX_WASTE_MASTER.SearchDataByFilter(Year, ItemCode, ItemCat, ItemType, TrnNo, TrnType, LotNo, Grade, BalQty, FinalRate, BalanceValue, PrtyName, PoType);
            if (dt != null && dt.Rows.Count > 0)
            {
                GrdItemQuery.DataSource = dt;
                GrdItemQuery.DataBind();
                AutoFillSearch(Year, ItemCode, ItemCat, ItemType, TrnNo, TrnType, LotNo, Grade, BalQty, FinalRate, BalanceValue, PrtyName, PoType);
            }
            else
            {
                DataTable dtblank = CreateDatatable();
                DataRow dr = dtblank.NewRow();

                dtblank.Rows.Add(dr);
                GrdItemQuery.DataSource = dtblank;
                GrdItemQuery.DataBind();
                AutoFillSearch(Year, ItemCode, ItemCat, ItemType, TrnNo, TrnType, LotNo, Grade, BalQty, FinalRate, BalanceValue, PrtyName, PoType);
                Common.CommonFuction.ShowMessage("Record Not Available By This Parameter.");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GetAllDataItemDetail();
            InitialControls();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void imgbtnExit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Session["RedirectURL"] != null)
            {
                Response.Redirect(Session["RedirectURL"].ToString(), false);
                Session["RedirectURL"] = null;
            }
            else
            {
                Response.Redirect("~/Module/Admin/Pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void Btnyear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            SearchByKeyword();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void GrdItemQuery_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdItemQuery.PageIndex = e.NewPageIndex;
            SearchByKeyword();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
