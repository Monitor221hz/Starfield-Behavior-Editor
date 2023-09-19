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
		private NodeViewModel? selectedNode;
		private NodifyObservableCollection<NodeViewModel> selectedNodes = new NodifyObservableCollection<NodeViewModel>();
		private NodifyObservableCollection<NodeViewModel> nodes = new NodifyObservableCollection<NodeViewModel>();
		private NodifyObservableCollection<LinkViewModel> connections = new NodifyObservableCollection<LinkViewModel>();
		

		public NodeViewModel? SelectedNode { 
			get => selectedNode; 
			set 
			{ 
				SetProperty(ref selectedNode, value);
				ShowPropertyExplorer = selectedNode != null;
			}  
		}

		public NodifyObservableCollection<NodeViewModel> SelectedNodes { get => selectedNodes; set => SetProperty(ref selectedNodes, value); }
		public NodifyObservableCollection<NodeViewModel> Nodes { get => nodes; set => SetProperty(ref nodes, value); }
		public NodifyObservableCollection<LinkViewModel> Connections { get => connections; set => SetProperty(ref connections, value); }

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
			var linkVM = new LinkViewModel(source, target);
			linkVM.TryAddLink();
			Connections.Add(linkVM);

		}

		public void Detach(ConnectorViewModel connector)
		{
			var linkVM = Connections.First(x => x.Source == connector || x.Target == connector);
			linkVM.Source.IsConnected = linkVM.TryRemoveLink(); 
			linkVM.Target.IsConnected = false;
			Connections.Remove(linkVM);


		}


		public void SetupConnections()
		{
			foreach(var nodeVM in Nodes)
			{
				var nodeConnections = nodeVM.GetLinkViewModels(Nodes);
				foreach(var connection in nodeConnections)
				{
					Connections.Add(connection);
				}
			}
		}


		private void LoadFile()
		{
			Nodes.Clear();
			Connections.Clear();
			SelectedNodes.Clear();
			root = dataProvider.LoadFile();
			coordinator = new TNodeCoordinator(root);
			for (int i = 0; i < root.Nodes.Count; i++)
			{
				Model.Starfield.TNode? node = root.Nodes[i];
				node.Name += i+1;
				Nodes.Add(new NodeViewModel(node));
			}
			coordinator.ResolveAll();
			SetupConnections();
		}

		private void SaveFile()
		{
			if (root == null) return;
			coordinator.TranslateAll();


			dataProvider.SaveFile(root);	
		}
	}
}
