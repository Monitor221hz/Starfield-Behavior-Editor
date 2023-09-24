using BehaviorEditor.MVVM.Model.Starfield;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviorEditor.MVVM.ViewModel
{
	public class EmbeddedNodeViewModel : NodeViewModel
	{
		public EmbeddedNodeViewModel(TNode node) : base(node)
		{
		}

		public EmbeddedNodeViewModel(EmbeddedNodeViewModel model) : base(model)
		{
		}
	}
}
