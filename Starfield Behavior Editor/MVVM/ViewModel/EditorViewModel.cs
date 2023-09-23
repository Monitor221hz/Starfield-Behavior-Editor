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
using System.Windows;
namespace BehaviorEditor.MVVM.ViewModel
{
    public class EditorViewModel : ObservableObject
    {		
		public PendingConnectionViewModel PendingConnection { get; set; }
		private NodeViewModel? selectedNodeViewModel;
		private NodifyObservableCollection<NodeViewModel> selectedNodes = new NodifyObservableCollection<NodeViewModel>();
		private NodifyObservableCollection<NodeViewModel> displayNodeViewModels = new NodifyObservableCollection<NodeViewModel>();
		private NodifyObservableCollection<LinkViewModel> connections = new NodifyObservableCollection<LinkViewModel>();
		

		public NodeViewModel? SelectedNodeViewModel { 
			get => selectedNodeViewModel; 
			set 
			{ 
				SetProperty(ref selectedNodeViewModel, value);
				ShowPropertyExplorer = selectedNodeViewModel != null;
			}  
		}
		private List<NodeViewModel> nodeViewModels { get; set; } = new List<NodeViewModel>();
		public NodifyObservableCollection<NodeViewModel> SelectedNodes { get => selectedNodes; set => SetProperty(ref selectedNodes, value); }
		public NodifyObservableCollection<NodeViewModel> DisplayNodeViewModels { get => displayNodeViewModels; set => SetProperty(ref displayNodeViewModels, value); }
		public NodifyObservableCollection<LinkViewModel> ConnectionViewModels { get => connections; set => SetProperty(ref connections, value); }
		
		private Point viewPortLocation;
		public Point ViewPortLocation
		{
			set
			{
				SetProperty(ref viewPortLocation, value);
			}
			get => viewPortLocation;
		}

		public bool ShowPropertyExplorer { get => showPropertyExplorer; set => SetProperty(ref showPropertyExplorer, value); }
		public ICommand DisconnectConnectorCommand { get; }

		public DelegateCommand LoadCommand { get; }
		public DelegateCommand SaveCommand { get; }

		public DelegateCommand CopyNodeCommand { get; }	
		public DelegateCommand PasteNodeCommand { get; }

		private TNodeCoordinator coordinator { get; set; } = new TNodeCoordinator(new RootContainer());

		private BehaviorDataProvider dataProvider = new BehaviorDataProvider();

		private RootContainer? root;
		private bool showPropertyExplorer = false;

		public EditorViewModel()
		{
			CopyNodeCommand = new DelegateCommand(CopyNode);
			PasteNodeCommand = new DelegateCommand(PasteNode);
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
			foreach(var nodeVM in nodeViewModels)
			{
				var nodeConnections = nodeVM.GetLinkViewModels(nodeViewModels);
				foreach(var connection in nodeConnections)
				{
					ConnectionViewModels.Add(connection);
				}
			}
		}
		public void AppendNodeViewModel(NodeViewModel nodeVM, bool PanToNode)
		{
			
			DisplayNodeViewModels.Add(nodeVM);
			if (SelectedNodeViewModel?.GraphViewModelData != null)
			{
				SelectedNodeViewModel.GraphViewModelData.NodeViewModels.Add(nodeVM);
				SelectedNodeViewModel.GraphViewModelData.DataGraph.Nodes.Add(nodeVM.DataNode);
				return;
			}
			nodeViewModels.Add(nodeVM);
			root!.Nodes.Add(nodeVM.DataNode);
		}


		public void PasteNode()
		{
			ClipboardViewModel.Paste(this);
		}
		public void CopyNode()
		{
			if (SelectedNodeViewModel == null) return;
			ClipboardViewModel.Copy(SelectedNodeViewModel);
		}


		private void DisplayNestedNodes(NodeViewModel nodeVM)
		{
			var nestedNodeVMs = nodeVM.GraphViewModelData?.NodeViewModels;
			if (nestedNodeVMs != null) 
			{
				foreach (var nestedNodeVM in nestedNodeVMs) 
				{ 
					DisplayNodeViewModels.Add(nestedNodeVM); 
					DisplayNestedNodes(nestedNodeVM);
				} 
			}
		}
		private void LoadFile()
		{
			nodeViewModels.Clear();
			DisplayNodeViewModels.Clear();
			ConnectionViewModels.Clear();
			SelectedNodes.Clear();
			root = dataProvider.LoadFile();
			coordinator = new TNodeCoordinator(root);
			for (int i = 0; i < root.Nodes.Count; i++)
			{
				TNode node = root.Nodes[i];
				var nodeVM = new NodeViewModel(node);
				DisplayNodeViewModels.Add(nodeVM);
				nodeViewModels.Add(nodeVM);
				DisplayNestedNodes(nodeVM);
			}
			coordinator.ResolveAll();
			foreach (var nodeVM in DisplayNodeViewModels)
			{
				nodeVM.GraphViewModelData?.Coordinator.ResolveAll();
			}
			SetupConnections();
		}

		private void SaveFile()
		{
			if (root == null) return;
			coordinator.TranslateAll();
			foreach( var nodeVM in DisplayNodeViewModels)
			{
				nodeVM.GraphViewModelData?.Coordinator.TranslateAll();
			}

			dataProvider.SaveFile(root);	
		}
	}
}
