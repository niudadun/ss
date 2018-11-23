using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFlow.Shared.Helpers
{
    /// <summary>
    /// Extension class for DB helper methods.
    /// </summary>
    public static class DBUtils
    {
        /// <summary>
        /// Reloads the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static TEntity Reload<TEntity>(this DbContext context, TEntity entity) where TEntity : class
        {
            return context.Entry(entity).ReloadDB();
        }

        /// <summary>
        /// Reloads the context.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="entity">The entity.</param>
        public static void ReloadContext<TEntity>(this DbContext context, TEntity entity) where TEntity : class
        {
            context.Entry(entity).Reload();
        }

        /// <summary>
        /// Gets the entity key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context">The context.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static object[] GetEntityKey<T>(this DbContext context, T entity) where T : class
        {
            var state = context.Entry(entity);
            var metadata = state.Metadata;
            var key = metadata.FindPrimaryKey();
            var props = key.Properties.ToArray();

            return props.Select(x => x.GetGetter().GetClrValue(entity)).ToArray();
        }

        /// <summary>
        /// Reloads the database.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entry">The entry.</param>
        /// <returns></returns>
        public static TEntity ReloadDB<TEntity>(this EntityEntry<TEntity> entry) where TEntity : class
        {
            if (entry.State == EntityState.Detached)
            {
                return entry.Entity;
            }

            var context = entry.Context;
            var entity = entry.Entity;
            var keyValues = context.GetEntityKey(entity);

            entry.State = EntityState.Detached;

            var newEntity = context.Set<TEntity>().Find(keyValues);
            var newEntry = context.Entry(newEntity);

            foreach (var prop in newEntry.Metadata.GetProperties())
            {
                prop.GetSetter().SetClrValue(entity,
                prop.GetGetter().GetClrValue(newEntity));
            }

            newEntry.State = EntityState.Detached;
            entry.State = EntityState.Unchanged;

            return entry.Entity;
        }

        /// <summary>
        /// Refreshes all.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        public static async Task RefreshAll(this DbContext context, EntityState state)
        {
            foreach (var entity in context.ChangeTracker.Entries().Where(i => i.State == state))
            {
                await entity.ReloadAsync();
            }
        }
       
    }
}
