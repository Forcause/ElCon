using System.ComponentModel;
using System.Runtime.CompilerServices;
using Static_analyzer_app.Annotations;

#nullable enable
namespace Static_analyzer_app.Model
{
    public class SemanticElement : INotifyPropertyChanged
    {
        private string _typeName;
        public string TypeName => _typeName;

        private int _typeCounter;

        public int TypeCounter
        {
            get => _typeCounter;
            set
            {
                _typeCounter = value;
                OnPropertyChanged("TypeCounter");
            }
        }

        public SemanticElement(string typeName)
        {
            _typeName = typeName;
            _typeCounter = 0;
            OnPropertyChanged("TypeCounter");
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