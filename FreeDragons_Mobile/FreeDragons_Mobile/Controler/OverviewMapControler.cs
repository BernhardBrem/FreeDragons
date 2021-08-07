using Freedragons.Model;
using FreeDragons_Mobile.View;
using Mapsui.Projection;
using Mapsui.UI.Forms;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FreeDragons_Mobile.Controler
{
    public class OverviewMapControler : IGameControler
    {
        public  OverviewMapControler(DragonOverviewMapView overviewMapView,  MessageView mView)
        {
            OverviewMapView = overviewMapView;
            MView = mView;
            PinsToData = new Dictionary<Pin, ChallangeMetadata>();
            OverviewMapView.PinClicked += MapView_PinClicked;
            SetMetadataPins();
        }

        public DragonOverviewMapView OverviewMapView { get; private set; }
        

        Dictionary<Pin, ChallangeMetadata> PinsToData { get; set; }

        MessageView MView { get; set; }

        public async Task EndControling()
        {
            await CrossGeolocator.Current.StopListeningAsync();
            CrossGeolocator.Current.PositionChanged -= LocationChangedEventHandler;

        }

        public async void SetMetadataPins()
        {
            PinsToData.Clear();
            var metadatalist= await    CChallangeMetadataList.GetInstance();
            OverviewMapView.Pins.Clear();
            foreach (ChallangeMetadata md in metadatalist)
            {
                var pin = new Pin(OverviewMapView)
                {
                    Label = md.Name,
                    Position = new Mapsui.UI.Forms.Position(md.Lat, md.Lng),
                    Address = new Point(md.Lat, md.Lng).ToString(),
                    Icon = GetDragonIcon(),
                    Color = Xamarin.Forms.Color.Azure,
                    Transparency = 0,
                    Scale = 1,
                    Type = PinType.Icon
                };
                PinsToData.Add(pin, md);
                OverviewMapView.Pins.Add(pin);
            }
        }

        private void LocationChangedEventHandler(object sender, PositionEventArgs e)
        {
            OverviewMapView.GetNavigator().CenterOn(SphericalMercator.FromLonLat(e.Position.Longitude, e.Position.Latitude));
        }

        public async Task StartControling()
        {
            await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(30), 5000);
            CrossGeolocator.Current.PositionChanged += LocationChangedEventHandler;
        }

        public async  void EndControlling()
        {
            await CrossGeolocator.Current.StopListeningAsync();
            CrossGeolocator.Current.PositionChanged -= LocationChangedEventHandler;
        }

        private void MapView_PinClicked(object sender, PinClickedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Clicked " + e.Pin.Label + e.ToString());
            var metaData = PinsToData[e.Pin];
            MView.Message(e.Pin.Label, metaData.Description);

        }

        private static byte[] _diconc =null;
        private static byte[] GetDragonIcon()
        {
            if (_diconc != null) { return _diconc;  }

            Stream image = App.getRessourceStream("japan.png");
            _diconc = image.ToBytes();
            return _diconc;

        }

    }
}
