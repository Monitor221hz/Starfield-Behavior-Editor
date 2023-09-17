using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BehaviorEditor.MVVM.Model.Starfield;
namespace BehaviorEditor.MVVM.ViewModel
{
	public class ConnectionViewModel
	{
		public ConnectionViewModel(ConnectorViewModel source, ConnectorViewModel target)
		{
			Source = source;
			Target = target;

			Source.IsConnected = true;
			Target.IsConnected = true;
		}



		public ConnectorViewModel Source { get; }
		public ConnectorViewModel Target { get; }
	}
}
