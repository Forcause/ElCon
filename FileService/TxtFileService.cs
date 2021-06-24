using System.Collections.Generic;
using System.IO;
using Microsoft.CodeAnalysis;

namespace ElCon.FileService
{
    public class TxtFileService : IFIleInterface
    {
        public void Save(string fileName, List<SyntaxNode> nodes)
        {
                using (TextWriter writer = new StreamWriter(fileName, append: true))
                {
                    foreach (var node in nodes)
                    {
                        node.WriteTo(writer);
                    }
                }
        }
    }
}