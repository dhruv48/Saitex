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

using Common;
using errorLog;
using System.IO;
using DBLibrary;

public partial class Module_HRMS_Controls_EmpSalaryMaster : System.Web.UI.UserControl
{
    string strTUser = string.Empty;
    string empId;
   private static  string gradeId = string.Empty;
    SaitexDM.Common.DataModel.HR_SAL_GRD HSG = new SaitexDM.Common.DataModel.HR_SAL_GRD();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["urLoginId"] != null)
            {
                if (Request.QueryString["EMP_CODE"] != null && Request.QueryString["GradeID"] != null)
                {
                    empId = Request.QueryString["EMP_CODE"].ToString().Trim();
                    gradeId = Request.QueryString["GradeID"].Trim();
                    HSG.GRADE_ID = gradeId.Trim().ToString();
                    if (!IsPostBack)
                    {
                        if (empId != null || gradeId != null)
                        {
                            bindempname();
                            bindGradeName();
                            bindSalaryHeadMaster();
                            getEmpSalRecord();
                        }
                    }
                }
            }
            else
            {
                Response.Redirect("/Saitex/Default.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading..\r\nSee Error Log"));
        }
    }
    protected void gvSalaryGrade_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label HeadId = (Label)e.Row.FindControl("lblHeadId");
                GridView gvSubHead = (GridView)e.Row.FindControl("gvSubHead");

                DataTable dt = SaitexBL.Interface.Method.HR_SAL_GRD.GetSubHeadMaster(gradeId);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataView dv = new DataView(dt);
                    dv.RowFilter = "HEAD_ID='" + Convert.ToInt16(HeadId.Text.Trim()) + "'";
                    if (dv.Count > 0)
                    {
                        for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                        {
                            gvSubHead.DataSource = dv;
                            gvSubHead.DataBind();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in GridView Row Command..\r\nSee Error Log"));
        }
    }     
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        int Res = 0;
        if (Page.IsValid)
        {
            if (gvSalaryGrade.Rows.Count > 0)
            {
              
                try
                {
                    foreach (GridViewRow rw in gvSalaryGrade.Rows)
                    {
                        Label lblHeadId = (Label)rw.FindControl("lblHeadId");
                        GridView gvSubHead = (GridView)rw.FindControl("gvSubHead");
                        foreach (GridViewRow rw1 in gvSubHead.Rows)
                        {
                            Label lblSubHeadId = (Label)rw1.FindControl("lblSubHeadId");
                            TextBox txtDefaultValue = (TextBox)rw1.FindControl("txtDefaultValue");
                            int IN_HEADID, IN_SUBHEADNAMEMASTER;
                            float ft_Amount = 0;
                            IN_HEADID = Convert.ToInt32(lblHeadId.Text.Trim());
                            IN_SUBHEADNAMEMASTER = Convert.ToInt32(lblSubHeadId.Text.Trim());
                            ft_Amount = float.Parse((txtDefaultValue.Text.Trim()));
                               bool Result=saveEmpSalary(IN_HEADID, IN_SUBHEADNAMEMASTER, ft_Amount);
                               if (Result)
                               {
                                   Res = Res + 1;
                               }                            
                        }
                    }
                    if (Res > 0)
                    {
                        bool r1 = SaitexBL.Interface.Method.HR_EMP_SAL_MST.Save_Salry_Type(empId, RBSalaryType.SelectedValue.Trim().ToString());
                        if (r1)
                        {
                            Common.CommonFuction.ShowMessage("Record Save Sucessfully");
                        }
                        else
                        {
                            Common.CommonFuction.ShowMessage("Unable to Save!Please try again ");
                        }
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Unable to Save!Please try again ");
                    }
                }
                catch (Exception ex)
                {
                    Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving Records..\r\nSee Error Log"));
                }
            }           
        }
    } 
    
    private void bindempname()
    {
        try
        {
            SaitexDM.Common.DataModel.HR_EMP_SAL_MST oHR_EMP_SAL_MST = new SaitexDM.Common.DataModel.HR_EMP_SAL_MST();
            oHR_EMP_SAL_MST.EMP_ID = empId;
            DataTable dt = SaitexBL.Interface.Method.HR_EMP_SAL_MST.GetEmpDetail(oHR_EMP_SAL_MST);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        lblEmployeeCode.Text = dv[iLoop]["EMP_CODE"].ToString();
                        lblEmployeeName.Text = dv[iLoop]["F_NAME"].ToString() + " " + dv[iLoop]["M_NAME"].ToString() + " " + dv[iLoop]["L_NAME"].ToString();
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }
    private void bindGradeName()
    {
        try
        {
            SaitexDM.Common.DataModel.HR_EMP_SAL_MST oHR_EMP_SAL_MST = new SaitexDM.Common.DataModel.HR_EMP_SAL_MST();

            oHR_EMP_SAL_MST.GRADE_ID = gradeId;

            DataTable dt = SaitexBL.Interface.Method.HR_EMP_SAL_MST.GetGradeDetail(oHR_EMP_SAL_MST);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        lblGrade.Text = dv[iLoop]["MST_DESC"].ToString().Trim();
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }
    private void bindSalaryHeadMaster()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.HR_SAL_GRD.GetHeadMaster();

            if (dt != null && dt.Rows.Count > 0)
            {
                gvSalaryGrade.DataSource = dt;
                gvSalaryGrade.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }
    private bool saveEmpSalary(int IN_HEADID, int IN_SUBHEADNAMEMASTER, float ft_Amount)
    {
        bool Result = false;
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            strTUser = oUserLoginDetail.LOGINDETAILID;
           SaitexDM.Common.DataModel.HR_EMP_SAL_MST oHR_EMP_SAL_MST = new SaitexDM.Common.DataModel.HR_EMP_SAL_MST();
            oHR_EMP_SAL_MST.GRADE_ID = gradeId;
            oHR_EMP_SAL_MST.HEAD_ID = IN_HEADID;
            oHR_EMP_SAL_MST.SUBH_ID = IN_SUBHEADNAMEMASTER;
            oHR_EMP_SAL_MST.EMP_ID = empId;
            oHR_EMP_SAL_MST.AMT = ft_Amount;           
            oHR_EMP_SAL_MST.TUSER = strTUser;
            //oHR_EMP_SAL_MST.SAL_TYPE=RBSalaryType.SelectedValue.Trim().ToString();
            bool bResult = SaitexBL.Interface.Method.HR_EMP_SAL_MST.Insert(oHR_EMP_SAL_MST);

            if (bResult)
            {
                Result= true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('This Record is already saved.. Please enter another.');", true);
                Result=false;
            }
            return Result;
        }
        catch
        {
            throw;
        }
    }  
    private void getEmpSalRecord()
    {
        try
        {
            SaitexDM.Common.DataModel.HR_EMP_SAL_MST oHR_EMP_SAL_MST = new SaitexDM.Common.DataModel.HR_EMP_SAL_MST();
            oHR_EMP_SAL_MST.EMP_ID = empId;
            oHR_EMP_SAL_MST.GRADE_ID = gradeId;
            DataTable dt = SaitexBL.Interface.Method.HR_EMP_SAL_MST.GetEmpSalDetail(oHR_EMP_SAL_MST);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string EmpSalId = string.Empty;
                    string amt;
                    EmpSalId = dr["HEAD_ID"].ToString().Trim();
                    decimal  subheadid = decimal .Parse(dr["SUBH_ID"].ToString().Trim());
                    amt = dr["AMT"].ToString().Trim();
                    SetEmpSal(Convert.ToInt32(EmpSalId.Trim()), subheadid, decimal.Parse (amt.Trim()));
                }
            }
            else
            {
            }
        }
        catch
        {
            throw;
        }
    }
    private void SetEmpSal(int iVCHEADid, decimal  subheadid, decimal  Amount)
    {
        try
        {
            if (gvSalaryGrade.Rows.Count > 0)
            {
                foreach (GridViewRow Row in gvSalaryGrade.Rows)
                {
                    Label lblHeadName = (Label)Row.FindControl("lblHeadName");
                    Label lblHeadId = (Label)Row.FindControl("lblHeadId");
                    int headId = Convert.ToInt32(lblHeadId.Text.Trim());

                    if (headId == iVCHEADid)
                    {
                        GridView gvSubHead = (GridView)Row.FindControl("gvSubHead");
                        foreach (GridViewRow rw1 in gvSubHead.Rows)
                        {
                            Label lblSubHeadId = (Label)rw1.FindControl("lblSubHeadId");
                            decimal  iSHeadId = decimal.Parse(lblSubHeadId.Text.Trim());
                            if (iSHeadId == subheadid)
                            {
                                TextBox txtAmount = (TextBox)rw1.FindControl("txtDefaultValue");
                                decimal Amt = Math.Round(Amount, 2);
                                txtAmount.Text = Amt.ToString();
                            }
                        }
                    }
                }
            }
        }
        catch
        {
            throw;
        }
       
    }
    protected void gvSubHead_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((TextBox)e.Row.FindControl("txtDefaultValue")).Attributes.Add("onkeyup", "javascript:pricevalidate(this);");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in GridView Row Data Bind..\r\nSee Error Log"));
        } 
    }
}
