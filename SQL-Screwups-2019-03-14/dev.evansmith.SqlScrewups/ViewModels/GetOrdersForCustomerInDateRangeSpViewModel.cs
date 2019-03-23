using System;
using System.Data;
using System.Data.SqlClient;

namespace dev.evansmith.SqlScrewups.ViewModels
{
    public class OrdersForCustomerInDateRangeViewModel : GetOrdersViewModelBase
    {
        public OrdersForCustomerInDateRangeViewModel(MainWindowViewModel mainWindowViewModel) : base(mainWindowViewModel)
        {
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
                              CommandText = "Sales.GetOrdersForCustomerInDateRange"
                          };

                cmd.Parameters.Add("@OrderDateRangeBegin", SqlDbType.DateTime2, 7).Value = FilterOnOrderDateRangeBegin ? OrderDateRangeBegin : (object)DBNull.Value;
                cmd.Parameters.Add("@OrderDateRangeEnd", SqlDbType.DateTime2, 7).Value = FilterOnOrderDateRangeEnd ? OrderDateRangeEnd : (object)DBNull.Value;
                cmd.Parameters.Add("@OnlineOrderFlag", SqlDbType.Bit).Value = OnlineOrderFlag;
                cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = FilterOnCustomerId && int.TryParse(CustomerId, out var customerId) ? customerId : (object)DBNull.Value;

                var sda = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                sda.Fill(dt);
                Data = dt;
            }
        }
    }
}