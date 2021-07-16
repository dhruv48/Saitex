using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OracleClient;
using DBLibrary;
using Common;
using errorLog;
using System.IO;
public partial class Admin_AddNavigation : System.Web.UI.Page
{
    OracleConnection con = null;
    OracleCommand cmd = null;
    OracleParameter param = null;
    OracleDataReader dr = null;
    DataSet ds = null;
    csSaitex obj = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        tdSerch.Visible = false;
        if (!IsPostBack)
        {
            chk_Status.Checked = true;
            bindAddModuleMaster();
            //    bindAddModuleMasterRight();
            //       bindGvAddModuleNavigation('Y');
            BindTabMAsterDropDown();
            BlanksControls();
            getUserMenuData();
        }

    }

    private void getUserMenuData()
    {
        try
        {
            SaitexDM.Common.DataModel.UserAccessRight oUserAccessRight = new SaitexDM.Common.DataModel.UserAccessRight();
            oUserAccessRight.UserCode = Session["urLoginId"].ToString().Trim();
            DataTable dtUserMenu = SaitexBL.Interface.Method.UserNavigationRight.GetUserNavigationRightByUserCode(oUserAccessRight);
            if (dtUserMenu != null && dtUserMenu.Rows.Count > 0)
                Session["UserMenu"] = dtUserMenu;
        }
        catch (Exception ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Saitex/Admin/AddNavigation.aspx", false);
    }
    protected void gvNavigation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        lblMessage.Text = "";
        lblErrorMessage.Text = "";
        gvNavigation.PageIndex = e.NewPageIndex;
        bindGvAddModuleNavigation('Y');
    }
    protected void gvNavigation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMessage.Text = "";
        lblErrorMessage.Text = "";

        if (e.CommandName == "RecordEdit")
        {
            ViewState["recordEdit"] = e.CommandArgument.ToString().Trim();
            getNavigationData(Convert.ToInt32(ViewState["recordEdit"]));
        }

        if (e.CommandName == "RecordDelete")
        {
            deleteNavigationData(Convert.ToInt32(e.CommandArgument));
        }
    }

    private void getNavigationData(int iModuleNavigationId)
    {
        try
        {
            string strSQL = "";
            strSQL = "select IN_MODULENAVIGATIONID,c.IN_ADDMODULEID,IN_TABID,vc_ModuleName,VC_CHILDMODULENAME,c.IN_CHILDMODULEID,VC_NAVIGATIONNNAME,VC_NAVIGATEURL,c.IN_DISPLAYORDER,c.IN_POSTEDLENGTH,c.VC_REMARKS,c.CH_STATUS,c.CH_DELETESTATUS,case c.CH_STATUS when '1' then 'Active' when '0' then 'De-Active' else 'Not Defined' end activeDes,case c.CH_DELETESTATUS when '0' then 'Not Deleted' when '1' then 'Deleted' else 'Not Defined' end DelDes,c.DT_CREATED,c.DT_UPDATED from tblAddModuleMaster a, tblAddChildModuleMaster b,tblAddModuleNavigation c where ltrim(rtrim(a.IN_ADDMODULEID))=ltrim(rtrim(b.IN_ADDMODULEID)) and ltrim(rtrim(a.IN_ADDMODULEID))=ltrim(rtrim(c.IN_ADDMODULEID)) and ltrim(rtrim(b.IN_CHILDMODULEID))=ltrim(rtrim(c.IN_CHILDMODULEID)) and ltrim(rtrim(IN_MODULENAVIGATIONID))='" + iModuleNavigationId + "'";
            string strChildModule = "";
            obj = new csSaitex();
            dr = obj.getDataReader(strSQL, CommandType.Text);

            if (dr.Read())
            {
                ddlParenModuleName.SelectedValue = dr["IN_ADDMODULEID"].ToString().Trim();
                //ddlChildModuleName.SelectedValue = dr["IN_CHILDMODULEID"].ToString().Trim();
                strChildModule = dr["IN_CHILDMODULEID"].ToString().Trim();
                txtNavigationName.Text = dr["VC_NAVIGATIONNNAME"].ToString().Trim();
                txtNavigationUrl.Text = dr["VC_NAVIGATEURL"].ToString().Trim();
                ddlTabMaster.SelectedIndex = ddlTabMaster.Items.IndexOf(ddlTabMaster.Items.FindByValue(dr["IN_TABID"].ToString().Trim()));
                txtNavigationUrl.Enabled = true;
                txtDisplayOrder.Text = dr["IN_DISPLAYORDER"].ToString().Trim();
                txtRemarks.Text = dr["VC_REMARKS"].ToString().Trim();


                tdSave.Visible = false;
                tdUpdate.Visible = true;
                lblMode.Text = "Update";
            }

            dr.Close();
            dr.Dispose();
            dr = null;
            bindChildModuleMaster(ddlParenModuleName.SelectedValue.Trim(), strChildModule);

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
            if (obj != null)
            {
                obj = null;
            }
        }
    }

    private void deleteNavigationData(int iModuleNavigationId)
    {
        try
        {
            string strSQL = "";
            strSQL = "delete from tblAddModuleNavigation where ltrim(rtrim(IN_MODULENAVIGATIONID))='" + iModuleNavigationId + "'";
            obj = new csSaitex();
            int iRecordEffected = obj.NonExecuteQuery(strSQL, CommandType.Text);

            if (iRecordEffected == 1)
            {
                lblMessage.Text = iRecordEffected + "&nbsp;&nbsp;Record Deleted Successfully";
                bindGvAddModuleNavigation('Y');
            }
        }

        catch (OracleException ex)
        {
            lblMessage.Text = "";
            if (ex.ErrorCode == -2146232008)
            {
                lblMessage.Text = "This record cann't be deleted because child record exist!";
            }
            else
            {
                lblErrorMessage.Text = ex.Message;
            }
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
            if (obj != null)
            {
                obj = null;
            }
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        bindGvAddModuleNavigation('Y');
    }

    private void bindAddModuleMaster()
    {
        try
        {
            con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            string strSQL = "";
            strSQL = "select MDL_ID,MDL_NAME from CM_MODULE_MST where ltrim(rtrim(STATUS))='1' order by DISP_ODR asc";
            cmd = new OracleCommand(strSQL, con);

            ddlParenModuleName.DataValueField = "MDL_ID";
            ddlParenModuleName.DataTextField = "MDL_NAME";
            ddlParenModuleName.DataSource = cmd.ExecuteReader();
            ddlParenModuleName.DataBind();
            ddlParenModuleName.Items.Insert(0, new ListItem("---------Select----------", ""));
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
            if (con != null)
            {
                con.Close();
                con.Dispose();
                con = null;
            }

            if (cmd != null)
            {
                cmd.Dispose();
                cmd = null;
            }
        }
    }
    private void bindAddModuleMasterRight()
    {
        try
        {
            con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            string strSQL = "";
            strSQL = "select in_AddModuleId,vc_ModuleName from tblAddModuleMaster where ltrim(rtrim(ch_Status))='1' order by vc_ModuleName asc";
            cmd = new OracleCommand(strSQL, con);
            ddlParenModuleNameRight.DataValueField = "in_AddModuleId";
            ddlParenModuleNameRight.DataTextField = "vc_ModuleName";
            ddlParenModuleNameRight.DataSource = cmd.ExecuteReader();
            ddlParenModuleNameRight.DataBind();
            ddlParenModuleNameRight.Items.Insert(0, new ListItem("---------Select----------", ""));
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
            if (con != null)
            {
                con.Close();
                con.Dispose();
                con = null;
            }

            if (cmd != null)
            {
                cmd.Dispose();
                cmd = null;
            }
        }
    }
    private void bindChildModuleMaster(string strModuleCategoryId, string strSelect)
    {
        try
        {
            con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            string strSQL = "";
            strSQL = "select CHILD_MDL_ID,CHILD_MDL_NAME from CM_CHILD_MDL_MST where ltrim(rtrim(MDL_ID))='" + Convert.ToInt32(strModuleCategoryId) + "' and ltrim(rtrim(STATUS))='1' order by DISP_ODR asc";
            cmd = new OracleCommand(strSQL, con);

            ddlChildModuleName.DataValueField = "CHILD_MDL_ID";
            ddlChildModuleName.DataTextField = "CHILD_MDL_NAME";
            ddlChildModuleName.DataSource = cmd.ExecuteReader();
            ddlChildModuleName.DataBind();
            ddlChildModuleName.Items.Insert(0, new ListItem("---------Select----------", ""));

            if (strSelect != "")
            {
                ddlChildModuleName.SelectedValue = strSelect;
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
            if (con != null)
            {
                con.Close();
                con.Dispose();
                con = null;
            }

            if (cmd != null)
            {
                cmd.Dispose();
                cmd = null;
            }
        }
    }
    private void bindAddChildModuleMasterRight()
    {
        try
        {
            con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            string strSQL = "";
            strSQL = "select IN_CHILDMODULEID,VC_CHILDMODULENAME from tblAddChildModuleMaster where ltrim(rtrim(IN_ADDMODULEID))='" + ddlChildModuleNameRight.SelectedValue.Trim() + "' and ltrim(rtrim(ch_Status))='1' order by VC_CHILDMODULENAME asc";
            cmd = new OracleCommand(strSQL, con);

            ddlChildModuleNameRight.DataValueField = "IN_CHILDMODULEID";
            ddlChildModuleNameRight.DataTextField = "VC_CHILDMODULENAME";
            ddlChildModuleNameRight.DataSource = cmd.ExecuteReader();
            ddlChildModuleNameRight.DataBind();
            ddlChildModuleNameRight.Items.Insert(0, new ListItem("---------Select----------", ""));
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
            if (con != null)
            {
                con.Close();
                con.Dispose();
                con = null;
            }

            if (cmd != null)
            {
                cmd.Dispose();
                cmd = null;
            }
        }
    }

    protected void ddlParenModuleName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlParenModuleName.SelectedValue.Trim() != "")
        {
            bindChildModuleMaster(ddlParenModuleName.SelectedValue.Trim(), "");
        }
        else
        {
            ddlChildModuleName.Items.Clear();
        }
    }
    protected void ddlParenModuleNameRight_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlParenModuleNameRight.SelectedValue != "")
        {
            bindAddChildModuleMasterRight();
        }
        else
        {
            ddlChildModuleNameRight.Items.Clear();
        }
    }


    private void BlanksControls()
    {
        try
        {
            ddlParenModuleName.SelectedValue = "";
            ddlChildModuleName.SelectedValue = "";
            txtNavigationName.Text = "";
            txtNavigationUrl.Text = "";
            txtDisplayOrder.Text = "";
            txtRemarks.Text = "";

            tdSave.Visible = true;
            tdUpdate.Visible = false;
            lblMode.Text = "Save";
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
        }

    }

    private void bindGvAddModuleNavigation(char ch_View)
    {
        try
        {
            string strSQL = "select IN_MODULENAVIGATIONID,c.IN_ADDMODULEID,vc_ModuleName,VC_CHILDMODULENAME,c.IN_CHILDMODULEID,VC_NAVIGATIONNNAME,VC_NAVIGATEURL,c.IN_DISPLAYORDER,c.IN_POSTEDLENGTH,c.VC_REMARKS,c.CH_STATUS,c.CH_DELETESTATUS,case c.CH_STATUS when '1' then 'Active' when '0' then 'De-Active' else 'Not Defined' end activeDes,case c.CH_DELETESTATUS when '0' then 'Not Deleted' when '1' then 'Deleted' else 'Not Defined' end DelDes,c.DT_CREATED,c.DT_UPDATED from tblAddModuleMaster a, tblAddChildModuleMaster b,tblAddModuleNavigation c where ltrim(rtrim(a.IN_ADDMODULEID))=ltrim(rtrim(b.IN_ADDMODULEID)) and ltrim(rtrim(a.IN_ADDMODULEID))=ltrim(rtrim(c.IN_ADDMODULEID)) and ltrim(rtrim(b.IN_CHILDMODULEID))=ltrim(rtrim(c.IN_CHILDMODULEID))";
            if (ddlParenModuleName.SelectedValue.Trim() != "")
            {
                strSQL = strSQL + " and ltrim(rtrim(c.IN_ADDMODULEID))='" + ddlParenModuleName.SelectedValue.Trim() + "'";
            }
            if (ddlParenModuleName.SelectedValue.Trim() != "")
            {
                strSQL = strSQL + " and ltrim(rtrim(c.IN_ADDMODULEID))='" + ddlParenModuleName.SelectedValue.Trim() + "'";
            }

            if (ddlChildModuleName.SelectedValue.Trim() != "")
            {
                strSQL = strSQL + " and ltrim(rtrim(c.IN_CHILDMODULEID))='" + ddlChildModuleName.SelectedValue.Trim() + "'";
            }

            if (ddlParenModuleName.SelectedValue.Trim() != "")
            {
                strSQL = strSQL + " and ltrim(rtrim(c.IN_ADDMODULEID))='" + ddlParenModuleName.SelectedValue.Trim() + "'";
            }

            if (radActiveDeActive.SelectedValue.Trim() != "")
            {
                strSQL = strSQL + " and ltrim(rtrim(c.CH_STATUS))='" + radActiveDeActive.SelectedValue.Trim() + "'";
            }
            if (radDeletedNNotDelted.SelectedValue.Trim() != "")
            {
                strSQL = strSQL + " and ltrim(rtrim(c.CH_DELETESTATUS))='" + radDeletedNNotDelted.SelectedValue.Trim() + "'";
            }

            if (ch_View == 'Y')
            {
                strSQL = strSQL + " order by a.IN_DISPLAYORDER asc";
            }
            else
            {
                strSQL = strSQL + " order by DT_UPDATED desc";
            }

            obj = new csSaitex();
            ds = obj.getDataSet(strSQL, CommandType.Text);
            lblTotalRecord.Text = ds.Tables[0].Rows.Count.ToString().Trim();
            gvNavigation.DataSource = ds;
            gvNavigation.DataBind();

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
            if (obj != null)
            {
                obj = null;
            }
        }

    }

    private void BindTabMAsterDropDown()
    {
        try
        {
            DataTable dtTabMaster = SaitexBL.Interface.Method.UserNavigationRight.GetTabMaster();
            if (dtTabMaster != null && dtTabMaster.Rows.Count > 0)
            {
                ddlTabMaster.DataSource = dtTabMaster;
                ddlTabMaster.DataTextField = "TAB_NAME";
                ddlTabMaster.DataValueField = "TAB_ID";
                ddlTabMaster.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string URL = "rptNavigation.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);


        imgbtnSave.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to save this record ?')");
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ?')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ?')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ?')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to exit this record ?')");
    }

    protected void btnRedirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/UserNavigationRight.aspx");
    }
    protected void btnCancelRed_Click(object sender, EventArgs e)
    {

    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string URL = "rptNavigation.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("/Saitex/Admin/AddNavigation.aspx", false);
    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            /////////////////////////////////////   Code to checking the duplicacy  ///////////////////////////////////////////
            string strDup = "";
            int iRecordFound = 0;
            strDup = "select NAV_NAME from CM_NAV_MST where ltrim(rtrim(NAV_NAME))=:NAV_NAME and ltrim(rtrim(MDL_ID))=:MDL_ID and ltrim(rtrim(CHILD_MDL_ID))=:CHILD_MDL_ID order by MDL_ID asc";

            cmd = new OracleCommand(strDup, con);

            param = new OracleParameter(":NAV_NAME", OracleType.VarChar, 100);
            param.Direction = ParameterDirection.Input;
            param.Value = CommonFuction.funFixQuotes(txtNavigationName.Text.Trim());
            cmd.Parameters.Add(param);

            param = new OracleParameter(":MDL_ID", OracleType.Int32);
            param.Direction = ParameterDirection.Input;
            param.Value = ddlParenModuleName.SelectedValue.Trim();
            cmd.Parameters.Add(param);

            param = new OracleParameter(":CHILD_MDL_ID", OracleType.Int32);
            param.Direction = ParameterDirection.Input;
            param.Value = ddlChildModuleName.Text.Trim();
            cmd.Parameters.Add(param);

            strDup = Convert.ToString(cmd.ExecuteOracleScalar());

            if (strDup != "")
            {
                iRecordFound = 1;
                lblMessage.Text = "This navigation already saved! Pls enter another record";
            }
            cmd.Dispose();

            if (iRecordFound == 0)
            {
                /////////////////////////////////////////////// Getting the max Id /////////////////////////////////////////////

                string strMaxId = "select nvl(max(NAV_ID),0) + 1  NAV_ID from CM_NAV_MST";
                obj = new csSaitex();
                strMaxId = obj.executeScalar(strMaxId, CommandType.Text);
                obj = null;

                byte[] bytearr = new byte[tPhoto.PostedFile.ContentLength];
                Stream fs = tPhoto.PostedFile.InputStream;
                if (tPhoto.PostedFile.ContentLength != 0)
                {
                    fs.Read(bytearr, 0, tPhoto.PostedFile.ContentLength);
                }


                string strSQL = "";
                strSQL = "insert into CM_NAV_MST (NAV_ID,CHILD_MDL_ID,MDL_ID,NAV_NAME,NAV_URL,SUB_CAT_IMG,POSTED_LENGTH,SUBCAT_CONTENT_TYPE,DISP_ODR,REMARKS,STATUS,DEL_STATUS,TDATE,TUSER,TAB_ID,IMG_URL) values(:IN_ModuleNavigationId,:IN_CHILDMODULEID,:IN_ADDMODULEID,:VC_NavigationNname,:vc_NavigateUrl,:IM_SUBCATIMAGE,:IN_POSTEDLENGTH,:IM_SUBCATCONTENTTYPE,:IN_DISPLAYORDER,:VC_REMARKS,:CH_STATUS,:CH_DELETESTATUS,:TDATE,:TUSER,:in_TabId,:ImageURL)";

                cmd = new OracleCommand(strSQL, con);

                param = new OracleParameter(":IN_ModuleNavigationId", OracleType.Int32);
                param.Direction = ParameterDirection.Input;
                param.Value = Convert.ToInt32(strMaxId);
                cmd.Parameters.Add(param);

                param = new OracleParameter(":in_TabId", OracleType.Int32);
                param.Direction = ParameterDirection.Input;
                int TabId = 1;
                int.TryParse(ddlTabMaster.SelectedValue.Trim(), out TabId);
                param.Value = TabId;
                cmd.Parameters.Add(param);

                param = new OracleParameter(":IN_ADDMODULEID", OracleType.Int32);
                param.Direction = ParameterDirection.Input;
                param.Value = ddlParenModuleName.SelectedValue.Trim();
                cmd.Parameters.Add(param);

                param = new OracleParameter(":IN_CHILDMODULEID", OracleType.Int32);
                param.Direction = ParameterDirection.Input;
                param.Value = ddlChildModuleName.SelectedValue.Trim();
                cmd.Parameters.Add(param);

                param = new OracleParameter(":VC_NAVIGATIONNNAME", OracleType.VarChar, 100);
                param.Direction = ParameterDirection.Input;
                param.Value = CommonFuction.funFixQuotes(txtNavigationName.Text.Trim());
                cmd.Parameters.Add(param);

                param = new OracleParameter(":VC_NAVIGATEURL", OracleType.VarChar, 250);
                param.Direction = ParameterDirection.Input;
                param.Value = CommonFuction.funFixQuotes(txtNavigationUrl.Text.Trim());
                cmd.Parameters.Add(param);

                if (tPhoto.PostedFile.ContentLength > 0 && tPhoto.PostedFile.ContentLength < 8388609)
                {
                    if (tPhoto.PostedFile.ContentType == "image/jpeg" || tPhoto.PostedFile.ContentType == "image/pjpeg" || tPhoto.PostedFile.ContentType == "image/gif" || tPhoto.PostedFile.ContentType == "image/bmp" || tPhoto.PostedFile.ContentType == "image/x-png")
                    {
                        param = new OracleParameter(":im_SubCatImage", OracleType.LongRaw);
                        param.Direction = ParameterDirection.Input;
                        param.Value = bytearr;
                        cmd.Parameters.Add(param);

                        param = new OracleParameter(":in_PostedLength", OracleType.Int32);
                        param.Direction = ParameterDirection.Input;
                        param.Value = tPhoto.PostedFile.ContentLength;
                        cmd.Parameters.Add(param);

                        param = new OracleParameter(":im_SubCatContentType", OracleType.VarChar, 50);
                        param.Direction = ParameterDirection.Input;
                        param.Value = tPhoto.PostedFile.ContentType;
                        cmd.Parameters.Add(param);

                        string ImageURL = "";
                        ImageURL = CommonFuction.SaveImageFile(tPhoto, "CommonImages", "Navigation");
                        tPhoto.PostedFile.SaveAs(Server.MapPath(@"~/" + ImageURL));
                        param = new OracleParameter(":ImageURL", OracleType.VarChar, 150);
                        param.Direction = ParameterDirection.Input;
                        param.Value = ImageURL;
                        cmd.Parameters.Add(param);

                    }
                }

                param = new OracleParameter(":in_displayOrder", OracleType.Int32);
                param.Direction = ParameterDirection.Input;
                param.Value = CommonFuction.funFixQuotes(txtDisplayOrder.Text.Trim());
                cmd.Parameters.Add(param);

                param = new OracleParameter(":vc_Remarks", OracleType.VarChar, 250);
                param.Direction = ParameterDirection.Input;
                param.Value = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
                cmd.Parameters.Add(param);

                if (chk_Status.Checked == true)
                {
                    param = new OracleParameter(":ch_Status", OracleType.Char, 1);
                    param.Direction = ParameterDirection.Input;
                    param.Value = '1';
                    cmd.Parameters.Add(param);
                }
                else
                {
                    param = new OracleParameter(":ch_Status", OracleType.Char, 1);
                    param.Direction = ParameterDirection.Input;
                    param.Value = '0';
                    cmd.Parameters.Add(param);
                }

                param = new OracleParameter(":ch_DeleteStatus", OracleType.Char, 1);
                param.Direction = ParameterDirection.Input;
                param.Value = '0';
                cmd.Parameters.Add(param);

                param = new OracleParameter(":TUSER", OracleType.VarChar, 15);
                param.Direction = ParameterDirection.Input;
                param.Value = "";
                cmd.Parameters.Add(param);

                param = new OracleParameter(":TDATE", OracleType.DateTime);
                param.Direction = ParameterDirection.Input;
                param.Value = System.DateTime.Now;
                cmd.Parameters.Add(param);

                int iRecordEffected = cmd.ExecuteNonQuery();

                if (iRecordEffected == 1)
                {
                    //Session["saveStatus"] = 1;
                    BlanksControls();
                    //Response.Redirect("/Saitex/Admin/AddNavigation.aspx?cId=S", false);
                    lblRedirect.Text = "Data Saved Successfully ,Go To UserNavigationRight page";
                    mpeRed.Show();

                }

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
            if (con != null)
            {
                con.Close();
                con.Dispose();
                con = null;
            }

            if (cmd != null)
            {
                cmd.Dispose();
                cmd = null;
            }

            if (param != null)
            {
                param = null;
            }
            if (obj != null)
            {
                obj = null;
            }
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
                Response.Redirect("~/Module/Admin/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtNavigationUrl.Enabled = true;
            con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            if (tPhoto.PostedFile != null)
            {
                if (tPhoto.PostedFile.ContentLength != 0)
                {
                    byte[] bytearr = new byte[tPhoto.PostedFile.ContentLength];
                    Stream fs = tPhoto.PostedFile.InputStream;
                    fs.Read(bytearr, 0, tPhoto.PostedFile.ContentLength);
                }
            }

            string strSQL = "";
            if (tPhoto.Value != "")
            {
                strSQL = "update tblAddModuleNavigation set VC_NAVIGATIONNNAME=:VC_NAVIGATIONNNAME, IN_ADDMODULEID=:IN_ADDMODULEID,IN_CHILDMODULEID=:IN_CHILDMODULEID,IM_SUBCATIMAGE=:IM_SUBCATIMAGE,IN_POSTEDLENGTH=:IN_POSTEDLENGTH,IM_SUBCATCONTENTTYPE=:IM_SUBCATCONTENTTYPE,IN_DISPLAYORDER=:IN_DISPLAYORDER, VC_REMARKS=:VC_REMARKS,CH_STATUS=:CH_STATUS,DT_UPDATED=:DT_UPDATED,IN_TABID=:IN_TABID where ltrim(rtrim(IN_MODULENAVIGATIONID))='" + Convert.ToInt32(ViewState["recordEdit"]) + "'";
            }
            else
            {
                strSQL = "update tblAddModuleNavigation set VC_NAVIGATIONNNAME=:VC_NAVIGATIONNNAME,IN_ADDMODULEID=:IN_ADDMODULEID,IN_CHILDMODULEID=:IN_CHILDMODULEID,IN_DISPLAYORDER=:IN_DISPLAYORDER, VC_REMARKS=:VC_REMARKS,CH_STATUS=:CH_STATUS,DT_UPDATED=:DT_UPDATED,IN_TABID=:IN_TABID where ltrim(rtrim(IN_MODULENAVIGATIONID))='" + Convert.ToInt32(ViewState["recordEdit"]) + "'";
            }

            cmd = new OracleCommand(strSQL, con);
            param = new OracleParameter(":IN_ADDMODULEID", OracleType.Int32);
            param.Direction = ParameterDirection.Input;
            param.Value = ddlParenModuleName.SelectedValue.Trim();
            cmd.Parameters.Add(param);

            param = new OracleParameter(":VC_NAVIGATIONNNAME", OracleType.VarChar, 100);
            param.Direction = ParameterDirection.Input;
            param.Value = CommonFuction.funFixQuotes(txtNavigationName.Text.Trim());
            cmd.Parameters.Add(param);

            param = new OracleParameter(":IN_TABID", OracleType.Int32);
            param.Direction = ParameterDirection.Input;
            param.Value = Convert.ToInt32(ddlTabMaster.SelectedValue.Trim());
            cmd.Parameters.Add(param);

            param = new OracleParameter(":IN_CHILDMODULEID", OracleType.Int32);
            param.Direction = ParameterDirection.Input;
            param.Value = ddlChildModuleName.SelectedValue.Trim();
            cmd.Parameters.Add(param);

            if (tPhoto.Value != "")
            {
                if (tPhoto.PostedFile.ContentLength > 0 && tPhoto.PostedFile.ContentLength < 8388609)
                {
                    if (tPhoto.PostedFile.ContentType == "image/pjpeg" || tPhoto.PostedFile.ContentType == "image/gif" || tPhoto.PostedFile.ContentType == "image/bmp" || tPhoto.PostedFile.ContentType == "image/x-png")
                    {
                        byte[] bytearr = new byte[tPhoto.PostedFile.ContentLength];
                        param = new OracleParameter(":im_SubCatImage", OracleType.LongRaw);
                        param.Direction = ParameterDirection.Input;
                        param.Value = bytearr;
                        cmd.Parameters.Add(param);

                        param = new OracleParameter(":in_PostedLength", OracleType.Int32);
                        param.Direction = ParameterDirection.Input;
                        param.Value = tPhoto.PostedFile.ContentLength;
                        cmd.Parameters.Add(param);

                        param = new OracleParameter(":im_SubCatContentType", OracleType.VarChar, 50);
                        param.Direction = ParameterDirection.Input;
                        param.Value = tPhoto.PostedFile.ContentType;
                        cmd.Parameters.Add(param);

                    }
                }
            }
            param = new OracleParameter(":in_displayOrder", OracleType.Int32);
            param.Direction = ParameterDirection.Input;
            param.Value = CommonFuction.funFixQuotes(txtDisplayOrder.Text.Trim());
            cmd.Parameters.Add(param);

            param = new OracleParameter(":vc_Remarks", OracleType.VarChar, 250);
            param.Direction = ParameterDirection.Input;
            param.Value = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            cmd.Parameters.Add(param);

            if (chk_Status.Checked == true)
            {
                param = new OracleParameter(":ch_Status", OracleType.Char, 1);
                param.Direction = ParameterDirection.Input;
                param.Value = '1';
                cmd.Parameters.Add(param);
            }
            else
            {
                param = new OracleParameter(":ch_Status", OracleType.Char, 1);
                param.Direction = ParameterDirection.Input;
                param.Value = '0';
                cmd.Parameters.Add(param);
            }

            param = new OracleParameter(":dt_Updated", OracleType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = System.DateTime.Now;
            cmd.Parameters.Add(param);

            int iRecordEffected = cmd.ExecuteNonQuery();

            if (iRecordEffected == 1)
            {
                //Session["saveStatus"] = 1;
                BlanksControls();
                //Response.Redirect("/Saitex/Admin/AddNavigation.aspx?cId=U", false);
                lblRedirect.Text = "Data Saved Successfully <br/>Go To UserNavigationRight page";
                mpeRed.Show();


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
            if (con != null)
            {
                con.Close();
                con.Dispose();
                con = null;
            }

            if (cmd != null)
            {
                cmd.Dispose();
                cmd = null;
            }

            if (param != null)
            {
                param = null;
            }
            if (obj != null)
            {
                obj = null;
            }
        }




    }
}
