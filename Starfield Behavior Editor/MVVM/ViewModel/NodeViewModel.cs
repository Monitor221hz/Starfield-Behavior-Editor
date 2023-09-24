using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Windows;
using BehaviorEditor.MVVM.Model.Starfield;
using BehaviorEditor.MVVM.Model.Starfield.Properties;
using Nodify;
using BehaviorEditor.MVVM.Model.Starfield.Connectors;
using static Nodify.EditorGestures;
using System.Xml.Linq;


namespace BehaviorEditor.MVVM.ViewModel
{


    public class NodeViewModel :  ObservableObject
    {
		const float SPREAD_MULTIPLIER = 3.0f;
		public TNode DataNode { get; private set; }
		private Point location;
		private NodifyObservableCollection<ConnectorViewModel> inputs = new NodifyObservableCollection<ConnectorViewModel>();
		private NodifyObservableCollection<ConnectorViewModel> outputs = new NodifyObservableCollection<ConnectorViewModel>();
		private NodifyObservableCollection<PropertySheetViewModel> propertySheetViewModels = new NodifyObservableCollection<PropertySheetViewModel>();
		private string name = "DefaultNode";
		private string nodeType = "NONE";
		private GraphViewModel? graphViewModelData;

		public Point Location
		{
			set
			{
				SetProperty(ref location, value);
				DataNode.ExpandedPositionX = (float)location.X/SPREAD_MULTIPLIER;
				DataNode.ExpandedPositionY = (float)location.Y/SPREAD_MULTIPLIER;
			}
			get => location;
		}


		public string Name { 
			get => name; 
			set 
			{ 
				SetProperty(ref name, value);
				DataNode.Name = name;
			} 
		}
		public string NodeType
		{
			get => nodeType;
			set
			{
				SetProperty(ref nodeType, value);
				DataNode.NodeType = value;
			}
		}

		public bool ShowPropertyExplorer => true;
		public DelegateCommand AddInputCommand { get; }
		public DelegateCommand RemoveInputCommand { get; }	
		public DelegateCommand AddOutputCommand { get; }

		public DelegateCommand CopyThisCommand { get; } 

		public DelegateCommand RemoveOutputCommand { get; }
		public NodifyObservableCollection<ConnectorViewModel> InputViewModels { get => inputs; set => SetProperty(ref inputs, value); }
		public NodifyObservableCollection<ConnectorViewModel> OutputViewModels { get => outputs; set => SetProperty(ref outputs, value); }

		public GraphViewModel GraphViewModelData { get => graphViewModelData; set => SetProperty(ref graphViewModelData, value); }


		public NodifyObservableCollection<PropertySheetViewModel> PropertySheetViewModels { get => propertySheetViewModels; set => SetProperty(ref propertySheetViewModels, value); }
		
		public void AppendPropertySheet(PropertySheetViewModel propertySheetViewModel)
		{
			PropertySheetViewModels.Add(propertySheetViewModel);
			DataNode.PropertySheets.Add(propertySheetViewModel.PropertySheetData);
		}
		
		
		public NodeViewModel(TNode node)
		{
			AddInputCommand = new DelegateCommand(AddNewInput);
			RemoveInputCommand = new DelegateCommand(RemoveLastInput);
			AddOutputCommand = new DelegateCommand(AddNewOutput);
			RemoveOutputCommand = new DelegateCommand(RemoveLastOutput);
			CopyThisCommand = new DelegateCommand(CopyThis);
			DataNode = node;
			Name = node.Name;
			NodeType = node.NodeType;
			Location = new Point(node.ExpandedPositionX*SPREAD_MULTIPLIER, node.ExpandedPositionY*SPREAD_MULTIPLIER);
			if (node.Graph != null) { GraphViewModelData = new GraphViewModel(node.Graph, this); }
			foreach (TNodeInput input in node.Inputs)
			{
				InputViewModels.Add(new ConnectorViewModel(this, input));
			}
			foreach (TNodeOutput output in node.Outputs)
			{
				OutputViewModels.Add(new ConnectorViewModel(this, output));
			}
			foreach (PropertySheet sheet in node.PropertySheets)
			{
				PropertySheetViewModels.Add(new PropertySheetViewModel(sheet));
			}

		}
		public NodeViewModel(NodeViewModel model)
		{
			AddInputCommand = new DelegateCommand(AddNewInput);
			RemoveInputCommand = new DelegateCommand(RemoveLastInput);
			AddOutputCommand = new DelegateCommand(AddNewOutput);
			RemoveOutputCommand = new DelegateCommand(RemoveLastOutput);
			CopyThisCommand = new DelegateCommand(CopyThis);
			DataNode = new TNode(model.DataNode);
			Name = model.Name;
			NodeType = model.NodeType;
			Location = new Point(model.Location.X + 10.0*SPREAD_MULTIPLIER, model.Location.Y + 10.0*SPREAD_MULTIPLIER);
			
			if (DataNode.Graph != null) { GraphViewModelData = new GraphViewModel(DataNode.Graph, this); }
			foreach (TNodeInput input in DataNode.Inputs)
			{
				InputViewModels.Add(new ConnectorViewModel(this, input));
			}
			foreach (TNodeOutput output in DataNode.Outputs)
			{
				OutputViewModels.Add(new ConnectorViewModel(this, output));
			}
			foreach (PropertySheet sheet in DataNode.PropertySheets)
			{
				PropertySheetViewModels.Add(new PropertySheetViewModel(sheet));
			}

		}

		public List<NodeViewModel> GetNestedNodeViewModels() => GraphViewModelData?.NodeViewModels.ToList();
		public List<LinkViewModel> GetLinkViewModels(List<NodeViewModel> nodes)
		{
			List<LinkViewModel> linkVMs = new List<LinkViewModel>();


			foreach (var inputVM in InputViewModels)
			{
				inputVM.GetViewModels(this, nodes, linkVMs);
			}

			if (GraphViewModelData != null)
			{
				nodes = GraphViewModelData.NodeViewModels.ToList();
				foreach (var nodeVM in nodes)
				{
					linkVMs.AddRange(nodeVM.GetLinkViewModels(nodes));
				}
			}
			return linkVMs;
		}
		public void CopyThis()
		{
			ClipboardViewModel.Copy(this);
		}
		public void AddNewOutput()
		{
			var output = new TNodeOutput() { ID = OutputViewModels.Count + InputViewModels.Count + 1, IDX = OutputViewModels.Count };
			OutputViewModels.Add(new ConnectorViewModel(this, output));
			DataNode.Outputs.Add(output);
		}
		public void AddNewInput()
		{
			var input = new TNodeInput() { ID = OutputViewModels.Count + InputViewModels.Count + 1, IDX = InputViewModels.Count };
			InputViewModels.Add(new ConnectorViewModel(this, input));
			DataNode.Inputs.Add(input);
		}
		public void RemoveConnector(ConnectorViewModel connectorVM)
		{
			if (connectorVM.LinkViewModels.Count > 0 || connectorVM.IsConnected) return;
			if (!InputViewModels.Remove(connectorVM)) { OutputViewModels.Remove(connectorVM); }
			switch (connectorVM.Connector)
			{
				case TNodeInput input:
					DataNode.Inputs.Remove(input);
					break;
				case TNodeOutput output:
					DataNode.Outputs.Remove(output);
					break;
				default:
					break;
			}
		}
		public void RemoveLastInput()
		{
			var inputVM = InputViewModels.Last();
			if (inputVM.LinkViewModels.Count > 0 || inputVM.IsConnected) return;
			InputViewModels.Remove(inputVM);
			DataNode.Inputs.Remove((TNodeInput)inputVM.Connector);
		}
		public void RemoveLastOutput()
		{
			var outputVM = OutputViewModels.Last();
			if (outputVM.LinkViewModels.Count > 0 || outputVM.IsConnected) return;
			OutputViewModels.Remove(outputVM);
			DataNode.Outputs.Remove((TNodeOutput)outputVM.Connector);
		}
	}
}
