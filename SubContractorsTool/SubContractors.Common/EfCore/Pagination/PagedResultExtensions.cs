using System.Linq;
using System.Linq.Dynamic.Core;

namespace SubContractors.Common.EfCore.Pagination
{
    public static class PagedResultExtensions
    {
        public static IQueryable<T> OrderAndSort<T>(this IQueryable<T> collection, PagedQueryBase query)
        {
            if (!string.IsNullOrWhiteSpace(query.SortOrder) && query.SortOrder.ToLowerInvariant() != "asc" && query.SortOrder.ToLowerInvariant() != "desc" && query.SortOrder.ToLowerInvariant() != "ascending" && query.SortOrder.ToLowerInvariant() != "descending")
            {
                query.SortOrder = "desc";
            }

            if (!string.IsNullOrWhiteSpace(query.OrderBy))
            {
                var element = collection.FirstOrDefault();
                var properties = element.GetType()
                                        .GetProperties()
                                        .Select(x => x.Name.ToLowerInvariant())
                                        .ToList();

                if (!properties.Contains(query.OrderBy.ToLowerInvariant()))
                {
                    query.OrderBy = "Id";
                }
            }

            if (!string.IsNullOrWhiteSpace(query.OrderBy) && !string.IsNullOrWhiteSpace(query.SortOrder))
            {
                return collection.OrderBy($"{query.OrderBy} {query.SortOrder.ToLowerInvariant()}");
            }

            if (string.IsNullOrWhiteSpace(query.OrderBy) && !string.IsNullOrWhiteSpace(query.SortOrder))
            {
                return collection.OrderBy($"Id {query.SortOrder.ToLowerInvariant()}");
            }

            if (!string.IsNullOrWhiteSpace(query.OrderBy) && string.IsNullOrWhiteSpace(query.SortOrder))
            {
                return collection.OrderBy($"{query.OrderBy} asc");
            }

            if (string.IsNullOrWhiteSpace(query.OrderBy) && string.IsNullOrWhiteSpace(query.SortOrder))
            {
                return collection.OrderBy($"Id asc");
            }

            return collection;
        }
    }
}