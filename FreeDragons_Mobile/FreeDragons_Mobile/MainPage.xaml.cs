using Mapsui.Projection;
using Mapsui.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using System.IO;
using Mapsui.UI.Forms;
using Position = Mapsui.UI.Forms.Position;
using System.Diagnostics;

namespace FreeDragons_Mobile
{
    public partial class MainPage : ContentPage
    {
        
        public MainPage()
        {
            InitializeComponent();
            DragonMapHandler.Instance.Init(mapView);
            mapView.Pins.Add(getPin(mapView));
            mapView.PinClicked += MapView_PinClicked;


            // Subscribe to the event
            //new FreeDragons_Mobile.GeoLocation().LocationChangedEvent += LocationChangedEventHandler;
            DragonLocator.startListening();
           
        }



     
        private void MapView_PinClicked(object sender, PinClickedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Clicked " + e.Pin.Label + e.ToString());
            messageView.Message(e.Pin.Label, "TEST\nTest\nTest");

        }

       

        public static Pin getPin(Mapsui.UI.Forms.MapView mapView)
        {
            var pin = new Pin(mapView) {
                Label = $"PinType.Pin",
                Position = new Position(48.1365155, 11.5055562),
                Address = new Point(48.1365155, 11.5055562).ToString(),
                Type = PinType.Icon,
                Icon=GetDragonIcon(),
                Color = Xamarin.Forms.Color.Azure,
                Transparency = 0,
                Scale = 1
            };
            
            return pin;
        }


 

  





        private static byte[] GetDragonIcon()
        {
            //var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            //var theApp = (App)(App.Current);
            //var image = theApp.getDragonIcon();
            //var image = new Image { Source = ImageSource.FromResource(imagePath) };
            Stream image = App.getRessourceStream("japan.png");
            var bts = image.ToBytes();
            return bts;
            //string utfString = Encoding.UTF8.GetString(bts, 0, bts.Length);
            //var assembly = typeof(App).GetTypeInfo().Assembly;
            //var image = assembly.GetManifestResourceStream(imagePath);
            //return utfString;
        }


    }
}
