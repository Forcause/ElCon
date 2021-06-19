using System;
using System.Collections.ObjectModel;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Static_analyzer_app.Model;
using Static_analyzer_app.FileService;

namespace Static_analyzer_app.ViewModel
{
    public class AnalyzerViewModel
    {
        private BaseCommand loadProject;
        private BaseCommand saveCommand;

        private static DialogService _dialogService = new DialogService();
        private JsonFileService _jsonFileService;
        private XmlFileService _xmlFileService;
        
        private AssemblyInfo _assemblyInfo;
        
        public AnalyzerViewModel(JsonFileService jsonFileService, XmlFileService xmlFileService, string projPath)
        {
            this._jsonFileService = jsonFileService;
            this._xmlFileService = xmlFileService;
            _assemblyInfo = new AssemblyInfo(projPath);
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
            
            SemanticElements = Analyzer.GetSemanticElements(_assemblyInfo, SemanticElements, ElementInfos);
            SyntaxElements = Analyzer.GetSyntaxInfo(_assemblyInfo, SyntaxElements);
        }
        
        public ObservableCollection<SemanticElement> SemanticElements { get; private set; }
        public ObservableCollection<SyntaxElement> SyntaxElements { get; private set; }
        public ObservableCollection<ElementInfo> ElementInfos { get; private set; }
        
        
        //дописать реализацию комманд
    }
}