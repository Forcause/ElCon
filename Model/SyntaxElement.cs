using System.ComponentModel;
using System.Runtime.CompilerServices;
using ElCon.Annotations;

namespace ElCon.Model
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
        public SyntaxElement(string type, int ty)
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