// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sofee.Core.Api.Models.Countries;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sofee.Core.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Country> Countries { get; set; }
        
        public async ValueTask<Country> InsertCountryAsync(Country country)
        {
            using var broker = new StorageBroker(this.configuration);

            EntityEntry<Country> countryEntityEntry =
                await broker.Countries.AddAsync(country);

            await broker.SaveChangesAsync();

            return countryEntityEntry.Entity;
        }

        public IQueryable<Country> SelectAllCountries()
        {
            using var broker =
                new StorageBroker(this.configuration);

            return broker.Countries;
        }

        public async ValueTask<Country> SelectCountryByIdAsync(Guid countryId)
        {
            using var broker =
                new StorageBroker(this.configuration);

            return await broker.Countries.FindAsync(countryId);
        }
    }
}
