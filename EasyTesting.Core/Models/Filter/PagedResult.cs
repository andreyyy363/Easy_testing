namespace EasyTesting.Core.Models.Filter
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();
        public int Count => Data.Count();
        public int Total { get; set; }
        public int Skip { get; set; }
        public int Limit { get; set; }

        internal static PagedResult<T> Create(IEnumerable<T> data, int total, int skip, int limit)
        {
            return new PagedResult<T>
            {
                Data = data,
                Total = total,
                Skip = skip,
                Limit = limit
            };
        }
    }
}
