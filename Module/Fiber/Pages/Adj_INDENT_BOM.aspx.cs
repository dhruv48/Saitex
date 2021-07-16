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

public partial class Module_Fiber_Pages_Adj_INDENT_BOM : System.Web.UI.Page
{
    private  DataTable dtBOMIndent = null;
    private  string TextBoxId="";
    private  string IND_TYPE;
    private  int IND_NUMB=0;
    private  string PI_NO;
    private string ItemCode;
    
         
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        if (Request.QueryString["IND_NUMB"] != null)
        {
            IND_NUMB = int.Parse(Request.QueryString["IND_NUMB"].ToString());
        }
        if (Request.QueryString["IND_TYPE"] != null)
        {
            IND_TYPE = Request.QueryString["IND_TYPE"].ToString();
        }
        if (Request.QueryString["PI_NO"] != null)
        {
            PI_NO = Request.QueryString["PI_NO"].ToString();
        }
        if (Request.QueryString["ItemCodeId"] != null)
        {
             ItemCode = Request.QueryString["ItemCodeId"].ToString();
           
        }
        if (Request.QueryString["TextBoxId"] != null)
        {
            TextBoxId = Request.QueryString["TextBoxId"].ToString();
        }

        if (!IsPostBack)
        {          
       
                GetDataForItemAdjustment(ItemCode);           
            
        }
    }
   
    public void GetDataForItemAdjustment(string FIBER_CODE)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLogindetail =(SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dtAdjBOM = SaitexBL.Interface.Method.FIBER_IND_MST.GetAdjBOMByItemCode1(oUserLogindetail.COMP_CODE, oUserLogindetail.CH_BRANCHCODE, IND_TYPE, IND_NUMB, FIBER_CODE, PI_NO);
            if (dtAdjBOM != null && dtAdjBOM.Rows.Count > 0)
            {
                grdAdjBom.DataSource = dtAdjBOM;
                grdAdjBom.DataBind();
                lblAdjITEMBOMCode.Text = FIBER_CODE;

            }
            else
            {
                lblAdjBomError.Text = "No Record exists for adjustment for provided Fiber";
            }
        }
        catch (Exception ex)
        {
            lblAdjBomError.Text = ex.Message;
        }
    }
    protected void txtAdjustedBOMQTY_TextChanged1(object sender, EventArgs e)
    {
        try
        {
            Label txtFinalQty = (Label)grdAdjBom.FooterRow.FindControl("txtTotalAdjBOMQTY");
            txtFinalQty.Text = GetFinalTotalOfItemAdjBOM().ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private double GetFinalTotalOfItemAdjBOM()
    {
        try
        {
            double FinalTotal = 0;
            for (int iLoop = 0; iLoop < grdAdjBom.Rows.Count; iLoop++)
            {
                TextBox txtAdjBOMQTY = (TextBox)grdAdjBom.Rows[iLoop].FindControl("txtAdjBOMQTY");
                Label lblAdjRemQty = (Label)grdAdjBom.Rows[iLoop].FindControl("lblAdjRemQty");
                double Total = double.Parse(txtAdjBOMQTY.Text);
                if (Total <= double.Parse(lblAdjRemQty.Text))
                {
                    FinalTotal = FinalTotal + Total;
                }
                else
                {
                    lblAdjBomError.Text = "Entered quantity is larger then indent quantity";
                    txtAdjBOMQTY.Text = 0.ToString();
                    break;
                }
            }
            return FinalTotal;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
         
    protected void grdAdjBom_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblAdjRemQty = (Label)row.FindControl("lblAdjRemQty");
            TextBox txtAdjBOMQTY = (TextBox)row.FindControl("txtAdjBOMQTY");
            txtAdjBOMQTY.Text = lblAdjRemQty.Text;
            Label txtFinalQTY = (Label)grdAdjBom.FooterRow.FindControl("txtTotalAdjBOMQTY");
            txtFinalQTY.Text = GetFinalTotalOfItemAdjBOM().ToString();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);

        }
    }
    protected void btnAdjBOMItem_Click(object sender, EventArgs e)
    
    {
        try
        {
            double TotalQTY = 0;
            DataTable dtBOMIndent = createdatatableforadjustment(out TotalQTY);
            Session["dtBOMIndent"] = dtBOMIndent;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:GetRowValue('" + TotalQTY + "','" + TextBoxId + "')", true);
        }
        catch (Exception ex)
        {
            lblAdjBomError.Text = ex.Message;
        }
    }
    public DataTable createdatatableforadjustment(out double TOTALQTY)
    {
        try
        {
            TOTALQTY = 0.0;
            DataTable dtBOMIndent = new DataTable();
            if (Session["dtBOMIndent"] == null)
            {
                dtBOMIndent = createAdjTable();
            }
            else
            {
                dtBOMIndent.Rows.Clear();
                dtBOMIndent = (DataTable)Session["dtBOMIndent"];
                if (!dtBOMIndent.Columns.Contains("ADJ_QTY"))
                {
                    dtBOMIndent.Columns.Add("ADJ_QTY", typeof(int));
                }
            }
            for (int iLoop = 0; iLoop < grdAdjBom.Rows.Count; iLoop++)
            {
                Label lblAdjBOM_BUSINESS_TYPE = (Label)grdAdjBom.Rows[iLoop].FindControl("lblAdjBOM_BUSINESS_TYPE");
                Label lblAdjBOM_PRODUCT_TYPE = (Label)grdAdjBom.Rows[iLoop].FindControl("lblAdjBOM_PRODUCT_TYPE");
                Label lblAdjBOM_ORDER_CAT = (Label)grdAdjBom.Rows[iLoop].FindControl("lblAdjBOM_ORDER_CAT");
                Label lblAdjBOM_ORDER_TYPE = (Label)grdAdjBom.Rows[iLoop].FindControl("lblAdjBOM_ORDER_TYPE");
                Label lblAdjBOM_ORDER_NO = (Label)grdAdjBom.Rows[iLoop].FindControl("lblAdjBOM_ORDER_NO");
                Label lblAdjBOM_PI_TYPE = (Label)grdAdjBom.Rows[iLoop].FindControl("lblAdjBOM_PI_TYPE");
                Label lblAdjBOM_PI_NO = (Label)grdAdjBom.Rows[iLoop].FindControl("lblAdjBOM_PI_NO");
                Label lblAdjBOM_ARTICAL_CODE = (Label)grdAdjBom.Rows[iLoop].FindControl("lblAdjBOM_ARTICAL_CODE");
                Label lblAdjFIBER_DESC = (Label)grdAdjBom.Rows[iLoop].FindControl("lblAdjFIBER_DESC");
                Label lblAdjBOM_W_SIDE = (Label)grdAdjBom.Rows[iLoop].FindControl("lblAdjBOM_W_SIDE");
                Label lblAdjBOM_BASE_ARTICAL_TYPE = (Label)grdAdjBom.Rows[iLoop].FindControl("lblAdjBOM_BASE_ARTICAL_TYPE");
                
                Label lblAdjBOM_BASE_ARTICAL_CODE = (Label)grdAdjBom.Rows[iLoop].FindControl("lblAdjBOM_BASE_ARTICAL_CODE");
                Label lblAdjYARN_DESC = (Label)grdAdjBom.Rows[iLoop].FindControl("lblAdjYARN_DESC");
                TextBox txtAdjBOMQTY = (TextBox)grdAdjBom.Rows[iLoop].FindControl("txtAdjBOMQTY");

                double ADJ_QTY = double.Parse(txtAdjBOMQTY.Text.Trim());
                string ItemCode = lblAdjITEMBOMCode.Text.Trim();
                    //lblAdjBOMItemCode.Text.Trim();

                DataView dv = new DataView(dtBOMIndent);
                dv.RowFilter = "BOM_COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' and BOM_BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' and BOM_BUSINESS_TYPE ='" + lblAdjBOM_BUSINESS_TYPE.Text + "' and BOM_PRODUCT_TYPE ='" + lblAdjBOM_PRODUCT_TYPE.Text + "' and BOM_ORDER_CAT ='" + lblAdjBOM_ORDER_CAT.Text + "' and BOM_ORDER_TYPE ='" + lblAdjBOM_ORDER_TYPE.Text + "' and BOM_ORDER_NO ='" + lblAdjBOM_ORDER_NO.Text + "' and FIBER_DESC ='" + lblAdjBOM_PI_TYPE.Text + "' and BOM_PI_NO ='" + lblAdjBOM_PI_NO.Text + "' and BOM_ARTICAL_CODE ='" + lblAdjBOM_ARTICAL_CODE.Text + "' and YARN_DESC ='" + lblAdjBOM_W_SIDE.Text + "' and BOM_BASE_ARTICAL_TYPE ='" + lblAdjBOM_BASE_ARTICAL_TYPE.Text + "' and BOM_BASE_ARTICAL_CODE ='" + lblAdjBOM_BASE_ARTICAL_CODE.Text + "'";
                if (dv.Count == 0)
                {
                    DataRow dr = dtBOMIndent.NewRow();

                    dr["BOM_COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                    dr["BOM_BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                    dr["BOM_BUSINESS_TYPE"] = lblAdjBOM_BUSINESS_TYPE.Text;
                    dr["BOM_PRODUCT_TYPE"] = lblAdjBOM_PRODUCT_TYPE.Text;
                    dr["BOM_ORDER_CAT"] = lblAdjBOM_ORDER_CAT.Text;
                    dr["BOM_ORDER_TYPE"] = lblAdjBOM_ORDER_TYPE.Text;
                    dr["BOM_ORDER_NO"] = lblAdjBOM_ORDER_NO.Text;
                    dr["BOM_PI_TYPE"] = lblAdjBOM_PI_TYPE.Text;
                    dr["BOM_PI_NO"] = lblAdjBOM_PI_NO.Text;
                    dr["BOM_ARTICAL_CODE"] = lblAdjBOM_ARTICAL_CODE.Text;
                    dr["FIBER_DESC"] = lblAdjFIBER_DESC.Text;
                    dr["BOM_W_SIDE"] = lblAdjBOM_W_SIDE.Text;
                    dr["BOM_BASE_ARTICAL_TYPE"] = lblAdjBOM_BASE_ARTICAL_TYPE.Text;
                    dr["BOM_BASE_ARTICAL_CODE"] = lblAdjBOM_BASE_ARTICAL_CODE.Text;
                    dr["YARN_DESC"] = lblAdjYARN_DESC.Text;
                    dr["ADJ_TYPE"] = "INDENT";
                    dr["ADJ_QTY"] = ADJ_QTY;

                    dtBOMIndent.Rows.Add(dr);
                }
                else
                {
                    dv[0]["ADJ_QTY"] = ADJ_QTY;
                    dv[0]["ADJ_TYPE"] = "INDENT";
                }
                dtBOMIndent.AcceptChanges();
                TOTALQTY = TOTALQTY + ADJ_QTY;
            }
            return dtBOMIndent;
            }

         

        
        catch (Exception ex)
        {
            throw ex;

        }
    }
    private DataTable createAdjTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("BOM_COMP_CODE", typeof(string));
        dt.Columns.Add("BOM_BRANCH_CODE", typeof(string));
        dt.Columns.Add("BOM_BUSINESS_TYPE", typeof(string));
        dt.Columns.Add("BOM_PRODUCT_TYPE", typeof(string));
        dt.Columns.Add("BOM_ORDER_CAT", typeof(string));
        dt.Columns.Add("BOM_ORDER_TYPE", typeof(string));
        dt.Columns.Add("BOM_ORDER_NO", typeof(string));
        dt.Columns.Add("BOM_PI_TYPE", typeof(string));
        dt.Columns.Add("FIBER_DESC", typeof(string));
        dt.Columns.Add("BOM_PI_NO", typeof(string));
        dt.Columns.Add("BOM_ARTICAL_CODE", typeof(string));
        dt.Columns.Add("YARN_DESC", typeof(string));
        dt.Columns.Add("BOM_W_SIDE", typeof(string));
        dt.Columns.Add("BOM_BASE_ARTICAL_TYPE", typeof(string));
        dt.Columns.Add("BOM_BASE_ARTICAL_CODE", typeof(string));
        dt.Columns.Add("REQ_QTY", typeof(double));
        dt.Columns.Add("ADJ_TYPE", typeof(string));
        dt.Columns.Add("ADJ_QTY", typeof(double));

        return dt;
    }
}
