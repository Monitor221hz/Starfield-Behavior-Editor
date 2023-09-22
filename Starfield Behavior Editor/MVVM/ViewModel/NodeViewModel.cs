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


namespace BehaviorEditor.MVVM.ViewModel
{


    public class NodeViewModel :  ObservableObject
    {
		const float SPREAD_MULTIPLIER = 2.0f;
		public TNode DataNode { get; private set; }
		private Point location;
		private NodifyObservableCollection<ConnectorViewModel> inputs = new NodifyObservableCollection<ConnectorViewModel>();
		private NodifyObservableCollection<ConnectorViewModel> outputs = new NodifyObservableCollection<ConnectorViewModel>();
		private NodifyObservableCollection<PropertySheetViewModel> propertySheetViewModels = new NodifyObservableCollection<PropertySheetViewModel>();
		private string name = "DefaultNode";

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

		public NodifyObservableCollection<ConnectorViewModel> InputViewModels { get => inputs; set => SetProperty(ref inputs, value); }
		public NodifyObservableCollection<ConnectorViewModel> OutputViewModels { get => outputs; set => SetProperty(ref outputs, value); }

		public GraphViewModel? GraphViewModelData { get; set; }
		public NodifyObservableCollection<PropertySheetViewModel> PropertySheetViewModels { get => propertySheetViewModels; set => SetProperty(ref propertySheetViewModels, value); }
		public NodeViewModel(TNode node)
		{
			DataNode = node;
			Name = node.Name;
			Location = new Point(node.ExpandedPositionX*SPREAD_MULTIPLIER, node.ExpandedPositionY*SPREAD_MULTIPLIER);
			if (node.Graph != null) { GraphViewModelData = new GraphViewModel(node.Graph); }
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
			DataNode = model.DataNode;
			Name = model.Name;
			Location = new Point(model.Location.X + 5.0, model.Location.Y + 5.0);
			if (model.GraphViewModelData != null) { GraphViewModelData = new GraphViewModel(model.GraphViewModelData); }

			foreach(var inputVM in model.InputViewModels) { InputViewModels.Add(new ConnectorViewModel(inputVM)); }
			foreach(var outputVM in model.OutputViewModels) { OutputViewModels.Add(new ConnectorViewModel(outputVM));  }
			foreach(var sheetVM in model.PropertySheetViewModels) { PropertySheetViewModels.Add(new PropertySheetViewModel(sheetVM)); }
		}

		public List<NodeViewModel> GetNestedNodeViewModels() => GraphViewModelData?.NodeViewModels.ToList();
		public List<LinkViewModel> GetLinkViewModels(Collection<NodeViewModel> nodes)
		{
			List<LinkViewModel> linkVMs = new List<LinkViewModel>();


			foreach (var inputVM in InputViewModels)
			{
				inputVM.GetViewModels(this, nodes.ToList(), linkVMs);
			}

			//if (GraphViewModelData != null)
			//{
			//	nodes = GraphViewModelData.NodeViewModels;
			//	foreach (var nodeVM in nodes)
			//	{
			//		linkVMs.AddRange(nodeVM.GetLinkViewModels(GraphViewModelData.NodeViewModels));
			//	}
			//}
			return linkVMs;
		}

	}
}
