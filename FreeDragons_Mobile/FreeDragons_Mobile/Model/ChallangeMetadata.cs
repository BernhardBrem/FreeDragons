namespace Freedragons.Model
{
    public class ChallangeMetadata
    {
        public ChallangeMetadata(string name, double lat, double lng, string description) {
            Name = name;
            Description = description;
            Lat = lat;
            Lng = lng;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }

    }
}
