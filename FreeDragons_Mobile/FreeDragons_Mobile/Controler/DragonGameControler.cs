using Freedragons.Model;
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

        public DragonGameEditorMapView DragonGameEditorMapView { get; set; }
        public GameEditorControler GameEditorControler { get; private set; }



        void StartControlingOverviewMap()
        {
            OverviewMapControler = new OverviewMapControler(OverviewMapView,DragonObjects.getMetadataList(),MView);
            OverviewMapView.MapAddQuestButton.Clicked += ShowNewQuest;
            OverviewMapControler.StartControling();
        }

       
        private void ShowNewQuest(object sender, object args)
        {
            NQView.IsVisible = true;
            NQView.InitDialog();
            OverviewMapView.IsVisible = false;
        }

        private void startControlingEditorMap()
        {
            GameEditorControler = new GameEditorControler(this.DragonGameEditorMapView);
            GameEditorControler.StartControling();
            DragonGameEditorMapView.OKButton.Clicked += EditorMapViewOKButton_Clicked;
            DragonGameEditorMapView.CancelButton.Clicked += EditorMapViewCancelButton_Clicked;
            
        }

     
        

        private void EditorMapViewCancelButton_Clicked(object sender, EventArgs e)
        {
            DragonGameEditorMapView.IsVisible = false;
            this.OverviewMapView.IsVisible = true;
        }

        private void EditorMapViewOKButton_Clicked(object sender, EventArgs e)
        {
            DragonGameEditorMapView.IsVisible = false;
            Quest NewQuest = GameEditorControler.Quest;
            // TODO: Submit this quest!
            this.OverviewMapView.IsVisible = true;
        }

        public void StartControling()
        {
            StartControlingOverviewMap();
            NQView.Cancel.Clicked += ShowOverviewData;
            NQView.OK.Clicked += NQOK_Clicked;
            startControlingEditorMap();


            DragonServices.StartListeningLocation();


        }

        

        private void NQOK_Clicked(object sender, EventArgs e)
        {
            NQView.IsVisible = false;
            ChallangeMetadata metadata = new ChallangeMetadata(NQView.getTitle(), NQView.getLatiitude(), NQView.getLongitude(),NQView.getDescription());
            GameEditorControler.StartEditing(metadata);
            
            
        }

        private void ShowOverviewData(object sender, object args)
        {
            NQView.IsVisible = false;
            OverviewMapView.IsVisible = true;
        }



    }
}
