using System.Windows;

namespace Static_analyzer_app.View
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