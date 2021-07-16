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

public partial class Module_Fiber_Pages_FIBER_TRN_SUB : System.Web.UI.Page
{
    private  DataTable dtTRN_SUB = null;
    private  string PI_TYPE = string.Empty;
    private  string FIBER_CODE = string.Empty;
    private  double lblMaxQTY = 0;
    private  int PO_NUMB = 0;
    private int PO_YEAR = 0;
    private  string PO_TYPE = string.Empty;
    private  string PO_COMP_CODE = string.Empty;
    private  string PO_BRANCH = string.Empty;
    private  string txtboxClientid = string.Empty;
    private  string txtNoOfUnit = string.Empty;
    private  string txtUOm = string.Empty;
    private  string txtWeightOfUnit = string.Empty;
    private string MERGE_NO = string.Empty;
    private string PALLET_CODE = string.Empty;
    private string GRADE = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;



    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (Request.QueryString["FIBER_CODE"] != null)
            {
                FIBER_CODE = Request.QueryString["FIBER_CODE"].Trim();
                lblYarnCode.Text = FIBER_CODE;
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
            if (Request.QueryString["txtUOm"] != null)
            {
                txtUOm = Request.QueryString["txtUOm"].Trim();
            }
            if (Request.QueryString["lblMaxQTY"] != null)
            {
                lblMaxQTY = double.Parse(Request.QueryString["lblMaxQTY"].Trim());
            } 
            if (Request.QueryString["txtWeightOfUnit"] != null)
            {
                txtWeightOfUnit = Request.QueryString["txtWeightOfUnit"].Trim();
            }
            if (Request.QueryString["txtNoOfUnit"] != null)
            {
                txtNoOfUnit = Request.QueryString["txtNoOfUnit"].Trim();
            }
            if (Request.QueryString["MERGE_NO"] != null)
            {
                MERGE_NO = Request.QueryString["MERGE_NO"].Trim();
            }
            if (Request.QueryString["PALLET_CODE"] != null)
            {
                PALLET_CODE = Request.QueryString["PALLET_CODE"].Trim();
            }
            if (Request.QueryString["GRADE"] != null)
            {
                GRADE = Request.QueryString["GRADE"].Trim();
               
            }
            if (Request.QueryString["PO_YEAR"] != null)
            {
                int.TryParse(Request.QueryString["PO_YEAR"].ToString(), out PO_YEAR);

            }
            if (!IsPostBack)
            {
                BindIntial();
                txtDofMfd.Text = System.DateTime.Now.Date.ToShortDateString();               
                lblRemaining.Text = lblMaxQTY.ToString();
                txtQty.Text = lblMaxQTY.ToString();               
                if (Session["dtTRN_SUB"] != null)
                {
                    if (dtTRN_SUB == null)
                        CreateSUBTRNTable();
                    dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];

                }
                else
                {
                    grdsub_trn.DataSource = null;
                    grdsub_trn.DataBind();
                    if (dtTRN_SUB != null && dtTRN_SUB.Rows.Count > 0)
                    {
                        dtTRN_SUB.Clear();
                    }
                }
                bindUOM("UOM");
                BindBOMGrid();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nsee error log for detail."));
        }
    }

    public void BindIntial()
    {

        try
        {
            if (!string.IsNullOrEmpty(GRADE))
            {
                txtGrade.Text = GRADE;
            }
            else { txtGrade.Text = "NA"; }
            txtLotNo.Text = "NA";
            txtNoofUnit.Text = "1";
            txtWeightofUnit.Text = "1";
        }
        catch
        {

        }
    }

    public void clearInitial()
    {
        //txtGrade.Text = "NA";
        //txtLotNo.Text = "NA";
        txtNoofUnit.Text = "1";
        txtWeightofUnit.Text = "1";
    }

    public void bindUOM(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {

                ddlUOM.Items.Clear();
                ddlUOM.DataSource = dt;
                ddlUOM.DataTextField = "MST_CODE";
                ddlUOM.DataValueField = "MST_CODE";
                ddlUOM.DataBind();
                ddlUOM.SelectedValue = "KG";
                //ddlUOM.Items.Insert(0, new ListItem("------Select------", "0"));
            }

        }
        catch
        {
            throw;
        }


    }

    private void CreateSUBTRNTable()
    {
        try
        {
            dtTRN_SUB = new DataTable();
            dtTRN_SUB.Columns.Add("UNIQUE_ID", typeof(int));
            dtTRN_SUB.Columns.Add("FIBER_CODE", typeof(string));
            dtTRN_SUB.Columns.Add("TRN_QTY", typeof(double));
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
            dtTRN_SUB.Columns.Add("PALLET_NO", typeof(string));
            dtTRN_SUB.Columns.Add("PALLET_CODE", typeof(string));
            dtTRN_SUB.Columns.Add("ROW_STATE", typeof(string));
            dtTRN_SUB.Columns.Add("PO_YEAR", typeof(int));
            
           

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
            if (Session["dtTRN_SUB"] != null)
            {
                dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];
            }
            if (dtTRN_SUB == null)
            {
                CreateSUBTRNTable();
            }
           
            DataView dv = new DataView(dtTRN_SUB);
            dv.RowFilter = "FIBER_CODE='" + FIBER_CODE + "' AND PO_NUMB='" + PO_NUMB + "'  AND LOT_NO='" + MERGE_NO + "'  AND PALLET_CODE='" + PALLET_CODE + "' AND ROW_STATE <> 'DELETE'";
            if (dv.Count > 0)
            {
                clearInitial();
                txtQty.Text = string.Empty;
                grdsub_trn.DataSource = dv;
                grdsub_trn.DataBind();
                CalculateAllData();
            }
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
            txtDofMfd.Text = System.DateTime.Now.Date.ToShortDateString();

            if (Session["dtTRN_SUB"] != null)
            {
                dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];
            }


            if (dtTRN_SUB == null)
            {
                CreateSUBTRNTable();
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
                        if (UNIQUE_ID > 0)
                        {
                            DataView dv = new DataView(dtTRN_SUB);
                            dv.RowFilter = "FIBER_CODE='" + FIBER_CODE + "' AND LOT_NO='" + MERGE_NO + "'  AND PALLET_CODE='" + PALLET_CODE + "'  and UNIQUE_ID=" + UNIQUE_ID;
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
                                dv[0]["GRADE"] = txtGrade.Text.Trim().ToUpper();
                                dv[0]["LOT_NO"] = MERGE_NO.ToUpper();
                                dv[0]["DATE_OF_MFG"] = txtDofMfd.Text.Trim();
                                dv[0]["NO_OF_UNIT"] = double.Parse(txtNoofUnit.Text.Trim());
                                dv[0]["UOM_OF_UNIT"] = ddlUOM.SelectedItem.ToString();
                                dv[0]["WEIGHT_OF_UNIT"] = double.Parse(txtWeightofUnit.Text.Trim());
                                dv[0]["PI_NO"] = "NA";
                                dv[0]["PALLET_NO"] = txtLotNo.Text.Trim().ToUpper();
                                dv[0]["PALLET_CODE"] = PALLET_CODE.ToUpper();
                                dv[0]["PO_YEAR"] = PO_YEAR;          
                                dtTRN_SUB.AcceptChanges();
                            }
                        }
                        else
                        {


                            DataRow dr = dtTRN_SUB.NewRow();
                            dr["UNIQUE_ID"] = dtTRN_SUB.Rows.Count + 1;
                            dr["FIBER_CODE"] = FIBER_CODE;

                            double QTY = 0f;
                            double.TryParse(txtQty.Text.Trim(), out QTY);

                            dr["TRN_QTY"] = QTY;
                            dr["PO_NUMB"] = Convert.ToInt32(PO_NUMB);
                            dr["PO_TYPE"] = PO_TYPE;
                            dr["PO_COMP_CODE"] = PO_COMP_CODE;
                            dr["PO_BRANCH"] = PO_BRANCH;
                            dr["MATERIAL_STATUS"] = ddlMaterialStatus.SelectedItem.ToString().Trim();
                            dr["GRADE"] = txtGrade.Text.Trim().ToUpper();
                            dr["LOT_NO"] = MERGE_NO.ToUpper();
                            dr["DATE_OF_MFG"] = txtDofMfd.Text.Trim();
                            dr["NO_OF_UNIT"] = double.Parse(txtNoofUnit.Text.Trim());
                            dr["UOM_OF_UNIT"] = ddlUOM.SelectedItem.ToString();
                            dr["WEIGHT_OF_UNIT"] = double.Parse(txtWeightofUnit.Text.Trim());
                            dr["PI_NO"] = "NA";
                            dr["PALLET_NO"] = txtLotNo.Text.Trim().ToUpper();
                            dr["PALLET_CODE"] = PALLET_CODE.ToUpper();
                            dr["ROW_STATE"] = "INSERT";
                            dr["PO_YEAR"] = PO_YEAR;   
                            dtTRN_SUB.Rows.Add(dr);                           
                               
                            

                        }
                        
                        RefresBOMRow();
                        Session["dtTRN_SUB"] = dtTRN_SUB;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Please Enter Another Lot No.This Already Added ');", true);
                    }
                    BindBOMGrid();

                
            }

            else
            {
                Common.CommonFuction.ShowMessage("You have reached the limit of Sub Transaction. Only 15 Sub Transaction Allowed.");
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
            //txtGrade.Text = string.Empty;
            //txtLotNo.Text = string.Empty;
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
            if (Session["dtTRN_SUB"] != null)
            {
                dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];
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
                txtLotNo.Text = dv[0]["PALLET_NO"].ToString();
                txtDofMfd.Text = dv[0]["DATE_OF_MFG"].ToString();
                txtNoofUnit.Text=dv[0]["NO_OF_UNIT"].ToString();
                txtWeightofUnit.Text = dv[0]["WEIGHT_OF_UNIT"].ToString();
                ViewState["UNIQUE_ID"] = UNIQUE_ID;
            }
        }
        catch
        {
            throw;
        }
    }

    //private void DeleteBOMRow(int UNIQUE_ID)
    //{
    //    try
    //    {
    //        if (Session["dtTRN_SUB"] != null)
    //        {
    //            dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];
    //        }
    //        if (grdsub_trn.Rows.Count == 1)
    //        {
    //            dtTRN_SUB.Rows.Clear();
    //        }
    //        else
    //        {
    //            foreach (DataRow dr in dtTRN_SUB.Rows)
    //            {
    //                int iUNIQUE_ID = int.Parse(dr["UNIQUE_ID"].ToString());
    //                if (iUNIQUE_ID == UNIQUE_ID)
    //                {
    //                    dtTRN_SUB.Rows.Remove(dr);
    //                    break;
    //                }
    //            }
    //            int iCount = 0;
    //            foreach (DataRow dr in dtTRN_SUB.Rows)
    //            {
    //                iCount = iCount + 1;
    //                dr["UNIQUE_ID"] = iCount;
    //            }
    //        }
    //        Session["dtTRN_SUB"] = dtTRN_SUB;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}


    private void DeleteBOMRow(int UNIQUE_ID)
    {
        try
        {
            dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];
            if (grdsub_trn.Rows.Count == 1)
            {                
                dtTRN_SUB.Rows[0].SetField("ROW_STATE", "DELETE");
            }
            else
            {
                foreach (DataRow dr in dtTRN_SUB.Rows)
                {
                    int iUNIQUE_ID = int.Parse(dr["UNIQUE_ID"].ToString());
                    if (iUNIQUE_ID == UNIQUE_ID)
                    {                        
                        dr.SetField("ROW_STATE", "DELETE");
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
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:BindYRNQTY('" + totalQty + "','" + NoOfUNIT + "','" + ddlUOM.SelectedItem.ToString() + "','" + WeightofUnit + "','" + txtboxClientid + "','" + txtNoOfUnit + "','" + txtWeightOfUnit + "','" + txtUOm + "')", true);
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
            int UNIQUE_ID = 0;
            if (ViewState["UNIQUE_ID"] != null)
            {
                UNIQUE_ID = int.Parse(ViewState["UNIQUE_ID"].ToString());
            }
            if (grdsub_trn.Rows.Count > 0)
            {
                for (int i = 0; i < grdsub_trn.Rows.Count; i++)
                {

                    Label txtSubTrnUNIQUE_ID = (Label)grdsub_trn.Rows[i].FindControl("txtSubTrnUNIQUE_ID");
                    int iUNIQUEID = int.Parse(txtSubTrnUNIQUE_ID.Text);
                    if (UNIQUE_ID != iUNIQUEID)
                    {
                        Label txtQTY = grdsub_trn.Rows[i].FindControl("txtQTY") as Label;
                        currentpage += double.Parse(txtQTY.Text);
                    }
                }
                double alltotal = currentpage + double.Parse(txtQty.Text);
                if (alltotal > lblMaxQTY)
                {
                    //return false;
                    return true;

                }
                else
                {
                    return true;
                }

            }
            else if (double.Parse(txtQty.Text) > lblMaxQTY)
            {
               // return false;
                return true;
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

    protected void txtNoofUnit_TextChanged(object sender, EventArgs e)
    {
        txtWeightofUnit.Text = (double.Parse(txtQty.Text) / double.Parse(txtNoofUnit.Text)).ToString();
        ddlUOM.Focus();
    }

    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        if (txtNoofUnit.Text != "")
        {
            txtWeightofUnit.Text = (double.Parse(txtQty.Text) / double.Parse(txtNoofUnit.Text)).ToString();
            txtNoofUnit.Focus();
            
        }

    }


    protected void CalculateAllData()
    {
        if (grdsub_trn.Rows.Count > 0)
        {
            double totalCops = 0;          
            double totalNetWt = 0;
           
            for (int i = 0; i < grdsub_trn.Rows.Count; i++)
            {
                double Cops = 0;                
                double NetWt = 0;
                Label lblNUnit = grdsub_trn.Rows[i].FindControl("lblNoUnit") as Label;               
                Label lblQTY = grdsub_trn.Rows[i].FindControl("txtQTY") as Label;               

                double.TryParse(lblNUnit.Text, out Cops);              
                double.TryParse(lblQTY.Text, out NetWt);
                totalNetWt = totalNetWt + NetWt;
                totalCops = totalCops + Cops;    

            }

            ((Label)grdsub_trn.FooterRow.FindControl("txtTotalQTY")).Text = totalNetWt.ToString();
            ((Label)grdsub_trn.FooterRow.FindControl("lblTotalNoUnit")).Text = totalCops.ToString();                

        }
    }



    protected void grdsub_trn_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button delButton = (Button)e.Row.FindControl("lnkBOMDelete");
                Label txtBOMState = (Label)e.Row.FindControl("txtBOMState");
                if (txtBOMState.Text.Equals("UPDATE"))
                {
                    delButton.Enabled = false;

                }
                
            }
        }
        catch (Exception ex)
        {
           
        }
    }
}
