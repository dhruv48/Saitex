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

public partial class Module_Appaerals_Pages_InvoiceTaxAdj : System.Web.UI.Page
{
    private DataTable dtRate1 = null;
    private DataTable dtdRateComponent = null;
    private double FinalAmount = 0;
   // private double StartFinalAmount = 0;
    private string TextBoxId = string.Empty;
    private string PO_COMP_CODE = string.Empty;
    private string PO_BRANCH = string.Empty;
    private string PO_TYPE = string.Empty;
    private int PO_NUMB = 0;
    private string FABRIC_CODE = string.Empty;
    private string COLOUR = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
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
                    FinalAmount = double.Parse(Request.QueryString["FinalAmount"].Trim());
                    lblBasicRate.Text = Request.QueryString["FinalAmount"].Trim();
                    ViewState["FinalAmount"] = FinalAmount;
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
                if (Request.QueryString["FABRIC_CODE"] != null)
                {
                    FABRIC_CODE = Request.QueryString["FABRIC_CODE"].ToString();
                    Label1.Text = FABRIC_CODE;
                    ViewState["FABRIC_CODE"] = FABRIC_CODE;
                }
                if (Request.QueryString["COLOUR"] != null)
                {
                    COLOUR = Request.QueryString["COLOUR"].ToString();
                    ViewState["COLOUR"] = COLOUR;
                    lblshade.Text = COLOUR;
                }
                if (Session["dtDicRate1"] != null)
                {
                    if (dtRate1 == null)
                        CreateDataTable();
                    dtRate1 = (DataTable)Session["dtDicRate1"];

                    if (!dtRate1.Columns.Contains("Amount"))
                    {
                        dtRate1.Columns.Add("Amount", typeof(double));
                    }

                    if (ViewState["FinalAmount"] != null)
                    {
                        FinalAmount = (Double)ViewState["FinalAmount"];
                    }

                    double dFinalRate = FinalAmount;
                    foreach (DataRow dr in dtRate1.Rows)
                    {
                        string syarnCode = dr["FABRIC_CODE"].ToString();
                        string sShadecode = dr["COLOUR"].ToString();
                        if (COLOUR.Equals(sShadecode, StringComparison.OrdinalIgnoreCase) && FABRIC_CODE.Equals(syarnCode, StringComparison.OrdinalIgnoreCase))
                        {
                            double dAmount = 0;

                            double cAmount = 0;
                            double rate = double.Parse(dr["Rate"].ToString());
                            if (dr["BASE_COMPO_CODE"].ToString().Equals("Basic Rate"))
                            {
                                cAmount = (FinalAmount * rate) / 100;
                            }
                            else if (dr["BASE_COMPO_CODE"].ToString().Equals("Final Rate"))
                            {
                                cAmount = (dFinalRate * rate) / 100;
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
                            dr["Amount"] = cAmount;

                        }
                    }
                    ViewState["dtRate1"] = dtRate1;


                    fillGridByDataTable();

                }
                //FinalAmount = StartFinalAmount;
                ViewState["FinalAmount"] = FinalAmount;
                GetRateComponent();
                BindRateComponentGrid();
                CalculateFinalAmount();
                txtFinalAmount.Text = FinalAmount.ToString();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Disablewinjs", "javascript:DisableTextObj('" + TextBoxId + "')", true);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading. See error log for detail"));
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

            if (ViewState["FABRIC_CODE"] != null)
            {
                FABRIC_CODE = (string)ViewState["FABRIC_CODE"];
            }
            if (ViewState["COLOUR"] != null)
            {
                COLOUR = (string)ViewState["COLOUR"];
            }
            if (ViewState["dtRate1"] != null)
            {
                dtRate1 = (DataTable)ViewState["dtRate1"];
            }
            grdRate.DataSource = null;
            grdRate.DataBind();
            DataView dv = new DataView(dtRate1);
            dv.RowFilter = "PO_COMP_CODE='" + PO_COMP_CODE + "' AND PO_BRANCH='" + PO_BRANCH + "' AND PO_TYPE='" + PO_TYPE + "' AND PO_NUMB='" + PO_NUMB + "' AND FABRIC_CODE='" + FABRIC_CODE + "' AND COLOUR='" + COLOUR + "'";
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

            if (ViewState["FABRIC_CODE"] != null)
            {
                FABRIC_CODE = (string)ViewState["FABRIC_CODE"];
            }
            if (ViewState["dtRate1"] != null)
            {
                dtRate1 = (DataTable)ViewState["dtRate1"];
            }
            if (ViewState["FinalAmount"] != null)
            {
                FinalAmount = (double)ViewState["FinalAmount"];
            }
            if (ViewState["COLOUR"] != null)
            {
                COLOUR = (string)ViewState["COLOUR"];
            }
            if (dtRate1 != null && dtRate1.Rows.Count > 0)
            {
                DataView dvRate = dtRate1.DefaultView;
                dvRate.RowFilter = "PO_COMP_CODE='" + PO_COMP_CODE + "' AND PO_BRANCH='" + PO_BRANCH + "' AND PO_TYPE='" + PO_TYPE + "' AND PO_NUMB='" + PO_NUMB + "' AND FABRIC_CODE='" + FABRIC_CODE + "' ";
                if (dvRate.Count > 0)
                {

                    dvRate.Sort = "COMPO_SL asc";

                    //FinalAmount = FinalAmount;

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


                    ViewState["FinalAmount"] = FinalAmount;
                    ViewState["dtRate1"] = dtRate1;
                    txtFinalAmount.Text = FinalAmount.ToString();
                }
            }
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
            dtRate1.Columns.Add("FABRIC_CODE", typeof(string));

            dtRate1.Columns.Add("COMPO_CODE", typeof(string));
            dtRate1.Columns.Add("Rate", typeof(double));
            dtRate1.Columns.Add("COMPO_SL", typeof(int));
            dtRate1.Columns.Add("COMPO_TYPE", typeof(string));
            dtRate1.Columns.Add("Amount", typeof(double));
            dtRate1.Columns.Add("BASE_COMPO_CODE", typeof(string));
            dtRate1.Columns.Add("IS_PO", typeof(string));
            dtRate1.Columns.Add("COLOUR", typeof(string));
            ViewState["dtRate1"] = dtRate1;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnSaveRateCompo_Click(object sender, EventArgs e)
    {
        try
        {
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

            if (ViewState["FABRIC_CODE"] != null)
            {
                FABRIC_CODE = (string)ViewState["FABRIC_CODE"];
            }
            if (ViewState["COLOUR"] != null)
            {
                COLOUR = (string)ViewState["COLOUR"];
            }
            if (ViewState["FinalAmount"] != null)
            {
                FinalAmount = (double)ViewState["FinalAmount"];
            }
            if (ViewState["StartFinalAmount"] != null)
            {
                FinalAmount = (double)ViewState["StartFinalAmount"];
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
                dvRate.RowFilter = "PO_COMP_CODE='" + PO_COMP_CODE + "' AND PO_BRANCH='" + PO_BRANCH + "' AND PO_TYPE='" + PO_TYPE + "' AND PO_NUMB='" + PO_NUMB + "' AND FABRIC_CODE='" + FABRIC_CODE + "' and COLOUR='" + COLOUR + "'  and COMPO_CODE='" + Code + "' and Uniqueid <> " + Uniqueid;

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
                            cAmount = (FinalAmount * rate) / 100;
                        }
                        else if (ddlBaseComponent.SelectedValue.Equals("Final Rate"))
                        {
                            cAmount = (FinalAmount * rate) / 100;
                        }
                        else
                        {
                            DataView dvv = new DataView(dtRate1);
                            dvv.RowFilter = "PO_COMP_CODE='" + PO_COMP_CODE + "' AND PO_BRANCH='" + PO_BRANCH + "' AND PO_TYPE='" + PO_TYPE + "' AND PO_NUMB='" + PO_NUMB + "' AND FABRIC_CODE='" + FABRIC_CODE + "'AND COLOUR='" + COLOUR + "' and COMPO_CODE='" + ddlBaseComponent.SelectedItem.Text.Trim() + "'";

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
                                dvUpdate[0]["Rate"] = rate;
                                dvUpdate[0]["COMPO_SL"] = int.Parse(dvRateComponent[0]["COMPO_SL"].ToString());
                                dvUpdate[0]["COMPO_TYPE"] = dvRateComponent[0]["COMPO_TYPE"].ToString();

                                dvUpdate[0]["Amount"] = cAmount;
                                dvUpdate[0]["IS_PO"] = "0";
                                dvUpdate[0]["BASE_COMPO_CODE"] = ddlBaseComponent.SelectedItem.Text.Trim();
                                dvUpdate[0]["COLOUR"] = COLOUR;
                                dvUpdate[0]["PO_COMP_CODE"] = PO_COMP_CODE;
                                dvUpdate[0]["PO_BRANCH"] = PO_BRANCH;
                                dvUpdate[0]["PO_TYPE"] = PO_TYPE;
                                dvUpdate[0]["PO_NUMB "] = PO_NUMB;
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
                            dr["FABRIC_CODE"] = FABRIC_CODE;
                            dr["COMPO_CODE"] = Code;
                            dr["Rate"] = rate;
                            dr["COMPO_SL"] = int.Parse(dvRateComponent[0]["COMPO_SL"].ToString());
                            dr["COMPO_TYPE"] = dvRateComponent[0]["COMPO_TYPE"].ToString();

                            dr["Amount"] = cAmount;
                            dr["IS_PO"] = "0";
                            dr["BASE_COMPO_CODE"] = ddlBaseComponent.SelectedItem.Text.Trim();
                            dr["COLOUR"] = COLOUR;

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

            Session["dtDicRate1"] = dtRate1;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:BindRate('" + FinalAmount + "','" + TextBoxId + "')", true);

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
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
            DV.RowFilter = "BASE_COMPO_CODE='" + compo_Code + "'";

            if (DV.Count == 0)
            {
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
}
