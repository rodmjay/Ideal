#region credits
// ***********************************************************************
// Assembly	: Ideal.Core
// Author	: Rod Johnson
// Created	: 02-24-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion
namespace Ideal.Core.Common.Photos
{
    /// <summary>
    /// The photo thumbnail.
    /// </summary>
    public class PhotoThumbnail
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the photo id.
        /// </summary>
        public int PhotoID { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the image size type value.
        /// </summary>
        public int ImageSizeTypeValue { get; set; }

        /// <summary>
        /// Gets or sets the image size type.
        /// </summary>
        public ImageSizeTypeEnum ImageSizeType
        {
            get
            {
                return (ImageSizeTypeEnum)ImageSizeTypeValue;
            }

            set
            {
                ImageSizeTypeValue = (int)value;
            }
        }
    }

    /// <summary>
    /// The image size type enum.
    /// </summary>
    public enum ImageSizeTypeEnum
    {
        /// <summary>
        /// The extra small.
        /// </summary>
        ExtraSmall = 1, 

        /// <summary>
        /// The small.
        /// </summary>
        Small = 2, 

        /// <summary>
        /// The medium.
        /// </summary>
        Medium = 3, 

        /// <summary>
        /// The large.
        /// </summary>
        Large = 4, 

        /// <summary>
        /// The extra large.
        /// </summary>
        ExtraLarge = 5
    }
}