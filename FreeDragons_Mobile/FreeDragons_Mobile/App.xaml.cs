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
            MainPage = new MainPage();
            
        }

       
       
        private static Dictionary<string, Stream> resources = new Dictionary<string, Stream>();

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
