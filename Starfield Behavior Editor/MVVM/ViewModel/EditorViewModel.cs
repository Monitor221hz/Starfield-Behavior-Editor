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
		private NodifyObservableCollection<ConnectionViewModel> connections = new NodifyObservableCollection<ConnectionViewModel>();
		

		public NodeViewModel? SelectedNode { 
			get => selectedNode; 
			set 
			{ 
				SetProperty(ref selectedNode, value);
				ShowPropertyExplorer = selectedNode != null;
				if (selectedNode != null) SelectedDataNode = selectedNode.DataNode;
			}  
		}
		public TNode SelectedDataNode { 
			get => selectedDataNode; 
			set 
			{
				SetProperty(ref selectedDataNode, value);
			}
		}
		public NodifyObservableCollection<NodeViewModel> SelectedNodes { get => selectedNodes; set => SetProperty(ref selectedNodes, value); }
		public NodifyObservableCollection<NodeViewModel> Nodes { get => nodes; set => SetProperty(ref nodes, value); }
		public NodifyObservableCollection<ConnectionViewModel> Connections { get => connections; set => SetProperty(ref connections, value); }

		public bool ShowPropertyExplorer { get => showPropertyExplorer; set => SetProperty(ref showPropertyExplorer, value); }
		public ICommand DisconnectConnectorCommand { get; }

		public DelegateCommand LoadCommand { get; }
		public DelegateCommand SaveCommand { get; }



		private BehaviorDataProvider dataProvider = new BehaviorDataProvider();

		private RootContainer? root;
		private bool showPropertyExplorer = false;
		private TNode selectedDataNode;

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
			Connections.Add(new ConnectionViewModel(source, target));
		}

		public void Detach(ConnectorViewModel connector)
		{
			var connection = Connections.First(x => x.Source == connector || x.Target == connector);
			connection.Source.IsConnected = false;  // This is not correct if there are multiple connections to the same connector
			connection.Target.IsConnected = false;
			Connections.Remove(connection);
		}


		public void SetupConnections()
		{
			foreach(var nodeVM in Nodes)
			{
				DisplayNodeConnections(nodeVM);
			}
		}

		public void DisplayNodeConnections(NodeViewModel nodeVM)
		{
			foreach(var input in nodeVM.DataNode.Inputs)
			{
				DisplayLinks(nodeVM, input);
			}
		}

		public void DisplayLinks(NodeViewModel nodeVM, TNodeInput nodeInput)
		{
			
			foreach(var link in nodeInput.Links)
			{
				var targetNode = Nodes[link.NodeID-1];
				if (targetNode == nodeVM) continue;
				var targetOutput = targetNode.Outputs[link.Output];
				var targetInput = nodeVM.Inputs[nodeInput.IDX];
				Connections.Add(new ConnectionViewModel( targetOutput, targetInput));
			}
		}


		private void LoadFile()
		{
			Nodes.Clear();
			Connections.Clear();
			SelectedNodes.Clear();
			root = dataProvider.LoadFile();

			for (int i = 0; i < root.Nodes.Count; i++)
			{
				Model.Starfield.TNode? node = root.Nodes[i];
				node.Name += i+1;
				Nodes.Add(new NodeViewModel(node));
			}
			SetupConnections();
		}

		private void SaveFile()
		{
			if (root == null) return; 

			dataProvider.SaveFile(root);	
		}
	}
}
