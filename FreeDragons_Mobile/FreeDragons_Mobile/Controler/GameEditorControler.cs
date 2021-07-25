using Freedragons.Model;
using Mapsui.Geometries;
using Mapsui.UI.Forms;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FreeDragons_Mobile.Controler
{
    public class GameEditorControler : IGameControler
    {
        public Plugin.Geolocator.Abstractions.Position OwnPosition { get; private set; }

        private Pin OwnLocationPin;

        public GameEditorControler(DragonGameEditorMapView editorMapView)
        {
            this.EditorMapView = editorMapView;
           

        }

        public DragonGameEditorMapView EditorMapView { get; private set; }
        public ChallangeMetadata Metadata { get; private set; }
        public CQuest Quest { get; private set; }

        public Dictionary<Pin, Figure> FigureDict = new Dictionary<Pin, Figure>();

        public void StartControling()
        {
            OwnPosition = DragonServices.GetLastKnownLocationCoords();
            OwnLocationPin = new Pin(EditorMapView)
            {
                Label = "",
                Position = new Position(OwnPosition.Latitude, OwnPosition.Longitude),
                Icon = GetOwnLocationIcon(),
                //Color = Xamarin.Forms.Color.Azure,
                Transparency = 0,
                Scale = 1,
                Type = PinType.Icon
            };

            EditorMapView.Pins.Add(OwnLocationPin);
            CrossGeolocator.Current.PositionChanged += LocationChangedEventHandler;
            EditorMapView.DragonButton.Clicked += AddDragonFigure;
            EditorMapView.GuardButton.Clicked += AddGuardFigure;
            EditorMapView.RebelButton.Clicked += AddRebelFigure;
            EditorMapView.OKButton.Clicked += UploadQuest;


        }

        async private void UploadQuest(object sender, EventArgs e) => await Quest.publishToServer();

        public void addOneFigure(Figure f, Byte[] icon)

        {
            Pin p = new Pin(EditorMapView)
            {
                Label = "",
                Position = new Position(OwnPosition.Latitude, OwnPosition.Longitude),
                Icon = icon,
                //Color = Xamarin.Forms.Color.Azure,
                Transparency = 0,
                Scale = 1,
                Type = PinType.Icon
            };

            if (!FigureDict.ContainsKey(p))
            {
                EditorMapView.Pins.Add(p);
                FigureDict.Add(p, f);
            }
            if (Quest.initialSetup == null)
            {
                Quest.initialSetup = new List<Figure>();
            }
            Quest.initialSetup.Add(f);
        }

        private void AddGuardFigure(object sender, EventArgs e)
        {
            Figure f = Figure.CreateGuard(OwnPosition.Longitude, OwnPosition.Latitude);
            addOneFigure(f, GetGuardLocationIcon());

        }

        private void AddRebelFigure(object sender, EventArgs e)
        {
            Figure f = Figure.CreateRebel(OwnPosition.Longitude, OwnPosition.Latitude);
            addOneFigure(f, GetRebelLocationIcon());
        }


        private void AddDragonFigure(object sender, EventArgs e)
        {
            Figure f = Figure.CreateDragon(OwnPosition.Longitude,OwnPosition.Latitude);
            addOneFigure(f, GetDragonLocationIcon());
          
        }

        private void LocationChangedEventHandler(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            OwnPosition = e.Position;
            OwnLocationPin.Position = new Position(OwnPosition.Latitude, OwnPosition.Longitude);
        }

        public void StartEditing(ChallangeMetadata metadata)
        {
            EditorMapView.IsVisible = true;
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
