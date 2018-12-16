using System.Linq;
using Core.Models;
using Data.Entities;

namespace Modules.TutorModule.Extensions
{
    public static class TutorSorting
    {
        public static IQueryable<Tutor> ApplySorting(this IQueryable<Tutor> query, Filter filter)
        {
            var by = filter.SortBy?.ToUpper() ?? "LASTNAME";
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
                case "FIRSTNAME":
                    switch (direction)
                    {
                        case "ASC":
                            query = query.OrderBy(t => t.FirstName);
                            break;
                        case "DESC":
                            query = query.OrderByDescending(t => t.FirstName);
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
                case "PATRONYMIC":
                    switch (direction)
                    {
                        case "ASC":
                            query = query.OrderBy(t => t.Patronymic);
                            break;
                        case "DESC":
                            query = query.OrderByDescending(t => t.Patronymic);
                            break;
                    }
                    break;
                case "DESCRIPTION":
                    switch (direction)
                    {
                        case "ASC":
                            query = query.OrderBy(t => t.Description);
                            break;
                        case "DESC":
                            query = query.OrderByDescending(t => t.Description);
                            break;
                    }
                    break;
                case "EDUCATION":
                    switch (direction)
                    {
                        case "ASC":
                            query = query.OrderBy(t => t.Education);
                            break;
                        case "DESC":
                            query = query.OrderByDescending(t => t.Education);
                            break;
                    }
                    break;
                case "JOB":
                    switch (direction)
                    {
                        case "ASC":
                            query = query.OrderBy(t => t.Job);
                            break;
                        case "DESC":
                            query = query.OrderByDescending(t => t.Job);
                            break;
                    }
                    break;
                case "ADDRESS":
                    switch (direction)
                    {
                        case "ASC":
                            query = query.OrderBy(t => t.Address);
                            break;
                        case "DESC":
                            query = query.OrderByDescending(t => t.Address);
                            break;
                    }
                    break;
                case "EXPERIENCE":
                    switch (direction)
                    {
                        case "ASC":
                            query = query.OrderBy(t => t.Experience);
                            break;
                        case "DESC":
                            query = query.OrderByDescending(t => t.Experience);
                            break;
                    }
                    break;
                case "COST":
                    switch (direction)
                    {
                        case "ASC":
                            query = query.OrderBy(t => t.Cost);
                            break;
                        case "DESC":
                            query = query.OrderByDescending(t => t.Cost);
                            break;
                    }
                    break;
                case "ISVISIBLE":
                    switch (direction)
                    {
                        case "ASC":
                            query = query.OrderBy(t => t.IsVisible);
                            break;
                        case "DESC":
                            query = query.OrderByDescending(t => t.IsVisible);
                            break;
                    }
                    break;
                case "DISTRICT":
                    switch (direction)
                    {
                        case "ASC":
                            query = query.OrderBy(t => t.District.Name);
                            break;
                        case "DESC":
                            query = query.OrderByDescending(t => t.District.Name);
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
            }

            return query;
        }
    }
}
