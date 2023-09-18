using Nodify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BehaviorEditor.MVVM.Model.Starfield;
using BehaviorEditor.MVVM.Model.Starfield.Properties;
using System.Diagnostics;

namespace BehaviorEditor.MVVM.ViewModel
{
	
	public class PropertySheetViewModel : ObservableObject
    {
		private NodifyObservableCollection<RowViewModel> rows = new NodifyObservableCollection<RowViewModel>();
		private NodifyObservableCollection<ColumnViewModel> columns = new NodifyObservableCollection<ColumnViewModel>();
		
		private PropertySheet propertySheet { get; set;  }

		
		public NodifyObservableCollection<RowViewModel> RowViewModels { get => rows; set => SetProperty(ref rows, value); }

		
		public NodifyObservableCollection<ColumnViewModel> ColumnViewModels { get => columns; set => SetProperty(ref columns, value); }



		public DelegateCommand<RowViewModel> RemoveRowCommand { get; set; }

		public DelegateCommand AddRowCommand { get; set; }

		public PropertySheetViewModel(PropertySheet propertySheet)
        {
			RemoveRowCommand = new DelegateCommand<RowViewModel>(RemoveRow);	
			AddRowCommand = new DelegateCommand(AddRow);
            this.propertySheet = propertySheet;
			List<string> columnHeaderNames = new List<string>();
            foreach(var column in propertySheet.Columns)
            {
                ColumnViewModels.Add(new ColumnViewModel(column));
				columnHeaderNames.Add(column.Header);
            }
			foreach (var row in propertySheet.Rows)
			{
				RowViewModels.Add(new RowViewModel(ColumnViewModels.Count, row, RemoveRowCommand));
			}
		}
		public void RemoveRow(RowViewModel rowVM)
		{
			int removeIndex = RowViewModels.IndexOf(rowVM);
			RowViewModels.RemoveAt(removeIndex);
			propertySheet.Rows.RemoveAt(removeIndex);
		}

		public void AddRow()
		{
			var row = new Row();
			foreach(var column in ColumnViewModels)
			{
				var property = new Property() { RawValue = column.Header, Type=2 };
				row.Properties.Add(property);
			}
			RowViewModels.Add(new RowViewModel(ColumnViewModels.Count, row, RemoveRowCommand));
			propertySheet.Rows.Add(row);
		}


		
	}
}
