using System.Collections.Generic;

namespace Freedragons.Model
{
    public class Quest
    {
        public ChallangeMetadata Metadata { get; set; }
        public List<Figure> initialSetup { get; set; }

        public string id { get; set; } = "";

       
    }
}
