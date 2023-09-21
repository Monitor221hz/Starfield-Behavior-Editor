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

		
		public NodifyObservableCollection<PropertySheetViewModel> PropertySheetViewModels { get => propertySheetViewModels; set => SetProperty(ref propertySheetViewModels, value); }
		public NodeViewModel(TNode node)
		{
			DataNode = node;
			Name = node.Name;
			Location = new Point(node.ExpandedPositionX*SPREAD_MULTIPLIER, node.ExpandedPositionY*SPREAD_MULTIPLIER);

			foreach (TNodeInput input in node.Inputs)
			{
				InputViewModels.Add(new ConnectorViewModel(node, input));
			}
			foreach (TNodeOutput output in node.Outputs)
			{
				OutputViewModels.Add(new ConnectorViewModel(node, output));
			}
			foreach (PropertySheet sheet in node.PropertySheets)
			{
				PropertySheetViewModels.Add(new PropertySheetViewModel(sheet));
			}

		}

		public List<LinkViewModel> GetLinkViewModels(Collection<NodeViewModel> nodes)
		{
			List<LinkViewModel> linkVMs = new List<LinkViewModel>();
			foreach(var inputVM in InputViewModels)
			{
				inputVM.GetViewModels(this, nodes.ToList(), linkVMs);
			}
			return linkVMs;
		}

	}
}
