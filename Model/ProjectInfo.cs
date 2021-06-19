using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Static_analyzer_app.Model
{
    public class ProjectInfo : CSharpSyntaxWalker
    {
        private int elementsCounter = 0; //всего элементов в проекте

        public int ElementsCounter
        {
            get => elementsCounter;
            set => elementsCounter = value;
        }

        private Compilation compile;
        public Compilation Compilation => compile;

        private List<SyntaxTree> _trees = new List<SyntaxTree>();
        public List<SyntaxTree> Trees => _trees;

        private List<SemanticModel> _models = new List<SemanticModel>();
        public List<SemanticModel> Models => _models;

        private List<SyntaxTrivia> _trivias = new List<SyntaxTrivia>();
        public List<SyntaxTrivia> Trivias => _trivias;

        //Construct
        public ProjectInfo(Project proj)
        {
            compile = proj.GetCompilationAsync().Result;
            GetTrees(proj, _models);
        }

        private void GetTrees(Project proj, List<SemanticModel> models)
        {
            foreach (var file in proj.Documents)
            {
                var tree = file.GetSyntaxTreeAsync().Result;
                Visit(tree.GetRoot());
                if (tree != null)
                {
                    _trees.Add(tree);
                    _trivias.AddRange(tree.GetRoot().DescendantTrivia());
                    models.Add(compile.GetSemanticModel(tree));
                }

                elementsCounter += tree.GetRoot().DescendantNodesAndSelf().Count();
            }
        }
    }
}