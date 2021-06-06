using System;


using System.Collections.Generic;

namespace FreeDragons_Objects
{
    public class GeoPoint
    {
        public GeoPoint(double lati, double longi)
        {
            longitude = longi;
            latitude = lati;
        }

        double longitude { get; set; }
        double latitude { get; set; }
    }

    public class gameMetaData
    {
        public gameMetaData(string nam, double lati, double longi )
        {
            name = nam;
            location = new GeoPoint(lati,longi);



        }
        string name { get; set; }
        GeoPoint location { get; set; }
    }

    public class gameList
    {
        List<gameMetaData> allGames = new List<gameMetaData>();
        gameList(bool isTest)
        {
            if (isTest)
            {
                fillListWithTestData();
            }
        }

        private void fillListWithTestData()
        {
            allGames.Add(new gameMetaData("FirstDragon", 48.1365155, 11.5055562));
        }

        public List<gameMetaData> getAllGames() { return allGames; }




    }



}
