using Nito.AsyncEx;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace FreeDragons_Mobile
{
    class DragonLocator
    {
        async static Task StartListeningAsync()
        {

      
            var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            if (status == PermissionStatus.Granted)
            {
                await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(5), 10, true);
            }
            
            

            
        }

        public static void startListening()
        {
            
            AsyncContext.Run(StartListeningAsync);
        }
    }

    
}
