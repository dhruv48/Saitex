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


public partial class Module_HRMS_Pages_AppraisalForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }    
    private void Open_File(string Filename)
    {
        Response.ContentType = "Application/pdf";
        string FilePath = MapPath("~/CommonForm/"+ Filename );
        Response.WriteFile(FilePath);
        Response.End();  
    }
    //private void ViewDocument(string viewingFileNameExt)
    //{
    //    switch (viewingFileNameExt)
    //    {
    //        case ".pdf": Response.ContentType = "Application/pdf";
    //            break;
    //        case ".txt": Response.ContentType = "text/plain";
    //            break;
    //        case ".jpg": Response.ContentType = "image/JPEG";
    //            break;
    //        case ".gif": Response.ContentType = "image/GIF";
    //            break;
    //        case ".doc": Response.ContentType = "Application/msword";
    //            break;
    //        case ".xls": Response.ContentType = "Application/x-msexcel";
    //            break;
    //        case ".ppt": Response.ContentType = "application/vnd.ms-powerpoint";
    //            break;
    //        case ".pps": Response.ContentType = "application/vnd.ms-powerpoint";
    //            break;
    //        case ".pptm": Response.ContentType = "application/vnd.ms-powerpoint.presentation.macroEnabled.12";
    //            break;
    //        case ".pptx": Response.ContentType = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
    //            break;
    //        case ".xlsm": Response.ContentType = "application/vnd.ms-excel.sheet.macroEnabled.12";
    //            break;
    //        case ".xlsx": Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    //            break;
    //        case ".docm": Response.ContentType = "application/vnd.ms-word.document.macroEnabled.12";
    //            break;
    //        case ".docx": Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
    //            break;
    //    }

    //    Response.WriteFile(hostedSubFolderLocation + "\\" + hostedFileName);
    //    Response.End();
    //}

    protected void ImgBtnAPE_Click(object sender, ImageClickEventArgs e)
    {
        Open_File("Appraisal Form Executive.pdf");
    }
    protected void ImgBtnAFEC_Click(object sender, ImageClickEventArgs e)
    {
        Open_File("Appraisal Form Executive-Confirmation.pdf");
    }
    protected void ImgBtnAFS_Click(object sender, ImageClickEventArgs e)
    {
        Open_File("Appraisal Form Staff.pdf");
    }
    protected void ImgBtnAFEW_Click(object sender, ImageClickEventArgs e)
    {
        Open_File("Appraisal Form Worker.pdf");
    }
}
