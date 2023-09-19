using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BehaviorEditor.MVVM.Model.Starfield
{
	[Serializable]
	[XmlRoot(ElementName = "blendInput")]
	public class BlendInput
	{
		[XmlElement("point", Order =1)]
		public List<TPoint> Points { get; set; }
	}
}
