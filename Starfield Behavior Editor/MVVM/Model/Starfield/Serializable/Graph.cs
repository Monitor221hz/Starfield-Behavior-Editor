using BehaviorEditor.MVVM.Model.Starfield;
using BehaviorEditor.MVVM.Model.Starfield.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BehaviorEditor.MVVM.Model
{
    [Serializable]
    [XmlRoot(ElementName = "graph")]
    public class Graph
    {
        [XmlElement(ElementName = "Link_Style", Order =1)]
        public int LinkStyle { get; set; } = 0;

        [XmlElement(ElementName ="node", Order =2)]
        public List<TNode> Nodes {  get; set; } = new List<TNode>();

		[XmlElement(ElementName = "node_group", Order = 3)]
		public List<NodeGroup> NodeGroups { get; set; } = new List<NodeGroup>();

        [XmlElement(ElementName = "comment", Order = 4)]
        public List<Comment> Comments { get; set; } = new List<Comment>();

        [XmlElement(ElementName ="property_sheet", Order =5)]
        public List<PropertySheet> PropertySheets { get; set; } = new List<PropertySheet>();

        internal Graph() { }

        public Graph(Graph graph)
        {
            LinkStyle = graph.LinkStyle;
            foreach(TNode node in graph.Nodes) { Nodes.Add(new TNode(node));  }
            foreach(NodeGroup nodeGroup in graph.NodeGroups) {  NodeGroups.Add(new NodeGroup(nodeGroup)); }
            foreach(Comment comment in graph.Comments) { Comments.Add(new Comment(comment)); }
            foreach(PropertySheet propertySheet in graph.PropertySheets) { PropertySheets.Add(new PropertySheet(propertySheet)); }
        }



        
    }
}
