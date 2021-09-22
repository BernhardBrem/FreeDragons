using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freedragons.Model
{
    public class CQuest:Quest
    {
        public async Task publishToServer()
        {
            string json = JsonConvert.SerializeObject(this);
            await Tools.putToServer("Quest", json);
        }

        public async static Task<Quest> getQuestFromServer(ChallangeMetadata md)
        {
            string json = JsonConvert.SerializeObject(md);
            string result = await Tools.getFromServer("Quest", json);
            return JsonConvert.DeserializeObject<Quest>(result);
                
        }

    }
}
