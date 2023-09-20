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
    [XmlRoot(ElementName = "root")]
    public class RootContainer
    {
        [XmlElement(ElementName = "Name", Order = 1)]
        public string Name { get; set; } = "NewRoot";

        [XmlElement(ElementName = "Category", Order = 2)]
        public string Category { get; set; } = "Default";

        [XmlElement(ElementName = "Link_Style", Order =3)]
        public int LinkStyle { get; set; }

        [XmlElement(ElementName = "node", Order =4)]
        public List<TNode> Nodes { get; set; }   = new List<TNode>();

        [XmlElement(ElementName = "node_group", Order =5)]
        public List<NodeGroup> NodeGroups { get; set; } = new List<NodeGroup>();

        [XmlElement(ElementName = "comment", Order = 6)]
        public List<Comment> Comments { get; set; } = new List<Comment>();

        [XmlElement(ElementName = "property_sheet", Order = 7)]
        public List<PropertySheet> PropertySheets { get; set; } = new List<PropertySheet>();

        internal RootContainer() { }

        public RootContainer(RootContainer container)
        {
            Name = container.Name;
            Category = container.Category;
            LinkStyle = container.LinkStyle;
            foreach(TNode node in container.Nodes) {  Nodes.Add(new TNode(node)); }
            foreach(NodeGroup nodeGroup in container.NodeGroups) { NodeGroups.Add(new NodeGroup(nodeGroup));  }
            foreach(Comment nodeComment in container.Comments) { Comments.Add(new Comment(nodeComment)); }
            foreach(PropertySheet propertySheet in container.PropertySheets) { PropertySheets.Add(new PropertySheet(propertySheet)); }
        }
        
    }
}
