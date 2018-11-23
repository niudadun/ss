using SmartFlow.Shared.Data;
using SmartFlow.Shared.Models;
using SmartFlow.Shared.Respository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SmartFlow.Shared.Models.Ede.Enums;
using SmartFlow.Shared.Helpers;
using SmartFlow.Shared.Data.DataModels;

namespace SmartFlow.Shared.Respository
{
    /// <summary>
    /// Class to define all methods from Declaration Interface
    /// </summary>
    public class DeclarationsRepository : IDeclarationsRepository
    {
        private static string TAG = "DeclarationsRepository";

        private readonly DatabaseContext _databaseContext;

        /// <summary>
        /// Declaration Repository for database handling
        /// </summary>
        /// <param name="dbContext"></param>
        public DeclarationsRepository(DatabaseContext dbContext)
        {
            _databaseContext = dbContext;
        }

        /// <summary>
        /// Add declaration method
        /// </summary>
        /// <param name="declaration"></param>
        /// <returns></returns>
        public async Task<bool> AddDeclarationAsync(Declaration_Dbo declaration)
        {
            try
            {
                var test = Task.Delay(1000);
                await test;

                var tracking = await _databaseContext.Declarations.AddAsync(declaration);

                await _databaseContext.SaveChangesAsync();

                var isAdded = tracking.State == EntityState.Added;

                return isAdded;
            }
            catch (Exception e)
            {
                LogHandler.AddExceptionLog(TAG, "", e, true);
                return false;
            }
        }

        /// <summary>
        /// Get declaration by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Declaration_Dbo> GetDeclarationByIdAsync(int id)
        {
            try
            {
                var declaration = await _databaseContext.Declarations
                                         .Include(i => i.Profile)
                                             .ThenInclude(i => i.Questions)
                                         .Include(i => i.Questions)
                                         .SingleAsync(i => i.Id == id);

                return declaration;
            }
            catch (Exception e)
            {
                LogHandler.AddExceptionLog(TAG, "", e, true);
                return null;
            }
        }

        /// <summary>
        /// Get all declarations
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Declaration_Dbo>> GetDeclarationsAsync()
        {
            try
            {
                var declarations = await _databaseContext.Declarations
                                       .Include(i => i.Profile)
                                             .ThenInclude(i => i.Questions)
                                         .Include(i => i.Questions)
                                       .ToListAsync();
                return declarations;
            }
            catch (Exception e)
            {
                LogHandler.AddExceptionLog(TAG, "", e, true);
                return null;
            }
        }

        /// <summary>
        /// Get declaration by profile id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Declaration_Dbo>> GetDeclarationByProfileIdAsync(int id)
        {
            try
            {
                var declarations = await _databaseContext.Declarations.Where(i => i.ProfileId == id)
                                        .Include(i => i.Profile)
                                             .ThenInclude(i => i.Questions)
                                        .Include(i => i.Questions)
                                        .ToListAsync();

                return declarations;
            }
            catch (Exception e)
            {
                LogHandler.AddExceptionLog(TAG, "", e, true);
                return null;
            }
        }

       /// <summary>
       /// Get declarations by declaration type
       /// </summary>
       /// <param name="Type"></param>
       /// <returns></returns>
        public async Task<Declaration_Dbo> GetDeclarationByTypeAsync(DeclarationType Type)
        {
            try
            {
                var declaration =  await _databaseContext.Declarations
                                        .Include(i => i.Profile)
                                             .ThenInclude(i => i.Questions)
                                        .Include(i => i.Questions)
                                        .SingleAsync(i => i.DeclarationType == Type);

                return declaration;
            }
            catch (Exception e)
            {
                LogHandler.AddExceptionLog(TAG, "", e, true);
                return null;
            }
        }

        /// <summary>
        /// Get declaration by params passed
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Declaration_Dbo>> QueryDeclarationsAsync(Func<Declaration_Dbo, bool> predicate)
        {
            try
            {
                var declarations = _databaseContext.Declarations.Where(predicate);

                return declarations.ToList();
            }
            catch (Exception e)
            {
                LogHandler.AddExceptionLog(TAG, "", e, true);
                return null;
            }
        }

        /// <summary>
        /// Remove declaration by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> RemoveDeclarationAsync(int id)
        {
            try
            {
                var test = Task.Delay(1000);
                await test;
                var product = await _databaseContext.Declarations.FindAsync(id);

                var tracking = _databaseContext.Remove(product);

                await _databaseContext.SaveChangesAsync();

                var isDeleted = tracking.State == EntityState.Deleted;

                return isDeleted;
            }
            catch (Exception e)
            {
                LogHandler.AddExceptionLog(TAG, "", e, true);
                return false;
            }
        }

        /// <summary>
        /// Update declaration
        /// </summary>
        /// <param name="declaration"></param>
        /// <returns></returns>
        public async Task<bool> UpdateDeclarationAsync(Declaration_Dbo declaration)
        {
            try
            {
                var test = Task.Delay(1000);
                await test;
                var tracking = _databaseContext.Update(declaration);
                await _databaseContext.SaveChangesAsync();             
                var isModified = tracking.State == EntityState.Modified;
                return isModified;
            }
            catch (Exception e)
            {
                LogHandler.AddExceptionLog(TAG, "", e, true);
                return false;
            }
        }


        /// <summary>
        /// Refreshes the database context.
        /// </summary>
        /// <param name="state">The state.</param>
        public async Task<bool> RefreshDbContext(EntityState state = EntityState.Modified)
        {
            var test = Task.Delay(1000);
            await test;
            await _databaseContext.RefreshAll(state);          
            return true;
        }
    }
}
