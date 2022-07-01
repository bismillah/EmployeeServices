namespace EmployeeServices.Wpf.ViewModels
{
    public class Pagination : ViewModelBase
    {
        private int _total;
        public int Total
        {
            get
            {
                return _total;
            }
            set
            {
                _total = value;
                OnPropertyChanged(nameof(Total));
            }
        }

        private int _totalPages;
        public int TotalPages
        {
            get
            {
                return _totalPages;
            }
            set
            {
                _totalPages = value;
                OnPropertyChanged(nameof(TotalPages));
            }
        }

        private int _currentPage;
        public int CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }
        private int _limit;
        public int Limit
        {
            get
            {
                return _limit;
            }
            set
            {
                _limit = value;
                OnPropertyChanged(nameof(Limit));
            }
        }

        private int _pageCount;
        public int PageCount
        {
            get
            {
                return _pageCount;
            }
            set
            {
                _pageCount = value;
                OnPropertyChanged(nameof(PageCount));
            }
        }
    }
}
