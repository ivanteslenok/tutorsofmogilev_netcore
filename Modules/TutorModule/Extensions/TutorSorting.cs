using System.Linq;
using Core.Models;
using Data.Entities;

namespace Modules.TutorModule.Extensions
{
    public static class TutorSorting
    {
        public static IQueryable<Tutor> ApplySorting(this IQueryable<Tutor> query, Filter filter)
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
                case "LASTNAME":
                    switch (direction)
                    {
                        case "ASC":
                            query = query.OrderBy(t => t.LastName);
                            break;
                        case "DESC":
                            query = query.OrderByDescending(t => t.LastName);
                            break;
                    }
                    break;
                case "RATING":
                    switch (direction)
                    {
                        case "ASC":
                            query = query.OrderBy(t => t.Rating);
                            break;
                        case "DESC":
                            query = query.OrderByDescending(t => t.Rating);
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
