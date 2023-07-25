using Kr4.View.Interface;
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

namespace Kr4.View
{
    /// <summary>
    /// Interaction logic for EditStarView.xaml
    /// </summary>
    public partial class EditStarView : Window, IEditWindow
    {
        public EditStarView()
        {
            InitializeComponent();
            Loaded += EditStarView_Loaded;
        }

        private void EditStarView_Loaded(object sender, RoutedEventArgs e)
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
