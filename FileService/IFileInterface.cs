using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace ElCon.FileService
{
    public interface IFIleInterface
    {
        void Save(string fileName, List<SyntaxNode> nodes);
    }
}