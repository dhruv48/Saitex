using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;

/// <summary>
/// Summary description for ImageResize
/// </summary>
public class ImageResize
{
    public ImageResize()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public class ThumbnailGenerator
    {

        /// <summary>

        /// Generates the thumbnail of given image.

        /// </summary>

        /// <param name="actualImagePath">The actual image path.</param>

        /// <param name="thumbnailPath">The thumbnail path.</param>

        /// <param name="thumbWidth">Width of the thumb.</param>

        /// <param name="thumbHeight">Height of the thumb.</param>
        public static void Generate(string actualImagePath, string thumbnailPath, int thumbWidth, int thumbHeight)
        {

            Image orignalImage = Image.FromFile(actualImagePath);

            // Rotating image 360 degrees to discart internal thumbnail image

            orignalImage.RotateFlip(RotateFlipType.Rotate180FlipNone);

            orignalImage.RotateFlip(RotateFlipType.Rotate180FlipNone);

            // Here is the basic formula to mantain aspect ratio

            // thumbHeight   imageHeight     

            // ----------- = -----------   

            // thumbWidth    imageWidth 

            //

            // Now lets assume that image width is greater and height is less and calculate the new height

            // So as per formula given above

            int newHeight = orignalImage.Height * thumbWidth / orignalImage.Width;

            int newWidth = thumbWidth;

            // New height is greater than our thumbHeight so we need to keep height fixed and calculate the width accordingly

            if (newHeight > thumbHeight)
            {

                newWidth = orignalImage.Width * thumbHeight / orignalImage.Height;

                newHeight = thumbHeight;

            }

            //Generate a thumbnail image

            Image thumbImage = orignalImage.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero);

            // Save resized picture

            var qualityEncoder = System.Drawing.Imaging.Encoder.Quality;

            var quality = (long)100; //Image Quality 

            var ratio = new EncoderParameter(qualityEncoder, quality);

            var codecParams = new EncoderParameters(1);

            codecParams.Param[0] = ratio;

            //Right now I am saving JPEG only you can choose other formats as well

            var codecInfo = GetEncoder(ImageFormat.Jpeg);

            thumbImage.Save(thumbnailPath, codecInfo, codecParams);

            // Dispose unnecessory objects

            orignalImage.Dispose();

            thumbImage.Dispose();

        }

        /// <summary>

        /// Gets the encoder for particulat image format.

        /// </summary>

        /// <param name="format">Image format</param>

        /// <returns></returns>
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {

                if (codec.FormatID == format.Guid)
                {

                    return codec;

                }

            }

            return null;

        }

    }
}
