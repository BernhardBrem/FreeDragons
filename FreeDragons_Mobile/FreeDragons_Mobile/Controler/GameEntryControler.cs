using Freedragons.Model;
using FreeDragons_Mobile.View;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace FreeDragons_Mobile.Controler
{
    public class QuestListItem
    {
        public ChallangeMetadata metaData;

        public string DisplayItem { get; private set; }

        public QuestListItem(ChallangeMetadata md)
        {
            this.metaData = md;
            this.DisplayItem = md.Name;
        }
    }
    public class GameEntryControler : IGameControler
    {
        

        public ObservableCollection<QuestListItem> ReachableQuests { get { return TheReachableQuests; } }
        private GameEntryView gameEntryView;
        private Position OwnPosition;
        private ObservableCollection<QuestListItem> TheReachableQuests;
        private ChallangeMetadataList metadataList = null;

        public QuestListItem NotReachableEntry { get; }

        public GameEntryControler(GameEntryView gameEntryView)
        {
            this.gameEntryView = gameEntryView;
            this.TheReachableQuests = new ObservableCollection<QuestListItem>();
            this.NotReachableEntry = new QuestListItem (new ChallangeMetadata("No quests available", 0, 0, "Sorry, You have no quests in reach!"));
            this.TheReachableQuests.Add(NotReachableEntry);
            this.gameEntryView.ReachableQuestView.ItemsSource = TheReachableQuests;
            ChallangeMetadataListHandler.Getinstance().ListChangedEvent += MetadataListChanged;
        }

        private void MetadataListChanged(object sender, ListChangedEventArgs e)
        {
            this.metadataList=e.ChallangeMetadataList;
            refreshListOfReachables();
        }

        public async Task EndControling()
        {
            await CrossGeolocator.Current.StopListeningAsync();
            CrossGeolocator.Current.PositionChanged += LocationChangedEventHandler;

        }

        public async Task StartControling()
        {
            await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(60), 1000);
            OwnPosition = await CrossGeolocator.Current.GetPositionAsync();
            
            CrossGeolocator.Current.PositionChanged += LocationChangedEventHandler;


        }

        private bool isReachable(ChallangeMetadata md)
        {
            // Allow 2 km
            double limit = 2.0;
            Location OwnL = new Location(OwnPosition.Latitude, OwnPosition.Longitude);
            Location compare = new Location(md.Lat, md.Lng);
            double distance = OwnL.CalculateDistance(compare, DistanceUnits.Kilometers);
            return distance <= limit;
        }

        private void refreshListOfReachables()
        {
            if (this.OwnPosition != null && this.metadataList != null)
            {
                bool alreadyExists;
                // Add new quests
                foreach ( ChallangeMetadata metadata in this.metadataList.MetadataOfGame)
                {
                    alreadyExists = false;
                    foreach (QuestListItem qi in this.ReachableQuests)
                    {
                       if (qi.metaData.Equals(metadata))
                        {
                            alreadyExists = true;
                            break;
                        }
                    }
                    if (!alreadyExists)
                    {
                        if (isReachable(metadata))
                        {
                            this.ReachableQuests.Add(new QuestListItem(metadata));
                        }
                    
                    }
                }
                // Remove deleted quests
                List<QuestListItem> toDelete = new List<QuestListItem>();
                foreach (QuestListItem qi in this.ReachableQuests)
                {
                    alreadyExists = false;
                    foreach (ChallangeMetadata metadata in this.metadataList.MetadataOfGame)
                    {
                        if (qi.metaData.Equals(metadata) && isReachable(metadata))
                        {
                            alreadyExists = true;
                            break;
                        }
                    }
                    if (!alreadyExists)
                    {
                        toDelete.Add(qi);
                    }
                }
                foreach (QuestListItem qi in toDelete)
                {
                    this.ReachableQuests.Remove(qi);
                }
            }
        }

        private void LocationChangedEventHandler(object sender, PositionEventArgs e)
        {
            //throw new NotImplementedException();
            this.OwnPosition = e.Position;
            
            refreshListOfReachables();
            
        }
    }
}
