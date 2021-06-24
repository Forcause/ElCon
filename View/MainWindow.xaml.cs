using System.Windows;
using ElCon.FileService;
using ElCon.ViewModel;
using Microsoft.Build.Locator;

namespace ElCon.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MSBuildLocator.RegisterDefaults();
            DataContext = new AnalyzerViewModel(new TxtFileService());
        }

        private void OpenAdditionalWindow(object sender, RoutedEventArgs e)
        {
            AdditionalWindow additional = new AdditionalWindow(this);
            additional.Show();
        }

        private void ShowInfoBox(object sender, RoutedEventArgs e)
        {
            InfoWindow info = new InfoWindow();
            info.Show();
        }
    }
}