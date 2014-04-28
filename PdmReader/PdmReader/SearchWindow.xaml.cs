using System.Windows.Input;

namespace PdmReader {
    public partial class SearchWindow {
        public SearchWindow() {
            InitializeComponent();
            SearchWindow1.KeyDown += (sender, e) => {
                if(e.Key == Key.Escape)
                    SearchWindow1.Close();
            };
        }
    }
}
