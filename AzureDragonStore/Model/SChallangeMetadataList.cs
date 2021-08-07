using Freedragons.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace AzureDragonStore.Model
{
    public class SChallangeMetadataList : ChallangeMetadataList
    {
        internal static ChallangeMetadataList GetInstance()
        {
            if (instance  == null)
            {
                instance = new SChallangeMetadataList();
                //instance.MetadataOfGame = instance.generateTestElements();
            }
            return instance;
        }

        public static async Task<ChallangeMetadataList> AsyncGetPopulatedInstance()
        {
            var instane = GetInstance();
            if (instance.MetadataOfGame == null){
                List<ChallangeMetadata> metadata = await AsyncPopulateMetadataList();
                instance.MetadataOfGame = metadata;
            }
            return instance;
            
        } 

        public async static Task<List<ChallangeMetadata>> AsyncPopulateMetadataList()
        {
            var mcontainer = await DragonCosmosDBHandler.getMetadataContainer();
            var q = mcontainer.GetItemQueryIterator<ChallangeMetadata>();
            var ar = await q.ReadNextAsync();
            List<ChallangeMetadata> l = new List<ChallangeMetadata>();
            foreach (var item in ar)
            {
                l.Add(item);
            }
            
            return l;
        }


        static ChallangeMetadataList instance { get; set; }
    }
}
