using dev.evansmith.SqlScrewups.ViewModels;

namespace dev.evansmith.SqlScrewups
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}