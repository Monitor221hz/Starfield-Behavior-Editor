using BehaviorEditor.MVVM.Model.Starfield.Properties;
using Nodify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace BehaviorEditor.MVVM.ViewModel
{
	[ExpandableObject]
	public class ColumnViewModel : ObservableObject
    {
		private string header;

		private Column column { get; set; }

		public string Header 
		{ 
			get => column.Header;
			set 
			{
				SetProperty(ref header, value);
				column.Header = value;
			}
		}

		public ColumnViewModel(Column column)
		{
			this.column= column;	
			header = column.Header;	
		}
	}
}
