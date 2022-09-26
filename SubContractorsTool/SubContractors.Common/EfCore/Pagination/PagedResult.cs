using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace SubContractors.Common.EfCore.Pagination
{
    public abstract class PagedResultBase
    {
        public int CurrentPage { get; }
        public int ResultsPerPage { get; }
        public int TotalPages { get; }
        public long TotalResults { get; }

        protected PagedResultBase()
        { }

        protected PagedResultBase(int currentPage, int resultsPerPage, int totalPages, long totalResults)
        {
            CurrentPage = currentPage > totalPages ? totalPages : currentPage;
            ResultsPerPage = resultsPerPage;
            TotalPages = totalPages;
            TotalResults = totalResults;
        }
    }

    public class PagedResult<T> : PagedResultBase
    {
        public IEnumerable<T> Items { get; }
        [JsonIgnore]
        public bool IsEmpty => Items == null || !Items.Any();

        protected PagedResult()
        {
            Items = Enumerable.Empty<T>();
        }

        [JsonConstructor]
        public PagedResult(IEnumerable<T> items, int currentPage, int resultsPerPage, int totalPages, long totalResults)
            : base(currentPage, resultsPerPage, totalPages, totalResults)
        {
            Items = items;
        }

        public static PagedResult<T> Create(IEnumerable<T> items, int currentPage, int resultsPerPage, int totalPages, long totalResults)
        {
            return new(items, currentPage, resultsPerPage, totalPages, totalResults);
        }

        public static PagedResult<T> From(PagedResultBase result, IEnumerable<T> items)
        {
            return new(items, result.CurrentPage, result.ResultsPerPage, result.TotalPages, result.TotalResults);
        }

        public static PagedResult<T> Empty => new();
    }
}