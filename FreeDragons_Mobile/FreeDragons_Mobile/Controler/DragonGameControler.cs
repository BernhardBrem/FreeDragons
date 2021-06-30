using Mapsui.UI.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace FreeDragons_Mobile.Controler
{
    public class DragonGameControler:IGameControler
    {
        // The views
        public DragonOverviewMapView OverviewMapView { get; set; }
        public MessageView MView { get; set; }

        public NewQuestView NQView { get; set; }
        public OverviewMapControler OverviewMapControler { get; private set; }

        void StartControlingOverviewMap()
        {
            OverviewMapControler = new OverviewMapControler(OverviewMapView);
            OverviewMapView.MapAddQuestButton.Clicked += ShowNewQuest;
            OverviewMapView.PinClicked += MapView_PinClicked;
            OverviewMapControler.StartControling();
        }

       
        private void ShowNewQuest(object sender, object args)
        {
            NQView.IsVisible = true;
            NQView.InitDialog();
            OverviewMapView.IsVisible = false;
        }

        private void MapView_PinClicked(object sender, PinClickedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Clicked " + e.Pin.Label + e.ToString());
            MView.Message(e.Pin.Label, "TEST\nTest\nTest");

        }





        public void StartControling()
        {
            StartControlingOverviewMap();
            NQView.Cancel.Clicked += ShowOverviewData;
            DragonServices.StartListeningLocation();


        }
        private void ShowOverviewData(object sender, object args)
        {
            NQView.IsVisible = false;
            OverviewMapView.IsVisible = true;
        }



    }
}
