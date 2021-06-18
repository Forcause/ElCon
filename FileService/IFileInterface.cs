using System.Collections.ObjectModel;
using roslynTest;

namespace Static_analyzer_app.FileService
{
    public interface IFIleInterface
    {
        void Save(string fileName, Analyzer analyzer);
    }
}