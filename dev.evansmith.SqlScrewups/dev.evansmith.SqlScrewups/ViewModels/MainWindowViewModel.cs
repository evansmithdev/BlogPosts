using System.Data.SqlClient;
using System.Security.Principal;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace dev.evansmith.SqlScrewups.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Server { get; set; } = @"ESMITHLT\SQLEXPRESS";
        public string Database { get; set; } = "AdventureWorks2014_SqlScrewups";
        public string UserId { get; set; } = WindowsIdentity.GetCurrent().Name;
        public RelayCommand TestConnectionCommand => new RelayCommand(TestConnection);

        public MainWindowViewModel()
        {
            GetOrdersForCustomerInDateRangeViewModel = new OrdersForCustomerInDateRangeViewModel(this);
            GetOrdersSpViewModel = new GetOrdersSpViewModel(this);
            GetOrdersSpWithDynamicViewModel = new GetOrdersDynamicSpViewModel(this);
            GetOrdersInlineViewModel = new GetOrdersInlineViewModel(this);
        }

        public string GetSqlConnectionString()
        {
            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder
            {
                ApplicationIntent = ApplicationIntent.ReadWrite,
                ApplicationName = "SQL Screwups",
                IntegratedSecurity = true,
                DataSource = Server,
                InitialCatalog = Database,
                ConnectTimeout = 1
            };
            return sqlConnectionStringBuilder.ConnectionString;
        }

        private void TestConnection()
        {
            try
            {
                // query data to test the connection
                using (var connection = new SqlConnection(GetSqlConnectionString()))
                {
                    connection.Open();
                    var cmd = new SqlCommand { Connection = connection, CommandText = "SELECT TOP 1 SalesOrderID FROM Sales.SalesOrderHeader" };
                    cmd.ExecuteScalar();
                }
                MessageBox.Show("Connection is legit.");
            }
            catch
            {
                MessageBox.Show("Connection failed.");
            }
        }

        private OrdersForCustomerInDateRangeViewModel _getOrdersForCustomerInDateRangeSpViewModel;

        public OrdersForCustomerInDateRangeViewModel GetOrdersForCustomerInDateRangeViewModel
        {
            get => _getOrdersForCustomerInDateRangeSpViewModel;
            set => Set(nameof(GetOrdersForCustomerInDateRangeViewModel), ref _getOrdersForCustomerInDateRangeSpViewModel, value, true);
        }

        private GetOrdersSpViewModel _getOrdersSpViewModel;

        public GetOrdersSpViewModel GetOrdersSpViewModel
        {
            get => _getOrdersSpViewModel;
            set => Set(nameof(GetOrdersSpViewModel), ref _getOrdersSpViewModel, value, true);
        }

        public GetOrdersDynamicSpViewModel GetOrdersSpWithDynamicViewModel { get; set; }

        public GetOrdersInlineViewModel GetOrdersInlineViewModel { get; set; }
    }
}