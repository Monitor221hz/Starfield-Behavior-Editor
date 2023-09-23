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

		private int typeCode;

		private List<string> typeNames = new List<string> { "int", "float", "string", "bool", "null" };
		private string typeName;

		public DelegateCommand<string?> SetTypeCommand { get; }
		public string Value 
		{ 
			get => property.RawValue; 
			set 
			{
				SetProperty(ref this.value, value);
				property.RawValue = this.value;
			}
		}

		public int TypeCode
		{
			get => property.Type; 
			set
			{
				SetProperty(ref this.typeCode, value);
				property.Type = value;
			}
		}

		public string TypeName => typeNames[typeCode];


		public Property Property { get => property; set => SetProperty(ref property, value); }

		public PropertyViewModel(Property prop)
		{
			SetTypeCommand = new DelegateCommand<string?>(SetType);
			Property = prop;
			Value = prop.RawValue;
			TypeCode = prop.Type;

		}

		public PropertyViewModel(PropertyViewModel propVM)
		{
			SetTypeCommand = new DelegateCommand<string?>(SetType);
			Property = propVM.Property;
			Value = propVM.Value;
			TypeCode = propVM.TypeCode;
		}

		public void SetType(string? code) => TypeCode = Int32.Parse(code!);


	}
}
