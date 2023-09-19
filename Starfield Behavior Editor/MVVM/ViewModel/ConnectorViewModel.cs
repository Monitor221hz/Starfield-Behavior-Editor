﻿using BehaviorEditor.MVVM.Model.Starfield;
using BehaviorEditor.MVVM.Model.Starfield.Connectors;
using Nodify;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
		public bool IsConnected
		{
			set
			{
				SetProperty(ref isConnected, value);	
			}
			get => isConnected;
		}

		public TNodeConnector Connector {  get; set; }	

		public TNode ParentNode { get; private set; }

		public string Name { get; set; }




		public ConnectorViewModel(TNode node, TNodeConnector connector)
		{
			ParentNode = node;
			Connector = connector;
			Name = connector.Name;
		}

		public void GetViewModels(NodeViewModel nodeVM, List<NodeViewModel> nodes, List<LinkViewModel> linkVMs)
		{



			if (Connector is not TNodeInput) return;

			var input = (TNodeInput)Connector;

			List<LinkViewModel> tempLinkVMs = new List<LinkViewModel>();
			foreach (var link in input.Links)
			{

				var targetNode = nodes[link.NodeID - 1];
				if (targetNode == nodeVM) continue;
				var targetOutput = targetNode.OutputViewModels[link.Output];
				var targetInput = nodeVM.InputViewModels[Connector.IDX];
				tempLinkVMs.Add(new LinkViewModel(targetOutput, targetInput, link));
			}
			foreach (var linkVM in tempLinkVMs)
			{
				linkVM.TryAddLink();
				linkVMs.Add(linkVM);
			}

		}
	}
}