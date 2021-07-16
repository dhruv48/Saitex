using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing.Imaging;
using System.Collections;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;

public partial class CommonControls_ImageGenerator : System.Web.UI.Page
{
    public string _ImageContrilId;
    public static  string filename  =string.Empty  ;
    public  string img = string.Empty ;
    public string oldimg = string.Empty;
    public string newimg = string.Empty;
    public  Stream filecontent = null;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _ImageContrilId = string.Empty;
            if (Request.QueryString["ImageContrilId"] != null && Request.QueryString["ImageContrilId"] != string.Empty)
            {
                _ImageContrilId = Request.QueryString["ImageContrilId"].ToString();
            }

            BindToEnum();
        }
    }

    protected void btnPreview_Click(object sender, EventArgs e)
    {
        //fuUploader.SaveAs(Server.MapPath(@"~/CommonImages/ImageResizer/Old.jpg"));
        //imgOld.ImageUrl = @"~/CommonImages/ImageResizer/Old.jpg";
          //  fuUploader.SaveAs(Server.MapPath("~/CommonImages/ImageResizer/") + filename);
          // img = imgNew.ImageUrl;
        if (fuUploader.HasFile)
        {
           var prename = Guid.NewGuid();
        filename = Path.GetFileName(fuUploader.FileName);
           filecontent=fuUploader.FileContent;
        string fileStoragePath = Server.MapPath("../CommonImages/ImageResizer/" + prename + filename);
        fuUploader.SaveAs(Server.MapPath("../CommonImages/ImageResizer/" + prename + filename));
        imgOld.ImageUrl = @"~/CommonImages/ImageResizer/" + prename + filename;
        oldimg = imgOld.ImageUrl;  
        img = imgOld.ImageUrl;
        } 
    }

    protected void chkCrop_CheckedChanged(object sender, EventArgs e)
    {
        btnCrop.Visible = chkCrop.Checked;
        CropImage1.Visible = chkCrop.Checked;
    }

    protected void btnCrop_Click(object sender, EventArgs e)
    {

     

        //CropImage1.Crop(Server.MapPath(@"img"));
        //imgNew.ImageUrl = @"img";


        //CropImage1.Crop(Server.MapPath(@"~/CommonImages/ImageResizer/" + img));
        //img = @"~/CommonImages/ImageResizer/" + img;



        //imgOld.ImageUrl = @"~/CommonImages/ImageResizer/imgOld";
      
        //CropImage1.Crop(Server.MapPath(@"~/CommonImages/ImageResizer/New.jpg"));
        //CropImage1.Crop(Server.MapPath(@"~/CommonImages/ImageResizer/Old.jpg"));
        //imgOld.ImageUrl = @"~/CommonImages/ImageResizer/Old.jpg";
        //imgNew.ImageUrl = @"~/CommonImages/ImageResizer/New.jpg";
        var prename = Guid.NewGuid();
        CropImage1.Crop(Server.MapPath(@"../CommonImages/ImageResizer/" + prename + filename));
       
        imgNew.ImageUrl = @"~/CommonImages/ImageResizer/" + prename + filename;
        newimg = imgNew.ImageUrl;
        
        
    }




    protected void chkRotate_CheckedChanged(object sender, EventArgs e)
    {
        ddlRotate.Visible = chkRotate.Checked;
    }

    private void BindToEnum()
    {
        //  Hashtable ht = Common.CommonFuction.GetEnumForBind(typeof(System.Drawing.RotateFlipType));

        ddlRotate.DataSource = Enum.GetNames(typeof(System.Drawing.RotateFlipType));// ht;
        //ddlRotate.DataTextField = "value";
        //ddlRotate.DataValueField = "key";
        ddlRotate.DataBind();

    }

    protected void ddlRotate_SelectedIndexChanged(object sender, EventArgs e)
    {
        System.Drawing.Image orignalImage = System.Drawing.Image.FromFile(Server.MapPath(@"~/CommonImages/ImageResizer/" + filename + ""));
        //System.Drawing.Image NewImage = System.Drawing.Image.FromFile(Server.MapPath(@"~/CommonImages/ImageResizer/New.jpg"));

        var va = Enum.Parse(typeof(System.Drawing.RotateFlipType), ddlRotate.SelectedValue);
        orignalImage.RotateFlip((System.Drawing.RotateFlipType)Enum.Parse(typeof(System.Drawing.RotateFlipType), ddlRotate.SelectedValue));

        orignalImage.Save(Server.MapPath(@"~/CommonImages/ImageResizer/" + filename + ""));
        // NewImage.Save(Server.MapPath(@"CommonImages/ImageResizer/old.jpg"));

        imgOld.ImageUrl = @"~/CommonImages/ImageResizer/" + filename + "";
        imgNew.ImageUrl = @"~/CommonImages/ImageResizer/" + filename + "";
 
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    protected void btnResize_Click(object sender, EventArgs e)
    {
        int NewHeight = 0;
        int NewWidth = 0;
        int.TryParse(txtNewHeight.Text, out NewHeight);
        int.TryParse(txtNewWidth.Text, out NewWidth);

       // ImageResize.ThumbnailGenerator.Generate(Server.MapPath((@"~/CommonImages/ImageResizer/imgOld")), Server.MapPath((@"~/CommonImages/ImageResizer/imgNew")), NewWidth, NewHeight);
       ImageResize.ThumbnailGenerator.Generate(Server.MapPath((imgOld.ImageUrl)), Server.MapPath((imgNew.ImageUrl)), NewWidth, NewHeight);

        //imgOld.ImageUrl = @"~/CommonImages/ImageResizer/imgOld";
        //imgNew.ImageUrl = @"~/CommonImages/ImageResizer/imgOld";
       imgNew.ImageUrl = imgNew.ImageUrl;
        
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string imagestring;
        if (Request.QueryString["ImageContrilId"] != null && Request.QueryString["ImageContrilId"] != string.Empty)
        {
            _ImageContrilId = Request.QueryString["ImageContrilId"].ToString();
        }
        if (!string.IsNullOrEmpty(imgNew.ImageUrl))
        {
            imagestring = imgNew.ImageUrl;
            Session["Name"] = imgNew.ImageUrl;
        }
        else 
        {
            imagestring = imgOld.ImageUrl;
            Session["Name"] = imgOld.ImageUrl;
        }

        
        
        var testimageurl = imgNew.ImageUrl;
       
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:GetImageUrl('" + testimageurl + "','" + _ImageContrilId + "')", true);

    
    }

}
