using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Static_analyzer_app.FileService;
using Static_analyzer_app.Model;
using Static_analyzer_app.ViewModel;
using ProjectInfo = Static_analyzer_app.Model.ProjectInfo;

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
        }

        private void FileClicked(object sender, RoutedEventArgs e)
        {
            DataContext = new AnalyzerViewModel(new JsonFileService(), new XmlFileService(), @"C:\Users\Forcause\RiderProjects\testing\testing.csproj");
        }
    }
}