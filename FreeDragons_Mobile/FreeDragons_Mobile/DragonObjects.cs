using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FreeDragons_Android
{
    public class ChallangeMetadataList: IEnumerable<ChallangeMetadata>
    {

        List<ChallangeMetadata> _elements;
        public ChallangeMetadataList() { 
            _elements = getTestElements(); 
        }
        
        IEnumerator<ChallangeMetadata> IEnumerable<ChallangeMetadata>.GetEnumerator()
        {
            return this._elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._elements.GetEnumerator();
        }


        List<ChallangeMetadata> getTestElements()
        {
            var _elements = new List<ChallangeMetadata>();
            _elements.Add(new ChallangeMetadata("FirstDragon", 48.1365155, 11.5055562, "Free the dragons here!"));

            return _elements;
        }
    }



    public class ChallangeMetadata
    {
        public ChallangeMetadata(string name, double lat, double lng, string description) {
            Name = name;
            Description = description;
            Lat = lat;
            Lng = lng;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }

    }

    public class Figure
    {
        public struct FigureDescription
        {
            public String TypeName;
            public int ID;
            public int Team;
            public double Range;
        }

        public static readonly IList<FigureDescription> FigureTypes = new ReadOnlyCollection<FigureDescription>(
            new List<FigureDescription>{
                new FigureDescription(){
                    TypeName="Dragon",
                    ID=1,
                    Range=0,
                    Team=0

                },
                new FigureDescription(){
                    TypeName="Keeper",
                    ID=2,
                    Range=4,
                    Team=2
                },
                new FigureDescription(){
                    TypeName="Saver",
                    ID=3,
                    Range=4,
                    Team=1
                }
            }
            );

        public String Name { get; set; }

        public double StartLong { get; set; }

        public double StartLat { get; set; }

        public int FigureType { get; set; }

    }

    public class Quest
    {
        public ChallangeMetadata Metadata { get; set; }
        public List<Figure> initialSetup { get; set; }
        public List<Figure> currentSetup { get; set; }
        public int Round { get; set; }

    }
}
