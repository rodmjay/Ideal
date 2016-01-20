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
    /// The photo.
    /// </summary>
    public partial class Photo
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>       
        /// todo: "Id" should only be reserved for fields with a database id.  Use 'PhotoCode' instead.
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the resize name.
        /// </summary>
        public string ResizeName { get; set; }
    }
}