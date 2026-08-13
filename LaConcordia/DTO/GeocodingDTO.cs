namespace LaConcordia.DTO
{
    public class GeocodingResultDTO
    {
        public string DisplayName { get; set; } = null!;
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
    }

    public class PlacePredictionDTO
    {
        public string PlaceId { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
