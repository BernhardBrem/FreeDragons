using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

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
            _elements.Add(new ChallangeMetadata("FirstDragon", 48.1365155, 11.5055562));

            return _elements;
        }
    }



    public class ChallangeMetadata
    {
        public ChallangeMetadata(string name, double lat, double lng) {
            Name = name;
            Lat = lat;
            Lng = lng;
        }

        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
