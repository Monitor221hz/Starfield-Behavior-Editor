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
    /// Interaction logic for PropertyExplorer.xaml
    /// </summary>
    public partial class PropertyExplorer : UserControl
    {
		public IEnumerable ItemsSource
		{
			get => (IEnumerable)GetValue(ItemsSourceProperty);
			set => SetValue(ItemsSourceProperty, value);
		}

		public static readonly DependencyProperty ItemsSourceProperty =
			DependencyProperty.Register(
				nameof(ItemsSource), typeof(IEnumerable), typeof(PropertyExplorer));
		public PropertyExplorer()
        {
            InitializeComponent();
        }
    }
}
