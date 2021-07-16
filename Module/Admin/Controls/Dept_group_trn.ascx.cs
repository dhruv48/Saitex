using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using errorLog;
using System.Data;

public partial class Module_Admin_Controls_Dept_group_trn : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetaill;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetaill = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                BindDeptGroup();
                BindDepartment();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, "Problem in page loading.\r\nSee error log for detail."));
        }
    }

    private void BindDeptGroup()
    {
        try
        {
            ddlDeptGroup.Items.Add(new ListItem("Select", "Select"));
            DataTable dtDeptGroup = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("DEPT_GROUP", oUserLoginDetaill.COMP_CODE);
            if (dtDeptGroup != null && dtDeptGroup.Rows.Count > 0)
            {
                ddlDeptGroup.DataSource = dtDeptGroup;
                ddlDeptGroup.DataTextField = "MST_CODE";
                ddlDeptGroup.DataValueField = "MST_CODE";
                ddlDeptGroup.DataBind();
            }
            else
            {
                CommonFuction.ShowMessage("No department group found.");
            }
        }
        catch
        {
            throw;
        }
    }

    private void BindDepartment()
    {
        try
        {
            DataTable dtDept = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
            if (dtDept != null && dtDept.Rows.Count > 0)
            {
                chklstDept.DataSource = dtDept;
                chklstDept.DataTextField = "DEPT_NAME";
                chklstDept.DataValueField = "DEPT_CODE";
                chklstDept.DataBind();
            }
            else
            {
                CommonFuction.ShowMessage("No department found.");
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlDeptGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            chklstDept.ClearSelection();
            if (ddlDeptGroup.SelectedIndex != -1 && ddlDeptGroup.SelectedValue != "Select")
            {
                MapDepartment();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, "Problem in Department Group.\r\nSee error log for detail."));
        }
    }

    private void MapDepartment()
    {
        try
        {
            DataTable dtDeptGrpInt = SaitexBL.Interface.Method.CM_DEPT_GROUP_INT.SelectByDeptGroup(oUserLoginDetaill.COMP_CODE, ddlDeptGroup.SelectedValue);
            if (dtDeptGrpInt != null && dtDeptGrpInt.Rows.Count > 0)
            {
                foreach (DataRow dr in dtDeptGrpInt.Rows)
                {
                    chklstDept.Items[chklstDept.Items.IndexOf(chklstDept.Items.FindByValue(dr["DEPT_CODE"].ToString()))].Selected = true;
                }
            }
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
            SaveData();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, "Problem in saving data.\r\nSee error log for detail."));
        }
    }

    private void SaveData()
    {
        try
        {
            int iSelected = 0;
            string successmsg = string.Empty;
            string failuremsg = string.Empty;

            if (ddlDeptGroup.SelectedIndex != -1 && ddlDeptGroup.SelectedValue != "Select")
            {
                string DeptGroup = ddlDeptGroup.SelectedValue;
                foreach (ListItem lst in chklstDept.Items)
                {
                    if (lst.Selected)
                    {
                        iSelected = iSelected + 1;
                        if (iSelected == 1)
                        {
                            SaitexBL.Interface.Method.CM_DEPT_GROUP_INT.Delete(oUserLoginDetaill.COMP_CODE, DeptGroup);
                        }

                        bool bResult = SaitexBL.Interface.Method.CM_DEPT_GROUP_INT.Insert(oUserLoginDetaill.COMP_CODE, DeptGroup, lst.Value.Trim(), System.DateTime.Now, oUserLoginDetaill.UserCode);
                        if (bResult)
                        {
                            successmsg = successmsg + "</ br>" + lst.Text.Trim();
                        }
                        else
                        {
                            failuremsg = failuremsg + "</ br> " + lst.Text.Trim();
                        }
                    }
                }
                if (iSelected > 0)
                {
                    string msg = string.Empty;
                    if (successmsg != string.Empty)
                    {
                        msg = msg + "record saved for group." + ddlDeptGroup.SelectedValue.Trim() + " and department " + successmsg;
                    }
                    if (failuremsg != string.Empty)
                    {
                        msg = msg + "</ br> record saved failed for group." + ddlDeptGroup.SelectedValue.Trim() + " and department " + failuremsg;
                    }

                    msg = msg.Replace("</ br>", "\\r\\n");

                    Common.CommonFuction.ShowMessage(msg);
                    RefreshData();
                }
                else
                {
                    CommonFuction.ShowMessage("Please select department to integrate.");
                }
            }
            else
            {
                CommonFuction.ShowMessage("Please select department group to integrate.");
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
            RefreshData();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, "Problem in reset data.\r\nSee error log for detail."));
        }
    }

    private void RefreshData()
    {
        try
        {
            ddlDeptGroup.SelectedIndex = -1;
            chklstDept.ClearSelection();
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }

}
