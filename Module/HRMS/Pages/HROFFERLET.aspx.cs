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

using SaitexBL.Interface.Method;
using System.Data.OracleClient;
using errorLog;
using Common;
using DBLibrary;


public partial class Module_HRMS_Pages_HROFFERLET : System.Web.UI.Page
{

    private static int MaxRefNO;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["urLoginId"] != null)
            {
                if (!IsPostBack)
                {
                    InitialisePage();
                    Bind_Postion();
                    Load_Max_RefNo();
                }
            }
            else
            {
                Response.Redirect("/Saitex/default.aspx", false);
            }            
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('" + ex.Message + "');", true);
       
        }
    }
    private void Bind_Postion()
    {
        try
        {
            //////////////////////////// Bind Branch Name//////////////////////////////////////
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.Bind_Position();
            DDLPosition.DataSource = dt;
            DDLPosition.DataValueField = "POSITION_CODE";
            DDLPosition.DataTextField = "POSITION_NAME";
            DDLPosition.DataBind();
            //DDLPosition.Items.Insert(0, new ListItem("------------Select------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
            ErrHandler.WriteError(ex.Message);
        }
    }
    private void Load_Max_RefNo()
    {
        try
        {
            MaxRefNO = SaitexBL.Interface.Method.HR_OFFER_LET.GetNewRefNo ();
            string StrRefNo = "STL/HRD/OFFER/" + System.DateTime.Now.Year + "/" + MaxRefNO;
            txtOFFERR.Text = StrRefNo.ToString();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
        }
    }
    private void InitialisePage()
    {
        try
        {
            tdClear.Visible = true;
            txtOFFERR.ReadOnly = true;
            tdExit.Visible = true;
            tdFind.Visible = true;
            tdSave.Visible = true;
            tdprint.Visible = false;
            tdUpdate.Visible = false;
            ValidationSummary2.Visible = true;
            txtADDRESS1.Text = "";
            txtADDRESS2.Text = "";
            txtOFFERDATE.Text = "";
            txtOFFERJOING.Text = "";
            txtOFFERName.Text = "";
            txtOFFERR.Text = "";
            DDLPosition.SelectedIndex = 0;
            txtSUBJECT.Text = "";
        }
        catch (Exception ex)
        {
            throw ex;   
        } 
    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        if (Page.IsValid)
        {
            SaveOfferLetterData();
        }
    }
    
    protected void SaveOfferLetterData()
    {
        try
        {
            
           
                SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                SaitexDM.Common.DataModel.HR_OFFER_LET oHR_OFFER_LET = new SaitexDM.Common.DataModel.HR_OFFER_LET();

               
                oHR_OFFER_LET.OFF_DATE = Convert.ToDateTime(txtOFFERDATE.Text.ToString().Trim());
                oHR_OFFER_LET.OFF_REF_NO = MaxRefNO;
                oHR_OFFER_LET.OFF_NAME = CommonFuction.funFixQuotes(txtOFFERName.Text.Trim());
                oHR_OFFER_LET.ADD1 = CommonFuction.funFixQuotes(txtADDRESS1.Text.Trim());
                oHR_OFFER_LET.ADD2 = CommonFuction.funFixQuotes(txtADDRESS2.Text.Trim());
                oHR_OFFER_LET.SUB = CommonFuction.funFixQuotes(txtSUBJECT.Text.ToString().Trim());
                oHR_OFFER_LET.OFF_JOING_DATE = Convert.ToDateTime(txtOFFERJOING.Text.ToString().Trim());            
                oHR_OFFER_LET.POSSITION = CommonFuction.funFixQuotes(DDLPosition.SelectedValue.ToString());
                oHR_OFFER_LET.STATUS = true;
                oHR_OFFER_LET.TUSER = oUserLoginDetail.UserCode;               
                int iRecordFound = 0;
                bool Result = SaitexBL.Interface.Method.HR_OFFER_LET.Insert(oHR_OFFER_LET, out iRecordFound);
                if (Result)
                {                   
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('New Employee Offer Letter Data saved Successfully');", true);
                }
                else if (iRecordFound > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('New Employee Offer Letter Data not saved Successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('New Employee Offer Letter Data not saved Successfully');", true);
                }                       
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('Record is all Ready Exist');", true);
        }
      

    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        UpdateOfferLetter();
    }
    protected void UpdateOfferLetter()
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            SaitexDM.Common.DataModel.HR_OFFER_LET oHR_OFFER_LET = new SaitexDM.Common.DataModel.HR_OFFER_LET();
           
            oHR_OFFER_LET.OFF_REF_NO = MaxRefNO;
            oHR_OFFER_LET.OFF_DATE = Convert.ToDateTime(CommonFuction.funFixQuotes(txtOFFERDATE.Text.Trim()));
            oHR_OFFER_LET.OFF_NAME = CommonFuction.funFixQuotes(txtOFFERName.Text.Trim());
            oHR_OFFER_LET.ADD1 = CommonFuction.funFixQuotes(txtADDRESS1.Text.Trim());
            oHR_OFFER_LET.ADD2 = CommonFuction.funFixQuotes(txtADDRESS2.Text.Trim());
            oHR_OFFER_LET.SUB = CommonFuction.funFixQuotes(txtSUBJECT.Text.ToString().Trim());
            oHR_OFFER_LET.OFF_JOING_DATE = Convert.ToDateTime(CommonFuction.funFixQuotes(txtOFFERJOING.Text.Trim()));
            oHR_OFFER_LET.POSSITION = CommonFuction.funFixQuotes(DDLPosition.SelectedValue.ToString());
            oHR_OFFER_LET.STATUS = true;
            oHR_OFFER_LET.TUSER = oUserLoginDetail.UserCode;
            oHR_OFFER_LET.DEL_STATUS = false;

            
            int iRecordFound = 0;
            bool Result = SaitexBL.Interface.Method.HR_OFFER_LET.Update(oHR_OFFER_LET, out iRecordFound);
            if (Result)
            {               
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('Offer Letter Update Successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('Offer Letter not Update Successfully');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('Offer Letter Entry Can not Blank');", true);
        }

    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        InitialisePage();        
        txtOFFERR.ReadOnly = false;
        tdClear.Visible = true;
        tdExit.Visible = true;
        tdFind.Visible = true;
        tdSave.Visible = false;
        tdprint.Visible = true;
        tdUpdate.Visible = true;  
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        InitialisePage();
        Response.Redirect("/Saitex/Module/HRMS/Pages/HROFFERLET.aspx", false);
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string URL = "../Reports/HROfferLettterR.aspx?RefNo=" + MaxRefNO +"&Ref="+ txtOFFERR.Text.Trim().ToString();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
        
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
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnSave.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save this record ? ')");
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");

        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnFind.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Find Some Control ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ?')");

    }
    protected void txtOFFERR_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable DTable = SaitexBL.Interface.Method.HR_OFFER_LET.GetReportData(txtOFFERR.Text.Trim().ToString());
            if(DTable.Rows.Count>0)
            {
                MaxRefNO=int.Parse(DTable.Rows[0]["OFF_REF_NO"].ToString());
                txtOFFERR.Text = "STL/HRD/OFFER/" + System.DateTime.Now.Year + "/" + MaxRefNO;
                txtADDRESS1.Text=DTable.Rows[0]["ADD1"].ToString();
                txtADDRESS2.Text=DTable.Rows[0]["ADD2"].ToString();
                txtSUBJECT.Text=DTable.Rows[0]["SUB"].ToString();
                txtOFFERDATE .Text=DTable.Rows[0]["OFF_DATE"].ToString();
                txtOFFERName.Text=DTable.Rows[0]["OFF_NAME"].ToString();
                txtOFFERJOING.Text=DTable.Rows[0]["OFF_JOING_DATE"].ToString();
               DDLPosition.SelectedValue=DTable.Rows[0]["POSSITION"].ToString();
                if(DTable.Rows[0]["STATUS"].ToString()=="1")
                {
                    chk_Status.Checked=true;
                }
                else
                {
                    chk_Status.Checked=false ;
                }
            }
        }
        catch (Exception EX)
        {

        }
    }
}
