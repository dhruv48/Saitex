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

public partial class Module_Inventory_Pages_ITEM_OP_BAL_LOT_DETAILS : System.Web.UI.Page
{
    
    private  string PI_TYPE = string.Empty;
    private  string ITEM_CODE = string.Empty;

   
    private int PO_NUMB = 999998;
    private string PO_TYPE = "OPI";
    private string PO_COMP_CODE = "C99999";
    private string PO_BRANCH = "B99999";
    private  string txtboxClientid = string.Empty;
    private  string txtNoOfUnit = string.Empty;
    private  string txtWeightOfUnit = string.Empty;
    
    private string PRTY_CODE = string.Empty;
    private string BILL_NUMB = string.Empty;
    private string BILL_DATE = string.Empty;
    private string STORE = string.Empty;
    private string LOCATION = string.Empty;
    private string UOM = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (Request.QueryString["ITEM_CODE"] != null)
            {
                ITEM_CODE = Request.QueryString["ITEM_CODE"].Trim();
            }
            if (Request.QueryString["PRTY_CODE"] != null)
            {
                PRTY_CODE = Request.QueryString["PRTY_CODE"].Trim().ToString();
            }
            if (Request.QueryString["BILL_NUMB"] != null)
            {
                BILL_NUMB = Request.QueryString["BILL_NUMB"].Trim().ToString();
            }
            if (Request.QueryString["BILL_DATE"] != null)
            {
                BILL_DATE = Request.QueryString["BILL_DATE"].Trim().ToString();
            }
            if (Request.QueryString["STORE"] != null)
            {
                STORE = Request.QueryString["STORE"].Trim().ToString();
            }
            if (Request.QueryString["LOCATION"] != null)
            {
                LOCATION = Request.QueryString["LOCATION"].Trim().ToString();
            }
            if (Request.QueryString["PO_NUMB"] != null)
            {
                PO_NUMB = Convert.ToInt32(Request.QueryString["PO_NUMB"].Trim().ToString());
            }
            if (Request.QueryString["PO_TYPE"] != null)
            {
                PO_TYPE = Request.QueryString["PO_TYPE"].Trim();
            }
            if (Request.QueryString["PO_COMP_CODE"] != null)
            {
                PO_COMP_CODE = Request.QueryString["PO_COMP_CODE"].Trim();
            }
            if (Request.QueryString["PO_BRANCH"] != null)
            {
                PO_BRANCH = Request.QueryString["PO_BRANCH"].Trim();
            }
            if (Request.QueryString["txtQTY"] != null)
            {
                txtboxClientid = Request.QueryString["txtQTY"].Trim();
            }

            if (Request.QueryString["txtNoOfUnit"] != null)
            {
                txtNoOfUnit = Request.QueryString["txtNoOfUnit"].Trim();
            }

            if (Request.QueryString["txtWeightOfUnit"] != null)
            {
                txtWeightOfUnit = Request.QueryString["txtWeightOfUnit"].Trim();
            }

            if (Request.QueryString["UOM"] != null)
            {
                UOM = Request.QueryString["UOM"].Trim();
            }
            
            if (!IsPostBack)
            {
                BindInital();
                txtDofMfd.Text = System.DateTime.Now.Date.ToShortDateString();  
               
                BindBOMGrid();           
               
            }
            
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nsee error log for detail."));
        }
    }

    private void BindInital()
    {
        try
        {
            txtLotNo.Text = "NA";
            txtGrade.Text = "NA";
            txtNoofUnit.Text = "0";
            txtWeightofUnit.Text = "0";
            txtDofMfd.Text = System.DateTime.Now.Date.ToShortDateString();
        }
        catch
        {

        }


    }

    private DataTable CreateSUBTRNTable()
    {
        try
        {
           DataTable  dtTRN_SUB = new DataTable();
            dtTRN_SUB.Columns.Add("UNIQUE_ID", typeof(int));
            dtTRN_SUB.Columns.Add("ITEM_CODE", typeof(string));
            dtTRN_SUB.Columns.Add("TRN_QTY", typeof(double));
            dtTRN_SUB.Columns.Add("UOM", typeof(string));
            dtTRN_SUB.Columns.Add("MATERIAL_STATUS", typeof(string));
            dtTRN_SUB.Columns.Add("GRADE", typeof(string));
            dtTRN_SUB.Columns.Add("LOT_NO", typeof(string));
            dtTRN_SUB.Columns.Add("DATE_OF_MFG", typeof(DateTime));
            dtTRN_SUB.Columns.Add("PO_NUMB", typeof(int));
            dtTRN_SUB.Columns.Add("PO_TYPE", typeof(string));
            dtTRN_SUB.Columns.Add("PO_COMP_CODE", typeof(string));
            dtTRN_SUB.Columns.Add("PO_BRANCH", typeof(string));
            dtTRN_SUB.Columns.Add("NO_OF_UNIT", typeof(double));
            dtTRN_SUB.Columns.Add("UOM_OF_UNIT", typeof(string));
            dtTRN_SUB.Columns.Add("WEIGHT_OF_UNIT", typeof(double));
            dtTRN_SUB.Columns.Add("PI_NO", typeof(string));
            dtTRN_SUB.Columns.Add("PRTY_CODE", typeof(string));
            dtTRN_SUB.Columns.Add("BILL_NUMB", typeof(string));
            dtTRN_SUB.Columns.Add("BILL_DATE", typeof(string));
            dtTRN_SUB.Columns.Add("STORE", typeof(string));
            dtTRN_SUB.Columns.Add("LOCATION", typeof(string));
            return dtTRN_SUB;
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
            DataTable dtTRN_SUB = null;
            if (Session["dtTRN_SUB"] != null)
            {
                dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];
            }
            else             
            {
               dtTRN_SUB = CreateSUBTRNTable();
            }            

            DataView dv = new DataView(dtTRN_SUB);
            dv.RowFilter = "ITEM_CODE='" + ITEM_CODE + "' AND PRTY_CODE='" + PRTY_CODE + "'  AND PRTY_CODE='" + PRTY_CODE + "' AND BILL_NUMB='" + BILL_NUMB + "' AND LOCATION='" + LOCATION  + "' AND STORE='" + STORE + "'";
            grdsub_trn.DataSource = dv;
            grdsub_trn.DataBind();

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
            DataTable dtTRN_SUB = new DataTable();
            if (Session["dtTRN_SUB"] != null)
            {

                dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];
            }
            else
            {
                dtTRN_SUB = CreateSUBTRNTable();
            }   

            
                int UNIQUE_ID = 0;
                if (ViewState["UNIQUE_ID"] != null)
                {
                    UNIQUE_ID = int.Parse(ViewState["UNIQUE_ID"].ToString());
                }
                bool bb = SearchInBOMgrid(txtLotNo.Text, UNIQUE_ID);
                if (!bb)
                {
                    DateTime DATE_OF_MFG = System.DateTime.Now.Date;
                    if (UNIQUE_ID > 0)
                    {
                        DataView dv = new DataView(dtTRN_SUB);
                        dv.RowFilter = "ITEM_CODE='" + ITEM_CODE + "' AND PRTY_CODE='" + PRTY_CODE + "'  AND PRTY_CODE='" + PRTY_CODE + "' AND BILL_NUMB='" + BILL_NUMB + "' AND LOCATION='" + LOCATION + "' AND STORE='" + STORE + "' and UNIQUE_ID=" + UNIQUE_ID;
                        if (dv.Count > 0)
                        {
                            double QTY = 0f;
                            double.TryParse(txtQty.Text.Trim(), out QTY);
                            dv[0]["TRN_QTY"] = QTY;
                            dv[0]["PO_NUMB"] = Convert.ToInt32(PO_NUMB);
                            dv[0]["PO_TYPE"] = PO_TYPE;
                            dv[0]["PO_COMP_CODE"] = PO_COMP_CODE;
                            dv[0]["PO_BRANCH"] = PO_BRANCH;
                            dv[0]["MATERIAL_STATUS"] = ddlMaterialStatus.SelectedItem.ToString().Trim();
                            dv[0]["GRADE"] = txtGrade.Text.Trim();
                            dv[0]["LOT_NO"] = txtLotNo.Text.Trim();
                            DateTime.TryParse(txtDofMfd.Text.Trim(), out DATE_OF_MFG);
                            dv[0]["DATE_OF_MFG"] = DATE_OF_MFG.ToShortDateString();
                            dv[0]["NO_OF_UNIT"] = double.Parse(txtNoofUnit.Text.Trim());
                            dv[0]["WEIGHT_OF_UNIT"] = double.Parse(txtWeightofUnit.Text.Trim());
                            dv[0]["PI_NO"] = "NA";
                            dv[0]["UOM_OF_UNIT"] = UOM ;
                            dv[0]["UOM"] = UOM;
                            dv[0]["PRTY_CODE"] = PRTY_CODE;
                            dv[0]["BILL_NUMB"] = BILL_NUMB;
                            dv[0]["BILL_DATE"] = BILL_DATE;
                            dv[0]["LOCATION"] = LOCATION;
                            dv[0]["STORE"] = STORE;
                            dtTRN_SUB.AcceptChanges();
                        }
                    }
                    else
                    {


                        DataRow dr = dtTRN_SUB.NewRow();
                        dr["UNIQUE_ID"] = dtTRN_SUB.Rows.Count + 1;
                        dr["ITEM_CODE"] = ITEM_CODE;

                        double QTY = 0f;
                        double.TryParse(txtQty.Text.Trim(), out QTY);

                        dr["TRN_QTY"] = QTY;
                        dr["PO_NUMB"] = Convert.ToInt32(PO_NUMB);
                        dr["PO_TYPE"] = PO_TYPE;
                        dr["PO_COMP_CODE"] = PO_COMP_CODE;
                        dr["PO_BRANCH"] = PO_BRANCH;
                        dr["MATERIAL_STATUS"] = ddlMaterialStatus.SelectedItem.ToString().Trim();
                        dr["GRADE"] = txtGrade.Text.Trim();
                        dr["LOT_NO"] = txtLotNo.Text.Trim();

                        DateTime.TryParse(txtDofMfd.Text.Trim(), out DATE_OF_MFG);
                        dr["DATE_OF_MFG"] = DATE_OF_MFG.ToShortDateString();
                        dr["NO_OF_UNIT"] = double.Parse(txtNoofUnit.Text.Trim());
                        dr["WEIGHT_OF_UNIT"] = double.Parse(txtWeightofUnit.Text.Trim());
                        dr["PI_NO"] = "NA";

                        dr["PRTY_CODE"] = PRTY_CODE;
                        dr["BILL_NUMB"] = BILL_NUMB;
                        dr["BILL_DATE"] = BILL_DATE;
                        dr["LOCATION"] = LOCATION;
                        dr["STORE"] = STORE;
                        dr["UOM_OF_UNIT"] = UOM;
                        dr["UOM"] = UOM;
                        dtTRN_SUB.Rows.Add(dr);

                    }
                    Session["dtTRN_SUB"] = dtTRN_SUB;
                    RefresBOMRow();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Please Enter Another Lot No.This Already Added ');", true);
                }
                BindBOMGrid();
           
                

            


        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving Sub Transaction Detail Row.\r\nSee error log for detail."));

        }
    }

    private void RefresBOMRow()
    {
        try
        {

            txtQty.Text = string.Empty;
            //txtQtyIssue.Text = string.Empty;
            ddlMaterialStatus.SelectedIndex = -1;
            txtGrade.Text = string.Empty;
            txtLotNo.Text = string.Empty;
            txtDofMfd.Text = string.Empty;
            ViewState["UNIQUE_ID"] = null;
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

    protected void grdSub_trnArticleDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UNIQUE_ID = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "SUBEdit")
            {
                FillBOMByGrid(UNIQUE_ID);
            }
            else if (e.CommandName == "SUBDelete")
            {
                DeleteBOMRow(UNIQUE_ID);
                BindBOMGrid();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Sub Tran Grid RowCommand Selection.\r\nSee error log for detail."));
        }
    }

    private void FillBOMByGrid(int UNIQUE_ID)
    {
        try
        {
            DataTable dtTRN_SUB = null;
            if (Session["dtTRN_SUB"] != null)
            {
                dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];
            }
            else
            {
                dtTRN_SUB = CreateSUBTRNTable();
            }    
            DataView dv = new DataView(dtTRN_SUB);
            dv.RowFilter = "UNIQUE_ID=" + UNIQUE_ID;
            if (dv.Count > 0)
            {

                //ddlW_Side.SelectedValue = dv[0]["W_SIDE"].ToString();
                txtQty.Text = dv[0]["TRN_QTY"].ToString();
                //txtQtyIssue.Text = dv[0]["QTY_ISSUE"].ToString();
                ddlMaterialStatus.SelectedValue = dv[0]["MATERIAL_STATUS"].ToString();
                txtGrade.Text = dv[0]["GRADE"].ToString();
                txtLotNo.Text = dv[0]["LOT_NO"].ToString();
                txtDofMfd.Text = dv[0]["DATE_OF_MFG"].ToString();
                txtNoofUnit.Text = dv[0]["NO_OF_UNIT"].ToString();
                txtWeightofUnit.Text = dv[0]["WEIGHT_OF_UNIT"].ToString();
                ViewState["UNIQUE_ID"] = UNIQUE_ID;
            }
        }
        catch
        {
            throw;
        }
    }

    private void DeleteBOMRow(int UNIQUE_ID)
    {
        try
        {
            DataTable dtTRN_SUB = null;
            if (Session["dtTRN_SUB"] != null)
            {
                dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];
            }
            else
            {
                dtTRN_SUB = CreateSUBTRNTable();
            }    
            if (grdsub_trn.Rows.Count == 1)
            {
                dtTRN_SUB.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtTRN_SUB.Rows)
                {
                    int iUNIQUE_ID = int.Parse(dr["UNIQUE_ID"].ToString());
                    if (iUNIQUE_ID == UNIQUE_ID)
                    {
                        dtTRN_SUB.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtTRN_SUB.Rows)
                {
                    iCount = iCount + 1;
                    dr["UNIQUE_ID"] = iCount;
                }
            }
            dtTRN_SUB.AcceptChanges();
            Session["dtTRN_SUB"] = dtTRN_SUB;
        }
        catch
        {
            throw;
        }
    }

    private bool SearchInBOMgrid(string LotNo, int UNIQUE_ID)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in grdsub_trn.Rows)
            {
                Label txtLotNo = (Label)grdRow.FindControl("lbtlotno");

                Button lnkDelete = (Button)grdRow.FindControl("lnkBOMDelete");
                int iUNIQUE_ID = int.Parse(lnkDelete.CommandArgument.Trim());
                if (txtLotNo.Text == LotNo && UNIQUE_ID != iUNIQUE_ID)
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            double totalQty = 0;
            double NoOfUNIT = 0;
            double TotalWeightofUnit = 0;
            double WeightofUnit = 0;

            for (int i = 0; i < grdsub_trn.Rows.Count; i++)
            {
                Label txtQTY = grdsub_trn.Rows[i].FindControl("txtQTY") as Label;
                totalQty += double.Parse(txtQTY.Text);

                Label lblNoUnit = grdsub_trn.Rows[i].FindControl("lblNoUnit") as Label;
                NoOfUNIT += double.Parse(lblNoUnit.Text);

                Label lblWeightofUnit = grdsub_trn.Rows[i].FindControl("lblWeightofUnit") as Label;
                TotalWeightofUnit += double.Parse(lblWeightofUnit.Text);


            }
            WeightofUnit = totalQty / NoOfUNIT;
            if (WeightofUnit.Equals("Infinity")) ;
            {
                WeightofUnit = 0;
            }
            


            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:BindYRNQTY('" + totalQty + "','" + NoOfUNIT + "', '" + WeightofUnit + "','" + txtboxClientid + "','" + txtNoOfUnit + "','" + txtWeightOfUnit + "')", true);

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in submitting delivery data.\r\nsee error log for detail."));
        }
    }

    protected bool CheckQTYtotal()
    {
        try
        {

            double currentpage = 0;

            if (grdsub_trn.Rows.Count > 0)
            {
                for (int i = 0; i < grdsub_trn.Rows.Count; i++)
                {
                    Label txtQTY = grdsub_trn.Rows[i].FindControl("txtQTY") as Label;
                    currentpage += double.Parse(txtQTY.Text);
                }
                double alltotal = currentpage + double.Parse(txtQty.Text);
                
            }
           
            return true;

        }
        catch
        {
            throw;

        }


    }
}