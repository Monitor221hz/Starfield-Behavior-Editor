using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BehaviorEditor.MVVM.Model.Starfield;
using BehaviorEditor.MVVM.Model.Starfield.Connectors;

namespace BehaviorEditor.MVVM.ViewModel
{
	public class LinkViewModel
	{

		public TNodeLink Link { get; private set; }


		public LinkViewModel(ConnectorViewModel source, ConnectorViewModel target)
		{
			Source = source;
			Target = target;

			Source.IsConnected = true;
			Target.IsConnected = true;
			Link = new TNodeLink() { ConnectedNode = target.ParentNode, Output = source.Connector.IDX };

		}
		public LinkViewModel(ConnectorViewModel source, ConnectorViewModel target, TNodeLink nodeLink)
		{
			Source = source;
			Target = target;

			Source.IsConnected = true;
			Target.IsConnected = true;

			Link = nodeLink;
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

			Link = new TNodeLink(model.Link);
			Link.ConnectedNode = Source.ParentNode;
			
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
