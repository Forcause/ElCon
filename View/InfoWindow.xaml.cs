using System.Windows;

namespace ElCon.View
{
    public partial class InfoWindow : Window
    {
        public InfoWindow(Window owner)
        {
            DataContext = owner.DataContext;
            InitializeComponent();
        }
    }
}