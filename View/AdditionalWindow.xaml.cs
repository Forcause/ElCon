using System.Windows;

namespace ElCon.View
{
    public partial class AdditionalWindow : Window
    {
        public AdditionalWindow(Window owner)
        {
            DataContext = owner.DataContext;
            InitializeComponent();
        }
    }
}