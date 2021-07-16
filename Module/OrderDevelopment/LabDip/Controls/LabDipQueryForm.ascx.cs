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

public partial class Module_OrderDevelopment_LabDip_Controls_LabDipQueryForm : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                Initialization();
                btnPrint.Attributes.Add("onclick", "javascript:CallPrint('divPrint');");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void Initialization()
    {
        try
        {
            lblMode.Text = "You are in Print Mode";
        }
        catch
        {
            throw;
        }
    }

    protected void btnShowCR_Click(object sender, EventArgs e)
    {
        try
        {
            grdLabDip.Columns.Clear();
            grdLabDip.Columns.Add(CreateBoundField("ORDER_TYPE", "Cust Req Type", false, "", true, false));
            grdLabDip.Columns.Add(CreateBoundField("ORDER_CAT", "cust Req Category", false, "", true, false));
            grdLabDip.Columns.Add(CreateBoundField("PRODUCT_TYPE", "Product Type", false, "", true, false));
            grdLabDip.Columns.Add(CreateBoundField("ORDER_NO", "cust Req No", false, "", true, false));
            grdLabDip.Columns.Add(CreateBoundField("BUSINESS_TYPE", "Business Type", false, "", true, false));
            grdLabDip.Columns.Add(CreateBoundField("ARTICLE_NO", "Article No", false, "", true, false));
            grdLabDip.Columns.Add(CreateBoundField("MAKE", "Make", false, "", true, false));
            grdLabDip.Columns.Add(CreateBoundField("TKT_NO", "TKT No", false, "", true, false));
            grdLabDip.Columns.Add(CreateBoundField("SHADE_NAME", "Shade", false, "", true, false));
            grdLabDip.Columns.Add(CreateBoundField("END_USE_NAME", "End Use", false, "", true, false));

            BindCRData();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private BoundField CreateBoundField(string DataField, string HeaderText, bool HTMLEncode, string DataFormatString, bool Visible, bool isNumeric)
    {
        try
        {
            BoundField boundField = new BoundField();
            boundField.DataField = DataField;
            boundField.HeaderText = HeaderText;
            boundField.HtmlEncode = HTMLEncode;

            TableItemStyle tt = new TableItemStyle();
            if (isNumeric)
                boundField.HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
            else
                boundField.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;

            boundField.Visible = Visible;
            if (DataFormatString != "")
                boundField.DataFormatString = DataFormatString;
            return boundField;
        }
        catch
        {
            throw;
        }
    }

    private void BindCRData()
    {
        try
        {
            grdLabDip.DataSource = null;
            grdLabDip.DataBind();
            string PRODUCT_TYPE = string.Empty;
            string ORDER_NO = string.Empty;
            string ARTICLE_NO = string.Empty;
            string SHADE = string.Empty;
            string END_USE = string.Empty;
            DataTable dtCR = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.GetCustReqPendForLabEntry(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, PRODUCT_TYPE, ORDER_NO, ARTICLE_NO, SHADE, END_USE);

            if (dtCR != null && dtCR.Rows.Count > 0)
            {
                grdLabDip.DataSource = dtCR;
                grdLabDip.DataBind();
            }
            else
            {
                Common.CommonFuction.ShowMessage("No Customer request pending for Lab dip Entry.");
            }
        }
        catch
        {
        }
    }

    protected void btnShowLDNA_Click(object sender, EventArgs e)
    {
        try
        {
            grdLabDip.Columns.Clear();
            CreateColLabDip(false);

            BindLDData(false);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void CreateColLabDip(bool IsApproved)
    {
        try
        {
            grdLabDip.Columns.Add(CreateBoundField("ORDER_NO", "Cust Req No", false, "", true, false));
            grdLabDip.Columns.Add(CreateBoundField("ORDER_DATE", "Cust Req Date", false, "", true, false));
            grdLabDip.Columns.Add(CreateBoundField("ORDER_REF_NO", "Order Ref No.", false, "", true, false));
            grdLabDip.Columns.Add(CreateBoundField("PRTY_NAME", "Party", false, "", true, false));
            grdLabDip.Columns.Add(CreateBoundField("ARTICLE_NO", "Article No", false, "", true, false));
            grdLabDip.Columns.Add(CreateBoundField("SHADE_NAME", "Shade", false, "", true, false));
            grdLabDip.Columns.Add(CreateBoundField("LAB_DIP_NO", "Lab Dip No", false, "", true, false));
            grdLabDip.Columns.Add(CreateBoundField("LAB_DIP_DATE", "Lab Dip Date", false, "", true, false));
            grdLabDip.Columns.Add(CreateBoundField("DUE_DATE", "Due Date", false, "", true, false));         
            grdLabDip.Columns.Add(CreateBoundField("LR_OPTION", "Option", false, "", true, false));
            if (IsApproved)
            {
                grdLabDip.Columns.Add(CreateBoundField("IS_APPROVED", "Is Approved", false, "", true, false));
                grdLabDip.Columns.Add(CreateBoundField("APPROVED_DATE", "Approve Date", false, "", true, false));
                grdLabDip.Columns.Add(CreateBoundField("APPROVED_COMMENT", "Approve Comment", false, "", true, false));
            }
        }
        catch
        {
            throw;
        }
    }

    private void BindLDData(bool IsApproved)
    {
        try
        {
            grdLabDip.DataSource = null;
            grdLabDip.DataBind();

            string ORDER_NO = string.Empty;
            string ARTICLE_NO = string.Empty;
            string SHADE = string.Empty;
            string PRTY_CODE = string.Empty;
            string IS_APPROVED = string.Empty;

            DateTime receiveFrom = DateTime.Now;
            DateTime ReceiveTo = DateTime.Now;
            DateTime dueFrom = DateTime.Now;
            DateTime dueTo = DateTime.Now;

            if (IsApproved)
                IS_APPROVED = "1";
            else
                IS_APPROVED = "0";

            DataTable dtLD = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.GetLabDipForAll(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ORDER_NO, ARTICLE_NO, SHADE, receiveFrom, ReceiveTo, dueFrom, dueTo, PRTY_CODE, IS_APPROVED);

            if (dtLD != null && dtLD.Rows.Count > 0)
            {
                grdLabDip.DataSource = dtLD;
                grdLabDip.DataBind();
            }
            else
            {
                Common.CommonFuction.ShowMessage("No data available.");
            }
        }
        catch
        {
        }
    }

    protected void btnShowLDA_Click(object sender, EventArgs e)
    {
        try
        {
            grdLabDip.Columns.Clear();
            CreateColLabDip(true);

            //BindLDData(true);
            BindApprovedData(true);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
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
                Response.Redirect("~/Module/Admin/pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in leaving page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Helping.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void btnRejected_Click(object sender, EventArgs e)
    {
        try
        {
            grdLabDip.Columns.Clear();
            CreateColLabDip(true);

            BindRejectedData(true);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void BindApprovedData(bool IsApproved)
    {
        try
        {
            grdLabDip.DataSource = null;
            grdLabDip.DataBind();

            string ORDER_NO = string.Empty;
            string ARTICLE_NO = string.Empty;
            string SHADE = string.Empty;
            string PRTY_CODE = string.Empty;
            string IS_APPROVED = string.Empty;

            DateTime receiveFrom = DateTime.Now;
            DateTime ReceiveTo = DateTime.Now;
            DateTime dueFrom = DateTime.Now;
            DateTime dueTo = DateTime.Now;

            if (IsApproved)
                IS_APPROVED = "1";
            else
                IS_APPROVED = "0";

            DataTable dtLD = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.GetLabDipForAll(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ORDER_NO, ARTICLE_NO, SHADE, receiveFrom, ReceiveTo, dueFrom, dueTo, PRTY_CODE, IS_APPROVED);
            if (dtLD != null && dtLD.Rows.Count > 0)
            {
                DataView dvRej = new DataView(dtLD);
                dvRej.RowFilter = "IS_APPROVED=1";
                if (dvRej.Count > 0)
                {
                    grdLabDip.DataSource = dvRej;
                    grdLabDip.DataBind();
                }
                else
                {
                    Common.CommonFuction.ShowMessage("No data available.");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("No data available.");
            }
        }
        catch
        {
        }
    }

    private void BindRejectedData(bool IsApproved)
    {
        try
        {
            grdLabDip.DataSource = null;
            grdLabDip.DataBind();

            string ORDER_NO = string.Empty;
            string ARTICLE_NO = string.Empty;
            string SHADE = string.Empty;
            string PRTY_CODE = string.Empty;
            string IS_APPROVED = string.Empty;

            DateTime receiveFrom = DateTime.Now;
            DateTime ReceiveTo = DateTime.Now;
            DateTime dueFrom = DateTime.Now;
            DateTime dueTo = DateTime.Now;

            if (IsApproved)
                IS_APPROVED = "1";
            else
                IS_APPROVED = "0";

            DataTable dtLD = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.GetLabDipForAll(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ORDER_NO, ARTICLE_NO, SHADE, receiveFrom, ReceiveTo, dueFrom, dueTo, PRTY_CODE, IS_APPROVED);
            if (dtLD != null && dtLD.Rows.Count > 0)
            {
                DataView dvRej = new DataView(dtLD);
                dvRej.RowFilter = "IS_APPROVED=0";
                if (dvRej.Count > 0)
                {
                    grdLabDip.DataSource = dvRej;
                    grdLabDip.DataBind();
                }
                else
                {
                    Common.CommonFuction.ShowMessage("No data available.");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("No data available.");
            }
        }
        catch
        {
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./LabDipQueryForm.aspx", false);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
}
