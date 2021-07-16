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

public partial class Module_OrderDevelopment_Pages_ProcessRoute : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.TX_MAC_PROC_MST oTX_MAC_PROC_MST = new SaitexDM.Common.DataModel.TX_MAC_PROC_MST();
    SaitexDM.Common.DataModel.TX_PRO_STN_MST oTX_PRO_STN_MST = new SaitexDM.Common.DataModel.TX_PRO_STN_MST();
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

   
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
    private  string PROS_ROUTE_CODE = string.Empty;
    private  string PROCESS_ROUTE_FLAG = string.Empty;
    private int YEAR = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
             if (Request.QueryString["COMP_CODE"] != null)
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

                if (Request.QueryString["ARTICAL_CODE"] != null)
                {
                    ARTICAL_CODE = Request.QueryString["ARTICAL_CODE"].Trim();
                }

                if (Request.QueryString["SHADE_CODE"] != null)
                {
                    SHADE_CODE = Request.QueryString["SHADE_CODE"].ToString();
                }
                if (Request.QueryString["PROCESS_ROUTE_FLAG"] != null)
                {
                    PROCESS_ROUTE_FLAG = Request.QueryString["PROCESS_ROUTE_FLAG"].ToString();
                }
                if (Request.QueryString["YEAR"] != null)
                {
                    int.TryParse(Request.QueryString["YEAR"].ToString(), out YEAR);
                }
                if (Request.QueryString["PROS_ROUTE_CODE"] != null)
                {
                    PROS_ROUTE_CODE = Request.QueryString["PROS_ROUTE_CODE"].ToString();
                }
            if (!IsPostBack)
            {
                     if (PROCESS_ROUTE_FLAG.Equals("1"))
                    {
                        CheckBox1.Checked = true;
                        DisableformByFlag();

                    }
                    else if (PROCESS_ROUTE_FLAG.Equals("0"))
                    {
                        CheckBox1.Checked = false;

                    }
                     InitialisePage();
                     if (!string.IsNullOrEmpty(PROS_ROUTE_CODE))
                     {
                         BindProcessRoot();
                         
                         oTX_PRO_STN_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                         oTX_PRO_STN_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                         oTX_PRO_STN_MST.YEAR = YEAR ;
                         oTX_PRO_STN_MST.PROS_ROUTE_CODE = PROS_ROUTE_CODE;
                         var dtProcessingTrn = SaitexBL.Interface.Method.TX_PRO_STN_MST.GetProcessingStandarProcessRouteCode(oTX_PRO_STN_MST);
                         if (dtProcessingTrn != null && dtProcessingTrn.Rows.Count > 0)
                         {
                             BindProcessingStandardDetailGrid(dtProcessingTrn);

                         }
                     }
                
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nsee error log for detail."));
        }
    }
    public void DisableformByFlag()
    {
        try
        {

            ddlProcessRoot.Enabled = false;
            btnSave.Enabled = false;
            btnupdateflag.Enabled = false;
            CheckBox1.Enabled = false;
            

        }
        catch
        {
            throw;
        }
    }
    private void BindProcessRoot()
    {
        try
        {
            ddlProcessRoot.Items.Clear();
            SaitexDM.Common.DataModel.TX_PRO_STN_MST oTX_PRO_STN_MST = new SaitexDM.Common.DataModel.TX_PRO_STN_MST();
            oTX_PRO_STN_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_PRO_STN_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_PRO_STN_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            var dtProcessRoot = SaitexBL.Interface.Method.TX_PRO_STN_MST.GetProcessingMaster(oTX_PRO_STN_MST);
            ddlProcessRoot.DataSource = dtProcessRoot;
            ddlProcessRoot.DataTextField = "PROS_ROUTE_CODE";
            ddlProcessRoot.DataValueField = "PROS_ROUTE_CODE";
            ddlProcessRoot.DataBind();
            ddlProcessRoot.Items.Insert(0,new ListItem("--Select--",""));
            ddlProcessRoot.SelectedIndex = ddlProcessRoot.Items.IndexOf(ddlProcessRoot.Items.FindByValue(PROS_ROUTE_CODE));
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
            BindProcessRoot();
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
       
        bool Result = SaitexDL.Interface.Method.OD_CAPTURE_MST.UpdateProcesscodeInTran_TRN_COST(ddlProcessRoot.SelectedItem.ToString(),oOD_CAPTURE_MST,PI_NO,ARTICAL_CODE,SHADE_CODE, PI_TYPE  );
        if (Result)
        {
            msg += "Proces Code Order Saved successfully.";
            Common.CommonFuction.ShowMessage(msg);
           
        }
        else
        {
            Common.CommonFuction.ShowMessage("Proces Code Data Saving Failed");
        }
    }    
    protected void ddlProcessRoot_SelectedIndexChanged(object sender, EventArgs e)
    {
        oTX_PRO_STN_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
        oTX_PRO_STN_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
        oTX_PRO_STN_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
        oTX_PRO_STN_MST.PROS_ROUTE_CODE = ddlProcessRoot.SelectedItem.ToString();
        var dtProcessingTrn = SaitexBL.Interface.Method.TX_PRO_STN_MST.GetProcessingStandarProcessRouteCode(oTX_PRO_STN_MST);
        if (dtProcessingTrn != null && dtProcessingTrn.Rows.Count > 0)
        {

            BindProcessingStandardDetailGrid(dtProcessingTrn);


        }
        else
        {
            Common.CommonFuction.ShowMessage("Dear !! No Process Defined For Current Login Branch Of Selected Route");   
        }


    }    
    private void BindProcessingStandardDetailGrid(DataTable dtProcessingTrn)
    {
        try
        {
            gvProcessingStandardMaster.DataSource = dtProcessingTrn;
            gvProcessingStandardMaster.DataBind();


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnupdateflag_Click(object sender, EventArgs e)
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
        if (CheckBox1.Checked)
        {
            status = true;


        }
        else
        {
            status = false;
        }

        bool Result = SaitexDL.Interface.Method.OD_CAPTURE_MST.UpdateProcesscodeflag(status,oOD_CAPTURE_MST, PI_NO, ARTICAL_CODE, SHADE_CODE, PI_TYPE);
        if (Result)
        {
            msg += "Proces Code Flag Updated";
            Common.CommonFuction.ShowMessage(msg);

        }
        else
        {
            Common.CommonFuction.ShowMessage("Proces Code Data Saving Failed");
        }
    }
}
