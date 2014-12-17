using System;
using System.Data.Entity.Spatial;

namespace PicShare.Data.Helpers
{
    public class GeoHelperMethods
    {

        public static DbGeography GetGeoFromLatLong(double latitude, double longitude)
        {
            return DbGeography.FromText(String.Format("POINT({0} {1})", longitude, latitude));
        }

        public static double CalculateDistance(DbGeography point1, DbGeography point2)
        {
            return point1.Distance(point2) ?? 0;
        }

        public static double CalculateDistance(double point1Latitude, double point1Longitude, double point2Latitude, double point2Longitude)
        {
            var point1 = GetGeoFromLatLong(point1Latitude, point1Longitude);
            var point2 = GetGeoFromLatLong(point2Latitude, point2Longitude);

            return CalculateDistance(point1, point2);
        }
    }
}
