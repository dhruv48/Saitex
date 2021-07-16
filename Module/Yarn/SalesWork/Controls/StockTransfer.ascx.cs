using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using errorLog;
using Common;
using Obout.ComboBox;
using Obout.Interface;

public partial class Module_Yarn_SalesWork_Controls_StockTransfer : System.Web.UI.UserControl
{
    private SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private DataTable dtDetailTBL = null;
    private string STISSUE_TYPE = "IYS06";
    private string STRECEIPT_TYPE = "RYS06";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                InitialisePage();
            }
        }
        catch (Exception ex)
        {

            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }

    protected override void OnInit(EventArgs e)
    {
        ddlReceiptCompany.AutoPostBack = true;
        ddlReceiptCompany.OnTextChanged += new CommonControls_LOV_CompanyLov.RefreshDataGridView(ddlReceiptCompany_OnTextChanged);
        ddlShift.AutoPostBack = false;
        ddlReceiptBranch.AutoPostBack = true;
        ddlReceiptBranch.OnTextChanged += new CommonControls_LOV_BranchLov.RefreshDataGridView(ddlReceiptBranch_OnTextChanged);
        ddlReceiptDepartment.AutoPostBack = true;
        ddlReceiptDepartment.OnTextChanged += new CommonControls_LOV_DepartmentLOV.RefreshDataGridView(ddlReceiptBranch_OnTextChanged);
        base.OnInit(e);
    }

    //void ddlReceiptDepartment_OnTextChanged(string Value, string Text)
    //{
    //    //BindReceiveNumb();
    //}
    void ddlReceiptBranch_OnTextChanged(string Value, string Text)
    {
        BindReceiveNumb();
    }

    void ddlReceiptCompany_OnTextChanged(string Value, string Text)
    {
        ddlReceiptBranch.LoadData(Value);
        BindReceiveNumb();
    }

    private void InitialisePage()
    {
        try
        {
            BindDepartment(ddlToStore);
            BindDepartment(ddlFromStore);
            BindDropDown(ddlFromLocation);
            BindDropDown(ddlToLocation);

            ddlFromLocation.SelectedIndex = ddlFromLocation.Items.IndexOf(ddlFromLocation.Items.FindByText(oUserLoginDetail.VC_BRANCHNAME));
            ddlToLocation.SelectedIndex = ddlToLocation.Items.IndexOf(ddlToLocation.Items.FindByText(oUserLoginDetail.VC_BRANCHNAME));
            ddlFromStore.SelectedIndex = ddlFromStore.Items.IndexOf(ddlFromStore.Items.FindByText(oUserLoginDetail.VC_DEPARTMENTNAME));
            ddlToStore.SelectedIndex = ddlToStore.Items.IndexOf(ddlToStore.Items.FindByText(oUserLoginDetail.VC_DEPARTMENTNAME));

            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            tdSave.Visible = true;
            
            Session["dtItemReceipt"] = null;
            ActivateSaveMode();
            Blankrecords();
            CreateDataTable();
            txtIssueDate.Text = System.DateTime.Now.ToShortDateString();
            RefreshDetailRow();

        }
        catch
        {
            throw;
        }
    }


    private void BindDropDown(DropDownList ddl)
    {
        DataTable dt = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("WAREHOUSE_LOCATION", oUserLoginDetail.COMP_CODE);
        if (dt != null && dt.Rows.Count > 0)
        {
            ddl.Items.Clear();
            ddl.DataSource = dt;
            ddl.DataTextField = "MST_DESC";
            ddl.DataValueField = "MST_DESC";
            ddl.DataBind();
        }
        else
        {
            ddl.DataSource = null;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem(oUserLoginDetail.VC_BRANCHNAME, oUserLoginDetail.VC_BRANCHNAME));

        }
        ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(oUserLoginDetail.VC_BRANCHNAME));

    }

    private void BindDepartment(DropDownList ddl)
    {
        try
        {
            ddl.Items.Clear();
            DataTable dt = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("WAREHOUSE_STORE", oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddl.DataSource = dt;
                ddl.DataTextField = "MST_DESC";
                ddl.DataValueField = "MST_DESC";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("--Select--", ""));
            }
            else
            {
                DataTable dtDepartment = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
                if (dtDepartment != null && dtDepartment.Rows.Count > 0)
                {
                    ddl.DataSource = dtDepartment;
                    ddl.DataTextField = "DEPT_NAME";
                    ddl.DataValueField = "DEPT_NAME";
                    ddl.DataBind();
                }
            }
            ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(oUserLoginDetail.VC_DEPARTMENTNAME));
        }
        catch
        {
            throw;
        }
    }


    private void ActivateSaveMode()
    {
        try
        {
            //txtIssueNumb.Text = string.Empty;
            ddlTRNNumber.SelectedIndex = -1;
            ddlTRNNumber.SelectedText = string.Empty;
            ddlTRNNumber.SelectedValue = string.Empty;
            ddlTRNNumber.Items.Clear();
            ddlTRNNumber.Visible = false;
            BindIssueNumb();
            BindReceiveNumb();
            txtIssueNumb.Visible = true;

        }
        catch
        {
            throw;
        }
    }

    private void Blankrecords()
    {
        try
        {
            STISSUE_TYPE = "IYS06";
            STRECEIPT_TYPE = "RYS06";
            txtDocDate.Text = "";
            txtDocNo.Text = "";
            txtIssueDate.Text = "";
            txtRemarks.Text = "";
            txtVehicleNo.Text = "";

            //ddlShift.SelectedIndex = 0;
            txtIssueDepartment.Text = "";

            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }

            if (dtDetailTBL != null)
                dtDetailTBL.Rows.Clear();

            BindGridFromDataTable();

            lblMode.Text = "You are in Save Mode";


            txtIssueCompany.Text = oUserLoginDetail.VC_COMPANYNAME;
            txtIssueBranch.Text = oUserLoginDetail.VC_BRANCHNAME;
            txtIssueDepartment.Text = oUserLoginDetail.VC_DEPARTMENTNAME;

            ddlReceiptCompany.SetIndexByValue(oUserLoginDetail.COMP_CODE);
            ddlReceiptBranch.LoadData(ddlReceiptCompany.SelectedValue);
            ddlReceiptBranch.SetIndexByValue(oUserLoginDetail.CH_BRANCHCODE);
            ddlReceiptDepartment.SelectedValue = oUserLoginDetail.VC_DEPARTMENTCODE;

            BindIssueNumb();

            BindReceiveNumb();

            lblTransporterCode.Text = string.Empty;
            txtTransporter.Text = string.Empty;
            txtTransporterCode.SelectedIndex = -1;
            txtFormType.Text = string.Empty;
            txtFormNo.Text = string.Empty;

            tdDelete.Visible = false;
            tdSave.Visible = true;
            tdUpdate.Visible = false;
        }
        catch
        {
            throw;
        }
    }

    private void BindIssueNumb()
    {
        try
        {

            txtIssueNumb.Text = GetNewTRNNumb(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, STISSUE_TYPE);
        }
        catch { throw; }
    }

    private void BindReceiveNumb()
    {
        try
        {
            txtReceiveNumb.Text = GetNewTRNNumb(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, STRECEIPT_TYPE);

        }
        catch { throw; }
    }

    private string GetNewTRNNumb(string Comp_code, string Branch_code, string ISSUE_TYPE)
    {
        try
        {
            SaitexDM.Common.DataModel.YRN_IR_MST oTX_YRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();
            oTX_YRN_IR_MST.TRN_TYPE = ISSUE_TYPE;
            oTX_YRN_IR_MST.COMP_CODE = Comp_code;
            oTX_YRN_IR_MST.BRANCH_CODE = Branch_code;
            oTX_YRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            return SaitexBL.Interface.Method.YRN_IR_MST.GetNewTRNNumber(oTX_YRN_IR_MST);
        }
        catch
        {
            throw;
        }
    }

    private void CreateDataTable()
    {
        try
        {
            dtDetailTBL = new DataTable();
            dtDetailTBL.Columns.Add("UNIQUEID", typeof(int));
            dtDetailTBL.Columns.Add("TRNNUMB", typeof(int));
            dtDetailTBL.Columns.Add("PO_COMP_CODE", typeof(string));
            dtDetailTBL.Columns.Add("PO_BRANCH", typeof(string));
            dtDetailTBL.Columns.Add("PO_TYPE", typeof(string));
            dtDetailTBL.Columns.Add("PO_NUMB", typeof(int));
            dtDetailTBL.Columns.Add("PO_YEAR", typeof(int));
            dtDetailTBL.Columns.Add("YARN_CODE", typeof(string));
            dtDetailTBL.Columns.Add("YARN_DESC", typeof(string));
            dtDetailTBL.Columns.Add("SHADE_FAMILY", typeof(string));
            dtDetailTBL.Columns.Add("SHADE_CODE", typeof(string));
            dtDetailTBL.Columns.Add("UOM", typeof(string));
            dtDetailTBL.Columns.Add("DATE_OF_MFG", typeof(DateTime));
            dtDetailTBL.Columns.Add("TRN_QTY", typeof(double));
            dtDetailTBL.Columns.Add("BASIC_RATE", typeof(double));
            dtDetailTBL.Columns.Add("FINAL_RATE", typeof(double));
            dtDetailTBL.Columns.Add("AMOUNT", typeof(double));
            dtDetailTBL.Columns.Add("COST_CODE", typeof(string));
            dtDetailTBL.Columns.Add("MAC_CODE", typeof(string));
            dtDetailTBL.Columns.Add("REMARKS", typeof(string));
            dtDetailTBL.Columns.Add("QCFLAG", typeof(string));
            dtDetailTBL.Columns.Add("PI_NO", typeof(string));
            dtDetailTBL.Columns.Add("NO_OF_UNIT", typeof(double));
            dtDetailTBL.Columns.Add("UOM_OF_UNIT", typeof(string));
            dtDetailTBL.Columns.Add("WEIGHT_OF_UNIT", typeof(double));

            dtDetailTBL.Columns.Add("LOT_NO", typeof(string));
            dtDetailTBL.Columns.Add("GRADE", typeof(string));
            dtDetailTBL.Columns.Add("GROSS_WT", typeof(double));
            dtDetailTBL.Columns.Add("TARE_WT", typeof(double ));
            dtDetailTBL.Columns.Add("CARTONS", typeof(double));
            dtDetailTBL.Columns.Add("JOBER", typeof(string));
        }
        catch
        {
            throw;
        }
    }

    private void BindGridFromDataTable()
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }

            if (dtDetailTBL == null)
                CreateDataTable();
            grdMaterialItemIssue.DataSource = dtDetailTBL;
            grdMaterialItemIssue.DataBind();
        }
        catch
        {
            throw;
        }
    }

    protected void txtTransporterCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetLOVForTransporter(e.Text, e.ItemsOffset);
            txtTransporterCode.Items.Clear();
            txtTransporterCode.DataSource = data;
            txtTransporterCode.DataTextField = "PRTY_CODE";
            txtTransporterCode.DataValueField = "Address";
            txtTransporterCode.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetTransporterCount(e.Text);
        }

        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in transporter selection.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetLOVForTransporter(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1,PRTY_GRP_CODE FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)=upper('Transporter') and ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE,PRTY_GRP_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE FROM TX_VENDOR_MST  WHERE PRTY_CODE LIKE :SearchQuery  OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)=upper('Transporter') and ROWNUM <= " + startOffset + ")";
            }
            string SortExpression = " order by PRTY_CODE";
            string SearchQuery = Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

        }
        catch
        {
            throw;
        }
    }

    protected int GetTransporterCount(string text)
    {

        string CommandText = "SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1 FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)=upper('Transporter') ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;

    }

    protected void txtTransporterCode_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            txtTransporter.Text = txtTransporterCode.SelectedValue;
            lblTransporterCode.Text = txtTransporterCode.SelectedText;
        }

        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in transporter selection.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }

    protected void cmbPOITEM_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetItemData(e.Text.ToUpper(), e.ItemsOffset);
            txtICODE.Items.Clear();
            txtICODE.DataSource = data;
            txtICODE.DataTextField = "YARN_CODE";
            txtICODE.DataValueField = "TRN_DATA";
            txtICODE.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetItemsCount(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for material detail.\r\nSee error log for detail."));
        }
    }

    protected DataTable GetItemData(string text, int Startoffset)
    {
        try
        {
            //string CommandText = "SELECT   *  FROM   (SELECT   *   FROM   (  SELECT   DISTINCT ASD.YARN_CODE, B.YARN_DESC,C.SHADE AS SHADE_CODE,C.SHADE_FAMILY ,  (ASD.YARN_CODE   || '@'|| b.YARN_DESC|| '@'||c.SHADE_FAMILY||'@'|| c.SHADE)   TRN_DATA  FROM   (SELECT   a.YEAR,  a.COMP_CODE,    a.BRANCH_CODE,  a.YARN_CODE,  A.SHADE_FAMILY,  A.SHADE_CODE,  c.conf_flag,NVL (a.TRN_QTY, 0) AS TRN_QTY,NVL (a.ISS_QTY, 0) AS TRN_QTY_ADJ,  NVL (a.TRN_QTY, 0) - NVL (a.ISS_QTY, 0)    AS REMQTY  FROM   YRN_IR_TRN a, YRN_IR_MST c WHERE       SUBSTR (a.trn_type, 1, 1) IN ('R','O') AND A.YEAR = c.YEAR     AND A.COMP_CODE = c.COMP_CODE           AND A.BRANCH_CODE = c.BRANCH_CODE    AND A.TRN_NUMB = c.TRN_NUMB    AND A.TRN_TYPE = c.TRN_TYPE                AND A.LOCATION = C.LOCATION    AND A.STORE = C.STORE  AND a.YEAR =  " + oUserLoginDetail.DT_STARTDATE.Year + " AND a.Comp_Code = '" + oUserLoginDetail.COMP_CODE + "'             AND a.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.LOCATION = '" + ddlFromLocation.SelectedItem.ToString() + "'            AND A.STORE = '" + ddlFromStore.SelectedItem.ToString() + "' )           asd,  YRN_MST B,YRN_MST_COLOR C    WHERE      B.YARN_CODE=C.YARN_CODE     AND  asd.conf_flag = '1'        AND REMQTY > 0          AND ASD.YARN_CODE = B.YARN_CODE   AND ASD.SHADE_CODE=C.SHADE        AND ASD.SHADE_FAMILY=C.SHADE_FAMILY          ORDER BY   asd.YARN_CODE ASC)  WHERE   (LTRIM (RTRIM (YARN_CODE)) LIKE :SearchQuery  OR LTRIM (RTRIM (YARN_desc)) LIKE :SearchQuery OR LTRIM (RTRIM (SHADE_FAMILY)) LIKE :SearchQuery OR LTRIM (RTRIM (SHADE_CODE)) LIKE :SearchQuery)) WHERE   ROWNUM <= 15";

            string CommandText = "SELECT   *  FROM   (SELECT   *   FROM   (  SELECT   DISTINCT ASD.YARN_CODE, B.YARN_DESC,C.SHADE AS SHADE_CODE,C.SHADE_FAMILY , a.ASS_YARN_DESC, (ASD.YARN_CODE   || '@'|| b.YARN_DESC|| '@'||c.SHADE_FAMILY||'@'|| c.SHADE)   TRN_DATA  FROM   (SELECT   a.YEAR,  a.COMP_CODE,    a.BRANCH_CODE,  a.YARN_CODE,  A.SHADE_FAMILY,  A.SHADE_CODE,  c.conf_flag,NVL (a.TRN_QTY, 0) AS TRN_QTY,NVL (a.ISS_QTY, 0) AS TRN_QTY_ADJ,  NVL (a.TRN_QTY, 0) - NVL (a.ISS_QTY, 0)    AS REMQTY  FROM   YRN_IR_TRN a, YRN_IR_MST c WHERE       SUBSTR (a.trn_type, 1, 1) IN ('R','O') AND A.YEAR = c.YEAR     AND A.COMP_CODE = c.COMP_CODE           AND A.BRANCH_CODE = c.BRANCH_CODE    AND A.TRN_NUMB = c.TRN_NUMB    AND A.TRN_TYPE = c.TRN_TYPE           AND a.YEAR =  " + oUserLoginDetail.DT_STARTDATE.Year + " AND a.Comp_Code = '" + oUserLoginDetail.COMP_CODE + "'             AND a.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.LOCATION = '" + ddlFromLocation.SelectedItem.ToString() + "'            AND A.STORE = '" + ddlFromStore.SelectedItem.ToString() + "' )           asd,  YRN_MST B,YRN_MST_COLOR C ,YRN_ASSOCATED_MST A   WHERE      B.YARN_CODE=C.YARN_CODE     AND  asd.conf_flag = '1'        AND REMQTY > 0          AND ASD.YARN_CODE = B.YARN_CODE   AND ASD.SHADE_CODE=C.SHADE        AND ASD.SHADE_FAMILY=C.SHADE_FAMILY  AND b.YARN_CODE = A.YARN_CODE        ORDER BY   asd.YARN_CODE ASC)  WHERE   (LTRIM (RTRIM (YARN_CODE)) LIKE :SearchQuery  OR LTRIM (RTRIM (YARN_desc)) LIKE :SearchQuery OR LTRIM (RTRIM (SHADE_FAMILY)) LIKE :SearchQuery OR LTRIM (RTRIM (SHADE_CODE)) LIKE :SearchQuery)) WHERE   1=1";

            string whereClause = string.Empty;
            if (Startoffset != 0)
            {
                //whereClause += " AND TRN_DATA NOT IN ( SELECT TRN_DATA FROM (SELECT TRN_DATA FROM (  SELECT   DISTINCT ASD.YARN_CODE, B.YARN_DESC,C.SHADE AS SHADE_CODE,C.SHADE_FAMILY ,  (ASD.YARN_CODE   || '@'|| b.YARN_DESC|| '@'||c.SHADE_FAMILY||'@'|| c.SHADE)   TRN_DATA  FROM   (SELECT   a.YEAR,  a.COMP_CODE,    a.BRANCH_CODE,  a.YARN_CODE,  A.SHADE_FAMILY,  A.SHADE_CODE,  c.conf_flag,NVL (a.TRN_QTY, 0) AS TRN_QTY,NVL (a.ISS_QTY, 0) AS TRN_QTY_ADJ,  NVL (a.TRN_QTY, 0) - NVL (a.ISS_QTY, 0)    AS REMQTY  FROM   YRN_IR_TRN a, YRN_IR_MST c WHERE       SUBSTR (a.trn_type, 1, 1) IN ('R','O') AND A.YEAR = c.YEAR     AND A.COMP_CODE = c.COMP_CODE           AND A.BRANCH_CODE = c.BRANCH_CODE    AND A.TRN_NUMB = c.TRN_NUMB    AND A.TRN_TYPE = c.TRN_TYPE                AND A.LOCATION = C.LOCATION    AND A.STORE = C.STORE  AND a.YEAR =  " + oUserLoginDetail.DT_STARTDATE.Year + " AND a.Comp_Code = '" + oUserLoginDetail.COMP_CODE + "'             AND a.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.LOCATION = '" + ddlFromLocation.SelectedItem.ToString() + "'            AND A.STORE = '" + ddlFromStore.SelectedItem.ToString() + "' )           asd,  YRN_MST B,YRN_MST_COLOR C    WHERE      B.YARN_CODE=C.YARN_CODE     AND  asd.conf_flag = '1'        AND REMQTY > 0          AND ASD.YARN_CODE = B.YARN_CODE   AND ASD.SHADE_CODE=C.SHADE        AND ASD.SHADE_FAMILY=C.SHADE_FAMILY          ORDER BY   asd.YARN_CODE ASC) WHERE  (LTRIM (RTRIM (YARN_CODE)) LIKE :SearchQuery  OR LTRIM (RTRIM (YARN_desc)) LIKE :SearchQuery OR LTRIM (RTRIM (SHADE_FAMILY)) LIKE :SearchQuery OR LTRIM (RTRIM (SHADE_CODE)) LIKE :SearchQuery)) WHERE ROWNUM <= " + Startoffset + ")";




                whereClause += " AND TRN_DATA NOT IN ( SELECT TRN_DATA FROM (SELECT TRN_DATA FROM (  SELECT   DISTINCT ASD.YARN_CODE, B.YARN_DESC,C.SHADE AS SHADE_CODE,C.SHADE_FAMILY , a.ASS_YARN_DESC, (ASD.YARN_CODE   || '@'|| b.YARN_DESC|| '@'||c.SHADE_FAMILY||'@'|| c.SHADE)   TRN_DATA  FROM   (SELECT   a.YEAR,  a.COMP_CODE,    a.BRANCH_CODE,  a.YARN_CODE,  A.SHADE_FAMILY,  A.SHADE_CODE,  c.conf_flag,NVL (a.TRN_QTY, 0) AS TRN_QTY,NVL (a.ISS_QTY, 0) AS TRN_QTY_ADJ,  NVL (a.TRN_QTY, 0) - NVL (a.ISS_QTY, 0)    AS REMQTY  FROM   YRN_IR_TRN a, YRN_IR_MST c WHERE       SUBSTR (a.trn_type, 1, 1) IN ('R','O') AND A.YEAR = c.YEAR     AND A.COMP_CODE = c.COMP_CODE           AND A.BRANCH_CODE = c.BRANCH_CODE    AND A.TRN_NUMB = c.TRN_NUMB    AND A.TRN_TYPE = c.TRN_TYPE                  AND a.YEAR =  " + oUserLoginDetail.DT_STARTDATE.Year + " AND a.Comp_Code = '" + oUserLoginDetail.COMP_CODE + "'             AND a.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.LOCATION = '" + ddlFromLocation.SelectedItem.ToString() + "'            AND A.STORE = '" + ddlFromStore.SelectedItem.ToString() + "' )           asd,  YRN_MST B,YRN_MST_COLOR C, YRN_ASSOCATED_MST A   WHERE      B.YARN_CODE=C.YARN_CODE     AND  asd.conf_flag = '1'        AND REMQTY > 0          AND ASD.YARN_CODE = B.YARN_CODE   AND ASD.SHADE_CODE=C.SHADE        AND ASD.SHADE_FAMILY=C.SHADE_FAMILY    AND b.YARN_CODE = A.YARN_CODE      ORDER BY   asd.YARN_CODE ASC) WHERE  (LTRIM (RTRIM (YARN_CODE)) LIKE :SearchQuery  OR LTRIM (RTRIM (YARN_desc)) LIKE :SearchQuery OR LTRIM (RTRIM (SHADE_FAMILY)) LIKE :SearchQuery OR LTRIM (RTRIM (SHADE_CODE)) LIKE :SearchQuery)) WHERE ROWNUM <= " + Startoffset + ")";


            }
            string SortExpression = " ORDER BY YARN_CODE asc";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

        }
        catch
        {
            throw;
        }
    }

    protected int GetItemsCount(string text)
    {
        string CommandText = " SELECT   *  FROM   (SELECT   *   FROM   (  SELECT   DISTINCT ASD.YARN_CODE, B.YARN_DESC,C.SHADE AS SHADE_CODE,C.SHADE_FAMILY ,  (ASD.YARN_CODE   || '@'|| b.YARN_DESC|| '@'||c.SHADE_FAMILY||'@'|| c.SHADE)   TRN_DATA  FROM   (SELECT   a.YEAR,  a.COMP_CODE,    a.BRANCH_CODE,  a.YARN_CODE,  A.SHADE_FAMILY,  A.SHADE_CODE,  c.conf_flag,NVL (a.TRN_QTY, 0) AS TRN_QTY,NVL (a.ISS_QTY, 0) AS TRN_QTY_ADJ,  NVL (a.TRN_QTY, 0) - NVL (a.ISS_QTY, 0)    AS REMQTY  FROM   YRN_IR_TRN a, YRN_IR_MST c WHERE       SUBSTR (a.trn_type, 1, 1) IN ('R','O') AND A.YEAR = c.YEAR     AND A.COMP_CODE = c.COMP_CODE           AND A.BRANCH_CODE = c.BRANCH_CODE    AND A.TRN_NUMB = c.TRN_NUMB    AND A.TRN_TYPE = c.TRN_TYPE                AND A.LOCATION = C.LOCATION    AND A.STORE = C.STORE  AND a.YEAR =  " + oUserLoginDetail.DT_STARTDATE.Year + " AND a.Comp_Code = '" + oUserLoginDetail.COMP_CODE + "'             AND a.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.LOCATION = '" + ddlFromLocation.SelectedItem.ToString() + "'            AND A.STORE = '" + ddlFromStore.SelectedItem.ToString() + "' )           asd,  YRN_MST B,YRN_MST_COLOR C    WHERE      B.YARN_CODE=C.YARN_CODE     AND  asd.conf_flag = '1'        AND REMQTY > 0          AND ASD.YARN_CODE = B.YARN_CODE   AND ASD.SHADE_CODE=C.SHADE        AND ASD.SHADE_FAMILY=C.SHADE_FAMILY          ORDER BY   asd.YARN_CODE ASC)  WHERE   (LTRIM (RTRIM (YARN_CODE)) LIKE :SearchQuery  OR LTRIM (RTRIM (YARN_desc)) LIKE :SearchQuery OR LTRIM (RTRIM (SHADE_FAMILY)) LIKE :SearchQuery OR LTRIM (RTRIM (SHADE_CODE)) LIKE :SearchQuery))  ";
        string WhereClause = " ";
        string SortExpression = " ORDER BY YARN_CODE asc";
        string SearchQuery = text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;

    }

    protected void cmbPOITEM_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            string cString = txtICODE.SelectedValue.Trim();
            string[] arrString = cString.Split('@'); 
            txtItemCode.Text = txtICODE.SelectedText;
            txtDESC.Text = arrString[1].ToString();
            txtShadeFamily.Text = arrString[2].ToString();
            txtShadeCode.Text=arrString[3].ToString();

            GetItemDetail(txtICODE.SelectedText);
            btnAdjRec.Focus();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selecting data.\r\nSee error log for detail."));
        }
    }

    private void GetItemDetail(string Item_Code)
    {
        try
        {
            int UNIQUEID = 0;
            if (ViewState["UNIQUEID"] != null)
                UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
            if (!SearchItemCodeInGrid(Item_Code, UNIQUEID))
            {
                DataTable dt = SaitexBL.Interface.Method.YRN_MST.GetItemDetailByItemCode(Item_Code, oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.CH_BRANCHCODE, ddlFromStore.SelectedItem.ToString(), ddlFromLocation.SelectedItem.ToString(),txtShadeFamily.Text,txtShadeCode.Text);

                if (dt != null && dt.Rows.Count > 0)
                {                   
                    txtDESC.Text = dt.Rows[0]["YARN_DESC"].ToString().Trim();
                    txtUNIT.Text = dt.Rows[0]["UOM"].ToString().Trim();
                    txtQTY.Text = "0";
                    txtBasicRate.Text = dt.Rows[0]["OP_RATE"].ToString();
                    CalculateAmount();
                }
            }
            else
            {
                RefreshDetailRow();
                CommonFuction.ShowMessage("Item Already Included");
            }
        }
        catch
        {
            throw;
        }
    }

    protected void btnAdjRec_Click(object sender, EventArgs e)
    {
        try
        {
            string URL = "Yrn_Recipt_Adjustment.aspx";
            URL = URL + "?ItemCodeId=" + txtItemCode.Text.Trim();
            URL = URL + "&TextBoxId=" + txtQTY.ClientID;
            URL = URL + "&txtBasicRate=" + txtBasicRate.ClientID;
            URL = URL + "&AmountId=" + txtAmount.ClientID;            
            URL = URL + "&ChallanNo=" + txtIssueNumb.Text.Trim();
            URL = URL + "&TRN_TYPE=" + STISSUE_TYPE;
            URL = URL + "&LOCATION=" + ddlFromLocation.SelectedItem.ToString();
            URL = URL + "&STORE=" + ddlFromStore.SelectedItem.ToString();
            URL = URL + "&SHADE_FAMILY=" + txtShadeFamily.Text ;
            URL = URL + "&SHADE_CODE=" + txtShadeCode.Text ;
            URL = URL + "&txtQtyUnit=" + txtNoOfUnit.ClientID;
            URL = URL + "&txtQtyUom=" + txtUnitUome.ClientID;
            URL = URL + "&txtQtyWeight=" + txtWeightOfUnit.ClientID;
            URL = URL + "&PI_NO=NA";
          

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=no,menubar=no,width=950,height=420,left=100,top=200');", true);
            txtQTY.ReadOnly = false;
            txtAmount.ReadOnly = false;
            txtBasicRate.ReadOnly = false;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in adjusting Receiving.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtQTY_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CalculateAmount();
            txtBasicRate.ReadOnly = true;
            txtAmount.ReadOnly = true;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in adjusting Receiving.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void CalculateAmount()
    {
        try
        {
            double Qty = 0;
            double Rate = 0;
            double Amount = 0;
            double.TryParse(CommonFuction.funFixQuotes(txtQTY.Text.Trim()), out Qty);
            double.TryParse(CommonFuction.funFixQuotes(txtBasicRate.Text.Trim()), out Rate);
            double.TryParse(CommonFuction.funFixQuotes(txtAmount.Text.Trim()), out Amount);
            Amount = Rate * Qty;
            txtAmount.Text = Amount.ToString();
            txtBasicRate.Text = Rate.ToString();
            txtQTY.Text = Qty.ToString();
        }
        catch
        {
            throw;
        }
    }

    protected void btnsaveTRNDetail_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }
            if (dtDetailTBL == null)
                CreateDataTable();

            if (txtItemCode.Text != "" && txtQTY.Text != "" && txtBasicRate.Text != "")
            {
                int UNIQUEID = 0;
                if (ViewState["UNIQUEID"] != null)
                    UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
                bool bb = SearchItemCodeInGrid(txtICODE.SelectedText.Trim(), UNIQUEID);
                if (!bb)
                {
                    double Qty = 0;
                    double.TryParse(txtQTY.Text.Trim(), out Qty);
                    if (Qty > 0)
                    {
                        if (UNIQUEID > 0)
                        {
                            DataView dv = new DataView(dtDetailTBL);
                            dv.RowFilter = "UNIQUEID=" + UNIQUEID;
                            if (dv.Count > 0)
                            {
                                dv[0]["PO_NUMB"] = 999998;
                                dv[0]["PO_TYPE"] = "MII";
                                dv[0]["PO_COMP_CODE"] = "C99999";
                                dv[0]["PO_BRANCH"] = "B99999";
                                dv[0]["PI_NO"] = "NA";
                                dv[0]["PO_YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                                dv[0]["YARN_CODE"] = txtItemCode.Text.Trim();
                                dv[0]["YARN_DESC"] = txtDESC.Text.Trim();
                                dv[0]["SHADE_FAMILY"] = txtShadeFamily.Text;
                                dv[0]["SHADE_CODE"] = txtShadeCode.Text ;
                                dv[0]["TRN_QTY"] = double.Parse(txtQTY.Text.Trim());
                                dv[0]["UOM"] = txtUNIT.Text.Trim();
                                dv[0]["BASIC_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                                dv[0]["FINAL_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                                dv[0]["AMOUNT"] = double.Parse(txtAmount.Text.Trim());
                                dv[0]["COST_CODE"] = string.Empty;
                                dv[0]["MAC_CODE"] = string.Empty;
                                dv[0]["REMARKS"] = txtDetRemarks.Text.Trim();
                                dv[0]["QCFLAG"] = "No";
                                DateTime dd = System.DateTime.Now;
                                dv[0]["DATE_OF_MFG"] = dd;
                                dv[0]["NO_OF_UNIT"] = 0;
                                dv[0]["UOM_OF_UNIT"] = txtUNIT.Text.Trim(); ;
                                dv[0]["WEIGHT_OF_UNIT"] = 0;

                                dv[0]["LOT_NO"] = "NA";
                                dv[0]["GRADE"] = "NA";
                                dv[0]["GROSS_WT"] = 0;
                                dv[0]["TARE_WT"] = 0;
                                dv[0]["CARTONS"] = 0;
                                dv[0]["JOBER"] = "NA";
                                dtDetailTBL.AcceptChanges();
                            }
                        }
                        else
                        {
                            DataRow dr = dtDetailTBL.NewRow();
                            dr["UNIQUEID"] = dtDetailTBL.Rows.Count + 1;
                            dr["PO_NUMB"] = 999998;
                            dr["PO_TYPE"] = "MII";
                            dr["PO_COMP_CODE"] = "C99999";
                            dr["PO_BRANCH"] = "B99999";
                            dr["PI_NO"] = "NA";
                            dr["PO_YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                            dr["YARN_CODE"] = txtItemCode.Text.Trim();
                            dr["YARN_DESC"] = txtDESC.Text.Trim();
                            dr["SHADE_FAMILY"] = txtShadeFamily.Text;
                            dr["SHADE_CODE"] = txtShadeCode.Text;
                            dr["TRN_QTY"] = double.Parse(txtQTY.Text.Trim());
                            dr["UOM"] = txtUNIT.Text.Trim();
                            dr["BASIC_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                            dr["FINAL_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                            dr["AMOUNT"] = double.Parse(txtAmount.Text.Trim());
                            dr["COST_CODE"] = string.Empty;
                            dr["MAC_CODE"] = string.Empty;
                            dr["REMARKS"] = txtDetRemarks.Text.Trim();
                            dr["QCFLAG"] = "No";
                            DateTime dd = System.DateTime.Now;
                            dr["DATE_OF_MFG"] = dd;
                            dr["NO_OF_UNIT"] = 0;
                            dr["UOM_OF_UNIT"] = txtUNIT.Text.Trim(); ;
                            dr["WEIGHT_OF_UNIT"] = 0;

                            dr["LOT_NO"] = "NA";
                            dr["GRADE"] = "NA";
                            dr["GROSS_WT"] = 0;
                            dr["TARE_WT"] = 0;
                            dr["CARTONS"] = 0;
                            dr["JOBER"] = "NA";
                            dtDetailTBL.Rows.Add(dr);

                        }
                        RefreshDetailRow();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Quantity can not be zero');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('enter valid item code');", true);
                }
            }
            ViewState["dtDetailTBL"] = dtDetailTBL;
            BindGridFromDataTable();

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem adding Material detail data.\r\nSee error log for detail."));

        }
    }

    private bool SearchItemCodeInGrid(string ItemCode, int UniqueId)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in grdMaterialItemIssue.Rows)
            {
                Label txtICODE = (Label)grdRow.FindControl("txtICODE");
                LinkButton lnkbtnEdit = (LinkButton)grdRow.FindControl("lnkbtnEdit");
                int iUniqueId = int.Parse(lnkbtnEdit.CommandArgument.Trim());

                if (txtICODE.Text.Trim() == ItemCode && UniqueId != iUniqueId)
                    Result = true;
            }
            return Result;
        }
        catch
        {
            throw;
        }
    }

    private void RefreshDetailRow()
    {
        try
        {
            txtItemCode.Text = "";
            txtICODE.SelectedIndex = -1;
            txtDESC.Text = "";
            txtQTY.Text = "";
            txtUNIT.Text = "";
            txtBasicRate.Text = "";
            txtAmount.Text = "";
            txtDetRemarks.Text = "";
            txtShadeFamily.Text = string.Empty;
            txtShadeCode.Text = string.Empty;
            ViewState["UNIQUEID"] = null;
            txtICODE.Enabled = true;

        }
        catch
        {
            throw;
        }
    }

    protected void btnTRNCancel_Click(object sender, EventArgs e)
    {
        try
        {
            RefreshDetailRow();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in refresh detail information.\r\nSee error log for detail."));
        }
    }

    protected void grdMaterialItemIssue_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "EditItem")
            {
                EditItemReceiptRow(UniqueId);
            }
            else if (e.CommandName == "DelItem")
            {
                deleteItemIssueRow(UniqueId);
                BindGridFromDataTable();
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Editing/ deletion of Material detail.\r\nSee error log for detail."));
        }
    }

    private void EditItemReceiptRow(int UNIQUEID)
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }

            if (dtDetailTBL == null)
                CreateDataTable();

            DataView dv = new DataView(dtDetailTBL);
            dv.RowFilter = "UNIQUEID=" + UNIQUEID;
            if (dv.Count > 0)
            {
                ViewState["UNIQUEID"] = UNIQUEID;
                txtICODE.Enabled = false;
                txtItemCode.Text = dv[0]["YARN_CODE"].ToString();
                txtDESC.Text = dv[0]["YARN_DESC"].ToString();
                txtQTY.Text = dv[0]["TRN_QTY"].ToString();
                txtUNIT.Text = dv[0]["UOM"].ToString();
                txtBasicRate.Text = dv[0]["BASIC_RATE"].ToString();
                txtAmount.Text = dv[0]["AMOUNT"].ToString();
                txtDetRemarks.Text = dv[0]["REMARKS"].ToString();
                txtShadeFamily.Text = dv[0]["SHADE_FAMILY"].ToString();
                txtShadeCode.Text = dv[0]["SHADE_CODE"].ToString();

            }
        }
        catch
        {
            throw;
        }
    }

    private void deleteItemIssueRow(int UniqueId)
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }

            if (dtDetailTBL == null)
                CreateDataTable();

            if (dtDetailTBL.Rows.Count == 1)
            {
                dtDetailTBL.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtDetailTBL.Rows)
                {
                    int iUniqueId = int.Parse(dr["UNIQUEID"].ToString());
                    if (iUniqueId == UniqueId)
                    {
                        dtDetailTBL.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtDetailTBL.Rows)
                {
                    iCount = iCount + 1;
                    dr["UNIQUEID"] = iCount;
                }
            }
            ViewState["dtDetailTBL"] = dtDetailTBL;
        }
        catch
        {
            throw;

        }
    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string msg = string.Empty;
            if (ValidateFormForSavingOrUpdating(out msg))
            {
                saveMaterialIssue();
            }
            else
            {
                CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in saving data.\r\nSee error log for detail."));
        }
    }

    private bool ValidateFormForSavingOrUpdating(out string msg)
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }

            if (dtDetailTBL == null)
                CreateDataTable();

            bool ReturnResult = false;
            int count = 0;
            int countTotal = 0;
            msg = string.Empty;
            countTotal = 1;
            if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Please Enter atleast one item detail for receiving.\r\n";
            }

            if (count == countTotal)
                ReturnResult = true;
            return ReturnResult;
        }
        catch
        {
            throw;
        }
    }

    private void saveMaterialIssue()
    {
        try
        {
            Hashtable htIssue = new Hashtable();
            SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();
            oYRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oYRN_IR_MST.LOCATION = ddlFromLocation.SelectedItem.ToString();
            oYRN_IR_MST.STORE = ddlFromStore.SelectedItem.ToString();
            oYRN_IR_MST.FORM_NUMB = txtFormNo.Text;
            oYRN_IR_MST.FORM_TYPE = txtFormType.Text;
            DateTime dt = System.DateTime.Now.Date;
            oYRN_IR_MST.GATE_DATE = dt;
            bool Is_Gate_Entry = false;
            htIssue.Add("GATE_ENTRY", Is_Gate_Entry);
            oYRN_IR_MST.GATE_NUMB = "";
            oYRN_IR_MST.GATE_OUT_NUMB = "";
            oYRN_IR_MST.GATE_PASS_TYPE = "";
            oYRN_IR_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtVehicleNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_LR = false;
            htIssue.Add("LR", Is_LR);
            oYRN_IR_MST.LR_DATE = dt;

            oYRN_IR_MST.LR_NUMB = "";

            dt = System.DateTime.Now.Date;
            bool Is_Party_challan = false;
            Is_Party_challan = DateTime.TryParse(txtDocDate.Text.Trim(), out dt);
            htIssue.Add("PARTY_CHALLAN", Is_Party_challan);
            oYRN_IR_MST.PRTY_CH_DATE = dt;

            oYRN_IR_MST.PRTY_CH_NUMB = CommonFuction.funFixQuotes(txtDocNo.Text.Trim());
            oYRN_IR_MST.PRTY_CODE = "NA";
            oYRN_IR_MST.PRTY_NAME = "";
            oYRN_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oYRN_IR_MST.REPROCESS = "";
            oYRN_IR_MST.SHIFT = ddlShift.SelectedValue.Trim();

            dt = System.DateTime.Now.Date;
            bool Is_MRN = false;
            Is_MRN = DateTime.TryParse(txtIssueDate.Text.Trim(), out dt);
            htIssue.Add("MRN", Is_MRN);
            oYRN_IR_MST.TRN_DATE = dt;

            oYRN_IR_MST.TRN_NUMB = int.Parse(CommonFuction.funFixQuotes(txtIssueNumb.Text.Trim()));
            oYRN_IR_MST.TRN_TYPE = CommonFuction.funFixQuotes(STISSUE_TYPE);

            if (lblTransporterCode.Text == "")
                oYRN_IR_MST.TRSP_CODE = "NA";
            else
                oYRN_IR_MST.TRSP_CODE = CommonFuction.funFixQuotes(lblTransporterCode.Text.Trim());

            oYRN_IR_MST.TUSER = oUserLoginDetail.UserCode;
            oYRN_IR_MST.BILL_NUMB = "";
            oYRN_IR_MST.BILL_TYPE = "";
            oYRN_IR_MST.BILL_YEAR = 0;
            oYRN_IR_MST.TOTAL_AMOUNT = 0;
            oYRN_IR_MST.FINAL_AMOUNT = 0;


          
          oYRN_IR_MST.LOT_ID_NO="NA";
          oYRN_IR_MST.TO_DEPT_CODE=string.Empty ;
          oYRN_IR_MST.REC_BRANCH_CODE=string.Empty ;
          oYRN_IR_MST.TO_LOCATION=string.Empty ;
          oYRN_IR_MST.TO_STORE = string.Empty;
          oYRN_IR_MST.SPINNER_CODE = string.Empty;
            DataTable dtItemReceipt = (DataTable)Session["dtItemReceipt"];

            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }

            if (dtDetailTBL == null)
                CreateDataTable();

            
            for(int i=0;i<dtDetailTBL.Rows.Count;i++)
            {

                dtDetailTBL.Rows[i]["JOBER"] = "NA";
            }

              for (int i = 0; i < dtItemReceipt.Rows.Count; i++)
                {
                    if (dtItemReceipt.Rows[i]["DYED_BATCH"] == string.Empty)
                    {
                        dtItemReceipt.Rows[i]["DYED_BATCH"] = "NA";
                    }
                }
            
            int TRN_NUMB = 0;
            bool result = SaitexBL.Interface.Method.YRN_IR_MST.Insert(oYRN_IR_MST, out TRN_NUMB, dtDetailTBL, dtItemReceipt, htIssue);
            if (result)
            {
                int REceipt_numb = 0;
                if (saveMaterialReceipt(out REceipt_numb, dtItemReceipt))
                {
                    InitialisePage();
                    CommonFuction.ShowMessage(@"Issue Number : " + TRN_NUMB + " saved successfully.\r\nReceipt Number : " + REceipt_numb + " saved suyccessfully");
                }
            }
            else
            {
                CommonFuction.ShowMessage("data  Saving Failed");
            }
        }
        catch
        {
            throw;
        }
    }

    private bool saveMaterialReceipt(out int RECEIPT_NUMB, DataTable dtItemReceipt)
    {
        RECEIPT_NUMB = 0;
        try
        {
            Hashtable htIssue = new Hashtable();

            SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();
            oYRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_IR_MST.BRANCH_CODE = ddlReceiptBranch.SelectedValue.Trim();
            oYRN_IR_MST.COMP_CODE = ddlReceiptCompany.SelectedValue.Trim();
            oYRN_IR_MST.DEPT_CODE = ddlReceiptDepartment.SelectedValue.Trim();
            oYRN_IR_MST.FORM_NUMB = "";
            oYRN_IR_MST.FORM_TYPE = "";

            DateTime dt = System.DateTime.Now.Date;
            oYRN_IR_MST.GATE_DATE = dt;
            bool Is_Gate_Entry = false;
            htIssue.Add("GATE_ENTRY", Is_Gate_Entry);

            oYRN_IR_MST.GATE_NUMB = "";
            oYRN_IR_MST.GATE_OUT_NUMB = "";
            oYRN_IR_MST.GATE_PASS_TYPE = "";
            oYRN_IR_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtVehicleNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_LR = false;
            htIssue.Add("LR", Is_LR);
            oYRN_IR_MST.LR_DATE = dt;

            oYRN_IR_MST.LR_NUMB = "";

            dt = System.DateTime.Now.Date;
            bool Is_Party_challan = false;
            Is_Party_challan = DateTime.TryParse(txtDocDate.Text.Trim(), out dt);
            htIssue.Add("PARTY_CHALLAN", Is_Party_challan);
            oYRN_IR_MST.PRTY_CH_DATE = dt;

            oYRN_IR_MST.PRTY_CH_NUMB = CommonFuction.funFixQuotes(txtDocNo.Text.Trim());
            oYRN_IR_MST.PRTY_CODE = "NA";
            oYRN_IR_MST.PRTY_NAME = "NA";
            oYRN_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oYRN_IR_MST.REPROCESS = string.Empty;
            oYRN_IR_MST.SHIFT = ddlShift.SelectedValue.Trim();

            dt = System.DateTime.Now.Date;
            bool Is_MRN = false;
            Is_MRN = DateTime.TryParse(txtIssueDate.Text.Trim(), out dt);
            htIssue.Add("MRN", Is_MRN);
            oYRN_IR_MST.TRN_DATE = dt;

            oYRN_IR_MST.TRN_NUMB = RECEIPT_NUMB;
            oYRN_IR_MST.TRN_TYPE = STRECEIPT_TYPE;

            if (lblTransporterCode.Text == "")
                oYRN_IR_MST.TRSP_CODE = "NA";
            else
                oYRN_IR_MST.TRSP_CODE = CommonFuction.funFixQuotes(lblTransporterCode.Text.Trim());

            oYRN_IR_MST.TUSER = oUserLoginDetail.UserCode;
            oYRN_IR_MST.BILL_NUMB = "";
            oYRN_IR_MST.BILL_TYPE = "";
            oYRN_IR_MST.BILL_YEAR = 0;
            oYRN_IR_MST.TOTAL_AMOUNT = 0;
            oYRN_IR_MST.FINAL_AMOUNT = 0;
            oYRN_IR_MST.LOCATION = ddlToLocation.SelectedItem.ToString();
            oYRN_IR_MST.STORE = ddlToStore.SelectedItem.ToString();

            oYRN_IR_MST.LOT_ID_NO = "NA";
            oYRN_IR_MST.TO_DEPT_CODE = string.Empty;
            oYRN_IR_MST.REC_BRANCH_CODE = string.Empty;
            oYRN_IR_MST.TO_LOCATION = string.Empty;
            oYRN_IR_MST.TO_STORE = string.Empty;
            oYRN_IR_MST.SPINNER_CODE = string.Empty;

            
            for (int i = 0; i < dtDetailTBL.Rows.Count; i++)
            {

                dtDetailTBL.Rows[i]["JOBER"] = "NA";
            }
            
            for (int i = 0; i < dtItemReceipt.Rows.Count; i++)
            {

                dtItemReceipt.Rows[i]["DYED_BATCH"] = "NA";
            }
            return SaitexBL.Interface.Method.YRN_IR_MST.Insert(oYRN_IR_MST, out RECEIPT_NUMB, dtDetailTBL, htIssue, dtItemReceipt);

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
            ActivateUpdateMode();

            lblMode.Text = "You are in Update Mode";

            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            tdSave.Visible = false;
        }
        catch (Exception ex)
        {

            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updation mode.\r\nSee error log for detail."));
        }
    }

    private void ActivateUpdateMode()
    {
        try
        {
            //txtReceiptNumb.Text = string.Empty;
            //txtIssueNumb.Text = string.Empty;
            ddlTRNNumber.SelectedIndex = -1;
            ddlTRNNumber.SelectedText = string.Empty;
            ddlTRNNumber.SelectedValue = string.Empty;
            ddlTRNNumber.Items.Clear();
            ddlTRNNumber.Visible = true;
            BindIssueNumb();
            BindReceiveNumb();
            txtIssueNumb.Visible = false;

        }
        catch
        {
            throw;
        }
    }

    protected void ddlTRNNumber_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            Obout.ComboBox.ComboBox thisTextBox = (Obout.ComboBox.ComboBox)sender;
            thisTextBox.SelectedIndex = 0;
            thisTextBox.Items.Clear();
            DataTable data = new DataTable();
            data = GetStockData(e.Text.ToUpper(), e.ItemsOffset, 10);
            thisTextBox.DataSource = data;
            thisTextBox.DataTextField = "TRN_NUMB";
            thisTextBox.DataValueField = "adjData";
            thisTextBox.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = data.Rows.Count;

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Indent for updation.\r\nSee error log for detail."));
        }
    }

    protected DataTable GetStockData(string text, int startOffset, int numberOfItems)
    {
        try
        {

            string whereClause = " where TRN_NUMB like :searchQuery or ISS_TRN_NUMB like :searchQuery ";
            string sortExpression = " order by TRN_NUMB desc";
            string commandText = "select * from (SELECT distinct ( I.TRN_TYPE|| '@'|| I.TRN_NUMB|| '@'|| I.COMP_CODE|| '@'|| I.BRANCH_CODE|| '@'|| R.TRN_TYPE|| '@'|| R.TRN_NUMB|| '@'|| R.COMP_CODE|| '@'|| R.BRANCH_CODE) adjData, I.TRN_TYPE ISS_TRN_TYPE,I.TRN_NUMB ISS_TRN_NUMB,I.COMP_CODE ISS_COMP,I.BRANCH_CODE ISS_BRANCH,R.TRN_TYPE ,R.TRN_NUMB ,R.COMP_CODE ,R.BRANCH_CODE  FROM YRN_IR_MST I,YRN_IR_MST R  WHERE  I.TRN_TYPE='IYS06' AND  R.TRN_TYPE='RYS06' AND NVL(I.CONF_FLAG,0)=0  AND R.TRN_NUMB=I.TRN_NUMB AND I.YEAR ='" + oUserLoginDetail.DT_STARTDATE.Year + "' AND R.YEAR ='" + oUserLoginDetail.DT_STARTDATE.Year + "' AND I.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "') asd  ";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + "%", "");
            //  AND I.DEPT_CODE ='"+oUserLoginDetail.VC_DEPARTMENTCODE+"'         
        }
        catch
        {
            throw;
        }
    }

    protected void ddlTRNNumber_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }
            if (dtDetailTBL == null)
                CreateDataTable();

            string adjData = ddlTRNNumber.SelectedValue.Trim();

            dtDetailTBL.Rows.Clear();

            int iRecordFound = GetdataByAdjData(adjData);
            BindGridFromDataTable();
            if (iRecordFound > 0)
            {

            }
            else
            {
                InitialisePage();
                ActivateUpdateMode();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('Invalid Challan No');", true);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selection of Challan Number.\r\nSee error log for detail."));

        }
    }

    private int GetdataByAdjData(string adjData)
    {
        int iRecordFound = 0;
        try
        {
            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }

            if (dtDetailTBL == null)
                CreateDataTable();

            DataSet ds = SaitexBL.Interface.Method.YRN_IR_MST.GetStockDataForUpdate(oUserLoginDetail.DT_STARTDATE.Year, adjData);

            if (ds != null && ds.Tables.Count > 0)
            {
                iRecordFound = 1;

                BindDestinationDataForUpdate(ds.Tables[0]);
                BindSourceDataForUpdate(ds.Tables[1]);
            }
            if (iRecordFound == 1)
            {
                dtDetailTBL.Rows.Clear();
                dtDetailTBL = SaitexBL.Interface.Method.YRN_IR_MST.GetIssueTRN_DataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, ds.Tables[0].Rows[0]["COMP_CODE"].ToString(), ds.Tables[0].Rows[0]["BRANCH_CODE"].ToString(), int.Parse(ds.Tables[0].Rows[0]["TRN_NUMB"].ToString()), STRECEIPT_TYPE);
                ViewState["dtDetailTBL"] = dtDetailTBL;
                MapDataTable();
            }
            else
            {
                InitialisePage();
                lblMode.Text = "You are in Update Mode";
                ActivateUpdateMode();
                RefreshDetailRow();
            }
            return iRecordFound;
        }
        catch
        {
            throw;
        }
    }

    private void BindSourceDataForUpdate(DataTable dtIssue)
    {
        try
        {
            if (dtIssue != null && dtIssue.Rows.Count > 0)
            {
                //BindCompanyDropDown(ddlSourceCompany);
                //ddlSourceCompany.SelectedValue = dtIssue.Rows[0]["COMP_CODE"].ToString();
                //BindBranchDropDown(ddlSourceBranch, ddlSourceCompany.SelectedValue.Trim());
                //ddlSourceBranch.SelectedValue = dtIssue.Rows[0]["BRANCH_CODE"].ToString();
                //BindDepartmentDropDown(ddlIssueDepartment);
                //ddlIssueDepartment.SelectedValue = dtIssue.Rows[0]["DEPT_CODE"].ToString();
                txtIssueNumb.Text = dtIssue.Rows[0]["TRN_NUMB"].ToString();
                txtIssueDate.Text = DateTime.Parse(dtIssue.Rows[0]["TRN_DATE"].ToString()).ToShortDateString();
                //ddlIssueShift.SelectedValue = dtIssue.Rows[0]["SHIFT"].ToString();
                txtDocNo.Text = dtIssue.Rows[0]["PRTY_CH_NUMB"].ToString();
                txtDocDate.Text = dtIssue.Rows[0]["PRTY_CH_DATE"].ToString();
                txtVehicleNo.Text = dtIssue.Rows[0]["LORY_NUMB"].ToString();
                txtRemarks.Text = dtIssue.Rows[0]["RCOMMENT"].ToString();

                txtFormType.Text = dtIssue.Rows[0]["FORM_TYPE"].ToString();
                txtFormNo.Text = dtIssue.Rows[0]["FORM_NUMB"].ToString();
                //ddlSourceCompany.Enabled = false;
                //ddlSourceBranch.Enabled = false;               
                BindDepartment(ddlFromStore);
                BindDropDown(ddlFromLocation);
                ddlFromLocation.SelectedIndex = ddlFromLocation.Items.IndexOf(ddlFromLocation.Items.FindByText(dtIssue.Rows[0]["LOCATION"].ToString()));
                ddlFromStore.SelectedIndex = ddlFromStore.Items.IndexOf(ddlFromStore.Items.FindByText(dtIssue.Rows[0]["STORE"].ToString()));
            }
        }
        catch
        {
            throw;
        }
    }

    private void BindDestinationDataForUpdate(DataTable dtReceive)
    {
        try
        {
            if (dtReceive != null && dtReceive.Rows.Count > 0)
            {
                ddlReceiptCompany.SetIndexByValue(oUserLoginDetail.COMP_CODE);
                ddlReceiptBranch.LoadData(ddlReceiptCompany.SelectedValue);
                ddlReceiptBranch.SetIndexByValue(dtReceive.Rows[0]["BRANCH_CODE"].ToString());
                ddlReceiptDepartment.SelectedValue = dtReceive.Rows[0]["DEPT_CODE"].ToString();
                BindDropDown(ddlToLocation);
                BindDepartment(ddlToStore);
                ddlToLocation.SelectedIndex = ddlToLocation.Items.IndexOf(ddlToLocation.Items.FindByText(dtReceive.Rows[0]["LOCATION"].ToString()));
                ddlToStore.SelectedIndex = ddlToStore.Items.IndexOf(ddlToStore.Items.FindByText(dtReceive.Rows[0]["STORE"].ToString()));
                txtReceiveNumb.Text = dtReceive.Rows[0]["TRN_NUMB"].ToString();

                //BindCompanyDropDown(ddlDestinationCompany);
                //ddlDestinationCompany.SelectedValue = dtReceive.Rows[0]["COMP_CODE"].ToString();
                //BindBranchDropDown(ddlDestinationBranch, ddlDestinationCompany.SelectedValue.Trim());
                //ddlDestinationBranch.SelectedValue = dtReceive.Rows[0]["BRANCH_CODE"].ToString();
                //BindDepartmentDropDown(ddlDestinationDepartment);
                //ddlDestinationDepartment.SelectedValue = dtReceive.Rows[0]["DEPT_CODE"].ToString();
                //// txtReceiptNumb.Text = dtReceive.Rows[0]["TRN_NUMB"].ToString();
                //ddlDestinationCompany.Enabled = false;
                //ddlDestinationBranch.Enabled = false;
            }
        }
        catch
        {
            throw;
        }
    }

    private void MapDataTable()
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }

            if (dtDetailTBL == null)
                CreateDataTable();

            if (!dtDetailTBL.Columns.Contains("UNIQUEID"))
                dtDetailTBL.Columns.Add("UNIQUEID", typeof(int));

            for (int iLoop = 0; iLoop < dtDetailTBL.Rows.Count; iLoop++)
            {
                dtDetailTBL.Rows[iLoop]["UNIQUEID"] = iLoop + 1;
            }
            ViewState["dtDetailTBL"] = dtDetailTBL;
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
            string msg = string.Empty;
            if (ValidateFormForSavingOrUpdating(out msg))
            {
                UpdateMaterialIssue();
            }
            else
            {
                CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in updating data.\r\nSee error log for detail."));
        }
    }

    private void UpdateMaterialIssue()
    {
        try
        {
            Hashtable htIssue = new Hashtable();
            SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();
            oYRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oYRN_IR_MST.FORM_NUMB = txtFormNo.Text;
            oYRN_IR_MST.FORM_TYPE = txtFormType.Text;
            oYRN_IR_MST.LOCATION = ddlFromLocation.SelectedItem.ToString();
            oYRN_IR_MST.STORE = ddlFromStore.SelectedItem.ToString();
            DateTime dt = System.DateTime.Now.Date;
            oYRN_IR_MST.GATE_DATE = dt;
            bool Is_Gate_Entry = false;
            htIssue.Add("GATE_ENTRY", Is_Gate_Entry);
            oYRN_IR_MST.GATE_NUMB = "";
            oYRN_IR_MST.GATE_OUT_NUMB = "";
            oYRN_IR_MST.GATE_PASS_TYPE = "";
            oYRN_IR_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtVehicleNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_LR = false;
            htIssue.Add("LR", Is_LR);
            oYRN_IR_MST.LR_DATE = dt;
            oYRN_IR_MST.LR_NUMB = "";
            dt = System.DateTime.Now.Date;
            bool Is_Party_challan = false;
            Is_Party_challan = DateTime.TryParse(txtDocDate.Text.Trim(), out dt);
            htIssue.Add("PARTY_CHALLAN", Is_Party_challan);
            oYRN_IR_MST.PRTY_CH_DATE = dt;
            oYRN_IR_MST.PRTY_CH_NUMB = CommonFuction.funFixQuotes(txtDocNo.Text.Trim());
            oYRN_IR_MST.PRTY_CODE = "NA";
            oYRN_IR_MST.PRTY_NAME = "";
            oYRN_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oYRN_IR_MST.REPROCESS = "";
            oYRN_IR_MST.SHIFT = ddlShift.SelectedValue.Trim();
            dt = System.DateTime.Now.Date;
            bool Is_MRN = false;
            Is_MRN = DateTime.TryParse(txtIssueDate.Text.Trim(), out dt);
            htIssue.Add("MRN", Is_MRN);
            oYRN_IR_MST.TRN_DATE = dt;

            oYRN_IR_MST.TRN_NUMB = int.Parse(CommonFuction.funFixQuotes(txtIssueNumb.Text.Trim()));
            oYRN_IR_MST.TRN_TYPE = CommonFuction.funFixQuotes(STISSUE_TYPE);

            if (lblTransporterCode.Text == "")
                oYRN_IR_MST.TRSP_CODE = "NA";
            else
                oYRN_IR_MST.TRSP_CODE = CommonFuction.funFixQuotes(lblTransporterCode.Text.Trim());

            oYRN_IR_MST.TUSER = oUserLoginDetail.UserCode;
            oYRN_IR_MST.BILL_NUMB = "";
            oYRN_IR_MST.BILL_TYPE = "";
            oYRN_IR_MST.BILL_YEAR = 0;
            oYRN_IR_MST.BILL_DATE = DateTime.MinValue;
            oYRN_IR_MST.TOTAL_AMOUNT = 0;
            oYRN_IR_MST.FINAL_AMOUNT = 0;

            oYRN_IR_MST.LOT_ID_NO = "NA";
            oYRN_IR_MST.TO_DEPT_CODE = string.Empty;
            oYRN_IR_MST.REC_BRANCH_CODE = string.Empty;
            oYRN_IR_MST.TO_LOCATION = string.Empty;
            oYRN_IR_MST.TO_STORE = string.Empty;
            oYRN_IR_MST.SPINNER_CODE = string.Empty;
            DataTable dtItemReceipt = (DataTable)Session["dtItemReceipt"];

            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }

            if (dtDetailTBL == null)
                CreateDataTable();

            bool result = SaitexBL.Interface.Method.YRN_IR_MST.Update(oYRN_IR_MST, dtDetailTBL, dtItemReceipt, htIssue);
            if (result)
            {
                int REceipt_numb = int.Parse(txtReceiveNumb.Text.Trim());
                if (UpdateMaterialReceipt(REceipt_numb, dtItemReceipt))
                {
                    InitialisePage();
                    CommonFuction.ShowMessage(@"Issue Number : " + int.Parse(CommonFuction.funFixQuotes(txtIssueNumb.Text.Trim())) + " saved successfully.\r\nReceipt Number : " + REceipt_numb + " saved suyccessfully");
                }
            }
            else
            {
                CommonFuction.ShowMessage("data updation Failed");
            }
        }
        catch
        {
            throw;
        }
    }

    private bool UpdateMaterialReceipt(int RECEIPT_NUMB, DataTable dtItemReceipt)
    {
        try
        {
            Hashtable htIssue = new Hashtable();
            SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();
            oYRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_IR_MST.BRANCH_CODE = ddlReceiptBranch.SelectedValue.Trim();
            oYRN_IR_MST.COMP_CODE = ddlReceiptCompany.SelectedValue.Trim();
            oYRN_IR_MST.DEPT_CODE = ddlReceiptDepartment.SelectedValue.Trim();
            oYRN_IR_MST.FORM_NUMB = txtFormNo.Text;
            oYRN_IR_MST.FORM_TYPE = txtFormType.Text;
            DateTime dt = System.DateTime.Now.Date;
            oYRN_IR_MST.GATE_DATE = dt;
            bool Is_Gate_Entry = false;
            htIssue.Add("GATE_ENTRY", Is_Gate_Entry);
            oYRN_IR_MST.GATE_NUMB = "";
            oYRN_IR_MST.GATE_OUT_NUMB = "";
            oYRN_IR_MST.GATE_PASS_TYPE = "";
            oYRN_IR_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtVehicleNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_LR = false;
            htIssue.Add("LR", Is_LR);
            oYRN_IR_MST.LR_DATE = dt;

            oYRN_IR_MST.LR_NUMB = "";

            dt = System.DateTime.Now.Date;
            bool Is_Party_challan = false;
            Is_Party_challan = DateTime.TryParse(txtDocDate.Text.Trim(), out dt);
            htIssue.Add("PARTY_CHALLAN", Is_Party_challan);
            oYRN_IR_MST.PRTY_CH_DATE = dt;

            oYRN_IR_MST.PRTY_CH_NUMB = CommonFuction.funFixQuotes(txtDocNo.Text.Trim());
            oYRN_IR_MST.PRTY_CODE = "NA";
            oYRN_IR_MST.PRTY_NAME = "NA";
            oYRN_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oYRN_IR_MST.REPROCESS = string.Empty;
            oYRN_IR_MST.SHIFT = ddlShift.SelectedValue.Trim();

            dt = System.DateTime.Now.Date;
            bool Is_MRN = false;
            Is_MRN = DateTime.TryParse(txtIssueDate.Text.Trim(), out dt);
            htIssue.Add("MRN", Is_MRN);
            oYRN_IR_MST.TRN_DATE = dt;

            oYRN_IR_MST.TRN_NUMB = RECEIPT_NUMB;
            oYRN_IR_MST.TRN_TYPE = STRECEIPT_TYPE;

            if (lblTransporterCode.Text == "")
                oYRN_IR_MST.TRSP_CODE = "NA";
            else
                oYRN_IR_MST.TRSP_CODE = CommonFuction.funFixQuotes(lblTransporterCode.Text.Trim());

            oYRN_IR_MST.TUSER = oUserLoginDetail.UserCode;
            oYRN_IR_MST.BILL_NUMB = "";
            oYRN_IR_MST.BILL_DATE = DateTime.MinValue;
            oYRN_IR_MST.BILL_TYPE = "";
            oYRN_IR_MST.BILL_YEAR = 0;
            oYRN_IR_MST.TOTAL_AMOUNT = 0;
            oYRN_IR_MST.FINAL_AMOUNT = 0;
            oYRN_IR_MST.LOCATION = ddlToLocation.SelectedItem.ToString();
            oYRN_IR_MST.STORE = ddlToStore.SelectedItem.ToString();

            oYRN_IR_MST.LOT_ID_NO = "NA";
            oYRN_IR_MST.TO_DEPT_CODE = string.Empty;
            oYRN_IR_MST.REC_BRANCH_CODE = string.Empty;
            oYRN_IR_MST.TO_LOCATION = string.Empty;
            oYRN_IR_MST.TO_STORE = string.Empty;
            oYRN_IR_MST.SPINNER_CODE = string.Empty;
            return SaitexBL.Interface.Method.YRN_IR_MST.Update(oYRN_IR_MST, dtDetailTBL, htIssue, dtItemReceipt);

        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in deleting data.\r\nSee error log for detail."));

        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InitialisePage();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in clearingdata.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in generating report data.\r\nSee error log for detail."));

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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in leaving page.\r\nSee error log for detail."));

        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected override void OnPreRender(EventArgs e)
    {
        try
        {
            base.OnPreRender(e);
            imgbtnClear.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to clear this record')");
            imgbtnDelete.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to delete this record')");
            imgbtnExit.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to exit this record')");
            imgbtnPrint.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to print this record')");
            imgbtnSave.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to save this record')");
            imgbtnUpdate.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to update this record')");
        }
        catch
        {
            throw;
        }
    }
}
