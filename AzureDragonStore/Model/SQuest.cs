using Freedragons.Model;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AzureDragonStore.Model
{
    public class SQuest:Quest
    {

        public String PartitionKey
        {
            get
            {
                return ((int)this.Metadata.Lat / 10).ToString() + "_" + ((int)this.Metadata.Lng / 10).ToString();
            }
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        public async Task<string> PublishToServer()
        {
            string result = "";
            //Calculate DB ID of metadata
            if (this.id == "")
            {
                id = this.Metadata.Name + "_" + this.Metadata.Lng.ToString() + "_" + this.Metadata.Lat.ToString() + "_" + DateTime.Now.ToString().Replace("/", "_").Replace(" ", "_");
                this.Metadata.id = id;
            }
            // Connect to DB
            var mcontainer = await DragonCosmosDBHandler.getMetadataContainer();
            var qcontainer = await DragonCosmosDBHandler.getQuestsContainer();


            try
            {
                // Read the item to see if it exists.  
                ItemResponse<ChallangeMetadata> r = await mcontainer.ReadItemAsync<ChallangeMetadata>(this.Metadata.id, new PartitionKey(this.Metadata.PartitionKey));
                result += "Can not generate "+ this.Metadata.id + "; It already exists";
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                ItemResponse<ChallangeMetadata> r = await mcontainer.CreateItemAsync<ChallangeMetadata>(this.Metadata, new PartitionKey(this.Metadata.PartitionKey));
                result += "Uploaded Metadata";
            }
            try
            {
                // Read the item to see if it exists.  
                ItemResponse<Quest> r = await qcontainer.ReadItemAsync<Quest>(this.id, new PartitionKey(this.PartitionKey));
                result += "Can not generate " + this.id + " to metadata; It already exists";
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                ItemResponse<Quest> r = await qcontainer.CreateItemAsync<Quest>(this, new PartitionKey(this.PartitionKey));
                result += "Uploaded Quest " + r.Resource.ToString();
            }


            return result;
        }
    }
}
