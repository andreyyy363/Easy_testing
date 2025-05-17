using EasyTesting.Core.Models.Filter;
using Microsoft.AspNetCore.Mvc;

namespace EasyTesting.Core.Utils
{
    public class ResultBuilder<T>
    {
        private int _statusCode = 200;
        private string _contentType = "application/json";
        private IEnumerable<T> _data = Enumerable.Empty<T>();
        private int? _count;
        private int? _total;
        private int? _skip;
        private int? _limit;

        public ResultBuilder<T> WithPagedResult(PagedResult<T> pagedResult)
        {
            _data = pagedResult.Data;
            _count = pagedResult.Count;
            _total = pagedResult.Total;
            _skip = pagedResult.Skip;
            _limit = pagedResult.Limit;
            return this;
        }

        public ResultBuilder<T> WithStatusCode(int statusCode)
        {
            _statusCode = statusCode;
            return this;
        }

        public ResultBuilder<T> WithContentType(string contentType)
        {
            _contentType = contentType;
            return this;
        }

        public ObjectResult Build()
        {
            var result = new
            {
                data = _data,
                pagination = _total.HasValue ? new
                {
                    count = _count,
                    total = _total,
                    skip = _skip,
                    limit = _limit
                } : null
            };

            return new ObjectResult(result)
            {
                StatusCode = _statusCode,
                ContentTypes = { _contentType }
            };
        }
    }
}
