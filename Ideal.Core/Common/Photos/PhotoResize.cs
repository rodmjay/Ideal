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

using Ideal.Core.Interfaces.Photos;

namespace Ideal.Core.Common.Photos
{
    #region

    

    #endregion

    /// <summary>
    /// The photo resize.
    /// </summary>
    public class PhotoResize
    {
        /// <summary>
        /// The name.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// The width.
        /// </summary>
        public readonly int Width;

        /// <summary>
        /// The height.
        /// </summary>
        public readonly int Height;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoResize"/> class.
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        public PhotoResize(IPhotoResize element)
        {
            Name = element.Name;
            Width = element.Width;
            Height = element.Height;
        }
    }
}