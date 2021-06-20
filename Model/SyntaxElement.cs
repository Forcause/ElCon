using System.ComponentModel;
using System.Runtime.CompilerServices;
using Static_analyzer_app.Annotations;

namespace Static_analyzer_app.Model
{
    public class SyntaxElement : INotifyPropertyChanged
    {
        private string _syntaxType;
        public string SyntaxType => _syntaxType;

        private int _typeCount;
        public int TypeCount { get => _typeCount;
            set
            {
                _typeCount = value;
                OnPropertyChanged("TypeCount");
            } }

        public SyntaxElement(string type)
        {
            _syntaxType = type;
            _typeCount = 0;
            OnPropertyChanged("TypeCount");
            OnPropertyChanged("SyntaxType");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}