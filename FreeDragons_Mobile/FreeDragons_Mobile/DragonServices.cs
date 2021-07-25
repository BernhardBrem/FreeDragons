using Nito.AsyncEx;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace FreeDragons_Mobile
{
    class DragonServices
    {
        async static Task StartListeningLocationAsync()
        {

      
            var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            if (status == PermissionStatus.Granted)
            {
                await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(5), 10, true);
            }
            
        }

        async private static Task<Plugin.Geolocator.Abstractions.Position> GetLastCoordsOnce()
        {
            var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            if (status == PermissionStatus.Granted)
            {
               var result = await CrossGeolocator.Current.GetLastKnownLocationAsync();
                return result;
            }
            return null;
        }

        async private static Task<Plugin.Geolocator.Abstractions.Position> GetActualCoordsOnce()
        {
            var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            if (status == PermissionStatus.Granted)
            {
                Plugin.Geolocator.Abstractions.Position result = await CrossGeolocator.Current.GetPositionAsync();
                return result;
            }
            return null;
        }
        public static void StartListeningLocation()
        {
            
            AsyncContext.Run(StartListeningLocationAsync);
        }

        public static Plugin.Geolocator.Abstractions.Position GetLastKnownLocationCoords()
        {
            Task<Plugin.Geolocator.Abstractions.Position> task = GetLastCoordsOnce();
            task.Wait();
            return task.Result;
        }

        public static Plugin.Geolocator.Abstractions.Position GetActualCoords()
        {
            Task<Plugin.Geolocator.Abstractions.Position> task = GetActualCoordsOnce();
            task.Wait();
            return task.Result;
        }
    }

    
}
