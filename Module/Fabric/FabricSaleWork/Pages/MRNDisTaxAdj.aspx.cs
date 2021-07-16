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

public partial class Module_Fabric_FabricSaleWork_Pages_MRNDisTaxAdj : System.Web.UI.Page
{
    private DataTable dtRate1 = null;
    private DataTable dtdRateComponent = null;
    private double FinalAmount = 0;
    private double StartFinalAmount = 0;
    private string TextBoxId = string.Empty;
    private string PO_COMP_CODE = string.Empty;
    private string PO_BRANCH = string.Empty;
    private string PO_TYPE = string.Empty;
    private string PO_YEAR = string.Empty;
    private int PO_NUMB = 0;
    private string FABR_CODE = string.Empty;
    private string SHADE_CODE = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["PO_YEAR"] != null)
            {
                PO_YEAR = Request.QueryString["PO_YEAR"].Trim();
            }
            if (!IsPostBack)
            {
                dtRate1 = null;
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
                if (Request.QueryString["PO_COMP_CODE"] != null)
                {
                    PO_COMP_CODE = Request.QueryString["PO_COMP_CODE"].ToString();
                    ViewState["PO_COMP_CODE"] = PO_COMP_CODE;
                }
                if (Request.QueryString["PO_BRANCH"] != null)
                {
                    PO_BRANCH = Request.QueryString["PO_BRANCH"].ToString();
                    ViewState["PO_BRANCH"] = PO_BRANCH;
                }
                if (Request.QueryString["PO_TYPE"] != null)
                {
                    PO_TYPE = Request.QueryString["PO_TYPE"].ToString();
                    ViewState["PO_TYPE"] = PO_TYPE;
                }
                if (Request.QueryString["PO_NUMB"] != null)
                {
                    PO_NUMB = int.Parse(Request.QueryString["PO_NUMB"].ToString());
                    ViewState["PO_NUMB"] = PO_NUMB;
                } 
                if (Request.QueryString["PO_YEAR"] != null)
                {
                    PO_YEAR = Request.QueryString["PO_YEAR"].Trim();
                }
                if (Request.QueryString["FABR_CODE"] != null)
                {
                    FABR_CODE = Request.QueryString["FABR_CODE"].ToString();
                    Label1.Text = FABR_CODE;
                    ViewState["FABR_CODE"] = FABR_CODE;
                }
                if (Request.QueryString["SHADE_CODE"] != null)
                {
                    SHADE_CODE = Request.QueryString["SHADE_CODE"].ToString();
                    ViewState["SHADE_CODE"] = SHADE_CODE;
                    lblshade.Text = SHADE_CODE;
                }
                if (Session["dtDicRate"] != null)
                {
                    if (dtRate1 == null)
                        CreateDataTable();
                    dtRate1 = (DataTable)Session["dtDicRate"];

                    if (!dtRate1.Columns.Contains("Amount"))
                    {
                        dtRate1.Columns.Add("Amount", typeof(double));
                    }

                    if (ViewState["StartFinalAmount"] != null)
                    {
                        StartFinalAmount = (Double)ViewState["StartFinalAmount"];
                    }

                    double dFinalRate =Math.Round(StartFinalAmount,3);
                    foreach (DataRow dr in dtRate1.Rows)
                    {
                        string syarnCode = dr["FABR_CODE"].ToString();
                        string sShadecode = dr["SHADE_CODE"].ToString();
                        if (SHADE_CODE.Equals(sShadecode, StringComparison.OrdinalIgnoreCase) && FABR_CODE.Equals(syarnCode, StringComparison.OrdinalIgnoreCase))
                        {
                            double dAmount = 0;

                            double cAmount = 0;
                            double rate = double.Parse(dr["Rate"].ToString());
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
                                DataView dvv = new DataView(dtRate1);
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
                            dr["Amount"] = Math.Round(cAmount,3);

                        }
                    }
                    ViewState["dtRate1"] = dtRate1;


                    fillGridByDataTable();

                }
                FinalAmount = Math.Round(StartFinalAmount,3);
                ViewState["FinalAmount"] =Math.Round( FinalAmount,3);
                GetRateComponent();
                BindRateComponentGrid();
                CalculateFinalAmount();
                txtFinalAmount.Text = Math.Round(FinalAmount,3).ToString();
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
            dtRate1 = new DataTable();
            dtRate1.Columns.Add("Uniqueid", typeof(int));
            dtRate1.Columns.Add("PO_COMP_CODE", typeof(string));
            dtRate1.Columns.Add("PO_BRANCH", typeof(string));
            dtRate1.Columns.Add("PO_TYPE", typeof(string));
            dtRate1.Columns.Add("PO_NUMB", typeof(int));
            dtRate1.Columns.Add("PO_YEAR", typeof(int));
           
            dtRate1.Columns.Add("FABR_CODE", typeof(string));

            dtRate1.Columns.Add("COMPO_CODE", typeof(string));
            dtRate1.Columns.Add("Rate", typeof(double));
            dtRate1.Columns.Add("COMPO_SL", typeof(int));
            dtRate1.Columns.Add("COMPO_TYPE", typeof(string));
            dtRate1.Columns.Add("Amount", typeof(double));
            dtRate1.Columns.Add("BASE_COMPO_CODE", typeof(string));
            dtRate1.Columns.Add("IS_PO", typeof(string));
            dtRate1.Columns.Add("SHADE_CODE", typeof(string));
            ViewState["dtRate1"] = dtRate1;

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
            if (ViewState["PO_COMP_CODE"] != null)
            {
                PO_COMP_CODE = (string)ViewState["PO_COMP_CODE"];
            }
            if (ViewState["PO_BRANCH"] != null)
            {
                PO_BRANCH = (string)ViewState["PO_BRANCH"];
            }
            if (ViewState["PO_TYPE"] != null)
            {
                PO_TYPE = (string)ViewState["PO_TYPE"];
            }
            if (ViewState["PO_NUMB"] != null)
            {
                PO_NUMB = int.Parse(ViewState["PO_NUMB"].ToString());
            }

            if (ViewState["FABR_CODE"] != null)
                FABR_CODE = (string)ViewState["FABR_CODE"];
            if (ViewState["dtRate1"] != null)
                dtRate1 = (DataTable)ViewState["dtRate1"];
            foreach (GridViewRow Row in grdRate.Rows)
            {
                Label lblComponentName = (Label)Row.FindControl("lblComponentName");
                string Code = lblComponentName.Text.Trim();
                foreach (DataRow dr in dtRate1.Rows)
                {
                    string sCode = dr["COMPO_CODE"].ToString();
                    string sITemCode = dr["FABR_CODE"].ToString();
                    if (sCode == Code && sITemCode == FABR_CODE)
                    {
                        TextBox txtRate = (TextBox)Row.FindControl("txtRate");
                        dr["Rate"] = double.Parse(txtRate.Text.Trim());
                    }
                }

                ViewState["dtRate1"] = dtRate1;

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

            if (ViewState["dtRate1"] != null)
            {
                dtRate1 = (DataTable)ViewState["dtRate1"];
            }

            if (ViewState["PO_COMP_CODE"] != null)
            {
                PO_COMP_CODE = (string)ViewState["PO_COMP_CODE"];
            }
            if (ViewState["PO_BRANCH"] != null)
            {
                PO_BRANCH = (string)ViewState["PO_BRANCH"];
            }
            if (ViewState["PO_TYPE"] != null)
            {
                PO_TYPE = (string)ViewState["PO_TYPE"];
            }
            if (ViewState["PO_NUMB"] != null)
            {
                PO_NUMB = int.Parse(ViewState["PO_NUMB"].ToString());
            }

            if (ViewState["FABR_CODE"] != null)
            {
                FABR_CODE = (string)ViewState["FABR_CODE"];
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

            if (dtRate1 == null)
                CreateDataTable();

            bool IsDuplicate = false;
            if (dtRate1.Rows.Count > 0)
            {
                DataView dvRate = new DataView(dtRate1);
                dvRate.RowFilter = "PO_COMP_CODE='" + PO_COMP_CODE + "' AND PO_BRANCH='" + PO_BRANCH + "' AND PO_TYPE='" + PO_TYPE + "' AND PO_NUMB='" + PO_NUMB + "' AND FABR_CODE='" + FABR_CODE + "' and SHADE_CODE='" + SHADE_CODE + "'  and COMPO_CODE='" + Code + "' and Uniqueid <> " + Uniqueid +" and PO_YEAR=" + PO_YEAR ;

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
                            DataView dvv = new DataView(dtRate1);
                            dvv.RowFilter = "PO_COMP_CODE='" + PO_COMP_CODE + "' AND PO_BRANCH='" + PO_BRANCH + "' AND PO_TYPE='" + PO_TYPE + "' AND PO_NUMB='" + PO_NUMB + "' AND FABR_CODE='" + FABR_CODE + "'AND SHADE_CODE='" + SHADE_CODE + "' and COMPO_CODE='" + ddlBaseComponent.SelectedItem.Text.Trim() + "'";

                            if (dvv.Count > 0)
                            {
                                dAmount = double.Parse(dvv[0]["Amount"].ToString());
                            }
                            cAmount = (dAmount * rate) / 100;
                        }

                        if (Uniqueid > 0)
                        {
                            DataView dvUpdate = new DataView(dtRate1);
                            dvUpdate.RowFilter = "Uniqueid = " + Uniqueid;
                            if (dvUpdate.Count > 0)
                            {
                                dvUpdate[0]["COMPO_CODE"] = Code;
                                dvUpdate[0]["Rate"] = Math.Round(rate,3);
                                dvUpdate[0]["COMPO_SL"] = int.Parse(dvRateComponent[0]["COMPO_SL"].ToString());
                                dvUpdate[0]["COMPO_TYPE"] = dvRateComponent[0]["COMPO_TYPE"].ToString();

                                dvUpdate[0]["Amount"] = Math.Round(cAmount,3);
                                dvUpdate[0]["IS_PO"] = "0";
                                dvUpdate[0]["BASE_COMPO_CODE"] = ddlBaseComponent.SelectedItem.Text.Trim();
                                dvUpdate[0]["SHADE_CODE"] = SHADE_CODE;
                                dvUpdate[0]["PO_COMP_CODE"] = PO_COMP_CODE;
                                dvUpdate[0]["PO_BRANCH"] = PO_BRANCH;
                                dvUpdate[0]["PO_TYPE"] = PO_TYPE;
                                dvUpdate[0]["PO_NUMB"] = PO_NUMB;
                                int _PO_YEAR = 0;
                                int.TryParse(PO_YEAR, out _PO_YEAR);
                                dvUpdate[0]["PO_YEAR"] = _PO_YEAR;
                                dtRate1.AcceptChanges();
                            }
                        }
                        else
                        {
                            DataRow dr = dtRate1.NewRow();
                            dr["Uniqueid"] = dtRate1.Rows.Count + 1;
                            dr["PO_COMP_CODE"] = PO_COMP_CODE;
                            dr["PO_BRANCH"] = PO_BRANCH;
                            dr["PO_TYPE"] = PO_TYPE;
                            dr["PO_NUMB"] = PO_NUMB;
                            dr["FABR_CODE"] = FABR_CODE;
                            dr["COMPO_CODE"] = Code;
                            dr["Rate"] =Math.Round(rate,3);
                            dr["COMPO_SL"] = int.Parse(dvRateComponent[0]["COMPO_SL"].ToString());
                            dr["COMPO_TYPE"] = dvRateComponent[0]["COMPO_TYPE"].ToString();
                            int _PO_YEAR = 0;
                            int.TryParse(PO_YEAR, out _PO_YEAR);
                            dr["PO_YEAR"] = _PO_YEAR;
                            dr["Amount"] =Math.Round(cAmount,3);
                            dr["IS_PO"] = "0";
                            dr["BASE_COMPO_CODE"] = ddlBaseComponent.SelectedItem.Text.Trim();
                            dr["SHADE_CODE"] = SHADE_CODE;

                            dtRate1.Rows.Add(dr);
                        }
                        ddlRateComponent.SelectedIndex = 0;
                        ddlRateComponent.SelectedValue = "";
                        txtRate.Text = string.Empty;
                        ViewState["dtRate1"] = dtRate1;
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
            if (ViewState["dtRate1"] != null)
            {
                dtRate1 = (DataTable)ViewState["dtRate1"];
            }
            if (dtRate1.Rows.Count == 1)
            {
                dtRate1.Rows.Clear();
            }

            DataView DV = new DataView(dtRate1);
           // DV.RowFilter = "BASE_COMPO_CODE='" + compo_Code + "'";

            //if (DV.Count == 0)
            //{
                foreach (DataRow dr in dtRate1.Rows)
                {
                    int iUniqueId = int.Parse(dr["Uniqueid"].ToString());
                    if (iUniqueId == UniqueId)
                    {
                        dtRate1.Rows.Remove(dr);
                        break;
                    }
                }
                dtRate1.AcceptChanges();
                ViewState["dtRate1"] = dtRate1;
                CalculateFinalAmount();
                if (ViewState["FinalAmount"] != null)
                {
                    txtFinalAmount.Text = ViewState["FinalAmount"].ToString();
                }
                fillGridByDataTable();
            //}
            //else
            //{
            //    Common.CommonFuction.ShowMessage("Child record exists. Please remove all child first.");
            //}

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
            if (ViewState["PO_COMP_CODE"] != null)
            {
                PO_COMP_CODE = (string)ViewState["PO_COMP_CODE"];
            }
            if (ViewState["PO_BRANCH"] != null)
            {
                PO_BRANCH = (string)ViewState["PO_BRANCH"];
            }
            if (ViewState["PO_TYPE"] != null)
            {
                PO_TYPE = (string)ViewState["PO_TYPE"];
            }
            if (ViewState["PO_NUMB"] != null)
            {
                PO_NUMB = int.Parse(ViewState["PO_NUMB"].ToString());
            }

            if (ViewState["FABR_CODE"] != null)
            {
                FABR_CODE = (string)ViewState["FABR_CODE"];
            }
            if (ViewState["SHADE_CODE"] != null)
            {
                SHADE_CODE = (string)ViewState["SHADE_CODE"];
            }
            if (ViewState["dtRate1"] != null)
            {
                dtRate1 = (DataTable)ViewState["dtRate1"];
            }
            grdRate.DataSource = null;
            grdRate.DataBind();
            DataView dv = new DataView(dtRate1);
            dv.RowFilter = "PO_COMP_CODE='" + PO_COMP_CODE + "' AND PO_BRANCH='" + PO_BRANCH + "' AND PO_TYPE='" + PO_TYPE + "' AND PO_NUMB='" + PO_NUMB + "' AND FABR_CODE='" + FABR_CODE + "' AND SHADE_CODE='" + SHADE_CODE + "'";
            if (dv.Count > 0)
            {
                grdRate.DataSource = dv;
                grdRate.DataBind();

                ddlBaseComponent.Items.Clear();
                ddlBaseComponent.Items.Add(new ListItem("Basic Rate", "Basic Rate"));
                ddlBaseComponent.Items.Add(new ListItem("Final Rate", "Final Rate"));
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
            if (ViewState["PO_COMP_CODE"] != null)
            {
                PO_COMP_CODE = (string)ViewState["PO_COMP_CODE"];
            }
            if (ViewState["PO_BRANCH"] != null)
            {
                PO_BRANCH = (string)ViewState["PO_BRANCH"];
            }
            if (ViewState["PO_TYPE"] != null)
            {
                PO_TYPE = (string)ViewState["PO_TYPE"];
            }
            if (ViewState["PO_NUMB"] != null)
            {
                PO_NUMB = int.Parse(ViewState["PO_NUMB"].ToString());
            }

            if (ViewState["FABR_CODE"] != null)
            {
                FABR_CODE = (string)ViewState["FABR_CODE"];
            }
            if (ViewState["dtRate1"] != null)
            {
                dtRate1 = (DataTable)ViewState["dtRate1"];
            }
            if (ViewState["StartFinalAmount"] != null)
            {
                StartFinalAmount = (Double)ViewState["StartFinalAmount"];
            }
            if (ViewState["SHADE_CODE"] != null)
            {
                SHADE_CODE = (string)ViewState["SHADE_CODE"];
            }
            if (dtRate1 != null && dtRate1.Rows.Count > 0)
            {
                DataView dvRate = dtRate1.DefaultView;
                dvRate.RowFilter = "PO_COMP_CODE='" + PO_COMP_CODE + "' AND PO_BRANCH='" + PO_BRANCH + "' AND PO_TYPE='" + PO_TYPE + "' AND PO_NUMB='" + PO_NUMB + "' AND FABR_CODE='" + FABR_CODE + "' ";
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


                    ViewState["FinalAmount"] = Math.Round(FinalAmount,3);
                    ViewState["dtRate1"] = dtRate1;
                    txtFinalAmount.Text = Math.Round(FinalAmount,3).ToString();
                }
            }
            else             
            {
                ViewState["FinalAmount"] = Math.Round(StartFinalAmount,3);
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
        if (ViewState["dtRate1"] != null)
        {
            dtRate1 = (DataTable)ViewState["dtRate1"];
        }
        DataView dvFillForEdit = new DataView(dtRate1);
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
            if (ViewState["dtRate1"] != null)
            {
                dtRate1 = (DataTable)ViewState["dtRate1"];
            }
            if (ViewState["FinalAmount"] != null)
            {
                FinalAmount = (double)ViewState["FinalAmount"];
            }


            if (ViewState["TextBoxId"] != null)
            {
                TextBoxId = (string)ViewState["TextBoxId"];
            }

            Session["dtDicRate"] = dtRate1;

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
                    txtRate.Text = string.Empty;
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
                txtRate.Text = string.Empty;
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
