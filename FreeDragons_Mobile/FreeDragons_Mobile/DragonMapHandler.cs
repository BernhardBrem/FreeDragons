using Mapsui;
using Mapsui.Layers;
using Mapsui.Projection;
using Mapsui.Providers;
using Mapsui.Styles;
using Mapsui.UI.Forms;
using Mapsui.Utilities;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FreeDragons_Mobile
{
    class DragonMapHandler
    {
        private DragonMapHandler() { }

        private MapView theView
        {
            get;
            set;
        }
        public Map map;
        public INavigator navigator;
        public void Init(MapView view)
        {
            theView = view;
            map = CreateMap();

        }

        private Map CreateMap()
        {
            map = new Map();
            var tileLayer = OpenStreetMap.CreateTileLayer();

            map.Layers.Add(tileLayer);

            map.Layers.Add(CreatePointLayer());
            map.Home = n => this.navigator=n;

            map.Widgets.Add(new Mapsui.Widgets.ScaleBar.ScaleBarWidget(map) { TextAlignment = Mapsui.Widgets.Alignment.Center, HorizontalAlignment = Mapsui.Widgets.HorizontalAlignment.Left, VerticalAlignment = Mapsui.Widgets.VerticalAlignment.Bottom });

            theView.Map = map;
            CrossGeolocator.Current.PositionChanged += LocationChangedEventHandler;

            return map;
        }


        private MemoryLayer CreatePointLayer() => new MemoryLayer
        {

            Name = "Points",
            IsMapInfoLayer = true,
            DataSource = new MemoryProvider(GetMetadataFromEmbeddedResource()),
            //Style = CreateBitmapStyle()
            //Style = CreateSymbolStyle()


        };

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

        }

        // Define what actions to take when the event is raised.
        void LocationChangedEventHandler(object sender, PositionEventArgs e)
        {

            if (navigator != null)
            {
                Debug.WriteLine("Set coords to " + e.Position.Longitude.ToString() + " " + e.Position.Latitude.ToString());

                Mapsui.Geometries.Point sphericalMercatorCoordinate = SphericalMercator.FromLonLat(e.Position.Longitude, e.Position.Latitude);
                navigator.NavigateTo(sphericalMercatorCoordinate, 10);
            }
        }


        // Singlet-Pattern
        private static DragonMapHandler theClass;


        public static DragonMapHandler Instance
        {
            get { 
                if (theClass == null){
                    theClass = new DragonMapHandler();
                }
                return theClass;
            }
        }
    }
}
