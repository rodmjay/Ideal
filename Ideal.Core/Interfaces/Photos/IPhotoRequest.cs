#region credits
// ***********************************************************************
// Assembly	: Ideal.Core
// Author	: Rod Johnson
// Created	: 03-16-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System.IO;

namespace Ideal.Core.Interfaces.Photos
{
    #region

    

    #endregion

    public interface IPhotoRequest
    {
        /// <summary>
        /// Gets the stream.
        /// </summary>
        Stream Stream { get; }

        /// <summary>
        /// Gets the mime type.
        /// </summary>
        string MimeType { get; }

        /// <summary>
        /// Gets the photo id.
        /// </summary>
        string PhotoId { get; }
    }
}