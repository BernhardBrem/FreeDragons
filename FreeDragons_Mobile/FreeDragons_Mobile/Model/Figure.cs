using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Freedragons.Model
{
    public class Figure
    {
        public struct FigureDescription
        {
            public String TypeName;
            public int ID;
            public int Team;
            public double Range;
        }

        public static readonly int DragonTypeConst=1;
        public static readonly int GuardTypeConst = 2;
        public static readonly int RebelTypeConst = 3;





        public static readonly IList<FigureDescription> FigureTypes = new ReadOnlyCollection<FigureDescription>(
            new List<FigureDescription>{
                new FigureDescription(){
                    TypeName="Dragon",
                    ID=DragonTypeConst,
                    Range=0,
                    Team=0

                },
                new FigureDescription(){
                    TypeName="Guard",
                    ID=GuardTypeConst,
                    Range=4,
                    Team=2
                },
                new FigureDescription(){
                    TypeName="Rebel",
                    ID=RebelTypeConst,
                    Range=4,
                    Team=1
                }
            }
            );

        public String Name { get; set; }

        public double StartLong { get; set; }

        public double StartLat { get; set; }

        public int FigureType { get; set; }


        public static Figure CreateFigure(double longitude, double latitude)
        {
            Figure fig = new Figure();
            fig.StartLat = latitude;
            fig.StartLong = longitude;
            fig.Name = "";
            return fig;
        }
        public static Figure CreateDragon(double longitude, double latitude)
        {
            Figure fig = CreateFigure(longitude, latitude);
            fig.FigureType = DragonTypeConst;
            return fig;
        }

        public static Figure CreateRebel(double longitude, double latitude)
        {
            Figure fig = CreateFigure(longitude, latitude);
            fig.FigureType = RebelTypeConst;
            return fig;
        }
        public static Figure CreateGuard(double longitude, double latitude)
        {
            Figure fig = CreateFigure(longitude, latitude);
            fig.FigureType = GuardTypeConst;
            return fig;
        }
    }

  


}
