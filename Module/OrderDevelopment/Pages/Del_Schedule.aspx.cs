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

public partial class Module_OrderDevelopment_Pages_Del_Schedule : System.Web.UI.Page
{
    private DataTable dtTRN_DEL_SCHEDULE = null;

    private static string TextBoxOrderQty = string.Empty;
    private static string TextBoxDelDate = string.Empty;

    private static string SSORDER_CAT = string.Empty;
    private static double Total_Qty = 0;
    private static bool IsBOMAdjQty = false;
    private static DateTime Final_Date;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Total_Qty = 0;
                IsBOMAdjQty = false;
                Final_Date = System.DateTime.Now;

                if (Request.QueryString["TextBoxOrderQty"] != null)
                    TextBoxOrderQty = Request.QueryString["TextBoxOrderQty"].Trim();

                if (Request.QueryString["TextBoxDelDate"] != null)
                    TextBoxDelDate = Request.QueryString["TextBoxDelDate"].Trim();

                if (Request.QueryString["ARTICAL_CODE"] != null)
                    ARTICAL_CODE.Text = Request.QueryString["ARTICAL_CODE"].Trim();

                if (Request.QueryString["PI_TYPE"] != null)
                {
                    PI_TYPE.Text = Request.QueryString["PI_TYPE"].ToString();
                }

                if (Request.QueryString["QTY"] != null)
                {
                    Total_Qty = double.Parse(Request.QueryString["QTY"].ToString());
                    if (Total_Qty > 0)
                        IsBOMAdjQty = true;
                }

                if (Request.QueryString["ORDER_CAT"] != null)
                {
                    SSORDER_CAT = Request.QueryString["ORDER_CAT"].ToString();
                }

                if (Session["dtTRN_DEL_SCHEDULE"] != null)
                {
                    if (dtTRN_DEL_SCHEDULE == null)
                        CreateDataTable();
                    dtTRN_DEL_SCHEDULE = (DataTable)Session["dtTRN_DEL_SCHEDULE"];
                    FillGridByDataTable();
                }
                txtTotalQuantity.Text = "0";
                //txtFinalQty.Text = Total_Qty.ToString();

                GetFinalDelDateAndQuantity();

                txtFinalDelDate.Text = Final_Date.ToShortDateString();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nsee error log for detail."));
        }
    }

    private void CreateDataTable()
    {
        try
        {
            dtTRN_DEL_SCHEDULE = new DataTable();
            dtTRN_DEL_SCHEDULE.Columns.Add("UNIQUE_ID", typeof(int));
            dtTRN_DEL_SCHEDULE.Columns.Add("ARTICAL_CODE", typeof(string));
            dtTRN_DEL_SCHEDULE.Columns.Add("PI_TYPE", typeof(string));
            dtTRN_DEL_SCHEDULE.Columns.Add("DEL_ADDRESS", typeof(string));
            dtTRN_DEL_SCHEDULE.Columns.Add("DEL_DATE", typeof(DateTime));
            dtTRN_DEL_SCHEDULE.Columns.Add("DEL_QTY", typeof(double));
            dtTRN_DEL_SCHEDULE.Columns.Add("DEL_REMARKS", typeof(string));
        }
        catch
        {
            throw;
        }
    }

    private void FillGridByDataTable()
    {
        try
        {
            if (Session["dtTRN_DEL_SCHEDULE"] == null)
            {
                CreateDataTable();
                Session["dtTRN_DEL_SCHEDULE"] = dtTRN_DEL_SCHEDULE;
            }

            dtTRN_DEL_SCHEDULE = (DataTable)Session["dtTRN_DEL_SCHEDULE"];


            DataView dv_YRNSPIN_Del_Schedule = new DataView(dtTRN_DEL_SCHEDULE);

            dv_YRNSPIN_Del_Schedule.RowFilter = "ARTICAL_CODE='" + ARTICAL_CODE.Text + "' and PI_TYPE='" + PI_TYPE.Text + "' ";
            if (dv_YRNSPIN_Del_Schedule.Count > 0)
            {
                grdDelSchedule.DataSource = dv_YRNSPIN_Del_Schedule;
                grdDelSchedule.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    private void GetFinalDelDateAndQuantity()
    {
        try
        {
            Final_Date = System.DateTime.Now;

            if (Session["dtTRN_DEL_SCHEDULE"] == null)
            {
                CreateDataTable();
                Session["dtTRN_DEL_SCHEDULE"] = dtTRN_DEL_SCHEDULE;
            }
            Total_Qty = 0;

            dtTRN_DEL_SCHEDULE = (DataTable)Session["dtTRN_DEL_SCHEDULE"];

            if (dtTRN_DEL_SCHEDULE != null && dtTRN_DEL_SCHEDULE.Rows.Count > 0)
            {

                DataView dv = new DataView(dtTRN_DEL_SCHEDULE);
                dv.RowFilter = "ARTICAL_CODE='" + ARTICAL_CODE.Text + "' and PI_TYPE='" + PI_TYPE.Text + "'";
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        DateTime Del_Date = DateTime.Parse(dv[iLoop]["DEL_DATE"].ToString());

                        double Del_Qty = 0;
                        double.TryParse(dv[iLoop]["DEL_QTY"].ToString(), out Del_Qty);

                        if (Del_Date > Final_Date)
                            Final_Date = Del_Date;

                        Total_Qty += Del_Qty;
                    }
                }
            }

            txtTotalQuantity.Text = Total_Qty.ToString();
            txtFinalDelDate.Text = Final_Date.ToShortDateString();
        }
        catch
        {
            throw;
        }
    }

    protected void btnSaveDelRow_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = string.Empty;
            if (ValidateDelivaryRow(out msg))
            {
                AddDeliveryRowToDataTable();
            }
            else
            {
                Common.CommonFuction.ShowMessage(msg);
            }

            FillGridByDataTable();
            GetFinalDelDateAndQuantity();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving delivery data.\r\nsee error log for detail."));
        }
    }

    private bool ValidateDelivaryRow(out string msg)
    {
        try
        {
            msg = string.Empty;
            int iCount = 0;
            int iTotalCount = 0;

            //iTotalCount++;
            //if (txtDelAddress.Text != string.Empty)
            //{
            //    iCount++;
            //}
            //else
            //{
            //    msg += @"\r\nPlease enter valid Delivery Address.";
            //}

            iTotalCount++;
            DateTime Del_date = System.DateTime.Now.Date;
            if (DateTime.TryParse(txtDelDate.Text.Trim(), out Del_date))
            {
                iCount++;
            }
            else
            {
                msg += @"\r\nPlease enter valid Delivery Date.";
            }

            iTotalCount++;
            double Del_qty = 0;
            if (double.TryParse(txtDelQuantity.Text.Trim(), out Del_qty))
            {
                iCount++;
            }
            else
            {
                msg += @"\r\nPlease enter valid Delivery Quantity.";
            }

            if (IsBOMAdjQty)
            {
                iTotalCount++;

                double EditQty = 0;
                if (ViewState["EditQty"] != null)
                    double.TryParse(ViewState["EditQty"].ToString(), out EditQty);

                if (SSORDER_CAT.Equals("INHOUSE"))
                {
                    if (((double.Parse(txtTotalQuantity.Text) - EditQty) - Del_qty) >= 0)
                    {
                        iCount++;
                    }
                    else
                    {
                        msg += @"\r\Delivery Quantity value is greater than Final Qty.";
                    }
                }
                else
                {
                    iCount++;
                }
            }

            if (iCount == iTotalCount)
                return true;
            else
                return false;
        }
        catch
        {
            throw;
        }
    }

    private bool SearchDelivaryScheduleInGrid(string Del_Address, DateTime delDate, int UNIQUE_ID)
    {
        bool Result = false;
        try
        {
            if (grdDelSchedule.Rows.Count > 0)
            {
                foreach (GridViewRow grdRow in grdDelSchedule.Rows)
                {
                    //  Label txtDelAdds = (Label)grdRow.FindControl("txtDelAdd");
                    Label txtDelDates = (Label)grdRow.FindControl("txtDelDate");
                    LinkButton lbtnEdit = (LinkButton)grdRow.FindControl("lbtnEdit");
                    int iUNIQUE_ID = int.Parse(lbtnEdit.CommandArgument.Trim());
                    DateTime dDel_date = DateTime.Parse(txtDelDates.Text.Trim());
                    //  string sDel_Address = txtDelAdds.Text.Trim();

                    if (dDel_date == delDate && UNIQUE_ID != iUNIQUE_ID)
                        Result = true;
                }
            }
            return Result;
        }
        catch
        {
            throw;
        }
    }

    private void AddDeliveryRowToDataTable()
    {
        try
        {
            if (Session["dtTRN_DEL_SCHEDULE"] == null)
            {
                CreateDataTable();
                Session["dtTRN_DEL_SCHEDULE"] = dtTRN_DEL_SCHEDULE;
            }

            dtTRN_DEL_SCHEDULE = (DataTable)Session["dtTRN_DEL_SCHEDULE"];

            if (dtTRN_DEL_SCHEDULE == null)
                CreateDataTable();

            DateTime Del_date = DateTime.Parse(txtDelDate.Text.Trim());
            string Del_Address = ""; //txtDelAddress.Text.Trim();

            int UNIQUE_ID = 0;
            if (ViewState["UNIQUE_ID"] != null)
                UNIQUE_ID = int.Parse(ViewState["UNIQUE_ID"].ToString());
            bool bb = SearchDelivaryScheduleInGrid(Del_Address, Del_date, UNIQUE_ID);

            if (!bb)
            {
                double Del_Qty = 0;
                double.TryParse(txtDelQuantity.Text.Trim(), out Del_Qty);
                if (Del_Qty > 0)
                {
                    if (UNIQUE_ID > 0)
                    {
                        DataView dv = new DataView(dtTRN_DEL_SCHEDULE);
                        dv.RowFilter = "ARTICAL_CODE='" + ARTICAL_CODE.Text + "' and PI_TYPE='" + PI_TYPE.Text + "' and UNIQUE_ID=" + UNIQUE_ID;
                        if (dv.Count > 0)
                        {
                            dv[0]["DEL_ADDRESS"] = ".";// txtDelAddress.Text.Trim();
                            dv[0]["DEL_DATE"] = Del_date;
                            dv[0]["DEL_QTY"] = Del_Qty;
                            dv[0]["DEL_REMARKS"] = txtDelRemarks.Text.Trim();

                            dtTRN_DEL_SCHEDULE.AcceptChanges();
                        }
                    }
                    else
                    {
                        DataRow dr = dtTRN_DEL_SCHEDULE.NewRow();

                        DataView dv = new DataView(dtTRN_DEL_SCHEDULE);
                        dv.RowFilter = "ARTICAL_CODE='" + ARTICAL_CODE.Text + "' and PI_TYPE='" + PI_TYPE.Text + "' ";
                        dr["UNIQUE_ID"] = dv.Count + 1;

                        dr["ARTICAL_CODE"] = ARTICAL_CODE.Text;
                        dr["PI_TYPE"] = PI_TYPE.Text;

                        dr["DEL_ADDRESS"] = ".";// txtDelAddress.Text.Trim();
                        dr["DEL_DATE"] = Del_date;
                        dr["DEL_QTY"] = Del_Qty;
                        dr["DEL_REMARKS"] = txtDelRemarks.Text.Trim();

                        dtTRN_DEL_SCHEDULE.Rows.Add(dr);
                    }
                    RefreshDeliveryRow();
                    Session["dtTRN_DEL_SCHEDULE"] = dtTRN_DEL_SCHEDULE;
                }
                else
                {
                    Common.CommonFuction.ShowMessage(@"Quantity can not be zero");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage(@"Delivery already Scheduled on provided date.");
            }
        }
        catch
        {
            throw;
        }
    }

    private void RefreshDeliveryRow()
    {
        try
        {
            // txtDelAddress.Text = string.Empty;
            txtDelDate.Text = string.Empty;
            txtDelQuantity.Text = string.Empty;
            txtDelRemarks.Text = string.Empty;

            ViewState["EditQty"] = 0;
            ViewState["UNIQUE_ID"] = 0;
        }
        catch
        {
        }
    }

    protected void btnCancelDelRow_Click(object sender, EventArgs e)
    {
        try
        {
            RefreshDeliveryRow();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in refreshing delivery data.\r\nsee error log for detail."));
        }
    }

    protected void grdDelSchedule_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UNIQUE_ID = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "delDEL")
            {
                RemoveRowFromDataTable(UNIQUE_ID);
            }
            else if (e.CommandName == "editDEL")
            {
                FillComponentForEdit(UNIQUE_ID);
            }

            FillGridByDataTable();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting delivery data for updation / deletion.\r\nsee error log for detail."));
        }
    }

    private void RemoveRowFromDataTable(int UNIQUE_ID)
    {
        try
        {
            if (Session["dtTRN_DEL_SCHEDULE"] == null)
            {
                CreateDataTable();
                Session["dtTRN_DEL_SCHEDULE"] = dtTRN_DEL_SCHEDULE;
            }

            dtTRN_DEL_SCHEDULE = (DataTable)Session["dtTRN_DEL_SCHEDULE"];

            if (dtTRN_DEL_SCHEDULE.Rows.Count == 1)
                dtTRN_DEL_SCHEDULE.Rows.Clear();

            foreach (DataRow dr in dtTRN_DEL_SCHEDULE.Rows)
            {
                int iUNIQUE_ID = int.Parse(dr["UNIQUE_ID"].ToString());
                string sARTICAL_CODE = dr["ARTICAL_CODE"].ToString();
                string sPI_TYPE = dr["PI_TYPE"].ToString();


                if (iUNIQUE_ID == UNIQUE_ID && ARTICAL_CODE.Text == sARTICAL_CODE && PI_TYPE.Text == sPI_TYPE)
                {
                    dtTRN_DEL_SCHEDULE.Rows.Remove(dr);
                    dtTRN_DEL_SCHEDULE.AcceptChanges();
                    break;
                }
            }
            Session["dtTRN_DEL_SCHEDULE"] = dtTRN_DEL_SCHEDULE;
        }
        catch
        {
            throw;
        }
    }

    private void FillComponentForEdit(int UNIQUE_ID)
    {
        try
        {
            if (Session["dtTRN_DEL_SCHEDULE"] == null)
            {
                CreateDataTable();
                Session["dtTRN_DEL_SCHEDULE"] = dtTRN_DEL_SCHEDULE;
            }

            dtTRN_DEL_SCHEDULE = (DataTable)Session["dtTRN_DEL_SCHEDULE"];

            DataView dvFillForEdit = new DataView(dtTRN_DEL_SCHEDULE);
            dvFillForEdit.RowFilter = "ARTICAL_CODE='" + ARTICAL_CODE.Text + "' and PI_TYPE='" + PI_TYPE.Text + "' and UNIQUE_ID=" + UNIQUE_ID;
            if (dvFillForEdit.Count > 0)
            {
                //   txtDelAddress.Text = dvFillForEdit[0]["DEL_ADDRESS"].ToString();
                txtDelDate.Text = dvFillForEdit[0]["DEL_DATE"].ToString();
                txtDelQuantity.Text = dvFillForEdit[0]["DEL_QTY"].ToString();
                txtDelRemarks.Text = dvFillForEdit[0]["DEL_REMARKS"].ToString();

                btnSaveDelRow.Focus();
                ViewState["UNIQUE_ID"] = UNIQUE_ID;
                ViewState["EditQty"] = txtDelQuantity.Text;
            }
        }
        catch
        {
            throw;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["dtTRN_DEL_SCHEDULE"] == null)
            {
                CreateDataTable();
                Session["dtTRN_DEL_SCHEDULE"] = dtTRN_DEL_SCHEDULE;
            }

            dtTRN_DEL_SCHEDULE = (DataTable)Session["dtTRN_DEL_SCHEDULE"];

            Session["dtTRN_DEL_SCHEDULE"] = dtTRN_DEL_SCHEDULE;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:BindDelShedule('" + Total_Qty + "','" + TextBoxOrderQty + "','" + Final_Date + "','" + TextBoxDelDate + "')", true);

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in submitting delivery data.\r\nsee error log for detail."));
        }
    }

}
