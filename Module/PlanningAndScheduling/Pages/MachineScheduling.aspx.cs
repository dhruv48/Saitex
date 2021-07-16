using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using SaitexDL.Interface.Method;
using Common;
public partial class Module_PlanningAndScheduling_Pages_MachineScheduling : System.Web.UI.Page
{
    string ORDER_NO = string.Empty;
    string PI_NO = string.Empty;
    string PARTY_CODE = string.Empty;
    string ARTICLE_CODE = string.Empty;
    string SHADE_CODE = string.Empty;
    string PROCESS_ROOT_CODE = string.Empty;
    string PROCESS_CODE = string.Empty;
    string SERIAL_NO = string.Empty;
    string MACHINE_CODE = string.Empty;
    string ORDER_QTY = string.Empty;
    string PLANNED_QTY = string.Empty;
    string REMAINING_QTY = string.Empty;
    string PRODUCT_TYPE = string.Empty;
    string ORDER_TYPE = string.Empty;
    string BRANCH_CODE = string.Empty;
    string COMP_CODE = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request.QueryString["ORDER_NO"]))
        {
            //ORDER_NO=Request.QueryString["ORDER_NO"].ToString();
        }

        if (!string.IsNullOrEmpty(Request.QueryString["PI_NO"]))
        {
            //PI_NO = Request.QueryString["PI_NO"].ToString();
        }

        if (!string.IsNullOrEmpty(Request.QueryString["PARTY_CODE"]))
        {
            //PARTY_CODE = Request.QueryString["PARTY_CODE"].ToString();
        }

        if (!string.IsNullOrEmpty(Request.QueryString["ARTICLE_CODE"]))
        {
            // ARTICLE_CODE = Request.QueryString["ARTICLE_CODE"].ToString();
        }

        if (!string.IsNullOrEmpty(Request.QueryString["SHADE_CODE"]))
        {
            //SHADE_CODE = Request.QueryString["SHADE_CODE"].ToString();
        }

        if (!string.IsNullOrEmpty(Request.QueryString["PROCESS_ROOT_CODE"]))
        {
            //PROCESS_ROOT_CODE = Request.QueryString["PROCESS_ROOT_CODE"].ToString();
        }

        if (!string.IsNullOrEmpty(Request.QueryString["PROCESS_CODE"]))
        {
            //PROCESS_CODE = Request.QueryString["PROCESS_CODE"].ToString();
        }

        if (!string.IsNullOrEmpty(Request.QueryString["SERIAL_NO"]))
        {
            //SERIAL_NO = Request.QueryString["SERIAL_NO"].ToString();
        }

        if (!string.IsNullOrEmpty(Request.QueryString["MACHINE_CODE"]))
        {
            MACHINE_CODE = Request.QueryString["MACHINE_CODE"].ToString();
        }

        if (!string.IsNullOrEmpty(Request.QueryString["PLANNED_QTY"]))
        {
            // PLANNED_QTY = Request.QueryString["PLANNED_QTY"].ToString();
        }

        if (!string.IsNullOrEmpty(Request.QueryString["ORDER_QTY"]))
        {
            //ORDER_QTY = Request.QueryString["ORDER_QTY"].ToString();
        }

        if (!string.IsNullOrEmpty(Request.QueryString["REMAINING_QTY"]))
        {
            // REMAINING_QTY = Request.QueryString["REMAINING_QTY"].ToString();
        }

        if (!string.IsNullOrEmpty(Request.QueryString["ORDER_TYPE"]))
        {
            ORDER_TYPE = Request.QueryString["ORDER_TYPE"].ToString();
        }

        if (!string.IsNullOrEmpty(Request.QueryString["PRODUCT_TYPE"]))
        {
            PRODUCT_TYPE = Request.QueryString["PRODUCT_TYPE"].ToString();
        }

        if (!string.IsNullOrEmpty(Request.QueryString["BRANCH_CODE"]))
        {
            BRANCH_CODE = Request.QueryString["BRANCH_CODE"].ToString();
        }

        if (!string.IsNullOrEmpty(Request.QueryString["COMP_CODE"]))
        {
            COMP_CODE = Request.QueryString["COMP_CODE"].ToString();
        }

        if (!IsPostBack)
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            var smonth = Calendar1.TodaysDate.Month - 1;
            var emonth = Calendar1.TodaysDate.Month + 2;
            var syear = DateTime.Now.Year;
            var sdate = smonth.ToString() + "/1" + "/" + syear.ToString();
            var edate = emonth.ToString() + "/1" + "/" + syear.ToString();
            DateTime.TryParse(sdate, out startDate);
            //DateTime endDate = startDate.AddDays(31);
            DateTime.TryParse(edate, out endDate);


            var dtMachineSchedule = GetMachineScheduleDetailsForSelectDate(startDate, endDate);
            Calendar1.EventDateColumnName = "START_TIME";
            Calendar1.EventDescriptionColumnName = "ORDERDESCRTIPTION";
            Calendar1.EventHeaderColumnName = "ORDER_NO";
            Calendar1.EventSource = dtMachineSchedule;
            Calendar1.DataBind();
            // Session["schedulingDetails"] = null;
        }

        //var dtMachineSchedule = GetEvents();
        //Session["schedulingDetails"] = dtMachineSchedule;
    }

    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        disableEnableControls(true);
        txtOrderNO.Text = ORDER_NO;
        txtQty.Text = ORDER_QTY;
        txtFromCalender.SelectedDate = Calendar1.SelectedDate;
        txtFrom.Text = Calendar1.SelectedDate.ToString("dd/MM/yyyy");
        txtToCalender.SelectedDate = Calendar1.SelectedDate;
        disableEnableControls(false);

        DateTime startDate = DateTime.Now;
        DateTime.TryParse(Calendar1.SelectedDate.ToShortDateString(), out startDate);
        var etime = TimeSpan.Parse(string.Format("{0}:{1}", 23, 59));
        var endDate = startDate.Add(etime);
        var selectedDate = Calendar1.SelectedDate.ToShortDateString();
        var dtSelectedDateEvents = GetMachineScheduleDetailsForSelectDate(startDate, endDate);
        bindDataGrid(dtSelectedDateEvents, gvSelectedDateEvents);
        //Session["schedulingDetails"] = dtSelectedDateEvents;

    }

    public void bindDataGrid(DataTable dt, GridView gridView)
    {
        if (dt.Rows.Count > 0 && dt != null)
        {
            gridView.DataSource = dt;
            gridView.DataBind();
        }
        else
        {
            gridView.DataSource = null;
            gridView.DataBind();
            gridView.EmptyDataText = "No Booking For selected date";
        }
    }

    private DataTable CreateDataTableForMachineDetails()
    {
        var dtMachineDetail = new DataTable();
        dtMachineDetail.Columns.Add("ORDER_NO", typeof(string));
        dtMachineDetail.Columns.Add("PI_NO", typeof(string));
        dtMachineDetail.Columns.Add("PLANNED_QTY", typeof(Int64));
        dtMachineDetail.Columns.Add("PROS_ROUTE_CODE", typeof(string));
        dtMachineDetail.Columns.Add("MACHINE_CODE", typeof(string));
        dtMachineDetail.Columns.Add("TOTAL_NO_OF_MACHINE", typeof(int));
        dtMachineDetail.Columns.Add("AVAILABLE_MACHINE", typeof(int));
        dtMachineDetail.Columns.Add("BOOKED_MACHINE", typeof(int));
        dtMachineDetail.Columns.Add("SCHEDULE_DATE_FROM", typeof(DateTime));
        dtMachineDetail.Columns.Add("SCHEDULE_DATE_TO", typeof(DateTime));
        dtMachineDetail.Columns.Add("SCHEDULE_HOURE", typeof(double));
        dtMachineDetail.Columns.Add("PRODUCT_TYPE", typeof(string));
        dtMachineDetail.Columns.Add("ORDER_TYPE", typeof(string));
        dtMachineDetail.Columns.Add("COMP_CODE", typeof(string));
        dtMachineDetail.Columns.Add("BRANCH_CODE", typeof(string));
        dtMachineDetail.Columns.Add("TDATE", typeof(DateTime));
        dtMachineDetail.Columns.Add("TUSER", typeof(string));
        dtMachineDetail.Columns.Add("ROWSTATE", typeof(string));
        //  dtMachineDetail.Columns.Add("FLAG", typeof(string));
        return dtMachineDetail;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //tblSchedule.Visible = true;
        //btnSubmit.Enabled = true;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!validateControls())
        {
            CommonFuction.ShowMessage("Please Select date from calender.");
        }

        DateTime sDate = DateTime.Now;
        DateTime eDate = DateTime.Now;
        DateTime.TryParse(txtFrom.Text, out sDate);
        DateTime.TryParse(txtTo.Text, out eDate);

        var stime = TimeSpan.Parse(string.Format("{0}:{1}", startTime.Hour, startTime.Minute));
        var etime = TimeSpan.Parse(string.Format("{0}:{1}", endTime.Hour, endTime.Minute));
        var startDateTime = sDate.Add(stime);
        var endDateTime = eDate.Add(etime);

        var Total = endDateTime.Subtract(startDateTime);
        var totalHours = (Total.TotalHours);



        //txtFromCalender.StartDate = Calendar1.SelectedDate;
        //txtFromCalender.EndDate = Calendar1.SelectedDate;
        //txtToCalender.StartDate = Calendar1.SelectedDate;
        //double T_Hours = 0.0;
        //var timediff = edate.Subtract(sdate).Minutes ;
        //double.TryParse(timediff.ToString(),out T_Hours);
        //var T_Hours1 = (T_Hours / 60);



        var schedulingDataTable = (DataTable)Session["schedulingDetails"];
        DataTable dtSchedulting = null;
        if (schedulingDataTable.Rows.Count > 0 && schedulingDataTable != null)
        {
            dtSchedulting = addRecordToDataTable(schedulingDataTable);
        }
        else
        {
            var newdt = CreateDataTableForMachineDetails();
            dtSchedulting = addRecordToDataTable(newdt);
        }
        Session["schedulingDetails"] = dtSchedulting;
        bindDataGrid(dtSchedulting, gvSelectedDateEvents);
        //tblSchedule.Visible = false;
        //btnSubmit.Enabled = false;

    }

    public bool validateControls()
    {
        int qty;
        int.TryParse(txtQty.Text, out qty);
        bool result = true; ;
        if (string.IsNullOrEmpty(txtOrderNO.Text))
        {
            result = false;
        }
        if (string.IsNullOrEmpty(txtQty.Text))
        {
            result = false;
        }
        if (qty < 1)
        {
            result = false;
        }
        if (string.IsNullOrEmpty(txtFrom.Text))
        {
            result = false;
        }
        if (string.IsNullOrEmpty(txtFrom.Text))
        {
            result = false;
        }

        return result;
    }

    protected DataTable addRecordToDataTable(DataTable dt)
    {
        var sdate = txtFrom.Text;
        var edate = txtTo.Text;

        if (!dt.Columns.Contains("ROWSTATE"))
        {
            dt.Columns.Add("ROWSTATE", typeof(string));
            foreach (DataRow dr1 in dt.Rows)
            {
                dr1["ROWSTATE"] = "0";
            }
        }

        DateTime sDate = DateTime.Now;
        DateTime eDate = DateTime.Now;
        DateTime.TryParse(txtFrom.Text, out sDate);
        DateTime.TryParse(txtTo.Text, out eDate);

        var stime = TimeSpan.Parse(string.Format("{0}:{1}", startTime.Hour, startTime.Minute));
        var etime = TimeSpan.Parse(string.Format("{0}:{1}", endTime.Hour, endTime.Minute));
        var startDateTime = sDate.Add(stime);
        var endDateTime = eDate.Add(etime);

        var Total = endDateTime.Subtract(startDateTime);
        var totalHours = (Total.TotalHours);

        var dr = dt.NewRow();
        dr["ORDER_NO"] = ORDER_NO.Trim();
        dr["PI_NO"] = PI_NO.Trim();
        dr["PLANNED_QTY"] = txtQty.Text;
        dr["PROS_ROUTE_CODE"] = PROCESS_ROOT_CODE.Trim();
        dr["MACHINE_CODE"] = MACHINE_CODE.Trim();
        dr["TOTAL_NO_OF_MACHINE"] = 1;
        dr["AVAILABLE_MACHINE"] = 0;
        dr["BOOKED_MACHINE"] = 0;
        dr["SCHEDULE_DATE_FROM"] = startDateTime;
        dr["SCHEDULE_DATE_TO"] = endDateTime;
        dr["SCHEDULE_HOURE"] = totalHours;
        dr["PRODUCT_TYPE"] = "PRODUCT_TYPE";
        dr["ORDER_TYPE"] = ORDER_TYPE.Trim();
        dr["COMP_CODE"] = COMP_CODE.Trim();
        dr["BRANCH_CODE"] = BRANCH_CODE.Trim();
        dr["TDATE"] = DateTime.Now.Date.ToString();
        dr["TUSER"] = string.Empty;
        dr["ROWSTATE"] = "1";
        dt.Rows.Add(dr);
        dt.AcceptChanges();
        return dt;
    }

    protected void Calendar1_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
    {
        var nextMonthDate = e.NewDate;
        var nextMonthLastDate = e.NewDate.AddDays(31);
        var dtSelectedDateEvents = GetMachineScheduleDetailsForSelectDate(nextMonthDate, nextMonthLastDate);
        bindDataGrid(dtSelectedDateEvents, gvSelectedDateEvents);
    }

    protected DataTable GetMachineScheduleDetailsForSelectDate(DateTime startDate, DateTime endDate)
    {
        return SaitexDL.Interface.Method.OD_ORDER_DEVELOPMENT.GetMachineSchedulingDataForDates(PROCESS_ROOT_CODE, MACHINE_CODE, startDate, endDate, COMP_CODE, BRANCH_CODE, PRODUCT_TYPE, ORDER_TYPE);
    }

    protected void disableEnableControls(bool flag)
    {
        txtOrderNO.Enabled = flag;
        //txtQty.Enabled = false;
        txtFrom.Enabled = flag;
    }

    protected void btnSaveSchedult_Click(object sender, EventArgs e)
    {

        var dtDetails = (DataTable)Session["schedulingDetails"];
        if (dtDetails.Rows.Count > 0 && dtDetails != null)
        {
            int iResult = SaitexDL.Interface.Method.OD_ORDER_DEVELOPMENT.SaveMachineScheduleRecord(dtDetails, null);
            if (iResult > 0)
            {
                Session["schedulingDetails"] = null;
                //ResetControls();
                CommonFuction.ShowMessage("Order Development Saved Successfully.");
            }
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {//javascript:BindRate('" + FinalAmount + "','" + TextBoxId + "')
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "", true);
    }
}
