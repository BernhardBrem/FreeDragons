﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;

namespace FreeDragons_Mobile.Droid
{
    [Activity(Label = "FreeDragons_Mobile", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            System.IO.Stream jpn =  Assets.Open("japan.png");
            App.setRessourceStream("japan.png", jpn);
            App.setRessourceStream("DragonLocation.png", Assets.Open("DragonLocation.png"));
            App.setRessourceStream("OwnLocation.png", Assets.Open("OwnLocation.png"));
            App.setRessourceStream("GuardLocation.png", Assets.Open("GuardLocation.png"));
            App.setRessourceStream("RebelLocation.png", Assets.Open("RebelLocation.png"));
            App.setRessourceStream("Dragon.svg", Assets.Open("Dragon.svg"));

            var app = new App();
            LoadApplication(app);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        
    }
}