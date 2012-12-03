﻿using System;
using System.Linq;
using System.Linq.Expressions;
using CodeTrip.Core.Paging;
using Raven.Client;
using Raven.Client.Indexes;
using Raven.Client.Linq;

namespace Errordite.Core.Session
{
    public static class SessionExtensions
    {
        public static Page<TEntity> GetPage<TEntity, TIndex, TOrdProp>(this IDocumentSession session, PageRequestWithSort PageRequestWithSort, Expression<Func<TEntity, bool>> whereClause = null, Expression<Func<TEntity, TOrdProp>> orderByClause = null, bool orderDescending = false)
            where TIndex : AbstractIndexCreationTask, new()
        {
            RavenQueryStatistics stats;

            var entities = session.Query<TEntity, TIndex>();

            if (whereClause != null)
                entities = entities.Where(whereClause);

            //TODO: check this works ok
            entities = entities.Statistics(out stats);

            if (orderByClause != null)
                entities = orderDescending ? entities.OrderByDescending(orderByClause) : entities.OrderBy(orderByClause);

            var retrievedEntities = entities.Skip((PageRequestWithSort.PageNumber - 1)*PageRequestWithSort.PageSize).Take(PageRequestWithSort.PageSize).ToList();

            return new Page<TEntity>(retrievedEntities, new PagingStatus(PageRequestWithSort.PageSize, PageRequestWithSort.PageNumber, stats.TotalResults));
        }
    }
}
