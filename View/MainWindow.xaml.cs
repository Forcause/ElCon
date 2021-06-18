using System.Windows;
using Microsoft.Build.Locator;
using Static_analyzer_app.FileService;
using Static_analyzer_app.ViewModel;

namespace Static_analyzer_app.View
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
            DataContext = new AnalyzerViewModel(new JsonFileService(), new XmlFileService());
        }
    }
}