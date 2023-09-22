using BehaviorEditor.Command;
using BehaviorEditor.Extern;
using BehaviorEditor.MVVM.Model.Starfield;
using BehaviorEditor.MVVM.Model.Starfield.Connectors;
using Microsoft.Win32;
using Nodify;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Xml;
using System.Xml.Serialization;

namespace BehaviorEditor.MVVM.ViewModel
{
    public class EditorViewModel : ObservableObject
    {		
		public PendingConnectionViewModel PendingConnection { get; set; }
		private NodeViewModel? selectedNodeViewModel;
		private NodifyObservableCollection<NodeViewModel> selectedNodes = new NodifyObservableCollection<NodeViewModel>();
		private NodifyObservableCollection<NodeViewModel> nodes = new NodifyObservableCollection<NodeViewModel>();
		private NodifyObservableCollection<LinkViewModel> connections = new NodifyObservableCollection<LinkViewModel>();
		

		public NodeViewModel? SelectedNodeViewModel { 
			get => selectedNodeViewModel; 
			set 
			{ 
				SetProperty(ref selectedNodeViewModel, value);
				ShowPropertyExplorer = selectedNodeViewModel != null;
			}  
		}

		public NodifyObservableCollection<NodeViewModel> SelectedNodes { get => selectedNodes; set => SetProperty(ref selectedNodes, value); }
		public NodifyObservableCollection<NodeViewModel> NodeViewModels { get => nodes; set => SetProperty(ref nodes, value); }
		public NodifyObservableCollection<LinkViewModel> ConnectionViewModels { get => connections; set => SetProperty(ref connections, value); }

		public bool ShowPropertyExplorer { get => showPropertyExplorer; set => SetProperty(ref showPropertyExplorer, value); }
		public ICommand DisconnectConnectorCommand { get; }

		public DelegateCommand LoadCommand { get; }
		public DelegateCommand SaveCommand { get; }

		private TNodeCoordinator coordinator { get; set; } = new TNodeCoordinator(new RootContainer());

		private BehaviorDataProvider dataProvider = new BehaviorDataProvider();

		private RootContainer? root;
		private bool showPropertyExplorer = false;

		public EditorViewModel()
		{
			DisconnectConnectorCommand = new DelegateCommand<ConnectorViewModel>(Detach);

			PendingConnection = new PendingConnectionViewModel(this);
			LoadCommand = new DelegateCommand(LoadFile);
			SaveCommand = new DelegateCommand(SaveFile);


			Debug.WriteLine("Init");
		}


		public void Connect(ConnectorViewModel source, ConnectorViewModel target)
		{
			if (source == target) return;
			LinkViewModel? existingLinkVM = ConnectionViewModels.Where(c => c.Source == source).FirstOrDefault();
			LinkViewModel linkVM;
			if (existingLinkVM != null)
			{
				Detach(source);
				
				bool suc1 = existingLinkVM.Target.LinkViewModels.Remove(existingLinkVM);
				bool suc2 = existingLinkVM.Source.LinkViewModels.Remove(existingLinkVM);
				linkVM = new LinkViewModel(existingLinkVM, source, target);
			}
			else
			{
				linkVM = new LinkViewModel(source, target);
			}
			target.LinkViewModels.Add(linkVM);
			source.LinkViewModels.Add(linkVM);
			linkVM.TryAddLink();
			ConnectionViewModels.Add(linkVM);

		}

		public void Detach(ConnectorViewModel connector)
		{
			var linkVM = ConnectionViewModels.First(x => x.Source == connector || x.Target == connector);
			linkVM.Source.IsConnected = linkVM.TryRemoveLink(); 
			linkVM.Target.IsConnected = false;

			bool removed = connector.LinkViewModels.Remove(linkVM);
			ConnectionViewModels.Remove(linkVM);
		}


		public void SetupConnections()
		{
			foreach(var nodeVM in NodeViewModels)
			{
				var nodeConnections = nodeVM.GetLinkViewModels(NodeViewModels);
				foreach(var connection in nodeConnections)
				{
					ConnectionViewModels.Add(connection);
				}
			}
		}


		private void LoadFile()
		{
			NodeViewModels.Clear();
			ConnectionViewModels.Clear();
			SelectedNodes.Clear();
			root = dataProvider.LoadFile();
			coordinator = new TNodeCoordinator(root);
			for (int i = 0; i < root.Nodes.Count; i++)
			{
				TNode node = root.Nodes[i];
				node.Name += i+1;
				var nodeVM = new NodeViewModel(node);
				NodeViewModels.Add(nodeVM);
				//var nestedNodeVMs = nodeVM.GraphViewModelData?.NodeViewModels; //nested node connections are non functional for now
				//if (nestedNodeVMs != null) { foreach (var nestedNodeVM in nestedNodeVMs) { NodeViewModels.Add(nestedNodeVM); } }
			}
			coordinator.ResolveAll();
			foreach (var nodeVM in NodeViewModels)
			{
				nodeVM.GraphViewModelData?.Coordinator.ResolveAll();
			}
			SetupConnections();
		}

		private void SaveFile()
		{
			if (root == null) return;
			coordinator.TranslateAll();
			foreach( var nodeVM in NodeViewModels)
			{
				nodeVM.GraphViewModelData?.Coordinator.TranslateAll();
			}

			dataProvider.SaveFile(root);	
		}
	}
}
