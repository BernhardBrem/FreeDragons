using Freedragons.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDragonStore.Model
{
    public class SChallangeMetadataList : ChallangeMetadataList
    {
        internal static ChallangeMetadataList GetInstance()
        {
            if (instance  == null)
            {
                instance = new ChallangeMetadataList();
                instance.MetadataOfGame = instance.generateTestElements();
            }
            return instance;
        }

        static ChallangeMetadataList instance { get; set; }
    }
}
