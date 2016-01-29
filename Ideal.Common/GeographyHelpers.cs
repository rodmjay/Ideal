#region credits
// ***********************************************************************
// Assembly	: Ideal.Core
// Author	: Rod Johnson
// Created	: 03-07-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System.Data.Entity.Spatial;
using System.Globalization;

namespace Ideal
{
    #region

    

    #endregion

    public static class GeographyHelpers
    {
        public static DbGeography CreatePoint(double latitude, double longitude)
        {
            var text = string.Format(CultureInfo.InvariantCulture.NumberFormat,
                                     "POINT({0} {1})", longitude, latitude);
            // 4326 is most common coordinate system used by GPS/Maps
            return DbGeography.FromText(text, 4326);
        }
    }
}
