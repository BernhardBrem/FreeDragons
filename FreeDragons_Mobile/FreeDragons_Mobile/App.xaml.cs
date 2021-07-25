using FreeDragons_Mobile.Controler;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FreeDragons_Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage mpage = new MainPage();
            MainPage = mpage;
            DragonGameControler dragonGameControler = new DragonGameControler()
            {
                OverviewMapView = mpage.overviewMapView,
                NQView = mpage.newQuestView,
                MView = mpage.messageView,
                DragonGameEditorMapView = mpage.gameEditorMapView
            };
            dragonGameControler.StartControling();
        }

       
       
        private static Dictionary<string, Stream> resources = new Dictionary<string, Stream>();

        public DragonGameControler Controler { get; set; }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public static Stream getRessourceStream(string path)
        {
            return resources[path];
        }

        public static void setRessourceStream(string key, Stream value)
        {
            resources[key] = value;
        }


    }
}
