using Nodify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BehaviorEditor.MVVM.Model.Starfield.Properties;


namespace BehaviorEditor.MVVM.ViewModel
{
	
	public class PropertyViewModel : ObservableObject
    {
		private string value;

		private Property property;

		public string Value 
		{ 
			get => property.RawValue; 
			set 
			{
				SetProperty(ref this.value, value);
				property.RawValue = this.value;
			} 
		}

		public PropertyViewModel(Property prop)
		{
			property = prop;
			value = prop.RawValue;

		}


		
	}
}
