using Mapsui;
using Mapsui.Projection;
using Mapsui.UI.Forms;
using Mapsui.Utilities;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace FreeDragons_Mobile
{
    public class DragonGameEditorMapView : MapView
    {
        public ImageButton OKButton;
        public ImageButton CancelButton;
        public ImageButton DragonButton;
        public ImageButton GuardButton;
        public ImageButton RebelButton;



        public StackLayout okcancelgroup { get; private set; }
        public StackLayout figuregroup { get; }

        public DragonGameEditorMapView() : base(){
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


            OKButton = new ImageButton()//Mapsui.Utilities.EmbeddedResourceLoader.Load("Images.RotationZero.svg", typeof(MapView)))
            {
                Source = "OK_CARD.png",
                // Important: Letters: Harlow Solid Italic 26; Rest copy/paste for new icons
                BackgroundColor = Xamarin.Forms.Color.Transparent,
                //BackgroundColor = Color.Black,
                WidthRequest = 120,
                HeightRequest = 80,
            };
            CancelButton = new ImageButton()//Mapsui.Utilities.EmbeddedResourceLoader.Load("Images.RotationZero.svg", typeof(MapView)))
            {
                Source = "Cancel_CARD.png",
                // Important: Letters: Harlow Solid Italic 26; Rest copy/paste for new icons
                BackgroundColor = Xamarin.Forms.Color.Transparent,
                //BackgroundColor = Color.Black,
                WidthRequest = 120,
                HeightRequest = 80,
            };
            DragonButton = new ImageButton()
            {
                Source = "Dragon_80_80.png",
                BackgroundColor = Xamarin.Forms.Color.Transparent,
                //BackgroundColor = Color.Black,
                WidthRequest = 80,
                HeightRequest = 80,
            };

            GuardButton = new ImageButton()
            {
                Source = "guard_80_80.png",
                BackgroundColor = Xamarin.Forms.Color.Transparent,
                //BackgroundColor = Color.Black,
                WidthRequest = 80,
                HeightRequest = 80,
            };
            RebelButton = new ImageButton()
            {
                Source = "Rebel_80_80.png",
                BackgroundColor = Xamarin.Forms.Color.Transparent,
                //BackgroundColor = Color.Black,
                WidthRequest = 80,
                HeightRequest = 80,
            };
            okcancelgroup = new StackLayout { BackgroundColor = Xamarin.Forms.Color.Transparent, Spacing = 5, IsVisible = true, InputTransparent = true, CascadeInputTransparent = false , Orientation=StackOrientation.Horizontal};
            okcancelgroup.Children.Add(OKButton);
            okcancelgroup.Children.Add(CancelButton);
            
            figuregroup = new StackLayout { BackgroundColor = Xamarin.Forms.Color.Transparent, Spacing = 5, IsVisible = true, InputTransparent = true, CascadeInputTransparent = false, Orientation = StackOrientation.Vertical };
            figuregroup.Children.Add(DragonButton);
            figuregroup.Children.Add(GuardButton);
            figuregroup.Children.Add(RebelButton);
            AbsoluteLayout.SetLayoutBounds(okcancelgroup, new Rectangle(0.07, 0.9, 250, 80));
            AbsoluteLayout.SetLayoutFlags(okcancelgroup, AbsoluteLayoutFlags.PositionProportional);
            ((AbsoluteLayout)Content).Children.Add(okcancelgroup);
            AbsoluteLayout.SetLayoutBounds(figuregroup, new Rectangle(0.9, 0.1, 80, 260));
            AbsoluteLayout.SetLayoutFlags(figuregroup, AbsoluteLayoutFlags.PositionProportional);
            ((AbsoluteLayout)Content).Children.Add(figuregroup);

            //map.Home = navigateHome ;//map.Resolutions[map.Resolutions.Count - 1]);
            Map = map;
            navigateHome(this.Navigator);

        }

        private void LocationChangedEventHandler(object sender, PositionEventArgs e)
        {
            Navigator.CenterOn(SphericalMercator.FromLonLat(e.Position.Longitude, e.Position.Latitude));
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
            n.NavigateTo(pt, Map.Resolutions[Map.Resolutions.Count - 1]);
        }
         

}
}
