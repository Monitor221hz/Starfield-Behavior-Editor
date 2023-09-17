using Nodify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BehaviorEditor.MVVM.Model.Starfield;
using BehaviorEditor.MVVM.Model.Starfield.Properties;


namespace BehaviorEditor.MVVM.ViewModel
{
	
	public class PropertySheetViewModel : ObservableObject
    {
		private NodifyObservableCollection<RowViewModel> rows = new NodifyObservableCollection<RowViewModel>();
		private NodifyObservableCollection<ColumnViewModel> columns = new NodifyObservableCollection<ColumnViewModel>();

		private PropertySheet propertySheet { get; set;  }

		
		public NodifyObservableCollection<RowViewModel> Rows { get => rows; set => SetProperty(ref rows, value); }

		
		public NodifyObservableCollection<ColumnViewModel> Columns { get => columns; set => SetProperty(ref columns, value); }



		public PropertySheetViewModel(PropertySheet propertySheet)
        {
            this.propertySheet = propertySheet;
			List<string> columnHeaderNames = new List<string>();
            foreach(var column in propertySheet.Columns)
            {
                Columns.Add(new ColumnViewModel(column));
				columnHeaderNames.Add(column.Header);
            }
			foreach (var row in propertySheet.Rows)
			{
				Rows.Add(new RowViewModel(row, columnHeaderNames));
			}
		}



		
	}
}
