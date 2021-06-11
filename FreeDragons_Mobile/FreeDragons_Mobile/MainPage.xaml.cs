using Mapsui.Projection;
using Mapsui.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Mapsui.Widgets;
using Mapsui;
using Mapsui.Layers;
using Mapsui.Providers;
using Mapsui.Styles;
using System.Reflection;
using System.IO;
using Mapsui.UI.Forms;

namespace FreeDragons_Mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var map = new Map();
            

            var tileLayer = OpenStreetMap.CreateTileLayer();

            map.Layers.Add(tileLayer);
            
            map.Layers.Add(CreatePointLayer());
            map.Home = n => n.NavigateTo(map.Layers[1].Envelope.Centroid, map.Resolutions[14]);
            
            map.Widgets.Add(new Mapsui.Widgets.ScaleBar.ScaleBarWidget(map) { TextAlignment = Mapsui.Widgets.Alignment.Center, HorizontalAlignment = Mapsui.Widgets.HorizontalAlignment.Left, VerticalAlignment = Mapsui.Widgets.VerticalAlignment.Bottom });

            mapView.Map = map;
            mapView.Pins.Add(getPin(mapView));
            mapView.PinClicked += MapView_PinClicked;
           




        }

     
        private void MapView_PinClicked(object sender, PinClickedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Clicked " + e.Pin.Label + e.ToString());
            messageView.Message(e.Pin.Label, "TEST\nTest\nTest");

        }

        private MemoryLayer CreatePointLayer() => new MemoryLayer
        {
           
            Name = "Points",
            IsMapInfoLayer = true,
            DataSource = new MemoryProvider(GetMetadataFromEmbeddedResource()),
            //Style = CreateBitmapStyle()
            //Style = CreateSymbolStyle()
           
           
        };

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


    private IEnumerable<IFeature> GetMetadataFromEmbeddedResource()
        {

            var metadata = DeserializeFromStream();

            return metadata.Select(c =>
            {
                var feature = new Feature();
                var point = SphericalMercator.FromLonLat(c.Lng, c.Lat);
                feature.Geometry = point;
                feature["name"] = c.Name;
                feature["country"] = "Germany";
                feature.Styles.Add(new LabelStyle { Text = "Default Label" });
                return feature;
            });
        }

        public static IEnumerable<FreeDragons_Android.ChallangeMetadata> DeserializeFromStream()

        {
            return new FreeDragons_Android.ChallangeMetadataList();
            //var serializer = new JsonSerializer();
            //
            //using (var sr = new StreamReader(stream))
            //using (var jsonTextReader = new JsonTextReader(sr))
            //{
            //    return serializer.Deserialize<List<T>>(jsonTextReader);
            //}
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
