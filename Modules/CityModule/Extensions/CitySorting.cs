using Core.Models;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modules.CityModule.Extensions
{
    public static class CitySorting
    {
        public static IQueryable<City> ApplySorting(this IQueryable<City> query, Filter filter)
        {
            var by = filter.SortBy?.ToUpper() ?? "ID";
            var direction = filter.SortDirection?.ToUpper() ?? "ASC";

            switch (by)
            {
                case "ID":
                    switch (direction)
                    {
                        case "ASC":
                            query = query.OrderBy(t => t.Id);
                            break;
                        case "DESC":
                            query = query.OrderByDescending(t => t.Id);
                            break;
                    }
                    break;
                default:
                    query = query.OrderBy(t => t.Id);
                    break;
            }

            return query;
        }
    }
}
