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
using Common;
using errorLog;
public partial class Admin_ShowImage : System.Web.UI.Page
{
    OracleConnection con = null;
    OracleCommand cmd = null;
    OracleDataAdapter da = null;
    DataSet ds = null;
    string contentType; byte[] content; int contentLength;

    protected void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            // Put user code to initialize the page here

            if (!Page.IsPostBack)
            {

                //////////////////////ModuleMaster ////////////////////////////////
                if (Request.QueryString["MDL_ID"] != null)
                {
                    DataTable dt = SaitexBL.Interface.Method.CM_MODULE_MST.getModuleBookImage(Convert.ToInt16(Request.QueryString["MDL_ID"]));
                    if (dt != null && dt.Rows.Count > 0)
                    {

                        Response.ContentType = dt.Rows[0]["SUB_CAT_CONTENT_TYPE"].ToString();
                        Response.OutputStream.Write((byte[])dt.Rows[0]["SUB_CAT_IMG"], 0, Convert.ToInt32(dt.Rows[0]["POSTED_LENGTH"]));

                    }
                    //////////////////////END ModuleMaster////////////////////////////////
                }
                //////////////////////Child ModuleMaster////////////////////////////////
                if (Request.QueryString["CHILD_MDL_ID"] != null)
                {
                    DataTable dt = SaitexBL.Interface.Method.CM_CHILD_MDL_MST.getChildModuleBookImage(Convert.ToInt16(Request.QueryString["CHILD_MDL_ID"]));
                    if (dt != null && dt.Rows.Count > 0)
                    {

                        Response.ContentType = dt.Rows[0]["SUB_CAT_CONTENT_TYPE"].ToString();
                        Response.OutputStream.Write((byte[])dt.Rows[0]["SUB_CAT_IMG"], 0, Convert.ToInt32(dt.Rows[0]["POSTED_LENGTH"]));

                    }
                    //////////////////////END Child ModuleMaster////////////////////////////////
                }

                //////////////////////Navigation Master////////////////////////////////
                if (Request.QueryString["NAV_ID"] != null)
                {
                    DataTable dt = SaitexBL.Interface.Method.CM_NAV_MST.SelectNavigationByNavId(Convert.ToInt16(Request.QueryString["NAV_ID"]));
                    if (dt != null && dt.Rows.Count > 0)
                    {

                        Response.ContentType = dt.Rows[0]["SUBCAT_CONTENT_TYPE"].ToString();
                        Response.OutputStream.Write((byte[])dt.Rows[0]["SUB_CAT_IMG"], 0, Convert.ToInt32(dt.Rows[0]["POSTED_LENGTH"]));

                    }
                    //////////////////////END Navigation Master////////////////////////////////
                }

                //////////////////////Employee Master////////////////////////////////
                if (Request.QueryString["EMP_CODE"] != null)
                {
                    DataTable dt = SaitexBL.Interface.Method.HR_EMP_MST.GetEmployeeImageByEmp_COde(Request.QueryString["EMP_CODE"].Trim());
                    if (dt != null && dt.Rows.Count > 0)
                    {

                        Response.ContentType = dt.Rows[0]["SUB_CONT_TYPE"].ToString();
                        Response.OutputStream.Write((byte[])dt.Rows[0]["SUB_IMG"], 0, Convert.ToInt32(dt.Rows[0]["POSTED_LEN"]));

                    }
                    //////////////////////END Employee Master////////////////////////////////
                }

                if (Request.QueryString["contentType"] != null)
                {
                    contentType = Request.QueryString["contentType"].ToString();
                    content = (byte[])Session["content"];
                    contentLength = int.Parse(Request.QueryString["contentLength"].ToString());
                    Response.ContentType = contentType;
                    Response.OutputStream.Write(content, 0, contentLength);
                    
                    Session["content"] = null;
                }

            }
            //  }
        }
        catch (Exception ex)
        {

        }

    }



    #region Web Form Designer generated code
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: This call is required by the ASP.NET Web Form Designer.
        //
        InitializeComponent();
        base.OnInit(e);
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
    }
    #endregion

    #region Public Methods

    private void getImage(int iImageId)
    {
        try
        {

            con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            string strSQL = "";
            strSQL = "select in_ImageId,im_SubCatImage,in_PostedLength,im_SubCatContentType from tblImageSave where ltrim(rtrim(in_ImageId))='" + iImageId + "'";
            cmd = new OracleCommand(strSQL, con);

            ds = new DataSet();
            da = new OracleDataAdapter(cmd);
            da.Fill(ds);

            if (ds != null)
            {
                Response.ContentType = ds.Tables[0].Rows[0]["im_SubCatContentType"].ToString();
                Response.OutputStream.Write((byte[])ds.Tables[0].Rows[0]["im_SubCatImage"], 0, Convert.ToInt32(ds.Tables[0].Rows[0]["in_PostedLength"]));
            }
        }

        catch (OracleException ex)
        {
            Response.Write(ex.Message);
            ErrHandler.WriteError(ex.Message);
        }

        catch (Exception ex)
        {
            Response.Write(ex.Message);
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

            if (da != null)
            {
                da.Dispose();
                da = null;
            }
            if (ds != null)
            {
                ds.Dispose();
                ds = null;
            }

        }
    }

    private void getAddModuleImage(int in_AddModuleId)
    {
        try
        {

            con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            string strSQL = "";
            strSQL = "select MDL_ID,SUB_CAT_IMG,POSTED_LENGTH,SUB_CAT_CONTENT_TYPE from CM_MODULE_MST where ltrim(rtrim(MDL_ID))='" + in_AddModuleId + "'";
            cmd = new OracleCommand(strSQL, con);

            ds = new DataSet();
            da = new OracleDataAdapter(cmd);
            da.Fill(ds);

            if (ds != null)
            {
                Response.ContentType = ds.Tables[0].Rows[0]["SUB_CAT_CONTENT_TYPE"].ToString();
                Response.OutputStream.Write((byte[])ds.Tables[0].Rows[0]["SUB_CAT_IMG"], 0, Convert.ToInt32(ds.Tables[0].Rows[0]["POSTED_LENGTH"]));
            }
        }

        catch (OracleException ex)
        {
            //Response.Write(ex.Message);
            ErrHandler.WriteError(ex.Message);
        }

        catch (Exception ex)
        {
            //Response.Write(ex.Message);
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

            if (da != null)
            {
                da.Dispose();
                da = null;
            }
            if (ds != null)
            {
                ds.Dispose();
                ds = null;
            }

        }

    }

    private void getChildAddModuleImage(int in_AddChildModuleId)
    {
        try
        {

            con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            string strSQL = "";
            strSQL = "select CHILD_MDL_ID,SUB_CAT_IMG,POSTED_LENGTH,SUB_CAT_CONTENT_TYPE from CM_CHILD_MDL_MST where ltrim(rtrim(CHILD_MDL_ID))='" + in_AddChildModuleId + "'";
            cmd = new OracleCommand(strSQL, con);

            ds = new DataSet();
            da = new OracleDataAdapter(cmd);
            da.Fill(ds);

            if (ds != null)
            {
                Response.ContentType = ds.Tables[0].Rows[0]["SUB_CAT_CONTENT_TYPE"].ToString();
                Response.OutputStream.Write((byte[])ds.Tables[0].Rows[0]["SUB_CAT_IMG"], 0, Convert.ToInt32(ds.Tables[0].Rows[0]["POSTED_LENGTH"]));
            }
        }

        catch (OracleException ex)
        {
            //Response.Write(ex.Message);
            ErrHandler.WriteError(ex.Message);
        }

        catch (Exception ex)
        {
            //Response.Write(ex.Message);
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

            if (da != null)
            {
                da.Dispose();
                da = null;
            }
            if (ds != null)
            {
                ds.Dispose();
                ds = null;
            }

        }

    }

    private void getNavigationModuleImage(int iNavigationModuleImageId)
    {
        try
        {

            con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            string strSQL = "";
            strSQL = "select NAV_ID,SUB_CAT_IMG,POSTED_LENGTH,SUBCAT_CONTENT_TYPE from CM_NAV_MST where ltrim(rtrim(NAV_ID))='" + iNavigationModuleImageId + "'";
            cmd = new OracleCommand(strSQL, con);

            ds = new DataSet();
            da = new OracleDataAdapter(cmd);
            da.Fill(ds);

            if (ds != null)
            {
                Response.ContentType = ds.Tables[0].Rows[0]["SUBCAT_CONTENT_TYPE"].ToString();
                Response.OutputStream.Write((byte[])ds.Tables[0].Rows[0]["SUB_CAT_IMG"], 0, Convert.ToInt32(ds.Tables[0].Rows[0]["POSTED_LENGTH"]));
            }
        }

        catch (OracleException ex)
        {
            //Response.Write(ex.Message);
            ErrHandler.WriteError(ex.Message);
        }

        catch (Exception ex)
        {
            //Response.Write(ex.Message);
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

            if (da != null)
            {
                da.Dispose();
                da = null;
            }
            if (ds != null)
            {
                ds.Dispose();
                ds = null;
            }

        }
    }


    #endregion
}
