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
using Obout.ComboBox;

public partial class Module_OrderDevelopment_CustomerRequest_Pages_Sales_Machine_Batch : System.Web.UI.Page
{
    private string ORDER_NO = string.Empty;
    private string ORDER_TYPE = string.Empty;
    private string YARN_CODE = string.Empty;
    private string SHADE_CODE = string.Empty;
    private string SHADE_FAMILY = string.Empty;
    private string BUSINESS_TYPE = string.Empty;
    private string PRTY_ITEM_DESC = string.Empty;
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


            if (Request.QueryString["PRTY_ITEM_DESC"] != null)
            {
                PRTY_ITEM_DESC = Request.QueryString["PRTY_ITEM_DESC"].Trim();
                lblYarnDesc.Text = PRTY_ITEM_DESC;
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
            if (Request.QueryString["BUSINESS_TYPE"] != null)
            {
                BUSINESS_TYPE = Request.QueryString["BUSINESS_TYPE"].Trim();
               
            }

            if (Request.QueryString["lblMaxQTY"] != null)
            {
                double qty = 0;
                if (Request.QueryString["lblMaxQTY"] != null)
                {
                    double.TryParse(Request.QueryString["lblMaxQTY"].ToString(), out qty);
                    lblMaxQTY = qty;
                }
                
                

            }

           

            if (!IsPostBack)
            {
                lblRemaining.Text = lblMaxQTY.ToString();
                BindTrnNo();
                GetRQty();
                initialise();
                
                

            }



        }

        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nsee error log for detail."));
        }
    }


    private void initialise() 
    {

        
        ddlMachine.SelectedIndex = -1;
        txtBatch.Text = "";
        txtAss.Text = "";
       // txtTrnNo.Text = "";
        BindBOMGrid();
    
    }
    private void BindTrnNo() 
    {

        string TRNNO = SaitexBL.Interface.Method.OD_SHADE_FAMILY.GetNewTRNMacCap(oUserLoginDetail);
        txtTrnNo.Text = TRNNO;
    }


    protected void ddlMacCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        BindMachineCode();
    }



    private void BindMachineCode()
    {

        try
        {
            DataTable dt = SaitexBL.Interface.Method.OD_SHADE_FAMILY.GetMachineCode(YARN_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlMachine.Items.Clear();
                ddlMachine.DataSource = dt;
                ddlMachine.DataTextField = "MACHINE_CODE";
                ddlMachine.DataValueField = "MACHINE_CAPACITY";
                ddlMachine.DataBind();
                //ddlMachine.Items.Insert(0, new ListItem("------SELECT----", "0"));
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
            if (lblRemaining.Text == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:BindYRNQTY()", true);
            }
            else 
            {

                Common.CommonFuction.ShowMessage("Please Assign " + lblRemaining.Text + " Remaining Quantity to any Machine!");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in submitting delivery data.\r\nsee error log for detail."));
        }
    }


    

    private DataTable CreateSUBTRNTable()
    {
        try
        {
            DataTable dtMachine = new DataTable();
            
            dtMachine.Columns.Add("SR_NO", typeof(int));
            dtMachine.Columns.Add("ORDER_NO", typeof(string));
            dtMachine.Columns.Add("ORDER_TYPE", typeof(string));
            dtMachine.Columns.Add("YARN_CODE", typeof(string));
            dtMachine.Columns.Add("SHADE_CODE", typeof(string));
            dtMachine.Columns.Add("SHADE_FAMILY", typeof(string));
            dtMachine.Columns.Add("MACHINE", typeof(string));
            dtMachine.Columns.Add("BATCH", typeof(string));
            dtMachine.Columns.Add("QUANTITY", typeof(double));
            dtMachine.Columns.Add("BUSINESS_TYPE", typeof(string));
            dtMachine.Columns.Add("TRN_NO", typeof(Int32));
            dtMachine.Columns.Add("MACHINE_CAPACITY", typeof(double));
            dtMachine.Columns.Add("ASS_QTY", typeof(double));

            return dtMachine;
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
          
            DataTable dtMachine = new DataTable();
            if (Session["dtMachine"] != null)
            {

                dtMachine = (DataTable)Session["dtMachine"];
            }
            else
            {
                dtMachine = CreateSUBTRNTable();
            }
            
            
                int SR_NO = 0;
                if (ViewState["SR_NO"] != null)
                {
                    SR_NO = int.Parse(ViewState["SR_NO"].ToString());
                }


                if (SR_NO > 0)
                {
                    DataView dv = new DataView(dtMachine);
                    dv.RowFilter = "ORDER_NO='" + lblOrderNo.Text + "' and YARN_CODE='" + lblYarnCode.Text + "' and SHADE_CODE='" + lblShade.Text + "'and SHADE_FAMILY='" + lblShadeFamily.Text + "' and SR_NO=" + SR_NO+"' and MACHINE="+ddlMachine.SelectedText.ToString();
                    if (dv.Count > 0)
                    {
                        dv[0]["MACHINE"] = ddlMachine.SelectedValue.ToString();
                        dv[0]["BATCH"] = txtBatch.Text.ToString();
                        dtMachine.AcceptChanges();
                    }
                }
                else
                {



                    DataRow dr = dtMachine.NewRow();
                    dr["SR_NO"] = dtMachine.Rows.Count + 1;
                    dr["YARN_CODE"] = YARN_CODE;
                    dr["ORDER_NO"] = ORDER_NO;
                    dr["ORDER_TYPE"] = ORDER_TYPE;
                    dr["SHADE_CODE"] = SHADE_CODE;
                    dr["SHADE_FAMILY"] = SHADE_FAMILY;
                    dr["MACHINE"] = ddlMachine.SelectedText.ToString();
                    dr["BATCH"] = txtBatch.Text.ToString();
                    dr["QUANTITY"] = lblMaxQTY;
                    dr["BUSINESS_TYPE"] = BUSINESS_TYPE;
                    dr["TRN_NO"] = Int32.Parse(txtTrnNo.Text);
                    dr["MACHINE_CAPACITY"] = double.Parse(lblMacCapacity.Text);
                    dr["ASS_QTY"] = double.Parse(txtAss.Text);

                    dtMachine.Rows.Add(dr);

                }
                Session["dtMachine"] = dtMachine;
                int m = 0;
                for (int i = 0; i < dtMachine.Rows.Count; i++)
                {
                    if (int.Parse(dtMachine.Rows[i]["TRN_NO"].ToString()) > m)
                        m = int.Parse(dtMachine.Rows[i]["TRN_NO"].ToString());
                    
                }


                    txtTrnNo.Text = (m + 1).ToString();
                    GetRQty();
                    initialise();
            
           
          //  BindBOMGrid();



        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving  Machine Batch Detail Row.\r\nSee error log for detail."));

        }
    }



    private void GetRQty()
    {

        if (Session["dtMachine"] != null)
        {
            
           
            lblMacCapacity.Text = "";

            DataTable dt = (DataTable)Session["dtMachine"];
            double RQty = 0.0;

            DataView dv = dt.DefaultView;
            dv.RowFilter = "SHADE_CODE='" + SHADE_CODE + "'and YARN_CODE='"+YARN_CODE+"'";
            for (int i = 0; i < dv.Count; i++)
            {
                RQty += double.Parse(dv[i]["ASS_QTY"].ToString());

            }

            lblRemaining.Text =(lblMaxQTY- double.Parse(RQty.ToString())).ToString();
        }
    
    }

    private void BindBOMGrid()
    {
        try
        {
            DataTable dtMachine = null;
            if (Session["dtMachine"] != null)
            {
                dtMachine = (DataTable)Session["dtMachine"];
            }
            else
            {
                dtMachine = CreateSUBTRNTable();
            }

            DataView dv = new DataView(dtMachine);
            dv.RowFilter = " ORDER_NO='" + lblOrderNo.Text + "' and YARN_CODE='" + YARN_CODE + "' and SHADE_CODE='" + lblShade.Text + "'and SHADE_FAMILY='" + lblShadeFamily.Text + "' ";
            grdsub_trn.DataSource = dv;
            grdsub_trn.DataBind();

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
            DataTable dtMachine = null;
            if (Session["dtMachine"] != null)
            {
                dtMachine = (DataTable)Session["dtMachine"];
                
            }
            else
            {
                dtMachine = CreateSUBTRNTable();
            }
            if (dtMachine.Rows.Count == 1)
            {
                txtTrnNo.Text = dtMachine.Rows[0]["TRN_NO"].ToString();
                lblRemaining.Text = (double.Parse(lblRemaining.Text) +double.Parse(dtMachine.Rows[0]["ASS_QTY"].ToString())).ToString();
                dtMachine.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtMachine.Rows)
                {
                    int iUNIQUE_ID = int.Parse(dr["SR_NO"].ToString());
                    
                    if (iUNIQUE_ID == SR_NO)
                    {
                        txtTrnNo.Text = dr["TRN_NO"].ToString();
                        lblRemaining.Text=  ((double.Parse(lblRemaining.Text))+(double.Parse(dr["ASS_QTY"].ToString()))).ToString();
                        dtMachine.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtMachine.Rows)
                {
                    iCount = iCount + 1;
                    dr["SR_NO"] = iCount;
                }
            }
            dtMachine.AcceptChanges();
            Session["dtMachine"] = dtMachine;
        }
        catch
        {
            throw;
        }
    }




    protected void BtnBOMCancel_Click(object sender, EventArgs e)
    {
        initialise();
    }
    protected void txtBatch_TextChanged(object sender, EventArgs e)
    {
        if (double.Parse(ddlMachine.SelectedValue) >= double.Parse(lblRemaining.Text))
        {
            if (lblRemaining.Text != "0")
                txtAss.Text = lblRemaining.Text.Trim();
            else 
            {
                Common.CommonFuction.ShowMessage("Remaining Quantity is Zero");
            }
        }
        else
        {
            if ((double.Parse(lblMacCapacity.Text)) * (double.Parse(txtBatch.Text)) < lblMaxQTY)
            { txtAss.Text = ((double.Parse(lblMacCapacity.Text)) * (double.Parse(txtBatch.Text))).ToString(); }
            else 
            {
                Common.CommonFuction.ShowMessage("Assigned Quantity can not be more than Order Qty");
            
            }
        }

    }
    protected void ddlMachine_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        lblMacCapacity.Text = ddlMachine.SelectedValue.ToString();
    }
}
