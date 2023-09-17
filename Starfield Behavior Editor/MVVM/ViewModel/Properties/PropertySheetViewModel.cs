using Nodify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BehaviorEditor.MVVM.Model.Starfield;
using BehaviorEditor.MVVM.Model.Starfield.Properties;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace BehaviorEditor.MVVM.ViewModel
{
	[ExpandableObject]
	public class PropertySheetViewModel : ObservableObject
    {
		private NodifyObservableCollection<RowViewModel> rows = new NodifyObservableCollection<RowViewModel>();
		private NodifyObservableCollection<ColumnViewModel> columns = new NodifyObservableCollection<ColumnViewModel>();

		private PropertySheet propertySheet { get; set;  }

		[ExpandableObject]
		public NodifyObservableCollection<RowViewModel> Rows { get => rows; set => SetProperty(ref rows, value); }

		[ExpandableObject]
		public NodifyObservableCollection<ColumnViewModel> Columns { get => columns; set => SetProperty(ref columns, value); }

		public PropertySheetViewModel(PropertySheet propertySheet)
        {
            this.propertySheet = propertySheet;
            foreach(var column in propertySheet.Columns)
            {
                Columns.Add(new ColumnViewModel(column));
            }
            foreach(var row in propertySheet.Rows)
            {
                Rows.Add(new RowViewModel(row));
            }
        }



		
	}
}
