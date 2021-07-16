//-----------------------------------------------------------------------
// <copyright file="FabricDesignMaster.ascx.cs" company="JIS">
//     Copyright Jingle Infosolutions Pvt. Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

/// <summary>
/// Class for Fabric Design Master Form
/// </summary>
public partial class Module_Fabric_FabricSaleWork_Controls_FabricDesignNew : System.Web.UI.UserControl
{
    List<SaitexDM.Common.DataModel.TX_FABRIC_DESIGN_SHADE> dtDesignShade;
    List<SaitexDM.Common.DataModel.TX_FABRIC_DESIGN_SHADE_TRN> dtShadeTrn;
    SaitexDM.Common.DataModel.TX_FABRIC_MST oTX_FABRIC_MST;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    string str = string.Empty;
    
    #region code to determine Refresh state of Page ie. button click or F5 hit
    private bool _refreshState; private bool _isRefresh;

    protected override void LoadViewState(object savedState)
    {
        object[] AllStates = (object[])savedState;
        base.LoadViewState(AllStates[0]);
        _refreshState = bool.Parse(AllStates[1].ToString());
        _isRefresh = _refreshState == bool.Parse(Session["__ISREFRESH"].ToString());
    }

    protected override object SaveViewState()
    {
        Session["__ISREFRESH"] = _refreshState;
        object[] AllStates = new object[2];
        AllStates[0] = base.SaveViewState();
        AllStates[1] = !(_refreshState);
        return AllStates;
    }
    #endregion

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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem is page loading.\r\nSee error log for detail."));
        }
    }

    private void InitialisePage()
    {
        try
        {
            BindFabricType();
            BindFabricGroup();
            BindUOM();
            //BindFabricProcess();
            BindEndUse();
            BindShadeCode();

            imgbtnSave.Visible = true;
            imgbtnUpdate.Visible = false;
            ddlFabricDesignMST.Visible = false;
            txtDesignCode.Visible = true;
            txtFabricCode.ReadOnly = false;
            ClearMasterDetail();
            ClearTRNDetail();
            txtDesignCode.Visible = true;
            lblMode.Text = "Save";
            ViewState["dtDesignShade"] = null;
            grdfabricShadeDetail.DataSource = null;
            grdfabricShadeDetail.DataBind();

            Image1.ImageUrl = @"~/CommonImages/ImageResizer/No_Image.jpg";            
        }
        catch
        {
            throw;
        }
    }

    private void BindShadeCode()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.OD_SHADE_FAMILY.GetShadeCodeALL();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlShadeCode.Items.Clear();
             

                ddlShadeCode.DataSource = dt;
                ddlShadeCode.DataTextField = "SHADE_CODE";
                ddlShadeCode.DataValueField = "SHADE_CODE";
                ddlShadeCode.DataBind();
                ddlShadeCode.Items.Insert(0, new ListItem("Select", ""));
            }
        }
        catch
        {
            throw;
        }
    }
    private void BindEndUse()
    {
        try
        {
            //////////////////////////// Bind Fabric End Use//////////////////////////////////////
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("END_USE", oUserLoginDetail.COMP_CODE);
            ddlEndUse.DataSource = dt;
            ddlEndUse.DataValueField = "MST_CODE";
            ddlEndUse.DataTextField = "MST_CODE";
            ddlEndUse.DataBind();
            ddlEndUse.Items.Insert(0, new ListItem("-------Select--------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }
    //private void BindFabricProcess()
    //{
    //    try
    //    {

    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}
    private void BindUOM()
    {
        try
        {
            //////////////////////////// Bind Fabric UOM//////////////////////////////////////
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("UOM", oUserLoginDetail.COMP_CODE);
            DDLUOM.DataSource = dt;
            DDLUOM.DataValueField = "MST_CODE";
            DDLUOM.DataTextField = "MST_CODE";
            DDLUOM.DataBind();
            DDLUOM.Items.Insert(0, new ListItem("-------Select--------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }
    private void BindFabricGroup()
    {
        try
        {
            //////////////////////////// Bind Fabric Group//////////////////////////////////////
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("FAB_GROUP", oUserLoginDetail.COMP_CODE);
            DDLGroup.DataSource = dt;
            DDLGroup.DataValueField = "MST_CODE";
            DDLGroup.DataTextField = "MST_DESC";
            DDLGroup.DataBind();
            DDLGroup.Items.Insert(0, new ListItem("-------Select--------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }
    private void BindFabricType()
    {
        try
        {
            //////////////////////////// Bind Fabric Type//////////////////////////////////////
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("FAB_TYPE", oUserLoginDetail.COMP_CODE);
            DDLType.DataSource = dt;
            DDLType.DataValueField = "MST_CODE";
            DDLType.DataTextField = "MST_DESC";
            DDLType.DataBind();
            DDLType.Items.Insert(0, new ListItem("---Select---", "0"));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }
    private void ClearTRNDetail()
    {
        try
        {
            ddlShadeCode.SelectedIndex = -1;
            txtShadeRGB.Text = string.Empty;
            txtRGBColor.BackColor = System.Drawing.Color.White;
            txtdocNo.Text = string.Empty;
            txtWarpCount.Text = string.Empty;
            txtWarpQuality.Text = string.Empty;
            txtNoOfWarp.Text = string.Empty;
            txtWeftCount.Text = string.Empty;
            txtWeftQuality.Text = string.Empty;
            txtNoOfWeft.Text = string.Empty;
            ddlShadeCode.Enabled = true;
            Image1.ImageUrl = @"~/CommonImages/ImageResizer/No_Image.jpg";
        }
        catch
        {
            throw;
        }
    }
    private void ClearMasterDetail()
    {
        try
        {
            txtCollection.Text = string.Empty;
            txtComposition.Text = string.Empty;
            TxtContraction.Text = string.Empty;
            txtDesignCode.Text = string.Empty;

            txtDesignRptHor.Text = string.Empty;
            txtDesignRptVer.Text = string.Empty;
            txtEnds.Text = string.Empty;
            txtEPI.Text = string.Empty;
            txtFabricBase.Text = string.Empty;
            txtFabricCode.Text = string.Empty;
            txtFabricDescription.Text = string.Empty;
            txtFabricQuality.Text = string.Empty;
            TxtGLM.Text = string.Empty;
            TxtGreyWidth.Text = string.Empty;
            txtGSM.Text = string.Empty;
            txtModification.Text = string.Empty;
            txtNoOfShade.Text = string.Empty;
            txtPicks.Text = string.Empty;
            txtPickUp.Text = string.Empty;
            txtPPI.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtSalePrice.Text = string.Empty;
            TxtShrink.Text = string.Empty;
            txtTransferPrice.Text = string.Empty;

            ddlEndUse.SelectedIndex = -1;
            DDLUOM.SelectedIndex = -1;
            ddlFinishProcess.SelectedIndex = -1;
            DDLGroup.SelectedIndex = -1;
            ddlRailRoad.SelectedIndex = -1;
            DDLType.SelectedIndex = -1;
            ddlFabricDesignMST.SelectedIndex = -1;
        }
        catch
        {
            throw;
        }
    }

    protected void lbtnSetImage_Click(object sender, EventArgs e)
    {
       //Image1.ImageUrl = @"~/CommonImages/ImageResizer/New.jpg";
        //Image1.ImageUrl = @"~/CommonImages/ImageResizer/imgNew";
        Image1.ImageUrl=  Session["Name"].ToString();  
    
    
    }

    protected void lbtnDesignImage_Click(object sender, EventArgs e)
    {
        string URL = "../../../../CommonControls/ImageGenerator.aspx";
        URL = URL + "?ImageContrilId=" + lbtnSetImage.ClientID;

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "');", true);

    }

    protected void txtShadeRGB_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int argb = 0;
            int.TryParse(txtShadeRGB.Text, out argb);
            txtRGBColor.BackColor = System.Drawing.Color.FromArgb(argb);
            txtShadeRGB.Text = argb.ToString();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem is getting Shade color.\r\nSee error log for detail."));
        }
    }

    protected void ddlShadeCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FindShadeCodeDuplicacy();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem is getting Shade Code.\r\nSee error log for detail."));
        }
    }

    private bool FindShadeCodeDuplicacy()
    {
        try
        {
            bool bResult = false;
            if (ViewState["dtDesignShade"] != null)
            {
                string ShadeCode = ddlShadeCode.SelectedValue.Trim();
                dtDesignShade = (List<SaitexDM.Common.DataModel.TX_FABRIC_DESIGN_SHADE>)ViewState["dtDesignShade"];
                
                var oVar = (from data in dtDesignShade
                            where data.SHADE_CODE == ShadeCode && data.ROW_STATUS != SaitexDM.Common.DataModel.ROWSTATE.Delete && data.ROW_STATUS != SaitexDM.Common.DataModel.ROWSTATE.EditMode
                            select data).ToList();

                if (oVar.Count > 0)
                {
                    bResult = true;
                }
                else
                {
                    bResult = false;
                }
            }
            return bResult;
        }
        catch
        {
            throw;
        }
    }

    protected void lbtnWarpDetail_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["dtShadeTrn"] == null)
            {
                dtShadeTrn = new List<SaitexDM.Common.DataModel.TX_FABRIC_DESIGN_SHADE_TRN>();
                Session["dtShadeTrn"] = dtShadeTrn;
            }

            if (ddlShadeCode.SelectedValue != string.Empty)
            {
                string URL = "FabricShadeTrn.aspx";
                URL = URL + "?SHADE_CODE=" + ddlShadeCode.SelectedValue;
                URL = URL + "&TextBoxId=" + txtNoOfWarp.ClientID;
                URL = URL + "&IsWarp=" + true;
                // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=no,menubar=no,width=850,height=320,left=200,top=300');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please select PO Number");
            }
        }
        catch
        {
        }

    }

    protected void lbtnWeftDetail_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["dtShadeTrn"] == null)
            {
                dtShadeTrn = new List<SaitexDM.Common.DataModel.TX_FABRIC_DESIGN_SHADE_TRN>();
                Session["dtShadeTrn"] = dtShadeTrn;
            }
            if (ddlShadeCode.SelectedValue != string.Empty)
            {
                string URL = "FabricShadeTrn.aspx";
                URL = URL + "?SHADE_CODE=" + ddlShadeCode.SelectedValue;
                URL = URL + "&TextBoxId=" + txtNoOfWeft.ClientID;
                URL = URL + "&IsWarp=" + false;
                // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=no,menubar=no,width=850,height=320,left=200,top=300');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=no,menubar=no,width=950,height=320,left=200,top=300');", true);
            }
            else
            {
              
                Common.CommonFuction.ShowMessage("Please select PO Number");
            }
        }
        catch
        {
        }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            str = "saveShad";
            if (ddlShadeCode.SelectedValue != string.Empty)
            {
                if (FindShadeCodeDuplicacy())
                {

                    Common.CommonFuction.ShowMessage("Process already exists. Select another Shade");
                }
                else
                {
                    SaitexDM.Common.DataModel.TX_FABRIC_DESIGN_SHADE oTX_FABRIC_DESIGN_SHADE = new SaitexDM.Common.DataModel.TX_FABRIC_DESIGN_SHADE();

                    oTX_FABRIC_DESIGN_SHADE.SHADE_CODE = ddlShadeCode.Text;
                 
                    int iCount = 0;

                    if (ViewState["dtDesignShade"] != null)
                    {
                        dtDesignShade = (List<SaitexDM.Common.DataModel.TX_FABRIC_DESIGN_SHADE>)ViewState["dtDesignShade"];
                    }

                    if (dtDesignShade == null)
                    {
                        dtDesignShade = new List<SaitexDM.Common.DataModel.TX_FABRIC_DESIGN_SHADE>();
                    }

                    var oSort = (from data in dtDesignShade
                                 where data.SHADE_CODE == oTX_FABRIC_DESIGN_SHADE.SHADE_CODE
                                 select data).ToList();

                    iCount = oSort.Count;

                    if (iCount == 0)
                    {
                        oTX_FABRIC_DESIGN_SHADE.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                        oTX_FABRIC_DESIGN_SHADE.COMP_CODE = oUserLoginDetail.COMP_CODE;
                        oTX_FABRIC_DESIGN_SHADE.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                        oTX_FABRIC_DESIGN_SHADE.FABR_CODE = txtFabricCode.Text;
                        oTX_FABRIC_DESIGN_SHADE.SHADE_RGB = txtShadeRGB.Text;
                        oTX_FABRIC_DESIGN_SHADE.DESIGN_DOC_NO = txtdocNo.Text;
                        int COUNT_WARP = 0;
                        int.TryParse(txtWarpCount.Text, out COUNT_WARP);
                        oTX_FABRIC_DESIGN_SHADE.COUNT_WARP = COUNT_WARP;
                        int COUNT_WEFT = 0;
                        int.TryParse(txtWeftCount.Text, out COUNT_WEFT);
                        oTX_FABRIC_DESIGN_SHADE.COUNT_WEFT = COUNT_WEFT;
                        oTX_FABRIC_DESIGN_SHADE.QUALITY_WARP = txtWarpQuality.Text;
                        oTX_FABRIC_DESIGN_SHADE.QUALITY_WEFT = txtWeftQuality.Text;

                        int NO_WARP = 0;
                        int.TryParse(txtNoOfWarp.Text, out NO_WARP);
                        oTX_FABRIC_DESIGN_SHADE.NO_WARP = NO_WARP;

                        int NO_WEFT = 0;
                        int.TryParse(txtNoOfWeft.Text, out NO_WEFT);
                        oTX_FABRIC_DESIGN_SHADE.NO_WEFT = NO_WEFT;

                        oTX_FABRIC_DESIGN_SHADE.POSTED_LENGTH = 0;

                        FileInfo imageInfo = new FileInfo(Server.MapPath(Image1.ImageUrl));
                        if (imageInfo.Exists)
                        {
                            //if (imageInfo.Extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase))
                            //{
                                byte[] content = new byte[imageInfo.Length];
                                FileStream imagestream = imageInfo.OpenRead();
                                imagestream.Read(content, 0, content.Length);
                                imagestream.Close();

                                oTX_FABRIC_DESIGN_SHADE.SUB_CAT_IMG = content;
                                oTX_FABRIC_DESIGN_SHADE.SUBCAT_CONTENT_TYPE = "image/pjpeg";
                                oTX_FABRIC_DESIGN_SHADE.POSTED_LENGTH = content.Length;
                            //}
                        }

                        oTX_FABRIC_DESIGN_SHADE.ROW_STATUS = SaitexDM.Common.DataModel.ROWSTATE.Insert;
                        oTX_FABRIC_DESIGN_SHADE.TDATE = DateTime.Now.Date;
                        oTX_FABRIC_DESIGN_SHADE.TUSER = oUserLoginDetail.UserCode;

                        dtDesignShade.Add(oTX_FABRIC_DESIGN_SHADE);
                    }
                    else
                    {
                        foreach (SaitexDM.Common.DataModel.TX_FABRIC_DESIGN_SHADE olist in dtDesignShade)
                        {
                            if (olist.SHADE_CODE == oTX_FABRIC_DESIGN_SHADE.SHADE_CODE)
                            {

                                olist.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                                olist.COMP_CODE = oUserLoginDetail.COMP_CODE;
                                olist.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                                olist.FABR_CODE = txtFabricCode.Text;
                                olist.SHADE_CODE = ddlShadeCode.Text;
                                olist.SHADE_RGB = txtShadeRGB.Text;
                                olist.DESIGN_DOC_NO = txtdocNo.Text;

                                int COUNT_WARP = 0;
                                int.TryParse(txtWarpCount.Text, out COUNT_WARP);
                                olist.COUNT_WARP = COUNT_WARP;

                                int COUNT_WEFT = 0;
                                int.TryParse(txtWeftCount.Text, out COUNT_WEFT);
                                olist.COUNT_WEFT = COUNT_WEFT;
                                olist.QUALITY_WARP = txtWarpQuality.Text;
                                olist.QUALITY_WEFT = txtWeftQuality.Text;

                                int NO_WARP = 0;
                                int.TryParse(txtNoOfWarp.Text, out NO_WARP);
                                olist.NO_WARP = NO_WARP;

                                int NO_WEFT = 0;
                                int.TryParse(txtNoOfWeft.Text, out NO_WEFT);
                                olist.NO_WEFT = NO_WEFT;

                                olist.POSTED_LENGTH = 0;

                                FileInfo imageInfo = new FileInfo(Server.MapPath(Image1.ImageUrl));
                                if (imageInfo.Exists)
                                {
                                    //if (imageInfo.Extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase))
                                    //{
                                        byte[] content = new byte[imageInfo.Length];
                                        FileStream imagestream = imageInfo.OpenRead();
                                        imagestream.Read(content, 0, content.Length);
                                        imagestream.Close();

                                        olist.SUB_CAT_IMG = content;
                                        olist.SUBCAT_CONTENT_TYPE = "image/pjpeg";
                                        olist.POSTED_LENGTH = content.Length;
                                    //}
                                }

                                olist.ROW_STATUS = SaitexDM.Common.DataModel.ROWSTATE.Update;
                                olist.TDATE = DateTime.Now.Date;
                                olist.TUSER = oUserLoginDetail.UserCode;

                            }
                        }
                    }

                    ViewState["dtDesignShade"] = dtDesignShade;
                   BindGridData();
                  ClearTRNDetail();
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please select Shade First");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem is Saving Shade detail.\r\nSee error log for detail."));
        }
    }

    protected void grdfabricShadeDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "trnDelete")
            {
                string SHADE_CODE = e.CommandArgument.ToString();

                if (ViewState["dtDesignShade"] != null)
                    dtDesignShade = (List<SaitexDM.Common.DataModel.TX_FABRIC_DESIGN_SHADE>)ViewState["dtDesignShade"];

                var oVar = (from data in dtDesignShade
                            where data.SHADE_CODE == SHADE_CODE
                            select data).ToList();
                if (oVar.Count > 0)
                {
                    oVar[0].ROW_STATUS = SaitexDM.Common.DataModel.ROWSTATE.Delete;
                }
                ViewState["dtDesignShade"] = dtDesignShade;
            }
            else if (e.CommandName == "trnEdit")
            {
                string SHADE_CODE = e.CommandArgument.ToString();

                if (ViewState["dtDesignShade"] != null)
                    dtDesignShade = (List<SaitexDM.Common.DataModel.TX_FABRIC_DESIGN_SHADE>)ViewState["dtDesignShade"];

                var oVar = (from data in dtDesignShade
                            where data.SHADE_CODE == SHADE_CODE
                            select data).ToList();

                if (oVar.Count > 0)
                {
                    ddlShadeCode.SelectedIndex = ddlShadeCode.Items.IndexOf(ddlShadeCode.Items.FindByValue(oVar[0].SHADE_CODE.ToString()));
                    ddlShadeCode.Enabled = false;
                    txtShadeRGB.Text = oVar[0].SHADE_RGB.ToString();
                    txtRGBColor.BackColor = System.Drawing.Color.FromArgb(int.Parse(oVar[0].SHADE_RGB));
                 
                    txtdocNo.Text = oVar[0].DESIGN_DOC_NO.ToString();
                    txtWarpCount.Text = oVar[0].COUNT_WARP.ToString();
                    txtWarpQuality.Text = oVar[0].QUALITY_WARP.ToString();
                    txtNoOfWarp.Text = oVar[0].NO_WARP.ToString();
                    txtWeftCount.Text = oVar[0].COUNT_WEFT.ToString();
                    txtWeftQuality.Text = oVar[0].QUALITY_WEFT.ToString();
                    txtNoOfWeft.Text = oVar[0].NO_WEFT.ToString();
                    oVar[0].ROW_STATUS = SaitexDM.Common.DataModel.ROWSTATE.EditMode;

                    MemoryStream ms = new MemoryStream(oVar[0].SUB_CAT_IMG);
                    System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                    string myUniqueFileName = string.Format(@"{0}.jpg", Guid.NewGuid());

                    returnImage.Save(Server.MapPath(@"~/CommonImages/ImageResizer/temp/" + myUniqueFileName));
                    Image1.ImageUrl = @"~/CommonImages/ImageResizer/temp/" + myUniqueFileName;
                    
                    //Image1.ImageUrl = Session["Name"].ToString();
                    //MemoryStream ms = new MemoryStream(oVar[0].SUB_CAT_IMG);
                    //System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                    //returnImage.Save(Server.MapPath(Image1.ImageUrl));



                    //MemoryStream ms = new MemoryStream(oVar[0].SUB_CAT_IMG);
                    //System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                    //returnImage.Save(Server.MapPath(@"~/CommonImages/ImageResizer/New.jpg"));
                    //Image1.ImageUrl = @"~/CommonImages/ImageResizer/New.jpg";

                    //Session["content"] = oVar[0].SUB_CAT_IMG;
                    //Image1.ImageUrl = "~/Module/Admin/ShowImage.aspx?contentType=" + oVar[0].SUBCAT_CONTENT_TYPE + "&contentLength=" + oVar[0].POSTED_LENGTH;

                    ViewState["SHADE_CODE"] = SHADE_CODE;
                }
            }
            BindGridData();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Deleting Process.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void BindGridData()
    {
        try
        {

            //DirectoryInfo dir = new DirectoryInfo(Server.MapPath(@"~/CommonImages/ImageResizer/"));
            //if (dir.Exists)
            //{
            //    FileInfo[] files = dir.GetFiles();

            //    foreach (FileInfo file in files)
            //    {
            //        file.Delete();
            //    }
            //}

            dtDesignShade = (List<SaitexDM.Common.DataModel.TX_FABRIC_DESIGN_SHADE>)ViewState["dtDesignShade"];

            if (dtDesignShade != null && dtDesignShade.Count > 0)
            {

                var oVar = (from data in dtDesignShade
                            where data.ROW_STATUS != SaitexDM.Common.DataModel.ROWSTATE.Delete
                            select data).ToList();

                grdfabricShadeDetail.DataSource = oVar;
                grdfabricShadeDetail.DataBind();

                //txtdocNo.Text = oVar.Count.ToString();
            }
            else
            {
                Common.CommonFuction.ShowMessage("Shade Detail Not Found .");
            }
        }
        catch
        {
            throw;
        }
    }

    protected void grdfabricShadeDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Image imgDesignImage = (Image)e.Row.FindControl("imgDesignImage");
                
                // Code to set RGB color In Grid
                LinkButton lbtnShadeRGBTrn = (LinkButton)e.Row.FindControl("lbtnShadeRGBTrn");
                TextBox txtShadeRGBColorTrn = (TextBox)e.Row.FindControl("txtShadeRGBColorTrn");

                int argb = 0;
                int.TryParse(lbtnShadeRGBTrn.Text, out argb);
                txtShadeRGBColorTrn.BackColor = System.Drawing.Color.FromArgb(argb);
                lbtnShadeRGBTrn.Text = argb.ToString();

                string ShadeCode = lbtnShadeRGBTrn.ToolTip.Trim();
                dtDesignShade = (List<SaitexDM.Common.DataModel.TX_FABRIC_DESIGN_SHADE>)ViewState["dtDesignShade"];


                //for displaying Image
                var oVar = (from data in dtDesignShade
                            where data.SHADE_CODE == ShadeCode && data.SUB_CAT_IMG != null && data.ROW_STATUS != SaitexDM.Common.DataModel.ROWSTATE.Delete
                            select data).ToList();

                imgDesignImage.ImageUrl = @"~/CommonImages/ImageResizer/No_Image.jpg";

                if (oVar.Count > 0)
                {

                    MemoryStream ms = new MemoryStream(oVar[0].SUB_CAT_IMG);
                    System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                    string myUniqueFileName = string.Format(@"{0}.jpg", Guid.NewGuid());

                    returnImage.Save(Server.MapPath(@"~/CommonImages/ImageResizer/temp/" + myUniqueFileName));
                    imgDesignImage.ImageUrl = @"~/CommonImages/ImageResizer/temp/" + myUniqueFileName;
                }

                // For Warp and weft entry display
                GridView grdWarptrn = (GridView)e.Row.FindControl("grdWarptrn");
                GridView grdWefttrn = (GridView)e.Row.FindControl("grdWefttrn");
                dtShadeTrn = (List<SaitexDM.Common.DataModel.TX_FABRIC_DESIGN_SHADE_TRN>)Session["dtShadeTrn"];

                if (dtShadeTrn != null && dtShadeTrn.Count > 0)
                {
                    var oVarWarp = (from data in dtShadeTrn
                                    where data.SHADE_CODE == ShadeCode && data.ROW_STATUS != SaitexDM.Common.DataModel.ROWSTATE.Delete && data.WARP_WEFT == "Warp"
                                    select data).ToList();

                    grdWarptrn.DataSource = oVarWarp;
                    grdWarptrn.DataBind();

                    var oVarWeft = (from data in dtShadeTrn
                                    where data.SHADE_CODE == ShadeCode && data.ROW_STATUS != SaitexDM.Common.DataModel.ROWSTATE.Delete && data.WARP_WEFT == "Weft"
                                    select data).ToList();

                    grdWefttrn.DataSource = oVarWeft;
                    grdWefttrn.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Setting Image"));
        }
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        try
        {
            ClearTRNDetail();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem is Clearing Shade Data.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //if (!string.IsNullOrEmpty(Request.Form[btnsave.UniqueID]))
            //{
                SaveData();
            //}
            //else
            //{
            //    Common.CommonFuction.ShowMessage("Please First Save Shad.");
            //}        
            
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem is Data Saving.\r\nSee error log for detail."));
        }
    }
    private void SaveData()
    {

        oTX_FABRIC_MST = new SaitexDM.Common.DataModel.TX_FABRIC_MST();
        oTX_FABRIC_MST.DESIGN_CODE = txtDesignCode.Text.Trim();
        int DESIGN_NO = 0;
        oTX_FABRIC_MST.DESIGN_NO = DESIGN_NO;
        oTX_FABRIC_MST.FABR_TYPE = DDLType.SelectedItem.ToString();
        oTX_FABRIC_MST.FABR_CODE = txtFabricCode.Text.Trim();
        oTX_FABRIC_MST.FABR_DESC = txtFabricDescription.Text.Trim();
        oTX_FABRIC_MST.FABRIC_QUALITY = txtFabricQuality.Text.Trim();
        oTX_FABRIC_MST.FABR_GROUP = DDLGroup.SelectedItem.ToString();
        double FABR_WIDTH = 0f;
        double.TryParse(TxtGreyWidth.Text.Trim(), out FABR_WIDTH);
        oTX_FABRIC_MST.FABR_WIDTH = FABR_WIDTH;
        oTX_FABRIC_MST.FABR_BASE = txtFabricBase.Text.Trim();
        oTX_FABRIC_MST.EPI = txtEPI.Text.Trim();
        oTX_FABRIC_MST.PPI = txtPPI.Text.Trim();
        oTX_FABRIC_MST.UOM = DDLUOM.SelectedItem.ToString();
        oTX_FABRIC_MST.FINISH_PROCESS = ddlFinishProcess.SelectedItem.ToString();
        bool RAILROAD = false;
        bool.TryParse(ddlRailRoad.Text.Trim(), out RAILROAD);
        oTX_FABRIC_MST.RAILROAD = RAILROAD;
        oTX_FABRIC_MST.COLLECTION_NAME = txtCollection.Text.Trim();
        oTX_FABRIC_MST.COMPOSITION = txtComposition.Text.Trim();
        int NO_SHADE = 0;
        int.TryParse(txtNoOfShade.Text.Trim(), out NO_SHADE);
        oTX_FABRIC_MST.NO_SHADE = NO_SHADE;
        double PICKUP = 0f;
        double.TryParse(txtPickUp.Text.Trim(), out PICKUP);
        oTX_FABRIC_MST.PICKUP = PICKUP;
        double SHRINKAGE = 0f;
        double.TryParse(TxtShrink.Text.Trim(), out SHRINKAGE);
        oTX_FABRIC_MST.SHRINKAGE = SHRINKAGE;
        double CONTRACTION = 0f;
        double.TryParse(TxtContraction.Text.Trim(), out CONTRACTION);
        oTX_FABRIC_MST.CONTRACTION = CONTRACTION;
        oTX_FABRIC_MST.GSM = txtGSM.Text.Trim();
        oTX_FABRIC_MST.GLM = TxtGLM.Text.Trim();
        double PICKS = 0f;
        double.TryParse(txtPicks.Text.Trim(), out PICKS);
        oTX_FABRIC_MST.PICKS = PICKS;
        oTX_FABRIC_MST.MODIFICATION = txtModification.Text.Trim();
        oTX_FABRIC_MST.DESIGN_REPEAT_HOR = txtDesignRptHor.Text.Trim();
        oTX_FABRIC_MST.DESIGN_REPEAT_VER = txtDesignRptVer.Text.Trim();
        double END = 0f;
        double.TryParse(txtEnds.Text.Trim(), out END);
        oTX_FABRIC_MST.END = END;
        oTX_FABRIC_MST.END_USE = ddlEndUse.Text.Trim();
        double SALE_PRICE = 0f;
        double.TryParse(txtSalePrice.Text.Trim(), out SALE_PRICE);
        oTX_FABRIC_MST.SALE_PRICE = SALE_PRICE;
        double TRANSFER_RATE = 0f;
        double.TryParse(txtTransferPrice.Text.Trim(), out TRANSFER_RATE);
        oTX_FABRIC_MST.TRANSFER_RATE = TRANSFER_RATE;
        oTX_FABRIC_MST.REMARKS = txtRemarks.Text.Trim();
        oTX_FABRIC_MST.TUSER = oUserLoginDetail.UserCode;

        SaitexDM.Common.DataModel.TX_FABRIC_OP_BAL oTX_FABRIC_OP_BAL = GetOpBalEntry(oTX_FABRIC_MST.FABR_CODE);
        dtDesignShade = (List<SaitexDM.Common.DataModel.TX_FABRIC_DESIGN_SHADE>)ViewState["dtDesignShade"];
        if (Session["dtShadeTrn"] != null)
        {
            dtShadeTrn = (List<SaitexDM.Common.DataModel.TX_FABRIC_DESIGN_SHADE_TRN>)Session["dtShadeTrn"];
        }
        else
        {
            dtShadeTrn = new List<SaitexDM.Common.DataModel.TX_FABRIC_DESIGN_SHADE_TRN>();
        }

        string FABR_CODE = oTX_FABRIC_MST.FABR_CODE;
        bool bResult = SaitexDL.Interface.Method.TX_FABRIC_MST.InsertFabricMaster(oTX_FABRIC_MST, oTX_FABRIC_OP_BAL, dtDesignShade, dtShadeTrn, out FABR_CODE, true);
        if (bResult)
        {
            Common.CommonFuction.ShowMessage(@"Fabric Data Saved Successfully.\r\nYour Fabric Code No is :-" + FABR_CODE);
            InitialisePage();
        }
        else
        {
            Common.CommonFuction.ShowMessage("Fabric Data saving failed.");
        }
    }
    private SaitexDM.Common.DataModel.TX_FABRIC_OP_BAL GetOpBalEntry(string FABR_CODE)
    {
        SaitexDM.Common.DataModel.TX_FABRIC_OP_BAL oTX_FABRIC_OP_BAL = new SaitexDM.Common.DataModel.TX_FABRIC_OP_BAL();
        oTX_FABRIC_OP_BAL.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
        oTX_FABRIC_OP_BAL.COMP_CODE = oUserLoginDetail.COMP_CODE;
        oTX_FABRIC_OP_BAL.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
        oTX_FABRIC_OP_BAL.PRTY_CODE = "SELF";
        oTX_FABRIC_OP_BAL.FABR_CODE = FABR_CODE;
        oTX_FABRIC_OP_BAL.OP_BAL_STOCK = 0;

        double SALE_PRICE = 0f;
        double.TryParse(txtSalePrice.Text.Trim(), out SALE_PRICE);
        oTX_FABRIC_OP_BAL.OP_RATE = SALE_PRICE;

        oTX_FABRIC_OP_BAL.STATUS = true;
        oTX_FABRIC_OP_BAL.DEL_STATUS = false;
        oTX_FABRIC_OP_BAL.TUSER = oUserLoginDetail.UserCode;
        oTX_FABRIC_OP_BAL.TDATE = DateTime.Now;
        oTX_FABRIC_OP_BAL.OPBAL_PRTY_STOK = 0;

        return oTX_FABRIC_OP_BAL;
    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
           
            imgbtnSave.Visible = false;
            imgbtnUpdate.Visible = true;
            ddlFabricDesignMST.Visible = true;
            txtDesignCode.Visible = false;
            txtFabricCode.ReadOnly = true;
            lblMode.Text = "Update";
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem is update mode.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            UpdateData();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem is Data Updating.\r\nSee error log for detail."));
        }
    }
    private void UpdateData()
    {

        oTX_FABRIC_MST = new SaitexDM.Common.DataModel.TX_FABRIC_MST();
        oTX_FABRIC_MST.DESIGN_CODE = txtDesignCode.Text.Trim();
        int DESIGN_NO = 0;
        oTX_FABRIC_MST.DESIGN_NO = DESIGN_NO;
        oTX_FABRIC_MST.FABR_TYPE = DDLType.SelectedItem.ToString();
        oTX_FABRIC_MST.FABR_CODE = txtFabricCode.Text.Trim();
        oTX_FABRIC_MST.FABR_DESC = txtFabricDescription.Text.Trim();
        oTX_FABRIC_MST.FABRIC_QUALITY = txtFabricQuality.Text.Trim();
        oTX_FABRIC_MST.FABR_GROUP = DDLGroup.SelectedItem.ToString();
        double FABR_WIDTH = 0f;
        double.TryParse(TxtGreyWidth.Text.Trim(), out FABR_WIDTH);
        oTX_FABRIC_MST.FABR_WIDTH = FABR_WIDTH;
        oTX_FABRIC_MST.FABR_BASE = txtFabricBase.Text.Trim();
        oTX_FABRIC_MST.EPI = txtEPI.Text.Trim();
        oTX_FABRIC_MST.PPI = txtPPI.Text.Trim();
        oTX_FABRIC_MST.UOM = DDLUOM.SelectedItem.ToString();
        oTX_FABRIC_MST.FINISH_PROCESS = ddlFinishProcess.SelectedItem.ToString();
        bool RAILROAD = false;
        bool.TryParse(ddlRailRoad.Text.Trim(), out RAILROAD);
        oTX_FABRIC_MST.RAILROAD = RAILROAD;
        oTX_FABRIC_MST.COLLECTION_NAME = txtCollection.Text.Trim();
        oTX_FABRIC_MST.COMPOSITION = txtComposition.Text.Trim();
        int NO_SHADE = 0;
        int.TryParse(txtNoOfShade.Text.Trim(), out NO_SHADE);
        oTX_FABRIC_MST.NO_SHADE = NO_SHADE;
        double PICKUP = 0f;
        double.TryParse(txtPickUp.Text.Trim(), out PICKUP);
        oTX_FABRIC_MST.PICKUP = PICKUP;
        double SHRINKAGE = 0f;
        double.TryParse(TxtShrink.Text.Trim(), out SHRINKAGE);
        oTX_FABRIC_MST.SHRINKAGE = SHRINKAGE;
        double CONTRACTION = 0f;
        double.TryParse(TxtContraction.Text.Trim(), out CONTRACTION);
        oTX_FABRIC_MST.CONTRACTION = CONTRACTION;
        oTX_FABRIC_MST.GSM = txtGSM.Text.Trim();
        oTX_FABRIC_MST.GLM = TxtGLM.Text.Trim();
        double PICKS = 0f;
        double.TryParse(txtPicks.Text.Trim(), out PICKS);
        oTX_FABRIC_MST.PICKS = PICKS;
        oTX_FABRIC_MST.MODIFICATION = txtModification.Text.Trim();
        oTX_FABRIC_MST.DESIGN_REPEAT_HOR = txtDesignRptHor.Text.Trim();
        oTX_FABRIC_MST.DESIGN_REPEAT_VER = txtDesignRptVer.Text.Trim();
        double END = 0f;
        double.TryParse(txtEnds.Text.Trim(), out END);
        oTX_FABRIC_MST.END = END;
        oTX_FABRIC_MST.END_USE = ddlEndUse.Text.Trim();
        double SALE_PRICE = 0f;
        double.TryParse(txtSalePrice.Text.Trim(), out SALE_PRICE);
        oTX_FABRIC_MST.SALE_PRICE = SALE_PRICE;
        double TRANSFER_RATE = 0f;
        double.TryParse(txtTransferPrice.Text.Trim(), out TRANSFER_RATE);
        oTX_FABRIC_MST.TRANSFER_RATE = TRANSFER_RATE;
        oTX_FABRIC_MST.REMARKS = txtRemarks.Text.Trim();
        oTX_FABRIC_MST.TUSER = oUserLoginDetail.UserCode;

        SaitexDM.Common.DataModel.TX_FABRIC_OP_BAL oTX_FABRIC_OP_BAL = GetOpBalEntry(oTX_FABRIC_MST.FABR_CODE);

        dtDesignShade = (List<SaitexDM.Common.DataModel.TX_FABRIC_DESIGN_SHADE>)ViewState["dtDesignShade"];
        if (Session["dtShadeTrn"] != null)
        {
            dtShadeTrn = (List<SaitexDM.Common.DataModel.TX_FABRIC_DESIGN_SHADE_TRN>)Session["dtShadeTrn"];
        }
        else
        {
            dtShadeTrn = new List<SaitexDM.Common.DataModel.TX_FABRIC_DESIGN_SHADE_TRN>();
        }

        bool bResult = SaitexDL.Interface.Method.TX_FABRIC_MST.UpdateFabricMaster(oTX_FABRIC_MST, oTX_FABRIC_OP_BAL, dtDesignShade, dtShadeTrn, false);
        if (bResult)
        {
            Common.CommonFuction.ShowMessage(@"Fabric Data Update Successfully.\r\nYour Fabric Code No is :-" + oTX_FABRIC_MST.FABR_CODE);
            InitialisePage();
           
        }
        else
        {
            Common.CommonFuction.ShowMessage("Fabric Not Update .");
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem is clear page.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "../Reports/Fabric_Mst_Opt.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
 
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem is Data Printing.\r\nSee error log for detail."));
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem is Leaving Page.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem is getting help.\r\nSee error log for detail."));
        }
    }
    protected void ddlFabricDesignMST_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
       {
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                ddlFabricDesignMST.Items.Clear();
                ddlFabricDesignMST.DataSource = data;
                ddlFabricDesignMST.DataBind();
            }
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for updation.\r\nSee error log for detail."));
        }
    }
    protected DataTable GetItems(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT FABR_CODE, FABR_DESC, FABR_TYPE, DESIGN_CODE FROM TX_FABRIC_MST WHERE FABR_CODE LIKE :SearchQuery OR FABR_DESC LIKE :SearchQuery OR FABR_TYPE LIKE :SearchQuery OR DESIGN_CODE LIKE :SearchQuery ORDER BY FABR_CODE) asd WHERE ROWNUM <= 200";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += " and FABR_CODE not in( SELECT FABR_CODE FROM (SELECT FABR_CODE, FABR_DESC, FABR_TYPE, DESIGN_CODE FROM TX_FABRIC_MST WHERE FABR_CODE LIKE :SearchQuery OR FABR_DESC LIKE :SearchQuery OR FABR_TYPE LIKE :SearchQuery OR DESIGN_CODE LIKE :SearchQuery ORDER BY FABR_CODE) asd WHERE ROWNUM <= '" + startOffset + "')";
            }

            string SortExpression = " ORDER BY FABR_CODE";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }
    protected int GetItemsCount(string text)
    {
        string CommandText = " SELECT * FROM (SELECT FABR_CODE, FABR_DESC, FABR_TYPE, DESIGN_CODE FROM TX_FABRIC_MST WHERE FABR_CODE LIKE :SearchQuery OR FABR_DESC LIKE :SearchQuery OR FABR_TYPE LIKE :SearchQuery OR DESIGN_CODE LIKE :SearchQuery ORDER BY FABR_CODE) asd ";
        string WhereClause = " ";
        string SortExpression = " ORDER BY FABR_CODE ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        return data.Rows.Count;
    }
    protected void ddlFabricDesignMST_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            string FabrCode = ddlFabricDesignMST.SelectedValue.Trim();
            FillDataByCode(FabrCode);
            BindGridData();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void FillDataByCode(string FABR_CODE)
    {
        try
        {
            SaitexDM.Common.DataModel.TX_FABRIC_MST oTX_FABRIC_MST = SaitexDL.Interface.Method.TX_FABRIC_MST.GetFabricMstDataByCode(FABR_CODE);
            txtCollection.Text = oTX_FABRIC_MST.COLLECTION_NAME;
            txtComposition.Text = oTX_FABRIC_MST.COMPOSITION;
            TxtContraction.Text = oTX_FABRIC_MST.CONTRACTION.ToString();
            txtDesignCode.Text = oTX_FABRIC_MST.DESIGN_CODE;
            txtDesignRptHor.Text = oTX_FABRIC_MST.DESIGN_REPEAT_HOR;
            txtDesignRptVer.Text = oTX_FABRIC_MST.DESIGN_REPEAT_VER;
            txtEnds.Text = oTX_FABRIC_MST.END.ToString();
            txtEPI.Text = oTX_FABRIC_MST.EPI;
            txtFabricBase.Text = oTX_FABRIC_MST.FABR_BASE;
            txtFabricCode.Text = oTX_FABRIC_MST.FABR_CODE;
            txtFabricDescription.Text = oTX_FABRIC_MST.FABR_DESC;
            txtFabricQuality.Text = oTX_FABRIC_MST.FABRIC_QUALITY;
            TxtGLM.Text = oTX_FABRIC_MST.GLM;
            TxtGreyWidth.Text = oTX_FABRIC_MST.FABR_WIDTH.ToString();
            txtGSM.Text = oTX_FABRIC_MST.GSM;
            txtModification.Text = oTX_FABRIC_MST.MODIFICATION;
            txtNoOfShade.Text = oTX_FABRIC_MST.NO_SHADE.ToString();
            txtPicks.Text = oTX_FABRIC_MST.PICKS.ToString();
            txtPickUp.Text = oTX_FABRIC_MST.PICKUP.ToString();
            txtPPI.Text = oTX_FABRIC_MST.PPI;
            txtRemarks.Text = oTX_FABRIC_MST.REMARKS;
            txtSalePrice.Text = oTX_FABRIC_MST.SALE_PRICE.ToString();
            TxtShrink.Text = oTX_FABRIC_MST.SHRINKAGE.ToString();
            txtTransferPrice.Text = oTX_FABRIC_MST.TRANSFER_RATE.ToString();

            ddlEndUse.SelectedIndex = ddlEndUse.Items.IndexOf(ddlEndUse.Items.FindByValue(oTX_FABRIC_MST.END_USE));
            ddlFinishProcess.SelectedIndex = ddlFinishProcess.Items.IndexOf(ddlFinishProcess.Items.FindByValue(oTX_FABRIC_MST.FINISH_PROCESS));
            DDLGroup.SelectedIndex = DDLGroup.Items.IndexOf(DDLGroup.Items.FindByValue(oTX_FABRIC_MST.FABR_GROUP));
            ddlRailRoad.SelectedIndex = ddlRailRoad.Items.IndexOf(ddlRailRoad.Items.FindByValue(oTX_FABRIC_MST.RAILROAD.ToString()));
            DDLType.SelectedIndex = DDLType.Items.IndexOf(DDLType.Items.FindByValue(oTX_FABRIC_MST.FABR_TYPE));
            DDLUOM.SelectedIndex = DDLUOM.Items.IndexOf(DDLUOM.Items.FindByValue(oTX_FABRIC_MST.UOM));

            dtDesignShade = SaitexDL.Interface.Method.TX_FABRIC_MST.GetFabShadeDataByCode(FABR_CODE);
            dtShadeTrn = SaitexDL.Interface.Method.TX_FABRIC_MST.GetFabShadeTrnDataByCode(FABR_CODE);

            ViewState["dtDesignShade"] = dtDesignShade;
            Session["dtShadeTrn"] = dtShadeTrn;
        }
        catch
        {
            throw;
        }
    }
    protected void DDLType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}

