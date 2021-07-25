using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Freedragons.Model
{
    public class ChallangeMetadataList: IEnumerable<ChallangeMetadata>
    {
        

        public List<ChallangeMetadata> MetadataOfGame;
        
        IEnumerator<ChallangeMetadata> IEnumerable<ChallangeMetadata>.GetEnumerator()
        {
            return this.MetadataOfGame.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.MetadataOfGame.GetEnumerator();
        }

        

        public List<ChallangeMetadata> generateTestElements()
        {
            MetadataOfGame = new List<ChallangeMetadata>();
            MetadataOfGame.Add(new ChallangeMetadata("FirstDragon", 48.1365155, 11.5055562, "Free the dragons here!"));
            MetadataOfGame.Add(new ChallangeMetadata("SecondDragon", 48.136, 11.5055, "Free the dragons here!\nI am a very unhappy dragon\nhelp me!"));

            return MetadataOfGame;
        }
    }
}
