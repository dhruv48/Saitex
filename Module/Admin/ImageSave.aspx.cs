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
using System.IO;
public partial class Admin_ImageSave : System.Web.UI.Page
{
    OracleConnection con = null;
    OracleCommand cmd = null;
    OracleParameter param = null;
    OracleDataAdapter da = null;
    DataSet ds = null;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindImage();
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


            Session["saveStatus"] = 0;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            byte[] bytearr = new byte[tPhoto.PostedFile.ContentLength];
            Stream fs = tPhoto.PostedFile.InputStream;
            if (tPhoto.PostedFile.ContentLength != 0)
            {
                fs.Read(bytearr, 0, tPhoto.PostedFile.ContentLength);
            }
            ///////////////////////////////////////////////////// Getting the Maxium Id of table //////////////////////////////////////
            string strMaxId = "select nvl(max(in_ImageId),0) + 1 in_ImageId from tblImageSave";
            cmd = new OracleCommand(strMaxId, con);
            strMaxId = Convert.ToString(cmd.ExecuteOracleScalar());
            cmd.Dispose();

            string strSQL = "";
            

            strSQL = "insert into tblImageSave (in_ImageId,im_SubCatImage,in_PostedLength,im_SubCatContentType,ch_Status) values (:in_ImageId,:im_SubCatImage,:in_PostedLength,:im_SubCatContentType,:ch_Status)";
            cmd = new OracleCommand(strSQL, con);

            if (tPhoto.PostedFile.ContentLength > 0 && tPhoto.PostedFile.ContentLength < 8388609)
            {
                if (tPhoto.PostedFile.ContentType == "image/pjpeg" || tPhoto.PostedFile.ContentType == "image/gif" || tPhoto.PostedFile.ContentType == "image/bmp" || tPhoto.PostedFile.ContentType == "image/x-png")
                {

                    param = new OracleParameter(":in_ImageId", OracleType.Int32);
                    param.Direction = ParameterDirection.Input;
                    param.Value = Convert.ToInt32(strMaxId);
                    cmd.Parameters.Add(param);

                    param = new OracleParameter(":im_SubCatImage", OracleType.LongRaw);
                    param.Direction = ParameterDirection.Input;
                    param .Value = bytearr;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter(":in_PostedLength", OracleType.Int32);
                    param.Direction = ParameterDirection.Input;
                    param.Value = tPhoto.PostedFile.ContentLength;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter(":im_SubCatContentType", OracleType.VarChar, 50);
                    param.Direction = ParameterDirection.Input;
                    param.Value = tPhoto.PostedFile.ContentType;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter(":ch_Status", OracleType.Char, 1);
                    param.Direction = ParameterDirection.Input;
                    param.Value = '1';
                    cmd.Parameters.Add(param);
                }
            }

            int iRecordEffected = cmd.ExecuteNonQuery();

            if (iRecordEffected == 1)
            {
                Session["saveStatus"] = 1;
                Response.Redirect("./ImageSave.aspx?cId=S", false);
                bindImage();
            }
        
        }

        catch (OracleException ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;

        }

        catch (Exception ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
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
            if (param != null)
            {
                param = null;
            }
        
        }
    }
   

    private void bindImage()
    {
        try
        {

            con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            string strSQL = "";
            strSQL = "select in_ImageId,im_SubCatImage,in_PostedLength,im_SubCatContentType from tblImageSave";
            cmd = new OracleCommand(strSQL, con);

            ds = new DataSet();
            da = new OracleDataAdapter(cmd);
            da.Fill(ds);

            gvImage.DataSource = ds;
            gvImage.DataBind();
        }
        
        catch (OracleException ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
        }

        catch (Exception ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
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
            if (param != null)
            {
                param = null;
            }

        }

    }
    protected void gvImage_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void gvImage_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("./ImageSave.aspx", false);
    }
}
