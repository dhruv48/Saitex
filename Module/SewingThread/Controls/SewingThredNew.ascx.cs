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

public partial class Module_Sewing_Thread_Controls_SewingThredNew : System.Web.UI.UserControl
{
    public static string strCompanyCode = string.Empty;
    public static string strBranchCode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["usrNames"] != null)
            {
                SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                strCompanyCode = oUserLoginDetail.COMP_CODE.Trim().ToString();
                strBranchCode = oUserLoginDetail.CH_BRANCHCODE.Trim().ToString();
                if (!IsPostBack)
                {
                    Initial_Control();
                }
            }
        }
        catch(Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Page loading"));
        }
    }
    private void Initial_Control()
    {
        try
        {
            Clear_Control();            
            //UOM
            Bind_Control(DDLUnit, "UOM");
            //PLY
            Bind_Control(DDLPly, "YARN_PLY");
            //TWIST
            Bind_Control(DDLTwist, "YARN_TWIST");
            //QUALITY
            Bind_Control(DDLQuality, "YARN_QUALITY");
            //BRAND
            Bind_Control(DDlBrand , "BRAND");
            //COLUR OF UNIT
            Bind_Control(DDLColor , "COU");
            //END USE
            Bind_Control(DDLEndUse , "END_USE");
        }
        catch 
        {
            throw;
        }
    }
    private void Clear_Control()
    {
        try
        {
            Max_Artical_Code();
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            tdFind.Visible = true;
            DDLArticalCode.Visible = false ;
            TxtArticalCode.Visible = true;
            TxtLenght.Text = string.Empty;          
            TxtTexSize.Text = string.Empty;
            TxtTPI.Text = string.Empty;
            TxtUnitSize.Text = string.Empty;
            TxtUnitWt.Text  = string.Empty;
            TxtTktNo.Text = string.Empty;
            DDLArticalCode.SelectedIndex = -1;
            DDlBrand.SelectedIndex = -1;
            DDLColor.SelectedIndex = -1;
            DDLEndUse.SelectedIndex = -1;
            DDLPly.SelectedIndex = -1;
            DDLQuality.SelectedIndex = -1;
            DDLTwist.SelectedIndex = -1;
            DDLUnit.SelectedIndex = -1; 
        }
        catch
        {
            throw;
        }
    }
    private void Max_Artical_Code()
    {
        try
        {
            string ArticalCode = SaitexBL.Interface.Method.ST_ARTICAL_MST.Get_Max_Artical_Code(strCompanyCode.Trim().ToString());
            TxtArticalCode.Text = ArticalCode.ToString();
        }
        catch
        {
            throw;
        }
    }    
    private void Bind_Control(DropDownList DDL,string MST_NAME)
    {
        try
        {
            DDL.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME , strCompanyCode);
            DDL.DataSource = dt;
            DDL.DataTextField = "MST_DESC";
            DDL.DataValueField = "MST_CODE";
            DDL.DataBind();
            DDL.Items.Insert(0, new ListItem("--------------Select--------------"));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Save_Record())
            {
                Clear_Control();
                Common.CommonFuction.ShowMessage("Record save sucessfully");
            }
            else
            {
                Common.CommonFuction.ShowMessage("unable to save!please try again");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex,"Problem in saving the record"));
        }
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Save_Record())
            {
                Clear_Control();
                Common.CommonFuction.ShowMessage("Record Update sucessfully");
            }
            else
            {
                Common.CommonFuction.ShowMessage("unable to Update!please try again");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in saving the record"));
        }

    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Clear_Control();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in clearing the control"));
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Exit.\\r\\nSee error log for detail."));
        }
    }
    private bool Save_Record()
    {
        try
        {
            SaitexDM.Common.DataModel.ST_ARTICAL_MST ST = new SaitexDM.Common.DataModel.ST_ARTICAL_MST();
            ST.ARTICAL_CODE = TxtArticalCode.Text.Trim().ToString();
            ST.BRANCH_CODE = strBranchCode.Trim().ToString();
            if (DDlBrand.SelectedIndex != 0)
            {
                ST.BRAND = DDlBrand.SelectedValue.Trim().ToString();
            }           
            if (DDLUnit.SelectedIndex != 0)
            {
                ST.UNIT = DDLUnit.SelectedValue.Trim().ToString();
            }           
            if (DDLColor.SelectedIndex != 0)
            {
                ST.COLOROFUNIT = DDLColor.SelectedValue.Trim().ToString();
            }           
            ST.COMP_CODE = strCompanyCode.Trim().ToString();
            if (DDLEndUse.SelectedIndex != 0)
            {
                ST.ENDUSE = DDLEndUse.SelectedValue.Trim().ToString();
            }           
            ST.LENMTR = TxtLenght.Text.Trim().ToString();
            if (DDLPly.SelectedIndex != 0)
            {
                ST.PLY = DDLPly.SelectedValue.Trim().ToString();
            }            
            if (DDLQuality.SelectedIndex != 0)
            {
                ST.QUALITY = DDLQuality.SelectedValue.Trim().ToString();
            }            
            ST.TEXSIZE=TxtTexSize.Text.Trim().ToString();
            ST.TPI = TxtTPI.Text.Trim().ToString();
            ST.TKTNO = TxtTktNo.Text.Trim().ToString();
            if (DDLTwist.SelectedIndex != 0)
            {
                ST.TWIST = DDLTwist.SelectedValue.Trim().ToString();
            }           
            if (TxtUnitSize.Text.Trim().ToString() != string.Empty)
            {
                ST.UNITSIZE = decimal.Parse(TxtUnitSize.Text.Trim().ToString());
            }            
            if (TxtUnitSize.Text.Trim().ToString() != string.Empty)
            {
                ST.UNITWT = decimal.Parse(TxtUnitWt.Text.Trim().ToString());
            }                     
            ST.TUSER=Session["urLoginId"].ToString().Trim();
            bool Res = SaitexBL.Interface.Method.ST_ARTICAL_MST.Insert_Record(ST);
            return Res;
        }
        catch
        {
            throw;
        }
    }
    private void Load_Artical_Record()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.ST_ARTICAL_MST.Load_Artical_Code (strCompanyCode,strBranchCode );
            DDLArticalCode.DataSource = dt;
            DDLArticalCode.DataTextField = "ARTICAL";
            DDLArticalCode.DataValueField = "ARTICAL_CODE";
            DDLArticalCode.DataBind();
            DDLArticalCode.Items.Insert(0, new ListItem("--------------Select--------------"));
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
            tdSave.Visible = false ;
            tdUpdate.Visible = true ;
            tdDelete.Visible = true ;
            tdFind.Visible = false;
            DDLArticalCode.Visible = true;
            TxtArticalCode.Visible = false;
            Load_Artical_Record();

        }
        catch(Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Finding Records.\\r\\nSee error log for detail."));
        }
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (TxtArticalCode.Text.Trim().ToString() != string.Empty)
            {
                bool Res = SaitexBL.Interface.Method.ST_ARTICAL_MST.Delete_Record(TxtArticalCode.Text.Trim().ToString());
                if (Res)
                {
                    Clear_Control();
                    Common.CommonFuction.ShowMessage("Record Delete Sucessfully");
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Unable to Delete");
                }
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Record Loading"));
        }
    }
    protected void DDLArticalCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DDLArticalCode.SelectedValue.Trim().ToString() != "0")
            {
                Load_Records(DDLArticalCode.SelectedValue.Trim().ToString());
            }            
        }
        catch(Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Record Loading"));
        }
    }
    private void Load_Records(string Artical_Code)
    {
        try
        {
            DataTable DTable = new DataTable();
            DTable = SaitexBL.Interface.Method.ST_ARTICAL_MST.Load_Record(Artical_Code);
            if (DTable.Rows.Count > 0)
            {
                TxtArticalCode.Text = DTable.Rows[0]["ARTICAL_CODE"].ToString().Trim();
                TxtLenght.Text = DTable.Rows[0]["LENMTR"].ToString().Trim();
                TxtTexSize.Text = DTable.Rows[0]["TEXSIZE"].ToString().Trim();
                TxtTktNo.Text = DTable.Rows[0]["TKTNO"].ToString().Trim();
                TxtTPI.Text = DTable.Rows[0]["TPI"].ToString().Trim();
                TxtUnitSize.Text = DTable.Rows[0]["UNITSIZE"].ToString().Trim();
                TxtUnitWt.Text = DTable.Rows[0]["UNITWT"].ToString().Trim();
                DDlBrand.SelectedValue = DTable.Rows[0]["BRAND"].ToString().Trim();
                DDLColor.SelectedValue = DTable.Rows[0]["COLOROFUNIT"].ToString().Trim();
                DDLEndUse.SelectedValue = DTable.Rows[0]["ENDUSE"].ToString().Trim();
                DDLPly.SelectedValue = DTable.Rows[0]["PLY"].ToString().Trim();
                DDLQuality.SelectedValue = DTable.Rows[0]["QUALITY"].ToString().Trim();
                DDLTwist.SelectedValue = DTable.Rows[0]["TWIST"].ToString().Trim();
                DDLUnit.SelectedValue = DTable.Rows[0]["UNIT"].ToString().Trim();
            }
        }
        catch
        {
            throw;
        }
    }
}
