using Freedragons.Model;
using Mapsui.UI.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace FreeDragons_Mobile.Controler
{
    public class OverviewMapControler : IGameControler
    {
        public OverviewMapControler(DragonOverviewMapView overviewMapView, Freedragons.Model.ChallangeMetadataList challangeMetadataList, MessageView mView)
        {
            OverviewMapView = overviewMapView;
            MetaDataList = challangeMetadataList;
            MView = mView;
            PinsToData = new Dictionary<Pin, ChallangeMetadata>();
            OverviewMapView.PinClicked += MapView_PinClicked;
        }

        public DragonOverviewMapView OverviewMapView { get; private set; }
        public ChallangeMetadataList MetaDataList { get; private set; }

        Dictionary<Pin, ChallangeMetadata> PinsToData { get; set; }

        MessageView MView { get; set; }

        public void StartControling()
        {
            foreach (ChallangeMetadata md in MetaDataList) {
                var pin = new Pin(OverviewMapView)
                {
                    Label = md.Name,
                    Position = new Position(md.Lat, md.Lng),
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
