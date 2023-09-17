using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BehaviorEditor.MVVM.Model.Starfield.Properties
{
    [Serializable]
    [XmlRoot(ElementName = "prop")]
    public class Property
    {


        [XmlElement(ElementName ="type", Order =1)]
        public int Type { get; set; } = 0;

        [XmlElement(ElementName = "value", Order =2)]

        public string RawValue { get; set; } = string.Empty;

        [XmlElement(ElementName = "listindex", Order = 3)]
        public int ListIndex { get; set; } = -1;

        public bool ShouldSerializeListIndex()
        {
            return ListIndex != -1;
        }


    }
}
