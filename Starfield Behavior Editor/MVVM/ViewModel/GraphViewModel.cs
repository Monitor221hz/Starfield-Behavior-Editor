using BehaviorEditor.MVVM.Model;
using BehaviorEditor.MVVM.Model.Starfield;
using Nodify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviorEditor.MVVM.ViewModel
{
	public class GraphViewModel : ObservableObject
	{
		private Graph dataGraph;
		private NodifyObservableCollection<PropertySheetViewModel> propertySheetViewModels = new NodifyObservableCollection<PropertySheetViewModel>();
		private NodifyObservableCollection<NodeViewModel> nodeViewModels = new NodifyObservableCollection<NodeViewModel>();

		public TNodeCoordinator Coordinator { get; private set; }

		public Graph DataGraph { get => dataGraph; private set => SetProperty(ref dataGraph, value);  }

		public NodifyObservableCollection<PropertySheetViewModel> PropertySheetViewModels { get => propertySheetViewModels; set => SetProperty(ref propertySheetViewModels, value); }
		public NodifyObservableCollection<NodeViewModel> NodeViewModels { get => nodeViewModels; set => SetProperty(ref nodeViewModels, value); }

		internal GraphViewModel(Graph graph, string parentName)
		{
			DataGraph = graph;
			Coordinator = new TNodeCoordinator(graph);
			foreach(var sheet in DataGraph.PropertySheets)
			{
				PropertySheetViewModels.Add(new PropertySheetViewModel(sheet));
			}
			for (int i = 0; i < DataGraph.Nodes.Count; i++)
			{
				TNode node = DataGraph.Nodes[i];
				NodeViewModels.Add(new NodeViewModel(node));
			}
		}

		public GraphViewModel(GraphViewModel model, string parentName)
		{
			DataGraph = new Graph(model.DataGraph);
			Coordinator = new TNodeCoordinator(DataGraph);
			foreach (var sheet in DataGraph.PropertySheets)
			{
				PropertySheetViewModels.Add(new PropertySheetViewModel(sheet));
			}
			for (int i = 0; i < DataGraph.Nodes.Count; i++)
			{
				TNode node = DataGraph.Nodes[i];
				node.Name = $"{parentName}_Child{i + 1}_{node.Name}";
				NodeViewModels.Add(new NodeViewModel(node));
			}
		}

		
	}
}
