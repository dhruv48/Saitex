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
using System.Collections.Generic;

public partial class Module_Yarn_SalesWork_Controls_Yarn_QC_CheckingQuery : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["LoginDetail"] != null)
            {
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                if (!IsPostBack)
                {
                    Initialisepage();
                }
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }

    }

    private void Initialisepage()
    {
        bindSTD_Type("");
        BlanksControls();
        bindQcGrid();
    }
    private void BlanksControls()
    {
        try
        {
            ddlTRNNumber.SelectedIndex = -1;
            ddlyarncode.SelectedIndex = -1;
            ddlStdType.SelectedIndex = -1;
            ddlstatus.SelectedIndex = 0;
        }
        catch
        {
            throw;
        }

    }

    protected void imgbtnAddNew_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Yarn/SalesWork/Pages/Yarn_QC_Checking.aspx");
    }


    private DataTable bindQcGrid()
    {
        int TRN_YEAR = 0, TRN_NUMB = 0;
        string  Yarn_Code = string.Empty, Status = "", Std_Type="";
        if (ddlTRNNumber.SelectedIndex > -1)
        {
           
            int.TryParse(hdYEAR.Value, out TRN_YEAR);
            int.TryParse(hdTRN_NUMB.Value, out TRN_NUMB);
        }

        if (ddlyarncode.SelectedIndex > -1)
        {
            Yarn_Code = ddlyarncode.SelectedText.Trim();
        }

        if (ddlStdType.SelectedIndex > 0)
        {
            Std_Type = ddlStdType.SelectedValue.Trim();
        }
        
        if (ddlstatus.SelectedItem.Text != "All")
        {
            Status = ddlstatus.SelectedItem.Text;
        }

        DataTable  dt = SaitexBL.Interface.Method.YRN_IR_MST.GetQCChecking_Query(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, "",TRN_NUMB, "", Yarn_Code, "", 0, TRN_YEAR, Std_Type, 0, "", "", Status, "", "");   
        if (dt != null && dt.Rows.Count > 0)
        {
            gvMaterialReceiptApproval.DataSource = dt;
            gvMaterialReceiptApproval.DataBind();
            lblTotalRecord.Text = dt.Rows.Count.ToString();
        }
        else
        {
            gvMaterialReceiptApproval.DataSource = dt;
            gvMaterialReceiptApproval.DataBind();
            lblTotalRecord.Text = "0";
        }
        return dt;
    }



   

    private void bindSTD_Type(string SearchQuery)
    {
        try
        {
            DataTable dt = GET_MOM_DATA(SearchQuery, "Y_STD_TYPE");
            ddlStdType.Items.Clear();
            ddlStdType.DataSource = dt;
            ddlStdType.DataTextField = "MST_CODE";
            ddlStdType.DataValueField = "MST_CODE";
            ddlStdType.DataBind();
            ddlStdType.Items.Insert(0, new ListItem("-Select-", "-1"));
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

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnPrint.Attributes.Add("OnClick", "window.confirm('Are you sure to print the record ?')");
        imgbtnExit.Attributes.Add("OnClick", "window.confirm('Are you sure to exit the record ?')");
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

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            int TRN_YEAR = 0, TRN_NUMB = 0;
            string  Yarn_Code = string.Empty, Status = "", Std_Type = "";
            if (ddlTRNNumber.SelectedIndex > -1)
            {
           
                int.TryParse(hdYEAR.Value, out TRN_YEAR);
                int.TryParse(hdTRN_NUMB.Value, out TRN_NUMB);
            }

            if (ddlyarncode.SelectedIndex > -1)
            {
                Yarn_Code = ddlyarncode.SelectedText.Trim();
            }

            if (ddlStdType.SelectedIndex > 0)
            {
                Std_Type = ddlStdType.SelectedValue.Trim();
            }

            if (ddlstatus.SelectedItem.Text != "All")
            {
                Status = ddlstatus.SelectedItem.Text;
            }
            string URL = "../Reports/Yarn_QC_CheckingReport.aspx?TRN_YEAR=" + TRN_YEAR + "&TRN_NUMB=" + TRN_NUMB + "&Y_Code=" + Yarn_Code +"&STD_TYPE=" + Std_Type + "&STATUS=" + Status;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);

        
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    protected void gvMaterialReceiptApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvMaterialReceiptApproval.PageIndex = e.NewPageIndex;
            gvMaterialReceiptApproval.DataBind();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void imgbtnExport_Click(object sender, ImageClickEventArgs e)
    {
        string strFilename = "Yarn_QC_Checking_List_" + DateTime.Now.ToString() + ".xls";
        Common.CommonFuction.ExporttoExcel(bindQcGrid(), strFilename, "Yarn QC Checking List", oUserLoginDetail.VC_COMPANYNAME);

    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Initialisepage();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }


    protected void ddlStdType_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindQcGrid();
    }
    protected void ddlTRNNumber_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            Obout.ComboBox.ComboBox thisTextBox = (Obout.ComboBox.ComboBox)sender;
            thisTextBox.SelectedIndex = 0;
            thisTextBox.Items.Clear();

            DataTable data = new DataTable();
            data = GetReceiving(e.Text.ToUpper(), e.ItemsOffset, 10);
            thisTextBox.DataSource = data;
            thisTextBox.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetReceivingCount(e.Text.ToUpper());

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Standard No selection.\r\nsee error log for detail"));
        }
    }

    protected DataTable GetReceiving(string text, int startOffset, int numberOfItems)
    {
        try
        {

            string CommandText = "SELECT   * FROM   (SELECT   * FROM   (SELECT   DISTINCT c.TRN_NUMB,c.YEAR, (c.TRN_TYPE||'@'||c.YEAR)as combined  FROM   YRN_QC a, YRN_IR_TRN c, YRN_MST d WHERE a.BRANCH_CODE = c.BRANCH_CODE AND a.COMP_CODE = c.COMP_CODE AND a.TRN_YEAR = c.YEAR AND a.TRN_TYPE = c.TRN_TYPE AND a.TRN_NUMB = c.TRN_NUMB AND c.BRANCH_CODE = d.BRANCH_CODE AND c.COMP_CODE = d.COMP_CODE  AND c.YARN_CODE = d.YARN_CODE  AND NVL (d.QC_REQUIRED, 0) = '1'  AND c.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND c.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "') WHERE   (TRN_NUMB LIKE :SearchQuery)) WHERE   ROWNUM <= 15";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " and TRN_NUMB not in (SELECT TRN_NUMB from (SELECT  TRN_NUMB FROM   (SELECT   DISTINCT c.TRN_NUMB,c.YEAR, (c.TRN_TYPE||'@'||c.YEAR)as combined FROM   YRN_QC a, YRN_IR_TRN c, YRN_MST d WHERE a.BRANCH_CODE = c.BRANCH_CODE AND a.COMP_CODE = c.COMP_CODE AND a.TRN_YEAR = c.YEAR  AND a.TRN_TYPE = c.TRN_TYPE AND a.TRN_NUMB = c.TRN_NUMB AND c.BRANCH_CODE = d.BRANCH_CODE AND c.COMP_CODE = d.COMP_CODE AND c.YARN_CODE = d.YARN_CODE AND NVL (d.QC_REQUIRED, 0) = '1'  AND c.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND c.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "') WHERE   (TRN_NUMB LIKE :SearchQuery)) where rownum<='" + startOffset + "')";
            }

            string SortExpression = "  ORDER BY TRN_NUMB DESC ";
            string SearchQuery = "%" + text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
        }
        catch
        {
            throw;
        }
    }

    protected int GetReceivingCount(string text)
    {
        try
        {
            string CommandText = "SELECT   * FROM   (SELECT   * FROM   (SELECT   DISTINCT c.TRN_NUMB,c.YEAR, (c.TRN_TYPE||'@'||c.YEAR)as combined  FROM   YRN_QC a, YRN_IR_TRN c, YRN_MST d WHERE a.BRANCH_CODE = c.BRANCH_CODE AND a.COMP_CODE = c.COMP_CODE  AND a.TRN_YEAR = c.YEAR AND a.TRN_TYPE = c.TRN_TYPE AND a.TRN_NUMB = c.TRN_NUMB AND c.BRANCH_CODE = d.BRANCH_CODE AND c.COMP_CODE = d.COMP_CODE  AND c.YARN_CODE = d.YARN_CODE  AND NVL (d.QC_REQUIRED, 0) = '1'   AND c.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND c.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "') WHERE   (TRN_NUMB LIKE :SearchQuery)) ";
            string SearchQuery = "%" + text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, "", "", "", SearchQuery, "").Rows.Count;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlTRNNumber_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            int TRN_NUMB = 0;
            int.TryParse(ddlTRNNumber.SelectedText.ToString().Trim(), out TRN_NUMB);
            string data = ddlTRNNumber.SelectedValue.Trim();
            string[] arr = data.Split('@');
            if (arr.Length > 0)
            {
                int YEAR = 0;
                string TRN_TYPE = arr[0].ToString();
                int.TryParse(arr[1].ToString(), out YEAR);
                hdTRN_NUMB.Value = TRN_NUMB.ToString();
                hdTRN_TYPE.Value = TRN_TYPE;
                hdYEAR.Value = YEAR.ToString();
                bindQcGrid();
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Yarn QC Standard  selection.\r\nsee error log for detail"));
        }
    }

    protected void ddlyarncode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {

        try
        {
            DataTable data = GetYarnData(e.Text.ToUpper(), e.ItemsOffset);
            ddlyarncode.Items.Clear();
            ddlyarncode.DataSource = data;
            ddlyarncode.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetYarnCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Yarn Code selection.\r\nSee error log for detail."));
        }

    }


    private DataTable GetYarnData(string Text, int startOffset)
    {
        try
        {

            string CommandText = "SELECT   * FROM   (  SELECT   * FROM   (SELECT   d.YARN_CODE, d.YARN_DESC  FROM   YRN_MST d WHERE   NVL (d.QC_REQUIRED, 0) = '1' AND d.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND d.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "') WHERE   (UPPER (YARN_CODE) LIKE :SearchQuery OR UPPER (YARN_DESC) LIKE :SearchQuery) ORDER BY   YARN_CODE) WHERE   ROWNUM <= 15";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND YARN_CODE NOT IN ( select YARN_CODE from ( SELECT   YARN_CODE FROM   (SELECT   d.YARN_CODE, d.YARN_DESC  FROM   YRN_MST d WHERE   NVL (d.QC_REQUIRED, 0) = '1' AND d.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND d.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "') WHERE   (UPPER (YARN_CODE) LIKE :SearchQuery OR UPPER (YARN_DESC) LIKE :SearchQuery) ORDER BY   YARN_CODE)    WHERE  ROWNUM <= " + startOffset + " )";
            }
            string SortExpression = " order by YARN_CODE";
            string SearchQuery = "%" + Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");


        }
        catch
        {
            throw;
        }
    }

    protected int GetYarnCount(string text)
    {

        string CommandText = "SELECT   * FROM   (  SELECT   * FROM   (SELECT   d.YARN_CODE, d.YARN_DESC  FROM   YRN_MST d WHERE   NVL (d.QC_REQUIRED, 0) = '1' AND d.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND d.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "') WHERE   (UPPER (YARN_CODE) LIKE :SearchQuery OR UPPER (YARN_DESC) LIKE :SearchQuery) ORDER BY   YARN_CODE)";
        string SearchQuery = "%" + text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, "", "", "", SearchQuery, "").Rows.Count;

    }


    protected void ddlyarncode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindQcGrid();

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Yarn Code Selection.\r\nSee error log for detail."));

        }

    }
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindQcGrid();
    }
}
