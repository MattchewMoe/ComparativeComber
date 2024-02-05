// GeoLocationService.cs
using System;
using System.Threading.Tasks;
using Azure.Core.GeoJson;
using BingMapsRESTToolkit;
using Microsoft.Extensions.Logging;

namespace ComparativeComber.GeoServices
{
    public class GeoLocationService
    {
        private readonly ILogger<GeoLocationService> _logger;
        private readonly string _bingMapsApiKey;
        private string bingMapsApiKey;
        private const double EarthRadiusKm = 6371.0;
        private const double KmToMilesConversionFactor = 0.621371;


        public GeoLocationService(string bingMapsApiKey)
        {
            _bingMapsApiKey = bingMapsApiKey ?? throw new ArgumentNullException(nameof(bingMapsApiKey));
        }
        // Method for geocoding an address to lat/lng
        public async Task<Location> GeocodeAsync(string address)
        {
            var request = new GeocodeRequest()
            {
                Query = address, // Use Query instead of Address for a freeform address
                BingMapsKey = _bingMapsApiKey
            };

            var response = await ServiceManager.GetResponseAsync(request);

            if (response != null && response.ResourceSets != null && response.ResourceSets.Length > 0 &&
                response.ResourceSets[0].Resources != null && response.ResourceSets[0].Resources.Length > 0)
            {
                return (response.ResourceSets[0].Resources[0] as Location);
            }

            return null;
        }

        public double CalculateDistanceMiles(
            double lat1, double lon1,
            double lat2, double lon2)
        {
            var distanceKm = CalculateDistanceKm(lat1, lon1, lat2, lon2);
            return distanceKm * KmToMilesConversionFactor;
        }

        private double CalculateDistanceKm(
            double lat1, double lon1,
            double lat2, double lon2)
        {
            var dLat = (lat2 - lat1) * (Math.PI / 180.0);
            var dLon = (lon2 - lon1) * (Math.PI / 180.0);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(lat1 * (Math.PI / 180.0)) * Math.Cos(lat2 * (Math.PI / 180.0)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
            return EarthRadiusKm * c;
        }


        public async Task<(double MinLat, double MaxLat, double MinLng, double MaxLng)> GetBoundingBoxAsync(double latitude, double longitude, double distance)
        {
            var earthRadiusKm = 6371;
            var latChange = distance / earthRadiusKm;
            var lngChange = distance / (earthRadiusKm * Math.Cos(Math.PI * latitude / 180));

            var minLat = latitude - latChange * (180 / Math.PI);
            var maxLat = latitude + latChange * (180 / Math.PI);
            var minLng = longitude - lngChange * (180 / Math.PI);
            var maxLng = longitude + lngChange * (180 / Math.PI);

            return (minLat, maxLat, minLng, maxLng);
        }

        public Task<bool> IsPointInBoundingBoxAsync(double latitude, double longitude, double minLat, double maxLat, double minLng, double maxLng)
        {
            var isInside = latitude >= minLat && latitude <= maxLat && longitude >= minLng && longitude <= maxLng;
            return Task.FromResult(isInside);
        }
    }
}
