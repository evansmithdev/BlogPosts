using System;
using System.Data;
using System.Data.SqlClient;

namespace dev.evansmith.SqlScrewups.ViewModels
{
    public class GetOrdersDynamicSpViewModel : GetOrdersViewModelBase
    {
        public GetOrdersDynamicSpViewModel(MainWindowViewModel mainWindowViewModel) : base(mainWindowViewModel)
        {
            OrderDateRangeEnd = OrderDateRangeBegin.AddDays(5);
            FilterOnCustomerId = false;
        }

        protected override void Search()
        {
            using (var connection = new SqlConnection(MainWindowViewModel.GetSqlConnectionString()))
            {
                connection.Open();
                var cmd = new SqlCommand 
                          { 
                              Connection = connection, 
                              CommandType = CommandType.StoredProcedure,
                              CommandText = "Sales.GetOrdersDynamic"
                };

                cmd.Parameters.Add("@OrderDateRangeBegin", SqlDbType.DateTime2, 7).Value = FilterOnOrderDateRangeBegin ? OrderDateRangeBegin : (object)DBNull.Value;
                cmd.Parameters.Add("@OrderDateRangeEnd", SqlDbType.DateTime2, 7).Value = FilterOnOrderDateRangeEnd ? OrderDateRangeEnd : (object)DBNull.Value;
                cmd.Parameters.Add("@OnlineOrderFlag", SqlDbType.Bit).Value = OnlineOrderFlag;
                cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = FilterOnCustomerId && int.TryParse(CustomerId, out var customerId) ? customerId : (object)DBNull.Value;
                cmd.Parameters.Add("@WildWestSql", SqlDbType.NVarChar).Value = string.IsNullOrWhiteSpace(LegacyTerritoryViewModel.GenerateSql()) ? (object)DBNull.Value : LegacyTerritoryViewModel.GenerateSql();

                var sda = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                sda.Fill(dt);
                Data = dt;
            }
        }

        public LegacyTerritoryViewModel LegacyTerritoryViewModel { get; set; } = new LegacyTerritoryViewModel();
    }
}