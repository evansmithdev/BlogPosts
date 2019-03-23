using System;
using System.Data;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace dev.evansmith.SqlScrewups.ViewModels
{
    public abstract class GetOrdersViewModelBase : ViewModelBase
    {
        private bool _filterOnOrderDateRangeBegin = true;

        public bool FilterOnOrderDateRangeBegin
        {
            get => _filterOnOrderDateRangeBegin;
            set => Set(nameof(FilterOnOrderDateRangeBegin), ref _filterOnOrderDateRangeBegin, value, true);
        }

        public DateTime OrderDateRangeBegin { get; set; } = new DateTime(2013, 07, 01);

        private bool _filterOnOrderDateRangeEnd = true;

        public bool FilterOnOrderDateRangeEnd
        {
            get => _filterOnOrderDateRangeEnd;
            set => Set(nameof(FilterOnOrderDateRangeEnd), ref _filterOnOrderDateRangeEnd, value, true);
        }

        public DateTime OrderDateRangeEnd { get; set; } = new DateTime(2014, 07, 01);

        public bool OnlineOrderFlag { get; set; } = true;

        private bool _filterOnCustomerId = true;

        public bool FilterOnCustomerId
        {
            get => _filterOnCustomerId;
            set => Set(nameof(FilterOnCustomerId), ref _filterOnCustomerId, value, true);
        }

        public string CustomerId { get; set; } = "11091";
        public RelayCommand SearchCommand => new RelayCommand(Search);

        protected readonly MainWindowViewModel MainWindowViewModel;

        protected GetOrdersViewModelBase(MainWindowViewModel mainWindowViewModel)
        {
            MainWindowViewModel = mainWindowViewModel;
        }

        protected abstract void Search();

        private DataTable _data;

        public DataTable Data
        {
            get => _data;
            set
            {
                Set(nameof(Data), ref _data, value, true);
                RaisePropertyChanged(nameof(RecordCount));
            }
        }

        public int RecordCount => Data?.Rows.Count ?? 0;
    }
}