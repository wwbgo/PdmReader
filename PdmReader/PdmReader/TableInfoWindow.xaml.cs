using System.Windows.Input;

namespace PdmReader {
    public partial class TableInfoWindow {
        public TableInfoWindow() {
            InitializeComponent();
        }

        private void TableInfoWindow1_KeyDown(object sender, KeyEventArgs e) {
            if(e.Key == Key.Escape)
                TableInfoWindow1.Close();
        }
    }
}
