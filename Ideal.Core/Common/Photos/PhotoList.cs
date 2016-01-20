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
    /// The photo list.
    /// </summary>
    public class PhotoList
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the list order.
        /// </summary>
        public int ListOrder { get; set; }

        /// <summary>
        /// Gets or sets the is active.
        /// </summary>
        public int IsActive { get; set; }
    }
}