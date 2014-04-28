﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
            Init();
        }

        private void Init() {
            Folder.Focus();
            Folder.MouseDoubleClick += (sender, e) => Folder.SelectAll();
            Search.MouseDoubleClick += (sender, e) => Search.SelectAll();
        }

        //未使用
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

        private PdmFileReader _pdmFileReader;
        public PdmFileReader PdmFileReader {
            get {
                return _pdmFileReader ?? (_pdmFileReader = new PdmFileReader());
            }
            set {
                _pdmFileReader = value;
                OnPropertyChanged("PdmFileReader");
            }
        }

        private SearchWindow _searchWindow;
        public SearchWindow SearchWindow {
            get {
                return _searchWindow ?? (_searchWindow = new SearchWindow());
            }
            set {
                _searchWindow = value;
                OnPropertyChanged("SearchWindow");
            }
        }

        private List<string> _listShows;
        public List<string> ListShows {
            get {
                return _listShows;
            }
            set {
                _listShows = value;
                OnPropertyChanged("ListShows");
            }
        }

        private ObservableCollection<PdmModels> _pdmModels;
        public ObservableCollection<PdmModels> PdmModels {
            get {
                return _pdmModels ?? (_pdmModels = new ObservableCollection<PdmModels>());
            }
            set {
                _pdmModels = value;
            }
        }

        private static IEnumerable<string> GetPdmFiles(string dir, string searchPattern) {
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
            ListShows = GetPdmFiles(Folder.Text, string.Format("*{0}*.pdm", SqlCheck.SelectedItem != null && SqlCheck.SelectionBoxItem.ToString() != "All" ? SqlCheck.SelectionBoxItem.ToString() : string.Empty)).ToList();
            ListShow.ItemsSource = ListShows;
            foreach(var listShow in ListShows) {
                PdmModels.Add(PdmFileReader.ReadFromFile(listShow));
            }
        }
        private void Folder_KeyDown(object sender, KeyEventArgs e) {
            if(e.Key == Key.Return) {
                SelectedPath = Folder.Text;
                ListShows = GetPdmFiles(Folder.Text, string.Format("*{0}*.pdm", SqlCheck.SelectedItem != null && SqlCheck.SelectionBoxItem.ToString() != "All" ? SqlCheck.SelectionBoxItem.ToString() : string.Empty)).ToList();
                ListShow.ItemsSource = ListShows;
                foreach(var listShow in ListShows) {
                    PdmModels.Add(PdmFileReader.ReadFromFile(listShow));
                }
            }
        }

        private void ListShow_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if(ListShow.SelectedItem == null) return;
            var pdmModels = PdmFileReader.ReadFromFile(ListShow.SelectedValue.ToString());
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

        private void Search_Click(object sender, RoutedEventArgs e) {
            if(!string.IsNullOrWhiteSpace(Search.Text) && PdmModels.Any()) {
                //var source = PdmModels.Where(r => r.Tables.Any(t => Search.Text.ToLower().Contains(t.Code.ToLower())));
                var source = PdmModels.SelectMany(r => r.Tables).Where(r => Search.Text.ToLower().Contains(r.Code.ToLower()) || Search.Text.ToLower().Contains(r.Name.ToLower()));
                var searchWindow = new SearchWindow {
                    Title = string.Format("关键字：{0}", Search.Text),
                    Search = {
                        IsReadOnly = true,
                        CanUserAddRows = false,
                        ItemsSource = source.SelectMany(r => r.Columns)
                    }
                };
                searchWindow.Show();
            }
        }

        private void Lock_Click(object sender, RoutedEventArgs e) {
            if(Lock.IsChecked != null && Lock.IsChecked == true) {
                LockAll.IsEnabled = false;
                return;
            }
            if(Lock.IsChecked != null && Lock.IsChecked == false) {
                LockAll.IsEnabled = true;
            }
        }
    }
}
