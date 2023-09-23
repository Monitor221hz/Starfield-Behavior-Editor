using BehaviorEditor.MVVM.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BehaviorEditor.MVVM.View.Controls
{
	/// <summary>
	/// Interaction logic for GraphExplorer.xaml
	/// </summary>
	public partial class GraphExplorer : UserControl
	{
		public GraphViewModel GraphSource
		{
			get => (GraphViewModel)GetValue(GraphSourceProperty);
			set => SetValue(GraphSourceProperty, value);
		}

		public static readonly DependencyProperty GraphSourceProperty =
			DependencyProperty.Register(
				nameof(GraphSource), typeof(GraphViewModel), typeof(GraphExplorer));
		public GraphExplorer()
		{
			InitializeComponent();
		}
	}
}
