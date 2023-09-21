using BehaviorEditor.MVVM.Model.Starfield.Properties;
using Nodify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BehaviorEditor.MVVM.ViewModel
{
	
	public class RowViewModel : ObservableObject
    {
		private NodifyObservableCollection<PropertyViewModel> propertyViewModels = new NodifyObservableCollection<PropertyViewModel>();
		private Row dataRow;

		public Row DataRow { get => dataRow; set => SetProperty(ref dataRow, value); }

		public string Name { get; set; } = "Name";

		
		public NodifyObservableCollection<PropertyViewModel> PropertyViewModels { get => propertyViewModels; set => SetProperty(ref propertyViewModels, value); }

		public DelegateCommand<RowViewModel> RemoveCommand { get; set; }
		public RowViewModel(int numColumns, Row row, DelegateCommand<RowViewModel> removeCommand) 
        { 
            this.DataRow = row;
			RemoveCommand = removeCommand;
			for (int i = 0; i < numColumns; i++) 
            {
				Property? property = row.Properties[i];
				PropertyViewModels.Add(new PropertyViewModel(property));
            }
        }

		public RowViewModel(RowViewModel model)
		{
			DataRow = model.DataRow;
			Name = model.Name;
			foreach(var propertyVM in model.PropertyViewModels) { PropertyViewModels.Add(new PropertyViewModel(propertyVM));  }
			RemoveCommand = model.RemoveCommand;
		}






    }
}
