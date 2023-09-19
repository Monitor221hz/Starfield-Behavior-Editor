using System.Collections.Generic;

namespace BehaviorEditor.MVVM.Model.Starfield.Connectors
{
	public interface TNodeConnector
	{
		public string Name { get; set; }
		int ID { get; set; }
		int IDX { get; set; }

	}
}