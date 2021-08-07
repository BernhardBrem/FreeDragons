using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freedragons.Model
{
    public class CChallangeMetadataList:ChallangeMetadataList
    {
        public CChallangeMetadataList()
        {
            //generateTestElements();
           
        }


        static readonly ChallangeMetadataList instance = new CChallangeMetadataList();
        public static async Task<ChallangeMetadataList> GetInstance() {
            var metadata = await getList();
            return instance; 
        }

        static async Task<List<ChallangeMetadata>> getList()
        {
            if (instance.MetadataOfGame == null)
            {
                instance.MetadataOfGame = await getListFromServer();
            }
            return instance.MetadataOfGame;
        }

        static async Task<List<ChallangeMetadata>> getListFromServer()
        {
            string url = "Quest";
            string json = await Tools.getFromServer(url);
            List<ChallangeMetadata> result = JsonConvert.DeserializeObject<List<ChallangeMetadata>>(json);
            return result;
        }
    }
}
