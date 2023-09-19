using BehaviorEditor.MVVM.Model.Starfield.Connectors;
using BehaviorEditor.MVVM.Model.Starfield.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BehaviorEditor.MVVM.Model.Starfield
{
	[Serializable]
	[XmlRoot(ElementName = "node")]
	public partial class TNode
    {
		[XmlElement(ElementName = "default_state", Order =1)]
		public string DefaultState { get; set; } //bool nullable
		public bool ShouldSerializeDefaultState() => !string.IsNullOrWhiteSpace(DefaultState);

		[XmlElement(ElementName = "node_type", Order =2)]
		public string NodeType { get; set; }

		[XmlElement(ElementName = "noninstanced", Order = 3)]
		public string NonInstanced { get; set; }

		[XmlElement(ElementName = "divisions", Order = 4)]
		public int Divisions { get; set; } = -1;

		public bool ShouldSerializeDivisions() => Divisions != -1;

		[XmlElement(ElementName = "blendInput", Order = 5)]
		public List<BlendInput> BlendInputs { get; set; }	
		//public bool ShouldSerializeBlendInput() => BlendInput != null;

		[XmlArray(ElementName = "flags", Order = 6, IsNullable = true)]
		[XmlArrayItem(ElementName = "flag", IsNullable = true)]
		public List<string> Flags { get; set; }

		public bool ShouldSerializeFlags()
		{
			return Flags.Count > 0; 
		}

		[XmlElement(ElementName = "name", Order =7)]
		public string Name { get; set; }

		[XmlElement(ElementName = "pos_x", Order =8)]
		public float PositionX { get; set; }

		[XmlElement(ElementName = "pos_y", Order =9)]
		public float PositionY { get; set; }

		[XmlElement(ElementName = "expanded_pos_x", Order =10)]
		public float ExpandedPositionX { get; set; }

		[XmlElement(ElementName = "expanded_pos_y", Order =11)]
		public float ExpandedPositionY { get; set; }

		[XmlElement(ElementName = "use_color_2", Order =12)]
		public string UseColor2 { get; set; }

		[XmlElement(ElementName = "user_id", Order =13)]
		public int UserID { get; set; }

		[XmlElement(ElementName = "collapsed", Order =14)]
		public string Collapsed { get; set; }

		[XmlElement(ElementName = "guid", Order =15)]
		public string GUID { get; set; }

		[XmlElement(ElementName = "input", Order = 16)]
		public List<TNodeInput> Inputs = new List<TNodeInput>();
		[XmlElement(ElementName ="output", Order =17)]
		public List<TNodeOutput> Outputs = new List<TNodeOutput>();



		[XmlElement(ElementName ="property_sheet", Order =18)]
		public List<PropertySheet> PropertySheets = new List<PropertySheet>();

		[XmlElement(ElementName ="graph", Order =19)]
		public Graph Graph { get; set; }
	}
}
