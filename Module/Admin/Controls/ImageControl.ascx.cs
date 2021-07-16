using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing.Imaging;

public partial class Module_Admin_Controls_ImageControl : System.Web.UI.UserControl
{
    private string _ImageContrilId;
    public string ImageContrilId
    {
        get;
        set;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            BindToEnum();
        }
    }
    protected void btnPreview_Click(object sender, EventArgs e)
    {
        fuUploader.SaveAs(Server.MapPath(@"CommonImages/ImageResizer/Old.jpg"));
        imgOld.ImageUrl = @"~/CommonImages/ImageResizer/Old.jpg";


    }
    protected void chkCrop_CheckedChanged(object sender, EventArgs e)
    {
        btnCrop.Visible = chkCrop.Checked;
        CropImage1.Visible = chkCrop.Checked;
    }

    protected void btnCrop_Click(object sender, EventArgs e)
    {
        CropImage1.Crop(Server.MapPath(@"CommonImages/ImageResizer/New.jpg"));
        CropImage1.Crop(Server.MapPath(@"CommonImages/ImageResizer/Old.jpg"));
        imgOld.ImageUrl = @"~/CommonImages/ImageResizer/Old.jpg";
        imgNew.ImageUrl = @"~/CommonImages/ImageResizer/New.jpg";
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
        System.Drawing.Image orignalImage = System.Drawing.Image.FromFile(Server.MapPath(@"~/CommonImages/ImageResizer/Old.jpg"));
        //System.Drawing.Image NewImage = System.Drawing.Image.FromFile(Server.MapPath(@"~/CommonImages/ImageResizer/New.jpg"));

        var va = Enum.Parse(typeof(System.Drawing.RotateFlipType), ddlRotate.SelectedValue);
        orignalImage.RotateFlip((System.Drawing.RotateFlipType)Enum.Parse(typeof(System.Drawing.RotateFlipType), ddlRotate.SelectedValue));

        orignalImage.Save(Server.MapPath(@"CommonImages/ImageResizer/New.jpg"));
        // NewImage.Save(Server.MapPath(@"CommonImages/ImageResizer/old.jpg"));

        imgOld.ImageUrl = @"~/CommonImages/ImageResizer/Old.jpg";
        imgNew.ImageUrl = @"~/CommonImages/ImageResizer/New.jpg";
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

        ImageResize.ThumbnailGenerator.Generate(Server.MapPath(@"CommonImages/ImageResizer/Old.jpg"), Server.MapPath(@"CommonImages/ImageResizer/New.jpg"), NewWidth, NewHeight);

        imgOld.ImageUrl = @"~/CommonImages/ImageResizer/Old.jpg";
        imgNew.ImageUrl = @"~/CommonImages/ImageResizer/New.jpg";


    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
}
