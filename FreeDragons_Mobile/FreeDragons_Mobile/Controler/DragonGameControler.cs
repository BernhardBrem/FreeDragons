﻿using Freedragons.Model;
using FreeDragons_Mobile.View;
using Mapsui.UI.Forms;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FreeDragons_Mobile.Controler
{
    public class DragonGameControler : IGameControler
    {
        // The views
        public DragonOverviewMapView OverviewMapView { get; set; }
        public MessageView MView { get; set; }
        public NewQuestView NewQuestDialogView { get; set; }
        public GameEntryView GameEntryView { get; set; }
        public QuestMapView QuestMapView { get; set; }
        // The controler
        public NewQuestDialogControler NewQuestDialogControler { get; set; }

        public OverviewMapControler OverviewMapControler { get; private set; }

        public DragonGameEditorMapView DragonGameEditorMapView { get; set; }
        public GameEditorControler GameEditorControler { get; private set; }

        public GameEntryControler GameEntryControler { get; private set; }

        public QuestExecutionControler QuestExecutionControler { get; private set; }

        public async Task StartControling()
        {
           
            GameEditorControler = new GameEditorControler(DragonGameEditorMapView);
            GameEditorControler.bindButtons();
            var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            //this.MView = new MessageView();
            if (status != PermissionStatus.Granted)
            {
                MView.Message("Permission needet!", "Game needs lokation. Without it game does not work! Please grant location permission to this game!");
            }


            OverviewMapControler = new OverviewMapControler(OverviewMapView, MView);
            NewQuestDialogControler = new NewQuestDialogControler(NewQuestDialogView);

            //StartControlingOverviewMap();
            NewQuestDialogView.Cancel.Clicked += SwitchToDefaultScreen;
            NewQuestDialogView.OK.Clicked += NQOK_Clicked;
            //startControlingEditorMap();
            //DragonLocationServices.StartListeningLocation();

            DragonGameEditorMapView.OKButton.Clicked += EditorMapViewOKButton_Clicked;
            DragonGameEditorMapView.CancelButton.Clicked += EditorMapViewCancelButton_Clicked;

            OverviewMapView.MapAddQuestButton.Clicked += ShowNewQuestDialog;
            GameEntryView.NewQuest.Clicked+= ShowNewQuestDialog;
            GameEntryControler = new GameEntryControler(GameEntryView);
            GameEntryView.SwitchToOverviewMap.Clicked += this.ShowOverviewMapview;
            GameEntryView.ReachableQuestView.ItemTapped += this.ReachableQuestTapped;
            GameEntryView.ReachableQuestView.ItemSelected += this.ReachableQuestSelected;
            QuestExecutionControler = new QuestExecutionControler(QuestMapView);

            await SwitchToDefaultScreen();


        }

        private void ReachableQuestSelected(object sender, SelectedItemChangedEventArgs e)
        {
            QuestListItem it = (QuestListItem)e.SelectedItem;
            this.startQuest(it);

        }

        private void ReachableQuestTapped(object sender, ItemTappedEventArgs e)
        {
            QuestListItem it = (QuestListItem)e.Item;
            this.startQuest(it);
           
        }

        private async Task startQuest(QuestListItem it)
        {
            await ClearForNewMode();
            QuestMapView.IsVisible = true;
            await QuestExecutionControler.StartControling();
            QuestExecutionControler.StartNewQuest(it.metaData);

        }

        private void ShowOverviewMapview(object sender, EventArgs e)
        {
            SwitchToOverviewMapview();
        }

        public async Task EndControling()
        {

        }

        private async Task ClearForNewMode()
        {
            NewQuestDialogView.IsVisible = false;
            OverviewMapView.IsVisible = false;
            DragonGameEditorMapView.IsVisible = false;
            GameEntryView.IsVisible = false;
            QuestMapView.IsVisible = false;
            await NewQuestDialogControler.EndControling();
            await GameEditorControler.EndControling();
            await OverviewMapControler.EndControling();
            await QuestExecutionControler.EndControling();

        }

        private async void ShowNewQuestDialog(object sender, object args)
        {
            await ClearForNewMode();
            await NewQuestDialogControler.StartControling();
        }

        private async void SwitchToGameEntry()
        {
            await ClearForNewMode();
           
            GameEntryView.IsVisible = true;
            await GameEntryControler.StartControling();
        }

        private async void EditorMapViewCancelButton_Clicked(object sender, EventArgs e)
        {
            await SwitchToDefaultScreen();
        }


        private async void SwitchToDefaultScreen(object sender, object args)
        { 
            await SwitchToDefaultScreen();
            
        }

        private async Task SwitchToDefaultScreen()
        {
            await ClearForNewMode();
            SwitchToGameEntry();
        }

        private async void EditorMapViewOKButton_Clicked(object sender, EventArgs e)
        {
            await GameEditorControler.EndControling();
            await SwitchToDefaultScreen();
            
            
        }

       
        private async void SwitchToOverviewMapview()
        {
            await ClearForNewMode();
            OverviewMapView.IsVisible = true;
            await OverviewMapControler.StartControling();
        }


        // Top Game logic: Switch between different game controler / views 

        private async void NQOK_Clicked(object sender, EventArgs e)
        {
            await NewQuestDialogControler.EndControling();
            ChallangeMetadata metadata = NewQuestDialogControler.GetMetadata();
            SwitchToGameEditorControlerNewQuest(metadata);


        }

        private async void  SwitchToGameEditorControlerNewQuest(ChallangeMetadata metadata)
        {
            await GameEditorControler.StartControling();
            GameEditorControler.StartEditingNewQuest(metadata);
        }





    }
}
