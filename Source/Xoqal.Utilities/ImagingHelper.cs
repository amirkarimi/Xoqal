#region License
// ImagingHelper.cs
// 
// Copyright (c) 2012 Xoqal.com
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

namespace Xoqal.Utilities
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;

    /// <summary>
    /// Helps simple image processing.
    /// </summary>
    public class ImagingHelper
    {
        /// <summary>
        /// Gets the custom thumbnail image.
        /// </summary>
        /// <param name="imageBytes"> The image. </param>
        /// <param name="width"> The width. </param>
        /// <param name="height"> The height. </param>
        /// <param name="shrinkOnly"> The value indicating whether do not make larger images if the given width and height are bigger than original image. </param>
        /// <param name="stretch"> The value indicating whether stretch the image to fit the given width and height or not. </param>
        /// <param name="quality"> The quality of the encoder. </param>
        /// <returns> </returns>
        public static byte[] GetCustomThumbnailImage(
            byte[] imageBytes, int? width = null, int? height = null, bool shrinkOnly = true, bool stretch = false, long quality = 80L)
        {
            if (width == null && height == null)
            {
                throw new ArgumentNullException(string.Empty, "Both of the width and height parameters could not be null");
            }

            if (imageBytes == null || imageBytes.Length == 0)
            {
                throw new ArgumentNullException("imageBytes", "imageBytes could not be null or empty.");
            }

            Image image = Image.FromStream(new MemoryStream(imageBytes));

            // Check shring-only requests
            if ((width == null || width.Value >= image.Width) && (height == null || height.Value >= image.Height) && shrinkOnly)
            {
                // Requested size is bigger than original image so if this is a shrink-only request then we return the original image
                return imageBytes;
            }

            int w;
            int h;

            if (stretch)
            {
                w = width ?? height.Value;
                h = height ?? width.Value;
            }
            else
            {
                CalculateFormalSize(width, height, image, out w, out h);
            }

            // Create thumbnail
            Image resizedImage = ResizeImage(image, w, h);

            EncoderParameters encoderParams;
            ImageCodecInfo jpegEncoder;
            GetEncoder(out encoderParams, out jpegEncoder, quality);

            // Send image 
            var memoryStream = new MemoryStream();
            resizedImage.Save(memoryStream, jpegEncoder, encoderParams);
            return memoryStream.ToArray();
        }

        /// <summary>
        /// Calculates the destination size in a formal manner (not stretched).
        /// </summary>
        /// <param name="width"> </param>
        /// <param name="height"> </param>
        /// <param name="image"> </param>
        /// <param name="w"> </param>
        /// <param name="h"> </param>
        private static void CalculateFormalSize(int? width, int? height, Image image, out int w, out int h)
        {
            // Cacluate the prerequisite
            bool isVerticalLimit;
            if (height == null || width == null)
            {
                isVerticalLimit = width == null;
            }
            else
            {
                double destinationVerticality = width.Value / (double)height.Value;
                double sourceVerticality = image.Width / (double)image.Height;

                isVerticalLimit = sourceVerticality < destinationVerticality;
            }

            // Cacluate the final image size
            if (isVerticalLimit)
            {
                // Limits on vertical
                h = height.Value;
                w = (height.Value * image.Width) / image.Height;
            }
            else
            {
                // Limits on horizontal
                w = width.Value;
                h = (width.Value * image.Height) / image.Width;
            }
        }

        /// <summary>
        /// Resizes the specified image to the specified width and height.
        /// </summary>
        /// <param name="image"> </param>
        /// <param name="width"> </param>
        /// <param name="height"> </param>
        /// <returns> </returns>
        private static Image ResizeImage(Image image, int width, int height)
        {
            int srcWidth = image.Width;
            int srcHeight = image.Height;

            var bmp = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(bmp);

            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            var rectDestination = new Rectangle(0, 0, width, height);
            graphics.DrawImage(image, rectDestination, 0, 0, srcWidth, srcHeight, GraphicsUnit.Pixel);
            return bmp;
        }

        /// <summary>
        /// Gets the JPEG encoder according to the specified quality.
        /// </summary>
        /// <param name="encoderParams"> </param>
        /// <param name="jpegEncoder"> </param>
        /// <param name="quality"> </param>
        private static void GetEncoder(out EncoderParameters encoderParams, out ImageCodecInfo jpegEncoder, long quality)
        {
            ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
            jpegEncoder = null;
            for (int x = 0; x < encoders.Length; x++)
            {
                if (string.Compare(encoders[x].MimeType, "image/jpeg", true) == 0)
                {
                    jpegEncoder = encoders[x];
                    break;
                }
            }

            if (jpegEncoder == null)
            {
                throw new ApplicationException("Could not find JPEG encoder!");
            }

            encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, quality);
        }
    }
}
