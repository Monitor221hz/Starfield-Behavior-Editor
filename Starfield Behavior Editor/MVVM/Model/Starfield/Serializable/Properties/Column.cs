using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BehaviorEditor.MVVM.Model.Starfield.Properties
{
    [Serializable]
    [XmlRoot(ElementName = "column")]
    public class Column
    {
        [XmlElement(ElementName = "header", Order =1)]
        public string Header { get; set; } = string.Empty;


        [XmlElement(ElementName = "types", Order =2)]

        public int Types { get; set; } = 0; 
    }
}
