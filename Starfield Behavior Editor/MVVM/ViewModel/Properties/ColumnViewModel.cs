using BehaviorEditor.MVVM.Model.Starfield.Properties;
using Nodify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BehaviorEditor.MVVM.ViewModel
{
	
	public class ColumnViewModel : ObservableObject
    {
		private string header;

		private Column dataColumn;

		public string Header 
		{ 
			get => dataColumn.Header;
			set 
			{
				SetProperty(ref header, value);
				dataColumn.Header = value;
			}
		}

		public Column DataColumn { get => dataColumn; set => SetProperty(ref dataColumn, value); }

		public ColumnViewModel(Column column)
		{
			DataColumn= column;	
			header = column.Header;	
		}

		public ColumnViewModel(ColumnViewModel columnVM)
		{
			DataColumn = columnVM.DataColumn;
			Header = columnVM.Header;
		}
	}
}
