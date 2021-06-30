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
        public OverviewMapControler(DragonOverviewMapView overviewMapView)
        {
            this.OverviewMapView = overviewMapView;
        }

        public DragonOverviewMapView OverviewMapView { get; private set; }

        public void StartControling()
        {
            OverviewMapView.Pins.Add(getPin(OverviewMapView));
            
           
        }

        public static Pin getPin(Mapsui.UI.Forms.MapView mapView)
        {
            var pin = new Pin(mapView)
            {
                Label = $"PinType.Pin",
                Position = new Position(48.1365155, 11.5055562),
                Address = new Point(48.1365155, 11.5055562).ToString(),
                Type = PinType.Icon,
                Icon = GetDragonIcon(),
                Color = Xamarin.Forms.Color.Azure,
                Transparency = 0,
                Scale = 1
            };
            return pin;
        }

       


        private static byte[] GetDragonIcon()
        {

            Stream image = App.getRessourceStream("japan.png");
            var bts = image.ToBytes();
            return bts;

        }

    }
}
