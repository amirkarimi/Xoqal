namespace Xoqal.Web.Mvc.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Represents data related extensions.
    /// </summary>
    public static class DataExtensions
    {
        /// <summary>
        /// To the paginated view model.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paginatedData">The paginated data.</param>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        public static Models.Paginated<T> ToPaginatedViewModel<T>(this Xoqal.Core.Models.IPaginated<T> paginatedData, Xoqal.Core.Models.IPaginatedCriteria criteria)
        {
            var paginatedViewModel = new Models.Paginated<T>(
                paginatedData.Data,
                paginatedData.TotalRowsCount,
                criteria.Page ?? 1,
                criteria.PageSize);

            return paginatedViewModel;
        }
    }
}
