using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BehaviorEditor.MVVM.Model.Starfield
{
	[Serializable]
	[XmlRoot(ElementName = "point")]
	public class TPoint
	{
		[XmlElement(ElementName ="Range", Order =1)]		
		public float Range { get; set; }

		[XmlElement(ElementName = "Weight",Order =2)]
		public float Weight {  get; set; }

		public TPoint() { }

		public TPoint(TPoint point)
		{
			Range = point.Range;
			Weight = point.Weight;
		}
	}

}
