using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using Entities.Models;

namespace Repository.Extensions
{
    public static class GameRepositoryExtensions
    {
        // #region Fitler by Category

        public static IQueryable<Game> FilterGamesByCategory(this IQueryable<Game>
            games, string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                return games;
          
            string[] categories = categoryName.Split(',');

            //Todo not done
            return games.Where(g=>g.Categories.Any(c=>categoryName.Contains(c.Title)));
        }

        // #endregion

        #region Filter by Date

        public static IQueryable<Game> FilterGamesByDate(this IQueryable<Game>
            games, DateTime from, DateTime to)
        {
            if (from == default)
                return games;

            return games.Where(p => p.GameDate >= from && p.GameDate <= to);
        }

        #endregion

        #region Search

        public static IQueryable<Game> Search(this IQueryable<Game>
            games, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return games;


            return games.Where(p => p.Title.ToLower().Contains(searchTerm) ||
                                    p.Body.Contains(searchTerm));
        }

        #endregion


        //install System.Linq.Dynamic.Core
        public static IQueryable<Game> Sort(this IQueryable<Game> games, string
            orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return games.OrderBy(p => p.GameDate);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Game).GetProperties(BindingFlags.Public |
                                                           BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi =>
                    pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";

                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction},");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            if (string.IsNullOrWhiteSpace(orderQuery))
                return games.OrderBy(p => p.GameDate);

            return games.OrderBy(orderQuery);
        }
    }
}