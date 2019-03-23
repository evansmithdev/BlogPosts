using System;
using System.Data;
using System.Data.SqlClient;

namespace dev.evansmith.SqlScrewups.ViewModels
{
    public class GetOrdersInlineViewModel : GetOrdersViewModelBase
    {
        public GetOrdersInlineViewModel(MainWindowViewModel mainWindowViewModel) : base(mainWindowViewModel)
        {
            OrderDateRangeEnd = OrderDateRangeBegin.AddDays(5);
            FilterOnCustomerId = false;
        }

        protected override void Search()
        {
            using (var connection = new SqlConnection(MainWindowViewModel.GetSqlConnectionString()))
            {
                connection.Open();
                var cmd = new SqlCommand  {  Connection = connection };
                cmd.CommandText = $@"
SELECT *
FROM Sales.SalesOrderHeader
{GetFilters(cmd)}
ORDER BY SalesOrderID;";

                var sda = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                sda.Fill(dt);
                Data = dt;
            }
        }

        private string GetFilters(SqlCommand cmd)
        {
            var filters = string.Empty;

            if (FilterOnOrderDateRangeBegin)
            {
                AddFilter(ref filters, "OrderDate >= @OrderDateRangeBegin");
                cmd.Parameters.Add("@OrderDateRangeBegin", SqlDbType.DateTime2).Value = OrderDateRangeBegin;
            }

            if (FilterOnOrderDateRangeEnd)
            {
                AddFilter(ref filters, "OrderDate < @OrderDateRangeEnd");
                cmd.Parameters.Add("@OrderDateRangeEnd", SqlDbType.DateTime2).Value = OrderDateRangeEnd;
            }

            if (true)
            {
                AddFilter(ref filters, "OnlineOrderFlag = @OnlineOrderFlag");
                cmd.Parameters.Add("@OnlineOrderFlag", SqlDbType.Bit).Value = OnlineOrderFlag;
            }

            if (FilterOnCustomerId &&
                int.TryParse(CustomerId, out var customerId))
            {
                AddFilter(ref filters, "CustomerID = @CustomerID");
                cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = customerId;
            }

            if (!string.IsNullOrWhiteSpace(LegacyTerritoryViewModel.GenerateSql()))
            {
                AddFilter(ref filters, LegacyTerritoryViewModel.GenerateSql());
            }

            return filters;
        }

        private static void AddFilter(ref string filters, string filter)
        {
            if (string.IsNullOrWhiteSpace(filter)) filters += filter;
            if (string.IsNullOrWhiteSpace(filters)) filters += $"WHERE {filter}";
            filters += $"  AND {filter}";
        }

        public LegacyTerritoryViewModel LegacyTerritoryViewModel { get; set; } = new LegacyTerritoryViewModel();
    }
}