using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using ElCon.Annotations;
using ElCon.FileService;
using ElCon.Model;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace ElCon.ViewModel
{
    public class AnalyzerViewModel : INotifyPropertyChanged
    {
        private BaseCommand loadProject;
        private BaseCommand saveCommand;

        private static DialogService _dialogService = new DialogService();
        private TxtFileService _txtFileService;

        private AssemblyInfo _assemblyInfo;
        public int ElementsCount => _assemblyInfo.ProjectInfo.ElementsCounter;

        public ObservableCollection<SemanticElement> SemanticElements { get; private set; }
        public ObservableCollection<SyntaxElement> SyntaxElements { get; private set; }
        public ObservableCollection<ElementInfo> ElementInfos { get; private set; }

        public AnalyzerViewModel(TxtFileService txtFileService)
        {
            _txtFileService = txtFileService;
            
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

        private void LoadInfo(AssemblyInfo assemblyInfo)
        {
            SemanticElements = Analyzer.GetSemanticElements(_assemblyInfo, SemanticElements, ElementInfos);
            SyntaxElements = Analyzer.GetSyntaxInfo(_assemblyInfo, SyntaxElements);
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

                            SemanticElements = new ObservableCollection<SemanticElement>();
                            SyntaxElements = new ObservableCollection<SyntaxElement>();
                            ElementInfos = new ObservableCollection<ElementInfo>();

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

        public BaseCommand SaveCommand
        {
            get
            {
                return saveCommand ??= new BaseCommand(obj =>
                {
                    try
                    {
                        if (_dialogService.SaveFileDialog() == true)
                        {
                            foreach (var tree in _assemblyInfo.ProjectInfo.Trees)
                            {
                                _txtFileService.Save(_dialogService.FilePath,
                                    tree.GetRoot().DescendantNodesAndSelf().ToList().Distinct().ToList());
                            }
                            _dialogService.ShowMessage("Save complete");
                        }
                    }
                    catch (Exception ex)
                    {
                        _dialogService.ShowMessage(ex.Message);
                    }
                });
            }
            
        }
    }
}