using Freedragons.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FreeDragons_Mobile.Controler
{
    public class ListChangedEventArgs
    {
        
        public ListChangedEventArgs(ChallangeMetadataList l)
        {
            this.ChallangeMetadataList=l;
        }

        public ChallangeMetadataList ChallangeMetadataList { get; private set; }
    }
    public class ChallangeMetadataListHandler
    {
        ChallangeMetadataListHandler()
        {
            initMetadata();
        }

        private async void initMetadata()
        {
            this.ChallangeMetadataList = await CChallangeMetadataList.GetInstance();
            RaiseListChangedEvent();
        }

        private static ChallangeMetadataListHandler instance = null;
        private ChallangeMetadataList ChallangeMetadataList;

        public static ChallangeMetadataListHandler Getinstance()
        {
            if (instance == null)
            {
                instance = new ChallangeMetadataListHandler();
            }
            return instance;
        }

        public delegate void ListChanged(object sender, ListChangedEventArgs e);

        // Declare the event.
        public event ListChanged ListChangedEvent;

        // Wrap the event in a protected virtual method
        // to enable derived classes to raise the event.
        protected virtual void RaiseListChangedEvent()
        {
            // Raise the event in a thread-safe manner using the ?. operator.
            ListChangedEvent?.Invoke(this, new ListChangedEventArgs(ChallangeMetadataList));
        }
    }
}
