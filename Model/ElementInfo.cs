using System.ComponentModel;
using System.Runtime.CompilerServices;
using Static_analyzer_app.Annotations;

#nullable enable
namespace Static_analyzer_app.Model
{
    public class ElementInfo : INotifyPropertyChanged
    {
        private string? _varName;
        public string VarName => _varName;

        private string _typeName;
        public string TypeName => _typeName;

        private string _parentFile;
        public string ParentFile => _parentFile;

        public ElementInfo(string? varName, string typeName, string parentFile)
        {
            _typeName = typeName;
            _parentFile = parentFile;
            _varName = varName;
            OnPropertyChanged("TypeName");
            OnPropertyChanged("VarName");
            OnPropertyChanged("MetadataName");
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}