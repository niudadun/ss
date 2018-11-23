﻿using Microsoft.EntityFrameworkCore;
using SmartFlow.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using SmartFlow.Shared.Data;
using SmartFlow.Shared.Respository.Interfaces;
using SmartFlow.Shared.Helpers;
using SmartFlow.Shared.Data.DataModels;

namespace SmartFlow.Shared.Respository
{
    /// <summary>
    /// Class to define all methods from ProfileRepository Interface
    /// </summary>
    public class ProfilesRepository : IProfilesRepository
    {
        private static string TAG = "ProfilesRepository";

        private readonly DatabaseContext _databaseContext;

        /// <summary>
        /// ProfilesRepository constructor
        /// </summary>
        /// <param name="dbContext"></param>
        public ProfilesRepository(DatabaseContext dbContext)
        {
            _databaseContext = dbContext;
        }

        /// <summary>
        /// Method to get all the profiles available in database.
        /// </summary>
        /// <returns> Profile list.</returns>
        public async Task<IEnumerable<Profile_Dbo>> GetProfilesAsync()
        {
            try
            {
                var profiles = await _databaseContext.Profiles.ToListAsync();
                return profiles;
            }
            catch (Exception e)
            {
                LogHandler.AddExceptionLog(TAG, "", e, true);
                return null;
            }
        }

        /// <summary>
        /// Get profile based on provided ID query.
        /// </summary>
        /// <param name="id">Selected profile ID</param>
        /// <returns></returns>
        public async Task<Profile_Dbo> GetProfileByIdAsync(int id)
        {
            try
            {
                var profile = await _databaseContext.Profiles
                                     .Include(i => i.Questions)
                                     .SingleAsync(i => i.Id == id);

                return profile;
            }
            catch (Exception e)
            {
                LogHandler.AddExceptionLog(TAG, "", e, true);
                return null;
            }
        }

        /// <summary>
        /// Add new profile to database.
        /// </summary>
        /// <param name="profile">Selected profle</param>
        /// <returns></returns>
        public async Task<bool> AddProfileAsync(Profile_Dbo profile)
        {
            try
            {
                var test = Task.Delay(1000);
                await test;
                var tracking = await _databaseContext.Profiles.AddAsync(profile);            
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
        /// Update existing profile.
        /// </summary>
        /// <param name="profile">Selected profile</param>
        /// <returns></returns>
        public async Task<bool> UpdateProfileAsync(Profile_Dbo profile)
        {
            try
            {
                var test = Task.Delay(1000);
                await test;
                var tracking = _databaseContext.Update(profile);

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
        /// Remove profile based on provided ID.
        /// </summary>
        /// <param name="id"> selected iD of profile</param>
        /// <returns></returns>
        public async Task<bool> RemoveProfileAsync(int id)
        {
            try
            {
                var test = Task.Delay(1000);
                await test;
                var product = await _databaseContext.Profiles.FindAsync(id);

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
        /// Get all profiles in database based on certain conditions.
        /// </summary>
        /// <param name="predicate">Query conditions.</param>
        /// <returns></returns>
        public async Task<IEnumerable<Profile_Dbo>> QueryProfilesAsync(Func<Profile_Dbo, bool> predicate)
        {
            try
            {
                var profiles = _databaseContext.Profiles.Where(predicate);

                return profiles.ToList();
            }
            catch (Exception e)
            {
                LogHandler.AddExceptionLog(TAG, "", e, true);
                return null;
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
