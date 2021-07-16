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

public partial class Module_Inventory_Pages_ITEM_TRN_SUB : System.Web.UI.Page
{
    //private static DataTable dtItemTRN_SUB = null;
    private  string PI_TYPE = string.Empty;
    private  string ITEM_CODE = string.Empty;

    private  double lblMaxQTY = 0;
    private  int PO_NUMB = 0;
    private  string PO_TYPE = string.Empty;
    private  string PO_COMP_CODE = string.Empty;
    private  string PO_BRANCH = string.Empty;
    private  string PO_YEAR = string.Empty;
    private  string txtboxClientid = string.Empty;
    private  string txtNoOfUnit = string.Empty;
    private  string txtWeightOfUnit = string.Empty;
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
            if (Request.QueryString["PO_YEAR"] != null)
            {
                PO_YEAR = Request.QueryString["PO_YEAR"].Trim();
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
                BindInital();
                txtDofMfd.Text = System.DateTime.Now.Date.ToShortDateString();
                
               

                lblRemaining.Text = lblMaxQTY.ToString();
                txtQty.Text = lblMaxQTY.ToString();

                //if (Session["dtItemTRN_SUB"] != null)
                //{
                //    if (dtItemTRN_SUB == null)
                //        CreateSUBTRNTable();
                //    dtItemTRN_SUB = (DataTable)Session["dtItemTRN_SUB"];
                //}
                //else
                //{
                //    grdsub_trn.DataSource = null;
                //    grdsub_trn.DataBind();
                //    if (dtItemTRN_SUB != null && dtItemTRN_SUB.Rows.Count > 0)
                //    {
                //        dtItemTRN_SUB.Clear();
                //    }
                //}
                BindBOMGrid();           
               
            }
            if (Request.QueryString["IsMIsc"] != null)
            {
                if (Request.QueryString["IsMIsc"].Trim() == "1")
                {
                    lblMaxQTY = 999999999.9999;
                    lblRemaining.Text = double.Parse("999999999.9999").ToString();
                }
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
            txtNoofUnit.Text = "1";
            txtWeightofUnit.Text = "1";
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
           DataTable  dtItemTRN_SUB = new DataTable();
            dtItemTRN_SUB.Columns.Add("UNIQUE_ID", typeof(int));
            dtItemTRN_SUB.Columns.Add("ITEM_CODE", typeof(string));
            dtItemTRN_SUB.Columns.Add("TRN_QTY", typeof(double));
            dtItemTRN_SUB.Columns.Add("MATERIAL_STATUS", typeof(string));
            dtItemTRN_SUB.Columns.Add("GRADE", typeof(string));
            dtItemTRN_SUB.Columns.Add("LOT_NO", typeof(string));
            dtItemTRN_SUB.Columns.Add("DATE_OF_MFG", typeof(DateTime));
            dtItemTRN_SUB.Columns.Add("PO_NUMB", typeof(int));
            dtItemTRN_SUB.Columns.Add("PO_TYPE", typeof(string));
            dtItemTRN_SUB.Columns.Add("PO_COMP_CODE", typeof(string));
            dtItemTRN_SUB.Columns.Add("PO_BRANCH", typeof(string));
            dtItemTRN_SUB.Columns.Add("PO_YEAR", typeof(int));
            dtItemTRN_SUB.Columns.Add("NO_OF_UNIT", typeof(double));
            dtItemTRN_SUB.Columns.Add("UOM_OF_UNIT", typeof(string));
            dtItemTRN_SUB.Columns.Add("WEIGHT_OF_UNIT", typeof(double));
            dtItemTRN_SUB.Columns.Add("PI_NO", typeof(string));
            return dtItemTRN_SUB;
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
            DataTable dtItemTRN_SUB = null;
            if (Session["dtItemTRN_SUB"] != null)
            {
                dtItemTRN_SUB = (DataTable)Session["dtItemTRN_SUB"];
            }
            else             
            {
               dtItemTRN_SUB = CreateSUBTRNTable();
            }            

            DataView dv = new DataView(dtItemTRN_SUB);
            dv.RowFilter = "ITEM_CODE='" + ITEM_CODE + "' AND PO_NUMB='" + PO_NUMB + "'";
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
            DataTable dtItemTRN_SUB = new DataTable();
            if (Session["dtItemTRN_SUB"] != null)
            {

                dtItemTRN_SUB = (DataTable)Session["dtItemTRN_SUB"];
            }
            else
            {
                dtItemTRN_SUB = CreateSUBTRNTable();
            }   

            if (CheckQTYtotal())
            {
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
                        DataView dv = new DataView(dtItemTRN_SUB);
                        dv.RowFilter = "ITEM_CODE='" + ITEM_CODE + "' and UNIQUE_ID=" + UNIQUE_ID;
                        if (dv.Count > 0)
                        {
                            double QTY = 0f;
                            double.TryParse(txtQty.Text.Trim(), out QTY);
                           
                            dv[0]["TRN_QTY"] = QTY;
                            dv[0]["PO_NUMB"] = Convert.ToInt32(PO_NUMB);
                            dv[0]["PO_TYPE"] = PO_TYPE;
                            dv[0]["PO_COMP_CODE"] = PO_COMP_CODE;
                            dv[0]["PO_BRANCH"] = PO_BRANCH;
                            int _PO_YEAR = 0;
                            int.TryParse(PO_YEAR, out _PO_YEAR);
                            dv[0]["PO_YEAR"] = _PO_YEAR;
                            dv[0]["MATERIAL_STATUS"] = ddlMaterialStatus.SelectedItem.ToString().Trim();
                            dv[0]["GRADE"] = txtGrade.Text.Trim();
                            dv[0]["LOT_NO"] = txtLotNo.Text.Trim();

                            DateTime.TryParse(txtDofMfd.Text.Trim(), out DATE_OF_MFG);
                            dv[0]["DATE_OF_MFG"] = DATE_OF_MFG.ToShortDateString();
                            dv[0]["NO_OF_UNIT"] = double.Parse(txtNoofUnit.Text.Trim());
                            dv[0]["WEIGHT_OF_UNIT"] = double.Parse(txtWeightofUnit.Text.Trim());
                            dv[0]["PI_NO"] = "NA";
                            dv[0]["UOM_OF_UNIT"] = string.Empty;
                            dtItemTRN_SUB.AcceptChanges();
                        }
                    }
                    else
                    {


                        DataRow dr = dtItemTRN_SUB.NewRow();
                        dr["UNIQUE_ID"] = dtItemTRN_SUB.Rows.Count + 1;
                        dr["ITEM_CODE"] = ITEM_CODE;

                        double QTY = 0f;
                        double.TryParse(txtQty.Text.Trim(), out QTY);

                        dr["TRN_QTY"] = QTY;
                        dr["PO_NUMB"] = Convert.ToInt32(PO_NUMB);
                        dr["PO_TYPE"] = PO_TYPE;
                        dr["PO_COMP_CODE"] = PO_COMP_CODE;
                        dr["PO_BRANCH"] = PO_BRANCH;
                        int _PO_YEAR = 0;
                        int.TryParse(PO_YEAR, out _PO_YEAR);
                        dr["PO_YEAR"] = _PO_YEAR;
                        dr["MATERIAL_STATUS"] = ddlMaterialStatus.SelectedItem.ToString().Trim();
                        dr["GRADE"] = txtGrade.Text.Trim();
                        dr["LOT_NO"] = txtLotNo.Text.Trim();

                        DateTime.TryParse(txtDofMfd.Text.Trim(), out DATE_OF_MFG);
                        dr["DATE_OF_MFG"] = DATE_OF_MFG.ToShortDateString();
                        dr["NO_OF_UNIT"] = double.Parse(txtNoofUnit.Text.Trim());
                        dr["WEIGHT_OF_UNIT"] = double.Parse(txtWeightofUnit.Text.Trim());
                        dr["PI_NO"] = "NA";
                        dtItemTRN_SUB.Rows.Add(dr);

                    }
                    Session["dtItemTRN_SUB"] = dtItemTRN_SUB;
                    RefresBOMRow();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Please Enter Another Lot No.This Already Added ');", true);
                }
                BindBOMGrid();
           
                

            }
            else
            {

                Common.CommonFuction.ShowMessage("You Cannot Recieve Exceeds Quantity !! Limit is =: " + lblMaxQTY);
                //Common.CommonFuction.ShowMessage("You Cannot Recieve Exceeds Quantity Limit.");
                //Common.CommonFuction.ShowMessage("You Cannot Recieve Exceeds Quantity Limit is" + lblMaxQTY); 
            }


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
            if (e.CommandName == "BOMEdit")
            {
                FillBOMByGrid(UNIQUE_ID);
            }
            else if (e.CommandName == "BOMDelete")
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
            DataTable dtItemTRN_SUB = null;
            if (Session["dtItemTRN_SUB"] != null)
            {
                dtItemTRN_SUB = (DataTable)Session["dtItemTRN_SUB"];
            }
            else
            {
                dtItemTRN_SUB = CreateSUBTRNTable();
            }    
            DataView dv = new DataView(dtItemTRN_SUB);
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
            DataTable dtItemTRN_SUB = null;
            if (Session["dtItemTRN_SUB"] != null)
            {
                dtItemTRN_SUB = (DataTable)Session["dtItemTRN_SUB"];
            }
            else
            {
                dtItemTRN_SUB = CreateSUBTRNTable();
            }    
            if (grdsub_trn.Rows.Count == 1)
            {
                dtItemTRN_SUB.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtItemTRN_SUB.Rows)
                {
                    int iUNIQUE_ID = int.Parse(dr["UNIQUE_ID"].ToString());
                    if (iUNIQUE_ID == UNIQUE_ID)
                    {
                        dtItemTRN_SUB.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtItemTRN_SUB.Rows)
                {
                    iCount = iCount + 1;
                    dr["UNIQUE_ID"] = iCount;
                }
            }
            dtItemTRN_SUB.AcceptChanges();
            Session["dtItemTRN_SUB"] = dtItemTRN_SUB;
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
            if (WeightofUnit.Equals("Infinity")) 
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
                if (alltotal > lblMaxQTY)
                {
                    return false;

                }
                else
                {
                    return true;
                }

            }
            else if (double.Parse(txtQty.Text) > lblMaxQTY)
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
}