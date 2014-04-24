using System.Windows.Input;
using PdmReader.Models.PdmModels;

namespace PdmReader {
    public partial class TableWindow {
        public TableWindow() {
            InitializeComponent();
        }

        private void Tables_MouseEnterLeave(object sender, MouseEventArgs e) {

        }

        private void Tables_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var table = Tables.CurrentItem as TableInfo;
            if(table == null) return;
            var tableInfo = new TableInfoWindow {
                Title = string.Format("{0}(表)", table.Name),
                TableInfo = {
                    ItemsSource = table.Columns,
                    IsReadOnly = true
                }
            };
            tableInfo.Show();
        }

        private void Views_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var view = Views.CurrentItem as ViewInfo;
            if(view == null) return;
            var tableInfo = new TableInfoWindow {
                Title = string.Format("{0}(视图)", view.Name),
                TableInfo = {
                    ItemsSource = view.Columns,
                    IsReadOnly = true
                }
            };
            tableInfo.Show();
        }

        private void TableWindow1_KeyDown(object sender, KeyEventArgs e) {
            if(e.Key == Key.Escape)
                TableWindow1.Close();
        }

    }
}
