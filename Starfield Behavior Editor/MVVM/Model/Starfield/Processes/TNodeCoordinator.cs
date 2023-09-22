using BehaviorEditor.MVVM.Model.Starfield.Connectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviorEditor.MVVM.Model.Starfield
{
	public class TNodeCoordinator
	{
		private List<TNode> nodeList;
		public TNodeCoordinator(List<TNode> NodeList) => nodeList = NodeList;
		public TNodeCoordinator(Graph graph) => nodeList = graph.Nodes;

		public TNodeCoordinator(RootContainer root) => nodeList = root.Nodes;

		public void ResolveAll()
		{
			foreach(var node in nodeList)
			{
				ResolveNode(node);
			}
		}


		public void ResolveNode(TNode node)
		{
			foreach(var input in node.Inputs)
			{
				ResolveInput(input);
			}
		}
		public void ResolveInput(TNodeInput input)
		{
			foreach(var link in input.Links)
			{
				link.ConnectedNode = nodeList[link.NodeID-1];
			}
		}
		public void TranslateAll()
		{
			foreach(var node in nodeList)
			{
				TranslateNode(node);
			}
		}
		public void TranslateNode(TNode node)
		{
			foreach(var input in node.Inputs)
			{
				TranslateInput(input);
			}
		}
		public void TranslateInput(TNodeInput input)
		{
			foreach(var link in input.Links)
			{
				link.NodeID = nodeList.IndexOf(link.ConnectedNode)+1;
			}
		}

		
	}
}
