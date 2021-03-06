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
using Common;

public partial class Module_OrderDevelopment_Fiber_Lap_Dip_Controls_Fiber_NewLRGenerationNew : System.Web.UI.UserControl
{    
    SaitexDM.Common.DataModel.FIBER_OD_LAB_DIP_ENTRY oFIBER_OD_LAB_DIP_ENTRY;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static ArrayList _Alphabet;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                GetLRSeriesdata();
                BindGridview();                
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void BindGridview()
    {
        try
        {
            lblMode.Text = "You are in Update Mode";
            DataTable dtPendingCR = SaitexBL.Interface.Method.FIBER_OD_LAB_DIP_ENTRY.GetCustReqPendForLabEntry(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, "FIBER_DYEING", "", "", "", "",oUserLoginDetail.UserCode);
            if (dtPendingCR != null && dtPendingCR.Rows.Count > 0)
            {
                grdLrGeneration.DataSource = dtPendingCR;
                grdLrGeneration.DataBind();
                lblTotalRecord.Text = dtPendingCR.Rows.Count.ToString();
            }
            else
            {
                Common.CommonFuction.ShowMessage("There is no LR for Generate..");
                lblTotalRecord.Text = "0";
            }
        }
        catch
        {
            throw;
        }
    }

    private void GetLRSeriesdata()
    {
        _Alphabet = new ArrayList();

        _Alphabet = new ArrayList();
        for (int i = 65; i < 91; i++)
        {
            _Alphabet.Add(Convert.ToChar(i));
        }
    }

    private void BindLRSeries(DropDownList ddlLRSeries)
    {
        try
        {
            ddlLRSeries.DataSource = _Alphabet;
            ddlLRSeries.DataBind();
        }
        catch
        {
            throw;
        }
    }

    protected DataTable GetApproveLR(string text, string SHADE_FAMILY_CODE, string SHADE_CODE, string PRTY_CODE)
    {
        try
        {
            string whereClause = " WHERE IS_APPROVED=1 and LAB_DIP_NO like :SearchQuery";
            if (!SHADE_FAMILY_CODE.Equals(string.Empty))
            {
                if (string.Compare(SHADE_FAMILY_CODE, "SELECT", true) != 1)
                {
                    whereClause += " and SHADE_FAMILY_CODE='" + SHADE_FAMILY_CODE + "' ";
                }
            }
            if (!SHADE_CODE.Equals(string.Empty))
            {
                if (string.Compare(SHADE_CODE, "SELECT", true) != 1)
                {
                    whereClause += " and SHADE_CODE='" + SHADE_CODE + "' ";
                }
            }
          

            string sortExpression = " ORDER BY LAB_DIP_NO asc, LR_OPTION asc";

            string commandText = "SELECT LAB_DIP_NO,LR_OPTION, (LAB_DIP_NO||LR_OPTION) DISP_DATA, (COMP_CODE||'@'||BRANCH_CODE||'@'||YEAR||'@'||ORDER_NO||'@'||PRTY_CODE||'@'||LAB_DIP_NO||'@'||LR_OPTION) MAIN_DATA FROM TX_ST_LABDIP_SUB_MST";

            string sPO = "";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);

            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected void grdLrGeneration_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //if (e.Row.RowIndex == 0)
                //    e.Row.Style.Add("height", "80px");

                Label lblShadeCode = (Label)e.Row.FindControl("lblShadeCode");
                Label lblShadeFamilyCode = (Label)e.Row.FindControl("lblShadeFamilyCode");
                Label lblSUBSTRATE = (Label)e.Row.FindControl("lblSUBSTRATE");                
                DropDownList ddlApproveLR = (DropDownList)e.Row.FindControl("ddlApproveLR");
                DataTable dt = GetApproveLR("%", lblShadeFamilyCode.Text, lblShadeCode.Text, lblSUBSTRATE.Text);
                ddlApproveLR.Items.Clear();
                ddlApproveLR.Items.Add(new ListItem("NA", "NA"));
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddlApproveLR.DataTextField = "DISP_DATA";
                    ddlApproveLR.DataValueField = "DISP_DATA";
                    ddlApproveLR.DataSource = dt;
                    ddlApproveLR.DataBind();
                }
                TextBox txtReqDelDate = (TextBox)e.Row.FindControl("txtReqDelDate");
                txtReqDelDate.Text = System.DateTime.Now.Date.ToShortDateString();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Data Loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ArrayList arrLstLRNo = new ArrayList();
            DataTable dtLRGenerate = CreateDataTable();
            oFIBER_OD_LAB_DIP_ENTRY = new SaitexDM.Common.DataModel.FIBER_OD_LAB_DIP_ENTRY();

            foreach (GridViewRow grdRow in grdLrGeneration.Rows)
            {
                CheckBox chkLRGenerate = (CheckBox)grdRow.FindControl("chkLRGenerate");
                if (chkLRGenerate.Checked)
                {
                    Label lblORDER_NO = (Label)grdRow.FindControl("lblORDER_NO");
                    Label lblORDER_DATE = (Label)grdRow.FindControl("lblORDER_DATE");
                    Label lblSUBSTRATE = (Label)grdRow.FindControl("lblSUBSTRATE");
                    Label lblCOUNT = (Label)grdRow.FindControl("lblLIGHT_SOURCE1");
                    Label lblPRTY_CODE = (Label)grdRow.FindControl("lblPRTY_CODE");
                    Label lblShadeFamilyCode = (Label)grdRow.FindControl("lblShadeFamilyCode");
                    Label lblShadeFamilyName = (Label)grdRow.FindControl("lblShadeFamilyName");
                    Label lblShadeCode = (Label)grdRow.FindControl("lblShadeCode");
                    Label lblShadeName = (Label)grdRow.FindControl("lblShadeName");
                    TextBox txtReqDelDate = (TextBox)grdRow.FindControl("txtReqDelDate");
                    DropDownList ddlLRSeries = (DropDownList)grdRow.FindControl("ddlLRSeries");
                    DropDownList ddlApproveLR = (DropDownList)grdRow.FindControl("ddlApproveLR");
                    Label lblOrderRefNo = (Label)grdRow.FindControl("lblOrderRefNo");
                    DataRow dr = dtLRGenerate.NewRow();
                    
                    dr["ORDER_NO"] = lblORDER_NO.Text;
                    dr["ORDER_DATE"] = lblORDER_DATE.Text;
                    dr["SUBSTRATE"] = lblSUBSTRATE.Text;
                    dr["ARTICLE_NO"] = lblSUBSTRATE.Text;
                    dr["COUNT"] = lblCOUNT.Text;
                    dr["PRTY_CODE"] = lblPRTY_CODE.Text.Trim();
                    dr["SHADE_FAMILY_CODE"] = lblShadeFamilyCode.Text;
                    dr["SHADE_FAMILY_NAME"] = lblShadeFamilyName.Text;
                    dr["SHADE_CODE"] = lblShadeCode.Text;
                    dr["SHADE_NAME"] = lblShadeName.Text;
                    dr["LAB_DIP_DATE"] = System.DateTime.Now.ToShortDateString();
                    dr["DUE_DATE"] = DateTime.Parse(txtReqDelDate.Text);
                    dr["SERIES"] = ddlLRSeries.SelectedItem.Text.Trim();
                    dr["ORDER_REF_NO"] = lblOrderRefNo.Text.Trim();
                    dr["LAB_DIP_NO"] = ddlApproveLR.SelectedItem.Text.Trim();
                    dtLRGenerate.Rows.Add(dr);
                }
            }
            oFIBER_OD_LAB_DIP_ENTRY.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFIBER_OD_LAB_DIP_ENTRY.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFIBER_OD_LAB_DIP_ENTRY.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oFIBER_OD_LAB_DIP_ENTRY.TUSER = oUserLoginDetail.UserCode;

            int bResult = SaitexBL.Interface.Method.FIBER_OD_LAB_DIP_ENTRY.Insert2(oFIBER_OD_LAB_DIP_ENTRY, dtLRGenerate, out arrLstLRNo);
            if (bResult > 0)
            {
                string strLRNo = string.Empty;

                if (arrLstLRNo != null && arrLstLRNo.Count > 0)
                {
                    for (int i = 0; i < arrLstLRNo.Count; ++i)
                    {
                        strLRNo = arrLstLRNo[i] + "  " + strLRNo;
                    }
                }
                CommonFuction.ShowMessage("LR Number Generated successfully! and Your LR Nos are : " + strLRNo);
                grdLrGeneration.DataSource = null;
                grdLrGeneration.DataBind();
                BindGridview();
            }
            else
            {
                CommonFuction.ShowMessage("Details Saving failed.");
            }

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable CreateDataTable()
    {
        try
        {
            DataTable dtLRGenerate = new DataTable();

            dtLRGenerate.Columns.Add("SERIES", typeof(string));
            dtLRGenerate.Columns.Add("LAB_DIP_NO", typeof(string));
            dtLRGenerate.Columns.Add("ORDER_NO", typeof(string));
            dtLRGenerate.Columns.Add("ORDER_DATE", typeof(string));
            dtLRGenerate.Columns.Add("BRANCH_CODE", typeof(string));
            dtLRGenerate.Columns.Add("LAB_DIP_DATE", typeof(string));
            dtLRGenerate.Columns.Add("ORDER_REF_NO", typeof(string));
            dtLRGenerate.Columns.Add("PRTY_CODE", typeof(string));
            dtLRGenerate.Columns.Add("SUBSTRATE", typeof(string));
            dtLRGenerate.Columns.Add("ARTICLE_NO", typeof(string));
            dtLRGenerate.Columns.Add("COUNT", typeof(string));
            dtLRGenerate.Columns.Add("SHADE_FAMILY_CODE", typeof(string));
            dtLRGenerate.Columns.Add("SHADE_FAMILY_NAME", typeof(string));
            dtLRGenerate.Columns.Add("SHADE_CODE", typeof(string));
            dtLRGenerate.Columns.Add("SHADE_NAME", typeof(string));
            dtLRGenerate.Columns.Add("DUE_DATE", typeof(string));

            return dtLRGenerate;
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BindGridview();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void imgbtnExit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Session["RedirectURL"] != null)
            {
                Response.Redirect(Session["RedirectURL"].ToString(), false);
                Session["RedirectURL"] = null;
            }
            else
            {
                Response.Redirect("~/Admin/Pages/welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }
    
}
