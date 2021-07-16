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

using System.IO;

public partial class Module_FileManagement_Pages_FileUploading : System.Web.UI.Page
{
    private static string File_Code;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["FILE_CODE"] != null && Request.QueryString["FILE_CODE"].ToString() != "")
        {
            File_Code = Request.QueryString["FILE_CODE"].ToString();

            DataTable dt = SaitexBL.Interface.Method.FM_FILE_UPLOAD.GetFileDetail(File_Code);
            if (dt != null && dt.Rows.Count > 0)
            {
                Response.ContentType = dt.Rows[0]["FILE_EXTENSION"].ToString();
                Response.OutputStream.Write((byte[])dt.Rows[0]["SUB_CAT_IMG"], 0, Convert.ToInt32(dt.Rows[0]["POSTED_LENGTH"]));
            }
        }
    }
}
