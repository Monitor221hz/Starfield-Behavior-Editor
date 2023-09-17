using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BehaviorEditor.MVVM.Model.Starfield.Connectors
{
	[Serializable]
	[XmlRoot(ElementName = "input")]
	public class TNodeInput
    {
		[XmlElement(ElementName = "name", Order =1,IsNullable =true)]
		public string Name { get; set; }

		[XmlElement(ElementName = "id", Order =2)]
		public int ID { get; set; }

		[XmlElement(ElementName = "idx", Order =3)]
		public int IDX { get; set; }

		[XmlElement(ElementName ="link", Order =4)]
		public List<TNodeLink> Links { get; set; } = new List<TNodeLink>();
	}
}
