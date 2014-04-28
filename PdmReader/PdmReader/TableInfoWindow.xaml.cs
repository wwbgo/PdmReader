using System.Windows.Input;

namespace PdmReader {
    public partial class TableInfoWindow {
        public TableInfoWindow() {
            InitializeComponent();
            TableInfoWindow1.KeyDown += (sender, e) => {
                if(e.Key == Key.Escape)
                    TableInfoWindow1.Close();
            };
        }
    }
}
