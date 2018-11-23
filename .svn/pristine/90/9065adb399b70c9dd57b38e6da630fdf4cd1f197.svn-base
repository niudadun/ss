using Microsoft.EntityFrameworkCore;
using SmartFlow.Shared.Data.DataModels;
using SmartFlow.Shared.Models;
using SmartFlow.Shared.Models.Ede.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFlow.Shared.Respository.Interfaces
{
    /// <summary>
    /// Interface for Declaration Database
    /// </summary>
    public interface IDeclarationsRepository
    {
        /// <summary>
        /// Get all declarations
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Declaration_Dbo>> GetDeclarationsAsync();

        /// <summary>
        /// Get declaration by profile id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<Declaration_Dbo>> GetDeclarationByProfileIdAsync(int id);

        /// <summary>
        /// Get declaration by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Declaration_Dbo> GetDeclarationByIdAsync(int id);

        /// <summary>
        /// Get declarations by declaration type
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        Task<Declaration_Dbo> GetDeclarationByTypeAsync(DeclarationType Type);

        /// <summary>
        /// Add declaration method
        /// </summary>
        /// <param name="declaration"></param>
        /// <returns></returns>
        Task<bool> AddDeclarationAsync(Declaration_Dbo declaration);

        /// <summary>
        /// Update declaration
        /// </summary>
        /// <param name="declaration"></param>
        /// <returns></returns>
        Task<bool> UpdateDeclarationAsync(Declaration_Dbo declaration);

        /// <summary>
        /// Remove declaration by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> RemoveDeclarationAsync(int id);

        /// <summary>
        /// Get declaration by params passed
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IEnumerable<Declaration_Dbo>> QueryDeclarationsAsync(Func<Declaration_Dbo, bool> predicate);

        /// <summary>
        /// Refreshes the database context.
        /// </summary>
        /// <param name="state">The state.</param>
        Task<bool> RefreshDbContext(EntityState state = EntityState.Modified);

    }
}
