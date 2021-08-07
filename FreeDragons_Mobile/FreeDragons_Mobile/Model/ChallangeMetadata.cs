using Newtonsoft.Json;

namespace Freedragons.Model
{
    public class ChallangeMetadata
    {
        public ChallangeMetadata(string name, double lat, double lng, string description) {
            Name = name;
            Description = description;
            Lat = lat;
            Lng = lng;
            id = "";
            Owner = "develope";
        }

        public string PartitionKey
        {
            get
            {
                return ((int)this.Lat / 10).ToString() + "_" + ((int)this.Lng / 10).ToString();
            }
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Benennungsstile", Justification = "IDs in cosmodb have to be named 'id', this will be serialized!")]
        public string id { get; set; }
        public string Owner { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }


    }
}
