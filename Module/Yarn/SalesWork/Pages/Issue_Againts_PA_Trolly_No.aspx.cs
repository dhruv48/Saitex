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
using Obout.ComboBox;

public partial class Module_Yarn_SalesWork_Pages_Issue_Againts_PA_Trolly_No : System.Web.UI.Page
{
    private string PA_NO = string.Empty;
    private string TRN_NO = string.Empty;
    private string TRN_TYPE = string.Empty;
    private string YARN_CODE = string.Empty;
    private string YARN_DESC = string.Empty;
    private string SHADE_CODE = string.Empty;
    private string SHADE_FAMILY = string.Empty;
   
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (Request.QueryString["PA_NO"] != null)
            {
                PA_NO = Request.QueryString["PA_NO"].Trim();
                lblPaNo.Text = PA_NO;
            }

            if (Request.QueryString["TRN_NO"] != null)
            {
                TRN_NO = Request.QueryString["TRN_NO"].Trim();
                lbTrnNo.Text = TRN_NO;
            }


            if (Request.QueryString["TRN_TYPE"] != null)
            {
                TRN_TYPE = Request.QueryString["TRN_TYPE"].Trim();
                lblTrnType.Text = TRN_TYPE;
            }

            if (Request.QueryString["YARN_CODE"] != null)
            {
                YARN_CODE = Request.QueryString["YARN_CODE"].Trim();
                lblYarnCode.Text = YARN_CODE;
            }
            if (Request.QueryString["YARN_DESC"] != null)
            {
                YARN_DESC = Request.QueryString["YARN_DESC"].Trim();
                lblYarnDesc.Text = YARN_DESC;
            }


            if (Request.QueryString["SHADE_CODE"] != null)
            {
                SHADE_CODE = Request.QueryString["SHADE_CODE"].Trim();
                lblShade.Text = SHADE_CODE;
            }

            if (Request.QueryString["SHADE_FAMILY"] != null)
            {
                SHADE_FAMILY = Request.QueryString["SHADE_FAMILY"].Trim();
                lblShadeFamily.Text = SHADE_FAMILY;
            }
           
         
            if (!IsPostBack)
            {
                initialise();
            }

            
        }

        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nsee error log for detail."));
        }
    }


    private void initialise()
    {


      
        BindBOMGrid();

    }
    private void BindTrnNo()
    {

       
    }


   





    protected void btnSubmit_Click(object sender, EventArgs e)
    {


        try
        {
           
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in submitting delivery data.\r\nsee error log for detail."));
        }
    }




    private DataTable CreateSUBTRNTable()
    {
        try
        {
            DataTable dtTrolly = new DataTable();

            dtTrolly.Columns.Add("SR_NO", typeof(int));
            dtTrolly.Columns.Add("PA_NO", typeof(string));
            dtTrolly.Columns.Add("TRN_NO", typeof(string));
            dtTrolly.Columns.Add("TRN_TYPE", typeof(string));
            dtTrolly.Columns.Add("YARN_CODE", typeof(string));
            dtTrolly.Columns.Add("YARN_DESC", typeof(string));
            dtTrolly.Columns.Add("SHADE_CODE", typeof(string));
            dtTrolly.Columns.Add("SHADE_FAMILY", typeof(string));
            dtTrolly.Columns.Add("TROLLY", typeof(string));
            dtTrolly.Columns.Add("TROLLY_WEIGHT", typeof(Double));



            return dtTrolly;
        }
        catch
        {
            throw;
        }
    }


    protected void BtnBOMSave_Click(object sender, EventArgs e)
    {
        try
        {

            DataTable dtTrolly = new DataTable();
            if (Session["dtTrolly"] != null)
            {

                dtTrolly = (DataTable)Session["dtTrolly"];
            }
            else
            {
                dtTrolly = CreateSUBTRNTable();
            }


            int SR_NO = 0;
            if (ViewState["SR_NO"] != null)
            {
                SR_NO = int.Parse(ViewState["SR_NO"].ToString());
            }


            if (SR_NO > 0)
            {
                DataView dv = new DataView(dtTrolly);
                dv.RowFilter = "PA_NO='" + lblPaNo.Text + "' and YARN_CODE='" + lblYarnCode.Text + "' and SHADE_CODE='" + lblShade.Text + "'and SHADE_FAMILY='" + lblShadeFamily.Text + "' and SR_NO=" + SR_NO + "' and TROLLY=" + txtTrollyNo.Text.ToString();
                if (dv.Count > 0)
                {
                    dv[0]["TROLLY"] = txtTrollyNo.Text.ToString();
                    dv[0]["TROLLY_WEIGHT"] = txtWeight.Text.ToString();
                    dtTrolly.AcceptChanges();
                }
            }
            else
            {



                DataRow dr = dtTrolly.NewRow();
                dr["SR_NO"] = dtTrolly.Rows.Count + 1;
                dr["YARN_CODE"] = YARN_CODE;
                dr["YARN_DESC"] = YARN_DESC;
                dr["PA_NO"] = PA_NO;
                dr["TRN_NO"] = TRN_NO;
                dr["TRN_TYPE"] = TRN_TYPE;
                dr["SHADE_CODE"] = SHADE_CODE;
                dr["SHADE_FAMILY"] = SHADE_FAMILY;
                dr["TROLLY"] = txtTrollyNo.Text.ToString();
                dr["TROLLY_WEIGHT"] = txtWeight.Text.ToString();

                dtTrolly.Rows.Add(dr);

            }
            Session["dtTrolly"] = dtTrolly;
            int m = 0;
           



            initialise();


            BindBOMGrid();



        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving  Machine Batch Detail Row.\r\nSee error log for detail."));

        }
    }





    private void BindBOMGrid()
    {
        try
        {
            DataTable dtTrolly = null;
            if (Session["dtTrolly"] != null)
            {
                dtTrolly = (DataTable)Session["dtTrolly"];
            }
            else
            {
                dtTrolly = CreateSUBTRNTable();
            }

            DataView dv = new DataView(dtTrolly);
            dv.RowFilter = " PA_NO='" + lblPaNo.Text.Trim() + "' and YARN_CODE='" + YARN_CODE.Trim() + "' and SHADE_CODE='" + lblShade.Text.Trim() + "'and SHADE_FAMILY='" + lblShadeFamily.Text.Trim() + "'and TRN_NO='" + lbTrnNo.Text.Trim() + "'and TRN_TYPE='" + lblTrnType.Text.Trim() + "' ";
            grdsub_trn.DataSource = dv;
            grdsub_trn.DataBind();

        }
        catch
        {
            throw;
        }
    }



    protected void grdSub_trnArticleDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int SR_NO = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "BOMDelete")
            {
                DeleteBOMRow(SR_NO);
                BindBOMGrid();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Sub Tran Grid RowCommand Selection.\r\nSee error log for detail."));
        }
    }



    private void DeleteBOMRow(int SR_NO)
    {
        try
        {
            DataTable dtTrolly = null;
            if (Session["dtTrolly"] != null)
            {
                dtTrolly = (DataTable)Session["dtTrolly"];

            }
            else
            {
                dtTrolly = CreateSUBTRNTable();
            }


            foreach (DataRow dr in dtTrolly.Rows)
                {
                    int iUNIQUE_ID = int.Parse(dr["SR_NO"].ToString());

                    if (iUNIQUE_ID == SR_NO)
                    {

                        dtTrolly.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtTrolly.Rows)
                {
                    iCount = iCount + 1;
                    dr["SR_NO"] = iCount;
                }

                dtTrolly.AcceptChanges();
            Session["dtTrolly"] = dtTrolly;
        }
        catch
        {
            throw;
        }
    }




    protected void BtnBOMCancel_Click(object sender, EventArgs e)
    {
        //initialise();
    }
   
    protected void ddlMachine_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        //lblMacCapacity.Text = ddlMachine.SelectedValue.ToString();
    }
}
