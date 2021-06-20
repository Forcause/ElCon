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

        private string _metadataName;
        public string MetadataName => _metadataName;

        public ElementInfo(string? varName, string typeName, string metadataName)
        {
            _typeName = typeName;
            _metadataName = metadataName;
            _varName = varName;
            OnPropertyChanged("TypeName");
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}