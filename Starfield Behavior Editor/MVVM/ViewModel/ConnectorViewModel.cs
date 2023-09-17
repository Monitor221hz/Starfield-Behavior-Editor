using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace BehaviorEditor.MVVM.ViewModel
{
    public class ConnectorViewModel : INotifyPropertyChanged
    {

		private Point _anchor;
		public Point Anchor
		{
			set
			{
				_anchor = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Anchor)));
			}
			get => _anchor;
		}

		private bool _isConnected;
		public bool IsConnected
		{
			set
			{
				_isConnected = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsConnected)));
			}
			get => _isConnected;
		}

		public string Name { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;

		public ConnectorViewModel(string name, PropertyChangedEventHandler eventHandler)
		{
			Name = name;
			PropertyChanged = eventHandler;
		}
		public ConnectorViewModel(string name) => Name = name;
	}
}
