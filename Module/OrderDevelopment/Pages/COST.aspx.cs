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

public partial class Module_OrderDevelopment_Pages_COST : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.OD_SHADE_FAMILY oOD_SHADE_FAMILY = new SaitexDM.Common.DataModel.OD_SHADE_FAMILY();

    private DataTable dtMachinePlan = null;
    private  string TextBoxCost = string.Empty;
    private  string COMP_CODE = string.Empty;
    private  string BRANCH_CODE = string.Empty;
    private  string BUSINESS_TYPE = string.Empty;
    private  string PRODUCT_TYPE = string.Empty;
    private  string ORDER_CAT = string.Empty;
    private  string ORDER_TYPE = string.Empty;
    private  string ORDER_NO = string.Empty;
    private  string PI_TYPE = string.Empty;
    private  string PI_NO = string.Empty;
    private  string ARTICAL_CODE = string.Empty;
    private  string SHADE_CODE = string.Empty;
    private  string COST_PRICE_FLAG = string.Empty;
    private  int YEAR = 0;

    private double lblMaxQTY = 0;

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
           
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
                lblorderTYPE.Text = ORDER_TYPE;
            }
            if (Request.QueryString["ORDER_NO"] != null)
            {
                ORDER_NO = Request.QueryString["ORDER_NO"].Trim();
                lblorderno.Text = ORDER_NO;
            }
            if (Request.QueryString["PI_TYPE"] != null)
            {
                PI_TYPE = Request.QueryString["PI_TYPE"].Trim();
            }
            if (Request.QueryString["PI_NO"] != null)
            {
                PI_NO = Request.QueryString["PI_NO"].Trim();
                lblpinov.Text = PI_NO;
            }

            if (Request.QueryString["ARTICAL_CODE"] != null)
            {
                ARTICAL_CODE = Request.QueryString["ARTICAL_CODE"].Trim();
                lblARTICAL_CODE.Text = ARTICAL_CODE;
            }

            if (Request.QueryString["SHADE_CODE"] != null)
            {
                SHADE_CODE = Request.QueryString["SHADE_CODE"].ToString();
                lblSHADE_CODE.Text = SHADE_CODE;
            }
            if (Request.QueryString["YEAR"] != null)
            {
                int.TryParse(Request.QueryString["YEAR"].ToString(), out YEAR);

            }
            if (Request.QueryString["COST_PRICE_FLAG"] != null)
            {
                COST_PRICE_FLAG = Request.QueryString["COST_PRICE_FLAG"].ToString();
            }


            if (Request.QueryString["lblMaxQTY"] != null)
            {
                double qty = 0;
                if (Request.QueryString["lblMaxQTY"] != null)
                {
                    double.TryParse(Request.QueryString["lblMaxQTY"].ToString(), out qty);
                }
                lblMaxQTY = qty;
                lblRemaining.Text = lblMaxQTY.ToString();
              }


           
            if (!IsPostBack)
            {
                txtPlanningDate.Text = System.DateTime.Today.Date.ToShortDateString();
            
                if (Session["dtMachinePlan"] != null)
                {
                    if (dtMachinePlan == null)
                    {
                        CreateDataTable();

                    }
                    dtMachinePlan = (DataTable)Session["dtMachinePlan"];
                }

                else
                {
                    grdsub_trn.DataSource = null;
                    grdsub_trn.DataBind();
                    if (dtMachinePlan != null && dtMachinePlan.Rows.Count > 0)
                    {
                        dtMachinePlan.Clear();
                    }
                }
                if (COST_PRICE_FLAG.Equals("1"))
                {
                    CheckBox1.Checked = true;
                    DisableformByFlag();
                }
                else if (COST_PRICE_FLAG.Equals("0"))
                {
                    CheckBox1.Checked = false;

                }
                LOT_ID();
                BindMachineCode();
                BindUOM();
               // BindBOMGrid();
                BindMacCodeTRN();
                //else
                //{
                    SaitexDM.Common.DataModel.OD_CAPTURE_MST oOD_CAPTURE_MST = new SaitexDM.Common.DataModel.OD_CAPTURE_MST();
                    oOD_CAPTURE_MST.COMP_CODE = COMP_CODE;
                    oOD_CAPTURE_MST.BRANCH_CODE = BRANCH_CODE;
                    oOD_CAPTURE_MST.BUSINESS_TYPE = BUSINESS_TYPE;
                    oOD_CAPTURE_MST.PRODUCT_TYPE = PRODUCT_TYPE;
                    oOD_CAPTURE_MST.ORDER_CAT = ORDER_CAT;
                    oOD_CAPTURE_MST.ORDER_TYPE = ORDER_TYPE;
                    oOD_CAPTURE_MST.ORDER_NO = ORDER_NO;
                    oOD_CAPTURE_MST.YEAR = YEAR;
                    DataTable dt = SaitexDL.Interface.Method.OD_CAPTURE_MST.GetMachinePlan_ByORDER_NO(oOD_CAPTURE_MST, PI_NO, ARTICAL_CODE, SHADE_CODE, PI_TYPE);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        Session["i"] = null;
                        Session["dtMachinePlan"] = dt;
                        if (dtMachinePlan != null && dtMachinePlan.Rows.Count > 0)
                        {
                            dtMachinePlan.Clear();
                            dtMachinePlan = (DataTable)Session["dtMachinePlan"];
                        }
                       // MapTrnBOM();
                        BindBOMGrid();
                        MapDataTable_YRNSPIN_MachinePlan(dtMachinePlan);
                        FillMachinePlanByDataTable();
                    }

                }
            //}
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nsee error log for detail."));
        }
    }
    public void DisableformByFlag()
    {
        try
        {

            //txtExMill.Enabled = false;
           
            btnSubmit.Enabled = false;
            btnupdate.Enabled = false;
            CheckBox1.Enabled = false;
          
           

         

        }
        catch
        {
            throw;
        }
    }



    private void LOT_ID()
    {
        try
        {
            string LOT_NO = SaitexDL.Interface.Method.OD_CAPTURE_MST.GetBASEQUALITYLOT(ARTICAL_CODE);
            txtLotNo.Text = LOT_NO;

        }
        catch
        {
            throw;
        }
    }




    private void BindMacCodeTRN()
    {
        try
        {
            oOD_SHADE_FAMILY = new SaitexDM.Common.DataModel.OD_SHADE_FAMILY();
            oOD_SHADE_FAMILY.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oOD_SHADE_FAMILY.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oOD_SHADE_FAMILY.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            string MAC_TRNNO = SaitexBL.Interface.Method.OD_SHADE_FAMILY.GetNewMacCodeTRN(oOD_SHADE_FAMILY);
            txtTRNNo.Text = MAC_TRNNO;
        }
        catch
        {
            throw;
        }
    }
    private void MapDataTable_YRNSPIN_MachinePlan(DataTable dtTemp)
    {
        try
        {

            if (Session["dtMachinePlan"] != null)
                Session["dtMachinePlan"] = null;

            DataTable dtMachinePlan = CreateDataTable();

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    DataRow dr = dtMachinePlan.NewRow();

                   
                    dr["COMP_CODE"] = drTemp["COMP_CODE"];
                    dr["BRANCH_CODE"] = drTemp["BRANCH_CODE"];
                    dr["BUSINESS_TYPE"] = drTemp["BUSINESS_TYPE"];
                    dr["PRODUCT_TYPE"] = drTemp["PRODUCT_TYPE"];
                    dr["ORDER_CAT"] = drTemp["ORDER_CAT"];
                    dr["ORDER_TYPE"] = drTemp["ORDER_TYPE"];
                    dr["ORDER_NO"] = drTemp["ORDER_NO"];
                    dr["PI_TYPE"] = drTemp["PI_TYPE"];
                    dr["PI_NO"] = drTemp["PI_NO"];
                    dr["ARTICAL_CODE"] = drTemp["ARTICAL_CODE"];
                    dr["SHADE_CODE"] = drTemp["SHADE_CODE"];
                    dr["MACHINE_CODE"] = drTemp["MACHINE_CODE"];
                    dr["QTY"] = drTemp["QTY"];
                    dr["CONS"] = drTemp["CONS"];
                    dr["UOM"] = drTemp["UOM"];
                    dr["REMARKS"] = drTemp["REMARKS"];
                    dr["SR_NO"] = drTemp["SR_NO"];
                    dr["TRN_NUMB"] = drTemp["TRN_NUMB"];  
                    dr["YEAR"] = drTemp["YEAR"];
                    dr["GREY_LOT_NO"] = drTemp["GREY_LOT_NO"];
                    dr["PLANNING_DATE"] = drTemp["PLANNING_DATE"];

                    dtMachinePlan.Rows.Add(dr);
                }
                dtTemp = null;
                Session["dtMachinePlan"] = dtMachinePlan;
            }
        }
        catch
        {
            throw;
        }
    }
    private void FillMachinePlanByDataTable()
    {
        try
        {
            if (dtMachinePlan == null)
                CreateDataTable();

            DataView dv = new DataView(dtMachinePlan);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("COMP_CODE='" + COMP_CODE + "' AND ");
            sb.Append("BRANCH_CODE='" + BRANCH_CODE + "' AND ");
            sb.Append("BUSINESS_TYPE='" + BUSINESS_TYPE + "' AND ");
            sb.Append("PRODUCT_TYPE='" + PRODUCT_TYPE + "' AND ");
            sb.Append("ORDER_CAT='" + ORDER_CAT + "' AND ");
            sb.Append("ORDER_TYPE='" + ORDER_TYPE + "' AND ");
            sb.Append("ORDER_NO='" + ORDER_NO + "' AND ");
            sb.Append("PI_TYPE='" + PI_TYPE + "' AND ");
            sb.Append("PI_NO='" + PI_NO + "' AND ");

            sb.Append("ARTICAL_CODE='" + ARTICAL_CODE + "' AND ");
            sb.Append("SHADE_CODE='" + SHADE_CODE + "'");
            dv.RowFilter = sb.ToString();
            if (dv.Count > 0)
            {
               
                //txtFreight.Text = dv[0]["FREIGHT"].ToString();
                //txtCommission.Text = dv[0]["COMMISSION"].ToString();
                //txtBrokerage.Text = dv[0]["BROKERAGE"].ToString();
                //txtIncentives.Text = dv[0]["INCENTIVES"].ToString();
                //txtExMill.Text = dv[0]["EX_MILL_RATE"].ToString();
                //txtTotalCost.Text = dv[0]["TOTAL"].ToString();

                //txtTotalCost.ReadOnly = false;
                //txtTotalCost.Text = GetTotalCost().ToString();
                //txtTotalCost.ReadOnly = true; ;
            }
        }
        catch
        {
            throw;
        }
    }
    private DataTable  CreateDataTable()
    {
        try
        {
            dtMachinePlan = new DataTable();

            dtMachinePlan.Columns.Add("COMP_CODE", typeof(string));
            dtMachinePlan.Columns.Add("BRANCH_CODE", typeof(string));
            dtMachinePlan.Columns.Add("TRN_NUMB", typeof(int));
            dtMachinePlan.Columns.Add("YEAR", typeof(int));
            dtMachinePlan.Columns.Add("BUSINESS_TYPE", typeof(string));
            dtMachinePlan.Columns.Add("PRODUCT_TYPE", typeof(string));
            dtMachinePlan.Columns.Add("ORDER_CAT", typeof(string));
            dtMachinePlan.Columns.Add("ORDER_TYPE", typeof(string));
            dtMachinePlan.Columns.Add("ORDER_NO", typeof(string));
            dtMachinePlan.Columns.Add("PI_TYPE", typeof(string));
            dtMachinePlan.Columns.Add("PI_NO", typeof(string));
            dtMachinePlan.Columns.Add("ARTICAL_CODE", typeof(string));
            dtMachinePlan.Columns.Add("SHADE_CODE", typeof(string));
            dtMachinePlan.Columns.Add("UOM", typeof(string));
            dtMachinePlan.Columns.Add("MACHINE_CODE", typeof(string));
            dtMachinePlan.Columns.Add("SR_NO", typeof(int));
            dtMachinePlan.Columns.Add("GREY_LOT_NO", typeof(string));
            dtMachinePlan.Columns.Add("QTY", typeof(double));
            dtMachinePlan.Columns.Add("CONS", typeof(double));
            dtMachinePlan.Columns.Add("REMARKS", typeof(string));
            dtMachinePlan.Columns.Add("TUSER", typeof(string));
            dtMachinePlan.Columns.Add("PLANNING_DATE", typeof(string));
            return dtMachinePlan;
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
            //GetMachineDataTable();
            //Session["dtMachinePlan"] = dtMachinePlan;

            dtMachinePlan = Session["dtMachinePlan"] as DataTable;
            string msg = string.Empty;
            bool Result = SaitexDL.Interface.Method.OD_CAPTURE_MST.Delete_TRN_MachinePlan(dtMachinePlan);
            if (Result)
            {
                msg += "Machine Plan Saved successfully.";
                Common.CommonFuction.ShowMessage(msg);
                if (dtMachinePlan != null)
                {
                    dtMachinePlan.Dispose();
                    dtMachinePlan = null;
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Machine Plan Data Saving Failed");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in submitting Machine Plan data.\r\nsee error log for detail."));
        }
    }
    private void GetMachineDataTable()
    {
        try
        {
            if (dtMachinePlan == null)
                CreateDataTable();

            if (dtMachinePlan.Rows.Count > 0)
            {
                DataView dv = new DataView(dtMachinePlan);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("COMP_CODE='" + COMP_CODE + "' AND ");
                sb.Append("BRANCH_CODE='" + BRANCH_CODE + "' AND ");
                sb.Append("BUSINESS_TYPE='" + BUSINESS_TYPE + "' AND ");
                sb.Append("PRODUCT_TYPE='" + PRODUCT_TYPE + "' AND ");
                sb.Append("ORDER_CAT='" + ORDER_CAT + "' AND ");
                sb.Append("ORDER_TYPE='" + ORDER_TYPE + "' AND ");
                sb.Append("ORDER_NO='" + ORDER_NO + "' AND ");
                sb.Append("PI_TYPE='" + PI_TYPE + "' AND ");
                sb.Append("PI_NO='" + PI_NO + "' AND ");
                sb.Append("ARTICAL_CODE='" + ARTICAL_CODE + "' AND ");
                sb.Append("SHADE_CODE='" + SHADE_CODE + "'");
                dv.RowFilter = sb.ToString();
                if (dv.Count > 0)
                {
                    dv[0]["MACHINE_CODE"] = ddlMachine.SelectedText;
                    dv[0]["QTY"] = double.Parse(txtQty.Text.Trim());
                    dv[0]["CONS"] = double.Parse(txtCons.Text.Trim());
                    dv[0]["UOM"] = "KGS";
                    dv[0]["REMARKS"] = txtRemarks.Text.Trim();
                    dv[0]["TRN_NUMB"] = txtTRNNo.Text.Trim();
                    dv[0]["GREY_LOT_NO"] = txtLotNo.Text.Trim();
                    dv[0]["TRN_NUMB"] = txtTRNNo.Text.Trim();
                    
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
                    dv[0]["TUSER"] = oUserLoginDetail.UserCode;
                    dv[0]["YEAR"] = YEAR;

                    dtMachinePlan.AcceptChanges();
                }
                else
                {
                    AddNewRowToDatatable();
                }
            }
            else
            {
                AddNewRowToDatatable();
            }
        }
        catch
        {
            throw;
        }
    }
    private void AddNewRowToDatatable()
    {
        try
        {
            DataRow dr = dtMachinePlan.NewRow();

            dr["SR_NO"] = dtMachinePlan.Rows.Count + 1;
            dr["ARTICAL_CODE"] = ARTICAL_CODE;
            dr["PI_TYPE"] = PI_TYPE;
            dr["MACHINE_CODE"] = ddlMachine.SelectedText;
            dr["QTY"] = double.Parse(txtQty.Text.Trim());
            dr["CONS"] = double.Parse(txtCons.Text.Trim());
            dr["UOM"] = "KGS";
            dr["REMARKS"] = txtRemarks.Text.Trim();
            dr["GREY_LOT_NO"] = txtLotNo.Text.Trim();
            dr["TRN_NUMB"] = txtTRNNo.Text.Trim();
            
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
            dr["TUSER"] = oUserLoginDetail.UserCode;
            dr["YEAR"] = YEAR;


            dtMachinePlan.Rows.Add(dr);
        }
        catch
        {
            throw;
        }
    }
  
    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            double totalQty = 0;
            for (int i = 0; i < grdsub_trn.Rows.Count; i++)
            {
                Label txtQTY = grdsub_trn.Rows[i].FindControl("txtQTY") as Label;
                totalQty += double.Parse(txtQTY.Text);
            }
         
             //if (lblMaxQTY >= totalQty)
            //{
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:BindYRNSPIN_COST()", true);
            //}
            // else
            // {
            //     Common.CommonFuction.ShowMessage(" Please enter the rest " + (lblMaxQTY - totalQty) + " Quantity");

            // }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in submitting Data data.\r\nsee error log for detail."));
        }

    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        SaitexDM.Common.DataModel.OD_CAPTURE_MST oOD_CAPTURE_MST = new SaitexDM.Common.DataModel.OD_CAPTURE_MST();
        oOD_CAPTURE_MST.COMP_CODE = COMP_CODE;
        oOD_CAPTURE_MST.BRANCH_CODE = BRANCH_CODE;
        oOD_CAPTURE_MST.BUSINESS_TYPE = BUSINESS_TYPE;
        oOD_CAPTURE_MST.PRODUCT_TYPE = PRODUCT_TYPE;
        oOD_CAPTURE_MST.ORDER_CAT = ORDER_CAT;
        oOD_CAPTURE_MST.ORDER_TYPE = ORDER_TYPE;
        oOD_CAPTURE_MST.ORDER_NO = ORDER_NO;
        oOD_CAPTURE_MST.YEAR = YEAR;
        bool status = false;
        if (CheckBox1.Checked)
        {
            status = true;


        }
        else
        {
            status = false;
        }

        bool Result = SaitexDL.Interface.Method.OD_CAPTURE_MST.UpdateCostflag (status, oOD_CAPTURE_MST, PI_NO, ARTICAL_CODE, SHADE_CODE, PI_TYPE);
        if (Result)
        {
            msg += "Machine Plan  Flag Updated";
            Common.CommonFuction.ShowMessage(msg);

        }
        else
        {
            Common.CommonFuction.ShowMessage("Machine Plan Flag Updating Failed");
        }
    }

    protected void BtnBOMSave_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtMachinePlan = new DataTable();
            if (Session["dtMachinePlan"] != null)
            {
                dtMachinePlan = (DataTable)Session["dtMachinePlan"];
            }
            else
            {
                dtMachinePlan = CreateDataTable();
              //CreateDataTable();
            }
            if (CheckQTYtotal())
            {
                if ( !(double.Parse(ddlMachine.SelectedValue) < double.Parse(txtQty.Text)) && ddlMachine.SelectedText != "" && txtQty.Text != "" && txtPlanningDate.Text != "" && txtPlanningDate.Text != System.DateTime.Today.ToShortDateString())
                {

                    int SR_NO = 0;
                    if (ViewState["SR_NO"] != null)
                    {
                        SR_NO = int.Parse(ViewState["SR_NO"].ToString());
                     }
                    bool bb = SearchItemCodeInGrid(txtTRNNo.Text, SR_NO);
                    if (!bb)
                    {
                        if (SR_NO > 0)
                        {
                            DataView dv = new DataView(dtMachinePlan);
                            dv.RowFilter = "ORDER_NO='" + lblorderno.Text + "' and ARTICAL_CODE='" + lblARTICAL_CODE.Text + "' and SHADE_CODE='" + lblSHADE_CODE.Text + "'and PI_NO='" + lblpinov.Text + "' and SR_NO=" + SR_NO;

                            System.Text.StringBuilder sb = new System.Text.StringBuilder();

                            sb.Append("COMP_CODE='" + COMP_CODE + "' AND ");
                            sb.Append("BRANCH_CODE='" + BRANCH_CODE + "' AND ");
                            sb.Append("YEAR='" + YEAR + "' AND ");
                            sb.Append("BUSINESS_TYPE='" + BUSINESS_TYPE + "' AND ");
                            sb.Append("PRODUCT_TYPE='" + PRODUCT_TYPE + "' AND ");
                            sb.Append("ORDER_CAT='" + ORDER_CAT + "' AND ");
                            sb.Append("ORDER_TYPE='" + ORDER_TYPE + "' AND ");
                            sb.Append("ORDER_NO='" + ORDER_NO + "' AND ");
                            sb.Append("PI_TYPE='" + PI_TYPE + "' AND ");
                            sb.Append("PI_NO='" + PI_NO + "' AND ");
                            sb.Append("ARTICAL_CODE='" + ARTICAL_CODE + "' AND ");
                            sb.Append("SHADE_CODE='" + SHADE_CODE + "' AND ");
                            sb.Append("SR_NO='" + SR_NO + "'");

                            dv.RowFilter = sb.ToString();
                            if (dv.Count > 0)
                            {
                                double QTY = 0f;
                                double.TryParse(txtQty.Text.Trim(), out QTY);
                                dv[0]["QTY"] = QTY;

                                dv[0]["MACHINE_CODE"] = ddlMachine.SelectedText.Trim();
                                double CONS = 0;
                                double.TryParse(txtCons.Text.Trim(), out CONS);
                                dv[0]["CONS"] = CONS;
                                dv[0]["UOM"] = "KGS";
                                dv[0]["REMARKS"] = txtRemarks.Text.Trim();
                                dv[0]["TRN_NUMB"] = txtTRNNo.Text.Trim();
                                dv[0]["GREY_LOT_NO"] = txtLotNo.Text.Trim();
                                dv[0]["PLANNING_DATE"] = txtPlanningDate.Text.Trim();
                                dtMachinePlan.AcceptChanges();
                            }
                        }
                        else
                        {
                            DataRow dr = dtMachinePlan.NewRow();
                            dr["SR_NO"] = dtMachinePlan.Rows.Count + 1;

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
                            dr["YEAR"] = YEAR;
                            double QTY = 0f;
                            double.TryParse(txtQty.Text.Trim(), out QTY);
                            dr["QTY"] = QTY;

                            dr["MACHINE_CODE"] = ddlMachine.SelectedText.Trim();
                            double CONS = 0;
                            double.TryParse(txtCons.Text.Trim(), out CONS);
                            dr["CONS"] = CONS;
                            dr["UOM"] = "KGS";
                            dr["REMARKS"] = txtRemarks.Text.Trim();
                            dr["TRN_NUMB"] = txtTRNNo.Text.Trim();
                            dr["GREY_LOT_NO"] = txtLotNo.Text.ToString();
                            dr["TUSER"] = oUserLoginDetail.UserCode;
                            dr["PLANNING_DATE"] = txtPlanningDate.Text.ToString();
                            dtMachinePlan.Rows.Add(dr);
                        }
                      
                        RefresBOMRow();
                    }
                   else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Please Enter Another MAchine No.This Already Added ');", true);
                    }
                    Session["dtMachinePlan"] = dtMachinePlan;
                     BindBOMGrid();
                }
                else if ( (double.Parse(ddlMachine.SelectedValue) < double.Parse(txtQty.Text)))
                {

                    Common.CommonFuction.ShowMessage("Qty Should not be more than Machine Capacity");
                }
                else if (txtPlanningDate.Text == System.DateTime.Today.ToShortDateString())
                {
                    Common.CommonFuction.ShowMessage("Select Another Date");
                }
                else if (ddlMachine.SelectedIndex == -1)
                {
                    Common.CommonFuction.ShowMessage("Machine Code Required");
                }
                else if (txtQty.Text == "")
                {
                    Common.CommonFuction.ShowMessage("Quantity can not be zero");
                }

                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Quantity can not be greater than Maximum Quantity!!! ');", true);
                }
            }
            //else
            //{
            //    Common.CommonFuction.ShowMessage("You have reached the limit of Machine. Only 12 Standard allowed in one Machine Plan Master.");
            //}
        }

        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving Machine Plan Date Detail Row.\r\nSee error log for detail."));

        }
    }

    private bool SearchItemCodeInGrid(string TRNNUM, int SR_NO)
    {
        bool Result = false;
        try
        {

            if (grdsub_trn.Rows.Count > 0)
            {
                foreach (GridViewRow grdRow in grdsub_trn.Rows)
                {
                    Label lblTRNNo = (Label)grdRow.FindControl("lblTRNNo");

                    Button lnkBOMEdit = (Button)grdRow.FindControl("lnkBOMEdit");
                    int iSR_NO = int.Parse(lnkBOMEdit.CommandArgument.Trim());

                    if (lblTRNNo.Text.Trim() == TRNNUM && SR_NO != iSR_NO)
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
    //private bool SearchItemCodeInGrid(string TRNNUM, int SR_NO)
    //{
    //    bool Result = false;
    //    try
    //    {
    //        foreach (GridViewRow grdRow in grdsub_trn.Rows)
    //        {
    //            Label lblTRNNo = (Label)grdRow.FindControl("lblTRNNo");
    //            Button lnkDelete = (Button)grdRow.FindControl("lnkBOMDelete");
    //            int iSR_NO = int.Parse(lnkDelete.CommandArgument.Trim());
    //            if (lblTRNNo.Text == TRNNUM && SR_NO != iSR_NO)
    //                Result = true;
    //        }


            
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}
    private void RefresBOMRow()
    {
        try
        {
            txtQty.Text = string.Empty;
            ddlMachine.SelectedIndex = -1;
            txtCons.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            LOT_ID();
            //txtLotNo.Text = string.Empty;
            ViewState["SR_NO"] = null;
            txtPlanningDate.Text = System.DateTime.Today.ToShortDateString();
        }
        catch
        {
            throw;
        }
    }


    protected void BtnBOMCancel_Click(object sender, EventArgs e)
    {
        RefresBOMRow();
    }

    protected bool CheckQTYtotal()
    {
        try
        {

            double currentpage = 0;
            //double currentQty = 0;
            double Qty = 0;
            double.TryParse(txtQty.Text, out Qty);
            if (grdsub_trn.Rows.Count > 0)
            {
                for (int i = 0; i < grdsub_trn.Rows.Count; i++)
                {
                    Label txtQTY = grdsub_trn.Rows[i].FindControl("txtQTY") as Label;
                    currentpage += double.Parse(txtQTY.Text);
                }
                 double alltotal = currentpage + double.Parse(txtQty.Text);
                 if (alltotal > lblMaxQTY)
                 {
                     return false;

                 }
                 else
                 {
                     return true;
                 }

            }
            else if (double.Parse(txtQty.Text) > (lblMaxQTY + lblMaxQTY * 4 / 100))
            {
                return false;
            }
            else
            {
                return true;

            }


        }
        catch
        {
            throw;

        }


    }


    protected void grdSub_trnArticleDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //int SR_NO = int.Parse(e.CommandArgument.ToString());
            //if (e.CommandName == "BOMDelete")
            //{
            //    DeleteBOMRow(SR_NO);
            //    BindBOMGrid();
            //}


            int SR_NO = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "BOMEdit")
            {
                //FillBOMByGrid(SR_NO);
            }
            else if (e.CommandName == "BOMDelete")
            {
                DeleteBOMRow(SR_NO);
                BindBOMGrid();
            }

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Sub Tran Grid RowCommand Selection.\r\nSee error log for detail."));
        }
    }

    private void BindBOMGrid()
    {
        try
        {
            DataTable dtMachinePlan = null;
            if (Session["dtMachinePlan"] != null)
            {
                dtMachinePlan = (DataTable)Session["dtMachinePlan"];
            }
            else
            {
                dtMachinePlan = CreateDataTable();
            }

            DataView dv = new DataView(dtMachinePlan);
            dv.RowFilter = " ORDER_NO='" + lblorderno.Text + "' and ARTICAL_CODE='" + ARTICAL_CODE + "' and SHADE_CODE='" + lblSHADE_CODE.Text + "'and PI_NO='" + lblpinov.Text + "' ";
            grdsub_trn.DataSource = dv;
            grdsub_trn.DataBind();
            clearInitial();
            Label lblTRNNo = grdsub_trn.Rows[grdsub_trn.Rows.Count - 1].FindControl("lblTRNNo") as Label;
            Int64 TRN_NUMB = 0;
            Int64.TryParse(lblTRNNo.Text, out TRN_NUMB);
            txtTRNNo.Text = (TRN_NUMB + 1).ToString();

        }


        catch
        {
            throw;
        }
    }
    public void clearInitial()
    {
        txtTRNNo.Text = string.Empty;
        txtQty.Text = string.Empty;
        txtCons.Text = string.Empty;
    }

    protected void ddlMacCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        BindMachineCode();
    }
    private void BindMachineCode()
    {

        try
        {
            DataTable dt = SaitexBL.Interface.Method.OD_SHADE_FAMILY.GetMachineCode(oOD_SHADE_FAMILY);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlMachine.Items.Clear();
                ddlMachine.DataSource = dt;
                ddlMachine.DataTextField = "MACHINE_CODE";
                ddlMachine.DataValueField = "MACHINE_CAPACITY";
                ddlMachine.DataBind();
                //ddlMachine.Items.Insert(0, new ListItem("------SELECT----", "0"));
            }
        }
        catch
        {
            throw;

        }
    }


    private void BindUOM()
    {

        try
        {
            //DataTable dt = SaitexBL.Interface.Method.OD_SHADE_FAMILY.GetMachineCode(oOD_SHADE_FAMILY);
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    ddlMachine.Items.Clear();
            //    ddlMachine.DataSource = dt;
            //    ddlMachine.DataTextField = "MACHINE_CODE";
            //    ddlMachine.DataValueField = "MACHINE_CODE";
            //    ddlMachine.DataBind();
            ddUOM.Items.Insert(0, new ListItem("KGS", "0"));
            //}
        }
        catch
        {
            throw;

        }
    }
    private void DeleteBOMRow(int SR_NO)
    {
        try
        {
            DataTable dtMachinePlan = null;
            if (Session["dtMachinePlan"] != null)
            {
                dtMachinePlan = (DataTable)Session["dtMachinePlan"];
            }
            else
            {
                dtMachinePlan = CreateDataTable();
            }
            if (dtMachinePlan.Rows.Count == 1)
            {
                dtMachinePlan.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtMachinePlan.Rows)
                {
                    int iUNIQUE_ID = int.Parse(dr["SR_NO"].ToString());
                    if (iUNIQUE_ID == SR_NO)
                    {
                        dtMachinePlan.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtMachinePlan.Rows)
                {
                    iCount = iCount + 1;
                    dr["SR_NO"] = iCount;
                }
            }
            dtMachinePlan.AcceptChanges();
            Session["dtMachinePlan"] = dtMachinePlan;
        }
        catch
        {
            throw;
        }
    }
}
