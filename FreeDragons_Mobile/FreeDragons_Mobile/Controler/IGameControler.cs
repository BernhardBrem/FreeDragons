using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FreeDragons_Mobile.Controler
{
    public interface IGameControler
    {
        Task StartControling();

        Task EndControling();
    }
}
