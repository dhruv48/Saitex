using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DBLibrary;
using Common;
using errorLog;

public partial class Module_PlanningAndScheduling_Pages_LotDetail : System.Web.UI.Page
{

    DataTable dtLOT_DET = null;
    private static string COMP_CODE = string.Empty;
    private static string BRANCH_CODE = string.Empty;
    private static string BUSINESS_TYPE = string.Empty;
    private static string PRODUCT_TYPE = string.Empty;
    private static string ORDER_CAT = string.Empty;
    private static string ORDER_TYPE = string.Empty;
    private static string ORDER_NO = string.Empty;
    private static string PI_TYPE = string.Empty;
    private static string PI_NO = string.Empty;
    private static string ARTICAL_CODE = string.Empty;
    private static string SHADE_CODE = string.Empty;
    private static string ORD_QTY = string.Empty;
    private static string LOT_FLAG = string.Empty;
    private static string LOT_ID = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
 
    SaitexDM.Common.DataModel.OD_CAPT_MST oOD_CAPT_MST;

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
                if (Request.QueryString["BUSINESS_TYPE"] != null)
                {
                    BUSINESS_TYPE = Request.QueryString["BUSINESS_TYPE"].Trim();
                }
                if (Request.QueryString["PRODUCT_TYPE"] != null)
                {
                    PRODUCT_TYPE = Request.QueryString["PRODUCT_TYPE"].Trim();
                }
                if (Request.QueryString["ORDER_CAT"] != null)
                {
                    ORDER_CAT = Request.QueryString["ORDER_CAT"].Trim();
                }
                if (Request.QueryString["ORDER_TYPE"] != null)
                {
                    ORDER_TYPE = Request.QueryString["ORDER_TYPE"].Trim();
                }
                if (Request.QueryString["ORDER_NO"] != null)
                {
                    ORDER_NO = Request.QueryString["ORDER_NO"].Trim();
                }
                if (Request.QueryString["PI_TYPE"] != null)
                {
                    PI_TYPE = Request.QueryString["PI_TYPE"].Trim();
                }
                if (Request.QueryString["PI_NO"] != null)
                {
                    PI_NO = Request.QueryString["PI_NO"].Trim();
                }
                if (Request.QueryString["ARTICAL_CODE"] != null)
                {
                    ARTICAL_CODE = Request.QueryString["ARTICAL_CODE"].Trim();
                }
                if (Request.QueryString["SHADE_CODE"] != null)
                {
                    SHADE_CODE = Request.QueryString["SHADE_CODE"].Trim();
                }
                if (Request.QueryString["ORD_QTY"] != null)
                {
                    ORD_QTY = Request.QueryString["ORD_QTY"].Trim();
                }

                if (Request.QueryString["LOT_ID"] != null)
                {
                    LOT_ID = Request.QueryString["LOT_ID"].Trim();
                }
                if (Request.QueryString["LOT_FLAG"] != null)
                {
                    LOT_FLAG = Request.QueryString["LOT_FLAG"].Trim();

                    if (LOT_FLAG.Equals("1"))
                    {
                        CheckBox1.Checked = true;
                        DisableformByFlag();
                    }
                    else
                    {
                        CheckBox1.Checked = false;
                    }


                    if (Session["dtLOT_DET"] != null)
                    {
                        if (dtLOT_DET == null)
                        {
                            CreateLOTTable();
                        }
                        dtLOT_DET = (DataTable)Session["dtLOT_DET"];
                    }
                }
            }
            
            InitialControl();
            
            oOD_CAPT_MST = new SaitexDM.Common.DataModel.OD_CAPT_MST();
            oOD_CAPT_MST.COMP_CODE = COMP_CODE;
            oOD_CAPT_MST.BRANCH_CODE = BRANCH_CODE;
            oOD_CAPT_MST.BUSINESS_TYPE = BUSINESS_TYPE;
            oOD_CAPT_MST.ORDERTYPE = ORDER_TYPE;
            oOD_CAPT_MST.PA_NO = PI_NO;
            oOD_CAPT_MST.LOT_ID = LOT_ID;
           // DataTable dt1 = SaitexBL.Interface.Method.OD_CAPT_MST.GetLOTDetail(oOD_CAPT_MST, PRODUCT_TYPE, PI_NO, ARTICAL_CODE, SHADE_CODE);
            DataTable dt1 = SaitexBL.Interface.Method.OD_CAPT_MST.GetLOTDetail1(oOD_CAPT_MST, PRODUCT_TYPE, ORDER_CAT, ORDER_NO, PI_TYPE, ARTICAL_CODE, SHADE_CODE);
         
            if (dt1 != null && dt1.Rows.Count > 0)
            {
                Session["dtLOT_DET"] = dt1;
                MapLotDet();
                BindLOTGrid();
                Clear();
            }

        
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nsee error log for detail."));
            throw ex;
        }
    }

    private void Clear()
    {
        txtLotId.Text = string.Empty;
        txtPANO.Text = string.Empty;
        txtProdType.Text = string.Empty;
      
        txtArticalCode.Text = string.Empty;
        txtOrdQty.Text = string.Empty;
        txtShadeCode.Text = string.Empty;

    
    }

    private void MapLotDet()
    {
        try
        {
            if (Session["dtLOT_DET"] != null)
            {
                dtLOT_DET = (DataTable)Session["dtLOT_DET"];
            }
            else
            {
                CreateLOTTable();
            }

            if (!dtLOT_DET.Columns.Contains("UNIQUE_ID"))
            {
                dtLOT_DET.Columns.Add("UNIQUE_ID", typeof(int));
            }

            for (int iLoop = 0; iLoop < dtLOT_DET.Rows.Count; iLoop++)
            {
                dtLOT_DET.Rows[iLoop]["UNIQUE_ID"] = iLoop + 1;
            }

            Session["dtLOT_DET"] = dtLOT_DET;
        }
        catch (Exception)
        {
            
            throw;
        }
    }

    private void BindLOTGrid()
    {
        try
        {
            if (Session["dtLOT_DET"] != null)
            {
                dtLOT_DET = (DataTable)Session["dtLOT_DET"];
            }
            else
            {
                CreateLOTTable();
            }

            DataView dv = new DataView(dtLOT_DET);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("COMP_CODE= '" + COMP_CODE + "' AND ");
            sb.Append("BRANCH_CODE= '" + BRANCH_CODE + "' AND ");
            sb.Append("BUSINESS_TYPE= '" + BUSINESS_TYPE + "' AND ");
            sb.Append("PRODUCT_TYPE= '" + PRODUCT_TYPE + "' AND ");
            sb.Append("ORDER_CAT= '" + ORDER_CAT + "' AND ");
            sb.Append("ORDER_TYPE= '" + ORDER_TYPE +"' AND ");
            sb.Append("ORDER_NO= '" + ORDER_NO + "' AND ");
            sb.Append("PI_TYPE= '" + PI_TYPE +"' AND ");
            sb.Append("PI_NO= '" + PI_NO + "' AND ");
            sb.Append("ARTICAL_CODE= '" + ARTICAL_CODE + "' AND ");
            sb.Append("SHADE_CODE= '" + SHADE_CODE + "' AND ");
            sb.Append("LOT_ID= '" + LOT_ID + "'");
            dv.RowFilter = sb.ToString();
            if (dv.Count > 0)
            {
                grdLOT.DataSource = dv;
                grdLOT.DataBind();
            }
            else
            {
                grdLOT.DataSource = null;
                grdLOT.DataBind();
            }

            
        }
        catch (Exception)
        {
            
            throw;
        }
    }

    private void InitialControl()
    {
        try
        {
         
            txtLotId.Text = LOT_ID;
            txtPANO.Text = PI_NO;
            txtProdType.Text = PRODUCT_TYPE;
            txtArticalCode.Text = ARTICAL_CODE;
            txtOrdQty.Text = ORD_QTY;
            txtShadeCode.Text = SHADE_CODE;
        }
        catch (Exception)
        {
            
            throw;
        }
    }

    private void CreateLOTTable()
    {
        try
        {
            dtLOT_DET = new DataTable();
            dtLOT_DET.Columns.Add("UNIQUE_ID", typeof(int));
            dtLOT_DET.Columns.Add("LOT_ID", typeof(string));
            dtLOT_DET.Columns.Add("COMP_CODE", typeof(string));
            dtLOT_DET.Columns.Add("BRANCH_CODE", typeof(string));
            dtLOT_DET.Columns.Add("BUSINESS_TYPE", typeof(string));
            dtLOT_DET.Columns.Add("PRODUCT_TYPE", typeof(string));
            dtLOT_DET.Columns.Add("ORDER_CAT", typeof(string));
            dtLOT_DET.Columns.Add("ORDER_TYPE", typeof(string));
            dtLOT_DET.Columns.Add("ORDER_NO", typeof(string));
            dtLOT_DET.Columns.Add("PI_TYPE", typeof(string));
            dtLOT_DET.Columns.Add("PI_NO", typeof(string));
            dtLOT_DET.Columns.Add("ARTICAL_CODE", typeof(string));
            dtLOT_DET.Columns.Add("SHADE_CODE", typeof(string));
            dtLOT_DET.Columns.Add("ORD_QTY", typeof(double));
            dtLOT_DET.Columns.Add("LOT_QTY", typeof(double));
            dtLOT_DET.Columns.Add("REM_QTY", typeof(double));

            Session["dtLOT_DET"] = dtLOT_DET;

        }
        catch
        {
            
            throw;
        }
    }

    private void DisableformByFlag()
    {
        try
        {
            txtLotId.Enabled = false;
            txtPANO.Enabled = false;
            txtProdType.Enabled = false;
            txtArticalCode.Enabled = false;
            txtShadeCode.Enabled = false;
            txtOrdQty.Enabled = false;
            txtRemQty.Enabled = false;
            BtnLOTCancel.Enabled = false;
            BtnLOTSave.Enabled = false;
            btnSave.Enabled = false;
            btnUpdate.Enabled = false;

           
        }
        catch
        {
            
            throw;
        }
    }
    protected void BtnLOTSave_Click(object sender, EventArgs e)
    {
        
        try
        {
            if (txtLotQty.Text != "")
            {
                if (Session["dtLOT_DET"] != null)
                {
                    dtLOT_DET = (DataTable)Session["dtLOT_DET"];
                }
                else
                {
                    CreateLOTTable();
                }
                if (dtLOT_DET.Rows.Count < 15)
                {
                    if (double.Parse(txtRemQty.Text) >= 0)
                    {
                        if (txtLotQty.Text != "")
                        {
                            int UNIQUE_ID = 0;
                            if (ViewState["UNIQUE_ID"] != null)
                            {
                                UNIQUE_ID = int.Parse(ViewState["UNIQUE_ID"].ToString());
                            }

                            bool bb = SearchInLOTGRID(txtProdType.Text, txtPANO.Text, txtRemQty.Text, ARTICAL_CODE, SHADE_CODE, UNIQUE_ID);
                            if (!bb)
                            {
                                if (UNIQUE_ID > 0)
                                {
                                    DataView dv = new DataView(dtLOT_DET);
                                    StringBuilder sb = new StringBuilder();
                                    sb.Append("COMP_CODE= '" + COMP_CODE + "' AND ");
                                    sb.Append("BRANCH_CODE= '" + BRANCH_CODE + "' AND ");
                                    sb.Append("BUSINESS_TYPE= '" + BUSINESS_TYPE + "' AND ");
                                    sb.Append("PRODUCT_TYPE= '" + PRODUCT_TYPE + "' AND ");
                                    sb.Append("ORDER_CAT= '" + ORDER_CAT + "' AND ");
                                    sb.Append("ORDER_TYPE = '" + ORDER_TYPE + "' AND ");
                                    sb.Append("ORDER_NO= '" + ORDER_NO + "' AND ");
                                    sb.Append("PI_TYPE= '" + PI_TYPE + "' AND ");
                                    sb.Append("PI_NO= '" + PI_NO + "' AND ");
                                    sb.Append("ARTICAL_CODE= '" + ARTICAL_CODE + "' AND ");
                                    sb.Append("SHADE_CODE= '" + SHADE_CODE + "' AND ");
                                    sb.Append("ORD_QTY= '" + ORD_QTY + "' AND ");
                                    sb.Append("LOT_ID= '" + LOT_ID + "' AND ");
                                    sb.Append("UNIQUE_ID= '" + UNIQUE_ID + "'");

                                    dv.RowFilter = sb.ToString();

                                    if (dv.Count > 0)
                                    {
                                        dv[0]["LOT_ID"] = LOT_ID;
                                        dv[0]["COMP_CODE"] = COMP_CODE;
                                        dv[0]["BRANCH_CODE"] = BRANCH_CODE;
                                        dv[0]["BUSINESS_TYPE"] = BUSINESS_TYPE;
                                        dv[0]["PRODUCT_TYPE"] = PRODUCT_TYPE;
                                        dv[0]["ORDER_CAT"] = ORDER_CAT;
                                        dv[0]["ORDER_TYPE"] = ORDER_TYPE;
                                        dv[0]["ORDER_NO"] = ORDER_NO;
                                        dv[0]["PI_TYPE"] = PI_TYPE;
                                        dv[0]["PI_NO"] = PI_NO;
                                        dv[0]["ARTICAL_CODE"] = ARTICAL_CODE;
                                        dv[0]["SHADE_CODE"] = SHADE_CODE;
                                        dv[0]["ORD_QTY"] = ORD_QTY;
                                        dv[0]["LOT_QTY"] = txtLotQty.Text.Trim();
                                        dv[0]["REM_QTY"] = txtRemQty.Text.Trim();

                                        dtLOT_DET.AcceptChanges();
                                    }

                                }

                                else
                                {
                                    DataRow dr = dtLOT_DET.NewRow();
                                    dr["UNIQUE_ID"] = dtLOT_DET.Rows.Count + 1;
                                    dr["LOT_ID"] = LOT_ID;
                                    dr["COMP_CODE"] = COMP_CODE;
                                    dr["BRANCH_CODE"] = BRANCH_CODE;
                                    dr["BUSINESS_TYPE"] = BUSINESS_TYPE;
                                    dr["PRODUCT_TYPE"] = PRODUCT_TYPE;
                                    dr["ORDER_CAT"] = ORDER_CAT;
                                    dr["ORDER_TYPE"] = ORDER_TYPE;
                                    dr["ORDER_NO"] = ORDER_NO;
                                    dr["PI_TYPE"] = PI_TYPE;
                                    dr["PI_NO"] = PI_NO;
                                    dr["ARTICAL_CODE"] = ARTICAL_CODE;
                                    dr["SHADE_CODE"] = SHADE_CODE;
                                    dr["ORD_QTY"] = double.Parse(ORD_QTY);
                                    dr["LOT_QTY"] = double.Parse(txtLotQty.Text.Trim());
                                    dr["REM_QTY"] = double.Parse(txtRemQty.Text.Trim());

                                    dtLOT_DET.Rows.Add(dr);
                                }
                                RefreshLOTRow();
                            }

                            else
                            {

                            }
                            Session["dtLOT_DET"] = dtLOT_DET;
                            BindLOTGrid();

                        }
                    }

                    else
                    {
                        txtRemQty.Text = (double.Parse(txtLotQty.Text) + double.Parse(txtRemQty.Text)).ToString();
                        txtLotQty.Text = "";
                        Common.CommonFuction.ShowMessage("Lot Quantity can not be more than remaining qty");
                    }
                }
            }

            else
            {

                Common.CommonFuction.ShowMessage("Please Enter Lot Qty.");
            }
            
        }
        catch (Exception ex)
        {

            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving LOT Detail Row.\r\nSee error log for detail."));
        }
    }

    private void RefreshLOTRow()
    {
        try
        {
            txtLotQty.Text = string.Empty;
            ViewState["UNIQUE_ID"] = null;
        }
        catch 
        {
            
            throw;
        }
    }

    private bool SearchInLOTGRID(string PROD_TYPE, string PANO, string REM_QTY, string ARTICAL_CODE, string SHADE_CODE, int UNIQUE_ID)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in grdLOT.Rows)
            {
                Label txtProd_Type = (Label)grdRow.FindControl("lblProductType");
                Label txtPA_no = (Label)grdRow.FindControl("lblPANO");
                Label txtRem_qty = (Label)grdRow.FindControl("lblRem_Qty");
                Label txtArtical_Code = (Label)grdRow.FindControl("lblArticalCode");
                Label txtShade_COde = (Label)grdRow.FindControl("lblShadeCode");
                Button lnkDelete = (Button)grdRow.FindControl("lnkLOTDelete");
                int iUNIQUE_ID = int.Parse(lnkDelete.CommandArgument.Trim());

                if (txtProd_Type.Text.Trim() == PROD_TYPE && txtPANO.Text.Trim() == PANO && txtRem_qty.Text.Trim() == REM_QTY && txtArtical_Code.Text.Trim() == ARTICAL_CODE && txtShade_COde.Text.Trim() == SHADE_CODE && UNIQUE_ID != iUNIQUE_ID) 
                {
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
    protected void BtnLOTCancel_Click(object sender, EventArgs e)
    {
        try
        {
            RefreshLOTRow();
        }
        catch 
        {
            
            throw;
        }
    }
    protected void txtLotQty_TextChanged(object sender, EventArgs e)
    {
        double d = 0;
        if (txtRemQty.Text != "0")
        {
            if (txtRemQty.Text == string.Empty)
            {
                
                d = double.Parse(txtOrdQty.Text) - double.Parse(txtLotQty.Text);
                txtRemQty.Text = d.ToString();
            }
            else 
            {
               d = double.Parse(txtRemQty.Text) - double.Parse(txtLotQty.Text);
               txtRemQty.Text = d.ToString();
            }
        } 
        else
        {
            txtLotQty.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Remaining Quantity reached zero. Lot cannot create now.');", true);
        }

    }
    protected void grdLOT_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UNIQUE_ID = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "LOTEdit")
            {
                FillLOTByGrid(UNIQUE_ID);
            }

            else if (e.CommandName == "LOTDelete")
            {
                DeleteLOTRow(UNIQUE_ID);
                BindLOTGrid();
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }

    private void DeleteLOTRow(int UNIQUE_ID)
    {
        try
        {
            double abc = 0;
            if (Session["dtLOT_DET"] != null)
            {
                dtLOT_DET = (DataTable)Session["dtLOT_DET"];
            }
            else
            {
                CreateLOTTable();
            }
            if (dtLOT_DET.Rows.Count == 1)
            {
                abc = double.Parse(dtLOT_DET.Rows[0]["LOT_QTY"].ToString());
                txtRemQty.Text = (double.Parse(txtRemQty.Text) + abc).ToString();
                
                dtLOT_DET.Rows.Clear();
               
            }
            else
            {
                foreach (DataRow dr in dtLOT_DET.Rows)
                {
                    int iUNIQUE_ID = int.Parse(dr["UNIQUE_ID"].ToString());
                    if (iUNIQUE_ID == UNIQUE_ID)
                    {
                        txtRemQty.Text = (double.Parse(dr["LOT_QTY"].ToString()) + double.Parse(txtRemQty.Text)).ToString();
                        dtLOT_DET.Rows.Remove(dr);
                        break;

                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtLOT_DET.Rows)
                {
                    iCount = iCount + 1;
                    dr["UNIQUE_ID"] = iCount;
                }

                Session["dtLOT_DET"] = dtLOT_DET;
            }
        }
        catch 
        {
            
            throw;
        }
    }

    private void FillLOTByGrid(int UNIQUE_ID)
    {
        try
        {
            if (Session["dtLOT_DET"] != null)
            {
                dtLOT_DET = (DataTable)Session["dtLOT_DET"];
            }
            else
            {
                CreateLOTTable();
            }
            DataView dv = new DataView(dtLOT_DET);
            dv.RowFilter = "UNIQUE_ID=" + UNIQUE_ID;
            if (dv.Count > 0)
            {
                txtLotQty.Text = dv[0]["LOT_QTY"].ToString();
                double d = double.Parse(txtLotQty.Text);
                double r = double.Parse(txtRemQty.Text);
                r = r + d;
                txtRemQty.Text = r.ToString();

                ViewState["UNIQUE_ID"] = UNIQUE_ID;
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            string BOM = string.Empty;
            string TextBoxBOM = string.Empty;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:BindYRNSPIN_BOM('" + BOM + "','" + TextBoxBOM + "')", true);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in submitting delivery data.\r\nsee error log for detail."));
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtRemQty.Text == "0")
            {
                dtLOT_DET = (DataTable)Session["dtLOT_DET"];
                    string msg = string.Empty;

                    bool Result = SaitexDL.Interface.Method.OD_CAPT_MST.Delete_TRN_LOT(dtLOT_DET);
                    if (Result)
                    {
                        msg += "LOT saved successfully.";
                        Common.CommonFuction.ShowMessage(msg);
                    }
                    if (dtLOT_DET != null)
                    {
                        dtLOT_DET.Dispose();
                        dtLOT_DET = null;
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Lot Saving Failed");
                    }
                }
              
            else
            {
                Common.CommonFuction.ShowMessage("Remaining qty is more than zero. create another lot.");
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        SaitexDM.Common.DataModel.OD_CAPT_MST oOD_CAPT_MST = new SaitexDM.Common.DataModel.OD_CAPT_MST();
        oOD_CAPT_MST.COMP_CODE = COMP_CODE;
        oOD_CAPT_MST.BRANCH_CODE = BRANCH_CODE;
        bool status = false;
        if (CheckBox1.Checked)
        {
            status = true;
        }
        else
        {
            status = false;
        }

        bool Result = SaitexDL.Interface.Method.OD_CAPT_MST.UpdateLOTFlag(status, oOD_CAPT_MST, PRODUCT_TYPE, ORDER_CAT, ORDER_NO, PI_NO, ARTICAL_CODE, SHADE_CODE, ORD_QTY, LOT_ID);
 
        if (Result)
        {
            msg += "LOT flag updated";
            Common.CommonFuction.ShowMessage(msg);
        }
        else
        {
            Common.CommonFuction.ShowMessage("LOT flag updating failed");
        }
    }
    //protected void grdLOT_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    try
    //    {
    //        if (e.Row.RowType == DataControlRowType.DataRow)
    //        {

    //            if (LOT_FLAG.Equals("1"))
    //            {
    //                Button lnkLOTEdit = (Button)e.Row.FindControl("lnkLOTEdit");
    //                Button lnkLOTDelete = (Button)e.Row.FindControl("lnkLOTDelete");

    //                lnkLOTEdit.Visible = false;
    //                lnkLOTDelete.Visible = false;

    //            }
    //            else if (LOT_FLAG.Equals("0"))
    //            {
    //                Button lnkLOTEdit = (Button)e.Row.FindControl("lnkLOTEdit");
    //                Button lnkLOTDelete = (Button)e.Row.FindControl("lnkLOTDelete");

    //                lnkLOTEdit.Visible = true;
    //                lnkLOTDelete.Visible = true;

    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid RowDataBound. See Error log for detail"));
          
    //    }
    //}
    //protected void grdLOT_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    grdLOT.EditIndex = e.NewEditIndex;
    //    BindLOTGrid();
    //}

    //protected void grdLOT_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    try
    //    {
    //        GridViewRow row = (GridViewRow)grdLOT.Rows[e.RowIndex];
    //        TextBox txtQty = (TextBox)row.FindControl("txtEditLotValue");
    //        double Qty = Convert.ToDouble(txtQty.Text);

    //        Label lblPA = (Label)row.FindControl("lblPANO");
    //        string PA = lblPA.Text;

    //        Label lblArtical_Code = (Label)row.FindControl("lblArticalCode");
    //        string Artical_code = lblArtical_Code.Text;

    //        Label lblShade_code = (Label)row.FindControl("lblShadeCode");
    //        string Shade_Code = lblShade_code.Text;

    //        DataTable dt = (DataTable)Session["dtLOT_DET"];
    //        if (dt != null && dt.Rows.Count > 0)
    //        {
    //            DataView dv = new DataView(dt);
    //            dv.RowFilter = "PA_NO= '" + PA + "' AND ARTICAL_CODE= '" + Artical_code + "' AND SHADE_CODE= '" + Shade_Code + "'";
    //            if (dv != null && dv.Count > 0)
    //            {
    //                dv[0]["LOT_QTY"] = Qty;
    //                dt.AcceptChanges();
    //                Session["dtLOT_DET"] = dt;
    //                BindLOTGrid();
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Row Updating.\r\nSee error log for detail."));

         
    //    }
    //}
    //protected void grdLOT_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    //{

    //}
    //protected void grdLOT_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    //{
    //    grdLOT.EditIndex = -1;
    //    BindLOTGrid();
    //}
}
