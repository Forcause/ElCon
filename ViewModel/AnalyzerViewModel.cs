using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Static_analyzer_app.Annotations;
using Static_analyzer_app.Model;
using Static_analyzer_app.FileService;
using Static_analyzer_app.View;

namespace Static_analyzer_app.ViewModel
{
    public class AnalyzerViewModel : INotifyPropertyChanged
    {
        private BaseCommand loadProject;
        private BaseCommand saveCommand;

        private static DialogService _dialogService = new DialogService();
        private JsonFileService _jsonFileService;
        private XmlFileService _xmlFileService;

        private AssemblyInfo _assemblyInfo;
        
        public ObservableCollection<SemanticElement> SemanticElements { get; private set; }
        public ObservableCollection<SyntaxElement> SyntaxElements { get; private set; }
        public ObservableCollection<ElementInfo> ElementInfos { get; private set; }

        public AnalyzerViewModel(JsonFileService jsonFileService, XmlFileService xmlFileService)
        {
            this._jsonFileService = jsonFileService;
            this._xmlFileService = xmlFileService;
            SemanticElements = new ObservableCollection<SemanticElement>();
            SyntaxElements = new ObservableCollection<SyntaxElement>();
            ElementInfos = new ObservableCollection<ElementInfo>();

            foreach (var t in Enum.GetNames(typeof(TypeKind)))
            {
                SemanticElements.Add(new SemanticElement(t));
            }

            foreach (var t in Enum.GetNames(typeof(SyntaxKind)))
            {
                SyntaxElements.Add(new SyntaxElement(t));
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //дописать реализацию комманд
        public BaseCommand LoadProject
        {
            get
            {
                return loadProject ??= new BaseCommand(obj =>
                {
                    try
                    {
                        if (_dialogService.OpenFileDialog() == true)
                        {
                            _assemblyInfo = new AssemblyInfo(_dialogService.FilePath);

                            LoadInfo(_assemblyInfo);
                            OnPropertyChanged("SemanticElements");
                            OnPropertyChanged("SyntaxElements");
                            _dialogService.ShowMessage($"Project {_dialogService.FilePath} successfully loaded");
                        }
                    }
                    catch (Exception ex)
                    {
                        _dialogService.ShowMessage($"There is an unhandled exception:\n{ex}");
                    }
                });
            }
        }

        private void LoadInfo(AssemblyInfo assemblyInfo)
        {
            SemanticElements = Analyzer.GetSemanticElements(_assemblyInfo, SemanticElements, ElementInfos);
            SyntaxElements = Analyzer.GetSyntaxInfo(_assemblyInfo, SyntaxElements);
        }
    }
}