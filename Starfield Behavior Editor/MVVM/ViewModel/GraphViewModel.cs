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
		private NodeViewModel parentNodeViewModel;

		public TNodeCoordinator Coordinator { get; private set; }

		public Graph DataGraph { get => dataGraph; private set => SetProperty(ref dataGraph, value);  }

		public NodeViewModel ParentNodeViewModel { get => parentNodeViewModel; set => SetProperty(ref parentNodeViewModel, value); }

		public NodifyObservableCollection<PropertySheetViewModel> PropertySheetViewModels { get => propertySheetViewModels; set => SetProperty(ref propertySheetViewModels, value); }
		public NodifyObservableCollection<NodeViewModel> NodeViewModels { get => nodeViewModels; set => SetProperty(ref nodeViewModels, value); }

		public DelegateCommand CallChildrenCommand { get;  }

		public bool HasChildren => true;


		internal GraphViewModel(Graph graph, NodeViewModel parentNodeVM)
		{
			CallChildrenCommand = new DelegateCommand(CallChildNodeViewModels);
			ParentNodeViewModel = parentNodeVM;
			DataGraph = graph;
			Coordinator = new TNodeCoordinator(graph);
			foreach(var sheet in DataGraph.PropertySheets)
			{
				PropertySheetViewModels.Add(new PropertySheetViewModel(sheet));
			}
			for (int i = 0; i < DataGraph.Nodes.Count; i++)
			{
				TNode node = DataGraph.Nodes[i];
				NodeViewModel childNode = node.Graph == null ? new NodeViewModel(node) : new EmbeddedNodeViewModel(node);
				
				NodeViewModels.Add(childNode);
			}
		}

		public GraphViewModel(GraphViewModel model, NodeViewModel parentNodeVM)
		{
			CallChildrenCommand = new DelegateCommand(CallChildNodeViewModels);
			ParentNodeViewModel = parentNodeVM;
			DataGraph = new Graph(model.DataGraph);
			Coordinator = new TNodeCoordinator(DataGraph);
			foreach (var sheet in DataGraph.PropertySheets)
			{
				PropertySheetViewModels.Add(new PropertySheetViewModel(sheet));
			}
			for (int i = 0; i < DataGraph.Nodes.Count; i++)
			{
				TNode node = DataGraph.Nodes[i];
				node.Name = $"{parentNodeVM.Name}_Child{i + 1}_{node.Name}";
				NodeViewModel childNode = node.Graph == null ? new NodeViewModel(node) : new EmbeddedNodeViewModel(node);

				NodeViewModels.Add(childNode);
			}
		}

		public void CallChildNodeViewModels()
		{
			for (int i = 0; i < NodeViewModels.Count; i++)
			{
				NodeViewModel nodeVM = NodeViewModels[i];
				nodeVM.Location = new System.Windows.Point(ParentNodeViewModel.Location.X + i * 10.0, ParentNodeViewModel.Location.Y + i * 10.0);
			}
			
		}

		
	}
}
