using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ElCon.Model;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace ElCon.ViewModel
{
    public static class Analyzer
    {
        public static ObservableCollection<SemanticElement> GetSemanticElements(AssemblyInfo assemblyInfo,
            ObservableCollection<SemanticElement> semanticElements, ObservableCollection<ElementInfo> elementInfos)
        {
            foreach (var model in assemblyInfo.ProjectInfo.Models)
            {
                foreach (var node in model.SyntaxTree.GetRoot().DescendantNodesAndSelf())
                {
                    var typeSymbol = model.GetTypeInfo(node).Type;
                    if (typeSymbol != null)
                    {
                        if (semanticElements.SingleOrDefault(e => e.TypeName == typeSymbol.TypeKind.ToString()) == null)
                            semanticElements.Add(new SemanticElement(typeSymbol.TypeKind.ToString(), 1));
                        else semanticElements.Single(e => e.TypeName == typeSymbol.TypeKind.ToString()).TypeCounter++;
                        elementInfos.Add(new ElementInfo((model.GetSymbolInfo(node).Symbol)?.Name, typeSymbol.Name,
                            node.GetLocation().ToString()));
                    }
                }
            }

            var res = new ObservableCollection<SemanticElement>(semanticElements.Where(el => el.TypeCounter != 0));
            return res;
        }

        public static ObservableCollection<SyntaxElement> GetSyntaxInfo(AssemblyInfo assemblyInfo,
            ObservableCollection<SyntaxElement> syntaxElements)
        {
            foreach (var tree in assemblyInfo.ProjectInfo.Trees)
            {
                List<SyntaxNode> allNodes = tree.GetRoot().DescendantNodesAndSelf().ToList().Distinct().ToList();
                foreach (var c in allNodes)
                {
                    if(syntaxElements.SingleOrDefault(e => e.SyntaxType == c.Kind().ToString()) == null)
                        syntaxElements.Add(new SyntaxElement(c.Kind().ToString(), 1));
                    syntaxElements.Single(e => e.SyntaxType == c.Kind().ToString()).TypeCount++;
                }
            }

            var res = new ObservableCollection<SyntaxElement>(syntaxElements.Where(el => el.TypeCount != 0));
            return res;
        }
    }
}