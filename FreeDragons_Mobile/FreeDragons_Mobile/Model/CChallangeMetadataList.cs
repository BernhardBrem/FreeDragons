using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Freedragons.Model
{
    public class CChallangeMetadataList:ChallangeMetadataList
    {
        public CChallangeMetadataList()
        {
            //generateTestElements();
            MetadataOfGame = getListFromServer();
        }


        static readonly ChallangeMetadataList instance = new CChallangeMetadataList();
        public static ChallangeMetadataList GetInstance() { return instance; }
        List<ChallangeMetadata> getListFromServer()
        {
            string url = "Quest";
            string json = Tools.getFromServer(url);
            List<ChallangeMetadata> result = JsonConvert.DeserializeObject<List<ChallangeMetadata>>(json);
            return result;
        }
    }
}
