#nullable enable
namespace Static_analyzer_app.Model
{
    public class SemanticElement
    {
        private string _typeName;
        public string TypeName => _typeName;

        private int _typeCounter;
        public int TypeCounter { get; set; }

        public SemanticElement(string typeName)
        {
            _typeName = typeName;
            _typeCounter = 0;
        }
    }
}