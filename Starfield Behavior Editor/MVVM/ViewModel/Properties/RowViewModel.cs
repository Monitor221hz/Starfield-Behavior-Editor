using BehaviorEditor.MVVM.Model.Starfield.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace BehaviorEditor.MVVM.ViewModel
{
	[ExpandableObject]
	public class RowViewModel
    {
        private Row row { get; set; }


        private List<PropertyViewModel> PropertyViewModels { get; set; } = new List<PropertyViewModel>();

        public RowViewModel(Row row) 
        { 
            this.row = row;

            foreach(var property in row.Properties) 
            {
                PropertyViewModels.Add(new PropertyViewModel(property));
            }
        }



    }
}
