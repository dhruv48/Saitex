using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Module_Production_Pages_GetBaleDetail : System.Web.UI.Page
{
    List<SaitexDM.Common.DataModel.TX_FABRIC_PROD_PACKING_BALE> oBaleDetail;
    private static string TextBoxId = "";
    private static int PACKING_ID = 0;
    private static double TotalQty = 0;
    private static double TotalWidth = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                oBaleDetail = null;

                TotalQty = 0;
                TotalWidth = 0;
                TextBoxId = "";
                PACKING_ID = 0;

                if (Request.QueryString["PACKING_ID"] != null)
                    PACKING_ID = int.Parse(Request.QueryString["PACKING_ID"].Trim());
                if (Request.QueryString["TextBoxId"] != null)
                    TextBoxId = Request.QueryString["TextBoxId"].Trim();

                fillGridByDataTable();
                GetTotalQty();
            }
        }
        catch (Exception ex)
        {
            //lblErrormsg.Text = ex.Message;
            errorLog.ErrHandler.WriteError(ex.Message);
            Common.CommonFuction.ShowMessage("Problem in page loading. See error log for detail");
        }

    }

    private void GetTotalQty()
    {
        try
        {
            TotalQty = 0;
            TotalWidth = 0;
            if (Session["dtBaleDetail"] != null)
            {
                oBaleDetail = (List<SaitexDM.Common.DataModel.TX_FABRIC_PROD_PACKING_BALE>)Session["dtBaleDetail"];
            }
            else
            {
                oBaleDetail = new List<SaitexDM.Common.DataModel.TX_FABRIC_PROD_PACKING_BALE>();
            }

            var obj = (from data in oBaleDetail
                       where data.PACKING_ID == PACKING_ID
                       orderby data.BALE_ID ascending
                       select data).ToList();
            if (obj.Count > 0)
            {
                TotalQty = obj.Sum(p => p.BALE_QTY);
                TotalWidth = obj.Sum(p => p.BALE_WIDTH);
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
            if (Session["dtBaleDetail"] != null)
            {
                oBaleDetail = (List<SaitexDM.Common.DataModel.TX_FABRIC_PROD_PACKING_BALE>)Session["dtBaleDetail"];
            }
            else
            {
                oBaleDetail = new List<SaitexDM.Common.DataModel.TX_FABRIC_PROD_PACKING_BALE>();
            }

            grdBaleDetails.DataSource = null;
            grdBaleDetails.DataBind();

            var obj = (from data in oBaleDetail
                       where data.PACKING_ID == PACKING_ID
                       orderby data.BALE_ID ascending
                       select data).ToList();

            if (obj.Count > 0)
            {
                grdBaleDetails.DataSource = obj;
                grdBaleDetails.DataBind();
            }
            else
            {
                grdBaleDetails.DataSource = null;
                grdBaleDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnSaveBaleDetail_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtBaleCode.Text != string.Empty)
            {
                double BaleQty = 0;
                double.TryParse(txtBaleQty.Text, out BaleQty);

                double BaleWidth = 0;
                double.TryParse(txtBaleWidth.Text, out BaleWidth);
                if (BaleQty != 0 && BaleWidth != 0)
                {
                    if (Session["dtBaleDetail"] != null)
                    {
                        oBaleDetail = (List<SaitexDM.Common.DataModel.TX_FABRIC_PROD_PACKING_BALE>)Session["dtBaleDetail"];
                    }
                    else
                    {
                        oBaleDetail = new List<SaitexDM.Common.DataModel.TX_FABRIC_PROD_PACKING_BALE>();
                    }
                    int BALE_ID = 0;
                    if (ViewState["BALE_ID"] != null)
                    {
                        int.TryParse(ViewState["BALE_ID"].ToString(), out BALE_ID);
                    }

                    if (FindDuplicate(oBaleDetail, BALE_ID))
                    {
                        // Insert new bale details here 
                        if (BALE_ID == 0)
                        {
                            SaitexDM.Common.DataModel.TX_FABRIC_PROD_PACKING_BALE obj = new SaitexDM.Common.DataModel.TX_FABRIC_PROD_PACKING_BALE();
                            int maxBaleId = oBaleDetail.Max(p => p.BALE_ID);

                            obj.BALE_ID = maxBaleId + 1;
                            obj.BALE_CODE = txtBaleCode.Text.Trim();
                            obj.PACKING_ID = PACKING_ID;
                            obj.BALE_QTY = BaleQty;
                            obj.BALE_WIDTH = BaleWidth;
                            oBaleDetail.Add(obj);
                        }
                        // update existing bale detail here
                        else
                        {
                            var obj = (from data in oBaleDetail
                                       where data.BALE_ID == BALE_ID && data.PACKING_ID == PACKING_ID
                                       select data).ToList();
                            if (obj.Count > 0)
                            {
                                obj[0].BALE_CODE = txtBaleCode.Text.Trim();
                                obj[0].PACKING_ID = PACKING_ID;
                                obj[0].BALE_QTY = BaleQty;
                                obj[0].BALE_WIDTH = BaleWidth;
                            }
                        }
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("please enter Bale qty and width");
                }
                Session["dtBaleDetail"] = oBaleDetail;
                fillGridByDataTable();
            }
            else
            {
                Common.CommonFuction.ShowMessage("please enter Bale Code");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in page loading. See error log for detail"));
        }
    }

    private bool FindDuplicate(List<SaitexDM.Common.DataModel.TX_FABRIC_PROD_PACKING_BALE> oBaleDetails, int BALE_ID)
    {
        try
        {

            string BaleCode = txtBaleCode.Text;
            var obj = (from data in oBaleDetails
                       where data.PACKING_ID == PACKING_ID && data.BALE_CODE == BaleCode && data.BALE_ID != BALE_ID
                       orderby data.BALE_ID ascending
                       select data).ToList();
            if (obj.Count > 0)
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

    protected void btnCancelBaleDetail_Click(object sender, EventArgs e)
    {
        txtBaleQty.Text = string.Empty;
        txtBaleWidth.Text = string.Empty;
    }

    protected void grdBaleDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "delBale")
            {
                // GridViewRow grdBatchTrn = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                int BALE_ID = int.Parse(e.CommandArgument.ToString());

                oBaleDetail = (List<SaitexDM.Common.DataModel.TX_FABRIC_PROD_PACKING_BALE>)Session["dtBaleDetail"];
                oBaleDetail.RemoveAll(x => x.BALE_ID == BALE_ID);

                var oVar = (from data in oBaleDetail
                            where data.PACKING_ID == PACKING_ID
                            orderby data.BALE_ID ascending
                            select data).ToList();

                int iMax = oVar.Max(p => p.BALE_ID);

                Session["dtBaleDetail"] = oVar;
                fillGridByDataTable();
            }
            else if (e.CommandName == "editBale")
            {
                int BALE_ID = int.Parse(e.CommandArgument.ToString());

                oBaleDetail = (List<SaitexDM.Common.DataModel.TX_FABRIC_PROD_PACKING_BALE>)Session["dtBaleDetail"];

                var oVar = (from data in oBaleDetail
                            orderby data.BALE_ID ascending
                            select data).ToList();

                Session["dtBaleDetail"] = oVar;
                fillGridByDataTable();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Editing / Deleting Bale Detail.\r\nSee error log for detail."));
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

    }
}
