using BehaviorEditor.MVVM.Model.Starfield;
using BehaviorEditor.MVVM.Model.Starfield.Connectors;
using Nodify;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace BehaviorEditor.MVVM.ViewModel
{
    public class ConnectorViewModel : ObservableObject, INotifyPropertyChanged
    {

		private Point anchor;
		public Point Anchor
		{
			set
			{
				SetProperty(ref anchor, value);
			}
			get => anchor;
		}

		private bool isConnected;
		private NodifyObservableCollection<LinkViewModel> linkViewModels = new NodifyObservableCollection<LinkViewModel>();
		private string name;

		public bool IsConnected
		{
			set
			{
				SetProperty(ref isConnected, value);	
			}
			get => isConnected;
		}

		public TNodeConnector Connector {  get; set; }	

		public NodeViewModel ParentNodeViewModel { get; private set; }

		public string Name { 
			get => name;
			set 
			{
				SetProperty(ref name, value);
				Connector.Name = name;
			}
		}

		public NodifyObservableCollection<LinkViewModel> LinkViewModels { get => linkViewModels; set => SetProperty(ref linkViewModels, value); }

		public DelegateCommand RemoveThisCommand { get; }
		public ConnectorViewModel(NodeViewModel nodeVM, TNodeConnector connector)
		{
			RemoveThisCommand = new DelegateCommand(RemoveThis);
			ParentNodeViewModel = nodeVM;
			Connector = connector;
			Name = connector.Name;
			if (string.IsNullOrWhiteSpace(Name)) Name = "-";
		}

		public ConnectorViewModel(ConnectorViewModel connectorViewModel)
		{
			RemoveThisCommand = new DelegateCommand(RemoveThis);
			ParentNodeViewModel = connectorViewModel.ParentNodeViewModel;
			
			switch (connectorViewModel.Connector)
			{
				case TNodeInput input:
					Connector = new TNodeInput(input);
					break;
				case TNodeOutput output:
					Connector = new TNodeOutput(output);
					break;
				default:
					Connector = connectorViewModel.Connector;
					break;
			}


			Name = connectorViewModel.Name;
		}
		public void RemoveThis()
		{
			ParentNodeViewModel?.RemoveConnector(this);
		}
		public void GetViewModels(NodeViewModel nodeVM, List<NodeViewModel> nodes, List<LinkViewModel> linkVMs)
		{
			if (Connector is not TNodeInput) return;

			var input = (TNodeInput)Connector;

			List<LinkViewModel> tempLinkVMs = new List<LinkViewModel>();
			foreach (var link in input.Links)
			{

				var targetNode = nodes[link.NodeID - 1];
				

				var targetOutput = targetNode.OutputViewModels[link.Output];
				var targetInput = nodeVM.InputViewModels[Connector.IDX];
				var linkVM = new LinkViewModel(targetOutput, targetInput, link);
				LinkViewModels.Add(linkVM);
				tempLinkVMs.Add(linkVM);
			}
			foreach (var linkVM in tempLinkVMs)
			{
				linkVM.TryAddLink();
				linkVMs.Add(linkVM);
			}

		}
	}
}
