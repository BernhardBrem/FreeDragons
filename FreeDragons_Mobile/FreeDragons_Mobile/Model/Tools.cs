using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
          
namespace Freedragons.Model
{
    public class Tools
    {
        static public async Task<string> getFromServer(string path)
        {
            String url=AzureConnectionProperties.connectionURL + "API" + "/" + path;
            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(url);
            var response = await wrGETURL.GetResponseAsync();
            var stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string result = reader.ReadToEnd();
            return result;
        }

        static public async Task<string> getFromServer(string path, string payload)
        {
            String url = AzureConnectionProperties.connectionURL + "API" + "/" + path;
            var uri = new System.Uri(url);
            string data = "";
            using (var client = new System.Net.WebClient())
            {
                data = await client.UploadStringTaskAsync(url, "GET", payload);
                Debug.WriteLine("Recieved " + data);
            }
        
            return data;
        }

        internal static async Task putToServer(string path, String payload)
        {
            String url = AzureConnectionProperties.connectionURL + "API" + "/" + path;
            var uri = new System.Uri(url);
            using (var client = new System.Net.WebClient())
            {
                var data = await client.UploadStringTaskAsync(url, "PUT", payload);
                Debug.WriteLine("Recieved " + data);
            }
        }
    }
}
