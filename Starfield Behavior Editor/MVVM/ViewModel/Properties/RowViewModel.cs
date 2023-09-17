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

		private Row row { get; set; }

		public string Name { get; set; } = "Name";

		
		public NodifyObservableCollection<PropertyViewModel> PropertyViewModels { get => propertyViewModels; set => SetProperty(ref propertyViewModels, value); }
		public RowViewModel(Row row, List<string> headerNames) 
        { 
            this.row = row;

			for (int i = 0; i < headerNames.Count; i++) 
            {
				Property? property = row.Properties[i];
				string headerName = headerNames[i];
				PropertyViewModels.Add(new PropertyViewModel(headerName, property));
            }
        }



    }
}
