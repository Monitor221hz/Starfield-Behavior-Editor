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
    public class NodeGroupMember
    {
        [XmlElement(ElementName = "idx", Order =1)]
        public int IDX { get; set;  }

        internal NodeGroupMember() { }

        public NodeGroupMember(NodeGroupMember nodeGroupMember)
        {
            IDX = nodeGroupMember.IDX;
        }
    }
}
