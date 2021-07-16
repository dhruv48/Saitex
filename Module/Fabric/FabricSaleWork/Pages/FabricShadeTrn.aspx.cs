using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Module_Fabric_FabricSaleWork_Pages_FabricShadeTrn : System.Web.UI.Page
{
    decimal grdTotal = 0;
    decimal  ReqTotal=0;
    List<SaitexDM.Common.DataModel.TX_FABRIC_DESIGN_SHADE_TRN> dtShadeTrn;
    private static DataTable dtShadeCodeTrn;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {

                if (dtShadeCodeTrn == null || dtShadeCodeTrn.Rows.Count == 0)
                {
                    dtShadeCodeTrn = SaitexBL.Interface.Method.OD_SHADE_FAMILY.GetShadeCodeALL();
                }

                if (Request.QueryString["SHADE_CODE"] != null && Request.QueryString["SHADE_CODE"].ToString() != string.Empty)
                {
                    lblSstrnHead.Text = Request.QueryString["SHADE_CODE"].ToString();
                }

                if (Request.QueryString["IsWarp"] != null)
                {
                    if (bool.Parse(Request.QueryString["IsWarp"].ToString()))
                    {
                        lbltrnWarpweft.Text = "Warp";
                    }
                    else
                    {
                        lbltrnWarpweft.Text = "Weft";
                    }
                }
                if (Request.QueryString["TextBoxId"] != null && Request.QueryString["TextBoxId"].ToString() != string.Empty)
                {
                    hf1.Value = Request.QueryString["TextBoxId"].ToString();
                }
                BindGridData();
                BindShadeCode();
            }
        }
        catch
        {
        }
    }
    private void BindShadeCode()
    {
        try
        {
            if (dtShadeCodeTrn != null && dtShadeCodeTrn.Rows.Count > 0)
            {
                ddlTrnShade.Items.Clear();
                ddlTrnShade.Items.Insert(0, new ListItem("Select", ""));
                ddlTrnShade.DataSource = dtShadeCodeTrn;
                ddlTrnShade.DataTextField = "SHADE_CODE";
                ddlTrnShade.DataValueField = "SHADE_CODE";
                ddlTrnShade.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }
    protected void txtShTrnCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);

            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                txtShTrnCode.Items.Clear();

                txtShTrnCode.DataSource = data;
                txtShTrnCode.DataBind();
            }

            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Yarn loading.\r\nSee error log for detail."));
            //   lblMode.Text = ex.ToString();
        }
    }
    protected DataTable GetItems(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT YARN_CODE, YARN_DESC, YARN_TYPE, (YARN_CODE||'@'||YARN_DESC) as YARN_DATA FROM YRN_MST WHERE YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE) asd WHERE   ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += " AND YARN_code NOT IN (SELECT YARN_CODE FROM (SELECT YARN_CODE, YARN_DESC FROM YRN_MST WHERE YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE) asd WHERE ROWNUM <= " + startOffset + ") ";
            }

            string SortExpression = " ORDER BY YARN_CODE";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }
    protected int GetItemsCount(string text)
    {
        string CommandText = " SELECT   *  FROM   (  SELECT   YARN_CODE, YARN_DESC, YARN_TYPE, (YARN_CODE||'@'||YARN_DESC) as YARN_DATA FROM   YRN_MST WHERE      YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY   YARN_CODE) asd WHERE YARN_TYPE <> 'SEWING THREAD'";
        string WhereClause = " ";
        string SortExpression = " ORDER BY YARN_CODE ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        return data.Rows.Count;
    }
    protected void txtShTrnCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            string YARN_DATA = txtShTrnCode.SelectedValue.Trim();

            string[] array = YARN_DATA.Split('@');
            string YarnCode = array[0].ToString();
            string Yarndesc = array[0].ToString();
            txttrnYarnCode.Text = YarnCode;
            txttrnyarnDesc.Text = Yarndesc;
            if (FindShadeTrnDuplicacy())
            {
                txtShTrnCode.SelectedIndex = -1;
                txttrnYarnCode.Text = string.Empty;
                txttrnyarnDesc.Text = string.Empty;
                Common.CommonFuction.ShowMessage("This Code with selected Shade already added.");
            }
        }
        catch (Exception ex)
        {
        }
    }
    private bool FindShadeTrnDuplicacy()
    {
        try
        {
            bool bResult = false;
            if (Session["dtShadeTrn"] != null)
            {
                string SHADE_CODE = lblSstrnHead.Text.Trim();
                string YarnShadeCode = ddlTrnShade.SelectedValue.Trim();
                string yarnCode = txttrnYarnCode.Text;
                dtShadeTrn = (List<SaitexDM.Common.DataModel.TX_FABRIC_DESIGN_SHADE_TRN>)Session["dtShadeTrn"];

                var oVar = (from data in dtShadeTrn
                            where data.SHADE_CODE == SHADE_CODE && data.YARN_CODE == yarnCode && data.YARN_SHADE_CODE == YarnShadeCode && data.ROW_STATUS != SaitexDM.Common.DataModel.ROWSTATE.Delete && data.ROW_STATUS != SaitexDM.Common.DataModel.ROWSTATE.EditMode && data.WARP_WEFT == lbltrnWarpweft.Text.Trim()
                            select data).ToList();

                if (oVar.Count > 0)
                {
                    bResult = true;
                }
                else
                {
                    bResult = false;
                }
            }
            return bResult;
        }
        catch
        {
            throw;
        }
    }
    protected void ddlTrnShade_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (FindShadeTrnDuplicacy())
            {
                ddlTrnShade.SelectedIndex = -1;
                Common.CommonFuction.ShowMessage("This Shade with selected Code already added.");
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btntrnSave_Click(object sender, EventArgs e)
    {
        double ReqQty = 0, Shrinkage = 0, ShrinkageVal = 0, Wastage = 0, WastageVal = 0, Rejection = 0, RejectionVal = 0, qty = 0;
        CalculateQty(out  ReqQty, out  Shrinkage, out  ShrinkageVal, out  Wastage, out  WastageVal, out  Rejection, out  RejectionVal, out  qty);

        string msg = string.Empty;
        if (ValidateShadeTrnRow(out msg))
        {
            if (FindShadeTrnDuplicacy())
            {
                ddlTrnShade.SelectedIndex = -1;
                txtShTrnCode.SelectedIndex = -1;
                txttrnYarnCode.Text = string.Empty;
                txttrnyarnDesc.Text = string.Empty;
                Common.CommonFuction.ShowMessage("This Shade with selected Code already added.");
            }
            else
            {
                SaitexDM.Common.DataModel.TX_FABRIC_DESIGN_SHADE_TRN oTX_FABRIC_DESIGN_SHADE_TRN = new SaitexDM.Common.DataModel.TX_FABRIC_DESIGN_SHADE_TRN();

                oTX_FABRIC_DESIGN_SHADE_TRN.SHADE_CODE = ddlTrnShade.Text;
                int iCount = 0;

                if (Session["dtShadeTrn"] != null)
                {
                    dtShadeTrn = (List<SaitexDM.Common.DataModel.TX_FABRIC_DESIGN_SHADE_TRN>)Session["dtShadeTrn"];
                }

                if (dtShadeTrn == null)
                {
                    dtShadeTrn = new List<SaitexDM.Common.DataModel.TX_FABRIC_DESIGN_SHADE_TRN>();
                }

                var oSortaa = (from data in dtShadeTrn
                               where data.SHADE_CODE == lblSstrnHead.Text && data.WARP_WEFT == lbltrnWarpweft.Text.Trim()
                               select data).ToList();

                var oSort = (from data in oSortaa
                             where data.YARN_CODE == oTX_FABRIC_DESIGN_SHADE_TRN.YARN_CODE && data.YARN_SHADE_CODE == oTX_FABRIC_DESIGN_SHADE_TRN.YARN_SHADE_CODE
                             select data).ToList();

                iCount = oSort.Count;

                if (iCount == 0)
                {
                    oTX_FABRIC_DESIGN_SHADE_TRN.SHADE_CODE = lblSstrnHead.Text;
                    oTX_FABRIC_DESIGN_SHADE_TRN.WARP_WEFT = lbltrnWarpweft.Text;
                    oTX_FABRIC_DESIGN_SHADE_TRN.SEQUENCE_NO = oSortaa.Count + 1;
                    oTX_FABRIC_DESIGN_SHADE_TRN.YARN_CODE = txttrnYarnCode.Text;
                    int count = 0;
                    int.TryParse(txttrnyarncount.Text, out count);
                    oTX_FABRIC_DESIGN_SHADE_TRN.COUNT = count;

                    oTX_FABRIC_DESIGN_SHADE_TRN.YARN_SHADE_CODE = ddlTrnShade.SelectedValue.Trim();

                    oTX_FABRIC_DESIGN_SHADE_TRN.YARN_SHADE_RGB = txtTrnShadeRGB.Text;
                    double yarnstd = 0;
                    double.TryParse(txttrnyarnstd.Text, out yarnstd);
                    oTX_FABRIC_DESIGN_SHADE_TRN.YARN_STD = yarnstd;

                    oTX_FABRIC_DESIGN_SHADE_TRN.REQ_QTY = ReqQty;
                    oTX_FABRIC_DESIGN_SHADE_TRN.SHRINKAGE = Shrinkage;
                    oTX_FABRIC_DESIGN_SHADE_TRN.WASTAGE = Wastage;
                    oTX_FABRIC_DESIGN_SHADE_TRN.REJECTION = Rejection;
                    oTX_FABRIC_DESIGN_SHADE_TRN.QTY = qty;

                    oTX_FABRIC_DESIGN_SHADE_TRN.ROW_STATUS = SaitexDM.Common.DataModel.ROWSTATE.Insert;
                    oTX_FABRIC_DESIGN_SHADE_TRN.TUSER = oUserLoginDetail.UserCode;
                    oTX_FABRIC_DESIGN_SHADE_TRN.TDATE = System.DateTime.Now.Date;
                    dtShadeTrn.Add(oTX_FABRIC_DESIGN_SHADE_TRN);
                }
                else
                {
                    foreach (SaitexDM.Common.DataModel.TX_FABRIC_DESIGN_SHADE_TRN olist in dtShadeTrn)
                    {
                        if (olist.SHADE_CODE == oTX_FABRIC_DESIGN_SHADE_TRN.SHADE_CODE)
                        {

                            olist.SHADE_CODE = lblSstrnHead.Text;
                            olist.WARP_WEFT = lbltrnWarpweft.Text;
                            olist.YARN_CODE = txttrnYarnCode.Text;
                            int count = 0;
                            int.TryParse(txttrnyarncount.Text, out count);
                            olist.COUNT = count;

                            olist.YARN_SHADE_CODE = ddlTrnShade.SelectedValue.Trim();

                            olist.YARN_SHADE_RGB = txtTrnShadeRGB.Text;
                            double yarnstd = 0;
                            double.TryParse(txttrnyarnstd.Text, out yarnstd);
                            olist.YARN_STD = yarnstd;

                            olist.REQ_QTY = ReqQty;
                            olist.SHRINKAGE = Shrinkage;
                            olist.WASTAGE = Wastage;
                            olist.REJECTION = Rejection;
                            olist.QTY = qty;

                            olist.TUSER = oUserLoginDetail.UserCode;
                            olist.TDATE = System.DateTime.Now.Date;
                            olist.ROW_STATUS = SaitexDM.Common.DataModel.ROWSTATE.Update;

                        }
                    }
                }

                Session["dtShadeTrn"] = dtShadeTrn;
                BindGridData();

                refreshShadeTrnRow();
            }
        }

    }
    private void CalculateQty(out double ReqQty, out double Shrinkage, out double ShrinkageVal, out double Wastage, out double WastageVal, out double Rejection, out double RejectionVal, out double qty)
    {
        try
        {
            ReqQty = 0;
            Shrinkage = 0;
            ShrinkageVal = 0;
            Wastage = 0;
            WastageVal = 0;
            Rejection = 0;
            RejectionVal = 0;
            qty = 0;
            double.TryParse(txttrnReqQty.Text, out ReqQty);
            double.TryParse(txttrnsrink.Text, out Shrinkage);
            double.TryParse(txttrnWastage.Text, out Wastage);
            double.TryParse(txttrnRejection.Text, out Rejection);

            ShrinkageVal = (ReqQty * Shrinkage) / 100;
            WastageVal = (ReqQty * Wastage) / 100;
            RejectionVal = (ReqQty * Rejection) / 100;
            qty = ReqQty + ShrinkageVal + WastageVal + RejectionVal;

            txttrnReqQty.Text = ReqQty.ToString();
            txttrnsrink.Text = Shrinkage.ToString();
            txttrnWastage.Text = Wastage.ToString();
            txttrnRejection.Text = Rejection.ToString();
            txttrnQty.Text = qty.ToString();
        }
        catch
        {
            throw;
        }
    }
    private void BindGridData()
    {
        try
        {
            dtShadeTrn = (List<SaitexDM.Common.DataModel.TX_FABRIC_DESIGN_SHADE_TRN>)Session["dtShadeTrn"];

            if (dtShadeTrn != null && dtShadeTrn.Count > 0)
            {

                var oVar = (from data in dtShadeTrn
                            where data.SHADE_CODE == lblSstrnHead.Text.Trim() && data.ROW_STATUS != SaitexDM.Common.DataModel.ROWSTATE.Delete && data.WARP_WEFT == lbltrnWarpweft.Text.Trim()
                            select data).ToList();

                grdshadetrn.DataSource = oVar;
                grdshadetrn.DataBind();
            }
            //else
            //{
            //    Common.CommonFuction.ShowMessage("No yarn selected for the shade.");
            //}
        }
        catch
        {
            throw;
        }
    }
    private bool ValidateShadeTrnRow(out string msg)
    {
        try
        {
            int iCount = 0;
            int iCountAll = 0;
            bool bResult = false;
            msg = string.Empty;

            iCountAll += 1;
            if (txttrnYarnCode.Text != string.Empty)
            {
                iCount += 1;
            }
            else
            {
                msg += "Please Select Yarn Code";
            }

            iCountAll += 1;
            if (ddlTrnShade.Text != string.Empty)
            {
                iCount += 1;
            }
            else
            {
                msg += "Please Select Shade Code";
            }

            iCountAll += 1;
            double Qty = 0;
            if (double.TryParse(txttrnQty.Text, out Qty))
            {
                iCount += 1;
            }
            else
            {
                msg += "Please enter valid Qty";
            }

            iCountAll += 1;
            Qty = 0;
            if (double.TryParse(txttrnsrink.Text, out Qty))
            {
                iCount += 1;
            }
            else
            {
                msg += "Please enter valid Shrinkage";
            }

            if (iCount == iCountAll)
            {
                bResult = true;
            }
            return bResult;
        }
        catch
        {
            throw;
        }
    }
    protected void btntrncancel_Click(object sender, EventArgs e)
    {
        refreshShadeTrnRow();
    }
    private void refreshShadeTrnRow()
    {
        try
        {
            txtShTrnCode.SelectedIndex = -1;
            txttrnQty.Text = string.Empty;
            txtTrnRGBColor.Text = string.Empty;
            txtTrnShadeRGB.Text = string.Empty;
            txttrnsrink.Text = string.Empty;
            txttrnYarnCode.Text = string.Empty;
            txttrnyarncount.Text = string.Empty;
            txttrnyarnDesc.Text = string.Empty;
            txttrnyarnstd.Text = string.Empty;
            ddlTrnShade.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
        }
    }
    protected void grdshadetrn_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int SEQUENCE_NO = int.Parse(e.CommandArgument.ToString());
            if (Session["dtShadeTrn"] != null)
                dtShadeTrn = (List<SaitexDM.Common.DataModel.TX_FABRIC_DESIGN_SHADE_TRN>)Session["dtShadeTrn"];
            var oVar = (from data in dtShadeTrn
                        where data.SEQUENCE_NO == SEQUENCE_NO
                        select data).ToList();

            if (e.CommandName == "trnDelete")
            {
                if (oVar.Count > 0)
                {
                    oVar[0].ROW_STATUS = SaitexDM.Common.DataModel.ROWSTATE.Delete;
                }
                Session["dtShadeTrn"] = dtShadeTrn;
            }
            else if (e.CommandName == "trnEdit")
            {
                if (oVar.Count > 0)
                {
                    txtShTrnCode.Enabled = false;
                    ddlTrnShade.Enabled = false;
                    txttrnYarnCode.Text = oVar[0].YARN_CODE.ToString();
                    //txttrnyarnDesc.Text = oVar[0].YARN_DESC.ToString();
                    ddlTrnShade.SelectedIndex = ddlTrnShade.Items.IndexOf(ddlTrnShade.Items.FindByValue(oVar[0].YARN_SHADE_CODE.ToString()));

                    txtTrnShadeRGB.Text = oVar[0].YARN_SHADE_RGB.ToString();
                    txtTrnRGBColor.BackColor = System.Drawing.Color.FromArgb(int.Parse(oVar[0].YARN_SHADE_RGB));

                    txttrnyarncount.Text = oVar[0].COUNT.ToString();
                    txttrnyarnstd.Text = oVar[0].YARN_STD.ToString();
                    txttrnReqQty.Text = oVar[0].REQ_QTY.ToString();
                    txttrnsrink.Text = oVar[0].SHRINKAGE.ToString();
                    txttrnWastage.Text = oVar[0].WASTAGE.ToString();
                    txttrnRejection.Text = oVar[0].REJECTION.ToString();
                    txttrnQty.Text = oVar[0].QTY.ToString();

                    oVar[0].ROW_STATUS = SaitexDM.Common.DataModel.ROWSTATE.EditMode;

                    ViewState["SEQUENCE_NO"] = SEQUENCE_NO;
                }
            }
            BindGridData();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Editing Process.\r\nSee error log for detail."));
        }
    }
    protected void txtTrnShadeRGB_TextChanged(object sender, EventArgs e)
    {
        int argb = 0;
        int.TryParse(txtTrnShadeRGB.Text, out argb);
        txtTrnRGBColor.BackColor = System.Drawing.Color.FromArgb(argb);
        txtTrnShadeRGB.Text = argb.ToString();

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        string SHADE_CODE = lblSstrnHead.Text.Trim();
        string YarnShadeCode = ddlTrnShade.SelectedValue.Trim();
        string yarnCode = txttrnYarnCode.Text;
        dtShadeTrn = (List<SaitexDM.Common.DataModel.TX_FABRIC_DESIGN_SHADE_TRN>)Session["dtShadeTrn"];

        var oVar = (from data in dtShadeTrn
                    where data.SHADE_CODE == SHADE_CODE && data.YARN_CODE == yarnCode && data.YARN_SHADE_CODE == YarnShadeCode && data.ROW_STATUS != SaitexDM.Common.DataModel.ROWSTATE.Delete
                    select data).ToList();
        int RowCount = oVar.Count;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:BindRate('" + RowCount + "','" + hf1.Value + "')", true);

    }
    protected void grdshadetrn_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Code to set RGB color In Grid
                LinkButton lbtnShadeRGBTrn = (LinkButton)e.Row.FindControl("lbtnShadeRGBTrn");
                TextBox txtShadeRGBColorTrn = (TextBox)e.Row.FindControl("txtShadeRGBColorTrn");
                int argb = 0;
                int.TryParse(lbtnShadeRGBTrn.Text, out argb);
                txtShadeRGBColorTrn.BackColor = System.Drawing.Color.FromArgb(argb);
                lbtnShadeRGBTrn.Text = argb.ToString();

                decimal rowTotal = Convert.ToDecimal
                                       (DataBinder.Eval(e.Row.DataItem, "QTY"));
                grdTotal = grdTotal + rowTotal;

                decimal reqTotal = Convert.ToDecimal
                                      (DataBinder.Eval(e.Row.DataItem, "REQ_QTY"));
                ReqTotal = ReqTotal + reqTotal;

            }

          
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl = (Label)e.Row.FindControl("lblTotal");
                Label lbl1 = (Label)e.Row.FindControl("reqlblTotal");
              
                lbl.Text = grdTotal.ToString();
                lbl1.Text = ReqTotal.ToString();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Setting Image"));
        }
    }
    protected void btnClose_Click1(object sender, EventArgs e)
    {
        string SHADE_CODE = lblSstrnHead.Text.Trim();
        string YarnShadeCode = ddlTrnShade.SelectedValue.Trim();
        string yarnCode = txttrnYarnCode.Text;
        dtShadeTrn = (List<SaitexDM.Common.DataModel.TX_FABRIC_DESIGN_SHADE_TRN>)Session["dtShadeTrn"];

        var oVar = (from data in dtShadeTrn
                    where data.SHADE_CODE == SHADE_CODE && data.WARP_WEFT == lbltrnWarpweft.Text.Trim() && data.ROW_STATUS != SaitexDM.Common.DataModel.ROWSTATE.Delete
                    select data).ToList();
        int RowCount = oVar.Count;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:BindRate('" + RowCount + "','" + hf1.Value + "')", true);

    }
    protected void txttrnReqQty_TextChanged(object sender, EventArgs e)
    {
        double ReqQty = 0, Shrinkage = 0, ShrinkageVal = 0, Wastage = 0, WastageVal = 0, Rejection = 0, RejectionVal = 0, qty = 0;
        CalculateQty(out  ReqQty, out  Shrinkage, out  ShrinkageVal, out  Wastage, out  WastageVal, out  Rejection, out  RejectionVal, out  qty);

    }
    protected void txttrnsrink_TextChanged(object sender, EventArgs e)
    {
        double ReqQty = 0, Shrinkage = 0, ShrinkageVal = 0, Wastage = 0, WastageVal = 0, Rejection = 0, RejectionVal = 0, qty = 0;
        CalculateQty(out  ReqQty, out  Shrinkage, out  ShrinkageVal, out  Wastage, out  WastageVal, out  Rejection, out  RejectionVal, out  qty);

    }
    protected void txttrnWastage_TextChanged(object sender, EventArgs e)
    {
        double ReqQty = 0, Shrinkage = 0, ShrinkageVal = 0, Wastage = 0, WastageVal = 0, Rejection = 0, RejectionVal = 0, qty = 0;
        CalculateQty(out  ReqQty, out  Shrinkage, out  ShrinkageVal, out  Wastage, out  WastageVal, out  Rejection, out  RejectionVal, out  qty);

    }
    protected void txttrnRejection_TextChanged(object sender, EventArgs e)
    {
        double ReqQty = 0, Shrinkage = 0, ShrinkageVal = 0, Wastage = 0, WastageVal = 0, Rejection = 0, RejectionVal = 0, qty = 0;
        CalculateQty(out  ReqQty, out  Shrinkage, out  ShrinkageVal, out  Wastage, out  WastageVal, out  Rejection, out  RejectionVal, out  qty);

    }
   
}
