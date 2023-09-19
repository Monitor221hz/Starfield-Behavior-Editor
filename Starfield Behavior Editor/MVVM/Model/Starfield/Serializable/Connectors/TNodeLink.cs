using BehaviorEditor.MVVM.Model.Starfield.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BehaviorEditor.MVVM.Model.Starfield.Connectors
{
    [Serializable]
    [XmlRoot(ElementName = "link")]
    public class TNodeLink
    {
        [XmlElement(ElementName = "node", Order =1)]
        public int NodeID { get; set;  }

        [XmlElement(ElementName = "output", Order =2)]
        public int Output { get; set; }

        [XmlElement(ElementName = "hidden", Order =3)]
        public string Hidden { get; set; }

        [XmlElement(ElementName ="property_sheet", Order =4)]
        public List<PropertySheet> PropertySheets { get; set; }


        [XmlIgnore]
        public TNode ConnectedNode { get; set; }




    }
}
