using System.Windows.Input;
using PdmReader.Models.PdmModels;

namespace PdmReader {
    public partial class SearchWindow {
        public SearchWindow() {
            InitializeComponent();
            SearchWindow1.KeyDown += (sender, e) => {
                if(e.Key == Key.Escape)
                    SearchWindow1.Close();
            };
        }

        private void Search_OnMouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var table = Search.CurrentItem as TableInfo;
            if(table == null) return;
            var tableInfo = new TableInfoWindow {
                Title = string.Format("{0}(表)({1})", table.Name, table.Code),
                TableInfo = {
                    ItemsSource = table.Columns,
                    IsReadOnly = true
                }
            };
            tableInfo.Show();
        }
    }
}
