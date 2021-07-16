using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;
using errorLog;
using Common;
using DBLibrary;
using System.IO;
using Obout.ComboBox;
public partial class Module_OrderDevelopment_Controls_IssueRequisitionl : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    OracleDataReader dr = null;
    csSaitex obj = null;
    private DataTable dtIssueReq;
    string itemtype;
    string articlecode;
    string shadcode;
    string uom;
    string qty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Clear();
        }
    }
    private void Clear()
    {
        txtLotNo.Enabled = true;
        ddlPiNo.Items.Clear();
        grdBOM.Visible = false;
        grdBOM.DataSource = null;
        txtReqNo.Enabled = false;
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        MaxReqNO();
        BindProductType();
        ddlfind.Visible = false;
        lblMode.Text = "Save";
        tdUpdate.Visible = false;
        tdDelete.Visible = false;
        tdFind.Visible = true;
        txtLotNo.Text = string.Empty;
        txtRemark.Text = string.Empty;
        txtReqDate.Text = System.DateTime.Now.ToShortDateString();
        tdSave.Visible = true;    
    }
    private void BindProductType()
    {
        try
        {
            ddlProductType.Items.Clear();
            DataTable dtProductionType = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("PRODUCT_TYPE", oUserLoginDetail.COMP_CODE); 
            ddlProductType.DataSource = dtProductionType;
            ddlProductType.DataTextField = "MST_DESC";
            ddlProductType.DataValueField = "MST_CODE";       
            ddlProductType.DataBind();
            ddlProductType.Items.Insert(0, new ListItem("SELECT", string.Empty));
           
        }
        catch
        {
            throw;
        }
    }
    private void BindPINo()
    {
        try
        {

            string ProductType = ddlProductType.SelectedValue.ToString();
            ddlPiNo.Items.Clear();

            DataTable dtPiNo = SaitexBL.Interface.Method.OD_ISS_REQ_MST.Getpino(ProductType);

            if (dtPiNo.Rows.Count > 0)
            {
                ddlPiNo.DataSource = dtPiNo;
                ddlPiNo.DataTextField = "PI_NO";
                ddlPiNo.DataValueField = "PI_NO";
                ddlPiNo.DataBind();
                ddlPiNo.Items.Insert(0, new ListItem("SELECT", string.Empty));
                tdSave.Visible = true;
              
            }
            else
            {
                Common.CommonFuction.ShowMessage("PI Record Not Found .");
                Clear();
            }
        
        }
        catch
        {
            throw;
        }
    }
    protected void ddlProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindPINo();
            bindBomGrid();
        }
        catch
        {
            throw;
        }
    }
    private void bindBomGrid()
    {
        string PInumber = ddlPiNo.SelectedValue.ToString();
        try
        {
            if (PInumber != null && PInumber != string.Empty)
            {
                DataTable dt = SaitexBL.Interface.Method.OD_CAPT_MST.GetGridBom();
                DataView dv = new DataView(dt);
                dv.RowFilter = "PI_NO='" + PInumber + "'" ;         
                grdBOM.DataSource = dv;
                grdBOM.DataBind();
                grdBOM.Visible = true;
            }
           
        }
        catch
        { 
            throw;
        }

    }
    protected void ddlPiNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindBomGrid();
        }
        catch
        {
            throw;
        }
    }
    private void MaxReqNO()
    {
        try
        {
            string x = "";
            int y = 0;
            DataTable dt = SaitexBL.Interface.Method.OD_CAPT_MST.GetMaxIssueReqNO();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        x = dv[iLoop]["ISSUE_REQ_NO"].ToString();
                        y = int.Parse(x);
                        y = y + 1;
                        txtReqNo.Text = y.ToString();
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }
    protected void imgbtnNew_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Insert();
        }
        catch
        {
            throw;
        }
    }
    private void Insert()
    {

        try
        {

            CreateDtIssueQuery();
            if (ViewState["dtIssueReq"] != null)
                dtIssueReq = (DataTable)ViewState["dtIssueReq"];

            if (txtLotNo.Text != "")
            {
                if (ddlProductType.SelectedIndex > 0)
                {
                    if (ddlPiNo.SelectedIndex > 0)
                    {

                        int iRecordFound = 0;

                        SaitexDM.Common.DataModel.OD_ISS_REQ_MST oOD_ISS_REQ_MST = new SaitexDM.Common.DataModel.OD_ISS_REQ_MST();
                        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                        oOD_ISS_REQ_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                        oOD_ISS_REQ_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                        oOD_ISS_REQ_MST.ISSUE_REQ_NO = CommonFuction.funFixQuotes(txtReqNo.Text.ToString());
                        oOD_ISS_REQ_MST.ISSUE_REQ_DATE = DateTime.Parse(txtReqDate.Text.Trim());
                        oOD_ISS_REQ_MST.PRODUCT_TYPE = CommonFuction.funFixQuotes(ddlProductType.SelectedValue.ToString());
                        oOD_ISS_REQ_MST.PA_NO = CommonFuction.funFixQuotes(ddlPiNo.SelectedValue.ToString());
                        oOD_ISS_REQ_MST.LOT_NUMBER = CommonFuction.funFixQuotes(txtLotNo.Text.Trim());
                        oOD_ISS_REQ_MST.REMARKS = CommonFuction.funFixQuotes(txtRemark.Text.ToString());
                        oOD_ISS_REQ_MST.TUSER = oUserLoginDetail.UserCode;

                        bool bResult = SaitexBL.Interface.Method.OD_ISS_REQ_MST.InsertIssueReq(oOD_ISS_REQ_MST, dtIssueReq, out iRecordFound);

                        if (bResult)
                        {
                            Common.CommonFuction.ShowMessage("Record  Saved .");
                            Clear();

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Duplicate Lot No.');", true);

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Select PI NO..');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Select Product Type .');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Save. Provide Lot No');", true);
            }
        }
        catch
        {
            throw;
        }
    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblMode.Text = "Update";
            tdUpdate.Visible = false;
            tdSave.Visible = false;
            tdDelete.Visible = false;
            ddlfind.Visible = true;
            ddlfind.SelectedIndex = -1;
        }
        catch
        {
            throw;
        }
    }
    protected void ddlfind_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            ddlFindIssueReqNo();
        }
        catch
        {
            throw;
        }

    }
    private void ddlFindIssueReqNo()
    {
        try
        {
            ddlfind.Items.Clear();

            DataTable dt = SaitexBL.Interface.Method.OD_ISS_REQ_MST.FindIssueReqNO();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlfind.DataSource = dt;
                ddlfind.DataValueField = "ISSUE_REQ_NO";
                ddlfind.DataTextField = "ISSUE_REQ_NO";
                ddlfind.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }
    private void findbtn()
    {
        try
        {
            
            tdUpdate.Visible = true;
            ddlfind.Items.Clear();
            string strSQL = "";
            ViewState["str"] = ddlfind.SelectedValue.Trim();
            strSQL =  " select * from OD_ISS_REQ_MST  where ISSUE_REQ_NO = '" + ViewState["str"] + "' and DEL_STATUS = '0'";
            obj = new csSaitex();
            dr = obj.getDataReader(strSQL, CommandType.Text);
            if (dr.Read())
            {
                
                txtReqNo.Text = dr["ISSUE_REQ_NO"].ToString().Trim();
                txtReqDate.Text = DateTime.Parse(dr["ISSUE_REQ_DATE"].ToString().Trim()).ToShortDateString().Trim();
                txtLotNo.Text = dr["LOT_NUMBER"].ToString().Trim();            
                ddlProductType.SelectedValue  = dr["PRODUCT_TYPE"].ToString().Trim();        
                string str = ddlProductType.SelectedValue.ToString();
               
                DataTable dtPiNo = SaitexBL.Interface.Method.OD_ISS_REQ_MST.GetIssuePINo(str);
                ddlPiNo.DataSource = dtPiNo;
                ddlPiNo.DataTextField = "PI_NO";
                ddlPiNo.DataValueField = "PI_NO";
                ddlPiNo.DataBind();
                ddlPiNo.SelectedValue = dr["PA_NO"].ToString().Trim();
                txtRemark.Text = dr["REMARKS"].ToString().Trim();
                       
            }
      
            string pino = ddlPiNo.SelectedValue.ToString();     
            DataTable dt = SaitexBL.Interface.Method.OD_ISS_REQ_MST.GetIssueGridBom(pino);
            DataView dv = new DataView(dt);
            dv.RowFilter = "ISSUE_REQ_NO='" + ddlfind.SelectedValue.ToString() + "'";    
            grdBOM.DataSource = dv;
            grdBOM.DataBind();
            grdBOM.Visible = true;
            dr.Close();
            dr.Dispose();
            dr = null;
            txtLotNo.Enabled = false;
        }
        catch (OracleException ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('" + ex.Message + "');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('" + ex.Message + "');", true);
        }
        finally
        {

        }
    }
    protected void ddlfind_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            findbtn();
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
        }
        catch
        {
            throw;
        }
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            update();
        }
        catch
        {
            throw;
        }
    }
    private void update()
    {
        try
        {

            if (txtLotNo.Text != "")
            {
              
                CreateDtIssueQuery();
                if (ViewState["dtIssueReq"] != null)
                    dtIssueReq = (DataTable)ViewState["dtIssueReq"];

                int iRecordFound = 0;
                SaitexDM.Common.DataModel.OD_ISS_REQ_MST oOD_ISS_REQ_MST = new SaitexDM.Common.DataModel.OD_ISS_REQ_MST();
                SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                oOD_ISS_REQ_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oOD_ISS_REQ_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oOD_ISS_REQ_MST.ISSUE_REQ_NO = CommonFuction.funFixQuotes(txtReqNo.Text.ToString());
                oOD_ISS_REQ_MST.ISSUE_REQ_DATE = DateTime.Parse(txtReqDate.Text.Trim());
                oOD_ISS_REQ_MST.PRODUCT_TYPE = CommonFuction.funFixQuotes(ddlProductType.SelectedValue.ToString());
                oOD_ISS_REQ_MST.PA_NO = CommonFuction.funFixQuotes(ddlPiNo.SelectedValue.ToString());
                oOD_ISS_REQ_MST.LOT_NUMBER = CommonFuction.funFixQuotes(txtLotNo.Text.Trim());
                oOD_ISS_REQ_MST.REMARKS = CommonFuction.funFixQuotes(txtRemark.Text.ToString());
                bool bResult = SaitexBL.Interface.Method.OD_ISS_REQ_MST.UpdateIssueReq(oOD_ISS_REQ_MST, dtIssueReq, out iRecordFound);
                if (bResult)
                {
                    Common.CommonFuction.ShowMessage("Record  Update Successfully .");
                    Clear();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Duplicate Record.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Provide Lot No');", true);
            }
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
            Clear();
        }
        catch
        {
            throw;
        }

    }
    private DataTable CreateDtIssueQuery()
    {
        try
        {
            dtIssueReq = new DataTable();
            DataRow dr;
            dtIssueReq.Columns.Add("ITEM_TYPE", typeof(string));
            dtIssueReq.Columns.Add("ARTICLE_CODE", typeof(string));
            dtIssueReq.Columns.Add("SHADE_CODE", typeof(string));
            dtIssueReq.Columns.Add("UOM", typeof(string));
            dtIssueReq.Columns.Add("QTY", typeof(string));

            foreach (GridViewRow row in grdBOM.Rows)
            {
                dr = dtIssueReq.NewRow();

                Label baseArtType = (Label)row.FindControl("txtBaseArticalType");             
                Label txtBomArt  = (Label)row.FindControl("txtBOMArticleCode");               
                Label shadcode  = (Label)row.FindControl("txtShadeCode");
                Label uom  = (Label)row.FindControl("lblBOMUOM");
                TextBox Eqty   = (TextBox)row.FindControl("TxtExpectQty");
               
                dr["ITEM_TYPE"] = baseArtType.Text;
                dr["ARTICLE_CODE"] = txtBomArt.Text;
                dr["SHADE_CODE"] = shadcode.Text;
                dr["UOM"] = uom.Text;
                dr["QTY"] = Eqty.Text.ToString();
                dtIssueReq.Rows.Add(dr);
            }

            ViewState["dtIssueReq"] = dtIssueReq;
            return dtIssueReq;  
        }
        catch
        {
            throw;
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
                Response.Redirect("~/Module/Admin/Pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    } 
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Delete();
        }
        catch
        {
            throw;
        }

    }
    private void Delete()
    {

        try
        {

            CreateDtIssueQuery();
            if (ViewState["dtIssueReq"] != null)
                dtIssueReq = (DataTable)ViewState["dtIssueReq"];

            if (txtReqNo.Text != "")
            {
                int iRecordFound = 0;

                SaitexDM.Common.DataModel.OD_ISS_REQ_MST oOD_ISS_REQ_MST = new SaitexDM.Common.DataModel.OD_ISS_REQ_MST();
                //SaitexDM.Common.DataModel.OD_ISS_REQ_TRN oOD_ISS_REQ_TRN = new SaitexDM.Common.DataModel.OD_ISS_REQ_TRN();
                SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                oOD_ISS_REQ_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oOD_ISS_REQ_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oOD_ISS_REQ_MST.ISSUE_REQ_NO = CommonFuction.funFixQuotes(txtReqNo.Text.ToString());
                            
                bool bResult = SaitexBL.Interface.Method.OD_ISS_REQ_MST.DeleteIssueReq(oOD_ISS_REQ_MST, dtIssueReq, out iRecordFound);
                if (bResult)
                {
                    Common.CommonFuction.ShowMessage("Record Delete .");
                    Clear();
                  
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Duplicate Record.');", true);

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Save. Provide Group Code');", true);
            }
        }
        catch
        {
            throw;
        }
    }
    //private void DuplicateLotNo()
    //{

    //    string lotno = txtLotNo.Text.ToString();
    //    DataTable bresult = SaitexBL.Interface.Method.OD_ISS_REQ_MST.getLotNo(lotno);
    //    if (bresult.Rows.Count > 0)
    //    {
    //        Common.CommonFuction.ShowMessage("Lot No Already Used .Please Insert Other Lot NO.");
    //        txtLotNo.Text = string.Empty;
    //    }
    //    else
    //    {
    //        Insert();
    //    }
        
    //}
}