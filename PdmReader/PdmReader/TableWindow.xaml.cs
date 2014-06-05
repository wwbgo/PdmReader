using System.Windows.Input;
using PdmReader.Models.PdmModels;

namespace PdmReader {
    public partial class TableWindow {
        public TableWindow() {
            InitializeComponent();
            TableWindow1.KeyDown += (sender, e) => {
                if(e.Key == Key.Escape)
                    TableWindow1.Close();
            };
        }

        private void Tables_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            TableInfoShow();
        }
        private void Views_KeyDown(object sender, KeyEventArgs e) {
            if(e.Key != Key.Enter) return;
            TableInfoShow();
        }

        private void Views_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var view = Views.CurrentItem as ViewInfo;
            if(view == null) return;
            var tableInfo = new TableInfoWindow {
                Title = string.Format("{0}(视图)({1})", view.Name, view.Code),
                TableInfo = {
                    ItemsSource = view.Columns,
                    IsReadOnly = true
                }
            };
            tableInfo.Show();
        }

        private void TableInfoShow() {
            var table = Tables.CurrentItem as TableInfo;
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
