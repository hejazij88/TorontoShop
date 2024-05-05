using System.Linq;

namespace TorontoShop.Domain.ViewModel.Paging
{
    public static class PagingExtensions
    {
        public static IQueryable<T> Paging<T>(this IQueryable<T> query,BasePaging basePaging)
        {
            return query.Skip(basePaging.SkipEntity).Take(basePaging.TakeEntity);
        }
    }
}
