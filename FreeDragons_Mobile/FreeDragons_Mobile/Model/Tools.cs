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
        static public string getFromServer(string path)
        {
            String url=AzureConnectionProperties.connectionURL + "API" + "/" + path;
            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(url);
            var response = wrGETURL.GetResponse();
            var stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string result = reader.ReadToEnd();
            return result;
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
