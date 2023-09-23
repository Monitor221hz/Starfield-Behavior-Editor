using Nodify;
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
	public class TNodeInput : TNodeConnector
	{
		[XmlElement(ElementName = "name", Order = 1, IsNullable = true)]
		public string Name { get; set; } = string.Empty;

		[XmlElement(ElementName = "id", Order = 2)]
		public int ID { get; set; }

		[XmlElement(ElementName = "idx", Order = 3)]
		public int IDX { get; set; }

		[XmlElement(ElementName = "link", Order = 4)]
		public List<TNodeLink> Links { get; set; } = new List<TNodeLink>();

		internal TNodeInput() { }

		public TNodeInput(TNodeInput input)
		{
			Name = input.Name;
			ID = input.ID;
			IDX = input.IDX;
			
			//foreach (TNodeLink link in input.Links) { Links.Add(new TNodeLink(link)); };
			//new nodes shouldn't have existing links
		}	
	}
}
