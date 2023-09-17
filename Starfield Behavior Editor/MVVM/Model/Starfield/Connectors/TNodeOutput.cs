using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BehaviorEditor.MVVM.Model.Starfield.Connectors
{
    [Serializable]
    [XmlRoot(ElementName = "output")]
    public class TNodeOutput
    {
        [XmlElement(ElementName = "name", Order =1)]
        public string Name { get; set; }

        [XmlElement(ElementName = "id", Order =2)]
        public int ID { get; set; }

		[XmlElement(ElementName = "idx", Order =3)]
		public int IDX { get; set; }
    }
}
