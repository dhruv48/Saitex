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

public partial class Module_WorkOrder_Pages_GetWODisTex : System.Web.UI.Page
{
    private DataTable dtRate = null;
    private DataTable dtdRateComponent = null;
    private static double FinalAmount = 0;
    private static double StartFinalAmount = 0;
    private static string TextBoxId = "";
    private static string ARTICLE_CODE = "";
    private static string SHADE_CODE = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ARTICLE_CODE = "";
                SHADE_CODE = "";
                FinalAmount = 0;
                StartFinalAmount = 0;
                TextBoxId = "";

                ViewState["dtRate"] = null;
                ViewState["dtdRateComponent"] = null;

                if (Request.QueryString["TextBoxId"] != null)
                    TextBoxId = Request.QueryString["TextBoxId"].Trim();

                if (Request.QueryString["FinalAmount"] != null)
                {
                    StartFinalAmount = double.Parse(Request.QueryString["FinalAmount"].Trim());
                    txtBaseRate.Text = Request.QueryString["FinalAmount"].Trim();
                    ViewState["StartFinalAmount"] = StartFinalAmount;
                }
                if (Request.QueryString["ARTICLE_CODE"] != null)
                {
                    ARTICLE_CODE = Request.QueryString["ARTICLE_CODE"].ToString();
                    Label1.Text = ARTICLE_CODE;
                }

                if (Request.QueryString["SHADE_CODE"] != null)
                {
                    SHADE_CODE = Request.QueryString["SHADE_CODE"].ToString();
                    Label2.Text = SHADE_CODE;
                }
                if (Session["dtWODicRate"] != null)
                {
                    if (ViewState["dtRate"] != null)
                        dtRate = (DataTable)ViewState["dtRate"];

                    if (dtRate == null)
                        CreateDataTable();
                    dtRate = (DataTable)Session["dtWODicRate"];

                    if (!dtRate.Columns.Contains("Amount"))
                    {
                        dtRate.Columns.Add("Amount", typeof(double));
                    }

                    if (ViewState["StartFinalAmount"] != null)
                    {
                        StartFinalAmount = (Double)ViewState["StartFinalAmount"];
                    }

                    double dFinalRate = Math.Round(StartFinalAmount, 3);
                    foreach (DataRow dr in dtRate.Rows)
                    {
                        string syarnCode = dr["ARTICLE_CODE"].ToString();
                        string sShadecode = dr["SHADE_CODE"].ToString();
                        if (SHADE_CODE.Equals(sShadecode, StringComparison.OrdinalIgnoreCase) && ARTICLE_CODE.Equals(syarnCode, StringComparison.OrdinalIgnoreCase))
                        {

                            double dAmount = 0;

                            double cAmount = 0;
                            double rate = Math.Round(double.Parse(dr["Rate"].ToString()), 3);
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
                                DataView dvv = new DataView(dtRate);
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
                            dr["Amount"] = Math.Round(cAmount, 3);

                        }
                    }
                    ViewState["dtRate"] = dtRate;


                    fillGridByDataTable();

                }

                FinalAmount = StartFinalAmount;
                ViewState["FinalAmount"] = Math.Round(FinalAmount, 3);
                GetRateComponent();
                BindRateComponentGrid();
                CalculateFinalAmount();
                txtFinalAmount.Text = Math.Round(FinalAmount, 3).ToString();
            }
        }
        catch (Exception ex)
        {
            //lblErrormsg.Text = ex.Message;
            errorLog.ErrHandler.WriteError(ex.Message);
            Common.CommonFuction.ShowMessage("Problem in page loading. See error log for detail");
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
            dtRate = new DataTable();
            dtRate.Columns.Add("UniqueId", typeof(int));
            dtRate.Columns.Add("ARTICLE_CODE", typeof(string));
            dtRate.Columns.Add("SHADE_CODE", typeof(string));
            dtRate.Columns.Add("COMPO_CODE", typeof(string));
            dtRate.Columns.Add("Rate", typeof(double));
            dtRate.Columns.Add("COMPO_SL", typeof(int));
            dtRate.Columns.Add("COMPO_TYPE", typeof(string));
            dtRate.Columns.Add("Amount", typeof(double));

            dtRate.Columns.Add("BASE_COMPO_CODE", typeof(string));
            ViewState["dtRate"] = dtRate;
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
            if (ViewState["dtRate"] != null)
                dtRate = (DataTable)ViewState["dtRate"];

            foreach (GridViewRow Row in grdRate.Rows)
            {
                Label lblComponentName = (Label)Row.FindControl("lblComponentName");
                string Code = lblComponentName.Text.Trim();
                foreach (DataRow dr in dtRate.Rows)
                {
                    string sCode = dr["COMPO_CODE"].ToString();
                    string sARTICLE_CODE = dr["ARTICLE_CODE"].ToString();
                    string sSHADE_CODE = dr["SHADE_CODE"].ToString();
                    if (sCode == Code && sARTICLE_CODE == ARTICLE_CODE && sSHADE_CODE == SHADE_CODE)
                    {
                        TextBox txtRate = (TextBox)Row.FindControl("txtRate");
                        dr["Rate"] = double.Parse(txtRate.Text.Trim());
                    }
                }

            }
            ViewState["dtRate"] = dtRate;
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
            if (ViewState["SHADE_CODE"] != null)
            {
                SHADE_CODE = (string)ViewState["SHADE_CODE"];
            }
            if (ViewState["dtRate"] != null)
            {
                dtRate = (DataTable)ViewState["dtRate"];
            }
            if (ViewState["ARTICLE_CODE"] != null)
            {
                ARTICLE_CODE = (string)ViewState["ARTICLE_CODE"];
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
                int.TryParse(ViewState["Uniqueid"].ToString(), out Uniqueid);

            string Code = string.Empty;
            Code = ddlRateComponent.SelectedValue.Trim();

            if (ViewState["dtRate"] != null)
                dtRate = (DataTable)ViewState["dtRate"];

            if (dtRate == null)
                CreateDataTable();

            bool IsDuplicate = false;
            if (dtRate.Rows.Count > 0)
            {
                DataView dvRate = new DataView(dtRate);
                dvRate.RowFilter = "ARTICLE_CODE='" + ARTICLE_CODE + "' and SHADE_CODE='" + SHADE_CODE + "' and COMPO_CODE='" + Code + "' and Uniqueid <> " + Uniqueid;

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
                dtdRateComponent = (DataTable)ViewState["dtdRateComponent"];
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
                            DataView dvv = new DataView(dtRate);
                            dvv.RowFilter = "ARTICLE_CODE='" + ARTICLE_CODE + "' and SHADE_CODE='" + SHADE_CODE + "' and COMPO_CODE='" + ddlBaseComponent.SelectedItem.Text.Trim() + "'";

                            if (dvv.Count > 0)
                            {
                                if (dvv[0]["COMPO_CODE"].ToString() == "CGST" || dvv[0]["COMPO_CODE"].ToString() == "SGST")
                                {
                                    dAmount = (FinalAmount - double.Parse(dvv[0]["Amount"].ToString()));
                                }

                                else
                                {
                                    dAmount = double.Parse(dvv[0]["Amount"].ToString());
                                }
                            }
                            cAmount = (dAmount * rate) / 100;
                        }

                        if (Uniqueid > 0)
                        {
                            DataView dvUpdate = new DataView(dtRate);
                            dvUpdate.RowFilter = "Uniqueid = " + Uniqueid;
                            if (dvUpdate.Count > 0)
                            {
                                dvUpdate[0]["COMPO_CODE"] = Code;
                                dvUpdate[0]["Rate"] = rate;
                                dvUpdate[0]["COMPO_SL"] = int.Parse(dvRateComponent[0]["COMPO_SL"].ToString());
                                dvUpdate[0]["COMPO_TYPE"] = dvRateComponent[0]["COMPO_TYPE"].ToString();

                                dvUpdate[0]["Amount"] = cAmount;
                                dvUpdate[0]["SHADE_CODE"] = SHADE_CODE;
                                dvUpdate[0]["BASE_COMPO_CODE"] = ddlBaseComponent.SelectedItem.Text.Trim();
                                dtRate.AcceptChanges();
                            }
                        }
                        else
                        {
                            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                            DataRow dr = dtRate.NewRow();
                            dr["Uniqueid"] = dtRate.Rows.Count + 1;
                            dr["ARTICLE_CODE"] = ARTICLE_CODE;
                            dr["SHADE_CODE"] = SHADE_CODE;
                            dr["COMPO_CODE"] = Code;
                            dr["Rate"] = rate;
                            dr["COMPO_SL"] = int.Parse(dvRateComponent[0]["COMPO_SL"].ToString());
                            dr["COMPO_TYPE"] = dvRateComponent[0]["COMPO_TYPE"].ToString();

                            dr["Amount"] = cAmount;
                            dr["SHADE_CODE"] = SHADE_CODE;
                            dr["BASE_COMPO_CODE"] = ddlBaseComponent.SelectedItem.Text.Trim();
                            dtRate.Rows.Add(dr);
                        }
                        ddlRateComponent.SelectedIndex = 0;
                        ddlRateComponent.SelectedValue = "";
                        txtRate.Text = string.Empty;
                        ViewState["dtRate"] = dtRate;
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

    private void RemoveRowFromDataTable(int UniqueId)
    {
        try
        {
            if (ViewState["dtRate1"] != null)
            {
                dtRate = (DataTable)ViewState["dtRate1"];
            }
            if (dtRate.Rows.Count == 1)
            {
                dtRate.Rows.Clear();
            }

            DataView DV = new DataView(dtRate);
            if (DV.Count > 0)
            {
                foreach (DataRow dr in dtRate.Rows)
                {
                    int iUniqueId = int.Parse(dr["Uniqueid"].ToString());
                    if (iUniqueId == UniqueId)
                    {
                        dtRate.Rows.Remove(dr);
                        break;
                    }
                }
                dtRate.AcceptChanges();

            }
            CalculateFinalAmount();
            ViewState["dtRate"] = dtRate;

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
            if (ViewState["ARTICLE_CODE"] != null)
            {
                ARTICLE_CODE = (string)ViewState["ARTICLE_CODE"];
            }
            if (ViewState["dtRate"] != null)
                dtRate = (DataTable)ViewState["dtRate"];

            grdRate.DataSource = null;
            grdRate.DataBind();
            DataView dv = new DataView(dtRate);
            dv.RowFilter = "ARTICLE_CODE='" + ARTICLE_CODE + "' AND SHADE_CODE='" + SHADE_CODE + "'";
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
            if (ViewState["ARTICLE_CODE"] != null)
            {
                ARTICLE_CODE = (string)ViewState["ARTICLE_CODE"];
            }
            if (ViewState["dtRate"] != null)
            {
                dtRate = (DataTable)ViewState["dtRate"];
            }
            if (ViewState["StartFinalAmount"] != null)
            {
                StartFinalAmount = (Double)ViewState["StartFinalAmount"];
            }
            if (dtRate != null && dtRate.Rows.Count > 0)
            {
                DataView dvRate = dtRate.DefaultView;
                dvRate.RowFilter = "ARTICLE_CODE='" + ARTICLE_CODE + "' AND SHADE_CODE='" + SHADE_CODE + "'";
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
                    ViewState["FinalAmount"] = Math.Round(FinalAmount, 3);
                    ViewState["dtRate"] = dtRate;
                }
            }
            else
            {
                double finalRate = 0;
                double.TryParse(txtBaseRate.Text, out finalRate);
                ViewState["FinalAmount"] = Math.Round(finalRate, 3);
                txtFinalAmount.Text = Math.Round(finalRate, 3).ToString();
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
                RemoveRowFromDataTable(Uniqueid);
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
        if (ViewState["dtRate"] != null)
            dtRate = (DataTable)ViewState["dtRate"];

        DataView dvFillForEdit = new DataView(dtRate);
        dvFillForEdit.RowFilter = "Uniqueid=" + Uniqueid;
        if (dvFillForEdit.Count > 0)
        {
            ddlRateComponent.SelectedIndex = ddlRateComponent.Items.IndexOf(ddlRateComponent.Items.FindByValue(dvFillForEdit[0]["COMPO_CODE"].ToString()));
            ddlBaseComponent.SelectedIndex = ddlBaseComponent.Items.IndexOf(ddlBaseComponent.Items.FindByValue(dvFillForEdit[0]["BASE_COMPO_CODE"].ToString()));

            txtRate.Text = dvFillForEdit[0]["Rate"].ToString();
            btnSaveRateCompo.Focus();
            ViewState["Uniqueid"] = Uniqueid;
        }
        ViewState["dtRate"] = dtRate;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            // FillDataTableByGrid();
            CalculateFinalAmount();
            if (ViewState["dtRate"] != null)
            {
                dtRate = (DataTable)ViewState["dtRate"];
            }

            Session["dtWODicRate"] = dtRate;
            if (ViewState["FinalAmount"] != null)
            {
                FinalAmount = (double)ViewState["FinalAmount"];
            }
            if (ViewState["TextBoxId"] != null)
            {
                TextBoxId = (string)ViewState["TextBoxId"];
            }
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
