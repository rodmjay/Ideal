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

using System.Configuration;

namespace Ideal.Core.Interfaces.Photos
{
    #region

    

    #endregion

    public interface IPhotoResize
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [ConfigurationProperty("name")]
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        [ConfigurationProperty("width")]
        int Width { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        [ConfigurationProperty("height")]
        int Height { get; set; }
    }
}