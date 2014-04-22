using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using PdmReader.Models;
using FolderBrowserDialog = System.Windows.Forms.FolderBrowserDialog;

namespace PdmReader {
    public partial class MainWindow : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) {
            var handler = PropertyChanged;
            if(handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindow() {
            InitializeComponent();
        }

        private string _folderShow = "请选择文件夹...";
        public string FolderShow {
            get {
                return _folderShow;
            }
            set {
                _folderShow = value;
                OnPropertyChanged("FolderShow");
            }
        }

        private void Folder_Click(object sender, RoutedEventArgs e) {
            var fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            FolderShow = fbd.SelectedPath;
            //Folder.Text = fbd.SelectedPath;
            ListShow.ItemsSource = GetFiles.GetPdmFiles(FolderShow);
        }

        private void Folder_KeyDown(object sender, KeyEventArgs e) {

        }
    }
}
