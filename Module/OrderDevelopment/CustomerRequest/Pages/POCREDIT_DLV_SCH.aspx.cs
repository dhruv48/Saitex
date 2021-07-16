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

public partial class Module_OrderDevelopment_CustomerRequest_Pages_POCREDIT_DLV_SCH : System.Web.UI.Page
{
    private string ORDER_NO = string.Empty;
    private string ORDER_TYPE = string.Empty;
    private string YARN_CODE = string.Empty;
    private string SHADE_CODE = string.Empty;
    private string SHADE_FAMILY = string.Empty;
    private double lblMaxQTY = 0;
    
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {


        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (Request.QueryString["ORDER_NO"] != null)
            {
                ORDER_NO = Request.QueryString["ORDER_NO"].Trim();
                lblOrderNo.Text = ORDER_NO;
            }

            if (Request.QueryString["ORDER_TYPE"] != null)
            {
                ORDER_TYPE = Request.QueryString["ORDER_TYPE"].Trim();
                lblOrderType.Text = ORDER_TYPE;
            }

            if (Request.QueryString["YARN_CODE"] != null)
            {
                YARN_CODE = Request.QueryString["YARN_CODE"].Trim();
                lblYarnCode.Text = YARN_CODE;
            }

            if (Request.QueryString["SHADE_CODE"] != null)
            {
                SHADE_CODE = Request.QueryString["SHADE_CODE"].Trim();
                lblShade.Text = SHADE_CODE;
            }

            if (Request.QueryString["SHADE_FAMILY"] != null)
            {
                SHADE_FAMILY = Request.QueryString["SHADE_FAMILY"].Trim();
                lblShadeFamily.Text = SHADE_FAMILY;
            }

            if (Request.QueryString["lblMaxQTY"] != null)
            {
                double qty = 0;
                if (Request.QueryString["lblMaxQTY"] != null)
                {
                    double.TryParse(Request.QueryString["lblMaxQTY"].ToString(), out qty);
                }
                lblMaxQTY = qty;

            }

            if (!IsPostBack)
            {
                txtDeliveryDate.Text = System.DateTime.Now.Date.ToShortDateString();
                lblRemaining.Text = lblMaxQTY.ToString();
                txtQty.Text = lblMaxQTY.ToString();
                BindBOMGrid();

            }
           
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nsee error log for detail."));
        }


    }
    private DataTable CreateSUBTRNTable()
    {
        try
        {
            DataTable dtDelivery = new DataTable();
           // dtDelivery.Columns.Add("UNIQUE_ID", typeof(int));
            dtDelivery.Columns.Add("SR_NO", typeof(int));
            dtDelivery.Columns.Add("ORDER_NO", typeof(string));
            dtDelivery.Columns.Add("ORDER_TYPE", typeof(string));
            dtDelivery.Columns.Add("YARN_CODE", typeof(string));
            dtDelivery.Columns.Add("SHADE_CODE", typeof(string));
            dtDelivery.Columns.Add("SHADE_FAMILY", typeof(string));
            dtDelivery.Columns.Add("QUANTITY", typeof(double));
            dtDelivery.Columns.Add("DELIVERY_DATE", typeof(DateTime));
            return dtDelivery;
        }
        catch
        {
            throw;
        }
    }

    protected void BtnBOMSave_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime DATE_OF_MFG = System.DateTime.Now.Date;
            DateTime.TryParse(txtDeliveryDate.Text.Trim(), out DATE_OF_MFG);
            DataTable dtDelivery = new DataTable();
            if (Session["dtDelivery"] != null)
            {

                dtDelivery = (DataTable)Session["dtDelivery"];
            }
            else
            {
                dtDelivery = CreateSUBTRNTable();
            }
            if (DATE_OF_MFG.Date < DateTime.Now.Date)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Delivery date can not be before today date!!! ');", true);
                return;
            }
            if (CheckQTYtotal())
            {
                int SR_NO = 0;
                if (ViewState["SR_NO"] != null)
                {
                    SR_NO = int.Parse(ViewState["SR_NO"].ToString());
                }


                if (SR_NO > 0)
                {
                    DataView dv = new DataView(dtDelivery);
                    dv.RowFilter = "ORDER_NO='" + lblOrderNo.Text + "' and YARN_CODE='" + lblYarnCode.Text + "' and SHADE_CODE='" + lblShade.Text + "'and SHADE_FAMILY='" + lblShadeFamily.Text + "' and SR_NO=" + SR_NO;
                    if (dv.Count > 0)
                    {
                        double QTY = 0f;
                        double.TryParse(txtQty.Text.Trim(), out QTY);
                        dv[0]["QUANTITY"] = QTY;

                        dv[0]["DELIVERY_DATE"] = DATE_OF_MFG.ToShortDateString();
                        dtDelivery.AcceptChanges();
                    }
                }
                else
                {


                    DataRow dr = dtDelivery.NewRow();
                    dr["SR_NO"] = dtDelivery.Rows.Count + 1;
                    dr["YARN_CODE"] = YARN_CODE;
                    dr["ORDER_NO"] = ORDER_NO;
                    dr["ORDER_TYPE"] = ORDER_TYPE;
                    dr["SHADE_CODE"] = SHADE_CODE;
                    dr["SHADE_FAMILY"] = SHADE_FAMILY;
                    double QTY = 0f;
                    double.TryParse(txtQty.Text.Trim(), out QTY);
                    dr["QUANTITY"] = QTY;
                    dr["DELIVERY_DATE"] = DATE_OF_MFG.ToShortDateString();
                    dtDelivery.Rows.Add(dr);

                }
                Session["dtDelivery"] = dtDelivery;
                RefresBOMRow();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Quantity can not be greater than Maximum Quantity!!! ');", true);
            }
            BindBOMGrid();



        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving Delivery Date Detail Row.\r\nSee error log for detail."));

        }
    }


    protected bool CheckQTYtotal()
    {
        try
        {

            double currentpage = 0;
            double currentQty = 0;
            double Qty = 0;
            double.TryParse(txtQty.Text, out Qty);
            if (grdsub_trn.Rows.Count > 0)
            {
                for (int i = 0; i < grdsub_trn.Rows.Count; i++)
                {
                    Label txtQTY = grdsub_trn.Rows[i].FindControl("txtQTY") as Label;
                    double.TryParse(txtQTY.Text, out currentQty);
                    currentpage += currentQty;
                }

            }
            double alltotal = currentpage + Qty;

             lblRemaining.Text = (lblMaxQTY - alltotal).ToString();

            if (alltotal > lblMaxQTY)
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
    private void BindBOMGrid()
    {
        try
        {
            DataTable dtDelivery = null;
            if (Session["dtDelivery"] != null)
            {
                dtDelivery = (DataTable)Session["dtDelivery"];
            }
            else
            {
                dtDelivery = CreateSUBTRNTable();
            }

            DataView dv = new DataView(dtDelivery);
            dv.RowFilter = " ORDER_NO='" + lblOrderNo.Text + "' and YARN_CODE='" + YARN_CODE + "' and SHADE_CODE='"+lblShade.Text+"'and SHADE_FAMILY='"+lblShadeFamily.Text+"' ";
            grdsub_trn.DataSource = dv;
            grdsub_trn.DataBind();

        }
        catch
        {
            throw;
        }
    }


    private void RefresBOMRow()
    {
        try
        {
            txtQty.Text = string.Empty;
            txtDeliveryDate.Text = string.Empty;
            ViewState["SR_NO"] = null;
        }
        catch
        {
            throw;
        }
    }




    protected void grdSub_trnArticleDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int SR_NO = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "BOMDelete")
            {
                DeleteBOMRow(SR_NO);
                BindBOMGrid();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Sub Tran Grid RowCommand Selection.\r\nSee error log for detail."));
        }
    }
    private void DeleteBOMRow(int SR_NO)
    {
        try
        {
            DataTable dtDelivery = null;
            if (Session["dtDelivery"] != null)
            {
                dtDelivery = (DataTable)Session["dtDelivery"];
            }
            else
            {
                dtDelivery = CreateSUBTRNTable();
            }
            if (dtDelivery.Rows.Count == 1)
            {
                dtDelivery.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtDelivery.Rows)
                {
                    int iUNIQUE_ID = int.Parse(dr["SR_NO"].ToString());
                    if (iUNIQUE_ID == SR_NO)
                    {
                        dtDelivery.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtDelivery.Rows)
                {
                    iCount = iCount + 1;
                    dr["SR_NO"] = iCount;
                }
            }
            dtDelivery.AcceptChanges();
            Session["dtDelivery"] = dtDelivery;
        }
        catch
        {
            throw;
        }
    }


    protected void BtnBOMCancel_Click(object sender, EventArgs e)
    {
        RefresBOMRow();

    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            double totalQty = 0;
            for (int i = 0; i < grdsub_trn.Rows.Count; i++)
            {
                Label txtQTY = grdsub_trn.Rows[i].FindControl("txtQTY") as Label;
                totalQty += double.Parse(txtQTY.Text);
            }

            if (lblMaxQTY == totalQty)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:BindYRNQTY()", true);
            }
            else 
            {
                Common.CommonFuction.ShowMessage(" Please enter the rest " + (lblMaxQTY - totalQty) + " Quantity Delivery Date");
              
            }

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in submitting delivery data.\r\nsee error log for detail."));
        }
    }

}
