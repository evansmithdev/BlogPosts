using System.Collections.Generic;
using GalaSoft.MvvmLight;

namespace dev.evansmith.SqlScrewups.ViewModels
{
    public class LegacyTerritoryViewModel : ViewModelBase
    {
        public List<string> TerritoryIds { get; set; } = new List<string> {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10"};
        public string TerritoryId { get; set; } = "10";

        public string GenerateSql()
        {
            return string.IsNullOrWhiteSpace(TerritoryId)
                 ? string.Empty
                 : $"Sales.SalesOrderHeader.CustomerID IN (SELECT Sales.Customer.CustomerID FROM Sales.Customer WHERE Sales.Customer.TerritoryID = {TerritoryId})";
        }
    }
}