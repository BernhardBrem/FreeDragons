using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FreeDragons_Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewQuestView : ContentView
    {
        public NewQuestView()
        {
            InitializeComponent();
            OK.Clicked += ProcessOK;
            Cancel.Clicked += ProcessCANCLE;
            this.IsVisible = false;
            
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