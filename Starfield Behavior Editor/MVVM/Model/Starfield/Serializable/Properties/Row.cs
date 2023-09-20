using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace BehaviorEditor.MVVM.Model.Starfield.Properties
{
	[Serializable]
    [XmlRoot(ElementName = "row")]
    public class Row
    {
        [XmlElement(ElementName = "prop", Order =1)]
        public List<Property> Properties { get; set; } = new List<Property>();

        
        public bool FindPropertyByValue(string value, out Property? outProperty)
        {
            outProperty = Properties.Where(p => p.RawValue == value).FirstOrDefault()!;

            return outProperty != null;
        }

        internal Row() { }

        public Row(Row row)
        {
            foreach(Property property in row.Properties) { Properties.Add(new Property(property)); }
        }
    }
}
