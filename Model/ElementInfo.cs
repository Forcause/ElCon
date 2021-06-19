#nullable enable
namespace Static_analyzer_app.Model
{
    public class ElementInfo
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
        }
    }
}