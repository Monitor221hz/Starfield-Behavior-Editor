using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BehaviorEditor.MVVM.Model.Starfield;
using BehaviorEditor.MVVM.Model.Starfield.Connectors;
using Nodify;

namespace BehaviorEditor.MVVM.ViewModel
{
	public class LinkViewModel : ObservableObject
	{
		private NodifyObservableCollection<PropertySheetViewModel> propertySheetViewModels = new NodifyObservableCollection<PropertySheetViewModel> ();


		public TNodeLink Link { get; private set; }
		public NodifyObservableCollection<PropertySheetViewModel> PropertySheetViewModels { get => propertySheetViewModels; set => SetProperty(ref propertySheetViewModels, value); }

		
		public LinkViewModel(ConnectorViewModel source, ConnectorViewModel target)
		{
			Source = source;
			Target = target;

			Source.IsConnected = true;
			Target.IsConnected = true;
			Link = new TNodeLink() { ConnectedNode = target.ParentNodeViewModel.DataNode, Output = source.Connector.IDX };

		}
		public LinkViewModel(ConnectorViewModel source, ConnectorViewModel target, TNodeLink nodeLink)
		{
			Source = source;
			Target = target;

			Source.IsConnected = true;
			Target.IsConnected = true;

			Link = nodeLink;
			foreach(var sheet in Link.PropertySheets)
			{
				PropertySheetViewModels.Add(new PropertySheetViewModel(sheet));
			}
		}

		public LinkViewModel(LinkViewModel model)
		{
			Source = model.Source;
			Target = model.Target;
			Source.IsConnected = true;
			Target.IsConnected = true;

			Link = new TNodeLink(model.Link);
		}
		public LinkViewModel(LinkViewModel model, ConnectorViewModel source, ConnectorViewModel target)
		{
			Source = source;
			Target = target;
			Source.IsConnected = true;
			Target.IsConnected = true;
			foreach(var sheetVM in model.PropertySheetViewModels) { PropertySheetViewModels.Add(new PropertySheetViewModel(sheetVM));  }
			Link = new TNodeLink(model.Link);
			Link.ConnectedNode = Source.ParentNodeViewModel.DataNode;
			
		}

		public void TryAddLink()
		{
			var input = (TNodeInput)Target.Connector;
			if (input.Links.Contains(Link)) return; 
			input.Links.Add(Link);
		}

		public bool TryRemoveLink()
		{
			var input = (TNodeInput)Target.Connector;
			input.Links.Remove(Link);
			return input.Links.Any(l => l.Output == Link.Output); //return whether source/output still has links remaining
		}
		
		


		public ConnectorViewModel Source { get; }
		public ConnectorViewModel Target { get; }
	}
}
