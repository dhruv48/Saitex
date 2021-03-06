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
using DBLibrary;
using Common;
using errorLog;
using Obout.ComboBox;
public partial class Module_OrderDevelopment_Pages_BOM : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.OD_SHADE_FAMILY oOD_SHADE_FAMILY = new SaitexDM.Common.DataModel.OD_SHADE_FAMILY();
    private  DataTable dtTRN_BOM = null;

    private  string COMP_CODE = string.Empty;
    private  string BRANCH_CODE = string.Empty;
    private  string BUSINESS_TYPE = string.Empty;
    private  string PRODUCT_TYPE = string.Empty;
    private  string ORDER_CAT = string.Empty;
    private  string ORDER_TYPE = string.Empty;
    private  string ORDER_NO = string.Empty;
    private  string PI_TYPE = string.Empty;
    private  string PI_NO = string.Empty;
    private  string ARTICAL_CODE = string.Empty;
    private  string SHADE_CODE = string.Empty;
    private  string BOM_FLAG = string.Empty;
    private  string BASE_ARTICLE_CODE = string.Empty;
    private string ARTICAL_DESC = string.Empty;
    private string ASS_ARTICAL_DESC = string.Empty;
    private string ASS_ARTICAL_CODE = string.Empty;
    
    private  double ORD_QTY = 0;
    private  int  YEAR = 0;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"]; if (Request.QueryString["COMP_CODE"] != null)
            {
                COMP_CODE = Request.QueryString["COMP_CODE"].Trim();
            }
            if (Request.QueryString["BRANCH_CODE"] != null)
            {
                BRANCH_CODE = Request.QueryString["BRANCH_CODE"].Trim();
            }
            if (Request.QueryString["BUSINESS_TYPE"] != null)
            {
                BUSINESS_TYPE = Request.QueryString["BUSINESS_TYPE"].Trim();
            }
            if (Request.QueryString["PRODUCT_TYPE"] != null)
            {
                PRODUCT_TYPE = Request.QueryString["PRODUCT_TYPE"].Trim();
            }
            if (Request.QueryString["ORDER_CAT"] != null)
            {
                ORDER_CAT = Request.QueryString["ORDER_CAT"].Trim();
            }
            if (Request.QueryString["ORDER_TYPE"] != null)
            {
                ORDER_TYPE = Request.QueryString["ORDER_TYPE"].Trim();
            }
            if (Request.QueryString["ORDER_NO"] != null)
            {
                ORDER_NO = Request.QueryString["ORDER_NO"].Trim();
            }
            if (Request.QueryString["PI_TYPE"] != null)
            {
                PI_TYPE = Request.QueryString["PI_TYPE"].Trim();
            }
            if (Request.QueryString["PI_NO"] != null)
            {
                PI_NO = Request.QueryString["PI_NO"].Trim();
            }
            if (Request.QueryString["ORD_QTY"] != null)
            {
                double _QTY=0;
                double.TryParse(Request.QueryString["ORD_QTY"].Trim(),out _QTY);
                ORD_QTY = _QTY;
            }
            if (Request.QueryString["ARTICAL_CODE"] != null)
            {
                ARTICAL_CODE = Request.QueryString["ARTICAL_CODE"].Trim();
                BASE_ARTICLE_CODE = Request.QueryString["ARTICAL_CODE"].Trim();
            }
            if (Request.QueryString["SHADE_CODE"] != null)
            {
                SHADE_CODE = Request.QueryString["SHADE_CODE"].ToString();
            }
            if (Request.QueryString["BOM_FLAG"] != null)
            {
                BOM_FLAG = Request.QueryString["BOM_FLAG"].ToString();
                
            }
            if (Request.QueryString["ARTICAL_DESC"] != null)
            {
                ARTICAL_DESC = Request.QueryString["ARTICAL_DESC"].ToString();

            }
            if (Request.QueryString["ASS_ARTICAL_CODE"] != null)
            {
                ASS_ARTICAL_CODE = Request.QueryString["ASS_ARTICAL_CODE"].ToString();
            }
            if (Request.QueryString["ASS_ARTICAL_DESC"] != null)
            {
                ASS_ARTICAL_DESC = Request.QueryString["ASS_ARTICAL_DESC"].ToString();

            }

            
            if (Request.QueryString["YEAR"] != null)
            {
                int.TryParse(Request.QueryString["YEAR"].ToString(),out YEAR);
               
                
            }
            if (!IsPostBack)
            {
                if (Session["dtTRN_BOM"] != null)
                {
                    if (dtTRN_BOM == null)
                    {
                        CreateBOMTable();
                    }
                    dtTRN_BOM = (DataTable)Session["dtTRN_BOM"];
                }
                if (BOM_FLAG.Equals("1"))
                {
                    chkFlag.Checked = true;
                    DisableformByFlag();
                }
                else if (BOM_FLAG.Equals("0"))
                {
                    chkFlag.Checked = false;
                } 
                bindBOMBASE_ARTICAL_TYPE(PI_TYPE);
                BindBOMBASIS();
                BindShadeCode();
                SaitexDM.Common.DataModel.OD_CAPTURE_MST oOD_CAPTURE_MST = new SaitexDM.Common.DataModel.OD_CAPTURE_MST();
                oOD_CAPTURE_MST.COMP_CODE = COMP_CODE;
                oOD_CAPTURE_MST.BRANCH_CODE = BRANCH_CODE;
                oOD_CAPTURE_MST.BUSINESS_TYPE = BUSINESS_TYPE;
                oOD_CAPTURE_MST.PRODUCT_TYPE = PRODUCT_TYPE;
                oOD_CAPTURE_MST.ORDER_CAT = ORDER_CAT;
                oOD_CAPTURE_MST.ORDER_TYPE = ORDER_TYPE;
                oOD_CAPTURE_MST.ORDER_NO = ORDER_NO;
                oOD_CAPTURE_MST.YEAR = YEAR;
                InitialisePage();
                DataTable dt = SaitexDL.Interface.Method.OD_CAPTURE_MST.GetBOMTRN_ByORDER_NO(oOD_CAPTURE_MST, PI_NO, ARTICAL_CODE, SHADE_CODE, PI_TYPE);
                if (dt != null && dt.Rows.Count > 0)
                {                    
                    Session["i"] = null;
                    Session["dtTRN_BOM"] = dt;
                    MapTrnBOM();
                    BindBOMGrid();
                }
                else
                {
                    Session["dtTRN_BOM"] = null;
                    Session["i"] = null;
                    CreateBOM(COMP_CODE, BRANCH_CODE, ARTICAL_CODE, ORD_QTY);
                }
                //else
                //{
                //    DataTable dtBom = SaitexBL.Interface.Method.TX_FABRIC_MST.GetBomFromFabricDesignShade(ARTICAL_CODE, SHADE_CODE, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
                //    if (dtBom != null && dtBom.Rows.Count > 0)
                //    {
                //        MapTrnBOM(dtBom);
                //        BindBOMGridfromDesignmaster();
                //    }
                //}                
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nsee error log for detail."));
        }
    }

    
    private void BindBomFromFabricDesignShade(string FABR_CODE, string SHADE_CODE)
    {

        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_FABRIC_MST.GetBomFromFabricDesignShade(FABR_CODE, SHADE_CODE, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlShadeCode.Items.Clear();
                ddlShadeCode.DataSource = dt;
                ddlShadeCode.DataTextField = "SHADE_CODE";
                ddlShadeCode.DataValueField = "SHADE_CODE";
                ddlShadeCode.DataBind();
                //ddlShadeCode.Items.Insert(0, new ListItem("SELECT", "0"));
                ddlShadeCode.SelectedIndex = ddlShadeCode.Items.IndexOf(ddlShadeCode.Items.FindByValue("GREIGE"));                
            }
        }
        catch
        {
            throw;

        }
    }
    private void MapTrnBOM()
    {
        try
        {

            if (Session["dtTRN_BOM"] != null)
            {
                dtTRN_BOM = (DataTable)Session["dtTRN_BOM"];
            }
            else
            {
                CreateBOMTable();
            }

            if (!dtTRN_BOM.Columns.Contains("UNIQUE_ID"))
            {
                dtTRN_BOM.Columns.Add("UNIQUE_ID", typeof(int));
            }
            for (int iLoop = 0; iLoop < dtTRN_BOM.Rows.Count; iLoop++)
            {
                dtTRN_BOM.Rows[iLoop]["UNIQUE_ID"] = iLoop + 1;
            }
            Session["dtTRN_BOM"] = dtTRN_BOM;
        }
        catch
        {
            throw;
        }
    }
    private void MapTrnBOM(DataTable dtbom)
    {
        try
        {

            if (!dtbom.Columns.Contains("UNIQUE_ID"))
            {
                dtbom.Columns.Add("UNIQUE_ID", typeof(int));
            }
            if (!dtbom.Columns.Contains("BUSINESS_TYPE"))
            {
                dtbom.Columns.Add("BUSINESS_TYPE", typeof(string));
            }
            if (!dtbom.Columns.Contains("PRODUCT_TYPE"))
            {
                dtbom.Columns.Add("PRODUCT_TYPE", typeof(string));
            }
            if (!dtbom.Columns.Contains("ORDER_CAT"))
            {
                dtbom.Columns.Add("ORDER_CAT", typeof(string));
            }
            if (!dtbom.Columns.Contains("ORDER_TYPE"))
            {
                dtbom.Columns.Add("ORDER_TYPE", typeof(string));
            }
            if (!dtbom.Columns.Contains("ORDER_NO"))
            {
                dtbom.Columns.Add("ORDER_NO", typeof(string));
            }
            if (!dtbom.Columns.Contains("PI_TYPE"))
            {
                dtbom.Columns.Add("PI_TYPE", typeof(string));
            }
            if (!dtbom.Columns.Contains("PI_NO"))
            {
                dtbom.Columns.Add("PI_NO", typeof(string));
            }
            if (!dtbom.Columns.Contains("ARTICAL_CODE"))
            {
                dtbom.Columns.Add("ARTICAL_CODE", typeof(string));
            }
            if (!dtbom.Columns.Contains("SHADE_CODE"))
            {
                dtbom.Columns.Add("SHADE_CODE", typeof(string));
            }
            if (!dtbom.Columns.Contains("BASE_ARTICAL_TYPE"))
            {
                dtbom.Columns.Add("BASE_ARTICAL_TYPE", typeof(string));
            }
            if (!dtbom.Columns.Contains("BASE_ARTICAL_CODE"))
            {
                dtbom.Columns.Add("BASE_ARTICAL_CODE", typeof(string));
            }
            if (!dtbom.Columns.Contains("BASE_ARTICAL_TYPE"))
            {
                dtbom.Columns.Add("BASE_ARTICAL_TYPE", typeof(string));
            }
            if (!dtbom.Columns.Contains("TUSER"))
            {
                dtbom.Columns.Add("TUSER", typeof(string));
            }

            if (!dtbom.Columns.Contains("BASE_SHADE_CODE"))
            {
                dtbom.Columns.Add("BASE_SHADE_CODE", typeof(string));
            }
            if (!dtbom.Columns.Contains("BASIS"))
            {
                dtbom.Columns.Add("BASIS", typeof(string));
            } if (!dtbom.Columns.Contains("BOM_REMARKS"))
            {
                dtbom.Columns.Add("BOM_REMARKS", typeof(string));
            }
            if (!dtbom.Columns.Contains("BOM_REMARKS"))
            {
                dtbom.Columns.Add("BOM_REMARKS", typeof(string));
            }


            for (int iLoop = 0; iLoop < dtbom.Rows.Count; iLoop++)
            {
                dtbom.Rows[iLoop]["UNIQUE_ID"] = iLoop + 1;
                dtbom.Rows[iLoop]["BUSINESS_TYPE"] = BUSINESS_TYPE;
                dtbom.Rows[iLoop]["PRODUCT_TYPE"] = PRODUCT_TYPE;
                dtbom.Rows[iLoop]["ORDER_CAT"] = ORDER_CAT;
                dtbom.Rows[iLoop]["ORDER_TYPE"] = ORDER_TYPE;
                dtbom.Rows[iLoop]["ORDER_NO"] = ORDER_NO;
                dtbom.Rows[iLoop]["PI_TYPE"] = PI_TYPE;
                dtbom.Rows[iLoop]["PI_NO"] = PI_NO;
                dtbom.Rows[iLoop]["ARTICAL_CODE"] = ARTICAL_CODE;
                dtbom.Rows[iLoop]["SHADE_CODE"] = SHADE_CODE;

            }
            Session["dtTRN_BOM"] = dtbom;
        }
        catch
        {
            throw;
        }
    }
    private void BindShadeCode()
    {

        try
        {
            DataTable dt = SaitexBL.Interface.Method.OD_SHADE_FAMILY.GetShadeCodeALL(oOD_SHADE_FAMILY);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlShadeCode.Items.Clear();
                ddlShadeCode.DataSource = dt;
                ddlShadeCode.DataTextField = "SHADE_CODE";
                ddlShadeCode.DataValueField = "SHADE_CODE";
                ddlShadeCode.DataBind();
                //ddlShadeCode.Items.Insert(0, new ListItem("SELECT", "0"));
                ddlShadeCode.SelectedIndex = ddlShadeCode.Items.IndexOf(ddlShadeCode.Items.FindByValue("GREY"));                
            }
        }
        catch
        {
            throw;

        }
    }
    private void BindItemCatetory()
    {

        try
        {
            DataTable dt = GET_MOM_DATA("", "ITEM_CATG");
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlShadeCode.Items.Clear();
                ddlShadeCode.DataSource = dt;
                ddlShadeCode.DataTextField = "MST_CODE";
                ddlShadeCode.DataValueField = "MST_CODE";
                ddlShadeCode.DataBind();               
               
            }
        }
        catch
        {
            throw;

        }
    }
    private DataTable GET_MOM_DATA(string Text, string MST_NAME)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            string CommandText = "select MST_CODE,MST_DESC from tx_Master_TRN where Del_Status=0 and MST_NAME=:MST_NAME and comp_code='" + oUserLoginDetail.COMP_CODE + "' ";
            string WhereClause = " and (UPPER(MST_CODE) like :SearchQuery OR UPPER(MST_DESC) like :SearchQuery)";
            string SortExpression = " order by MST_CODE asc";
            string SearchQuery = "%" + Text.ToUpper() + "%";
           return SaitexBL.Interface.Method.TX_MASTER_TRN.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, MST_NAME, oUserLoginDetail.COMP_CODE);
           
        }
        catch
        {
            throw;
        }
    }


    public void DisableformByFlag()
    {
        try
        {

            ddlW_Side.Enabled = false;
            ddlBOMProductType.Enabled = false;
            //cmbArticle.Enabled = false;
            //ddlBOMUOM.Enabled = false;
            ddlShadeCode.Enabled = false;
            cmbArticle.Enabled = false;
            txtArticleCode.Enabled = false;
            txtArticleDesc.Enabled = false;
            txtBOMRequiredQty.Enabled = false;
            BtnBOMSave.Enabled = false;
            BtnBOMCancel.Enabled = false;
            //chkFlag.Enabled = false;
            btnUpdate.Enabled = false;
            btnSave.Enabled = false;

        }
        catch
        {
            throw;
        }
    }

    public void InitialisePage()
    {

        try
        {
            if (string.Compare(PI_TYPE, "GRAY_WEAV", true) == 0)
            {
                grdBOM.Columns[1].Visible = true;
                tdWarpDet.Visible = false;
                tdWarpHeads.Visible = false;
            }
            else if (string.Compare(PI_TYPE, "FABR_PROC", true) == 0)
            {
                grdBOM.Columns[1].Visible = true;
                tdWarpDet.Visible = false;
                tdWarpHeads.Visible = false;
            }
            else if (string.Compare(PI_TYPE, "SEWING_THREAD", true) == 0)
            {
                grdBOM.Columns[1].Visible = false;
                tdWarpDet.Visible = false;
                tdWarpHeads.Visible = false;
            }
            else if (string.Compare(PI_TYPE, "YARN_DYEING", true) == 0)
            {
                grdBOM.Columns[1].Visible = false;
                tdWarpDet.Visible = false;
                tdWarpHeads.Visible = false;
            }
            else if (string.Compare(PI_TYPE, "YARN_SPINING", true) == 0)
            {
                grdBOM.Columns[1].Visible = false;
                tdWarpDet.Visible = false;
                tdWarpHeads.Visible = false;
                tdshadeDropDown.Visible = false;
                tdShadeCode.Visible = false;
                 
            }
            CreateBOMTable();
        }
        catch
        {
            throw;
        }
    }

    public void bindBOMBASE_ARTICAL_TYPE(string PI_TYPE)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.YRN_MST.GetBaseArticleType();
            if (PI_TYPE.Equals("YARN_SPINING"))
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "ARTICLE_TYPE='Poy'";
                if (dv != null && dv.Count > 0)
                {
                    ddlBOMProductType.Items.Clear();
                    ddlBOMProductType.DataSource = dv;
                    ddlBOMProductType.DataTextField = "ARTICLE_TYPE";
                    ddlBOMProductType.DataValueField = "ARTICLE_TYPE";
                    ddlBOMProductType.DataBind();
                    // ddlBOMProductType.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                if (dt != null && dt.Rows.Count > 0)
                {

                    ddlBOMProductType.Items.Clear();
                    ddlBOMProductType.DataSource = dt;
                    ddlBOMProductType.DataTextField = "ARTICLE_TYPE";
                    ddlBOMProductType.DataValueField = "ARTICLE_TYPE";
                    ddlBOMProductType.DataBind();
                    ddlBOMProductType.Items.Insert(0, new ListItem("Select", ""));
                }
            }
        }
        catch
        {
            throw;
        }


    }

  

    private void BindBOMBASIS()
    {
        //try
        //{

        //    DataTable dt = new DataTable();
        //    dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("MACHINE_BASIS", oUserLoginDetail.COMP_CODE);
        //    ddlBOMBasis.DataSource = dt;
        //    ddlBOMBasis.DataValueField = "MST_CODE";
        //    ddlBOMBasis.DataTextField = "MST_DESC";
        //    ddlBOMBasis.DataBind();
        //    //ddlBOMBASIS.Items.Insert(0, new ListItem("-------Select--------", ""));
        //    dt.Dispose();
        //    dt = null;
        //}
        //catch
        //{
        //    throw;

        //}

    }

    private void CreateBOMTable()
    {
        try
        {
            dtTRN_BOM = new DataTable();
            dtTRN_BOM.Columns.Add("UNIQUE_ID", typeof(int));
            dtTRN_BOM.Columns.Add("COMP_CODE", typeof(string));
            dtTRN_BOM.Columns.Add("BRANCH_CODE", typeof(string));
            dtTRN_BOM.Columns.Add("YEAR", typeof(int));
            dtTRN_BOM.Columns.Add("BUSINESS_TYPE", typeof(string));
            dtTRN_BOM.Columns.Add("PRODUCT_TYPE", typeof(string));
            dtTRN_BOM.Columns.Add("ORDER_CAT", typeof(string));
            dtTRN_BOM.Columns.Add("ORDER_TYPE", typeof(string));
            dtTRN_BOM.Columns.Add("ORDER_NO", typeof(string));
            dtTRN_BOM.Columns.Add("PI_TYPE", typeof(string));
            dtTRN_BOM.Columns.Add("PI_NO", typeof(string));
            dtTRN_BOM.Columns.Add("PRODUCT_CODE", typeof(string));
            dtTRN_BOM.Columns.Add("PRODUCT_SHADE_CODE", typeof(string));      
            dtTRN_BOM.Columns.Add("ARTICAL_TYPE", typeof(string));
            dtTRN_BOM.Columns.Add("ARTICAL_CODE", typeof(string));
            dtTRN_BOM.Columns.Add("ARTICAL_DESC", typeof(string));
            dtTRN_BOM.Columns.Add("ASS_ARTICAL_CODE", typeof(string));
            dtTRN_BOM.Columns.Add("ASS_ARTICAL_DESC", typeof(string));
            dtTRN_BOM.Columns.Add("SHADE_CODE", typeof(string));
            dtTRN_BOM.Columns.Add("W_SIDE", typeof(string));
            dtTRN_BOM.Columns.Add("BASE_ARTICAL_TYPE", typeof(string));
            dtTRN_BOM.Columns.Add("BASE_ARTICAL_DESC", typeof(string));
            dtTRN_BOM.Columns.Add("BASE_ARTICAL_CODE", typeof(string));
            dtTRN_BOM.Columns.Add("ASS_BASE_ARTICAL_CODE", typeof(string));
            dtTRN_BOM.Columns.Add("BASE_SHADE_CODE", typeof(string));
            dtTRN_BOM.Columns.Add("UOM", typeof(string));
            dtTRN_BOM.Columns.Add("UOM1", typeof(string));
            dtTRN_BOM.Columns.Add("UOM_BAIL", typeof(string));
            dtTRN_BOM.Columns.Add("BASIS", typeof(string));
            dtTRN_BOM.Columns.Add("VALUE_QTY", typeof(double));
            dtTRN_BOM.Columns.Add("REQ_QTY", typeof(double));
            dtTRN_BOM.Columns.Add("BOM_REMARKS", typeof(double));
            dtTRN_BOM.Columns.Add("TUSER", typeof(string));               
            Session["dtTRN_BOM"] = dtTRN_BOM;
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
            if (Session["dtTRN_BOM"] != null)
            {
                dtTRN_BOM = (DataTable)Session["dtTRN_BOM"];
            }
            else
            {
                CreateBOMTable();
            }

            DataView dv = new DataView(dtTRN_BOM);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("COMP_CODE='" + COMP_CODE + "' AND ");
            sb.Append("BRANCH_CODE='" + BRANCH_CODE + "' AND ");
            sb.Append("BUSINESS_TYPE='" + BUSINESS_TYPE + "' AND ");
            sb.Append("PRODUCT_TYPE='" + PRODUCT_TYPE + "' AND ");
            sb.Append("ORDER_CAT='" + ORDER_CAT + "' AND ");
            sb.Append("ORDER_TYPE='" + ORDER_TYPE + "' AND ");
            sb.Append("ORDER_NO='" + ORDER_NO + "' AND ");
            sb.Append("PI_TYPE='" + PI_TYPE + "' AND ");
            sb.Append("PI_NO='" + PI_NO + "' AND ");           
            //sb.Append("ARTICAL_CODE='" + ARTICAL_CODE + "' AND ");
            //sb.Append("SHADE_CODE='" + SHADE_CODE + "'");
            sb.Append("PRODUCT_CODE='" + ARTICAL_CODE + "' AND ");
            sb.Append("PRODUCT_SHADE_CODE='" + SHADE_CODE + "'");
            dv.RowFilter = sb.ToString();
            if (dv.Count > 0)
            {
                grdBOM.DataSource = dv;
                grdBOM.DataBind();
            }
            else
            {
                grdBOM.DataSource = null;
                grdBOM.DataBind();
                grdBOM.EmptyDataText = "No Data is available.";
            }
        }
        catch
        {
            throw;
        }
    }

    private void BindBOMGridfromDesignmaster()
    {
        try
        {
            if (Session["dtTRN_BOM"] != null)
            {
                dtTRN_BOM = (DataTable)Session["dtTRN_BOM"];
            }
            else
            {
                CreateBOMTable();
            }


            if (dtTRN_BOM != null && dtTRN_BOM.Rows.Count > 0)
            {
                grdBOM.DataSource = dtTRN_BOM;
                grdBOM.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlBOMProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            cmbArticle.Items.Clear();
            cmbArticle.SelectedText = string.Empty;
            cmbArticle.SelectedIndex = -1;
            txtArticleDesc.Text = string.Empty;
            txtArticleCode.Text = string.Empty;
            if (ddlBOMProductType.SelectedValue == "ITEM")
            {
                ltrShade.Text = "Item Cat";
                BindItemCatetory();
            }
            else
            {
                ltrShade.Text = "Shade Code";
                BindShadeCode();
            }
            // BindARTICAL_CODE();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in product type selection.See error log for detail."));
        }
    }

    protected void BindARTICAL_CODE()
    { 
        try
        {
            DataTable dtallbasedetail = SaitexBL.Interface.Method.YRN_MST.GetALLBaseArticleDetail();
            DataView dvBOMArticle = new DataView(dtallbasedetail);
            dvBOMArticle.RowFilter = "ARTICLE_TYPE='" + ddlBOMProductType.SelectedItem.ToString() + "'";
            if (dvBOMArticle != null && dvBOMArticle.Count > 0)
            {
                // cmbArticle.Items.Clear();
                // cmbArticle.DataSource = dvBOMArticle;
                // cmbArticle.DataValueField = "CODE";
                // cmbArticle.DataTextField = "ARTICLE_CODE";
                // cmbArticle.DataBind();
                // cmbArticle.Items.Insert(0, new ListItem("----Select---", ""));

            }
            else
            {
                // cmbArticle.Items.Clear();
                // cmbArticle.Items.Insert(0, new ListItem("----Select----", ""));
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

            if (Session["dtTRN_BOM"] != null)
            {
                dtTRN_BOM = (DataTable)Session["dtTRN_BOM"];
            }
            else
            {
                CreateBOMTable();
            }

            if (dtTRN_BOM.Rows.Count < 15)
            {
                if (txtBOMRequiredQty.Text != "")
                {
                    int UNIQUE_ID = 0;
                    if (ViewState["UNIQUE_ID"] != null)
                    {
                        UNIQUE_ID = int.Parse(ViewState["UNIQUE_ID"].ToString());
                    }
                    bool bb = SearchInBOMgrid(ddlW_Side.SelectedItem.ToString(), ddlBOMProductType.SelectedItem.ToString().Trim(), cmbArticle.SelectedText.ToString(), UNIQUE_ID); //ddlShadeCode.SelectedItem.ToString()
                    if (!bb)
                    {
                        if (UNIQUE_ID > 0)
                        {
                            DataView dv = new DataView(dtTRN_BOM);
                            dv.RowFilter = "PRODUCT_CODE='" + ARTICAL_CODE + "' and PI_TYPE='" + PI_TYPE + "' and UNIQUE_ID=" + UNIQUE_ID;
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();

                            sb.Append("COMP_CODE='" + COMP_CODE + "' AND ");
                            sb.Append("BRANCH_CODE='" + BRANCH_CODE + "' AND "); 
                            sb.Append("BUSINESS_TYPE='" + BUSINESS_TYPE + "' AND ");
                            sb.Append("PRODUCT_TYPE='" + PRODUCT_TYPE + "' AND ");
                            sb.Append("ORDER_CAT='" + ORDER_CAT + "' AND ");
                            sb.Append("ORDER_TYPE='" + ORDER_TYPE + "' AND ");
                            sb.Append("ORDER_NO='" + ORDER_NO + "' AND ");
                            sb.Append("PI_TYPE='" + PI_TYPE + "' AND ");
                            sb.Append("PI_NO='" + PI_NO + "' AND ");
                            //sb.Append("ARTICAL_CODE='" + ARTICAL_CODE + "' AND ");
                            //sb.Append("PRODUCT_SHADE_CODE='" + SHADE_CODE + "' AND ");
                            sb.Append("UNIQUE_ID='" + UNIQUE_ID + "'");

                            dv.RowFilter = sb.ToString();
                            if (dv.Count > 0)
                            {
                                double reqqty = 0;
                                double.TryParse(txtBOMRequiredQty.Text, out reqqty);
                                //dv[0]["VALUE_QTY"] = reqqty;
                                dv[0]["REQ_QTY"] = reqqty;                              
                                dtTRN_BOM.AcceptChanges();
                            }
                        }
                        else
                        {
                            DataRow dr = dtTRN_BOM.NewRow();
                            dr["UNIQUE_ID"] = dtTRN_BOM.Rows.Count + 1;
                            dr["COMP_CODE"] = COMP_CODE;
                            dr["BRANCH_CODE"] = BRANCH_CODE;
                            dr["BUSINESS_TYPE"] = BUSINESS_TYPE;
                            dr["PRODUCT_TYPE"] = PRODUCT_TYPE;
                            dr["ORDER_CAT"] = ORDER_CAT;
                            dr["ORDER_TYPE"] = ORDER_TYPE;
                            dr["ORDER_NO"] = ORDER_NO;
                            dr["PI_TYPE"] = PI_TYPE;
                            dr["PI_NO"] = PI_NO;
                            dr["PRODUCT_CODE"] = ARTICAL_CODE;
                            dr["PRODUCT_SHADE_CODE"] = SHADE_CODE;
                            dr["ARTICAL_TYPE"] = ddlBOMProductType.SelectedValue; ;
                            dr["ARTICAL_CODE"] = ARTICAL_CODE;//cmbArticle.SelectedValue;
                            dr["ARTICAL_DESC"] = ARTICAL_DESC;

                            string DESC = cmbArticle.SelectedValue;
                            string[] arrDesc = cmbArticle.SelectedValue.Split('@');
                            
                            if (ddlBOMProductType.SelectedValue.Equals("TEXTURISED YARN") && !PRODUCT_TYPE.Equals("TEXTURISED"))
                            {
                                dr["ASS_ARTICAL_CODE"] = cmbArticle.SelectedText;
                                dr["ASS_ARTICAL_DESC"] = arrDesc[1].ToString();
                            }
                            
                            else if (ddlBOMProductType.SelectedValue.Equals("DYED YARN") && !PRODUCT_TYPE.Equals("DYED"))
                            {
                                dr["ASS_ARTICAL_CODE"] = cmbArticle.SelectedText;
                                dr["ASS_ARTICAL_DESC"] = arrDesc[1].ToString();
                            }
                            
                            else if (ddlBOMProductType.SelectedValue.Equals("TWISTED YARN") && !PRODUCT_TYPE.Equals("TWISTED"))
                            {
                                dr["ASS_ARTICAL_CODE"] = cmbArticle.SelectedText;                              
                                dr["ASS_ARTICAL_DESC"] = arrDesc[1].ToString();
                            }
                            else if (ddlBOMProductType.SelectedValue.Equals("ITEM"))
                            {
                                dr["ASS_ARTICAL_CODE"] = cmbArticle.SelectedText;
                                dr["ASS_ARTICAL_DESC"] = arrDesc[1].ToString();
                            }

                            else if (ddlBOMProductType.SelectedValue.Equals("P.O.Y."))
                            {
                                dr["ASS_ARTICAL_CODE"] = cmbArticle.SelectedText;
                                dr["ASS_ARTICAL_DESC"] = arrDesc[1].ToString();

                            }
                            else
                            {
                                dr["ASS_ARTICAL_CODE"] = ASS_ARTICAL_CODE;
                                dr["ASS_ARTICAL_DESC"] = ASS_ARTICAL_DESC;
                            
                            }
                                
                           

                           
                            if (ddlBOMProductType.SelectedValue == "ITEM")
                            {
                                dr["SHADE_CODE"] = ddlShadeCode.SelectedValue;
                            }
                            else
                            {
                               // dr["SHADE_CODE"] = "WHITE";
                                dr["SHADE_CODE"] = "GREY"; 
                            }
                          
                            dr["W_SIDE"] = ddlW_Side.SelectedItem.ToString().Trim();
                            dr["BASE_ARTICAL_TYPE"] = ddlBOMProductType.SelectedItem.ToString().Trim();
                            dr["BASE_ARTICAL_CODE"] = txtArticleCode.Text.Trim();
                            dr["UOM"] = txtUom.Text.Trim();
                            dr["UOM1"] = txtUom.Text.Trim();
                            dr["UOM_BAIL"] = txtUom.Text.Trim(); 
                            dr["VALUE_QTY"] = txtBOMRequiredQty.Text;
                            dr["REQ_QTY"] = txtBOMRequiredQty.Text;
                            dr["TUSER"] = oUserLoginDetail.UserCode;
                            dr["BASE_ARTICAL_DESC"] = txtArticleDesc.Text.Trim();
                            dr["BASE_SHADE_CODE"] = ddlShadeCode.SelectedItem.ToString();
                            dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                            dtTRN_BOM.Rows.Add(dr);

                        }
                        RefresBOMRow();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Selected Product Type Already Added.Please Select Another');", true);
                    }
                    Session["dtTRN_BOM"] = dtTRN_BOM;
                    BindBOMGrid();
                }

                else
                {
                    Common.CommonFuction.ShowMessage("Please Enter Value Quantity");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("You have reached the limit of BOM Article. Only 15 Standard allowed in one Machine Process Master.");
            }

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving BOM Detail Row.\r\nSee error log for detail."));

        }
    }

    private void RefresBOMRow()
    {
        try
        {
            ddlW_Side.SelectedIndex = -1;
            ddlBOMProductType.SelectedIndex = -1;

            cmbArticle.SelectedIndex = -1;
            txtUom.Text = string.Empty;
            txtArticleCode.Text = string.Empty;
            txtArticleDesc.Text = string.Empty;

            txtBOMRequiredQty.Text = string.Empty;
            txtKgBail.Text = string.Empty;
            txtUom1.Text = string.Empty;

            ddlBOMProductType.Enabled = true;
            cmbArticle.Enabled = true;

            ViewState["UNIQUE_ID"] = null;
            ltrShade.Text = "Shade Code";
            BindShadeCode();
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

    protected void grdBOMArticleDetail_RowCommand(object sender, GridViewCommandEventArgs e)
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in BOM Grid RowCommand Selection.\r\nSee error log for detail."));
        }
    }

    private void  FillBOMByGrid(int UNIQUE_ID)
    {
        try
        {
            if (Session["dtTRN_BOM"] != null)
            {
                dtTRN_BOM = (DataTable)Session["dtTRN_BOM"];
            }
            else
            {
                CreateBOMTable();
            }
            DataView dv = new DataView(dtTRN_BOM);
            dv.RowFilter = "UNIQUE_ID=" + UNIQUE_ID;
            if (dv.Count > 0)
            {

                ddlBOMProductType.Enabled = false;
                cmbArticle.Enabled = false;
                ddlW_Side.SelectedIndex = ddlW_Side.Items.IndexOf(ddlW_Side.Items.FindByText(dv[0]["W_SIDE"].ToString()));
                ddlBOMProductType.SelectedIndex = ddlBOMProductType.Items.IndexOf(ddlBOMProductType.Items.FindByText(dv[0]["BASE_ARTICAL_TYPE"].ToString()));

                if (ddlBOMProductType.SelectedValue == "ITEM")
                {
                    ltrShade.Text = "Item Cat";
                    BindItemCatetory();
                }
                else
                {
                    ltrShade.Text = "Shade Code";
                    BindShadeCode();
                }

                // BindARTICAL_CODE();
                txtArticleCode.Text = dv[0]["BASE_ARTICAL_CODE"].ToString();
                txtUom.Text = dv[0]["UOM"].ToString();
                txtArticleDesc.Text = dv[0]["BASE_ARTICAL_DESC"].ToString();
                txtBOMRequiredQty.Text = dv[0]["REQ_QTY"].ToString();
                ddlShadeCode.SelectedIndex = ddlShadeCode.Items.IndexOf(ddlShadeCode.Items.FindByText(dv[0]["BASE_SHADE_CODE"].ToString()));
                txtUom1.Text = dv[0]["UOM1"].ToString();
                txtKgBail.Text = dv[0]["UOM_BAIL"].ToString(); 
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
            if (Session["dtTRN_BOM"] != null)
            {
                dtTRN_BOM = (DataTable)Session["dtTRN_BOM"];
            }
            else
            {
                CreateBOMTable();
            }
            if (grdBOM.Rows.Count == 1)
            {
                dtTRN_BOM.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtTRN_BOM.Rows)
                {
                    int iUNIQUE_ID = int.Parse(dr["UNIQUE_ID"].ToString());
                    if (iUNIQUE_ID == UNIQUE_ID)
                    {
                        dtTRN_BOM.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtTRN_BOM.Rows)
                {
                    iCount = iCount + 1;
                    dr["UNIQUE_ID"] = iCount;
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private bool SearchInBOMgrid(string sW_SIDE, string BASE_ARTICAL_TYPE, string BASE_ARTICAL_CODE, int UNIQUE_ID)//, string Shade_CodeS
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in grdBOM.Rows)
            {
                Label txtBOMW_SIDEs = (Label)grdRow.FindControl("txtBOMW_SIDE");
                Label txtBOMProductTypes = (Label)grdRow.FindControl("txtBOMProductType");
                Label txtBOMArticleCodes = (Label)grdRow.FindControl("txtBOMArticleCode");
               // Label txtShadeCode = (Label)grdRow.FindControl("txtShadeCode");
                Button lnkDelete = (Button)grdRow.FindControl("lnkBOMDelete");
                int iUNIQUE_ID = int.Parse(lnkDelete.CommandArgument.Trim());
                //&& txtShadeCode.Text == Shade_Code
                if (txtBOMW_SIDEs.Text.Trim() == sW_SIDE && txtBOMProductTypes.Text.Trim() == BASE_ARTICAL_TYPE && txtBOMArticleCodes.Text.Trim() == BASE_ARTICAL_CODE && UNIQUE_ID != iUNIQUE_ID )
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

    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            string BOM = string.Empty;
            string TextBoxBOM = string.Empty;
           
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:BindYRNSPIN_BOM('" + BOM + "','" + TextBoxBOM + "')", true);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in submitting delivery data.\r\nsee error log for detail."));
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        dtTRN_BOM = Session["dtTRN_BOM"] as DataTable;
        dtTRN_BOM.Rows[0]["SHADE_CODE"] = SHADE_CODE;
        dtTRN_BOM.Rows[0]["BASE_SHADE_CODE"] = "GREY";
        //dtTRN_BOM.Rows[0]["BASE_SHADE_FAMILY"] = "GREY";
        string msg = string.Empty;
        bool Result = SaitexDL.Interface.Method.OD_CAPTURE_MST.Delete_TRN_BOM(dtTRN_BOM, out msg);
        if (Result)
        {
           // msg += "BOM Order Saved successfully.";
            Common.CommonFuction.ShowMessage(msg);

            if (dtTRN_BOM != null && msg.Contains("BOM Order Saved successfully."))
            {
                dtTRN_BOM.Dispose();
                dtTRN_BOM = null;
                Session["dtTRN_BOM"] = null;
                Session["i"] = null;
            }
        }
        else
        {
            Common.CommonFuction.ShowMessage("Bom Data Saving Failed");
        }
    }
    

    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        string msg = string.Empty;
        SaitexDM.Common.DataModel.OD_CAPTURE_MST oOD_CAPTURE_MST = new SaitexDM.Common.DataModel.OD_CAPTURE_MST();
        oOD_CAPTURE_MST.COMP_CODE = COMP_CODE;
        oOD_CAPTURE_MST.BRANCH_CODE = BRANCH_CODE;
        oOD_CAPTURE_MST.BUSINESS_TYPE = BUSINESS_TYPE;
        oOD_CAPTURE_MST.PRODUCT_TYPE = PRODUCT_TYPE;
        oOD_CAPTURE_MST.ORDER_CAT = ORDER_CAT;
        oOD_CAPTURE_MST.ORDER_TYPE = ORDER_TYPE;
        oOD_CAPTURE_MST.ORDER_NO = ORDER_NO;
        oOD_CAPTURE_MST.YEAR = YEAR;
        bool status = false;
        if (chkFlag.Checked)
        {
            status = true;
        }
        else
        {
            status = false;
        }

        bool Result = SaitexDL.Interface.Method.OD_CAPTURE_MST.UpdateBOMflag(status, oOD_CAPTURE_MST, PI_NO, ASS_ARTICAL_CODE, SHADE_CODE, PI_TYPE);
        if (Result)
        {
            msg += "BOM Code Flag Updated";
            Common.CommonFuction.ShowMessage(msg);
        }
        else
        {
            Common.CommonFuction.ShowMessage("BOM Flag Updating Failed");
        }
    }

    protected void grdBOM_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {



                if (BOM_FLAG.Equals("1"))
                {
                    Button lnkBOMEdit = (Button)e.Row.FindControl("lnkBOMEdit");
                    Button lnkBOMDelete = (Button)e.Row.FindControl("lnkBOMDelete");

                    lnkBOMEdit.Visible = false;
                    lnkBOMDelete.Visible = false;

                }
                else if (BOM_FLAG.Equals("0"))
                {
                    Button lnkBOMEdit = (Button)e.Row.FindControl("lnkBOMEdit");
                    Button lnkBOMDelete = (Button)e.Row.FindControl("lnkBOMDelete");

                    lnkBOMEdit.Visible = true;
                    lnkBOMDelete.Visible = true;

                }

            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid RowDataBound. See Error log for detail"));
        }
    }

    protected void cmbArticle_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);

            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                cmbArticle.Items.Clear();

                cmbArticle.DataSource = data;
                cmbArticle.DataBind();
            }

            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Article  loading.\r\nSee error log for detail."));
            //lblMode.Text = ex.ToString();

        }
    }

    protected int GetItemsCount(string text)
    {
        string CommandText = " SELECT   *  FROM   (  SELECT   CODE, YARNDESC, article_type,UOM, UOM1,UOM_BAIL, CODE || '@'      || YARNDESC || '@' || UOM|| '@' ||UOM1|| '@' ||UOM_BAIL AS Combined      FROM   VW_GET_ALL_BASE_ARTICLE_DETAIL      WHERE      CODE LIKE :SearchQuery OR YARNDESC LIKE :SearchQuery OR UOM      LIKE :SearchQuery      ORDER BY   CODE) asd      WHERE   asd.article_type <>      '" + ddlBOMProductType.SelectedItem.ToString().Trim() + "'";
        string WhereClause = " ";
        string SortExpression = " ORDER BY CODE ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        return data.Rows.Count;
    }

    protected DataTable GetItems(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT   *  FROM   (  SELECT   CODE,   YARNDESC,   article_type,   UOM, UOM1,UOM_BAIL,  CODE || '@' || YARNDESC || '@' || UOM|| '@' ||UOM1|| '@' ||UOM_BAIL AS Combined   FROM   VW_GET_ALL_BASE_ARTICLE_DETAIL   WHERE      CODE LIKE :SearchQuery   OR YARNDESC LIKE :SearchQuery   OR UOM LIKE :SearchQuery          ORDER BY   CODE) asd WHERE   ROWNUM <= 15  and asd.article_type='" + ddlBOMProductType.SelectedItem.ToString().Trim() + "'";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += "  AND CODE NOT IN   (SELECT   CODE FROM   ( SELECT   CODE, YARNDESC, article_type, UOM,UOM1,UOM_BAIL, CODE || '@' || YARNDESC || '@' || UOM|| '@' ||UOM1|| '@' ||UOM_BAIL AS Combined  FROM   VW_GET_ALL_BASE_ARTICLE_DETAIL WHERE      CODE LIKE :SearchQuery OR YARNDESC LIKE :SearchQuery OR UOM LIKE :SearchQuery ORDER BY   CODE) asd  WHERE   ROWNUM <= " + startOffset + " AND asd.article_type =  '" + ddlBOMProductType.SelectedItem.ToString().Trim() + "')";
            }

            string SortExpression = " ORDER BY CODE";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected void cmbArticle_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            string cString = cmbArticle.SelectedValue.Trim();
            char[] splitter = { '@' };
            string[] arrString = cString.Split(splitter);
            string YARN_CODE = arrString[0].ToString();
            string YARN_DESC = arrString[1].ToString();
            string UOM = arrString[2].ToString();
            string UOM1 = arrString[3].ToString();
            string UOM_BAIL = arrString[4].ToString();
            //string TRANSFER_PRICE = arrString[3].ToString();
            txtArticleCode.Text = YARN_CODE;
            txtArticleDesc.Text = YARN_DESC;
            txtUom.Text = UOM;
            if (string.IsNullOrEmpty(UOM1))
            {
                txtUom1.Text = "N/A";
                txtKgBail.Text = "N/A";
            }
            else 
            {
                txtUom1.Text = UOM1;
                txtKgBail.Text = UOM_BAIL;
            }
            ddlShadeCode.SelectedIndex = ddlShadeCode.Items.IndexOf(ddlShadeCode.Items.FindByValue(UOM1));
           
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem Article Selection.\r\nSee error log for detail."));
            //lblMode.Text = ex.ToString();
        }

    }

    protected void grdBOM_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdBOM.EditIndex = e.NewEditIndex;
        BindBOMGrid();

    }
    protected void grdBOM_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)grdBOM.Rows[e.RowIndex];
            TextBox txtvalue = (TextBox)row.FindControl("txtEditValue");
            int value = Convert.ToInt32(txtvalue.Text);

            TextBox txtReqvalue = (TextBox)row.FindControl("txtEditReqValue");
            int reqvalue = Convert.ToInt32(txtReqvalue.Text);

            Label lblWarp = (Label)row.FindControl("txtBOMW_SIDE");
            string wrap = lblWarp.Text;

            Label lblarticle = (Label)row.FindControl("txtBOMArticleCode");
            string article = lblarticle.Text;

            Label lblShade = (Label)row.FindControl("txtShadeCode");
            string Shade = lblShade.Text;

            

            DataTable dt = Session["dtTRN_BOM"] as DataTable;

            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "W_SIDE='" + wrap + "' and BASE_ARTICAL_CODE='" + article + "' and BASE_SHADE_CODE='" + Shade + "'";
                if (dv != null && dv.Count > 0)
                {

                    dv[0]["VALUE_QTY"] = value;
                    dv[0]["REQ_QTY"] = reqvalue;
                    dt.AcceptChanges();

                    Session["dtTRN_BOM"] = dt;
                    BindBOMGrid();

                }
            }

            grdBOM.EditIndex = -1;
            BindBOMGrid();

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Row Updating.\r\nSee error log for detail."));

        }




    }
    protected void grdBOM_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {

    }
    protected void grdBOM_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdBOM.EditIndex = -1;
        BindBOMGrid();
    }



    public void  CreateBOM(string COMP_CODE, string BRANCH_CODE, string BASE_ARTICLE_CODE, double ORD_QTY)
    {
        DataTable DT = SaitexDL.Interface.Method.OD_CAPTURE_MST.CreateBOM(COMP_CODE, BRANCH_CODE, BASE_ARTICLE_CODE, ORD_QTY);
       
        ADD_ROW(DT);
        
        if (Session["dtTRN_BOM"] != null)
        {
            dtTRN_BOM = (DataTable)Session["dtTRN_BOM"];
        }
        // close code according looping BOM By Arun Sharma
        //int i = 0;
        //int _i=0;        
        //if (Session["i"] != null)
        //{
        //    int.TryParse(Session["i"].ToString(),out _i);            
        //}
        //i = _i;
        //if (i < dtTRN_BOM.Rows.Count)// for (i = _i; i < dtTRN_BOM.Rows.Count; ++i)
        //{
        //    Session["i"] = i+1;
        //    double _QTY = 0;
        //    double.TryParse(dtTRN_BOM.Rows[i]["REQ_QTY"].ToString(), out _QTY);
        //    CreateBOM(COMP_CODE, BRANCH_CODE, dtTRN_BOM.Rows[i]["BASE_ARTICAL_CODE"].ToString(), _QTY);
           
            
        //}
        BindBOMGrid();
    
    }

    public void ADD_ROW(DataTable DT)
    {
        if (DT != null && (DataTable)Session["dtTRN_BOM"] == null)
        {
            CreateBOMTable();           
        }
        else if (DT != null && (DataTable)Session["dtTRN_BOM"] != null)
        {
            dtTRN_BOM = (DataTable)Session["dtTRN_BOM"];
        }
        
        for (int i = 0; i < DT.Rows.Count; i++)
        {
            DataRow dr = dtTRN_BOM.NewRow();
            dr["UNIQUE_ID"] = dtTRN_BOM.Rows.Count + 1;
            dr["COMP_CODE"] = COMP_CODE;
            dr["BRANCH_CODE"] = BRANCH_CODE;
            dr["YEAR"] = YEAR;
            dr["BUSINESS_TYPE"] = BUSINESS_TYPE;
            dr["PRODUCT_TYPE"] = PRODUCT_TYPE;
            dr["ORDER_CAT"] = ORDER_CAT;
            dr["ORDER_TYPE"] = ORDER_TYPE;
            dr["ORDER_NO"] = ORDER_NO;
            dr["PI_TYPE"] = PI_TYPE;
            dr["PI_NO"] = PI_NO;
            dr["PRODUCT_CODE"] = ARTICAL_CODE;
            dr["PRODUCT_SHADE_CODE"] = SHADE_CODE;
            dr["ARTICAL_TYPE"] = DT.Rows[i]["ARTICAL_TYPE"].ToString();
            dr["ARTICAL_CODE"] = DT.Rows[i]["ARTICAL_CODE"].ToString();
            dr["ARTICAL_DESC"] = DT.Rows[i]["ARTICAL_DESC"].ToString();
            //dr["ASS_ARTICAL_CODE"] = ASS_ARTICAL_CODE;
            //dr["ASS_ARTICAL_DESC"] = ASS_ARTICAL_DESC;
            dr["ASS_ARTICAL_CODE"] = DT.Rows[i]["ASS_ARTICLE_CODE"].ToString();
            dr["ASS_ARTICAL_DESC"] = DT.Rows[i]["ASS_ARTICLE_DESC"].ToString();
          
                //dr["SHADE_CODE"] = "WHITE";
            dr["SHADE_CODE"] = "GREY"; 

            dr["W_SIDE"] = ddlW_Side.SelectedItem.ToString().Trim();            
            dr["UOM"] = DT.Rows[i]["UOM"].ToString();
            dr["UOM1"] = DT.Rows[i]["UOM"].ToString();
            dr["UOM_BAIL"] = DT.Rows[i]["UOM"].ToString();
            dr["VALUE_QTY"] = DT.Rows[i]["VALUE_QTY"].ToString();
            dr["REQ_QTY"] = DT.Rows[i]["REQ_QTY"].ToString();
            dr["TUSER"] = oUserLoginDetail.UserCode;
            dr["BASE_ARTICAL_TYPE"] = DT.Rows[i]["BASE_ARTICLE_TYPE"].ToString();
            dr["BASE_ARTICAL_CODE"] = DT.Rows[i]["BASE_ARTICLE_CODE"].ToString();
            dr["BASE_ARTICAL_DESC"] = DT.Rows[i]["BASE_ARTICLE_DESC"].ToString();
            dr["ASS_BASE_ARTICAL_CODE"] = DT.Rows[i]["ASS_BASE_ARTICLE_CODE"].ToString();
            //dr["BASE_SHADE_CODE"] = "WHITE";
            //dr["BASE_SHADE_CODE"] = DT.Rows[i]["BASE_SHADE_CODE"].ToString();
            dr["SHADE_CODE"] = "GREY"; 
            dtTRN_BOM.Rows.Add(dr);
        }
        Session["dtTRN_BOM"] = dtTRN_BOM;

    
    }


    
}
