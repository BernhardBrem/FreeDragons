using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureDragonStore.Model
{
    public class DragonCosmosDBHandler
    {
        public static CosmosClient getCosmoClient()
        {
            CosmosClient cosmosClient = null;
            cosmosClient = new CosmosClient(ServerConnection.Endpointuri, ServerConnection.Key);
            return cosmosClient;
        }
        public static async Task<Database> getDatabase() { 
            DatabaseResponse databaser = await getCosmoClient().CreateDatabaseIfNotExistsAsync(ServerConnection.Database);
            return databaser.Database;
        }
        public static async Task<Container> getMetadataContainer()
        {
            var db = await getDatabase();
            ContainerResponse mcontainer = await db.CreateContainerIfNotExistsAsync("Metadata", "/PartitionKey");
            return mcontainer.Container;
        }

        public static async Task<Container> getQuestsContainer()
        {
            var db = await getDatabase();
            ContainerResponse qcontainer = await db.CreateContainerIfNotExistsAsync("Quests", "/PartitionKey");
            return qcontainer.Container;
        }

        
    }
}
