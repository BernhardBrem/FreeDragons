using Freedragons.Model;
using FreeDragons_Mobile.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FreeDragons_Mobile.Controler
{
    public class NewQuestDialogControler : IGameControler
    {
        private NewQuestView newQuestDialogView;

        public NewQuestDialogControler(NewQuestView newQuestDialogView)
        {
            this.newQuestDialogView = newQuestDialogView;
        }

        public async Task EndControling()
        {
            newQuestDialogView.IsVisible = false;
        }

        public async Task StartControling()
        {
            
            await newQuestDialogView.InitDialog();
            newQuestDialogView.IsVisible = true;
        }

        internal ChallangeMetadata GetMetadata()
        {
            return new ChallangeMetadata(newQuestDialogView.getTitle(), newQuestDialogView.getLatiitude(), newQuestDialogView.getLongitude(), newQuestDialogView.getDescription());

        }
    }
}
