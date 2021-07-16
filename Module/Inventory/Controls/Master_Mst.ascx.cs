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
using System.Data.OracleClient;
using Common;
using errorLog;

public partial class Module_Inventory_Controls_Master_Mst : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["urLoginId"] != null)
        {
            if (!IsPostBack)
            {
                lnkSearch.Visible = false;
                txtMstName.Visible = true;
                txtFind.Visible = false;
                tdUpdate.Visible = false;
                tdDelete.Visible = false;
                tdFind.Visible = true;
                lblMode.Text = "Save";
            }
            if (Convert.ToInt16(Session["saveStatus"]) == 1)
            {
                if (Request.QueryString["cId"].ToString().Trim() == "S")
                {
                    lblMessage.Text = "Record Saved successfully";

                }
                if (Request.QueryString["cId"].ToString().Trim() == "U")
                {
                    lblMessage.Text = "Record Updated successfully";
                }
                if (Request.QueryString["cId"].ToString().Trim() == "D")
                {
                    lblMessage.Text = "Record Deleted successfully";
                }

                Session["saveStatus"] = 0;
            }
        }
        else
        {
            Response.Redirect("../Default.aspx", false);
        }
    }
    protected void lnkSearch_Click(object sender, EventArgs e)
    {

    }
    protected void txtFind_TextChanged(object sender, EventArgs e)
    {
        if (txtFind.Text != "")
        {
            GetFindData();
            txtMstName.Visible = false;
            txtFind.Visible = true;
            txtFind.Enabled = false;
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            lblMode.Text = "Find";
        }
        else
        {
            lblErrorMessage.Text = "Please enter Master Name";
        }
    }
    private bool CheckForDuplicacy(string MstName)
    {
        try
        {
            bool iRecordFound;
            iRecordFound = false;
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_MST.Select(oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "MST_NAME='" + MstName + "'";
                if (dv.Count > 0)
                {
                    lblMessage.Text = "This record already saved! Pls enter another record.";
                    iRecordFound = true;
                }
            }
            return iRecordFound;
        }
        catch
        {
            return true;
        }
    }
    private void SaveMasterData()
    {
        if (!CheckForDuplicacy(txtMstName.Text.Trim()))
        {
            try
            {
                SaitexDM.Common.DataModel.TX_MASTER_MST oTX_MASTER_MST = new SaitexDM.Common.DataModel.TX_MASTER_MST();
                oTX_MASTER_MST.MST_NAME = txtMstName.Text.ToUpper().Trim();
                oTX_MASTER_MST.MST_DESC = txtDesc.Text.Trim();
                oTX_MASTER_MST.MAX_FLD_LNGTH = int.Parse(txtMaxLen.Text.Trim());
                oTX_MASTER_MST.TUSER = Session["urLoginId"].ToString().Trim();
                oTX_MASTER_MST.TDATE = DateTime.Now.Date;

                int iRecordFound = 0;
                bool bResult = SaitexBL.Interface.Method.TX_MASTER_MST.Insert(oTX_MASTER_MST, out iRecordFound);
                if (bResult)
                {
                    Session["saveStatus"] = 1;
                    Response.Redirect("~/Module/Inventory/Pages/Master_Mst.aspx?cId=S", false);
                }
                else if (iRecordFound > 0)
                {
                    lblMessage.Text = "This Record is already saved.. Please enter another.";
                }
            }
            catch (OracleException ex)
            {
                lblMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
                ErrHandler.WriteError(ex.Message);
            }
            catch (Exception ex)
            {
                lblMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
                ErrHandler.WriteError(ex.Message);
            }
            finally
            {
            }
        }
        else
        {
            lblMessage.Text = "This Record is already saved.. Please enter another.";
        }
    }
    private void UpdateData()
    {
        if (CheckForDuplicacy(txtFind.Text.Trim()))
        {
            try
            {
                SaitexDM.Common.DataModel.TX_MASTER_MST oTX_MASTER_MST = new SaitexDM.Common.DataModel.TX_MASTER_MST();

                oTX_MASTER_MST.MST_NAME = txtFind.Text.ToUpper().Trim();
                oTX_MASTER_MST.MST_DESC = txtDesc.Text.Trim();
                oTX_MASTER_MST.MAX_FLD_LNGTH = int.Parse(txtMaxLen.Text.Trim());
                oTX_MASTER_MST.TUSER = Session["urLoginId"].ToString().Trim();
                oTX_MASTER_MST.TDATE = DateTime.Now.Date;

                int iRecordFound = 0;
                bool bResult = SaitexBL.Interface.Method.TX_MASTER_MST.Update(oTX_MASTER_MST, out iRecordFound);

                if (bResult)
                {
                    Session["saveStatus"] = 1;
                    Response.Redirect("~/Module/Inventory/Pages/Master_Mst.aspx?cId=U", false);
                }
                else if (iRecordFound > 0)
                {
                    lblMessage.Text = "This Record is already saved.. Please enter another.";
                }
            }

            catch (OracleException ex)
            { lblMessage.Text = ""; lblErrorMessage.Text = ex.Message; ErrHandler.WriteError(ex.Message); }
            catch (Exception ex)
            { lblMessage.Text = ""; lblErrorMessage.Text = ex.Message; ErrHandler.WriteError(ex.Message); }
        }
        else
        {
            txtMstName.Visible = false;
            txtFind.Visible = true;
            lblMessage.Text = "No such record exits.! Pls enter valid Category Code.";
        }
    }
    private void DeleteMSTData()
    {
        try
        {
            SaitexDM.Common.DataModel.TX_MASTER_MST oTX_MASTER_MST = new SaitexDM.Common.DataModel.TX_MASTER_MST();

            oTX_MASTER_MST.MST_NAME = txtMstName.Text.ToUpper().Trim();
            oTX_MASTER_MST.TUSER = Session["urLoginId"].ToString().Trim();
            oTX_MASTER_MST.TDATE = DateTime.Now.Date;

            int iRecordFound = 0;
            bool bResult = SaitexBL.Interface.Method.TX_MASTER_MST.Delete(oTX_MASTER_MST, out iRecordFound);

            if (bResult)
            {
                Session["saveStatus"] = 1;
                Response.Redirect("~/Module/Inventory/Pages/Master_Mst.aspx?cId=D", false);
            }
            else if (iRecordFound > 0)
            {
                lblMessage.Text = "This Record is already saved.. Please enter another.";
            }
        }

        catch (OracleException ex)
        { lblMessage.Text = ""; lblErrorMessage.Text = ex.Message; ErrHandler.WriteError(ex.Message); }
        catch (Exception ex)
        { lblMessage.Text = ""; lblErrorMessage.Text = ex.Message; ErrHandler.WriteError(ex.Message); }
    }
    private void GetFindData()
    {
        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        DataTable dt = SaitexBL.Interface.Method.TX_MASTER_MST.Select(oUserLoginDetail.COMP_CODE);
        if (dt != null && dt.Rows.Count > 0)
        {
            DataView dv = new DataView(dt);
            dv.RowFilter = "MST_NAME='" + txtFind.Text.Trim() + "'";
            if (dv.Count > 0)
            {
                txtMstName.Text = dt.Rows[0]["MST_NAME"].ToString();
                txtDesc.Text = dt.Rows[0]["MST_DESC"].ToString();
                txtMaxLen.Text = dt.Rows[0]["MAX_FLD_LNGTH"].ToString();
            }
        }
    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        if (txtMstName.Text != "")
        {
            SaveMasterData();
        }
        else
        {
            lblMessage.Text = "Please Insert a record to save.";
        }
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        if (txtMstName.Text != "")
        {
            UpdateData();
        }
        else
        {
            lblMessage.Text = "Please Find a record to update";
        }
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        if (txtMstName.Text != "")
        {
            DeleteMSTData();
        }
        else
        {
            lblMessage.Text = "Please find a record to delete";
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Inventory/Pages/Master_Mst.aspx", false);
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string URL = "Master_Mst_Opt.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=300,height=150');", true);
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
                Response.Redirect("~/Module/Admin/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        string URL = "SearchMaster.aspx";

        URL = URL + "?MstNameId=" + txtFind.ClientID;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);

        //lnkSearch.Visible = true;
        txtFind.Visible = true;
        txtMstName.Visible = false;
        tdSave.Visible = false;
        tdUpdate.Visible = true;
        tdDelete.Visible = true;
        lblMode.Text = "Find";
    }
    protected void txtMstName_TextChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    DataTable dt = SaitexBL.Interface.Method.TX_MASTER_MST.Select();
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        DataView dv = new DataView(dt);
        //        dv.RowFilter = "MST_NAME='" + txtMstName.Text.Trim() + "'";
        //        if (dv.Count > 0)
        //        {
        //            txtMstName.Text = dt.Rows[0]["MST_NAME"].ToString();
        //            txtDesc.Text = dt.Rows[0]["MST_DESC"].ToString();
        //            txtMaxLen.Text = dt.Rows[0]["MAX_FLD_LNGTH"].ToString();
        //        }
        //    }
        //    else
        //    {
        //        tdSave.Visible = true;
        //        tdUpdate.Visible = false;
        //        tdDelete.Visible = false;
        //        lblMode.Text = "Save";
        //    }
        //}
        //catch (Exception ex)
        //{
        //    lblErrorMessage.Text = ex.Message;
        //    errorLog.ErrHandler.WriteError(ex.Message);
        //}
    }
}
