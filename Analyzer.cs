using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Static_analyzer_app.Model;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Static_analyzer_app
{
    public static class Analyzer
    {
        public static ObservableCollection<SemanticElement> GetSemanticElements(AssemblyInfo _assemblyInfo,
            ObservableCollection<SemanticElement> SemanticElements, ObservableCollection<ElementInfo> ElementInfos)
        {
            foreach (var model in _assemblyInfo.ProjectInfo.Models)
            {
                foreach (var node in model.SyntaxTree.GetRoot().DescendantNodesAndSelf())
                {
                    var typeSymbol = model.GetTypeInfo(node).Type;
                    if (typeSymbol != null)
                    {
                        SemanticElements.Single(e => e.TypeName == typeSymbol.TypeKind.ToString()).TypeCounter++;
                        ElementInfos.Add(new ElementInfo((model.GetSymbolInfo(node).Symbol)?.Name, typeSymbol.Name,
                            typeSymbol.MetadataName));
                    }
                }
            }

            return new ObservableCollection<SemanticElement>(SemanticElements.Where(el => el.TypeCounter != 0));
        }

        public static ObservableCollection<SyntaxElement> GetSyntaxInfo(AssemblyInfo assemblyInfo,
            ObservableCollection<SyntaxElement> syntaxElements)
        {
            foreach (var tree in assemblyInfo.ProjectInfo.Trees)
            {
                List<SyntaxNode> allNodes = tree.GetRoot().DescendantNodesAndSelf().ToList().Distinct().ToList();
                foreach (var c in allNodes)
                {
                    syntaxElements.Single(e => e.SyntaxType == c.Kind().ToString()).TypeCount++;
                }
            }

            return new ObservableCollection<SyntaxElement>(syntaxElements.Where(el => el.TypeCount != 0));
        }
    }
}