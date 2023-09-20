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
    public class TNodeOutput : TNodeConnector
    {
        [XmlElement(ElementName = "name", Order =1)]
        public string Name { get; set; } = string.Empty;

        [XmlElement(ElementName = "id", Order =2)]
        public int ID { get; set; }

		[XmlElement(ElementName = "idx", Order =3)]
		public int IDX { get; set; }

        internal TNodeOutput() { }


        public TNodeOutput(TNodeOutput output)
        {
            Name = output.Name;
            ID = output.ID;
            IDX = output.IDX;
        }
    }
}
