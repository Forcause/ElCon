using System.Windows.Forms;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace Static_analyzer_app.FileService
{
    public class DialogService
    {
        public string FilePath { get; set; }
        public int FilterIndex { get; set; }

        public bool OpenFileDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = System.Environment.CurrentDirectory;
            ofd.Filter = "Файл csproj|*.csproj";
            if (ofd.ShowDialog() == true)
            {
                FilePath = ofd.FileName;
                FilterIndex = ofd.FilterIndex;
                return true;
            }
            return false;
        }

        public bool SaveFileDialog()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = System.Environment.CurrentDirectory;
            sfd.Filter = "Файл в xml|*.xml|Файл в json|*.json";
            if (sfd.ShowDialog() == true)
            {
                FilterIndex = sfd.FilterIndex;
                FilePath = sfd.FileName;
                return true;
            }
            return false;
        }

        public void ShowMessage(string mes)
        {
            MessageBox.Show(mes);
        }
    }
}