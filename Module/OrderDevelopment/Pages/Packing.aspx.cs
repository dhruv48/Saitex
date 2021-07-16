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

public partial class Module_OrderDevelopment_Pages_Packing : System.Web.UI.Page
{

    private static DataTable dtTRN_PACK = null;

    private static string PI_TYPE = string.Empty;
    //private static string TextBoxPACK = string.Empty;
    private static string ARTICAL_CODE = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                bindPACK_DDL();

                if (Request.QueryString["ARTICAL_CODE"] != null)
                    ARTICAL_CODE = Request.QueryString["ARTICAL_CODE"].Trim();

                if (Request.QueryString["PI_TYPE"] != null)
                    PI_TYPE = Request.QueryString["PI_TYPE"].ToString();

                if (Session["dtTRN_PACK"] != null)
                {
                    if (dtTRN_PACK == null)
                        CreatePACKTable();
                    dtTRN_PACK = (DataTable)Session["dtTRN_PACK"];

                }

                BindPACKGrid();

            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nsee error log for detail."));
        }
    }

    public void bindPACK_DDL()
    {
        try
        {
            ddlPACK_CODE.Items.Add("Select");
            DataTable dt = SaitexBL.Interface.Method.TX_PACKING_MST.Load_Packing_Code(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            if (dt != null && dt.Rows.Count > 0)
            {

                ddlPACK_CODE.Items.Clear();
                ddlPACK_CODE.DataSource = dt;
                ddlPACK_CODE.DataValueField = "PACK_DETAIL";
                ddlPACK_CODE.DataTextField = "PCK_CODE";
                ddlPACK_CODE.DataBind();
                ddlPACK_CODE.Items.Insert(0, new ListItem("--------Select-------", "0"));
            }

        }
        catch
        {
            throw;
        }


    }

    private void CreatePACKTable()
    {
        try
        {
            dtTRN_PACK = new DataTable();
            dtTRN_PACK.Columns.Add("UNIQUE_ID", typeof(int));
            dtTRN_PACK.Columns.Add("ARTICAL_CODE", typeof(string));
            dtTRN_PACK.Columns.Add("PI_TYPE", typeof(string));

            dtTRN_PACK.Columns.Add("PCK_CODE", typeof(string));
            dtTRN_PACK.Columns.Add("PCK_DESC", typeof(string));
            dtTRN_PACK.Columns.Add("PCK_QTY", typeof(string));

        }
        catch
        {
            throw;
        }
    }

    private void BindPACKGrid()
    {
        try
        {
            if (dtTRN_PACK == null)
                CreatePACKTable();

            DataView dv = new DataView(dtTRN_PACK);
            dv.RowFilter = "ARTICAL_CODE='" + ARTICAL_CODE + "' and PI_TYPE='" + PI_TYPE + "' ";

            grdPACK.DataSource = dv;
            grdPACK.DataBind();
        }
        catch
        {
            throw;
        }
    }

    protected void ddlPACK_CODE_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPACK_CODE.SelectedIndex > -1 || ddlPACK_CODE.SelectedItem.Text.Equals("Select") == false)
            {
                lblPACKING_DESC.Text = ddlPACK_CODE.SelectedValue.Trim();
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please select Packing Code");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in product type selection.See error log for detail."));
        }
    }

    protected void BtnPACKSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (dtTRN_PACK == null)
            {
                CreatePACKTable();
            }

            if (dtTRN_PACK.Rows.Count < 15)
            {
                if (txtPACKQty.Text != "")
                {
                    int UNIQUE_ID = 0;
                    if (ViewState["UNIQUE_ID"] != null)
                    {
                        UNIQUE_ID = int.Parse(ViewState["UNIQUE_ID"].ToString());
                    }
                    bool bb = SearchInPACKgrid(ddlPACK_CODE.SelectedItem.Text.Trim(), UNIQUE_ID);
                    if (!bb)
                    {
                        if (UNIQUE_ID > 0)
                        {
                            DataView dv = new DataView(dtTRN_PACK);
                            dv.RowFilter = "ARTICAL_CODE='" + ARTICAL_CODE + "' and PI_TYPE='" + PI_TYPE + "' and UNIQUE_ID=" + UNIQUE_ID;
                            if (dv.Count > 0)
                            {

                                dv[0]["PCK_CODE"] = ddlPACK_CODE.SelectedItem.Text.ToString().Trim();
                                dv[0]["PCK_DESC"] = lblPACKING_DESC.Text;
                                dv[0]["PCK_QTY"] = txtPACKQty.Text;
                                dtTRN_PACK.AcceptChanges();
                            }
                        }
                        else
                        {
                            DataRow dr = dtTRN_PACK.NewRow();
                            dr["UNIQUE_ID"] = dtTRN_PACK.Rows.Count + 1;
                            dr["ARTICAL_CODE"] = ARTICAL_CODE;
                            dr["PI_TYPE"] = PI_TYPE;

                            dr["PCK_CODE"] = ddlPACK_CODE.SelectedItem.Text.ToString().Trim();
                            dr["PCK_DESC"] = lblPACKING_DESC.Text;
                            dr["PCK_QTY"] = txtPACKQty.Text;
                            dtTRN_PACK.Rows.Add(dr);

                        }
                        RefresPACKRow();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Selected Product Type Already Added.Please Select Another');", true);
                    }
                    BindPACKGrid();
                }

                else
                {
                    Common.CommonFuction.ShowMessage("Please Enter Value Quantity");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("You have reached the limit of PACK Article. Only 15 Standard allowed in one Machine Process Master.");
            }

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving PACK Detail Row.\r\nSee error log for detail."));

        }
    }

    private bool SearchInPACKgrid(string PCK_CODE, int UNIQUE_ID)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in grdPACK.Rows)
            {
                Label lblgrdPACK_CODE = (Label)grdRow.FindControl("lblgrdPACK_CODE");
                Label txtPACKUNIQUE_ID = (Label)grdRow.FindControl("txtPACKUNIQUE_ID");

                int iUNIQUE_ID = int.Parse(txtPACKUNIQUE_ID.Text.Trim());
                if (lblgrdPACK_CODE.Text.Trim() == PCK_CODE && UNIQUE_ID != iUNIQUE_ID)
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

    private void RefresPACKRow()
    {
        try
        {
            ddlPACK_CODE.SelectedIndex = -1;
            lblPACKING_DESC.Text = string.Empty;
            txtPACKQty.Text = string.Empty;
            ViewState["UNIQUE_ID"] = null;
        }
        catch
        {
            throw;
        }
    }

    protected void BtnPACKCancel_Click(object sender, EventArgs e)
    {
        RefresPACKRow();
    }

    protected void grdPACKArticleDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UNIQUE_ID = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "PACKEdit")
            {
                FillPACKByGrid(UNIQUE_ID);
            }
            else if (e.CommandName == "PACKDelete")
            {
                DeletePACKRow(UNIQUE_ID);
                BindPACKGrid();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in PACK Grid RowCommand Selection.\r\nSee error log for detail."));
        }
    }

    private void FillPACKByGrid(int UNIQUE_ID)
    {
        try
        {
            DataView dv = new DataView(dtTRN_PACK);
            dv.RowFilter = "UNIQUE_ID=" + UNIQUE_ID;
            if (dv.Count > 0)
            {

                ddlPACK_CODE.SelectedIndex = ddlPACK_CODE.Items.IndexOf(ddlPACK_CODE.Items.FindByText(dv[0]["PCK_CODE"].ToString()));
                lblPACKING_DESC.Text = dv[0]["PCK_DESC"].ToString();
                txtPACKQty.Text = dv[0]["PCK_QTY"].ToString();
                ViewState["UNIQUE_ID"] = UNIQUE_ID;
            }
        }
        catch
        {
            throw;
        }
    }

    private void DeletePACKRow(int UNIQUE_ID)
    {
        try
        {
            if (grdPACK.Rows.Count == 1)
            {
                dtTRN_PACK.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtTRN_PACK.Rows)
                {
                    int iUNIQUE_ID = int.Parse(dr["UNIQUE_ID"].ToString());
                    if (iUNIQUE_ID == UNIQUE_ID)
                    {
                        dtTRN_PACK.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtTRN_PACK.Rows)
                {
                    iCount = iCount + 1;
                    dr["UNIQUE_ID"] = iCount;
                }
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

            Session["dtTRN_PACK"] = dtTRN_PACK;

            string PACK = string.Empty;
            string TextBoxPACK = string.Empty;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:BindYRNSPIN_PACK('" + PACK + "','" + TextBoxPACK + "')", true);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in submitting delivery data.\r\nsee error log for detail."));
        }
    }

}
