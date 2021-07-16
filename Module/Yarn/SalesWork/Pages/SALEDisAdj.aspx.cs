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

public partial class Module_Yarn_SalesWork_Pages_SALEDisAdj : System.Web.UI.Page
{
    private DataTable dtDicRate = null;
    private DataTable dtdRateComponent = null;
    private double FinalAmount = 0;
    private double StartFinalAmount = 0;
    private string TextBoxId = string.Empty;
    private string COMP_CODE = string.Empty;
    private string BRANCH_CODE = string.Empty;
    private string PO_TYPE = string.Empty;
    private string PA_NO = string.Empty;
    private string YARN_CODE = string.Empty;
    private string SHADE_CODE = string.Empty;
    private int YEAR = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["PO_YEAR"] != null)
            {
                int.TryParse(Request.QueryString["YEAR"].Trim().ToString(), out YEAR);
            }
            if (!IsPostBack)
            {

                if (Request.QueryString["TextBoxId"] != null)
                {
                    TextBoxId = Request.QueryString["TextBoxId"].Trim();
                    ViewState["TextBoxId"] = TextBoxId;
                }
                if (Request.QueryString["FinalAmount"] != null)
                {
                    StartFinalAmount = double.Parse(Request.QueryString["FinalAmount"].Trim());
                    lblBasicRate.Text = Request.QueryString["FinalAmount"].Trim();
                    ViewState["StartFinalAmount"] = StartFinalAmount;
                }
                if (Request.QueryString["COMP_CODE"] != null)
                {
                    COMP_CODE = Request.QueryString["COMP_CODE"].ToString();
                    ViewState["COMP_CODE"] = COMP_CODE;
                }
                if (Request.QueryString["BRANCH_CODE"] != null)
                {
                    BRANCH_CODE = Request.QueryString["BRANCH_CODE"].ToString();
                    ViewState["BRANCH_CODE"] = BRANCH_CODE;
                }
                if (Request.QueryString["PO_TYPE"] != null)
                {
                    PO_TYPE = Request.QueryString["PO_TYPE"].ToString();
                    ViewState["PO_TYPE"] = PO_TYPE;
                }
                if (Request.QueryString["PA_NO"] != null)
                {
                    PA_NO = Request.QueryString["PA_NO"].ToString();
                    ViewState["PA_NO"] = PA_NO;
                }
                if (Request.QueryString["YARN_CODE"] != null)
                {
                    YARN_CODE = Request.QueryString["YARN_CODE"].ToString();
                    Label1.Text = YARN_CODE;
                    ViewState["YARN_CODE"] = YARN_CODE;
                }
                if (Request.QueryString["SHADE_CODE"] != null)
                {
                    SHADE_CODE = Request.QueryString["SHADE_CODE"].ToString();
                    ViewState["SHADE_CODE"] = SHADE_CODE;
                    lblshade.Text = SHADE_CODE;
                }

                if (Session["dtDicRate"] != null)
                {
                    if (dtDicRate == null)
                        CreateDataTable();
                    dtDicRate = (DataTable)Session["dtDicRate"];

                    if (!dtDicRate.Columns.Contains("Amount"))
                    {
                        dtDicRate.Columns.Add("Amount", typeof(double));
                    }

                    if (ViewState["StartFinalAmount"] != null)
                    {
                        StartFinalAmount = (Double)ViewState["StartFinalAmount"];
                    }

                    double dFinalRate = Math.Round(StartFinalAmount, 4);
                    foreach (DataRow dr in dtDicRate.Rows)
                    {
                        string syarnCode = dr["YARN_CODE"].ToString();
                        string sShadecode = dr["SHADE_CODE"].ToString();
                       
                        if ( SHADE_CODE.Equals(sShadecode, StringComparison.OrdinalIgnoreCase) && YARN_CODE.Equals(syarnCode, StringComparison.OrdinalIgnoreCase))
                        {
                            double dAmount = 0;

                            double cAmount = 0;
                            double rate = Math.Round(double.Parse(dr["Rate"].ToString()), 4);
                            if (dr["BASE_COMPO_CODE"].ToString().Equals("Basic Rate"))
                            {
                                cAmount = (StartFinalAmount * rate) / 100;
                            }
                            else if (dr["BASE_COMPO_CODE"].ToString().Equals("Final Rate"))
                            {
                                cAmount = (dFinalRate * rate) / 100;
                            }
                            else if (dr["BASE_COMPO_CODE"].ToString().Equals("Flat Amount"))
                            {
                                cAmount = rate;
                            }
                            else
                            {
                                DataView dvv = new DataView(dtDicRate);
                                dvv.RowFilter = "COMPO_CODE='" + dr["BASE_COMPO_CODE"].ToString() + "'";

                                if (dvv.Count > 0)
                                {
                                    double.TryParse(dvv[0]["Amount"].ToString(), out dAmount);
                                }
                                cAmount = (dAmount * rate) / 100;
                            }

                            if (dr["COMPO_TYPE"].ToString().Equals("D"))
                            {
                                dFinalRate = dFinalRate - cAmount;
                            }
                            else
                            {
                                dFinalRate = dFinalRate + cAmount;
                            }
                            dr["Amount"] = Math.Round(cAmount, 4);

                        }
                    }
                    ViewState["dtDicRate"] = dtDicRate;


                    fillGridByDataTable();

                }
                FinalAmount = Math.Round(StartFinalAmount, 4);
                ViewState["FinalAmount"] = Math.Round(FinalAmount, 4);
                GetRateComponent();
                BindRateComponentGrid();
                CalculateFinalAmount();
                txtFinalAmount.Text = Math.Round(FinalAmount, 4).ToString();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Disablewinjs", "javascript:DisableTextObj('" + TextBoxId + "')", true);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading. See error log for detail"));
        }

    }

    private void GetRateComponent()
    {
        try
        {
            dtdRateComponent = SaitexBL.Interface.Method.TX_RATE_COMPONENT.GetRateComponent();

            ViewState["dtdRateComponent"] = dtdRateComponent;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindRateComponentGrid()
    {
        try
        {
            if (ViewState["dtdRateComponent"] != null)
                dtdRateComponent = (DataTable)ViewState["dtdRateComponent"];
            ddlRateComponent.Items.Clear();
            ddlRateComponent.DataSource = null;
            ddlRateComponent.DataBind();

            ddlRateComponent.Items.Add(new ListItem("Select", ""));

            ddlRateComponent.DataSource = dtdRateComponent;
            ddlRateComponent.DataTextField = "COMPO_VALUE";
            ddlRateComponent.DataValueField = "COMPO_CODE";
            ddlRateComponent.DataBind();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void CreateDataTable()
    {
        try
        {
            dtDicRate = new DataTable();
            dtDicRate.Columns.Add("Uniqueid", typeof(int));
            dtDicRate.Columns.Add("COMP_CODE", typeof(string));
            dtDicRate.Columns.Add("BRANCH_CODE", typeof(string));
            dtDicRate.Columns.Add("PO_TYPE", typeof(string));
            dtDicRate.Columns.Add("PA_NO", typeof(string));
            dtDicRate.Columns.Add("YEAR", typeof(int));
            dtDicRate.Columns.Add("YARN_CODE", typeof(string));

            dtDicRate.Columns.Add("COMPO_CODE", typeof(string));
            dtDicRate.Columns.Add("Rate", typeof(double));
            dtDicRate.Columns.Add("COMPO_SL", typeof(int));
            dtDicRate.Columns.Add("COMPO_TYPE", typeof(string));
            dtDicRate.Columns.Add("Amount", typeof(double));
            dtDicRate.Columns.Add("BASE_COMPO_CODE", typeof(string));
            dtDicRate.Columns.Add("IS_PO", typeof(string));
            dtDicRate.Columns.Add("SHADE_CODE", typeof(string));
            ViewState["dtDicRate"] = dtDicRate;
            // Session["dtDicRate"] = dtDicRate;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void FillDataTableByGrid()
    {
        try
        {
            if (ViewState["COMP_CODE"] != null)
            {
                COMP_CODE = (string)ViewState["COMP_CODE"];
            }
            if (ViewState["BRANCH_CODE"] != null)
            {
                BRANCH_CODE = (string)ViewState["BRANCH_CODE"];
            }
            if (ViewState["PO_TYPE"] != null)
            {
                PO_TYPE = (string)ViewState["PO_TYPE"];
            }
            if (ViewState["PA_NO"] != null)
            {
                PA_NO = (string)ViewState["PA_NO"];
            }

            if (ViewState["YARN_CODE"] != null)
                YARN_CODE = (string)ViewState["YARN_CODE"];
            if (ViewState["dtDicRate"] != null)
                dtDicRate = (DataTable)ViewState["dtDicRate"];
            foreach (GridViewRow Row in grdRate.Rows)
            {
                Label lblComponentName = (Label)Row.FindControl("lblComponentName");
                string Code = lblComponentName.Text.Trim();
                foreach (DataRow dr in dtDicRate.Rows)
                {
                    string sCode = dr["COMPO_CODE"].ToString();
                    string sITemCode = dr["YARN_CODE"].ToString();
                    if (sCode == Code && sITemCode == YARN_CODE)
                    {
                        TextBox txtRate = (TextBox)Row.FindControl("txtRate");
                        dr["Rate"] = double.Parse(txtRate.Text.Trim());
                    }
                }

                ViewState["dtDicRate"] = dtDicRate;

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void AddRowInDataTable()
    {
        try
        {

            if (ViewState["dtDicRate"] != null)
            {
                dtDicRate = (DataTable)ViewState["dtDicRate"];
            }

            if (ViewState["COMP_CODE"] != null)
            {
                COMP_CODE = (string)ViewState["COMP_CODE"];
            }
            if (ViewState["BRANCH_CODE"] != null)
            {
                BRANCH_CODE = (string)ViewState["BRANCH_CODE"];
            }
            if (ViewState["PO_TYPE"] != null)
            {
                PO_TYPE = (string)ViewState["PO_TYPE"];
            }
            if (ViewState["PA_NO"] != null)
            {
                PA_NO = (string)ViewState["PA_NO"];
            }

            if (ViewState["YARN_CODE"] != null)
            {
                YARN_CODE = (string)ViewState["YARN_CODE"];
            }
            if (ViewState["SHADE_CODE"] != null)
            {
                SHADE_CODE = (string)ViewState["SHADE_CODE"];
            }
            if (ViewState["FinalAmount"] != null)
            {
                FinalAmount = (Double)ViewState["FinalAmount"];
            }
            if (ViewState["StartFinalAmount"] != null)
            {
                StartFinalAmount = (Double)ViewState["StartFinalAmount"];
            }
            int Uniqueid = 0;
            if (ViewState["Uniqueid"] != null)
            {
                int.TryParse(ViewState["Uniqueid"].ToString(), out Uniqueid);
            }

            string Code = string.Empty;
            Code = ddlRateComponent.SelectedValue.Trim();

            if (dtDicRate == null)
                CreateDataTable();

            bool IsDuplicate = false;
            if (dtDicRate.Rows.Count > 0)
            {
                DataView dvRate = new DataView(dtDicRate);
                dvRate.RowFilter = "COMP_CODE='" + COMP_CODE + "' AND BRANCH_CODE='" + BRANCH_CODE + "' AND PO_TYPE='" + PO_TYPE + "' AND PA_NO='" + PA_NO + "' AND YARN_CODE='" + YARN_CODE + "' and SHADE_CODE='" + SHADE_CODE + "'  and COMPO_CODE='" + Code + "' AND YEAR=" + YEAR + " and Uniqueid <> " + Uniqueid;

                if (dvRate.Count > 0)
                {
                    IsDuplicate = true;
                }
            }
            if (!IsDuplicate)
            {
                if (ViewState["dtdRateComponent"] != null)
                {
                    dtdRateComponent = (DataTable)ViewState["dtdRateComponent"];
                }
                DataView dvRateComponent = new DataView(dtdRateComponent);
                dvRateComponent.RowFilter = "COMPO_CODE='" + Code + "'";
                if (dvRateComponent.Count > 0)
                {
                    double rate = 0;
                    double.TryParse(txtRate.Text.Trim(), out rate);
                    if (rate > 0)
                    {
                        double dAmount = 0;

                        double cAmount = 0;
                        if (ddlBaseComponent.SelectedValue.Equals("Basic Rate"))
                        {
                            cAmount = (StartFinalAmount * rate) / 100;
                        }
                        else if (ddlBaseComponent.SelectedValue.Equals("Final Rate"))
                        {
                            cAmount = (FinalAmount * rate) / 100;
                        }
                        else if (ddlBaseComponent.SelectedValue.Equals("Flat Amount"))
                        {
                            cAmount = rate;
                        }
                        else
                        {
                            DataView dvv = new DataView(dtDicRate);
                            dvv.RowFilter = "COMP_CODE='" + COMP_CODE + "' AND BRANCH_CODE='" + BRANCH_CODE + "' AND PO_TYPE='" + PO_TYPE + "' AND PA_NO='" + PA_NO + "' AND YARN_CODE='" + YARN_CODE + "'AND SHADE_CODE='" + SHADE_CODE + "'  and COMPO_CODE='" + ddlBaseComponent.SelectedItem.Text.Trim() + "' AND YEAR=" + YEAR;

                            if (dvv.Count > 0)
                            {
                                dAmount = double.Parse(dvv[0]["Amount"].ToString());
                            }
                            cAmount = (dAmount * rate) / 100;
                        }

                        if (Uniqueid > 0)
                        {
                            DataView dvUpdate = new DataView(dtDicRate);
                            dvUpdate.RowFilter = "Uniqueid = " + Uniqueid;
                            if (dvUpdate.Count > 0)
                            {
                                dvUpdate[0]["COMPO_CODE"] = Code;
                                dvUpdate[0]["Rate"] = Math.Round(rate, 4);
                                dvUpdate[0]["COMPO_SL"] = int.Parse(dvRateComponent[0]["COMPO_SL"].ToString());
                                dvUpdate[0]["COMPO_TYPE"] = dvRateComponent[0]["COMPO_TYPE"].ToString();
                                dvUpdate[0]["Amount"] = Math.Round(cAmount, 4);
                                dvUpdate[0]["IS_PO"] = "0";
                                dvUpdate[0]["BASE_COMPO_CODE"] = ddlBaseComponent.SelectedItem.Text.Trim();
                                dvUpdate[0]["SHADE_CODE"] = SHADE_CODE;
                                dvUpdate[0]["COMP_CODE"] = COMP_CODE;
                                dvUpdate[0]["BRANCH_CODE"] = BRANCH_CODE;
                                dvUpdate[0]["PO_TYPE"] = PO_TYPE;
                                dvUpdate[0]["PA_NO"] = PA_NO;
                                dvUpdate[0]["YEAR"] = YEAR;
                                dtDicRate.AcceptChanges();
                            }
                        }
                        else
                        {
                            if (!dtDicRate.Columns.Contains("YEAR"))
                            {
                                dtDicRate.Columns.Add("YEAR", typeof(int));
                            }
                            DataRow dr = dtDicRate.NewRow();

                            dr["Uniqueid"] = dtDicRate.Rows.Count + 1;
                            dr["COMP_CODE"] = COMP_CODE;
                            dr["BRANCH"] = BRANCH_CODE;
                            dr["PO_TYPE"] = PO_TYPE;
                            dr["PA_NO"] = PA_NO;
                            dr["YEAR"] = YEAR;
                            dr["YARN_CODE"] = YARN_CODE;
                            dr["COMPO_CODE"] = Code;
                            dr["Rate"] = Math.Round(rate, 4);
                            dr["COMPO_SL"] = int.Parse(dvRateComponent[0]["COMPO_SL"].ToString());
                            dr["COMPO_TYPE"] = dvRateComponent[0]["COMPO_TYPE"].ToString();

                            dr["Amount"] = Math.Round(cAmount, 4);
                            dr["IS_PO"] = "0";
                            dr["BASE_COMPO_CODE"] = ddlBaseComponent.SelectedItem.Text.Trim();
                            dr["SHADE_CODE"] = SHADE_CODE;
                            dtDicRate.Rows.Add(dr);
                        }
                        ddlRateComponent.SelectedIndex = 0;
                        ddlRateComponent.SelectedValue = "";
                        txtRate.Text = string.Empty;
                        ViewState["dtDicRate"] = dtDicRate;
                    }

                    else
                    {
                        Common.CommonFuction.ShowMessage("Enter correct rate");
                        txtRate.Text = string.Empty;
                        txtRate.Focus();
                    }
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("This Component already added");

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alret('This Component already added')", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void RemoveRowFromDataTable(int UniqueId, string compo_Code)
    {
        try
        {
            if (ViewState["dtDicRate"] != null)
            {
                dtDicRate = (DataTable)ViewState["dtDicRate"];
            }
            if (dtDicRate.Rows.Count == 1)
            {
                dtDicRate.Rows.Clear();
            }

            DataView DV = new DataView(dtDicRate);
            DV.RowFilter = "BASE_COMPO_CODE='" + compo_Code + "'";

            if (DV.Count == 0)
            {
                foreach (DataRow dr in dtDicRate.Rows)
                {
                    int iUniqueId = int.Parse(dr["Uniqueid"].ToString());
                    if (iUniqueId == UniqueId)
                    {
                        dtDicRate.Rows.Remove(dr);
                        break;
                    }
                }
                dtDicRate.AcceptChanges();
                ViewState["dtDicRate"] = dtDicRate;
            }
            else
            {
                Common.CommonFuction.ShowMessage("Child record exists. Please remove all child first.");
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void fillGridByDataTable()
    {
        try
        {
            if (ViewState["COMP_CODE"] != null)
            {
               COMP_CODE = (string)ViewState["COMP_CODE"];
            }
            if (ViewState["BRANCH_CODE"] != null)
            {
                BRANCH_CODE = (string)ViewState["BRANCH_CODE"];
            }
            if (ViewState["PO_TYPE"] != null)
            {
                PO_TYPE = (string)ViewState["PO_TYPE"];
            }
            if (ViewState["PA_NO"] != null)
            {
                PA_NO = (string)ViewState["PA_NO"];
            }

            if (ViewState["YARN_CODE"] != null)
            {
                YARN_CODE = (string)ViewState["YARN_CODE"];
            }
            if (ViewState["SHADE_CODE"] != null)
            {
                SHADE_CODE = (string)ViewState["SHADE_CODE"];
            }
            if (ViewState["dtDicRate"] != null)
            {
                dtDicRate = (DataTable)ViewState["dtDicRate"];
            }
            grdRate.DataSource = null;
            grdRate.DataBind();
            DataView dv = new DataView(dtDicRate);
            dv.RowFilter = "COMP_CODE='" + COMP_CODE + "' AND BRANCH_CODE='" + BRANCH_CODE + "' AND PO_TYPE='" + PO_TYPE + "' AND PA_NO='" + PA_NO + "' AND YARN_CODE='" + YARN_CODE + "' AND SHADE_CODE='" + SHADE_CODE  + "'";
            if (dv.Count > 0)
            {
                grdRate.DataSource = dv;
                grdRate.DataBind();

                ddlBaseComponent.Items.Clear();
                ddlBaseComponent.Items.Add(new ListItem("Basic Rate", "Basic Rate"));
                ddlBaseComponent.Items.Add(new ListItem("Final Rate", "Final Rate"));
                ddlBaseComponent.Items.Add(new ListItem("Flat Amount", "Flat Amount"));
                ddlBaseComponent.DataSource = dv;
                ddlBaseComponent.DataTextField = "COMPO_CODE";
                ddlBaseComponent.DataValueField = "COMPO_CODE";
                ddlBaseComponent.DataBind();
            }
            else
            {
                grdRate.DataSource = null;
                grdRate.DataBind();
            }
            CalculateFinalAmount();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void CalculateFinalAmount()
    {
        try
        {
            if (ViewState["COMP_CODE"] != null)
            {
                COMP_CODE = (string)ViewState["COMP_CODE"];
            }
            if (ViewState["BRANCH_CODE"] != null)
            {
                BRANCH_CODE = (string)ViewState["BRANCH_CODE"];
            }
            if (ViewState["PO_TYPE"] != null)
            {
                PO_TYPE = (string)ViewState["PO_TYPE"];
            }
            if (ViewState["PA_NO"] != null)
            {
                PA_NO = (string)ViewState["PA_NO"];
            }

            if (ViewState["YARN_CODE"] != null)
            {
                YARN_CODE = (string)ViewState["YARN_CODE"];
            }
            if (ViewState["dtDicRate"] != null)
            {
                dtDicRate = (DataTable)ViewState["dtDicRate"];
            }
            if (ViewState["StartFinalAmount"] != null)
            {
                StartFinalAmount = (Double)ViewState["StartFinalAmount"];
            }
            if (ViewState["SHADE_CODE"] != null)
            {
                SHADE_CODE = (string)ViewState["SHADE_CODE"];
            }
            if (dtDicRate != null && dtDicRate.Rows.Count > 0)
            {
                DataView dvRate = dtDicRate.DefaultView;
                dvRate.RowFilter = "COMP_CODE='" + COMP_CODE + "' AND BRANCH_CODE='" + BRANCH_CODE + "' AND PO_TYPE='" + PO_TYPE + "' AND PA_NO='" + PA_NO + "' AND YARN_CODE='" + YARN_CODE + "' ";
                if (dvRate.Count > 0)
                {

                    dvRate.Sort = "COMPO_SL asc";

                    FinalAmount = StartFinalAmount;

                    for (int iLoop = 0; iLoop < dvRate.Count; iLoop++)
                    {
                        double Rate = double.Parse(dvRate[iLoop]["Rate"].ToString());
                        string Type = dvRate[iLoop]["COMPO_TYPE"].ToString();
                        double Amount = double.Parse(dvRate[iLoop]["Amount"].ToString());

                        if (Type == "A")
                            FinalAmount = FinalAmount + Amount;
                        else
                            FinalAmount = FinalAmount - Amount;
                    }


                    ViewState["FinalAmount"] = Math.Round(FinalAmount, 4);
                    ViewState["dtDicRate"] = dtDicRate;
                    txtFinalAmount.Text = Math.Round(FinalAmount, 4).ToString();
                }
            }
            else
            {
                double finalRate = 0;
                double.TryParse(lblBasicRate.Text, out finalRate);
                ViewState["FinalAmount"] = Math.Round(finalRate, 4);
                txtFinalAmount.Text = Math.Round(finalRate, 4).ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void grdRate_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int Uniqueid = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "delRate")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                Label lblComponentName = (Label)row.FindControl("lblComponentName");
                string compo_Code = lblComponentName.Text;
                RemoveRowFromDataTable(Uniqueid, compo_Code);
            }
            else if (e.CommandName == "editRate")
            {
                FillComponentForEdit(Uniqueid);
            }

            fillGridByDataTable();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    private void FillComponentForEdit(int Uniqueid)
    {
        if (ViewState["dtDicRate"] != null)
        {
            dtDicRate = (DataTable)ViewState["dtDicRate"];
        }
        DataView dvFillForEdit = new DataView(dtDicRate);
        dvFillForEdit.RowFilter = "Uniqueid=" + Uniqueid;
        if (dvFillForEdit.Count > 0)
        {
            ddlRateComponent.SelectedIndex = ddlRateComponent.Items.IndexOf(ddlRateComponent.Items.FindByValue(dvFillForEdit[0]["COMPO_CODE"].ToString()));
            ddlBaseComponent.SelectedIndex = ddlBaseComponent.Items.IndexOf(ddlBaseComponent.Items.FindByValue(dvFillForEdit[0]["BASE_COMPO_CODE"].ToString()));

            txtRate.Text = dvFillForEdit[0]["Rate"].ToString();
            btnSaveRateCompo.Focus();
            ViewState["Uniqueid"] = Uniqueid;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            // FillDataTableByGrid();

            CalculateFinalAmount();
            if (ViewState["dtDicRate"] != null)
            {
                dtDicRate = (DataTable)ViewState["dtDicRate"];
            }
            if (ViewState["FinalAmount"] != null)
            {
                FinalAmount = (double)ViewState["FinalAmount"];
            }


            if (ViewState["TextBoxId"] != null)
            {
                TextBoxId = (string)ViewState["TextBoxId"];
            }

            Session["dtDicRate"] = dtDicRate;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:BindRate('" + FinalAmount + "','" + TextBoxId + "')", true);

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void btnSaveRateCompo_Click(object sender, EventArgs e)
    {
        try
        {
            if (!ddlBaseComponent.SelectedValue.Equals("Flat Amount"))
            {
                if (!checkRatePercent())
                {
                    return;
                }
            }
            if (ddlRateComponent.SelectedValue.Trim() != "" || ddlRateComponent.SelectedIndex != 0)
            {
                AddRowInDataTable();
                fillGridByDataTable();
                CalculateFinalAmount();

                ViewState["FinalAmount"] = FinalAmount;
                txtFinalAmount.Text = FinalAmount.ToString();

            }
            else
            {
                Common.CommonFuction.ShowMessage("Please select rate component");
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void btnCancelRateCompo_Click(object sender, EventArgs e)
    {
        try
        {
            txtRate.Text = string.Empty;
            ddlRateComponent.SelectedIndex = 0;
            ddlBaseComponent.SelectedIndex = 0;

            ViewState["Uniqueid"] = 0;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            Common.CommonFuction.ShowMessage("Problem is cancel. See Error log for detail");
        }
    }

    protected void grdRate_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblIS_PO = (Label)e.Row.FindControl("lblIS_PO");
                if (lblIS_PO.Text.Equals("1", StringComparison.OrdinalIgnoreCase))
                {
                    LinkButton lbtnEdit = (LinkButton)e.Row.FindControl("lbtnEdit");
                    LinkButton lbtnDelete = (LinkButton)e.Row.FindControl("lbtnDelete");

                    lbtnDelete.Visible = false;
                    lbtnEdit.Visible = false;
                }

            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem is cancel. See Error log for detail"));
        }
    }
    protected void txtRate_TextChanged(object sender, EventArgs e)
    {
        if (!ddlBaseComponent.SelectedValue.Equals("Flat Amount"))
        {
            if (!checkRatePercent())
            {
                return;
            }
        }
    }

    protected bool checkRatePercent()
    {
        bool result = true;
        double _rate = 0;
        double.TryParse(txtRate.Text, out _rate);
        if (_rate > 99.99 || _rate < 0)
        {
            Common.CommonFuction.ShowMessage("Rate(%) should not be less than 0 or grater than 99.9%.");
            result = false;
        }
        return result;
    }



}
