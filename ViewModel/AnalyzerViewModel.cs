using roslynTest;
using Static_analyzer_app.FileService;

namespace Static_analyzer_app.ViewModel
{
    public class AnalyzerViewModel
    {
        public AnalyzerViewModel(JsonFileService jsonFileService, XmlFileService xmlFileService)
        {
            this._jsonFileService = jsonFileService;
            this._xmlFileService = xmlFileService;
        }
        
        public Analyzer Analyzer { get; set; }
        
        private BaseCommand loadProject;
        private BaseCommand saveCommand;

        private static DialogService _dialogService = new DialogService();
        private JsonFileService _jsonFileService;
        private XmlFileService _xmlFileService;
        
        //дописать реализацию комманд
    }
}