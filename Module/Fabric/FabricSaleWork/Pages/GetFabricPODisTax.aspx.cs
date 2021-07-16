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

public partial class Module_Inventory_Pages_GetFabricPODisTax : System.Web.UI.Page
{
    private static DataTable dtRate = null;
    private static DataTable dtdRateComponent = null;
    private static float FinalAmount = 0;
    private static float StartFinalAmount = 0;
    private static string TextBoxId = "";
    private static string FABR_CODE = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["TextBoxId"] != null)
                    TextBoxId = Request.QueryString["TextBoxId"].Trim();
                if (Request.QueryString["FinalAmount"] != null)
                    StartFinalAmount = float.Parse(Request.QueryString["FinalAmount"].Trim());

                if (Request.QueryString["FABR_CODE"] != null)
                {
                    FABR_CODE = Request.QueryString["FABR_CODE"].ToString();
                    Label1.Text = FABR_CODE;
                }
                if (Session["dtDicRate"] != null)
                {
                    if (dtRate == null)
                        CreateDataTable();
                    dtRate = (DataTable)Session["dtDicRate"];
                    fillGridByDataTable();
                }

                FinalAmount = StartFinalAmount;
                txtBaseRate.Text = StartFinalAmount.ToString();
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
            dtdRateComponent = SaitexBL.Interface.Method.TX_RATE_COMPONENT.GetRateComponent();
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
            dtRate.Columns.Add("YEAR", typeof(int));
            dtRate.Columns.Add("FABR_CODE", typeof(string));
            dtRate.Columns.Add("COMPO_CODE", typeof(string));
            dtRate.Columns.Add("Rate", typeof(float));
            dtRate.Columns.Add("COMPO_SL", typeof(int));
            dtRate.Columns.Add("COMPO_TYPE", typeof(string));
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
                foreach (DataRow dr in dtRate.Rows)
                {
                    string sCode = dr["COMPO_CODE"].ToString();
                    string sFabricCode = dr["FABR_CODE"].ToString();
                    if (sCode == Code && sFabricCode == FABR_CODE)
                    {
                        TextBox txtRate = (TextBox)Row.FindControl("txtRate");
                        dr["Rate"] = float.Parse(txtRate.Text.Trim());
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

            if (dtRate == null)
                CreateDataTable();

            bool IsDuplicate = false;
            if (dtRate.Rows.Count > 0)
            {
                DataView dvRate = new DataView(dtRate);
                dvRate.RowFilter = "FABR_CODE='" + FABR_CODE + "' and COMPO_CODE='" + Code + "' and Uniqueid <> " + Uniqueid;

                if (dvRate.Count > 0)
                {
                    IsDuplicate = true;
                }
            }
            if (!IsDuplicate)
            {

                DataView dvRateComponent = new DataView(dtdRateComponent);
                dvRateComponent.RowFilter = "COMPO_CODE='" + Code + "'";
                if (dvRateComponent.Count > 0)
                {
                    double rate = 0;
                    double.TryParse(txtRate.Text.Trim(), out rate);
                    if (rate > 0)
                    {
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
                                dtRate.AcceptChanges();
                            }
                        }
                        else
                        {
                            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                            DataRow dr = dtRate.NewRow();
                            dr["Uniqueid"] = dtRate.Rows.Count + 1;
                            dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                            dr["FABR_CODE"] = FABR_CODE;
                            dr["COMPO_CODE"] = Code;
                            dr["Rate"] = rate;
                            dr["COMPO_SL"] = int.Parse(dvRateComponent[0]["COMPO_SL"].ToString());
                            dr["COMPO_TYPE"] = dvRateComponent[0]["COMPO_TYPE"].ToString();
                            dtRate.Rows.Add(dr);
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
            if (dtRate.Rows.Count == 1)
                dtRate.Rows.Clear();

            foreach (DataRow dr in dtRate.Rows)
            {
                int iUniqueId = int.Parse(dr["Uniqueid"].ToString());
                if (iUniqueId == UniqueId)
                    dtRate.Rows.Remove(dr);
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
            DataView dv = new DataView(dtRate);
            dv.RowFilter = "FABR_CODE='" + FABR_CODE + "'";
            if (dv.Count > 0)
            {
                grdRate.DataSource = dv;
                grdRate.DataBind();
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
            if (dtRate != null && dtRate.Rows.Count > 0)
            {
                DataView dvRate = dtRate.DefaultView;
                dvRate.RowFilter = "FABR_CODE='" + FABR_CODE + "'";
                if (dvRate.Count > 0)
                {
                    dvRate.Sort = "COMPO_SL asc";
                    FinalAmount = StartFinalAmount;

                    for (int iLoop = 0; iLoop < dvRate.Count; iLoop++)
                    {
                        float Rate = float.Parse(dvRate[iLoop]["Rate"].ToString());
                        string Type = dvRate[iLoop]["COMPO_TYPE"].ToString();
                        float Amount = (FinalAmount * Rate) / 100;
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
        DataView dvFillForEdit = new DataView(dtRate);
        dvFillForEdit.RowFilter = "Uniqueid=" + Uniqueid;
        if (dvFillForEdit.Count > 0)
        {
            ddlRateComponent.SelectedIndex = ddlRateComponent.Items.IndexOf(ddlRateComponent.Items.FindByValue(dvFillForEdit[0]["COMPO_CODE"].ToString()));
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
            Session["dtDicRate"] = dtRate;
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
                string msg = string.Empty;
                double dRate = 0;
                if (Common.CommonFuction.ToDouble(txtRate.Text, "Rate", 7, 3, out msg, out dRate))
                {
                    AddRowInDataTable();
                    fillGridByDataTable();
                    CalculateFinalAmount();
                    txtFinalAmount.Text = FinalAmount.ToString();
                }
                else
                {
                    Common.CommonFuction.ShowMessage(msg);
                }
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

            ViewState["Uniqueid"] = 0;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            Common.CommonFuction.ShowMessage("Problem is cancel. See Error log for detail");
        }
    }
}
