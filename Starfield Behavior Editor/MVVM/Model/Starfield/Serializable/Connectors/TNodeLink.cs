using BehaviorEditor.MVVM.Model.Starfield.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
#nullable disable
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

        [XmlElement(ElementName = "hidden", Order = 3)]
        public string Hidden { get; set; } = string.Empty;

        [XmlElement(ElementName ="property_sheet", Order =4)]
        public List<PropertySheet> PropertySheets { get; set; } = new List<PropertySheet>();


        [XmlIgnore]
        public TNode ConnectedNode { get; set; }

        internal TNodeLink() { }

        public TNodeLink(TNodeLink link)
        {
            NodeID = link.NodeID;
            Output = link.Output;
            Hidden = link.Hidden;
            ConnectedNode = link.ConnectedNode;
            foreach(PropertySheet propertySheet in link.PropertySheets) { PropertySheets.Add(new PropertySheet(propertySheet)); }
        }




    }
}
