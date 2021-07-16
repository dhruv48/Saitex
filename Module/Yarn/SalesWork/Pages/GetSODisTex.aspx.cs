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

using System.Data.OracleClient;
public partial class Module_Yarn_SalesWork_Pages_GetSODisTex : System.Web.UI.Page
{
    private static DataTable dtSORate1 = null;
    private static DataTable dtdSORateComponent = null;
    private static double FinalAmount = 0;
    private static double StartFinalAmount = 0;
    private static string TextBoxId = "";
    private static string YARN_CODE = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                dtSORate1 = null;
                if (Request.QueryString["TextBoxId"] != null)
                    TextBoxId = Request.QueryString["TextBoxId"].Trim();
                if (Request.QueryString["FinalAmount"] != null)
                    StartFinalAmount = double.Parse(Request.QueryString["FinalAmount"].Trim());
                lblBasicRate.Text = Request.QueryString["FinalAmount"].Trim();
                if (Request.QueryString["YARN_CODE"] != null)
                {
                    YARN_CODE = Request.QueryString["YARN_CODE"].ToString();
                    Label1.Text = YARN_CODE;
                }

                if (Session["dtDicRate"] != null)
                {
                    if (dtSORate1 == null)
                        CreateDataTable();
                    dtSORate1 = (DataTable)Session["dtDicRate"];

                    if (!dtSORate1.Columns.Contains("Amount"))
                    {
                        dtSORate1.Columns.Add("Amount", typeof(double));
                    }

                    double dFinalRate = StartFinalAmount;
                    foreach (DataRow dr in dtSORate1.Rows)
                    {
                        string syarnCode = dr["YARN_CODE"].ToString();
                        string sShadecode = dr["SHADE_CODE"].ToString();
                        //if (SHADE_CODE.Equals(sShadecode, StringComparison.OrdinalIgnoreCase) && YARN_CODE.Equals(syarnCode, StringComparison.OrdinalIgnoreCase))
                        //{

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
                            else
                            {
                                DataView dvv = new DataView(dtSORate1);
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
                   // }

                    fillGridByDataTable();

                }
                FinalAmount = StartFinalAmount;
                GetRateComponent();
                BindRateComponentGrid();
                CalculateFinalAmount();
                txtFinalAmount.Text = FinalAmount.ToString();
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
            dtdSORateComponent = SaitexBL.Interface.Method.TX_RATE_COMPONENT.GetRateComponent();
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
            ddlRateComponent.Items.Clear();
            ddlRateComponent.DataSource = null;
            ddlRateComponent.DataBind();

            ddlRateComponent.Items.Add(new ListItem("Select", ""));

            ddlRateComponent.DataSource = dtdSORateComponent;
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
            dtSORate1 = new DataTable();
            dtSORate1.Columns.Add("Uniqueid", typeof(int));
            dtSORate1.Columns.Add("YARN_CODE", typeof(string));
            dtSORate1.Columns.Add("COMPO_CODE", typeof(string));
            dtSORate1.Columns.Add("Rate", typeof(double));
            dtSORate1.Columns.Add("COMPO_SL", typeof(int));
            dtSORate1.Columns.Add("COMPO_TYPE", typeof(string));
            dtSORate1.Columns.Add("Amount", typeof(double));

            dtSORate1.Columns.Add("BASE_COMPO_CODE", typeof(string));
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
            foreach (GridViewRow Row in grdRate.Rows)
            {
                Label lblComponentName = (Label)Row.FindControl("lblComponentName");
                string Code = lblComponentName.Text.Trim();
                foreach (DataRow dr in dtSORate1.Rows)
                {
                    string sCode = dr["COMPO_CODE"].ToString();
                    string sITemCode = dr["YARN_CODE"].ToString();
                    if (sCode == Code && sITemCode == YARN_CODE)
                    {
                        TextBox txtRate = (TextBox)Row.FindControl("txtRate");
                        dr["Rate"] = double.Parse(txtRate.Text.Trim());
                    }
                }

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
            int Uniqueid = 0;
            if (ViewState["Uniqueid"] != null)
                int.TryParse(ViewState["Uniqueid"].ToString(), out Uniqueid);

            string Code = string.Empty;
            Code = ddlRateComponent.SelectedValue.Trim();

            if (dtSORate1 == null)
                CreateDataTable();

            bool IsDuplicate = false;
            if (dtSORate1.Rows.Count > 0)
            {
                DataView dvRate = new DataView(dtSORate1);
                dvRate.RowFilter = "YARN_CODE='" + YARN_CODE + "' and COMPO_CODE='" + Code + "' and Uniqueid <> " + Uniqueid;

                if (dvRate.Count > 0)
                {
                    IsDuplicate = true;
                }
            }
            if (!IsDuplicate)
            {

                DataView dvRateComponent = new DataView(dtdSORateComponent);
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
                        else
                        {
                            DataView dvv = new DataView(dtSORate1);
                            dvv.RowFilter = "YARN_CODE='" + YARN_CODE + "' and COMPO_CODE='" + ddlBaseComponent.SelectedItem.Text.Trim() + "'";

                            if (dvv.Count > 0)
                            {
                                dAmount = double.Parse(dvv[0]["Amount"].ToString());
                            }
                            cAmount = (dAmount * rate) / 100;
                        }

                        if (Uniqueid > 0)
                        {
                            DataView dvUpdate = new DataView(dtSORate1);
                            dvUpdate.RowFilter = "Uniqueid = " + Uniqueid;
                            if (dvUpdate.Count > 0)
                            {
                                dvUpdate[0]["COMPO_CODE"] = Code;
                                dvUpdate[0]["Rate"] = rate;
                                dvUpdate[0]["COMPO_SL"] = int.Parse(dvRateComponent[0]["COMPO_SL"].ToString());
                                dvUpdate[0]["COMPO_TYPE"] = dvRateComponent[0]["COMPO_TYPE"].ToString();

                                dvUpdate[0]["Amount"] = cAmount;
                                dvUpdate[0]["BASE_COMPO_CODE"] = ddlBaseComponent.SelectedItem.Text.Trim();
                                dtSORate1.AcceptChanges();
                            }
                        }
                        else
                        {
                            DataRow dr = dtSORate1.NewRow();
                            dr["Uniqueid"] = dtSORate1.Rows.Count + 1;
                            dr["YARN_CODE"] = YARN_CODE;
                            dr["COMPO_CODE"] = Code;
                            dr["Rate"] = rate;
                            dr["COMPO_SL"] = int.Parse(dvRateComponent[0]["COMPO_SL"].ToString());
                            dr["COMPO_TYPE"] = dvRateComponent[0]["COMPO_TYPE"].ToString();

                            dr["Amount"] = cAmount;
                            dr["BASE_COMPO_CODE"] = ddlBaseComponent.SelectedItem.Text.Trim();
                            dtSORate1.Rows.Add(dr);
                        }
                        ddlRateComponent.SelectedIndex = 0;
                        ddlRateComponent.SelectedValue = "";
                        txtRate.Text = string.Empty;
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
            if (dtSORate1.Rows.Count == 1)
                dtSORate1.Rows.Clear();

            DataView DV = new DataView(dtSORate1);
            DV.RowFilter = "BASE_COMPO_CODE=" + UniqueId;

            if (DV.Count == 0)
            {
                foreach (DataRow dr in dtSORate1.Rows)
                {
                    int iUniqueId = int.Parse(dr["Uniqueid"].ToString());
                    if (iUniqueId == UniqueId)
                        dtSORate1.Rows.Remove(dr);
                }
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
            grdRate.DataSource = null;
            grdRate.DataBind();
            DataView dv = new DataView(dtSORate1);
            dv.RowFilter = "YARN_CODE='" + YARN_CODE + "'";
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
            if (dtSORate1 != null && dtSORate1.Rows.Count > 0)
            {
                DataView dvRate = dtSORate1.DefaultView;
                dvRate.RowFilter = "YARN_CODE='" + YARN_CODE + "' ";
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
                }
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
        DataView dvFillForEdit = new DataView(dtSORate1);
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
            Session["dtDicRate"] = dtSORate1;
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
            if (ddlRateComponent.SelectedValue.Trim() != "" || ddlRateComponent.SelectedIndex != 0)
            {
                AddRowInDataTable();
                fillGridByDataTable();
                CalculateFinalAmount();
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

}