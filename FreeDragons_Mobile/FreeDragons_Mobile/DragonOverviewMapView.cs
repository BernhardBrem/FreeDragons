using Mapsui;
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
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;


namespace FreeDragons_Mobile
{
    public class DragonOverviewMapView:MapView
    {
        public ImageButton MapAddQuestButton;
        private readonly ImageButton _mapLockButton;
        private readonly ImageButton _mapHelpButton;



        private readonly StackLayout _overviewmapButtons;

        public DragonOverviewMapView() : base()
        {

            Mapsui.Map map = new Map();
            var tileLayer = OpenStreetMap.CreateTileLayer();

            map.Layers.Add(tileLayer);
            //map.Layers.Add(CreatePointLayer(new MemoryProvider(GetMetadataFromEmbeddedResource())));
            //map.Home = initNavigator;

            //map.Widgets.Add(new Mapsui.Widgets.ScaleBar.ScaleBarWidget(map) { TextAlignment = Mapsui.Widgets.Alignment.Center, HorizontalAlignment = Mapsui.Widgets.HorizontalAlignment.Left, VerticalAlignment = Mapsui.Widgets.VerticalAlignment.Bottom });

            
            CrossGeolocator.Current.PositionChanged += LocationChangedEventHandler;
            




            // Buttons:
            // Disable old ones
            IsMyLocationButtonVisible = false;
            IsNorthingButtonVisible = false;
            IsZoomButtonVisible = false;
            ZoomLock = false;

            MapAddQuestButton = new ImageButton()//Mapsui.Utilities.EmbeddedResourceLoader.Load("Images.RotationZero.svg", typeof(MapView)))
            {
                Source = "newquest.png",
                // Important: Letters: Harlow Solid Italic 26; Rest copy/paste for new icons
                BackgroundColor = Xamarin.Forms.Color.Transparent,
                //BackgroundColor = Color.Black,
                WidthRequest = 80,
                HeightRequest = 80,
                //Command = new Command(obj => Device.BeginInvokeOnMainThread(() => Navigator.RotateTo(0))),
            };
            _mapLockButton = new ImageButton()//Mapsui.Utilities.EmbeddedResourceLoader.Load("Images.RotationZero.svg", typeof(MapView)))
            {
                Source = "unlock.png",
                // Important: Letters: Harlow Solid Italic 26; Rest copy/paste for new icons
                BackgroundColor = Xamarin.Forms.Color.Transparent,
                //BackgroundColor = Color.Black,
                WidthRequest = 80,
                HeightRequest = 80,
                Command = new Command(obj => Device.BeginInvokeOnMainThread(() => Navigator.RotateTo(0))),
            };
            _mapHelpButton = new ImageButton()//Mapsui.Utilities.EmbeddedResourceLoader.Load("Images.RotationZero.svg", typeof(MapView)))
            {
                Source = "help.png",
                // Important: Letters: Harlow Solid Italic 26; Rest copy/paste for new icons
                BackgroundColor = Xamarin.Forms.Color.Transparent,
                //BackgroundColor = Color.Black,
                WidthRequest = 80,
                HeightRequest = 80,
                Command = new Command(obj => Device.BeginInvokeOnMainThread(() => Navigator.RotateTo(0))),
            };


            _overviewmapButtons = new StackLayout { BackgroundColor = Xamarin.Forms.Color.Transparent, Spacing = 5, IsVisible = true, InputTransparent = true, CascadeInputTransparent = false };
            _overviewmapButtons.Children.Add(MapAddQuestButton);
            _overviewmapButtons.Children.Add(_mapLockButton);
            _overviewmapButtons.Children.Add(_mapHelpButton);
            AbsoluteLayout.SetLayoutBounds(_overviewmapButtons, new Rectangle(0.95, 0.07, 80, 176));
            AbsoluteLayout.SetLayoutFlags(_overviewmapButtons, AbsoluteLayoutFlags.PositionProportional);
            ((AbsoluteLayout)Content).Children.Add(_overviewmapButtons);

            //map.Home = navigateHome ;//map.Resolutions[map.Resolutions.Count - 1]);
            Map = map;
            navigateHome(this.Navigator);
        }

        public void navigateHome(INavigator n)
        {
            var pt = SphericalMercator.FromLonLat(0, 0);
            var pos = DragonServices.GetLastKnownLocationCoords();
            try
            {
                pt = SphericalMercator.FromLonLat(pos.Longitude, pos.Latitude);
            }
            catch
            {
                // No prev. avail - start from default
            }
            Debug.WriteLine("Navigating!");
            n.NavigateTo(pt, this.Map.Resolutions[5]);
        }

        private void LocationChangedEventHandler(object sender, PositionEventArgs e)
        {
            Navigator.CenterOn(SphericalMercator.FromLonLat(e.Position.Longitude, e.Position.Latitude));
        }

        


    }
}
