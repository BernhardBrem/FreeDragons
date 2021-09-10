using Freedragons.Model;
using FreeDragons_Mobile.View;
using Mapsui.Geometries;
using Mapsui.UI.Forms;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FreeDragons_Mobile.Controler
{
    public class QuestExecutionControler : IGameControler
    {
        public Plugin.Geolocator.Abstractions.Position OwnPosition { get; private set; }

        private Pin OwnLocationPin;

        public QuestExecutionControler(QuestMapView questMapView)
        {
            this.QuestMapView = questMapView;
        }

        public QuestMapView QuestMapView { get; private set; }
        public ChallangeMetadata Metadata { get; private set; }
        public CQuest Quest { get; private set; }

        public Dictionary<Pin, Figure> FigureDict = new Dictionary<Pin, Figure>();

        public async Task StartControling()
        {
            await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(5), 2);
            OwnPosition = await CrossGeolocator.Current.GetPositionAsync(); ;

            OwnLocationPin = new Pin(QuestMapView)
            {
                Label = "",
                Position = new Position(OwnPosition.Latitude, OwnPosition.Longitude),
                Icon = GetOwnLocationIcon(),
                //Color = Xamarin.Forms.Color.Azure,
                Transparency = 0,
                Scale = 1,
                Type = PinType.Icon
            };

            QuestMapView.Pins.Add(OwnLocationPin);
            CrossGeolocator.Current.PositionChanged += LocationChangedEventHandler;

        }


        public void bindButtons()
        {
            /*EditorMapView.DragonButton.Clicked += AddDragonFigure;
            EditorMapView.GuardButton.Clicked += AddGuardFigure;
            EditorMapView.RebelButton.Clicked += AddRebelFigure;
            EditorMapView.OKButton.Clicked += UploadQuest;
            */
            QuestMapView.CancelButton.Clicked += CancelQuest;
        }

        public async Task EndControling()
        {
            await CrossGeolocator.Current.StopListeningAsync();
            CrossGeolocator.Current.PositionChanged -= LocationChangedEventHandler;
        }

        
        private void CancelQuest(object sender, EventArgs e)
        {
            this.QuestMapView.Pins.Clear();
        }


        private void LocationChangedEventHandler(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            OwnPosition = e.Position;
            OwnLocationPin.Position = new Position(OwnPosition.Latitude, OwnPosition.Longitude);
        }

        public void StartNewQuest(ChallangeMetadata metadata)
        {
            QuestMapView.IsVisible = true;
            Metadata = metadata;
            Quest = new CQuest()
            {
                Metadata = metadata
            };
            
        }


        private static byte[] _owniconc = null;
        private static byte[] GetOwnLocationIcon()
        {
            if (_owniconc != null) { return _owniconc; }

            Stream image = App.getRessourceStream("OwnLocation.png");
            _owniconc = image.ToBytes();
            return _owniconc;

        }

        private static byte[] ricon = null;

        private static byte[] GetRebelLocationIcon()
        {
            if (ricon != null) { return ricon; }

            Stream image = App.getRessourceStream("RebelLocation.png");
            ricon = image.ToBytes();
            return ricon;
        }

        private static byte[] gicon = null;

        private static byte[] GetGuardLocationIcon()
        {
            if (gicon != null) { return gicon; }

            Stream image = App.getRessourceStream("GuardLocation.png");
            gicon = image.ToBytes();
            return gicon;
        }

        private static byte[] dricon = null;

        private static byte[] GetDragonLocationIcon()
        {
            if (dricon != null) { return dricon; }

            Stream image = App.getRessourceStream("DragonLocation.png");
            dricon = image.ToBytes();
            return dricon;
        }

    }
}
