using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing.Drawing2D;

namespace SOS.Lib.Util
{
	public class ImageUtility
	{
		public static void SaveImage(string mimeType, Image imgToSave, Stream outputStream, long qualityFactor)
		{
			ImageCodecInfo codec = GetImageCodec(mimeType);

			if (codec != null)
			{
				// Set the quality factor
				var encodeParams = new EncoderParameters(1);
				encodeParams.Param[0] = new EncoderParameter(Encoder.Quality, qualityFactor);

				imgToSave.Save(outputStream, codec, encodeParams);
			}
			else
			{
				imgToSave.Save(outputStream, ImageFormat.Jpeg);
			}
		}

		public static ImageCodecInfo GetImageCodec(string mimeType)
		{
			ImageCodecInfo codec = null;
			foreach (ImageCodecInfo curr in ImageCodecInfo.GetImageEncoders())
			{
				if (curr.MimeType == mimeType)
				{
					codec = curr;
					break;
				}
			}
			return codec;
		}

        /// <summary>
        /// CropAndResize will resize and crop the image to fit the width and height and match the aspect ratio
        /// </summary>
        public static Image CropAndResize(Image image, int maxWidth, int maxHeight, float aspect)
        {
            Image newImage = null;

            // Get the aspect ratio and crop the image to match
            float ratio = (float)image.Width / (float)image.Height;
            if (ratio > aspect)
            {
                // Image is too wide, let's crop it and take a center out of the middle
                int width = (int)((float)image.Height * aspect);
                int offset = (image.Width - width) / 2;
                if (offset < 0) offset = 0;

                newImage = CropImage(image, new Rectangle(offset, 0, width, image.Height));
            }
            else if (ratio < aspect)
            {
                // Image is too tall, just cut the bottom off -- Andres TODO: Crop center as above
                int height = (int)((float)image.Width * (1.0 / aspect));
            	int nHOffset = (image.Height - height)/2;
				newImage = CropImage(image, new Rectangle(0, nHOffset, image.Width, height));
            }
            else
            {
                newImage = image;
            }

            //
            // Resize the image
            //
            newImage = ResizeImage(newImage, new Size(maxWidth, maxHeight));
            return newImage;
        }

        /// <summary>
        /// CropImage will crop an image and return a new image
        /// </summary>
        private static Image CropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            return (Image)(bmpCrop);
        }

        /// <summary>
        /// ResizeImage will resize the source image returning a new image
        /// </summary>
        private static Image ResizeImage(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            //
            // NOTE: I have removed this logic below because I was trying to maintain aspect ratio and the
            //       calculation was resulting in a scaled size 1px less for width and height based on the 
            //       floating point math.  Since the imgToResize is cropped and has the aspect ratio, then
            //       this logic isn't needed.  Beware, that if your source image aspect ratio doesn't match
            //       the destination image aspect ratio, you will get a skewed (squashed) image.
            //
            //float nPercent = 0;
            //float nPercentW = 0;
            //float nPercentH = 0;

            //nPercentW = ((float)size.Width / (float)sourceWidth);
            //nPercentH = ((float)size.Height / (float)sourceHeight);

            //if (nPercentH < nPercentW)
            //    nPercent = nPercentH;
            //else
            //    nPercent = nPercentW;

            //int destWidth = (int)(sourceWidth * nPercent);
            //int destHeight = (int)(sourceHeight * nPercent);

            int destWidth = size.Width;
            int destHeight = size.Height;

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }
	}
}