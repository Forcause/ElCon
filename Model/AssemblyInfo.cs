using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;

namespace ElCon.Model
{
    public class AssemblyInfo
    {
        private MSBuildWorkspace _workspace;
        public MSBuildWorkspace Workspace => _workspace;

        private Project _project;
        public Project Project => _project;

        private ProjectInfo _projectInfo;
        public ProjectInfo ProjectInfo => _projectInfo;

        public AssemblyInfo(string projpath)
        {
            _workspace = MSBuildWorkspace.Create();
            _project = _workspace.OpenProjectAsync(projpath).Result;
            _projectInfo = new ProjectInfo(_project);
        }
    }
}