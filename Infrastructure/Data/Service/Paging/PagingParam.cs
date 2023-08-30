namespace Infrastructure.Data.Service.Paging
{
    public class PagingParam
    {
        private const int MaxPageSize = 50;
        public const int DefaultPageSize = 10;
        private int _pageSize { get; set; } = DefaultPageSize;
        public int PageIndex { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
    }
}
