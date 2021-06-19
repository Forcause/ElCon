namespace Static_analyzer_app.Model
{
    public class SyntaxElement
    {
        private string _syntaxType;
        public string SyntaxType => _syntaxType;

        private int _typeCount;
        public int TypeCount { get; set; }

        public SyntaxElement(string type)
        {
            _syntaxType = type;
            _typeCount = 0;
        }
    }
}