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
        public string Name { get; set; } = string.Empty;

        [XmlElement(ElementName = "collapsed", Order = 2)]
        public string Collapsed { get; set; } = string.Empty;

        [XmlElement(ElementName = "node", Order =3)]
        public List<NodeGroupMember> Members {  get; set; } = new List<NodeGroupMember>();

        internal NodeGroup() { }    

        public NodeGroup(NodeGroup group)
        {
            Name = group.Name;
            Collapsed = group.Collapsed;

            foreach(NodeGroupMember member in group.Members) { Members.Add(new NodeGroupMember(member));}
        }
    }
}
