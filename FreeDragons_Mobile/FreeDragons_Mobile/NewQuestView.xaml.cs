using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FreeDragons_Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewQuestView : ContentView
    {
        public Position cords { get; private set; }

        public NewQuestView()
        {
            InitializeComponent();
            //OK.Clicked += ProcessOK;
            //Cancel.Clicked += ProcessCANCLE;
            IsVisible = false;          
        }

        public void InitDialog()
        {
            this.Title.Text = "";
            this.Description.Text = "";
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            if (mainDisplayInfo.Height < 1500) 
            { 
                this.Description.HeightRequest = 200;
                this.TitleLabel.FontSize = TitleLabel.FontSize * 0.5;
                this.DescLabel.FontSize=this.TitleLabel.FontSize;




            }
                    
            cords =DragonServices.GetActualCoords();
            this.Longitude.Text = cords.Longitude.ToString();
            this.Latitude.Text = cords.Latitude.ToString();
        }

        public string getTitle()
        {
            return this.Title.Text;
        }

        public string getDescription()
        {
            return this.Description.Text;
        }

        public double getLongitude()
        {
            return cords.Longitude;
        }

        public double getLatiitude()
        {
            return cords.Latitude;
        }

        private void ProcessOK(object sender, EventArgs e)
        {
            this.IsVisible = false;
        }

        private void ProcessCANCLE(object sender, EventArgs e)
        {
            this.IsVisible = false;
        }
    }
}