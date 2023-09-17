using BehaviorEditor.MVVM.ViewModel;
using System;
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

namespace BehaviorEditor.MVVM.View
{
    /// <summary>
    /// Interaction logic for EditorView.xaml
    /// </summary>
    public partial class EditorView : UserControl
    {
        private EditorViewModel viewModel { get; }
        public EditorView()
        {
            viewModel= new EditorViewModel();
            DataContext = viewModel;
            InitializeComponent();
        }

		private void NodifyEditor_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
            foreach(NodeViewModel node in e.AddedItems)
            {
                
            }
		}
	}
}
