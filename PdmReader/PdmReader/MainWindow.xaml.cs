using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using PdmReader.Models;
using PdmReader.Models.PdmModels;
using FolderBrowserDialog = System.Windows.Forms.FolderBrowserDialog;

namespace PdmReader {
    public partial class PdmReaderWindow : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) {
            var handler = PropertyChanged;
            if(handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public PdmReaderWindow() {
            InitializeComponent();
            Folder.Focus();
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

        private ObservableCollection<string> _listShows;
        public ObservableCollection<string> ListShows {
            get {
                return _listShows;
            }
            set {
                _listShows = value;
                OnPropertyChanged("ListShows");
            }
        }

        private IEnumerable<string> GetPdmFiles(string dir, string searchPattern) {
            return string.IsNullOrEmpty(searchPattern) ? GetFiles.GetPdmFiles(dir) : GetFiles.GetPdmFiles(dir, searchPattern);
        }

        private string SelectedPath {
            get;
            set;
        }
        private void Folder_Click(object sender, RoutedEventArgs e) {
            var fbd = new FolderBrowserDialog {
                ShowNewFolderButton = false,
                SelectedPath = SelectedPath
            };
            var result = fbd.ShowDialog();
            if(result == System.Windows.Forms.DialogResult.Cancel)
                return;
            Folder.Text = fbd.SelectedPath;
            SelectedPath = fbd.SelectedPath;
            ListShow.ItemsSource = GetPdmFiles(Folder.Text, string.Format("*{0}*.pdm", SqlCheck.SelectedItem != null && SqlCheck.SelectionBoxItem.ToString() != "All" ? SqlCheck.SelectionBoxItem.ToString() : string.Empty));
        }

        private void Folder_KeyDown(object sender, KeyEventArgs e) {
            if(e.Key == Key.Return) {
                ListShow.ItemsSource = GetPdmFiles(Folder.Text, string.Format("*{0}*.pdm", SqlCheck.SelectedItem != null && SqlCheck.SelectionBoxItem.ToString() != "All" ? SqlCheck.SelectionBoxItem.ToString() : string.Empty));
                SelectedPath = Folder.Text;
            }
        }

        private void ListShow_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if(ListShow.SelectedItem == null) return;
            var pdmFileReader = new PdmFileReader();
            var pdmModels = pdmFileReader.ReadFromFile(ListShow.SelectedValue.ToString());
            var tableWindow = new TableWindow {
                Title = ListShow.SelectedValue.ToString(),
                Views = {
                    ItemsSource = pdmModels.Views,
                    IsReadOnly = true,
                    CanUserAddRows = false
                },
                Tables = {
                    ItemsSource = pdmModels.Tables,
                    IsReadOnly = true,
                    CanUserAddRows = false
                }
            };
            tableWindow.Show();
        }

        private void Folder_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            Folder.SelectAll();
        }
    }
}
