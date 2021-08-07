using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using AzureDragonStore.Model;
using Freedragons.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace AzureDragonStore
{
    public static class Quests
    {
        [FunctionName("QuestsOld")]
        [OpenApiOperation(operationId: "RunOld", tags: new[] { "name" })]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public static async Task<IActionResult> RunOld(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }

        [FunctionName("ListQuests")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public static async Task<IActionResult> ListQuests(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Quest")] HttpRequest req,
            ILogger log)
        {
            try { 
               log.LogInformation("C# HTTP trigger function processed a request.");
               var questlist = await SChallangeMetadataList.AsyncGetPopulatedInstance();
                var result = JsonConvert.SerializeObject(questlist);
               return new OkObjectResult(result);
            } catch(Exception e){
                return new OkObjectResult(e.Message + " " + e.StackTrace);
            }
        }

        [FunctionName("PutQuest")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public static async Task<IActionResult> PutQuests(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post","put", Route = "Quest")] HttpRequest req,
            ILogger log)
        {
            try { 
            log.LogInformation("upload quest request: " + req.Body.ToString());
            StreamReader r = new StreamReader(req.Body);
            string json = await r.ReadToEndAsync();
            SQuest q = JsonConvert.DeserializeObject<SQuest>(json);
            string result= await q.PublishToServer();
            return new OkObjectResult("Uploaded " + json + " Result of upload: " + result);
            }
            catch(Exception e)
            {
                return new OkObjectResult(e.Message + " " + e.StackTrace);
            }
        }

    }
}

