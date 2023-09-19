using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BehaviorEditor.MVVM.Model.Starfield
{
    [Serializable]
    [XmlRoot(ElementName = "node_group")]
    public class NodeGroup
    {
        [XmlElement(ElementName = "name", Order =1)]
        public string Name { get; set; }

        [XmlElement(ElementName = "collapsed", Order =2)]
        public string Collapsed { get; set; }

        [XmlElement(ElementName = "node", Order =3)]
        public List<NodeGroupMember> Members {  get; set; }

        
    }
}
