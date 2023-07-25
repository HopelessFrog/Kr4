using Kr4.ViewModel.EditViewModels.Interface;
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
using System.Windows.Shapes;
using Kr4.View.Interface;

namespace Kr4.View
{
    /// <summary>
    /// Interaction logic for EditGalaxyView.xaml
    /// </summary>
    public partial class EditGalaxyView : Window, IEditWindow
    {
        public EditGalaxyView()
        {
            InitializeComponent();
            Loaded += EditGalaxyView_Loaded;
        }

        private void EditGalaxyView_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ICloseWindows viewModel)
            {
                viewModel.Close +=
                    () => { this.Close(); };

                Closing += (s, e) => { e.Cancel = !viewModel.CanClose(); };
            }
        }
    }
}
