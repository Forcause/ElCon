using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace roslynTest
{
    public class Analyzer
    {
        private Dictionary<string, int> _syntaxElementsCounter = new Dictionary<string, int>();
        public Dictionary<string, int> SyntaxElementsCounter => _syntaxElementsCounter;
        
        private Dictionary<string, int> _typesCounter = new Dictionary<string, int>();
        public Dictionary<string, int> TypesCounter => _typesCounter;
        
        private Dictionary<string, string> _varNames = new Dictionary<string, string>();
        public Dictionary<string, string> VarNames => _varNames;
        
        public Analyzer(ProjectInfo project)
        {
            foreach (var t in Enum.GetNames(typeof(SyntaxKind)))
            {
                _syntaxElementsCounter.Add(t, 0);
            }

            foreach (var t in Enum.GetNames(typeof(TypeKind)))
            {
                _typesCounter.Add(t, 0);
            }

            GetSyntaxInfo(_syntaxElementsCounter, _typesCounter, project);

            /*
            foreach (var t in _syntaxElementsCounter)
            {
                if (t.Value != 0)
                    Console.WriteLine($"Element type in syntax model: {t.Key}, count in project: {t.Value}");
            }

            foreach (var t in _typesCounter)
            {
                if (t.Value != 0)
                    Console.WriteLine($"Element type in semantic model: {t.Key}, count in project: {t.Value}");
            }

            Console.WriteLine($"Number of elements: {project.ElementsCounter}");
            */
        }

        private void GetSemanticInfo(Dictionary<string, int> typesCounter, List<SyntaxNode> allNodes,
            ProjectInfo project)
        {
            foreach (var model in project.Models)
            {
                foreach (var node in model.SyntaxTree.GetRoot().DescendantNodesAndSelf())
                {
                    var typeSymbol = model.GetTypeInfo(node).Type;

                    if (typeSymbol != null)
                    {
                        //переделать хранилища и посмотреть, как хранить используемые типы данных
                        typesCounter[typeSymbol.TypeKind.ToString()]++;
                        var varName = (model.GetSymbolInfo(node).Symbol)?.Name;
                        _varNames.Add(varName, typeSymbol.Name);
                        /*Console.WriteLine(varName);
                        Console.WriteLine(typeSymbol.MetadataName);
                        Console.WriteLine(typeSymbol.Name);
                        Console.WriteLine(typeSymbol.BaseType);
                        Console.WriteLine("--------------");*/
                    }
                }
            }
        }

        private void GetSyntaxInfo(Dictionary<string, int> syntaxElementsCounter,
            Dictionary<string, int> typesCounter, ProjectInfo project)
        {
            foreach (var tree in project.Trees)
            {
                List<SyntaxNode> allnodes = tree.GetRoot().DescendantNodesAndSelf().ToList();
                allnodes = allnodes.Distinct().ToList();
                GetSemanticInfo(typesCounter, allnodes, project);
                foreach (var c in allnodes)
                {
                    syntaxElementsCounter[c.Kind().ToString()]++;
                }
            }
        }
    }
}